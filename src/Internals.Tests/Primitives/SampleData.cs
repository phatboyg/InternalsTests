namespace Trie.Tests
{
    using System.Collections.Generic;
    using System.IO;


    public class SampleData
    {
        static string[] _lastNames;

        public static string[] LastNames
        {
            get
            {
                if (_lastNames != null)
                    return _lastNames;

                const string path = "Internals.Tests.Primitives.LastNames.txt";

                using (Stream stream = typeof(SampleData).Assembly.GetManifestResourceStream(path))
                {
                    if (stream == null)
                        throw new FileNotFoundException("Unable to load resource", path);

                    using (var reader = new StreamReader(stream))
                    {
                        var names = new List<string>();

                        var trimChars = new[] {' ', '\t', '\r', '\n'};

                        while (!reader.EndOfStream)
                        {
                            string text = reader.ReadLine();
                            if (string.IsNullOrWhiteSpace(text))
                                continue;

                            names.Add(text.Trim(trimChars));
                        }

                        _lastNames = names.ToArray();
                    }
                }

                return _lastNames;
            }
        }
    }
}