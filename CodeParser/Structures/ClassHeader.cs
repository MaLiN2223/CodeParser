
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeParser.Structures
{
    internal class ClassHeader
    {
        private List<string> modifiers { get; set; } = new List<string>();
        private List<string> inheritence { get; set; } = new List<string>();
        public string Modifiers { get; }
        public string Name { get; }
        public string Inheritence { get; } = "object"; // TODO : inheritence
        public string FullName { get; }

        public ClassHeader(string fullNamespaceName, string header)
        {
            var splitted = header.Split(' ');
            int i = 0;
            while (splitted[i] != "class")
            {
                modifiers.Add(splitted[i]);
                ++i;
            }
            i++;
            Name = splitted[i];
            bool skipDot = Name[Name.Length - 1] == ':';
            if (skipDot)
            {
                Name = Name.Substring(0, Name.Length - 1);
            }
            else
            {
                ++i;
            }
            while (i < splitted.Length)
            {
                inheritence.Add(splitted[i]);
                ++i;
            }
            Modifiers = modifiers.Aggregate("", (x, y) => x + " " + y);
            if (inheritence.Count != 0)
                Inheritence = inheritence.Aggregate("", (x, y) => x + " " + y);
            FullName = $"{fullNamespaceName}.{Name}";
        }

        private static readonly string[] PossibleModifiers = { "public", "protected", "internal", "private", "abstract", "sealed", "new" };

    }
}
