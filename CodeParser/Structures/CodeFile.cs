using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParser.Structures
{
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public class FileData
    {
        public void Parse()
        {
            foreach (var namespaceData in Namespaces)
            {
                namespaceData.Parse();
            }
        }
        private FileData(List<NamespaceData> namespaces)
        {
            Namespaces = namespaces;
        }
        public List<NamespaceData> Namespaces { get; set; }
        private static readonly Regex namespaceRegex = new Regex(@"^namespace (.*)[{]?");
        public static FileData FromFile(string[] fileText)
        {
            Stack<NamespaceData> namespaces = new Stack<NamespaceData>();
            string comment = "";
            for (int index = 0; index < fileText.Length; index++)
            {
                var line = fileText[index];
                if (line.TrimStart().StartsWith("///"))
                {
                    comment += line;
                }
                var match = namespaceRegex.Match(line);
                if (match.Success)
                {
                    var lst = new List<string>();
                    int brackets = line.Count(x => x == '{') - line.Count(x => x == '}');
                    int j = index + 1;
                    do
                    {
                        line = fileText[j];
                        brackets += line.Count(x => x == '{');
                        brackets -= line.Count(x => x == '}');
                        lst.Add(fileText[j]);
                        j++;

                    } while (j < fileText.Length && brackets > 0);
                    index = j;
                    var data = new NamespaceData(match.Value, lst, comment);
                    namespaces.Push(data);
                    comment = ""; 
                }
            }
            var list = new List<NamespaceData>(namespaces);
            return new FileData(list);
        }
    }
}
