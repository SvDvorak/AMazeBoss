using System;
using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public class AddObjectCommand : ICommand
    {
        private PuzzleLayout _layout;
        private string _type;
        private TilePos _position;

        public string Name { get { return "Added object"; } }

        public AddObjectCommand(PuzzleLayout layout, string type, TilePos position)
        {
            _position = position;
            _type = type;
            _layout = layout;
        }

        public void Execute()
        {
            _layout.AddObject(_type, _position);
        }

        public void Undo()
        {
        }
    }
}
