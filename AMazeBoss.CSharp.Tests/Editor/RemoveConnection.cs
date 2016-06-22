using Assets;
using Assets.LevelEditorUnity;
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
                .ShouldNotHaveConnectionsBetween(start, farAwayEnd / 2, farAwayEnd)
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
                .ShouldNotHaveConnectionsBetween(Node1Position, Node2Position);
        }


        public RemoveConnection ShouldNotHaveConnectionsBetween(params TilePos[] nodeConnections)
        {
            for (int i = 0; i < nodeConnections.Length - 1; i++)
            {
                var current = nodeConnections[i];
                var next = nodeConnections[i+1];

                HasConnection(current, next).Should().BeFalse($"connection between {0} and {1} shouldn't exist", current, next);
            }

            return this;
        }

        public bool HasConnection(TilePos start, TilePos end)
        {
            var startNode = Sut.Nodes.ContainsKey(start) ? Sut.Nodes[start] : null;
            var endNode = Sut.Nodes.ContainsKey(start) ? Sut.Nodes[start] : null;

            if (startNode != null && endNode != null)
            {
                return startNode.Connections.Contains(endNode) ||
                       endNode.Connections.Contains(startNode);
            }

            return false;
        }
    }
}
