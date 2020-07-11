using System.Collections.Generic;

namespace TNRD.CodeGeneration
{
    public interface IIndenter
    {
        void AddLevel();
        void AddLevels(int amount);
        void SubtractLevel();
        void SubtractLevels(int amount);
        string Indent(string input);
        string Indent(string input, params string[] args);
        IEnumerable<string> Indent(IEnumerable<string> input);
    }
}