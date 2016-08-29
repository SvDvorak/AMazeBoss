using Assets;
using Assets.Level;
using Assets.Level.Editor_;
using Entitas;
using FluentAssertions;
using Xunit;

namespace AMazeBoss.CSharp.Tests
{
    public class GameLevelLoaderSystemTests
    {
        [Fact]
        public void DoesNotCreateOverlappingTiles()
        {
            var layout = new PuzzleLayout();
            layout.AddNodeConnections(new NodeConnection(new TilePos(0, 0), new TilePos(1, 0)));
            layout.PlaceObject("Trap", new TilePos(0, 0));

            var pool = new TestGamePool();
            new Systems()
                .Add(pool.CreateSystem<PositionsCacheUpdateSystem>())
                .Add(pool.CreateSystem(new GameLevelLoaderSystem(layout)))
                .Initialize();

            var tilesAtPosition = pool.GetEntitiesAt(new TilePos(0, 0), e => e.IsTile());
            tilesAtPosition.Count.Should().Be(1);
        }
    }
}
