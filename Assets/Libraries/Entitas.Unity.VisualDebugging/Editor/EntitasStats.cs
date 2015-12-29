﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Entitas.CodeGenerator;
using UnityEditor;
using UnityEngine;

namespace Entitas.Unity.VisualDebugging {
    public static class EntitasStats {

        [MenuItem("Entitas/Log Stats", false, 2)]
        public static void LogStats() {
            foreach (var stat in GetStats()) {
                Debug.Log(stat.Key + ": " + stat.Value);
            }
        }

        public static Dictionary<string, int> GetStats() {
            var types = Assembly.GetAssembly(typeof(Entity)).GetTypes();
            var components = types.Where(implementsComponent).ToArray();
            var pools = getPools(components);

            var stats = new Dictionary<string, int> {
                { "Total Components", components.Length },
                { "Systems", types.Count(implementsSystem) }
            };

            foreach (var pool in pools) {
                stats.Add("Components in " + pool.Key, pool.Value);
            }

            return stats;
        }

        static bool implementsComponent(Type type) {
            return type.GetInterfaces().Contains(typeof(IComponent))
                && type != typeof(IComponent);
        }

        static Dictionary<string, int> getPools(Type[] components) {
            return components.Aggregate(new Dictionary<string, int>(), (lookups, type) => {
                var lookupTags = type.PoolNames();
                if (lookupTags.Length == 0) {
                    lookupTags = new [] { "Pool" };
                }
                foreach (var lookupTag in lookupTags) {
                    if (!lookups.ContainsKey(lookupTag)) {
                        lookups.Add(lookupTag, 0);
                    }

                    lookups[lookupTag] += 1;
                }
                return lookups;
            });
        }

        static bool implementsSystem(Type type) {
            return type.GetInterfaces().Contains(typeof(ISystem))
                && type != typeof(ISystem)
                && type != typeof(IInitializeSystem)
                && type != typeof(IExecuteSystem)
                && type != typeof(IReactiveSystem)
                && type != typeof(ReactiveSystem)
                && type != typeof(Systems)
                && type != typeof(DebugSystems);
        }
    }
}

