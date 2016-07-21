using System;
using System.Collections.Generic;
using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public abstract class PlaceObjectCommand : ICommand
    {
        protected readonly TilePos Position;
        protected readonly string Type;
        protected readonly PuzzleLayout Layout;
        private readonly List<ChangedObject> _removedObjects = new List<ChangedObject>();

        public abstract string Name { get; }

        protected PlaceObjectCommand(PuzzleLayout layout, string type, TilePos position)
        {
            Position = position;
            Type = type;
            Layout = layout;
        }

        protected abstract void ExecutePlace();

        protected abstract void UndoPlace();

        public void Execute()
        {
            Layout.ObjectRemoved += AddRemovedObject;

            ExecutePlace();

            Layout.ObjectRemoved -= AddRemovedObject;
        }

        private void AddRemovedObject(string type, TilePos position)
        {
            _removedObjects.Add(new ChangedObject(type, position));
        }

        public void Undo()
        {
            UndoPlace();

            foreach (var removedObject in _removedObjects)
            {
                Layout.PlaceObject(removedObject.Type, removedObject.Position);
            }

            _removedObjects.Clear();
        }
    }
}
