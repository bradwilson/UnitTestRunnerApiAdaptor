namespace TestApiRunner
{
    using System;
    using TestApiRunner.NUnit;
    using UnitTestRunnerApiAdaptor.MSTest;
    using UnitTestRunnerApiAdaptor.XUnit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not required for XML Serialization")]
    public class Program
    {
        static void Main(string[] args)
        {
            MSTestRunner.Run();
            //XUnitTestRunner.Run();
            //NUnitTestRunner.Run();
        }
    }
}
