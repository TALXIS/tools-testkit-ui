namespace TALXIS.TestKit.Bindings
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Reads test data from JSON files.
    /// </summary>
    public class FileDataRepository : ITestDataRepository
    {
        private const string FileDirectory = "data";
        private static readonly string RootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <inheritdoc cref="ITestDataRepository"/>
        public string GetTestData(string identifier)
        {
            string fileName = Path.GetExtension(identifier) == ".json" ? identifier : $"{identifier}.json";
            string dataRoot = ResolveDataRoot(RootDirectory, FileDirectory) ?? throw new FileNotFoundException($"Could not find data directory '{FileDirectory}' under '{RootDirectory}'.");

            // Check flat path first (backwards-compatible)
            string flatPath = Path.Combine(dataRoot, fileName);
            if (File.Exists(flatPath))
            {
                return File.ReadAllText(flatPath);
            }

            // Search recursively in subdirectories
            string[] matches = Directory.GetFiles(dataRoot, fileName, SearchOption.AllDirectories);
            if (matches.Length == 1)
            {
                return File.ReadAllText(matches[0]);
            }

            if (matches.Length > 1)
            {
                throw new FileNotFoundException($"Ambiguous data file '{fileName}': found in multiple locations under '{dataRoot}'. Use a subdirectory-qualified path to disambiguate.");
            }

            throw new FileNotFoundException($"Could not find data file '{fileName}' in '{dataRoot}' or any of its subdirectories.");
        }

        private static string ResolveDataRoot(string root, string directoryName)
        {
            // Case-sensitive match first (fast path, works on all platforms)
            string candidate = Path.Combine(root, directoryName);
            if (Directory.Exists(candidate))
            {
                return candidate;
            }

            // Case-insensitive fallback (handles Data/ vs data/ on Linux)
            foreach (string dir in Directory.GetDirectories(root))
            {
                if (string.Equals(Path.GetFileName(dir), directoryName, StringComparison.OrdinalIgnoreCase))
                {
                    return dir;
                }
            }

            return null;
        }
    }
}