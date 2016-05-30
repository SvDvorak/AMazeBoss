using System;
using System.Linq;
using Assets;
using Assets.Editor.Undo;
using Assets.LevelEditorUnity;
using FluentAssertions;
using Xunit;

namespace AMazeBoss.CSharp.Tests
{
    public class PuzzleEditorAcceptanceTests : AcceptanceTests<PuzzleEditorAcceptanceTests>
    {
        private readonly PuzzleLayout _sut;
        private readonly TilePos _node1Position = new TilePos(0, 0);
        private readonly TilePos _node2Position = new TilePos(1, 0);
        private readonly TilePos _node3Position = new TilePos(0, 1);
        private readonly TilePos _node4Position = new TilePos(1, 1);
        private int _callsToNodeAdded;
        private int _callsToNodeRemoved;
        private int _callsToConnectionRemoved;
        private int _callsToConnectionAdded;

        public PuzzleEditorAcceptanceTests()
        {
            _sut = new PuzzleLayout();
        }

        public class AddConnection : PuzzleEditorAcceptanceTests
        {
            [Fact]
            public void CreatesTwoConnectedNodesWhenAddingConnectionOnEmptyLayout()
            {
                When
                    .ConnectingBetween(_node1Position, _node2Position);

                Then
                    .ShouldHaveNodeAt(_node1Position, node => node
                        .WithConnectionsTo(_node2Position))
                    .ShouldHaveNodeAt(_node2Position, node => node
                        .WithConnectionsTo(_node1Position));
            }

            [Fact]
            public void DoesNothingWhenAddingConnectionBetweenAlreadyConnectedNodes()
            {
                When
                    .ConnectingBetween(_node1Position, _node2Position)
                    .ConnectingBetween(_node2Position, _node1Position);

                Then
                    .ShouldHaveNodeCountOf(2)
                    .ShouldHaveTotalConnectionCountOf(2);
            }

            [Fact]
            public void OnlyCreatesOneNewNodeWhenAddingConnectionBetweenExistingAndNewNode()
            {
                When
                    .ConnectingBetween(_node1Position, _node2Position)
                    .ConnectingBetween(_node1Position, _node3Position);

                Then
                    .ShouldHaveNodeCountOf(3);
            }

            [Fact]
            public void UpdatesConnectionsWhenAddingConnectionBetweenTwoAlreadyExistingNodes()
            {
                When
                    .ConnectingBetween(_node1Position, _node2Position)
                    .ConnectingBetween(_node3Position, _node4Position)
                    .ConnectingBetween(_node1Position, _node4Position);

                Then
                    .ShouldHaveNodeCountOf(4)
                    .ShouldHaveNodeAt(_node1Position, node => node
                        .WithConnectionsTo(_node2Position, _node4Position));
            }

            [Fact]
            public void SplitsUpLongConnectionIntoSeveralSmaller()
            {
                var start = new TilePos(0, 0);
                var farAwayEnd = new TilePos(2, 0);
                var middlePoint = farAwayEnd/2;

                When
                    .ConnectingBetween(start, farAwayEnd);

                Then
                    .ShouldHaveNodeCountOf(3)
                    .ShouldHaveNodeAt(start, node => node
                        .WithConnectionsTo(middlePoint))
                    .ShouldHaveNodeAt(middlePoint, node => node
                        .WithConnectionsTo(farAwayEnd, start));
            }
        }

        public class RemoveConnection : PuzzleEditorAcceptanceTests
        {
            [Fact]
            public void UpdatesBothNodesWhenRemovingConnection()
            {
                Given
                    .ConnectionBetween(_node1Position, _node2Position)
                    .ConnectionBetween(_node1Position, _node3Position)
                    .ConnectionBetween(_node2Position, _node3Position);

                When
                    .RemovingConnectionBetween(_node1Position, _node2Position);

                Then
                    .ShouldHaveNodeAt(_node1Position, node => node
                        .WithConnectionsTo(_node3Position))
                    .ShouldHaveNodeAt(_node2Position, node => node
                        .WithConnectionsTo(_node3Position));
            }

            [Fact]
            public void RemovesNodesWithNoConnectionsWhenRemovingConnection()
            {
                Given
                    .ConnectionBetween(_node1Position, _node2Position);

                When
                    .RemovingConnectionBetween(_node1Position, _node2Position);

                Then
                    .ShouldHaveNodeCountOf(0);
            }

            [Fact]
            public void DoesNothingIfRemovingConnectionBetweenNodesThatDontExist()
            {
                Given
                    .ConnectionBetween(_node1Position, _node2Position);

                When
                    .RemovingConnectionBetween(_node3Position, _node4Position);

                Then
                    .ShouldHaveNodeCountOf(2);
            }

