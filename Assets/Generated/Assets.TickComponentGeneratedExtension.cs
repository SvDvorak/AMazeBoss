using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.TickComponent tick { get { return (Assets.TickComponent)GetComponent(ComponentIds.Tick); } }

        public bool hasTick { get { return HasComponent(ComponentIds.Tick); } }

        static readonly Stack<Assets.TickComponent> _tickComponentPool = new Stack<Assets.TickComponent>();

        public static void ClearTickComponentPool() {
            _tickComponentPool.Clear();
        }

        public Entity AddTick(float newTimeLeft) {
            var component = _tickComponentPool.Count > 0 ? _tickComponentPool.Pop() : new Assets.TickComponent();
            component.TimeLeft = newTimeLeft;
            return AddComponent(ComponentIds.Tick, component);
        }

        public Entity ReplaceTick(float newTimeLeft) {
            var previousComponent = hasTick ? tick : null;
            var component = _tickComponentPool.Count > 0 ? _tickComponentPool.Pop() : new Assets.TickComponent();
            component.TimeLeft = newTimeLeft;
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

        public Entity SetTick(float newTimeLeft) {
            if (hasTick) {
                throw new SingleEntityException(Matcher.Tick);
            }
            var entity = CreateEntity();
            entity.AddTick(newTimeLeft);
            return entity;
        }

        public Entity ReplaceTick(float newTimeLeft) {
            var entity = tickEntity;
            if (entity == null) {
                entity = SetTick(newTimeLeft);
            } else {
                entity.ReplaceTick(newTimeLeft);
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
