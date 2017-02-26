using CodeParser.Helpers;

namespace CodeParser.Structures
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Interfaces;
    public class NamespaceData : IYamlable
    {

        public NamespaceData(string headLine, List<string> wholeCode, string comment)
        {
            Name = headLine.Split(' ')[1];
            TextData = wholeCode;
            var match = RegexHelpers.CommentRegex.Match(comment);
            if (match.Success)
            {
                Comment = match.Groups[1].Value;
            }
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
                var match = RegexHelpers.ClassHeaderRegex.Match(line);
                if (match.Success)
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
                    var data = new ClassData(Name, match.Value, lst, comment);
                    ClassList.Add(data);
                    comment = "";
                }
            }
            TextData = null;
        }

        public string ToYaml(int indentation)
        {
            string indent = "";
            for (int i = 0; i < indentation - 1; ++i)
                indent += "\t";
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("namespace");
            builder.AppendLine($"{indent}\tname: {Name}");
            if (Comment != "")
                builder.AppendLine($"{indent}\tcomment:{Comment}");
            builder.AppendLine($"{indent}\tclasses:");
            foreach (var classData in ClassList)
            {
                builder.AppendLine(classData.ToYaml(indentation + 2));
            }
            return builder.ToString();
        }
        public void Parse()
        {
            SelfParse();
            foreach (var classData in ClassList)
            {
                classData.Parse();
            }
        }
        private string Comment { get; }
        private string Name { get; }
        private List<string> TextData { get; set; }
        public List<ClassData> ClassList { get; set; } = new List<ClassData>();
    }
}