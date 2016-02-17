using System;
using System.IO;
using System.Linq;

namespace Entitas.CodeGenerator {
    public static class CodeGenerator {
        public const string COMPONENT_SUFFIX = "Component";
        public const string DEFAULT_COMPONENT_LOOKUP_TAG = "ComponentIds";

        public static void Generate(ICodeGeneratorDataProvider provider, string directory, ICodeGenerator[] codeGenerators) {
            directory = GetSafeDir(directory);
            CleanDir(directory);
            
            foreach (var generator in codeGenerators.OfType<IPoolCodeGenerator>()) {
                writeFiles(directory, generator.Generate(provider.poolNames));
            }

            foreach (var generator in codeGenerators.OfType<IComponentCodeGenerator>()) {
                writeFiles(directory, generator.Generate(provider.componentInfos));
            }
        }

        public static string GetSafeDir(string directory) {
            if (!directory.EndsWith("/", StringComparison.Ordinal)) {
                directory += "/";
            }
            if (!directory.EndsWith("Generated/", StringComparison.Ordinal)) {
                directory += "Generated/";
            }
            return directory;
        }

        public static void CleanDir(string directory) {
            directory = GetSafeDir(directory);
            if (Directory.Exists(directory)) {
                var files = new DirectoryInfo(directory).GetFiles("*.cs", SearchOption.AllDirectories);
                foreach (var file in files) {
                    try {
                        File.Delete(file.FullName);
                    } catch {
                        Console.WriteLine("Could not delete file " + file);
                    }
                }
            } else {
                Directory.CreateDirectory(directory);
            }
        }

        static void writeFiles(string directory, CodeGenFile[] files) {
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }
            foreach (var file in files) {
                var fileName = directory + file.fileName + ".cs";
                var fileContent = file.fileContent.Replace("\n", Environment.NewLine);
                File.WriteAllText(fileName, fileContent);
            }
        }
    }

    public static class CodeGeneratorExtensions {

        public static string RemoveComponentSuffix(this string componentName) {
            return componentName.EndsWith(CodeGenerator.COMPONENT_SUFFIX, StringComparison.Ordinal)
                    ? componentName.Substring(0, componentName.Length - CodeGenerator.COMPONENT_SUFFIX.Length)
                    : componentName;
        }

        public static string[] ComponentLookupTags(this ComponentInfo componentInfo) {
            if (componentInfo.pools.Length == 0) {
                return new [] { CodeGenerator.DEFAULT_COMPONENT_LOOKUP_TAG };
            }

            return componentInfo.pools
                .Select(poolName => poolName + CodeGenerator.DEFAULT_COMPONENT_LOOKUP_TAG)
                .ToArray();
        }

        public static string UppercaseFirst(this string str) {
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string LowercaseFirst(this string str) {
            return char.ToLower(str[0]) + str.Substring(1);
        }

        public static string ToUnixLineEndings(this string str) {
            return str.Replace(Environment.NewLine, "\n");
        }
    }
}
