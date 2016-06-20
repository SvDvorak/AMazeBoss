using Assets;
using FluentAssertions;
using Xunit;

namespace AMazeBoss.CSharp.Tests.Editor
{
    public class RemoveConnection : PuzzleEditorAcceptanceTests<RemoveConnection>
    {
        [Fact]
        public void UpdatesBothNodesWhenRemovingConnection()
        {
            Given
                .ConnectionBetween(Node1Position, Node2Position)
                .ConnectionBetween(Node1Position, Node3Position)
                .ConnectionBetween(Node2Position, Node3Position);

            When
                .RemovingConnectionBetween(Node1Position, Node2Position);

            Then
                .ShouldHaveNodeAt(Node1Position, node => node
                    .WithConnectionsTo(Node3Position))
                .ShouldHaveNodeAt(Node2Position, node => node
                    .WithConnectionsTo(Node3Position));
        }

        [Fact]
        public void RemovesNodesWithNoConnectionsWhenRemovingConnection()
        {
            Given
                .ConnectionBetween(Node1Position, Node2Position);

            When
                .RemovingConnectionBetween(Node1Position, Node2Position);

            Then
                .ShouldHaveNodeCountOf(0);
        }

        [Fact]
        public void DoesNothingIfRemovingConnectionBetweenNodesThatDontExist()
        {
            Given
                .ConnectionBetween(Node1Position, Node2Position);

            When
                .RemovingConnectionBetween(Node3Position, Node4Position);

            Then
                .ShouldHaveNodeCountOf(2);
        }

        [Fact]
        public void DoesNothingIfRemovingConnectionBetweenUnconnectedNodes()
        {
            Given
                .ConnectionBetween(Node1Position, Node2Position)
                .ConnectionBetween(Node3Position, Node4Position);

            When
                .RemovingConnectionBetween(Node1Position, Node4Position);

            Then
                .ShouldHaveNodeCountOf(4)
                .ShouldHaveTotalConnectionCountOf(4);
        }

        [Fact]
        public void RemovesAndReturnsSubdividedConnectionsInLongConnection()
        {
            var start = new TilePos(0, 0);
            var farAwayEnd = new TilePos(2, 0);

            Given
                .ConnectionBetween(start, farAwayEnd);

            When
                .RemovingConnectionBetween(start, farAwayEnd);

            Then
                .ShouldReturnRemoved(start, farAwayEnd / 2, farAwayEnd)
                .ShouldHaveNodeCountOf(0)
                .ShouldHaveTotalConnectionCountOf(0);
        }

        [Fact]
        public void OnlyRemovesAndReturnsSubConnectionsThatExistInLayout()
        {
            Given
                .ConnectionBetween(Node1Position, Node2Position);

            When
                .RemovingConnectionBetween(Node1Position, Node2Position * 4);

            Then
                .ShouldReturnRemoved(Node1Position, Node2Position);
        }


        public RemoveConnection ShouldReturnRemoved(params TilePos[] nodeConnections)
        {
            RemovedConnections.Count.Should()
                .Be(nodeConnections.Length - 1, "removed connections does not match expected count");

            for (int i = 0; i < RemovedConnections.Count; i++)
            {
                var current = RemovedConnections[i];
                current.Start.Should().Be(nodeConnections[i], "connection " + i + " start does not match expected");
                current.End.Should().Be(nodeConnections[i + 1], "connection " + i + " end does not match expected");
            }

            return this;
        }
    }
}
