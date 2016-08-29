using System;
using Assets.Features.Level;
using Assets.Level;

namespace Assets.Editor.Undo
{
    public class SetPropertyCommand : ICommand
    {
        private readonly PuzzleLayout _sut;
        private readonly TilePos _position;
        private readonly string _key;
        private readonly object _value;
        private object _currentValue;

        public string Name { get { return "Set property"; } }

        public SetPropertyCommand(PuzzleLayout sut, TilePos position, string key, object value)
        {
            _sut = sut;
            _position = position;
            _key = key;
            _value = value;
        }

        public void Execute()
        {
            try
            {
                _currentValue = _sut.GetProperty(_position, _key);
            }
            catch (Exception)
            {
                _currentValue = null;
            }

            _sut.SetProperty(_position, _key, _value);
        }

        public void Undo()
        {
            if (_currentValue == null)
            {
                _sut.RemoveProperty(_position, _key);
            }
            else
            {
                _sut.SetProperty(_position, _key, _currentValue);
            }
        }
    }
}
