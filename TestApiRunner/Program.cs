namespace TestApiRunner
{
    using System;
    using TestApiRunner.NUnit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not required for XML Serialization")]
    public class Program
    {
        static void Main(string[] args)
        {
            NUnitTestRunner.Run();
            Console.WriteLine("Hello World!");
        }
    }
}
