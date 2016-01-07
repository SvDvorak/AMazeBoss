﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Entitas.Unity.VisualDebugging {
    public class DictionaryTypeDrawer : ITypeDrawer {
        public bool HandlesType(Type type) {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
        }

        public object DrawAndGetNewValue(Type type, string fieldName, object value, Entity entity, int index, IComponent component) {
            var dictionary = (IDictionary)value;
            var keyType = type.GetGenericArguments()[0];
            var valueType = type.GetGenericArguments()[1];

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(fieldName);
            if (GUILayout.Button("+", GUILayout.Width(19), GUILayout.Height(14))) {
                object defaultKey;
                if (EntityInspector.CreateDefault(keyType, out defaultKey)) {
                    object defaultValue;
                    if (EntityInspector.CreateDefault(valueType, out defaultValue)) {
                        dictionary[defaultKey] = defaultValue;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            if (dictionary.Count > 0) {
                var indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = indent + 1;

                var keys = new ArrayList(dictionary.Keys);
                for (int i = 0; i < keys.Count; i++) {
                    var key = keys[i];
                    EntityInspector.DrawAndSetElement(keyType, "key", key,
                        entity, index, component, newValue => {
                        var tmpValue = dictionary[key];
                        dictionary.Remove(key);
                        if (newValue != null) {
                            dictionary[newValue] = tmpValue;
                        }
                    });

                    EntityInspector.DrawAndSetElement(valueType, "value", dictionary[key],
                        entity, index, component, newValue => dictionary[key] = newValue);

                    EditorGUILayout.Space();
                }

                EditorGUI.indentLevel = indent;
            }

            return dictionary;
        }
    }
}
