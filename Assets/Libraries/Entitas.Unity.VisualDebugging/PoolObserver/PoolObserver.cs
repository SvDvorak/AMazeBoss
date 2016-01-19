﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entitas.Unity.VisualDebugging {
    public class PoolObserver {
        public Pool pool { get { return _pool; } }
        public Group[] groups { get { return _groups.ToArray(); }}
        public GameObject entitiesContainer { get { return _entitiesContainer.gameObject; } }

        readonly Pool _pool;
        readonly Type[] _componentTypes;
        readonly List<Group> _groups;
        readonly Transform _entitiesContainer;

        public PoolObserver(Pool pool, Type[] componentTypes) {
            _pool = pool;
            _componentTypes = componentTypes;
            _groups = new List<Group>();
            _entitiesContainer = new GameObject().transform;
            _entitiesContainer.gameObject.AddComponent<PoolObserverBehaviour>().Init(this);

            _pool.OnEntityCreated += onEntityCreated;
            _pool.OnGroupCreated += onGroupCreated;
            _pool.OnGroupCleared += onGroupCleared;
        }

        void onEntityCreated(Pool pool, Entity entity) {
            var entityBehaviour = new GameObject().AddComponent<EntityBehaviour>();
            entityBehaviour.Init(_pool, entity, _componentTypes);
            entityBehaviour.transform.SetParent(_entitiesContainer, false);
        }

        void onGroupCreated(Pool pool, Group group) {
            _groups.Add(group);
        }

        void onGroupCleared(Pool pool, Group group) {
            _groups.Remove(group);
        }

        public override string ToString() {
            return _entitiesContainer.name = 
                _pool.metaData.poolName + " (" +
                _pool.count + " entities, " +
                _pool.reusableEntitiesCount + " reusable, " +
                _pool.retainedEntitiesCount + " retained, " +
                _groups.Count + " groups)";
        }
    }
}