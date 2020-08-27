namespace UnitTestRunnerApiAdaptor
{
    /// <summary>
    /// Contains the runner settings.
    /// </summary>
    public class RunnerSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunnerSettings"/> class.
        /// </summary>
        /// <param name="testAssemblyFullPath">Sets the full path of the test assembly whose test we are wating to execute</param>
        public RunnerSettings(string testAssemblyFullPath)
        {
            this.TestAssemblyFullPath = testAssemblyFullPath;
        }

        /// <summary>
        /// Gets the property denoting the full path of the test assembly whose test we are wating to execute.
        /// </summary>
        public string TestAssemblyFullPath { get; private set; }
    }
}
