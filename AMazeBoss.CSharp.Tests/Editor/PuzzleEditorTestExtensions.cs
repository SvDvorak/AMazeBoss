using System.Linq;
using Assets;
using Assets.LevelEditorUnity;
using FluentAssertions;

namespace AMazeBoss.CSharp.Tests.Editor
{
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