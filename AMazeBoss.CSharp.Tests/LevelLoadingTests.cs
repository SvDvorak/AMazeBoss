using System;
using System.Linq;
using Assets;
using Assets.Features.Level;
using Assets.Level.Editor_;
using Entitas;
using FluentAssertions;
using Xunit;

namespace AMazeBoss.CSharp.Tests
{
    public class LevelLoadingTests : AcceptanceTests<LevelLoadingTests>
    {
        private readonly PuzzleLayout _layout;
        private readonly TestGamePool _pool;
        private Systems _systems;

        public LevelLoadingTests()
        {
            _layout = new PuzzleLayout();

            _pool = new TestGamePool();
        }

        [Fact]
        public void CreatesTilesForAllNodesInLayout()
        {
            var position1 = new TilePos(0, 0);
            var position2 = new TilePos(1, 0);
            var position3 = new TilePos(2, 0);

            Given
                .LayoutConnectionBetween(position1, position3);

            When
                .StartingGame();

            Then
                .ShouldHaveTilesAt(position1, position2, position3);
        }

        [Fact]
        public void PlacesObjectsUsingLayout()
        {
            var playerPosition = new TilePos(1, 0);
            var bossPosition = new TilePos(2, 0);
            var trapPosition1 = new TilePos(3, 0);
            var trapPosition2 = new TilePos(4, 0);

            Given
                .ObjectAt("Player", playerPosition)
                .ObjectAt("Boss", bossPosition)
                .ObjectAt("Trap", trapPosition1)
                .ObjectAt("Trap", trapPosition2);

            When
                .StartingGame();

            Then
                .ShouldHaveObjectAt(playerPosition, GameMatcher.Hero)
                .ShouldHaveObjectAt(bossPosition, GameMatcher.Boss)
                .ShouldHaveObjectAt(trapPosition1, GameMatcher.SpikeTrap)
                .ShouldHaveObjectAt(trapPosition2, GameMatcher.SpikeTrap);
        }

        private LevelLoadingTests ObjectAt(string type, TilePos position)
        {
            _layout.PlaceObject(type, position);
            return this;
        }

        private LevelLoadingTests ShouldHaveObjectAt(TilePos position, IMatcher matcher)
        {
            var objectEntities = _pool.GetEntities(matcher);
            objectEntities.Select(x => x.position.Value).Should().Contain(position);
            return this;
        }

        private void StartingGame()
        {
            _systems = new Systems();
            _systems.AddSystems(_pool, x => x
                .UpdateSystems()
                .LevelHandlingSystems(_layout)
                .DestroySystems());
            _systems.Initialize();
        }

        private void LayoutConnectionBetween(TilePos position1, TilePos position2)
        {
            _layout.AddNodeConnections(new NodeConnection(position1, position2));
        }

        private void ShouldHaveTilesAt(params TilePos[] expectedPositions)
        {
            foreach (var expectedPosition in expectedPositions)
            {
                var matchedTile = _pool.GetEntityAt(expectedPosition, x => x.gameObject.Type == ObjectType.Tile);
                if (matchedTile == null)
                {
                    throw new Exception("Could not find matching tile for position " + expectedPosition);
                }
            }
        }
    }
}