using System.Collections.Generic;
using System.Linq;
using Assets;
using Assets.Features.Level;
using Assets.Features.Level.Editor_;
using Assets.Level.Editor_;
using FluentAssertions;
using Xunit;

namespace AMazeBoss.CSharp.Tests
{
    public class PuzzleLayoutLoaderTests
    {
        private readonly PuzzleLayoutLoader _sut = new PuzzleLayoutLoader();
        private readonly FlatLayout _savedLayout = new FlatLayout();
        private readonly PuzzleLayout _layoutToSave = new PuzzleLayout();

        [Fact]
        public void SavedConnectionAddsConnectionToLayout()
        {
            var connection = new NodeConnection(new TilePos(0, 0), new TilePos(1, 0));
            _savedLayout.Connections.Add(connection);
            var gameLayout = new PuzzleLayout();
            _sut.LoadFromFlatLayout(_savedLayout, gameLayout);

            gameLayout.GetAllConnections().ShouldAllBeEquivalentTo(new[] { connection });
        }

        [Fact]
        public void SavedObjectAddsObjectToLayout()
        {
            var puzzleObject = new FlatLayout.PuzzleObject
            {
                Type = "type",
                Position = new TilePos(1, 1),
                Properties =
                {
                    new FlatLayout.PuzzleObject.Property
                    {
                        Key = "key",
                        Type = "System.Boolean",
                        Value = "true"
                    }
                }
            };

            _savedLayout.Objects.Add(puzzleObject);
            var gameLayout = new PuzzleLayout();
            _sut.LoadFromFlatLayout(_savedLayout, gameLayout);

            var puzzleObjects = gameLayout.GetAllObjects();
            puzzleObjects.Should().HaveCount(1);
            puzzleObjects[0].Type.Should().Be(puzzleObject.Type);
            puzzleObjects[0].Position.Should().Be(puzzleObject.Position);
            puzzleObjects[0].Properties.Should().HaveCount(1);
            var property = puzzleObjects[0].Properties.First().Value;
            property.Key.Should().Be("key");
            property.Type.Should().Be(typeof(bool));
            property.Value.Should().Be(true);
        }

        [Fact]
        public void SavesNodeConnectionToLayout()
        {
            var layoutToSave = new PuzzleLayout();
            var nodeConnection = new NodeConnection(new TilePos(0, 0), new TilePos(1, 1));
            layoutToSave.AddNodeConnections(nodeConnection);

            var savedLayout = _sut.SaveToFlatLayout(layoutToSave);

            savedLayout.Connections[0].Should().Be(nodeConnection);
        }

        [Fact]
        public void SavesObjectToLayout()
        {
            var position = new TilePos(1, 1);
            _layoutToSave.PlaceObject("object", position, new SetProperties().Add("property", true));

            var savedLayout = _sut.SaveToFlatLayout(_layoutToSave);

            savedLayout.Objects.Should().HaveCount(1);
            savedLayout.Objects[0].Type.Should().Be("object");
            savedLayout.Objects[0].Position.Should().Be(position);
            savedLayout.Objects[0].Properties.Should().HaveCount(1);
            var property = savedLayout.Objects[0].Properties[0];
            property.Key.Should().Be("property");
            property.Type.Should().Be("System.Boolean");
            property.Value.Should().Be("True");
        }
    }
}
