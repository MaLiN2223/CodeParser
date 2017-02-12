namespace CodeParser.Structures
{
    using System.Collections.Generic;
    using Interfaces;

    public class MethodData : IYamlable
    {
        public void Parse()
        {

        }
        public string Name { get; set; }
        public List<ArgumentData> ArgumentList { get; set; }
        public string ToYaml(int indentation = 0)
        {
            return "method";
        }
    }
}