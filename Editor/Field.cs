using System;

namespace TNRD.CodeGeneration
{
    public class Field : Member
    {
        public object Value;

        public Field()
        {
        }

        public Field(string name, object value, Type valueType) : base(name, valueType)
        {
            Value = value;
        }

        public string Generate()
        {
            var output = string.Empty;

            output += Accessibility.ToPrintableString();

            if (IsStatic)
                output += " static";

            if (IsConst)
                output += " const";

            if (IsReadOnly)
                output += " readonly";

            output += " " + ValueType.ToPrintableString();

            output += " " + Name;

            output += " =";

            output += " " + GetPrintableValue();

            output += ";";

            return output;
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
