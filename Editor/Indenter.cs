using System.Collections.Generic;
using System.Linq;

namespace TNRD.CodeGeneration
{
    public class Indenter : IIndenter
    {
        private readonly char character = ' ';
        private readonly int amountPerLevel = 4;
        private int level = 0;

        public Indenter()
        {
        }

        public Indenter(char character, int amountPerLevel)
        {
            this.character = character;
            this.amountPerLevel = amountPerLevel;
        }

        public void AddLevel()
        {
            level++;
        }

        public void AddLevels(int amount)
        {
            level += amount;
        }

        public void SubtractLevel()
        {
            level--;
            
            if (level < 0)
                level = 0;
        }

        public void SubtractLevels(int amount)
        {
            level -= amount;
            
            if (level < 0)
                level = 0;
        }

        public string Indent(string input)
        {
            var indentation = string.Empty;
            var count = level * amountPerLevel;

            for (int i = 0; i < count; i++)
            {
                indentation += character.ToString();
            }

            return indentation + input;
        }

        public string Indent(string input, params string[] args)
        {
            var indentation = string.Empty;
            var count = level * amountPerLevel;

            for (int i = 0; i < count; i++)
            {
                indentation += character.ToString();
            }

            return indentation + string.Format(input, args);
        }

        public IEnumerable<string> Indent(IEnumerable<string> input)
        {
            var indentation = string.Empty;
            var count = level * amountPerLevel;

            for (int i = 0; i < count; i++)
            {
                indentation += character.ToString();
            }

            return input.Select(x => indentation + x);
        }
    }
}
