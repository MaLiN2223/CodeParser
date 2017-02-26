using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParser.Interfaces
{
    public interface IYamlable
    {
        string ToYaml(int indentation = 0);
    }
}
