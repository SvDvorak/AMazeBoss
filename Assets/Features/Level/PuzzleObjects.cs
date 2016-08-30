using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Assets.Features.Level
{
    public class PuzzleObjects
    {
        private readonly Dictionary<TilePos, PuzzleObject> _objects = new Dictionary<TilePos, PuzzleObject>();

        public event Action<string, TilePos> ObjectAdded;
        public event Action<PuzzleObject> ObjectRemoved;
        public event Action<TilePos, string, object> PropertySet;
        public event Action<TilePos, string> PropertyRemoved;

        public void SetSingleton(string type, TilePos position)
        {
            var existingObject = _objects.Values.SingleOrDefault(x => x.Type == type);
            if (existingObject != null)
            {
                RemoveObject(existingObject.Position);
            }

            PlaceObject(type, position);
        }

        public void PlaceObject(string type, TilePos position, Dictionary<string, PuzzleObject.Property> properties = null)
        {
            RemoveObject(position);

            _objects[position] = new PuzzleObject(type, position);

            if (properties != null)
            {
                properties.ToList().ForEach(x => SetProperty(position, x.Key, x.Value));
            }

            ObjectAdded.CallEvent(type, position);
        }

        public void RemoveObject(TilePos position)
        {
            var currentAtSamePosition = GetObjectAt(position);

            if (currentAtSamePosition != null)
            {
                _objects.Remove(position);

                ObjectRemoved.CallEvent(currentAtSamePosition);
            }
        }

        public PuzzleObject GetObjectAt(TilePos position)
        {
            return _objects.ContainsKey(position) ? _objects[position] : null;
        }

        public PuzzleObject GetSingleton(string type)
        {
            return _objects.Values.SingleOrDefault(x => x.Type == type);
        }

        public List<PuzzleObject> GetObjects(string type)
        {
            return GetAllObjects().Where(x => x.Type == type).ToList();
        }

        public List<PuzzleObject> GetAllObjects()
        {
            return _objects.Values.ToList();
        }

        public void SetProperty(TilePos position, string key, object value)
        {
            try
            {
                var objectToSetProperty = GetObjectAt(position);
                objectToSetProperty.Properties[key] = new PuzzleObject.Property(key, value);
                PropertySet.CallEvent(position, key, value);
            }
            catch (Exception)
            {
                throw new ObjectNotFoundException();
            }
        }

        public void RemoveProperty(TilePos position, string key)
        {
            try
            {
                var objectToSetProperty = GetObjectAt(position);
                objectToSetProperty.Properties.Remove(key);
                PropertyRemoved.CallEvent(position, key);
            }
            catch (NullReferenceException)
            {
                throw new ObjectNotFoundException();
            }
        }

        public bool HasProperty(TilePos position, string key)
        {
            var puzzleObject = GetObjectAt(position);
            return puzzleObject != null && puzzleObject.Properties.ContainsKey(key);
        }

        public PuzzleObject.Property GetProperty(TilePos position, string key)
        {
            try
            {
                return GetObjectAt(position).Properties[key];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception(string.Format("Could not find property {0} at {1}", key, position));
            }
        }

        public class ObjectNotFoundException : Exception
        {
        }
    }
}