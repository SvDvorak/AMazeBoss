﻿using System;
using System.Linq;
using System.Reflection;
using Entitas;
using Entitas.CodeGenerator;
using UnityEditor;

namespace Entitas.Unity.CodeGenerator {
    public static class CodeGenerator {

        [MenuItem("Entitas/Generate", false, 1)]
        public static void Generate() {
            assertCanGenerate();

            var types = Assembly.GetAssembly(typeof(Entity)).GetTypes();
            var codeGenerators = GetCodeGenerators();
            var codeGeneratorNames = codeGenerators.Select(cg => cg.Name).ToArray();
            var config = new CodeGeneratorConfig(EntitasPreferences.LoadConfig(), codeGeneratorNames);

            var enabledCodeGeneratorNames = config.enabledCodeGenerators;
            var enabledCodeGenerators = codeGenerators
                .Where(type => enabledCodeGeneratorNames.Contains(type.Name))
                .Select(type => (ICodeGenerator)Activator.CreateInstance(type))
                .ToArray();

            Entitas.CodeGenerator.CodeGenerator.Generate(types, config.pools, config.generatedFolderPath, enabledCodeGenerators);

            AssetDatabase.Refresh();
        }

        public static Type[] GetCodeGenerators() {
            return Assembly.GetAssembly(typeof(ICodeGenerator)).GetTypes()
                .Where(type => type.GetInterfaces().Contains(typeof(ICodeGenerator))
                    && type != typeof(ICodeGenerator)
                    && type != typeof(IPoolCodeGenerator)
                    && type != typeof(IComponentCodeGenerator)
                    && type != typeof(ISystemCodeGenerator))
                .OrderBy(type => type.FullName)
                .ToArray();
        }

        static void assertCanGenerate() {
            if (EditorApplication.isCompiling) {
                throw new Exception("Can not generate because Unity is still compiling. Please wait...");
            }

            var assembly = Assembly.GetAssembly(typeof(Editor));
            var logEntries = assembly.GetType("UnityEditorInternal.LogEntries");
            logEntries.GetMethod("Clear").Invoke(new object(), null);
            var canCompile = (int)logEntries.GetMethod("GetCount").Invoke(new object(), null) == 0;
            if (!canCompile) {
                throw new Exception("Can not generate because there are compile errors!");
            }
        }
    }
}
