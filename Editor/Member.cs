using System;

namespace TNRD.CodeGeneration
{
    public abstract class Member
    {
        private const string INVALID_CHARS = "()";

        private bool isStatic;
        private bool isConst;
        private bool isReadOnly;
        private string name;

        public Accessibility Accessibility { get; set; }
        public Type ValueType { get; set; }

        public bool IsStatic
        {
            get => isStatic;
            set
            {
                isStatic = value;
                if (isStatic)
                    isConst = false;
            }
        }

        public bool IsConst
        {
            get => isConst;
            set
            {
                isConst = value;
                if (isConst)
                {
                    isReadOnly = false;
                    isStatic = false;
                }
            }
        }

        public bool IsReadOnly
        {
            get => isReadOnly;
            set
            {
                isReadOnly = value;
                if (isReadOnly)
                    isConst = false;
            }
        }

        public string Name
        {
            get => name;
            set => name = EscapeName(value);
        }

        private string EscapeName(string input)
        {
            string output = input;

            output = output.Replace(" ", string.Empty);

            for (int i = 0; i < INVALID_CHARS.Length; i++)
            {
                char @char = INVALID_CHARS[i];
                output = output.Replace(@char.ToString(), "");
            }

            return output.Trim();
        }

        protected Member()
        {
        }

        protected Member(string name, Type valueType)
        {
            Name = name;
            ValueType = valueType;
        }
    }
}
