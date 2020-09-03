namespace UnitTestRunnerApiAdaptor
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Immutable;
    using System.IO;

    /// <summary>
    /// Contains the runner settings.
    /// </summary>
    public class TestRunnerSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunnerSettings"/> class.
        /// </summary>
        /// <param name="testAssemblyFullPath">Sets the full path of the test assembly whose test we are wating to execute.</param>
        /// <param name="fullyQualifiedNamesOfTestsToRun">
        /// A list of test names to run. Fully qualified with Namespace.TestClass.TestMethod.
        /// </param>
        public TestRunnerSettings(
            string testAssemblyFullPath,
            ImmutableList<TestRunItem> fullyQualifiedNamesOfTestsToRun)
        {
            this.TestAssemblyFullName = testAssemblyFullPath;
            this.TestsToRun = fullyQualifiedNamesOfTestsToRun;
            this.TestAssemblyFullPath = Path.GetDirectoryName(this.TestAssemblyFullName);
            this.TestAssemblyFileName = Path.GetFileName(this.TestAssemblyFullName);
        }

        /// <summary>
        /// Gets the value denoting the full path and file name of the test assembly whose tests are to be executed.
        /// </summary>
        public string TestAssemblyFullName { get; private set; }

        /// <summary>
        /// Gets the value denoting the full path of the test assembly whose tests are to be executed.
        /// </summary>
        public string TestAssemblyFullPath { get; private set; }

        /// <summary>
        /// Gets the value denoting the file name of the test assembly whose tests are to be executed.
        /// </summary>
        public string TestAssemblyFileName { get; private set; }

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
