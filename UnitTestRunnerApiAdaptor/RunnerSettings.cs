namespace UnitTestRunnerApiAdaptor
{
    using System;
    using System.Collections.Immutable;

    /// <summary>
    /// Contains the runner settings.
    /// </summary>
    public class RunnerSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunnerSettings"/> class.
        /// </summary>
        /// <param name="testAssemblyFullPath">Sets the full path of the test assembly whose test we are wating to execute.</param>
        /// <param name="fullyQualifiedNamesOfTestsToRun">
        /// A list of test names to run. Fully qualified with Namespace.TestClass.TestMethod.
        /// </param>
        public RunnerSettings(
            string testAssemblyFullPath,
            ImmutableList<TestRunItem> fullyQualifiedNamesOfTestsToRun)
        {
            this.TestAssemblyFullPath = testAssemblyFullPath;
            this.TestsToRun = fullyQualifiedNamesOfTestsToRun;
        }

        /// <summary>
        /// Gets the value denoting the full path of the test assembly whose test we are wating to execute.
        /// </summary>
        public string TestAssemblyFullPath { get; private set; }

        /// <summary>
        /// Gets the value denoting the list of tests to run. If this is omitted, all tests are run.
        /// </summary>
        public ImmutableList<TestRunItem> TestsToRun { get; private set; }

        /// <summary>
        /// Gets the value denoting the type of the test runner to run.
        /// </summary>
        public TestRunnerType TestRunnerType { get; private set; }
    }
}
