using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CodeParser.Helpers;
using CodeParser.Interfaces;

namespace CodeParser.Structures
{

    public class ClassData : IYamlable
    {

        public ClassData(string namespaceName, string headLine, List<string> wholeCode, string comment)
        {
            TextData = wholeCode;
            var match = RegexHelpers.CommentRegex.Match(comment);
            if (match.Success)
            {
                Comment = match.Groups[1].Value;
            }
            header = new ClassHeader(namespaceName, headLine);
        }

        private void SelfParse()
        {
            string comment = "";
            for (int index = 0; index < TextData.Count; index++)
            {
                var line = TextData[index];
                if (line.TrimStart().StartsWith("///"))
                {
                    comment += line;
                    continue;
                }
                var match = RegexHelpers.MethodRegex.Match(line);
                if (match.Success) // method found
                {
                    var lst = new List<string>();
                    int brackets = line.Count(x => x == '{') - line.Count(x => x == '}');
                    int j = index + 1;
                    do
                    {
                        line = TextData[j];
                        brackets += line.Count(x => x == '{');
                        brackets -= line.Count(x => x == '}');
                        lst.Add(TextData[j]);
                        j++;

                    } while (j < TextData.Count && brackets > 0);
                    index = j;
                    var data = new MethodData(match.Value, lst, comment);
                    MethodList.Add(data);
                    comment = "";
                }
            }
            TextData = null;

        }
        public void Parse()
        {
            SelfParse();
        }
        public string ToYaml(int indentation = 0)
        {
            string indent = "";
            for (int i = 0; i < indentation; ++i)
                indent += "\t";
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"{indent}-{header.FullName}");
            builder.AppendLine($"{indent}\tname: {header.Name}");
            builder.AppendLine($"{indent}\tmodifiers: {header.Modifiers}");
            builder.AppendLine($"{indent}\tinheritance: {header.Inheritence}");
            if (!string.IsNullOrEmpty(Comment))
                builder.AppendLine($"{indent}\tcomment: {Comment}");
            builder.AppendLine($"{indent}\tmethods:");
            foreach (var methodData in MethodList)
            {
                builder.Append(methodData.ToYaml(indentation + 1));
            }
            return builder.ToString();
        }
        private string Comment { get; set; } 
        public List<MethodData> MethodList { get; set; } = new List<MethodData>();
        private List<string> TextData { get; set; }
        private readonly ClassHeader header;
    }
}