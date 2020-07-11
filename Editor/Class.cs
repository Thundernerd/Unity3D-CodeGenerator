using System;
using System.Collections.Generic;
using System.Linq;

namespace TNRD.CodeGeneration
{
    public class Class
    {
        private string name;

        private readonly List<Type> inheritTypes = new List<Type>();
        private readonly List<Class> classes = new List<Class>();
        private readonly List<Field> fields = new List<Field>();
        private readonly List<Property> properties = new List<Property>();

        public Accessibility Accessibility;
        public bool IsPartial;
        public bool IsStatic;
        public bool IsAbstract;

        public Class(string name)
        {
            this.name = name;
        }

        public void AddClass(Class @class)
        {
            classes.Add(@class);
        }

        public void AddField(Field field)
        {
            fields.Add(field);
        }

        public void AddProperty(Property property)
        {
            properties.Add(property);
        }

        public void AddInheritedType(Type type)
        {
            inheritTypes.Add(type);
        }

        public string[] Generate()
        {
            var lines = new IndentedList();

            lines.Add(GenerateStub());

            lines.Add("{");

            lines.AddLevel();

            foreach (var @class in classes)
            {
                lines.AddRange(@class.Generate());
            }

            if (classes.Count > 0 && (fields.Count > 0 || properties.Count > 0))
                lines.Add(string.Empty);

            foreach (var field in fields)
            {
                lines.Add(field.Generate());
            }

            if ((classes.Count > 0 || fields.Count > 0) && properties.Count > 0)
                lines.Add(string.Empty);

            for (int i = 0; i < properties.Count; i++)
            {
                var property = properties[i];
                lines.AddRange(property.Generate());

                if (i < properties.Count - 1)
                    lines.Add(string.Empty);
            }

            lines.SubtractLevel();

            lines.Add("}");

            return lines.ToArray();
        }

        private string GenerateStub()
        {
            var name = Accessibility.ToPrintableString();

            if (IsStatic)
                name += " static";

            if (IsAbstract)
                name += " abstract";

            if (IsPartial)
                name += " partial";

            name += string.Format(" class {0}", this.name);

            if (inheritTypes.Count > 0)
                name += " :";

            for (int i = 0; i < inheritTypes.Count; i++)
            {
                var inheritType = inheritTypes[i];
                if (i > 0)
                    name += ",";

                name += inheritType.ToPrintableString();
            }

            return name;
        }
    }
}
