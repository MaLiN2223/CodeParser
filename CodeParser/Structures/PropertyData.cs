namespace CodeParser.Structures
{
    using System;
    using Interfaces;

    public class PropertyData : IYamlable
    {
        public PropertyData(string header, string comment)
        {

        } 
        public string Name { get; set; }
        public Type Type { get; set; }
        public string Description { get; set; }
        public string ToYaml(int indentation = 0)
        {
            return "";
            //throw new NotImplementedException();
        }
    }
}