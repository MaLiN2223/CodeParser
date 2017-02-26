using System.Collections.Generic;
using System.Linq;

namespace CodeParser.Extensions
{
    public static class ListStringExtensions
    {
        /// <summary>
        /// Aggregates list ignoring empty strings
        /// </summary>
        /// <param name="data"></param>
        /// <param name="space"></param>
        /// <returns></returns>
        public static string AggregateToString(this List<string> data, string space = " ")
        {
            if (data.Count() == 1)
                return data[0];
            return data.Where(x => !string.IsNullOrEmpty(x)).Aggregate((x, y) => x + space + y);
        }
    }
}
