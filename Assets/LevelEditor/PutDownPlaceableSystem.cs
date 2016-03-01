using System;
using System.Collections.Generic;
using Entitas;

namespace Assets.LevelEditor
{
    public class PutDownPlaceableSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.InputPlace, GameMatcher.Position).OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return GameMatcher.Input; } }

        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            var input = entities.SingleEntity();

            var selectedPlaceable = input.selectedPlaceable.Value;
            var preview = _pool.GetGroup(GameMatcher.Preview).GetSingleEntity();
            var position = input.position.Value;

            // Spikes
            var tileBelow = _pool.GetTileAt(position);
            var tileIsSpikeTrap = tileBelow != null && tileBelow.isSpikeTrap;

            if (preview.maintype.Value == ItemType.Spikes.ToString() && tileIsSpikeTrap && !tileBelow.hasLoaded)
            {
                tileBelow.AddLoaded(true);
            }
            else
            {
                var currentObject = _pool.GetEntityAt(position, x => x.gameObject.Type == preview.gameObject.Type);
                var newRotation = UnityEngine.Random.Range(0, 4);

                if (currentObject != null)
                {
                    newRotation = currentObject.rotation.GetNextRotation();
                    currentObject.IsDestroyed(true);
                }

                var newObject = _pool.CreateEntity()
                    .ReplacePosition(position)
                    .AddRotation(newRotation);

                selectedPlaceable.Do(newObject);
            }
        }
    }

    //public class Selector<T>
    //{
    //    private readonly Func<Entity, bool> _matcher;

    //    public Selector(Func<Entity, bool> matcher, T value)
    //    {
    //        _matcher = matcher;
    //        Value = value;
    //    }

    //    public bool IsMatch(Entity entity)
    //    {
    //        return _matcher(entity);
    //    }

    //    public T Value { get; private set; }
    //}

    //public class SelectorList<T> : List<Selector<T>>
    //{
    //}
}