            [Fact]
            public void DoesNothingIfRemovingConnectionBetweenUnconnectedNodes()
            {
                Given
                    .ConnectionBetween(_node1Position, _node2Position)
                    .ConnectionBetween(_node3Position, _node4Position);

                When
                    .RemovingConnectionBetween(_node1Position, _node4Position);

                Then
                    .ShouldHaveNodeCountOf(4)
                    .ShouldHaveTotalConnectionCountOf(4);
            }

            [Fact]
            public void RemovesSubdividedConnectionsInLongConnection()
            {
                var start = new TilePos(0, 0);
                var farAwayEnd = new TilePos(2, 0);

                Given
                    .ConnectionBetween(start, farAwayEnd);

                When
                    .RemovingConnectionBetween(start, farAwayEnd);

                Then
                    .ShouldHaveNodeCountOf(0)
                    .ShouldHaveTotalConnectionCountOf(0);
            }
        }

        public class LayoutEvents : PuzzleEditorAcceptanceTests
        {
            [Fact]
            public void CallsLayoutChangedEvents()
            {
                Given
                    .ListeningToEvents();

                When
                    .ConnectingBetween(_node1Position, _node2Position)
                    .RemovingConnectionBetween(_node1Position, _node2Position);

                Then
                    .ShouldHaveCalledNodeAdded(2)
                    .ShouldHaveCalledConnectionAdded(1)
                    .ShouldHaveCalledNodeRemoved(2)
                    .ShouldHaveCalledConnectionRemoved(1);
            }
        }

        public class ClearLayout : PuzzleEditorAcceptanceTests
        {
            [Fact]
            public void RemovesAllNodesAndCallsEventsWhenClearing()
            {
                Given
                    .ConnectionBetween(_node1Position, _node2Position)
                    .ListeningToEvents();

                When
                    .ClearingLayout();

                Then
                    .ShouldHaveNodeCountOf(0)
                    .ShouldHaveCalledNodeRemoved(2)
                    .ShouldHaveCalledConnectionRemoved(1);
            }
        }

        public PuzzleEditorAcceptanceTests ConnectingBetween(TilePos position1, TilePos position2)
        {
            _sut.AddNodeConnection(new NodeConnection(position1, position2));
            return this;
        }

        public PuzzleEditorAcceptanceTests ConnectionBetween(TilePos position1, TilePos position2)
        {
            ConnectingBetween(position1, position2);
            return this;
        }

        public PuzzleEditorAcceptanceTests ShouldHaveNodeAt(TilePos position, Action<Node> nodeAction)
        {
            _sut.Nodes.ContainsKey(position).Should().BeTrue("there should be node at " + position);
            nodeAction(_sut.Nodes[position]);
            return this;
        }

        private PuzzleEditorAcceptanceTests ShouldHaveNodeCountOf(int count)
        {
            _sut.Nodes.Count.Should().Be(count);
            return this;
        }

        private void ShouldHaveTotalConnectionCountOf(int connectionCount)
        {
            _sut.Nodes.Values.SelectMany(x => x.Connections).Count().Should().Be(connectionCount);
        }

        private void RemovingConnectionBetween(TilePos position1, TilePos position2)
        {
            _sut.RemoveNodeConnection(new NodeConnection(position1, position2));
        }

        private PuzzleEditorAcceptanceTests ListeningToEvents()
        {
            _sut.NodeAdded += node => _callsToNodeAdded++;
            _sut.ConnectionAdded += connection => _callsToConnectionAdded++;
            _sut.NodeRemoved += node => _callsToNodeRemoved++;
            _sut.ConnectionRemoved += node => _callsToConnectionRemoved++;
            return this;
        }

        private PuzzleEditorAcceptanceTests ShouldHaveCalledNodeAdded(int callCount)
        {
            _callsToNodeAdded.Should().Be(callCount, "event should be called for each added node");
            return this;
        }

        private PuzzleEditorAcceptanceTests ShouldHaveCalledConnectionAdded(int callCount)
        {
            _callsToConnectionAdded.Should().Be(callCount, "event should be called for each added connection");
            return this;
        }

        private PuzzleEditorAcceptanceTests ShouldHaveCalledNodeRemoved(int callCount)
        {
            _callsToNodeRemoved.Should().Be(callCount, "event should be called for each removed node");
            return this;
        }

        private PuzzleEditorAcceptanceTests ShouldHaveCalledConnectionRemoved(int callCount)
        {
            _callsToConnectionRemoved.Should().Be(callCount, "event should be called for each removed connection");
            return this;
        }

        public PuzzleEditorAcceptanceTests ClearingLayout()
        {
            _sut.Clear();
            return this;
        }
    }

    public static class PuzzleEditorTestExtensions
    {
        public static Node WithConnectionsTo(this Node node, params TilePos[] connectedNodePositions)
        {
            node.Connections.Count.Should().Be(
                connectedNodePositions.Length,
                "node at " + node.Position + " should have " + connectedNodePositions.Length + " connections");

            node.Connections.Select(x => x.Position)
                .Should()
                .BeEquivalentTo(connectedNodePositions, "it should have those connections");

            return node;
        }
    }
}