using System;
using System.Collections.Generic;
using System.Linq;
using Assets.LevelEditor;
using Entitas;

namespace Assets.FileOperations
{
    public class DescriptorResolver
    {
        private readonly List<IDescriptorSet> _descriptorSets = new List<IDescriptorSet>()
            {
                new ValueDescriptorSet<int>("GAMEOBJECT", e => e.hasGameObject, (e, val) => e.AddGameObject((ObjectType)int.Parse(val)), e => (int)e.gameObject.Type),
                new ValueDescriptorSet<int>("ID", e => e.hasId, (e, val) => e.AddId(int.Parse(val)), e => e.id.Value),
                new FlagDescriptorSet("WALL", e => e.isWall, e => e.IsWall(true)),
                new FlagDescriptorSet("BLOCKINGTILE", e => e.isBlockingTile, e => e.IsBlockingTile(true)),
                new FlagDescriptorSet("SPIKETRAP", e => e.isSpikeTrap, e => e.IsSpikeTrap(true)),
                new FlagDescriptorSet("LOADED", e => e.hasLoaded, e => e.AddLoaded(true)),
                new FlagDescriptorSet("CURSESWITCH", e => e.isCurseSwitch, e => e.IsCurseSwitch(true)),
                new FlagDescriptorSet("DYNAMIC", e => e.isDynamic, e => e.IsDynamic(true)),
                new FlagDescriptorSet("SPIKES", e => e.isSpikes, e => e.IsSpikes(true)),
                new FlagDescriptorSet("BOX", e => e.isBox, e => e.IsBox(true)),
                new FlagDescriptorSet("VICTORYEXIT", e => e.isVictoryExit, e => e.IsVictoryExit(true)),
                new FlagDescriptorSet("LEVELEXIT", e => e.isExitTrigger, e => e.IsExitTrigger(true)),
                new FlagDescriptorSet("SETCHECKPOINT", e => e.hasSetCheckpoint, e => e.HasSetCheckpoint(true)),
                new FlagDescriptorSet("PUZZLE", e => e.isPuzzleArea, e => e.IsPuzzleArea(true)),
                new ValueDescriptorSet<int>("BOSSCONNECTION", e => e.hasBossConnection, (e, val) => e.AddBossConnection(int.Parse(val)), e => e.bossConnection.BossId),
                new ValueDescriptorSet<int>("EDITORONLYVISUAL", e => e.hasEditorOnlyVisual, (e, val) => e.AddEditorOnlyVisual((ViewMode)int.Parse(val)), e => (int)e.editorOnlyVisual.ShowInMode),
                new FlagDescriptorSet("HERO", e => e.isHero, e => e.IsCharacter(true).IsHero(true)),
                new FlagDescriptorSet("SPIKESCARRIED", e => e.isSpikesCarried, e => e.IsSpikesCarried(true)),
                new FlagDescriptorSet("BOSS", e => e.isBoss, e => e.IsCharacter(true).IsBoss(true)),
                new FlagDescriptorSet("DEAD", e => e.isDead, e => e.IsDead(true)),
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
                var currentDescriptor = descriptor;
                var matchedDescriptors = _descriptorSets
                    .Where(ds => ds.SetDescriptor(entity, currentDescriptor))
                    .ToList();

                if (matchedDescriptors.Count() > 1)
                {
                    throw new MultipleDescriptorsFoundException(descriptor, matchedDescriptors);
                }
                if (!matchedDescriptors.Any())
                {
                    throw new InvalidDescriptorException(descriptor);
                }
            }
        }

        public interface IDescriptorSet
        {
            Func<Entity, bool> HasDescriptor { get; }
            string CreateDescriptorText(Entity entity);
            bool SetDescriptor(Entity e, string text);
        }

        private class FlagDescriptorSet : IDescriptorSet
        {
            public FlagDescriptorSet(string descriptorText, Func<Entity, bool> hasDescriptor, Action<Entity> setFlagDescriptor)
            {
                _descriptorText = descriptorText;
                HasDescriptor = hasDescriptor;
                SetFlagDescriptor = setFlagDescriptor;
            }

            private readonly string _descriptorText;

            public Func<Entity, bool> HasDescriptor { get; private set; }
            private Action<Entity> SetFlagDescriptor { get; set; }

            public string CreateDescriptorText(Entity entity)
            {
                return _descriptorText;
            }

            public bool SetDescriptor(Entity e, string text)
            {
                if (text == _descriptorText)
                {
                    SetFlagDescriptor(e);
                    return true;
                }

                return false;
            }

            public override string ToString()
            {
                return _descriptorText;
            }
        }

        private class ValueDescriptorSet<TValueType> : IDescriptorSet
        {
            public ValueDescriptorSet(string descriptorText, Func<Entity, bool> hasDescriptor, Action<Entity, string> setValueDescriptor, Func<Entity, TValueType> getValue = null)
            {
                _descriptorText = descriptorText;
                HasDescriptor = hasDescriptor;
                SetValueDescriptor = setValueDescriptor;
                GetValue = getValue;
            }

            private readonly string _descriptorText;

            public Func<Entity, bool> HasDescriptor { get; private set; }
            private Action<Entity, string> SetValueDescriptor { get; set; }
            private Func<Entity, TValueType> GetValue { get; set; }

            public string CreateDescriptorText(Entity entity)
            {
                var descriptor = _descriptorText;
                if (GetValue != null)
                {
                    descriptor += "(" + GetValue(entity) + ")";
                }
                return descriptor;
            }

            public bool SetDescriptor(Entity e, string text)
            {
                var parseResult = TryParseNameAndValue(text);
                if (parseResult != null && parseResult.Descriptor == _descriptorText)
                {
                    SetValueDescriptor(e, parseResult.Value);
                    return true;
                }
                return false;
            }

            private ParseResult TryParseNameAndValue(string text)
            {
                if (text.Contains("("))
                {
                    var startIndex = text.IndexOf("(") + 1;
                    var endIndex = text.IndexOf(")");
                    var descriptor = text.Substring(0, startIndex - 1);
                    var value = text.Substring(startIndex, endIndex - startIndex);
                    return new ParseResult(descriptor, value);
                }

                return null;
            }

            private class ParseResult
            {
                public readonly string Descriptor;
                public readonly string Value;

                public ParseResult(string descriptor, string value)
                {
                    Descriptor = descriptor;
                    Value = value;
                }
            }

            public override string ToString()
            {
                return _descriptorText;
            }
        }
    }

    public class MultipleDescriptorsFoundException : Exception
    {
        public MultipleDescriptorsFoundException(string descriptor, List<DescriptorResolver.IDescriptorSet> matchedDescriptors) :
            base(string.Format("Found multiple descriptors when using tag {0}, matching descriptors: {1}",
                descriptor,
                string.Join(", ", matchedDescriptors.Select(x => x.ToString()).ToArray())))
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