using System.Collections.Generic;
using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public class SetSingletonObjectCommand : ICommand
    {
        private readonly TilePos? _position;
        private readonly PuzzleLayout _layout;
        private readonly TilePos? _previousPosition;
        private readonly string _type;
        private readonly List<WorldObject> _removedObjects = new List<WorldObject>();

        public string Name { get { return (_position != null ? "Added" : "Removed") + " player"; } }

        public SetSingletonObjectCommand(PuzzleLayout layout, string type, TilePos? position)
        {
            _type = type;
            _layout = layout;
            _position = position;
            _previousPosition = layout.GetSingleton(type);
        }

        public void Execute()
        {
            _layout.SingletonRemoved += AddRemovedObject;

            _layout.SetSingleton(_type, _position);

            _layout.SingletonRemoved -= AddRemovedObject;
        }

        private void AddRemovedObject(string type, TilePos? position)
        {
            _removedObjects.Add(new WorldObject(type, position));
        }

        public void Undo()
        {
            _layout.SetSingleton(_type, _previousPosition);

            foreach (var removedObject in _removedObjects)
            {
                _layout.SetSingleton(removedObject.Type, removedObject.Position);
            }

            _removedObjects.Clear();
        }
    }
}
