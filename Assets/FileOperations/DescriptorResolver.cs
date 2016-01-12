using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets.FileOperations
{
    public class DescriptorResolver
    {
        private interface IDescriptorSet
        {
            string DescriptorText { get; }
            Func<Entity, bool> HasDescriptor { get; }
            string CreateDescriptorText(Entity entity);
            void SetDescriptorLOL(Entity e, string text);
        }

        private class FlagDescriptorSet : IDescriptorSet
        {
            public FlagDescriptorSet(string descriptorText, Func<Entity, bool> hasDescriptor, Action<Entity> setDescriptor)
            {
                DescriptorText = descriptorText;
                HasDescriptor = hasDescriptor;
                SetDescriptor = setDescriptor;
            }

            public string DescriptorText { get; private set; }
            public Func<Entity, bool> HasDescriptor { get; private set; }

            private Action<Entity> SetDescriptor { get; set; }

            public string CreateDescriptorText(Entity entity)
            {
                return DescriptorText;
            }

            public void SetDescriptorLOL(Entity e, string text)
            {
                SetDescriptor(e);
            }
        }

        private class ValueDescriptorSet<TValueType> : IDescriptorSet
        {
            public ValueDescriptorSet(string descriptorText, Func<Entity, bool> hasDescriptor, Action<Entity, string> setDescriptor, Func<Entity, TValueType> getValue = null)
            {
                DescriptorText = descriptorText;
                HasDescriptor = hasDescriptor;
                SetDescriptor = setDescriptor;
                GetValue = getValue;
            }

            public string DescriptorText { get; private set; }
            public Func<Entity, bool> HasDescriptor { get; private set; }

            private Action<Entity, string> SetDescriptor  { get; set; }
            private Func<Entity, TValueType> GetValue  { get; set; }

            public string CreateDescriptorText(Entity entity)
            {
                var descriptor = DescriptorText;
                if (GetValue != null)
                {
                    descriptor += "(" + GetValue(entity) + ")";
                }
                return descriptor;
            }

            public void SetDescriptorLOL(Entity e, string text)
            {
                string value = null;
                if (text.Contains("("))
                {
                    var startIndex = text.IndexOf("(") + 1;
                    var endIndex = text.IndexOf(")");
                    value = text.Substring(startIndex, endIndex - startIndex);
                }
                SetDescriptor(e, value);
            }
        }

        private readonly List<IDescriptorSet> _descriptorSets = new List<IDescriptorSet>()
            {
                new ValueDescriptorSet<int>("ID", e => e.hasId, (e, val) => e.AddId(int.Parse(val)), e => e.id.Value),
                new FlagDescriptorSet("TILE", e => e.isTile, e => e.IsTile(true)),
                new FlagDescriptorSet("WALKABLE", e => e.isWalkable, e => e.IsWalkable(true)),
                new FlagDescriptorSet("SPIKETRAP", e => e.hasSpikeTrap, e => e.AddSpikeTrap(false)),
                new FlagDescriptorSet("DYNAMIC", e => e.isDynamic, e => e.IsDynamic(true)),
                new FlagDescriptorSet("ITEM", e => e.isItem, e => e.IsItem(true)),
                new FlagDescriptorSet("SPIKES", e => e.isSpikes, e => e.IsSpikes(true)),
                new FlagDescriptorSet("HERO", e => e.isHero, e => e.IsHero(true)),
                new FlagDescriptorSet("BOSS", e => e.isBoss, e => e.IsBoss(true)),
                new FlagDescriptorSet("CURSED", e => e.isCursed, e => e.IsCursed(true)),
                new ValueDescriptorSet<int>("HEALTH", e => e.hasHealth, (e, val) => e.AddHealth(int.Parse(val)), e => e.health.Value),
            };

        public IEnumerable<string> ToDescriptors(Entity entity)
        {
            return _descriptorSets
                .Where(ds => ds.HasDescriptor(entity))
                .Select(ds => ds.CreateDescriptorText(entity))
                .ToList();
        }

        public void FromDescriptors(string descriptors, Entity entity)
        {
            foreach (var descriptor in descriptors.Split(';'))
            {
                var correctDescriptorSet = _descriptorSets.Single(ds => descriptor.Contains(ds.DescriptorText));
                correctDescriptorSet.SetDescriptorLOL(entity, descriptor);
            }
        }
    }
}