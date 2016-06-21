using System;
using Assets;
using Assets.Editor.Undo;
using FluentAssertions;
using Xunit;

namespace AMazeBoss.CSharp.Tests.Editor
{
    public class EditingWorldObjects : PuzzleEditorAcceptanceTests<EditingWorldObjects>
    {
        private TilePos? _addedPlayerCallPosition;
        private bool _playerRemovedCalled;
        private ICommand _lastCommand;

        [Fact]
        public void PlayerAddedWhenAddingToEmptyLayout()
        {
            var playerPosition = new TilePos(0, 0);

            When
                .ListeningToEvents()
                .AddingPlayerAt(playerPosition);

            Then
                .ShouldHavePlayerAt(playerPosition)
                .ShouldHaveCalledPlayerAdded(playerPosition);
        }

        [Fact]
        public void RemovesPlayerFromLayoutWithPlayer()
        {
            Given
                .PlayerAt(new TilePos(1, 1));

            When
                .ListeningToEvents()
                .RemovingPlayer();

            Then
                .ShouldNotHavePlayer()
                .ShouldHaveCalledPlayerRemoved();
        }

        [Fact]
        public void RemovesCurrentPlayerAndAddsNewWhenAddingToLayoutWithPlayer()
        {
            var playerPosition = new TilePos(1, 1);

            Given
                .PlayerAt(playerPosition);

            When
                .ListeningToEvents()
                .AddingPlayerAt(playerPosition);

            Then
                .ShouldHaveCalledPlayerRemoved()
                .ShouldHaveCalledPlayerAdded(playerPosition);
        }

        [Fact]
        public void ReplacesPreviousPlayerWhenUndoingTheSecondOne()
        {
            var position1 = new TilePos(1, 1);
            var position2 = new TilePos(2, 2);

            Given
                .PlayerAt(position1)
                .AddingPlayerAt(position2);

            When
                .ListeningToEvents()
                .UndoingLastCommand();

            Then
                .ShouldHavePlayerAt(position1)
                .ShouldHaveCalledPlayerRemoved()
                .ShouldHaveCalledPlayerAdded(position1);
        }

        private void AddingPlayerAt(TilePos tilePos)
        {
            _lastCommand = new AddPlayerCommand(_sut, tilePos);
            _lastCommand.Execute();
        }

        private EditingWorldObjects PlayerAt(TilePos tilePos)
        {
            AddingPlayerAt(tilePos);
            return this;
        }

        private void RemovingPlayer()
        {
            _lastCommand = new RemovePlayerCommand(_sut);
            _lastCommand.Execute();
        }

        private EditingWorldObjects ShouldHavePlayerAt(TilePos tilePos)
        {
            _sut.PlayerPosition.Should().Be(tilePos);
            return this;
        }

        private EditingWorldObjects ShouldNotHavePlayer()
        {
            _sut.PlayerPosition.Should().BeNull();
            return this;
        }

        private void ShouldHaveCalledPlayerAdded(TilePos playerPosition)
        {
            _addedPlayerCallPosition.Should().Be(playerPosition);
        }

        private EditingWorldObjects ShouldHaveCalledPlayerRemoved()
        {
            _playerRemovedCalled.Should().BeTrue("should have called player removed event");
            return this;
        }

        private void UndoingLastCommand()
        {
            _lastCommand.Undo();
        }

        public new EditingWorldObjects ListeningToEvents()
        {
            base.ListeningToEvents();
            _sut.PlayerAdded += playerPosition => _addedPlayerCallPosition = playerPosition;
            _sut.PlayerRemoved += () => _playerRemovedCalled = true;
            return this;
        }
    }
}