using Assets;
using Assets.LevelEditorUnity;
using FluentAssertions;
using Xunit;

namespace AMazeBoss.CSharp.Tests
{
    public class NodeConnectionTests
    {
        [Fact]
        public void LengthReturnsLengthBetweenStartAndEnd()
        {
            var sut = new NodeConnection(new TilePos(0, 0), new TilePos(3, 3));

            sut.Length().Should().Be(6);
        }
    }
}
