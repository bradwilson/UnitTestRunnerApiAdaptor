/// <summary>
/// Main XUnit test runner class
/// </summary>
namespace UnitTestRunnerApiAdaptor.XUnit
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using Xunit;
    using Xunit.Runners;

    /// <summary>
    /// Entry point to the main XUnit test runner.
    /// </summary>
    public static class XUnitTestRunner
    {
        // We use consoleLock because messages can arrive in parallel, so we want to make sure we get
        // consistent console output.
        private static readonly object ConsoleLock = new object();

        // Use an event to know when we're done
        private static readonly ManualResetEvent Finished = new ManualResetEvent(false);

        /// <summary>
        /// Main entry point into the XUnit test runner.
        /// </summary>
        public static void Run()
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

            var dllFolder = @"C:\Dev\repro\xunit-2140\SampleUnderTest.Tests.XUnit\bin\Debug\netcoreapp3.1";
            var dllFile = "SampleUnderTest.Tests.XUnit.dll";
            var dllFullPath = Path.Combine(dllFolder, dllFile);

            var assembly = Assembly.LoadFrom(dllFullPath);

            using (AssemblyHelper.SubscribeResolveForAssembly(assembly.Location))
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
        }

        private static void OnDiscoveryComplete(DiscoveryCompleteInfo info)
        {
            lock (ConsoleLock)
                Console.WriteLine($"Running {info.TestCasesToRun} of {info.TestCasesDiscovered} tests...");
        }

        private static void OnExecutionComplete(ExecutionCompleteInfo info)
        {
            lock (ConsoleLock)
                Console.WriteLine($"Finished: {info.TotalTests} tests in {Math.Round(info.ExecutionTime, 3)}s ({info.TestsFailed} failed, {info.TestsSkipped} skipped)");

            Finished.Set();
        }

        private static void OnTestPassed(TestPassedInfo info)
        {
            lock (ConsoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[PASSED] {info.TestDisplayName}: {Math.Round(info.ExecutionTime, 3)}s");
                Console.ResetColor();
            }
        }

        private static void OnTestFailed(TestFailedInfo info)
        {
            lock (ConsoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine($"[FAIL] {info.TestDisplayName}: {info.ExceptionMessage}");
                if (info.ExceptionStackTrace != null)
                    Console.WriteLine(info.ExceptionStackTrace);

                Console.ResetColor();
            }
        }

        private static void OnTestSkipped(TestSkippedInfo info)
        {
            lock (ConsoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[SKIP] {info.TestDisplayName}: {info.SkipReason}");
                Console.ResetColor();
            }
        }
    }
}
