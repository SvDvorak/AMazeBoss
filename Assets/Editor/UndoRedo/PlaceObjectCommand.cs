using System;
using System.Collections.Generic;
using Assets.Level;

namespace Assets.Editor.Undo
{
    public abstract class PlaceObjectCommand : ICommand
    {
        protected readonly TilePos Position;
        protected readonly string Type;
        protected readonly PuzzleLayout Layout;
        private readonly List<PuzzleObject> _removedObjects = new List<PuzzleObject>();

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

        private void AddRemovedObject(PuzzleObject puzzleObject)
        {
            _removedObjects.Add(puzzleObject);
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
