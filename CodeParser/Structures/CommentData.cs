using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeParser.Interfaces;

namespace CodeParser.Structures
{
    public class CommentData : IYamlable
    {
        public CommentData(string comment)
        {

        }

        public string ToYaml(int indentation = 0)
        {
            return "";
        }
    }
}
