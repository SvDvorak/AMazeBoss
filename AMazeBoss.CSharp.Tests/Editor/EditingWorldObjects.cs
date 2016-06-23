using System;
using System.Collections.Generic;
using Assets;
using Assets.Editor.Undo;
using FluentAssertions;
using Xunit;

namespace AMazeBoss.CSharp.Tests.Editor
{
    public class EditingWorldObjects : PuzzleEditorAcceptanceTests<EditingWorldObjects>
    {
        private const string SingletonType = "Singleton";
        private const string ObjectType = "Object";
        private const string OtherType = "Other";
        private readonly TilePos _position1 = new TilePos(0, 0);
        private readonly TilePos _position2 = new TilePos(1, 0);

        private readonly WorldObjectCallsAssertions _callsAssertions = new WorldObjectCallsAssertions();

        [Fact]
        public void CannotPlaceObjectWhenThereIsNotANodeBelowIt()
        {
            ShouldNotBeAbleToPlace(_position1);
        }

        [Fact]
        public void CanPlaceObjectWhenThereIsANodeBelowIt()
        {
            Given
                .ConnectionBetween(_position1, _position2);

            Then
                .ShouldBeAbleToPlace(_position1);
        }

        [Fact]
        public void AddsObjectToLayoutWithSameTypeObjectButDifferentPosition()
        {
            Given
                .ObjectAt(ObjectType, _position1);

            When
                .ListeningToEvents()
                .AddingObjectAt(ObjectType, _position2);

            Then
                .ShouldHaveObjectsAt(ObjectType, _position1, _position2);
        }

        [Fact]
        public void SingletonObjectAddedWhenAddingToEmptyLayout()
        {
            When
                .ListeningToEvents()
                .AddingObjectAt(SingletonType, _position1);

            Then
                .ShouldHaveObjectsAt(SingletonType, _position1)
                .ShouldHaveCalled(x => x.Added(SingletonType, _position1));
        }

        [Fact]
        public void RemovesObjectFromLayoutThatHasObjectAtSpecifiedPosition()
        {
            Given
                .ObjectAt(ObjectType, _position1)
                .ObjectAt(ObjectType, _position2);

            When
                .ListeningToEvents()
                .RemovingObject(_position2);

            Then
                .ShouldHaveObjectsAt(ObjectType, _position1)
                .ShouldHaveCalled(x => x.Removed(ObjectType, _position2));
        }

        [Fact]
        public void ReplacesCurrentSingletonWhenAddingToLayoutWithSameTypeSingleton()
        {
            Given
                .SingletonAt(SingletonType, _position1);

            When
                .ListeningToEvents()
                .AddingSingletonAt(SingletonType, _position2);

            Then
                .ShouldHaveCalled(x => x
                    .Removed(SingletonType, _position1)
                    .Added(SingletonType, _position2));
        }

        [Fact]
        public void RemovesObjectWhenUndoingAdd()
        {
            When
                .AddingObjectAt(SingletonType, _position1)
                .ListeningToEvents()
                .UndoingLastCommand();

            Then
                .ShouldNotHaveObjects(SingletonType)
                .ShouldHaveCalled(x => x.Removed(SingletonType, _position1));
        }


        [Fact]
        public void AddsObjectAgainWhenUndoingObjectRemove()
        {
            Given
                .ObjectAt(ObjectType, _position1);

            When
                .RemovingObject(_position1)
                .UndoingLastCommand();

            Then
                .ShouldHaveObjectsAt(ObjectType, _position1);
        }

        [Fact]
        public void ReplacesPreviousSingletonWhenUndoingTheSecondOne()
        {
            Given
                .SingletonAt(SingletonType, _position1);

            When
                .AddingSingletonAt(SingletonType, _position2)
                .ListeningToEvents()
                .UndoingLastCommand();

            Then
                .ShouldHaveSingletonAt(SingletonType, _position1)
                .ShouldHaveCalled(x => x
                    .Removed(SingletonType, _position2)
                    .Added(SingletonType, _position1));
        }

        [Fact]
        public void DoesntAffectSingletonOfOtherTypeWhenAddingSingleton()
        {
            Given
                .SingletonAt(OtherType, _position1);

            When
                .AddingSingletonAt(SingletonType, _position2);

            Then
                .ShouldHaveSingletonAt(OtherType, _position1)
                .ShouldHaveSingletonAt(SingletonType, _position2);
        }

        [Fact]
        public void RemovesObjectAtSamePositionWhenPlacingNewObject()
        {
            var position = new TilePos(1, 1);

            Given
                .ObjectAt(ObjectType, position);

            When
                .AddingObjectAt(OtherType, position);

            Then
                .ShouldHaveObjectsAt(OtherType, position)
                .ShouldNotHaveObjects(ObjectType);
        }

        [Fact]
        public void ReplacesPreviousObjectWhenUndoingObjectReplacement()
        {
            var position = new TilePos(1, 1);

            Given
                .ObjectAt(SingletonType, position)
                .ObjectAt(OtherType, new TilePos(2, 2));

            When
                .AddingObjectAt(OtherType, position)
                .UndoingLastCommand();

            Then
                .ShouldHaveObjectsAt(SingletonType, position);
        }

