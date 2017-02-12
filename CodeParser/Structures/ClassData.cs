namespace CodeParser.Structures
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using Interfaces;

    public class ClassData : IYamlable
    {
        private static readonly Regex classRegex = new Regex(@"(public|private|internal)class(\w+)(:(.*))?");
        private static readonly Regex commentRegex = new Regex(@"///\s*?<summary>\s*?///\s*?(.*)\s*?///\s*?</summary>", RegexOptions.Singleline);

        public ClassData(string namespaceName, string headLine, List<string> wholeCode, string comment)
        {
            headLine = headLine.Replace("\t", "").Replace(" ", "");
            TextData = wholeCode;
            var match = commentRegex.Match(comment);
            if (match.Success)
            {
                Comment = match.Groups[1].Value;
            }
            match = classRegex.Match(headLine);

            if (match.Success)
            {
                Access = match.Groups[1].Value;
                Name = match.Groups[2].Value;
                var inheritance = match.Groups[4].Value;
                Inheritance = inheritance != "" ? inheritance : "object";
                FullName = namespaceName + "." + Name;
            }
        }
        private void SelfParse()
        {


        }
        public void Parse()
        {
            SelfParse();
        }
        public string ToYaml(int indentation = 0)
        {
            string indent = "";
            for (int i = 0; i < indentation - 1; ++i)
                indent += "\t";
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"{indent}\tfull name: {FullName}");
            builder.AppendLine($"{indent}\tname: {Name}");
            builder.AppendLine($"{indent}\taccess modifier: {Access}");
            builder.AppendLine($"{indent}\tinheritance: {Inheritance}");
            if (!string.IsNullOrEmpty(Comment))
                builder.AppendLine($"{indent}\tcomment: {Comment}");
            builder.AppendLine($"{indent}\tmethods:");
            foreach (var methodData in MethodList)
            {
                builder.Append(methodData.ToYaml(indentation + 2));
            }
            return builder.ToString();
        }
        public string FullName { get; set; }
        private string Access { get; set; }
        private string Comment { get; set; }
        private string Inheritance { get; set; }
        private string Name { get; set; }
        private bool IsPartial { get; set; } = false;
        public List<MethodData> MethodList { get; set; } = new List<MethodData>();
        private List<string> TextData { get; set; }
    }
}