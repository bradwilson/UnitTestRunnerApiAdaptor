/// <summary>
/// Main MStest test runner class
/// </summary>
namespace UnitTestRunnerApiAdaptor.MSTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.VisualStudio.TestPlatform.MSTestFramework;

    /// <summary>
    /// Entry point to the main MStest test runner.
    /// </summary>
    public static class MSTestRunner
    {
        /// <summary>
        /// Main entry point into the MSTest test runner.
        /// </summary>
        public static void Run()
        {
            var testDeployer = new TestDeployer();
            //// https://social.msdn.microsoft.com/Forums/vstudio/en-US/ccd08ce1-a86b-4965-af68-1db8cd7d6715/how-to-execute-test-methods-programmatically-using-c-may-be-using-reflection?forum=vsunittest

            var dllFolder = @"C:\Users\james\source\repos\jameswiseman76\UnitTestRunnerApiAdaptor\SampleUnderTest.Test.MSTest\bin\Debug\netcoreapp3.1";
            var deploymentPath = testDeployer.DeployItems(dllFolder);

            var dllFile = "SampleUnderTest.Test.MSTest.dll";
            var dllFullPath = Path.Combine(deploymentPath, dllFile);

            var nameOfTestClass = "SampleUnderTest.Test.MSTest.MathServiceTests";

            var testRunner = new UnitTestRunner(deploymentPath, dllFullPath, false, 1000);

            //// var testResults = testRunner.RunDataDrivenTest(nameOfTestMethod, nameOfTestClass, false, properties);
            //// var testResults = testRunner.RunDataDrivenTest(nameOfTestMethod, nameOfTestClass, false);

            //// IRunContext
            ////IFrameworkHandle

            ////var tem = new TestExecutionManager();
            ////tem.RunTests(new[] { dllFullPath },)

            var testResults = new Dictionary<string, UnitTestResult>
            {
                { "AddWithGivenInputsReturnsExpectedResults", testRunner.RunSingleTest("AddWithGivenInputsReturnsExpectedResults", nameOfTestClass, false) },
                { "DoSomethingDoesABunchOfStuff", testRunner.RunSingleTest("DoSomethingDoesABunchOfStuff", nameOfTestClass, false) },
            };

            foreach (var result in testResults)
            {
                if (result.Value.Outcome == UnitTestOutcome.Passed)
                {
                    Console.WriteLine($"{result.Key} Test Case Passed");
                }
                else
                {
                    Console.WriteLine($"{result.Key} Test Case Failed with error ${result.Value.ErrorMessage}");
                }
            }
        }
    }
}
