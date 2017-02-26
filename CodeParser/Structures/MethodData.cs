
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CodeParser.Extensions;
using CodeParser.Helpers;
using CodeParser.Interfaces;

namespace CodeParser.Structures
{
    public class MethodData : IYamlable
    {
        public MethodData(string headLine, string comment, string className)
        {
            var match = RegexHelpers.CommentRegex.Match(comment);
            if (match.Success)
            {
                Comment = match.Groups[1].Value;
            }
            header = new MethodHeader(headLine, className);

        } 
        private MethodHeader header { get; }
        private string Comment { get; set; }
        public string ToYaml(int indentation = 0)
        {
            string indent = YamlHelpers.GenerateIndent(indentation);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"{indent}- {header.FullName}:");
            builder.AppendLine($"{indent}{YamlHelpers.PropertyIndentSpace}name: {header.Name}");
            builder.AppendLine($"{indent}{YamlHelpers.PropertyIndentSpace}type: {header.Type}");
            builder.AppendLine($"{indent}{YamlHelpers.PropertyIndentSpace}modifiers: [{header.Modifiers.AggregateToString(", ")}]");
            builder.AppendLine($"{indent}{YamlHelpers.PropertyIndentSpace}arguments: [{header.Arguments.AggregateToString(", ")}]");
            builder.AppendLine($"{indent}{YamlHelpers.PropertyIndentSpace}return type: {header.ReturnType}");
            if (!string.IsNullOrEmpty(Comment))
                builder.AppendLine($"{indent}{YamlHelpers.PropertyIndentSpace}comment: {Comment}");
            return builder.ToString();
        }
    }
}