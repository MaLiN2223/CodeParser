using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Namespace summary
/// </summary>
namespace SomeSweet.Namespace
{
    /// <summary>
    /// Test class
    /// </summary>
    public class Class : IInterface, SomeObject
    {
        /// <summary>
        /// Foo documentation
        /// </summary>
        /// <param name="a">First parameter</param>
        /// <param name="b">Second parameter</param>
        /// <returns></returns>
        public int Foo(double a, double b)
        {
            return 0;
        }
    }
	
	private class Class2 : IInterface2, SomeObject2
    {
        /// <summary>
        /// Foo documentation
        /// </summary>
        /// <param name="a">First parameter</param>
        /// <param name="b">Second parameter</param>
        /// <returns></returns>
        public int Foo(double a, double b)
        {
            return 0;
        }
    }
}
