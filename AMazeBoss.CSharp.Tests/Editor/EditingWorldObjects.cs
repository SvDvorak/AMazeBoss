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

        private string _addedSingletonCallType;
        private TilePos? _addedSingletonCallPosition;
        private string _singletonRemovedCallType;
        private ICommand _lastCommand;

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
        public void ReplacesPreviousSingletonWhenUndoingTheSecondOne()
        {
            var position1 = new TilePos(1, 1);
            var position2 = new TilePos(2, 2);

            Given
                .SingletonAt(PlayerType, position1)
                .AddingSingletonAt(PlayerType, position2);

            When
                .ListeningToEvents()
                .UndoingLastCommand();

            Then
                .ShouldHaveSingletonAt(PlayerType, position1)
                .ShouldHaveCalledSingletonRemoved(PlayerType)
                .ShouldHaveCalledSingletonAdded(PlayerType, position1);
        }

        [Fact]
        public void DoesntAffectSingletonOfOtherTypeWhenAddingSingleton()
        {
            var position1 = new TilePos(1, 1);
            var position2 = new TilePos(2, 2);


            Given
                .SingletonAt(OtherType, position1);

            When
                .AddingSingletonAt(PlayerType, position2);

            Then
                .ShouldHaveSingletonAt(OtherType, position1)
                .ShouldHaveSingletonAt(PlayerType, position2);
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
        public void RemovesObjectWhenRemovingNodeBelowIt()
        {
            var position1 = new TilePos(1, 1);
            var position2 = new TilePos(2, 2);

            Given
                .SingletonAt(PlayerType, position1)
                .ConnectionBetween(position1, position2);

            When
                .RemovingConnectionBetween(position1, position2);

            Then
                .ShouldNotHaveSingleton(PlayerType);
        }

        private void AddingSingletonAt(string type, TilePos tilePos)
        {
            _lastCommand = new SetSingletonObjectCommand(_sut, type, tilePos);
            _lastCommand.Execute();
        }

        private EditingWorldObjects SingletonAt(string type, TilePos tilePos)
        {
            AddingSingletonAt(type, tilePos);
            return this;
        }

        private void RemovingSingleton(string type)
        {
            _lastCommand = new SetSingletonObjectCommand(_sut, type, null);
            _lastCommand.Execute();
        }

        private EditingWorldObjects ShouldHaveSingletonAt(string type, TilePos tilePos)
        {
            _sut.GetSingleton(type).Should().Be(tilePos);
            return this;
        }

        private EditingWorldObjects ShouldNotHaveSingleton(string type)
        {
            _sut.GetSingleton(type).Should().BeNull();
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
            _lastCommand.Undo();
        }

        public new EditingWorldObjects ListeningToEvents()
        {
            base.ListeningToEvents();
            _sut.SingletonAdded += (type, position) =>
                {
                    _addedSingletonCallType = type;
                    _addedSingletonCallPosition = position;
                };
            _sut.SingletonRemoved += type => _singletonRemovedCallType = type;
            return this;
        }
    }
}