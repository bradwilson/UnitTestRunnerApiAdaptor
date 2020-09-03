namespace TestApiRunner
{
    using System;
    using System.Collections.Immutable;
    using TestApiRunner.NUnit;
    using UnitTestRunnerApiAdaptor;
    using UnitTestRunnerApiAdaptor.MSTest;
    using UnitTestRunnerApiAdaptor.XUnit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not required for XML Serialization")]
    public class Program
    {
        static void Main(string[] args)
        {
            var msTestDllFullName = @"C:\Users\james\source\repos\jameswiseman76\UnitTestRunnerApiAdaptor\SampleUnderTest.Test.MSTest\bin\Debug\netcoreapp3.1\SampleUnderTest.Test.MSTest.dll";

            var msTestsToRun = ImmutableList.Create(
                new TestRunItem("SampleUnderTest.Test.MSTest.MathServiceTests", "AddWithGivenInputsReturnsExpectedResults"),
                new TestRunItem("SampleUnderTest.Test.MSTest.MathServiceTests", "DoSomethingDoesABunchOfStuff"));

            var msTestResults = new TestRunner<MSTestRunner>()
                .WithRunnerSettings(new RunnerSettings(msTestDllFullName, msTestsToRun))
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
