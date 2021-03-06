﻿using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Entitas;

namespace Assets
{
    public static class EntityExtensions
    {
        public static Entity AddId(this Entity entity, Pool pool)
        {
            var identifiables = pool.GetEntities(GameMatcher.Id);
            var currentId = identifiables.Any() ? identifiables.Max(x => x.id.Value) : 0;
            entity.AddId(currentId + 1);
            return entity;
        }

        public static Entity SetParent(this Entity child, Entity parent, Pool pool)
        {
            if (!parent.hasId)
            {
                parent.AddId(pool);
            }
            child.AddChild(parent.id.Value);
            return child;
        }

        public static bool IsMoving(this Entity entity)
        {
            var target = entity.position.Value.ToV3();
            var current = entity.view.Value.transform.position;
            return entity.hasPosition && entity.hasView && target != current;
        }

        public static void AddActingSequence(this Entity entity, float time, Sequence action)
        {
            action.Pause();
            var actingSequence = new ActingSequence(time, action);
            Queue<ActingSequence> sequences;
            if (entity.hasActingSequences)
            {
                sequences = entity.actingSequences.Sequences;
            }
            else
            {
                sequences = new Queue<ActingSequence>();
                actingSequence.Sequence.Play();
            }

            sequences.Enqueue(actingSequence);

            entity.ReplaceActingSequences(sequences);
        }

        public static void AddActingSequence(this Entity entity, float time, Action action)
        {
            entity.AddActingSequence(time, DOTween.Sequence().OnStart(() => action()));
        }

        public static void AddActingSequence(this Entity entity, float time)
        {
            entity.AddActingSequence(time, DOTween.Sequence());
        }

        public static bool IsTile(this Entity entity)
        {
            return entity.IsObjectType(ObjectType.Tile);
        }

        public static bool IsItem(this Entity entity)
        {
            return entity.IsObjectType(ObjectType.Item);
        }

        public static bool IsArea(this Entity entity)
        {
            return entity.IsObjectType(ObjectType.Area);
        }

        public static bool IsObjectType(this Entity entity, ObjectType type)
        {
            return entity.gameObject.Type == type;
        }
    }
}