using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets.FileOperations
{
    public class DescriptorResolver
    {
        private readonly List<IDescriptorSet> _descriptorSets = new List<IDescriptorSet>()
            {
                new ValueDescriptorSet<int>("ID", e => e.hasId, (e, val) => e.AddId(int.Parse(val)), e => e.id.Value),
                new FlagDescriptorSet("TILE", e => e.isTile, e => e.IsTile(true)),
                new FlagDescriptorSet("BLOCKINGTILE", e => e.isBlockingTile, e => e.IsBlockingTile(true)),
                new FlagDescriptorSet("SPIKETRAP", e => e.isSpikeTrap, e => e.IsSpikeTrap(true)),
                new FlagDescriptorSet("LOADED", e => e.hasLoaded, e => e.AddLoaded(true)),
                new FlagDescriptorSet("CURSESWITCH", e => e.isCurseSwitch, e => e.IsCurseSwitch(true)),
                new FlagDescriptorSet("DYNAMIC", e => e.isDynamic, e => e.IsDynamic(true)),
                new FlagDescriptorSet("ITEM", e => e.isItem, e => e.IsItem(true)),
                new FlagDescriptorSet("SPIKES", e => e.isSpikes, e => e.IsSpikes(true)),
                new FlagDescriptorSet("BOX", e => e.isBox, e => e.IsBox(true)),
                new FlagDescriptorSet("HERO", e => e.isHero, e => e.IsCharacter(true).IsHero(true)),
                new FlagDescriptorSet("BOSS", e => e.isBoss, e => e.IsCharacter(true).IsBoss(true)),
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
                try
                {
                    var correctDescriptorSet = _descriptorSets.Single(ds => IsDescriptor(descriptor, ds));
                    correctDescriptorSet.SetDescriptor(entity, descriptor);
                }
                catch (InvalidOperationException)
                {
                    throw new MultipleDescriptorsFoundException(descriptor);
                }
                catch (Exception)
                {
                    throw new InvalidDescriptorException(descriptor);
                }
            }
        }

        private static bool IsDescriptor(string descriptor, IDescriptorSet ds)
        {
            var isShorter = descriptor.Length < ds.DescriptorText.Length;
            return !isShorter && descriptor.Substring(0, ds.DescriptorText.Length) == ds.DescriptorText;
        }

        private interface IDescriptorSet
        {
            string DescriptorText { get; }
            Func<Entity, bool> HasDescriptor { get; }
            string CreateDescriptorText(Entity entity);
            void SetDescriptor(Entity e, string text);
        }

        private class FlagDescriptorSet : IDescriptorSet
        {
            public FlagDescriptorSet(string descriptorText, Func<Entity, bool> hasDescriptor, Action<Entity> setFlagDescriptor)
            {
                DescriptorText = descriptorText;
                HasDescriptor = hasDescriptor;
                SetFlagDescriptor = setFlagDescriptor;
            }

            public string DescriptorText { get; private set; }
            public Func<Entity, bool> HasDescriptor { get; private set; }

            private Action<Entity> SetFlagDescriptor { get; set; }

            public string CreateDescriptorText(Entity entity)
            {
                return DescriptorText;
            }

            public void SetDescriptor(Entity e, string text)
            {
                SetFlagDescriptor(e);
            }
        }

        private class ValueDescriptorSet<TValueType> : IDescriptorSet
        {
            public ValueDescriptorSet(string descriptorText, Func<Entity, bool> hasDescriptor, Action<Entity, string> setValueDescriptor, Func<Entity, TValueType> getValue = null)
            {
                DescriptorText = descriptorText;
                HasDescriptor = hasDescriptor;
                SetValueDescriptor = setValueDescriptor;
                GetValue = getValue;
            }

            public string DescriptorText { get; private set; }
            public Func<Entity, bool> HasDescriptor { get; private set; }

            private Action<Entity, string> SetValueDescriptor  { get; set; }
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

            public void SetDescriptor(Entity e, string text)
            {
                string value = null;
                if (text.Contains("("))
                {
                    var startIndex = text.IndexOf("(") + 1;
                    var endIndex = text.IndexOf(")");
                    value = text.Substring(startIndex, endIndex - startIndex);
                }
                SetValueDescriptor(e, value);
            }
        }
    }

    public class MultipleDescriptorsFoundException : Exception
    {
        public MultipleDescriptorsFoundException(string descriptor) : base("Found multiple descriptors when using tag " + descriptor)
        {
        }
    }

    public class InvalidDescriptorException : Exception
    {
        public InvalidDescriptorException(string descriptor) : base("Invalid descriptor found: " + descriptor)
        {
        }
    }
}