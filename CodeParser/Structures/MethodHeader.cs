using System.Collections.Generic;
using System.Linq;
using CodeParser.Extensions;

namespace CodeParser.Structures
{
    public enum MethodTypes
    {
        Constructor, Static, Class
    }
    public class MethodHeader
    {
        public List<string> Modifiers { get; set; } = new List<string>();
        public string Name { get; }
        public string FullName { get; }
        public string ReturnType { get; }
        public MethodTypes Type { get; set; } = MethodTypes.Class;
        public List<string> Arguments { get; } = new List<string>();

        public MethodHeader(string data, string className = "")
        {
            var splittedByBracket = data.Split('(');
            var splitted = splittedByBracket[0].Split(' ');
            int i = 0;
            while (i < splitted.Length)
            {
                Modifiers.Add(splitted[i]);
                ++i;
            }
            Name = Modifiers[Modifiers.Count - 1];
            Modifiers.RemoveAt(Modifiers.Count - 1);
            var tmp = Modifiers[Modifiers.Count - 1];
            if (PossibleModifiers.Contains(tmp))
            {
                Type = MethodTypes.Constructor;
            }
            else
            {
                ReturnType = tmp;
                Modifiers.RemoveAt(Modifiers.Count - 1);
            }
            splitted = splittedByBracket[1].Replace(")", "").Split(',');
            i = 0;
            while (i < splitted.Length)
            {
                Arguments.Add(splitted[i]);
                ++i;
            }
            if (Modifiers.Contains("static"))
            {
                FullName = $"{className}.{Name}";
                Type = MethodTypes.Static;
            }
            else FullName = Name;
        }
        private static readonly string[] PossibleModifiers = { "public", "protected", "internal", "private", "abstract", "sealed", "new" };
    }
}
