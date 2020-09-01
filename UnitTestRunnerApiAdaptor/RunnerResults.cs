namespace UnitTestRunnerApiAdaptor
{
    /// <summary>   The results for the test execution. </summary>
    public class RunnerResults
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunnerResults"/> class.   Constructor. </summary>
        /// <param name="success">
        /// A value indicating whether the test run was a success.
        /// </param>
        /// <param name="testRunnerType">
        /// Gets the value denoting the type of the test runner to run.
        /// </param>
        public RunnerResults(bool success, TestRunnerType testRunnerType)
        {
            this.Success = success;
            this.TestRunnerType = testRunnerType;
        }

        /// <summary>   Gets a value indicating whether the test run was a success. </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Gets the value denoting the type of the test runner to run.
        /// </summary>
        public TestRunnerType TestRunnerType { get; private set; }
    }
}
