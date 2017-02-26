
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CodeParser.Helpers;
using CodeParser.Interfaces;

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
            string indent = "";
            for (int i = 0; i < indentation - 1; ++i)
                indent += "\t";
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"{indent}\tfull name: {FullName}");
            builder.AppendLine($"{indent}\tname: {Name}");
            builder.AppendLine($"{indent}\taccess: {Access}");
            if (!string.IsNullOrEmpty(Comment))
                builder.AppendLine($"{indent}\tcomment: {Comment}"); 
            return builder.ToString();
        }
    }
}