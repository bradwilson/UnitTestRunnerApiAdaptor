/// <summary>
/// Main XUnit test runner class
/// </summary>
namespace UnitTestRunnerApiAdaptor.Runners.XUnit
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    using Xunit.Runners;

    /// <summary>
    /// Entry point to the main XUnit test runner.
    /// </summary>
    public class XUnitTestRunner : ITestRunner<XUnitTestRunner>
    {
        // We use consoleLock because messages can arrive in parallel, so we want to make sure we get
        // consistent console output.
        private static readonly object LoggerLock = new object();

        // Use an event to know when we're done
        private static readonly ManualResetEvent Finished = new ManualResetEvent(false);

        private TestRunnerSettings runnerSettings;

        /// <summary>   Include runner settings for the test run. </summary>
        /// <param name="runnerSettings">   The runner settings. </param>
        /// <returns>   The current instance of this TestRunner. </returns>
        public ITestRunner<XUnitTestRunner> WithRunnerSettings(TestRunnerSettings runnerSettings)
        {
            this.runnerSettings = runnerSettings;
            return this;
        }

        /// <summary>   Runs the tests. </summary>
        /// <returns>   The Results of the test run. </returns>
        public TestRunnerResults Run()
        {
            var assembly = Assembly.LoadFrom(this.runnerSettings.TestAssemblyFullName);

            using var runner = AssemblyRunner.WithoutAppDomain(assembly.Location);

            runner.OnDiscoveryComplete = OnDiscoveryComplete;
            runner.OnExecutionComplete = OnExecutionComplete;
            runner.OnTestPassed = OnTestPassed;
            runner.OnTestFailed = OnTestFailed;
            runner.OnTestSkipped = OnTestSkipped;
            runner.TestCaseFilter =
                f => this.runnerSettings.TestsToRun.Any(
                    t => t.TestClassName == f.TestMethod.TestClass.Class.Name &&
                            t.TestName == f.TestMethod.Method.Name);

            Console.WriteLine("Discovering...");

            runner.Start(parallel: false);

            Finished.WaitOne();
            Finished.Dispose();

            return new TestRunnerResults(true, TestRunnerType.XUnit);
        }

        private static void OnDiscoveryComplete(DiscoveryCompleteInfo info)
        {
            lock (LoggerLock)
            {
                Console.WriteLine($"Running {info.TestCasesToRun} of {info.TestCasesDiscovered} tests...");
            }
        }

        private static void OnExecutionComplete(ExecutionCompleteInfo info)
        {
            lock (LoggerLock)
            {
                Console.WriteLine($"Finished: {info.TotalTests} tests in {Math.Round(info.ExecutionTime, 3)}s ({info.TestsFailed} failed, {info.TestsSkipped} skipped)");
                Finished.Set();
            }
        }

        private static void OnTestPassed(TestPassedInfo info)
        {
            lock (LoggerLock)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[PASSED] {info.TestDisplayName}: {Math.Round(info.ExecutionTime, 3)}s");
                Console.ResetColor();
            }
        }

        private static void OnTestFailed(TestFailedInfo info)
        {
            lock (LoggerLock)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine($"[FAIL] {info.TestDisplayName}: {info.ExceptionMessage}");
                if (info.ExceptionStackTrace != null)
                {
                    Console.WriteLine(info.ExceptionStackTrace);
                }

                Console.ResetColor();
            }
        }

        private static void OnTestSkipped(TestSkippedInfo info)
        {
            lock (LoggerLock)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[SKIP] {info.TestDisplayName}: {info.SkipReason}");
                Console.ResetColor();
            }
        }
    }
}
