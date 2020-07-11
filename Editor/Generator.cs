using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TNRD.CodeGeneration
{
    public class Generator
    {
        private readonly string @namespace;

        private readonly List<string> namespaces = new List<string>();

        private readonly List<Class> classes = new List<Class>();

        private bool HasNamespace => !string.IsNullOrEmpty(@namespace);

        public Generator()
        {
        }

        public Generator(string @namespace)
        {
            this.@namespace = @namespace;
        }

        public void AddNamespace(string @namespace)
        {
            @namespace = @namespace.Trim();

            if (!@namespace.EndsWith(";"))
                @namespace = @namespace + ";";

            if (!@namespace.StartsWith("using "))
                @namespace = "using " + @namespace;

            namespaces.Add(@namespace);
        }

        public void AddClass(Class @class)
        {
            classes.Add(@class);
        }

        public IEnumerable<string> Generate()
        {
            var lines = new IndentedList();

            foreach (var t in namespaces)
            {
                lines.Add(t);
            }

            if (namespaces.Count > 0)
                lines.Add(string.Empty);

            if (HasNamespace)
            {
                lines.Add($"namespace {@namespace}");
                lines.Add("{");
                lines.AddLevel();
            }

            for (int i = 0; i < classes.Count; i++)
            {
                var @class = classes[i];

                lines.AddRange(@class.Generate());
            }


            if (HasNamespace)
            {
                lines.SubtractLevel();
                lines.Add("}");
            }

            return lines;
        }

        public void SaveToFile(string path)
        {
            var contents = Generate();

            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllLines(path, contents.ToArray());
        }
    }
}
