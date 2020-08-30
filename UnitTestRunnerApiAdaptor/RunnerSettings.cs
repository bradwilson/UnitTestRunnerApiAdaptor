namespace UnitTestRunnerApiAdaptor
{
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
        public RunnerSettings(string testAssemblyFullPath)
        {
            this.TestAssemblyFullPath = testAssemblyFullPath;
        }

        /// <summary>
        /// Gets the property denoting the full path of the test assembly whose test we are wating to execute.
        /// </summary>
        public string TestAssemblyFullPath { get; private set; }

        /// <summary>
        /// Gets the property denoting the list of tests to run. If this is omitted, all tests are run.
        /// </summary>
        public ImmutableList<string> TestsToRun { get; private set; }

        /// <summary>
        /// Gets a value indicating whether all tests in the given assembly are to be run.
        /// </summary>
        public bool RunAllTests { get => this.TestsToRun.Count == 0; }
 
    }
}
