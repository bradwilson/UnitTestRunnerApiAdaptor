namespace TestApiRunner
{
    using System;
    using System.Collections.Immutable;
    using UnitTestRunnerApiAdaptor;
    using UnitTestRunnerApiAdaptor.Runners.MSTest;
    using UnitTestRunnerApiAdaptor.Runners.NUnit;
    using UnitTestRunnerApiAdaptor.Runners.XUnit;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not required")]
    public class Program
    {
        static void Main()
        {
            var msTestResults = RunMsTests();
            Console.WriteLine(msTestResults.Success);

            var nunitResults = RunNUnitTests();
            Console.WriteLine(nunitResults.Success);

            var xunitResults = RunXUnitTests();
            Console.WriteLine(xunitResults.Success);
        }

        private static TestRunnerResults RunMsTests()
        {
            var msTestDllFullName = @"C:\Users\james\source\repos\jameswiseman76\UnitTestRunnerApiAdaptor\SampleUnderTest.Test.MSTest\bin\Debug\netcoreapp3.1\SampleUnderTest.Test.MSTest.dll";

            var msTestsToRun = ImmutableList.Create(
                new TestRunItem("SampleUnderTest.Test.MSTest.MathServiceTests", "AddWithGivenInputsReturnsExpectedResults"),
                new TestRunItem("SampleUnderTest.Test.MSTest.MathServiceTests", "DoSomethingDoesABunchOfStuff"));

            var msTestResults = new TestRunner<MSTestRunner>()
                .WithRunnerSettings(new TestRunnerSettings(msTestDllFullName, msTestsToRun))
                .Run();

            return msTestResults;
        }

        private static TestRunnerResults RunNUnitTests()
        {
            var nunitDllFullName = @"C:\Users\james\source\repos\jameswiseman76\UnitTestRunnerApiAdaptor\SampleUnderTest.NUnit\bin\Debug\netcoreapp3.1\SampleUnderTest.Tests.NUnit.dll";

            var nunitTestsToRun = ImmutableList.Create(
                new TestRunItem("SampleUnderTest.Tests.NUnit.PrimeServiceTests", "Test1"),
                new TestRunItem("SampleUnderTest.Tests.NUnit.MathServiceTests", "AddWithGivenInputsReturnsExpectedResults"),
                new TestRunItem("SampleUnderTest.Tests.NUnit.MathServiceTests", "DoSomethingDoesABunchOfStuff"));

            var nunitResults = new TestRunner<NUnitTestRunner>()
                .WithRunnerSettings(new TestRunnerSettings(nunitDllFullName, nunitTestsToRun))
                .Run();

            return nunitResults;
        }

        private static TestRunnerResults RunXUnitTests()
        {
            var xunitDllFullName = @"C:\Users\james\source\repos\jameswiseman76\UnitTestRunnerApiAdaptor\SampleUnderTest.Tests.XUnit\bin\Debug\netcoreapp3.1\SampleUnderTest.Tests.XUnit.dll";

            var xunitTestsToRun = ImmutableList.Create(
                new TestRunItem("SampleUnderTest.Tests.XUnit.UnitTest1", "OneIsNotPrime"),
                new TestRunItem("SampleUnderTest.Tests.XUnit.UnitTest1", "TwoIsPrime"));

            var xunitResults = new TestRunner<XUnitTestRunner>()
                .WithRunnerSettings(new TestRunnerSettings(xunitDllFullName, xunitTestsToRun))
                .Run();

            return xunitResults;
        }
    }
}
