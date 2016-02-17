﻿using System;

namespace Entitas {

    /// Implement this interface if you want to create a system which needs a reference to a pool.
    /// Recommended way to create systems in general: pool.CreateSystem<RenderPositionSystem>();
    /// Calling pool.CreateSystem<RenderPositionSystem>() will automatically inject the pool if ISetPool is implemented.
    /// It's recommended to pass in the pool as a dependency using ISetPool rather than using Pools.pool directly within the pool to avoid tight coupling.
    public interface ISetPool {
        void SetPool(Pool pool);
    }

    public static class PoolExtension {

        /// Returns all entities matching the specified matcher.
        public static Entity[] GetEntities(this Pool pool, IMatcher matcher) {
            return pool.GetGroup(matcher).GetEntities();
        }

        /// This is the recommended way to create systems.
        /// It will create a new instance of the type, will inject the pool if ISetPool is implemented
        /// and will automatically create a ReactiveSystem if it is a IReactiveSystem or IMultiReactiveSystem.
        public static ISystem CreateSystem<T>(this Pool pool) where T: ISystem, new() {
            return pool.CreateSystem(typeof(T));
        }

        /// This is the recommended way to create systems.
        /// It will create a new instance of the type, will inject the pool if ISetPool is implemented
        /// and will automatically create a ReactiveSystem if it is a IReactiveSystem or IMultiReactiveSystem.
        public static ISystem CreateSystem(this Pool pool, Type systemType) {
            var system = (ISystem)Activator.CreateInstance(systemType);
            return pool.CreateSystem(system);
        }

        /// This is the recommended way to create systems.
        /// It will inject the pool if ISetPool is implemented
        /// and will automatically create a ReactiveSystem if it is a IReactiveSystem or IMultiReactiveSystem.
        public static ISystem CreateSystem(this Pool pool, ISystem system) {
            setPool(system, pool);
            var reactiveSystem = system as IReactiveSystem;
            if (reactiveSystem != null) {
                return new ReactiveSystem(pool, reactiveSystem);
            }
            var multiReactiveSystem = system as IMultiReactiveSystem;
            if (multiReactiveSystem != null) {
                return new ReactiveSystem(pool, multiReactiveSystem);
            }

            return system;
        }

        static void setPool(ISystem system, Pool pool) {
            var poolSystem = system as ISetPool;
            if (poolSystem != null) {
                poolSystem.SetPool(pool);
            }
        }
    }
}

