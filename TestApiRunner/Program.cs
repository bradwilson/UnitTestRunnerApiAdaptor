namespace TestApiRunner
{
    using System;
    using TestApiRunner.NUnit;
    using UnitTestRunnerApiAdaptor;
    using UnitTestRunnerApiAdaptor.MSTest;
    using UnitTestRunnerApiAdaptor.XUnit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not required for XML Serialization")]
    public class Program
    {
        static void Main(string[] args)
        {
            var msTestResults = new TestRunner<MSTestRunner>()
                .WithRunnerSettings(new RunnerSettings("", null))
                .Run();

            Console.WriteLine(msTestResults.Success);

            var nunitResults = new TestRunner<NUnitTestRunner>()
                .WithRunnerSettings(new RunnerSettings("", null))
                .Run();

            Console.WriteLine(nunitResults.Success);

            var xunitResults = new TestRunner<XUnitTestRunner>()
                .WithRunnerSettings(new RunnerSettings("", null))
                .Run();

            Console.WriteLine(xunitResults.Success);

        }
    }
}
