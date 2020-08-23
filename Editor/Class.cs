using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TNRD.CodeGeneration
{
    public class Class
    {
        private readonly string name;

        private readonly List<Type> inheritTypes = new List<Type>();
        private readonly List<Class> classes = new List<Class>();
        private readonly List<Field> fields = new List<Field>();
        private readonly List<Property> properties = new List<Property>();

        public Accessibility Accessibility { get; set; }
        public bool IsPartial { get; set; }
        public bool IsStatic { get; set; }
        public bool IsAbstract { get; set; }

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
            IndentedList lines = new IndentedList();

            lines.Add(GenerateStub());

            lines.Add("{");

            lines.AddLevel();

            foreach (Class @class in classes)
            {
                lines.AddRange(@class.Generate());
            }

            if (classes.Count > 0 && (fields.Count > 0 || properties.Count > 0))
                lines.Add(string.Empty);

            foreach (Field field in fields)
            {
                lines.Add(field.Generate());
            }

            if ((classes.Count > 0 || fields.Count > 0) && properties.Count > 0)
                lines.Add(string.Empty);

            for (int i = 0; i < properties.Count; i++)
            {
                Property property = properties[i];
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
            StringBuilder builder = new StringBuilder(Accessibility.ToPrintableString());

            if (IsStatic)
                builder.Append(" static");

            if (IsAbstract)
                builder.Append(" abstract");

            if (IsPartial)
                builder.Append(" partial");

            builder.Append($" class {name}");

            if (inheritTypes.Count > 0)
                builder.Append(" :");

            for (int i = 0; i < inheritTypes.Count; i++)
            {
                Type inheritType = inheritTypes[i];
                if (i > 0)
                    builder.Append(",");

                builder.Append(inheritType.ToPrintableString());
            }

            return builder.ToString();
        }
    }
}
