using Assets;
using Xunit;

namespace AMazeBoss.CSharp.Tests.Editor
{
    public class AddConnection : PuzzleEditorAcceptanceTests<AddConnection>
    {
        [Fact]
        public void CreatesTwoConnectedNodesWhenAddingConnectionOnEmptyLayout()
        {
            When
                .ConnectingBetween(Node1Position, Node2Position);

            Then
                .ShouldHaveNodeAt(Node1Position, node => node
                    .WithConnectionsTo(Node2Position))
                .ShouldHaveNodeAt(Node2Position, node => node
                    .WithConnectionsTo(Node1Position));
        }

        [Fact]
        public void DoesNothingWhenAddingConnectionBetweenAlreadyConnectedNodes()
        {
            When
                .ConnectingBetween(Node1Position, Node2Position)
                .ConnectingBetween(Node2Position, Node1Position);

            Then
                .ShouldHaveNodeCountOf(2)
                .ShouldHaveTotalConnectionCountOf(2);
        }

        [Fact]
        public void OnlyCreatesOneNewNodeWhenAddingConnectionBetweenExistingAndNewNode()
        {
            When
                .ConnectingBetween(Node1Position, Node2Position)
                .ConnectingBetween(Node1Position, Node3Position);

            Then
                .ShouldHaveNodeCountOf(3);
        }

        [Fact]
        public void UpdatesConnectionsWhenAddingConnectionBetweenTwoAlreadyExistingNodes()
        {
            When
                .ConnectingBetween(Node1Position, Node2Position)
                .ConnectingBetween(Node3Position, Node4Position)
                .ConnectingBetween(Node1Position, Node4Position);

            Then
                .ShouldHaveNodeCountOf(4)
                .ShouldHaveNodeAt(Node1Position, node => node
                    .WithConnectionsTo(Node2Position, Node4Position));
        }

        [Fact]
        public void SplitsUpLongConnectionIntoSeveralSmaller()
        {
            var start = new TilePos(0, 0);
            var farAwayEnd = new TilePos(2, 0);
            var middlePoint = farAwayEnd / 2;

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
}
