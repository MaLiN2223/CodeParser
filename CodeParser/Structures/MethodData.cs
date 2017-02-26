
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CodeParser.Helpers;
using CodeParser.Interfaces;
using CodeParser.Helpers;
namespace CodeParser.Structures
{
    public class MethodData : IYamlable
    {
        public MethodData(string headLine, List<string> wholeCode, string comment)
        {
            var match = RegexHelpers.CommentRegex.Match(comment);
            if (match.Success)
            {
                Comment = match.Groups[1].Value;
            }
        }
        public void Parse()
        {
            //TODO: parse
        }
        public string FullName { get; set; }
        private string Access { get; set; }
        private string Comment { get; set; }
        public string Name { get; set; }
        public List<ArgumentData> ArgumentList { get; set; }
        public string ToYaml(int indentation = 0)
        {
            string indent = YamlHelpers.GenerateIndent(indentation);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"{indent}- {FullName}:");
            builder.AppendLine($"{indent}{YamlHelpers.PropertyIndentSpace}name: {Name}");
            builder.AppendLine($"{indent}{YamlHelpers.PropertyIndentSpace}access: {Access}");
            if (!string.IsNullOrEmpty(Comment))
                builder.AppendLine($"{indent}{YamlHelpers.PropertyIndentSpace}comment: {Comment}");
            return builder.ToString();
        }
    }
}