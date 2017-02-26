
using System;
using System.Collections.Generic;
using System.Linq;
using CodeParser.Extensions;

namespace CodeParser.Structures
{
    internal class ClassHeader
    {
        public List<string> Modifiers { get; set; } = new List<string>();
        public List<string> Inheritence { get; set; } = new List<string>();
        public string Name { get; }
        public string FullName { get; }

        public ClassHeader(string fullNamespaceName, string header)
        {
            var splittedByColon = header.Split(':');
            var splitted = splittedByColon[0].Split(' ');
            int i = 0;
            while (splitted[i] != "class")
            {
                Modifiers.Add(splitted[i].Replace(" ", ""));
                ++i;
            }
            i++;
            Name = splitted[i];
            bool skipDot = Name[Name.Length - 1] == ':';
            if (skipDot)
            {
                Name = Name.Substring(0, Name.Length - 1).Replace(" ", "");
            }
            if (splitted.Length > 1)
            {
                splitted = splittedByColon[1].Split(',');
                foreach (string s in splitted)
                {
                    Inheritence.Add(s.Replace(" ", ""));
                }
            }
            if (Inheritence.Count == 0)
                Inheritence.Add("object");
            FullName = $"{fullNamespaceName}.{Name}";
        }


    }
}
