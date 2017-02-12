namespace CodeParser.Structures
{
    using System;
    using Interfaces;

    public class ArgumentData : IYamlable
    {
        public void Parse()
        {

        }
        public string Name { get; set; }
        public Type Type { get; set; }
        public string Description { get; set; }
        public string ToYaml(int indentation = 0)
        {
            throw new NotImplementedException();
        }
    }
}