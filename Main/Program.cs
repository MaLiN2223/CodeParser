namespace Main
{
    using System;
    using CodeParser;

    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser();
            parser.ParseFile("../../../TestFiles/Class1.cs", "../../../TestFiles/tmp.yaml");
            Console.WriteLine("done"); 
        }
    }
}
