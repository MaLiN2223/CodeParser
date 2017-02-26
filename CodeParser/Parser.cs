using System;

using System.IO;
using System.Linq;

namespace CodeParser
{
    using Structures;

    public class Parser
    {
        public void ParseDirectory(string inputPath, string outputPath)
        {
            throw new NotImplementedException();
        }

        public void ParseFile(string inputPath, string outputPath)
        {
            var file = System.IO.File.ReadAllLines(inputPath);
            var output = FileData.FromFile(file);
            output.Parse();
            var toFile = output.Namespaces.Select(x => x.ToYaml()).Aggregate("", (x, y) => x + "\n" + y);
            File.WriteAllText(outputPath, toFile);

        }
    }
}
