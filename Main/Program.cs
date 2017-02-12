namespace Main
{
    using System;
    using CodeParser;

    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser();
            var data = parser.ParseFile("../../../TestFiles/Class1.cs", "tmp.yaml");
            Console.WriteLine(data);
            Console.ReadKey();
        }
    }
}
