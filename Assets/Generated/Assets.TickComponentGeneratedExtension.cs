using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.TickComponent tick { get { return (Assets.TickComponent)GetComponent(ComponentIds.Tick); } }

        public bool hasTick { get { return HasComponent(ComponentIds.Tick); } }

        static readonly Stack<Assets.TickComponent> _tickComponentPool = new Stack<Assets.TickComponent>();

        public static void ClearTickComponentPool() {
            _tickComponentPool.Clear();
        }

        public Entity AddTick(float newTime) {
            var component = _tickComponentPool.Count > 0 ? _tickComponentPool.Pop() : new Assets.TickComponent();
            component.Time = newTime;
            return AddComponent(ComponentIds.Tick, component);
        }

        public Entity ReplaceTick(float newTime) {
            var previousComponent = hasTick ? tick : null;
            var component = _tickComponentPool.Count > 0 ? _tickComponentPool.Pop() : new Assets.TickComponent();
            component.Time = newTime;
            ReplaceComponent(ComponentIds.Tick, component);
            if (previousComponent != null) {
                _tickComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveTick() {
            var component = tick;
            RemoveComponent(ComponentIds.Tick);
            _tickComponentPool.Push(component);
            return this;
        }
    }

    public partial class Pool {
        public Entity tickEntity { get { return GetGroup(Matcher.Tick).GetSingleEntity(); } }

        public Assets.TickComponent tick { get { return tickEntity.tick; } }

        public bool hasTick { get { return tickEntity != null; } }

        public Entity SetTick(float newTime) {
            if (hasTick) {
                throw new SingleEntityException(Matcher.Tick);
            }
            var entity = CreateEntity();
            entity.AddTick(newTime);
            return entity;
        }

        public Entity ReplaceTick(float newTime) {
            var entity = tickEntity;
            if (entity == null) {
                entity = SetTick(newTime);
            } else {
                entity.ReplaceTick(newTime);
            }

            return entity;
        }

        public void RemoveTick() {
            DestroyEntity(tickEntity);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTick;

        public static IMatcher Tick {
            get {
                if (_matcherTick == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Tick);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherTick = matcher;
                }

                return _matcherTick;
            }
        }
    }
}
