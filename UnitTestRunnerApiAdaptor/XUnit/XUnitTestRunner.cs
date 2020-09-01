﻿/// <summary>
/// Main XUnit test runner class
/// </summary>
namespace UnitTestRunnerApiAdaptor.XUnit
{
    using System;
    using System.IO;
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

        private RunnerSettings runnerSettings;

        /// <summary>   Include runner settings for the test run. </summary>
        /// <param name="runnerSettings">   The runner settings. </param>
        /// <returns>   The current instance of this TestRunner. </returns>
        public ITestRunner<XUnitTestRunner> WithRunnerSettings(RunnerSettings runnerSettings)
        {
            this.runnerSettings = runnerSettings;
            return this;
        }

        /// <summary>   Runs the tests. </summary>
        /// <returns>   The Results of the test run. </returns>
        public RunnerResults Run()
        {
            //// https://github.com/xunit/xunit/issues/542
            //// https://github.com/xunit/samples.xunit/blob/main/TestRunner/Program.cs

            //// http://stackoverflow.com/questions/22616239/getting-the-path-of-a-nuget-package-programmatically
            ////
            //// This returns the URI of the assembly containing the 'MyClass' type, 
            //// e.g.: file:///C:\temp\myassembly.dll
            //// var codeBase = typeof(MyClass).Assembly.CodeBase;

            //// This return the file path without any of the URI qualifiers
            //// var location = new Uri(codeBase).LocalPath;
            //// var assembly = Assembly.Load(new AssemblyName("SampleUnderTest.Tests.XUnit"));

            var dllFolder = @"C:\Users\james\source\repos\jameswiseman76\UnitTestRunnerApiAdaptor\SampleUnderTest.Tests.XUnit\bin\Debug\netcoreapp3.1";
            var dllFile = "SampleUnderTest.Tests.XUnit.dll";
            var dllFullPath = Path.Combine(dllFolder, dllFile);

            var assembly = Assembly.LoadFrom(dllFullPath);

            using (var runner = AssemblyRunner.WithoutAppDomain(assembly.Location))
            {
                runner.OnDiscoveryComplete = OnDiscoveryComplete;
                runner.OnExecutionComplete = OnExecutionComplete;
                runner.OnTestPassed = OnTestPassed;
                runner.OnTestFailed = OnTestFailed;
                runner.OnTestSkipped = OnTestSkipped;

                Console.WriteLine("Discovering...");

                ////https://github.com/xunit/xunit/issues/1202
                runner.Start(parallel: false);

                Finished.WaitOne();
                Finished.Dispose();
            }

            return new RunnerResults(true, TestRunnerType.NUnit);
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
            }

            Finished.Set();
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
