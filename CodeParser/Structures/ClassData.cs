using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CodeParser.Extensions;
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
                    int brackets = line.Count(x => x == '{') - line.Count(x => x == '}');
                    int j = index + 1;
                    do
                    {
                        line = TextData[j];
                        brackets += line.Count(x => x == '{');
                        brackets -= line.Count(x => x == '}');
                        j++;

                    } while (j < TextData.Count && brackets > 0);
                    index = j;
                    var data = new MethodData(match.Value, comment, header.FullName);
                    StructuresList.Add(data);
                    comment = "";
                }
                match = RegexHelpers.PropertyRegex.Match(line);
                if (match.Success)
                {
                    int brackets = line.Count(x => x == '{') - line.Count(x => x == '}'); int j = index + 1;
                    do
                    {
                        line = TextData[j];
                        brackets += line.Count(x => x == '{');
                        brackets -= line.Count(x => x == '}');
                        j++;

                    } while (j < TextData.Count && brackets > 0);
                    index = j;
                    var data = new PropertyData(match.Value, comment);
                    StructuresList.Add(data);
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
            string indent = YamlHelpers.GenerateIndent(indentation);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"{indent}- {header.FullName}:");
            builder.AppendLine($"{indent}{YamlHelpers.PropertyIndentSpace}name: {header.Name}");
            builder.AppendLine($"{indent}{YamlHelpers.PropertyIndentSpace}modifiers: [{header.Modifiers.AggregateToString(",")}]");
            builder.AppendLine($"{indent}{YamlHelpers.PropertyIndentSpace}inheritance: [{header.Inheritence.AggregateToString(",")}]");
            if (!string.IsNullOrEmpty(Comment))
                builder.AppendLine($"{indent}{YamlHelpers.PropertyIndentSpace}comment: {Comment}");
            builder.AppendLine($"{indent}{YamlHelpers.PropertyIndentSpace}methods:");
            foreach (var structureData in StructuresList)
            {
                builder.Append(structureData.ToYaml(indentation + 1 + YamlHelpers.ListIndentSize));
            }
            return builder.ToString();
        }
        private string Comment { get; set; }
        public List<IYamlable> StructuresList { get; set; } = new List<IYamlable>();
        private List<string> TextData { get; set; }
        private readonly ClassHeader header;
    }
}