/// <summary>
/// Main MStest test runner class
/// </summary>
namespace UnitTestRunnerApiAdaptor.Runners.MSTest
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestPlatform.MSTestFramework;

    /// <summary>
    /// Entry point to the main MStest test runner.
    /// </summary>
    public class MSTestRunner : ITestRunner<MSTestRunner>
    {
        private TestRunnerSettings runnerSettings;

        /// <summary>   Include runner settings for the test run. </summary>
        /// <param name="runnerSettings">   The runner settings. </param>
        /// <returns>   The current instance of this TestRunner. </returns>
        public ITestRunner<MSTestRunner> WithRunnerSettings(TestRunnerSettings runnerSettings)
        {
            this.runnerSettings = runnerSettings;
            return this;
        }

        /// <summary>   Runs the tests. </summary>
        /// <returns>   The Results of the test run. </returns>
        public TestRunnerResults Run()
        {
            var testDeployer = new TestDeployer();

            var deploymentPath = testDeployer.DeployItems(Path.GetDirectoryName(this.runnerSettings.TestAssemblyFullPath));

            var testRunner = new UnitTestRunner(deploymentPath, this.runnerSettings.TestAssemblyFullPath, false, 1000);

            var testResults = this.runnerSettings.TestsToRun.ToDictionary(
                x => x,
                x => testRunner.RunSingleTest(x.TestName, x.TestClassName, false));

            foreach (var result in testResults)
            {
                if (result.Value.Outcome == UnitTestOutcome.Passed)
                {
                    Console.WriteLine($"MSTest: {result.Key.TestClassName}.{result.Key.TestName} Test Case Passed");
                }
                else
                {
                    Console.WriteLine($"MSTest: {result.Key} Test Case Failed with error ${result.Value.ErrorMessage}");
                }
            }

            return new TestRunnerResults(true, TestRunnerType.MSTest);
        }
    }
}
