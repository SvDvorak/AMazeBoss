using System;
using Assets;
using Assets.Editor.Undo;
using FluentAssertions;
using Xunit;

namespace AMazeBoss.CSharp.Tests.Editor
{
    public class EditingWorldObjects : PuzzleEditorAcceptanceTests<EditingWorldObjects>
    {
        private const string PlayerType = "Player";
        private const string OtherType = "Other";
        private readonly TilePos _position1 = new TilePos(0, 0);
        private readonly TilePos _position2 = new TilePos(1, 0);

        private string _addedSingletonCallType;
        private TilePos? _addedSingletonCallPosition;
        private string _singletonRemovedCallType;

        [Fact]
        public void AddsObjectToLayoutWithSameTypeObject()
        {
            Given
                .ObjectAt(OtherType, _position1);

            When
                .ListeningToEvents()
                .AddingObjectAt(OtherType, _position2);

            Then
                .ShouldHaveObjectsAt(OtherType, _position1, _position2);
        }

        private void AddingObjectAt(string type, TilePos position)
        {
            var command = new AddObjectCommand(Sut, type, position);
            command.Execute();
        }

        private void ObjectAt(string type, TilePos position)
        {
            AddingObjectAt(type, position);
        }

        private void ShouldHaveObjectsAt(string type, params TilePos[] positions)
        {
            Sut.GetObjects(type).Should().BeEquivalentTo(positions);
        }

        [Fact]
        public void SingletonObjectAddedWhenAddingToEmptyLayout()
        {
            var position = new TilePos(0, 0);

            When
                .ListeningToEvents()
                .AddingSingletonAt(PlayerType, position);

            Then
                .ShouldHaveSingletonAt(PlayerType, position)
                .ShouldHaveCalledSingletonAdded(PlayerType, position);
        }

        [Fact]
        public void RemovesSingletonFromLayoutThatAlreadyHasSingleton()
        {
            Given
                .SingletonAt(PlayerType, new TilePos(1, 1));

            When
                .ListeningToEvents()
                .RemovingSingleton(PlayerType);

            Then
                .ShouldNotHaveSingleton(PlayerType)
                .ShouldHaveCalledSingletonRemoved(PlayerType);
        }

        [Fact]
        public void ReplacesCurrentSingletonWhenAddingToLayoutWithSameTypeSingleton()
        {
            var playerPosition = new TilePos(1, 1);

            Given
                .SingletonAt(PlayerType, playerPosition);

            When
                .ListeningToEvents()
                .AddingSingletonAt(PlayerType, playerPosition);

            Then
                .ShouldHaveCalledSingletonRemoved(PlayerType)
                .ShouldHaveCalledSingletonAdded(PlayerType, playerPosition);
        }

        [Fact]
        public void RemovesSingletonWhenUndoingAdd()
        {
            var position = new TilePos(0, 0);

            When
                .AddingSingletonAt(PlayerType, position)
                .ListeningToEvents()
                .UndoingLastCommand();

            Then
                .ShouldNotHaveSingleton(PlayerType)
                .ShouldHaveCalledSingletonRemoved(PlayerType);
        }

        [Fact]
        public void ReplacesPreviousSingletonWhenUndoingTheSecondOne()
        {
            Given
                .SingletonAt(PlayerType, _position1)
                .AddingSingletonAt(PlayerType, _position2);

            When
                .ListeningToEvents()
                .UndoingLastCommand();

            Then
                .ShouldHaveSingletonAt(PlayerType, _position1)
                .ShouldHaveCalledSingletonRemoved(PlayerType)
                .ShouldHaveCalledSingletonAdded(PlayerType, _position1);
        }

        [Fact]
        public void DoesntAffectSingletonOfOtherTypeWhenAddingSingleton()
        {
            Given
                .SingletonAt(OtherType, _position1);

            When
                .AddingSingletonAt(PlayerType, _position2);

            Then
                .ShouldHaveSingletonAt(OtherType, _position1)
                .ShouldHaveSingletonAt(PlayerType, _position2);
        }

        [Fact]
        public void RemovesObjectAtSamePositionWhenPlacingNewObject()
        {
            var position = new TilePos(1, 1);

            Given
                .SingletonAt(PlayerType, position);

            When
                .AddingSingletonAt(OtherType, position);

            Then
                .ShouldHaveSingletonAt(OtherType, position)
                .ShouldNotHaveSingleton(PlayerType);
        }

        [Fact]
        public void ReplacesPreviousObjectWhenUndoingObjectReplacement()
        {
            var position = new TilePos(1, 1);

            Given
                .SingletonAt(PlayerType, position)
                .SingletonAt(OtherType, new TilePos(2, 2));

            When
                .AddingSingletonAt(OtherType, position)
                .UndoingLastCommand();

            Then
                .ShouldHaveSingletonAt(PlayerType, position);
        }

        [Fact]
        public void RemovesObjectWhenRemovingNodeBelowIt()
        {
            Given
                .SingletonAt(PlayerType, _position1)
                .ConnectionBetween(_position1, _position2);

            When
                .RemovingConnectionBetween(_position1, _position2);

            Then
                .ShouldNotHaveSingleton(PlayerType);
        }

        [Fact]
        public void ReplacesObjectWhenUndoingNodeConnectionRemovalThatHadObjectOnANode()
        {
            Given
                .SingletonAt(PlayerType, _position1)
                .ConnectionBetween(_position1, _position2);

            When
                .RemovingConnectionBetween(_position1, _position2)
                .UndoingLastCommand();

            Then
                .ShouldHaveSingletonAt(PlayerType, _position1);
        }

        private EditingWorldObjects AddingSingletonAt(string type, TilePos tilePos)
        {
            LastCommand = new SetSingletonObjectCommand(Sut, type, tilePos);
            LastCommand.Execute();
            return This;
        }

        private EditingWorldObjects SingletonAt(string type, TilePos tilePos)
        {
            AddingSingletonAt(type, tilePos);
            return this;
        }

        private void RemovingSingleton(string type)
        {
            LastCommand = new SetSingletonObjectCommand(Sut, type, null);
            LastCommand.Execute();
        }

        private EditingWorldObjects ShouldHaveSingletonAt(string type, TilePos tilePos)
        {
            Sut.GetSingleton(type).Should().Be(tilePos);
            return this;
        }

        private EditingWorldObjects ShouldNotHaveSingleton(string type)
        {
            Sut.GetSingleton(type).Should().BeNull();
            return this;
        }

        private void ShouldHaveCalledSingletonAdded(string type, TilePos playerPosition)
        {
            _addedSingletonCallType.Should().Be(type);
            _addedSingletonCallPosition.Should().Be(playerPosition);
        }

        private EditingWorldObjects ShouldHaveCalledSingletonRemoved(string type)
        {
            _singletonRemovedCallType.Should().Be(type, "should have called {0} removed event", type);
            return this;
        }

        private void UndoingLastCommand()
        {
            LastCommand.Undo();
        }

        public new EditingWorldObjects ListeningToEvents()
        {
            base.ListeningToEvents();
            Sut.SingletonAdded += (type, position) =>
                {
                    _addedSingletonCallType = type;
                    _addedSingletonCallPosition = position;
                };
            Sut.SingletonRemoved += (type, position) => _singletonRemovedCallType = type;
            return this;
        }
    }
}