using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TNRD.CodeGeneration
{
    public class Property : Member
    {
        private readonly List<string> getterBody = new List<string>();
        private readonly List<string> setterBody = new List<string>();

        public Accessibility WriteAccessibility;

        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }

        public Property()
        {
        }

        public Property(string name, Type valueType) : base(name, valueType)
        {
        }

        public void AddGetterBody(IEnumerable<string> lines)
        {
            getterBody.AddRange(lines.Select(x => x.Trim()));
        }

        public void AddGetterBody(string line)
        {
            getterBody.Add(line.Trim());
        }

        public void ClearGetterBody()
        {
            getterBody.Clear();
        }

        public void AddSetterBody(IEnumerable<string> lines)
        {
            setterBody.AddRange(lines.Select(x => x.Trim()));
        }

        public void AddSetterBody(string line)
        {
            setterBody.Add(line.Trim());
        }

        public void ClearSetterBody()
        {
            setterBody.Clear();
        }

        public IEnumerable<string> Generate()
        {
            IndentedList lines = new IndentedList();

            lines.Add(GenerateIdentifier());

            lines.Add("{");
            lines.AddLevel();

            if (CanRead)
            {
                lines.AddRange(GenerateGetterBody());
            }

            if (CanWrite)
            {
                lines.AddRange(GenerateSetterBody());
            }

            lines.SubtractLevel();
            lines.Add("}");

            return lines;
        }

        private string GenerateIdentifier()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(Accessibility.ToPrintableString());

            if (IsStatic)
                builder.Append(" static");

            if (IsConst)
                builder.Append(" const");

            if (IsReadOnly)
                builder.Append(" readonly");

            builder.Append($" {ValueType.ToPrintableString()}");
            builder.Append($" {Name}");

            return builder.ToString();
        }

        private IEnumerable<string> GenerateGetterBody()
        {
            if (getterBody.Count == 0)
                return new List<string> {"{ get; }"};
            if (getterBody.Count == 1)
                return new List<string> {$"get {{ {getterBody.First()} }}"};

            IndentedList lines = new IndentedList();

            lines.Add("get");
            lines.Add("{");
            lines.AddLevel();
            lines.AddRange(getterBody);
            lines.SubtractLevel();
            lines.Add("}");

            return lines;
        }

        private IEnumerable<string> GenerateSetterBody()
        {
            if (setterBody.Count == 0)
                return new List<string> {"{ set; }"};
            if (getterBody.Count == 1)
                return new List<string> {"set { " + setterBody.First() + " }"};

            IndentedList lines = new IndentedList();

            lines.Add("set");
            lines.Add("{");
            lines.AddLevel();
            lines.AddRange(setterBody);
            lines.SubtractLevel();
            lines.Add("}");

            return lines;
        }
    }
}
