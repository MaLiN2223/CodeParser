using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeParser.Helpers
{
    public static class RegexHelpers
    {
        public static readonly Regex CommentRegex = new Regex(@"///\s*?<summary>\s*?///\s*?(.*)\s*?///\s*?</summary>", RegexOptions.Singleline);
        public static readonly Regex MethodRegex = new Regex(@"(\w+)?\s?(\w+)?\s?(\w+)\s?(\w+)\((.*?)\)[{]?");
        public static readonly Regex ClassHeaderRegex = new Regex(@"(public|private|internal) class (\w+)\s?:\s?(.*?)[{]?$", RegexOptions.Singleline);
        public static readonly Regex PropertyRegex = new Regex(@"(\w+)?\s?(\w+)\s(\w+)\s(\w+)[{]?$", RegexOptions.Singleline);
        

    }
}
