using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TNRD.CodeGeneration
{
    public class IndentedList : IList<string>, IIndenter
    {
        private readonly List<string> indented = new List<string>();
        private readonly List<string> regular = new List<string>();
        private readonly Indenter indenter = new Indenter();

        public IEnumerator<string> GetEnumerator()
        {
            return indented.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(string item)
        {
            indented.Add(indenter.Indent(item));
            regular.Add(item);
        }

        public void AddRange(IEnumerable<string> collection)
        {
            indented.AddRange(collection.Select(indenter.Indent));
            regular.AddRange(collection);
        }

        public void Clear()
        {
            indented.Clear();
            regular.Clear();
        }

        public bool Contains(string item)
        {
            return regular.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            indented.CopyTo(array, arrayIndex);
        }

        public bool Remove(string item)
        {
            var index = IndexOf(item);
            if (indented.InRange(index))
                indented.RemoveAt(index);

            return regular.Remove(item);
        }

        public int Count => indented.Count;

        public bool IsReadOnly => false;

        public int IndexOf(string item)
        {
            return regular.IndexOf(item);
        }

        public void Insert(int index, string item)
        {
            indented.Insert(index, indenter.Indent(item));
            indented.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            indented.RemoveAt(index);
            regular.RemoveAt(index);
        }

        public string this[int index]
        {
            get => indented[index];
            set
            {
                indented[index] = indenter.Indent(value);
                regular[index] = value;
            }
        }

        public void AddLevel()
        {
            indenter.AddLevel();
        }

        public void AddLevels(int amount)
        {
            indenter.AddLevels(amount);
        }

        public void SubtractLevel()
        {
            indenter.SubtractLevel();
        }

        public void SubtractLevels(int amount)
        {
            indenter.SubtractLevels(amount);
        }

        public string Indent(string input)
        {
            return indenter.Indent(input);
        }

        public string Indent(string input, params string[] args)
        {
            return indenter.Indent(input, args);
        }

        public IEnumerable<string> Indent(IEnumerable<string> input)
        {
            return indenter.Indent(input);
        }
    }
}