        [Fact]
        public void RemovesObjectsWhenRemovingNodesBelowThem()
        {
            Given
                .ObjectAt(ObjectType, _position1)
                .ObjectAt(ObjectType, _position2)
                .ConnectionBetween(_position1, _position2);

            When
                .ListeningToEvents()
                .RemovingConnectionBetween(_position1, _position2);

            Then
                .ShouldNotHaveObjects(ObjectType)
                .ShouldHaveCalled(x => x
                    .Removed(ObjectType, _position1)
                    .Removed(ObjectType, _position2));
        }

        [Fact]
        public void ReplacesObjectWhenUndoingNodeConnectionRemovalThatHadObjectOnANode()
        {
            Given
                .ObjectAt(ObjectType, _position1)
                .ObjectAt(ObjectType, _position2)
                .ConnectionBetween(_position1, _position2);

            When
                .RemovingConnectionBetween(_position1, _position2)
                .UndoingLastCommand();

            Then
                .ShouldHaveObjectsAt(ObjectType, _position1, _position2);
        }

        private void ShouldNotBeAbleToPlace(TilePos position)
        {
            Sut.CanPlaceAt(position).Should().BeFalse();
        }

        private void ShouldBeAbleToPlace(TilePos position)
        {
            Sut.CanPlaceAt(position).Should().BeTrue();
        }

        private EditingWorldObjects AddingObjectAt(string type, TilePos position)
        {
            LastCommand = new PlaceNormalObjectCommand(Sut, type, position);
            LastCommand.Execute();
            return this;
        }

        private EditingWorldObjects ObjectAt(string type, TilePos position)
        {
            AddingObjectAt(type, position);
            return this;
        }

        private EditingWorldObjects AddingSingletonAt(string type, TilePos tilePos)
        {
            LastCommand = new SetSingletonObjectCommand(Sut, type, tilePos);
            LastCommand.Execute();
            return this;
        }

        private EditingWorldObjects SingletonAt(string type, TilePos tilePos)
        {
            AddingSingletonAt(type, tilePos);
            return this;
        }

        private EditingWorldObjects RemovingObject(TilePos position)
        {
            LastCommand = new RemoveObjectCommand(Sut, position);
            LastCommand.Execute();
            return this;
        }

        private EditingWorldObjects ShouldHaveObjectsAt(string type, params TilePos[] positions)
        {
            Sut.GetObjects(type).Should().BeEquivalentTo(positions);
            return this;
        }

        private EditingWorldObjects ShouldNotHaveObjects(string type)
        {
            Sut.GetObjects(type).Should().BeEquivalentTo(new TilePos[] { });
            return this;
        }
        private EditingWorldObjects ShouldHaveSingletonAt(string type, TilePos tilePos)
        {
            Sut.GetSingleton(type).Should().Be(tilePos);
            return this;
        }

        private EditingWorldObjects ShouldHaveCalled(Action<WorldObjectCallsAssertions> func)
        {
            func(_callsAssertions);
            _callsAssertions.AssertAllCalls();
            return this;
        }

        private void UndoingLastCommand()
        {
            LastCommand.Undo();
        }

        public new EditingWorldObjects ListeningToEvents()
        {
            base.ListeningToEvents();
            Sut.ObjectAdded += (type, position) => _callsAssertions.AddCalled(new WorldObject(type, position));
            Sut.ObjectRemoved += (type, position) => _callsAssertions.RemoveCalled(new WorldObject(type, position));
            return this;
        }

        private class WorldObjectCallsAssertions
        {
            private class CallInfo
            {
                public enum CallType
                {
                    Add,
                    Remove
                }

                public readonly CallType Call;
                public readonly string Type;
                public readonly TilePos Position;

                public CallInfo(CallType call, string type, TilePos position)
                {
                    Call = call;
                    Type = type;
                    Position = position;
                }
            }

            private readonly List<CallInfo> Calls = new List<CallInfo>();
            private readonly List<CallInfo> Expected = new List<CallInfo>();

            public WorldObjectCallsAssertions Added(string type, TilePos position)
            {
                Expected.Add(new CallInfo(CallInfo.CallType.Add, type, position));
                return this;
            }

            public WorldObjectCallsAssertions Removed(string type, TilePos position)
            {
                Expected.Add(new CallInfo(CallInfo.CallType.Remove, type, position));
                return this;
            }

            public void AssertAllCalls()
            {
                for (var i = 0; i < Expected.Count; i++)
                {
                    var currentExpected = Expected[i];
                    if (i > Calls.Count - 1)
                    {
                        throw new Exception(
                            $"Expected {currentExpected.Call} call with type: {currentExpected.Type}, position: {currentExpected.Position} but no more calls were done");
                    }

                    var currentCall = Calls[i];
                    currentCall.ShouldBeEquivalentTo(currentExpected, "call number " + i + " did not match expected");
                }
            }

            public void AddCalled(WorldObject worldObject)
            {
                Calls.Add(new CallInfo(CallInfo.CallType.Add, worldObject.Type, worldObject.Position));
            }

            public void RemoveCalled(WorldObject worldObject)
            {
                Calls.Add(new CallInfo(CallInfo.CallType.Remove, worldObject.Type, worldObject.Position));
            }
        }
    }
}