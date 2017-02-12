using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParser
{
    using Structures;

    public class Parser
    {
        public void ParseDirectory(string inputPath, string outputPath)
        {

        }

        public string ParseFile(string inputPath, string outputPath)
        {
            var file = System.IO.File.ReadAllLines(inputPath);
            var output = FileData.FromFile(file);
            output.Parse();
            return output.Namespaces.Select(x => x.ToYaml(0)).Aggregate("", (x, y) => x + "\n" + y);

        }
    }
}
