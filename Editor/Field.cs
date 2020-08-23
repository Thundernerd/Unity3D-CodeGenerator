using System;
using System.Text;

namespace TNRD.CodeGeneration
{
    public class Field : Member
    {
        public object Value { get; set; }

        public Field()
        {
        }

        public Field(string name, object value, Type valueType) : base(name, valueType)
        {
            Value = value;
        }

        public string Generate()
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
            builder.Append(" =");
            builder.Append($" {GetPrintableValue()}");
            builder.Append(";");

            return builder.ToString();
        }

        private string GetPrintableValue()
        {
            if (ValueType.IsClassOrStruct())
                return $"new {ValueType.ToPrintableString()}()";

            if (ValueType == typeof(string))
                return Wrap(Value.ToString());

            if (ValueType == typeof(float))
                return $"{Value}f";

            if (ValueType == typeof(double))
                return $"{Value}d";

            if (ValueType == typeof(long))
                return $"{Value}L";

            if (ValueType == typeof(decimal))
                return $"{Value}m";

            return Value.ToString();
        }

        private string Wrap(string input)
        {
            return $"\"{input}\"";
        }
    }
}
