using System;
using System.Collections.Generic;
using Assets;
using Assets.LevelEditorUnity;
using FluentAssertions;
using Xunit;

namespace AMazeBoss.CSharp.Tests.Editor
{
    public class WorldObjectProperties : PuzzleEditorAcceptanceTests<WorldObjectProperties>
    {
        private TilePos _objectPosition;

        [Fact]
        public void SetsPropertyToObject()
        {
            var trapPosition = new TilePos(1, 1);

            Given
                .ObjectAt("Trap", trapPosition);

            When
                .SettingProperty(trapPosition, "IsLoaded", false)
                .SettingProperty(trapPosition, "IsLoaded", true);

            Then
                .ShouldHaveSetting(trapPosition, "IsLoaded", true);
        }

        [Fact]
        public void LoadsPropertiesWithObject()
        {
            _objectPosition = new TilePos(1, 1);

            Given
                .ObjectAt("Trap", _objectPosition, new Dictionary<string, string>() { { "IsLoaded", "true" } });

            Then
                .ShouldHaveSetting(_objectPosition, "IsLoaded", true);
        }

        [Fact]
        public void UndoRemovesPropertyIfItWasntSetPreviously()
        {
            _objectPosition = new TilePos(1, 1);

            Given
                .ObjectAt("Trap", _objectPosition);

            When
                .SettingProperty(_objectPosition, "property", "true")
                .UndoingLastCommand();

            Then
                .ShouldNotHaveSetting(_objectPosition, "property");
        }

        [Fact]
        public void UndoSetsPreviousValueIfWasAlreadySet()
        {
            _objectPosition = new TilePos(1, 1);

            Given
                .ObjectAt("Trap", _objectPosition);

            When
                .SettingProperty(_objectPosition, "property", "true")
                .SettingProperty(_objectPosition, "property", "false")
                .UndoingLastCommand();

            Then
                .ShouldHaveSetting(_objectPosition, "property", "true");
        }

        [Fact]
        public void ThrowsNoFoundObjectWhenTryingToGetPropertyForNoObject()
        {
            var exception = Record.Exception(() => 
                When
                    .SettingProperty(new TilePos(), "property", true));

            Then
                .ExceptionWasThrown<ObjectNotFoundException>(exception);
        }

        private void ExceptionWasThrown<T>(Exception exception)
        {
            Assert.IsType<T>(exception);
        }

        private void ShouldHaveSetting<T>(TilePos position, string key, T value)
        {
            Sut.GetProperty<T>(position, key).Should().Be(value);
        }

        private void ShouldNotHaveSetting(TilePos position, string key)
        {
            Sut.HasProperty(position, key).Should().BeFalse();
        }
    }
}
