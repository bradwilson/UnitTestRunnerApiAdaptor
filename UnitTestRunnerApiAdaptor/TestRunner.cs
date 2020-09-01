namespace UnitTestRunnerApiAdaptor
{
    /// <summary>   Entry point to the test runner API adaptor suite. </summary>
    /// <typeparam name="T">    The type of test suite to run. </typeparam>
    public class TestRunner<T> : ITestRunner<T>
        where T : ITestRunner<T>, new()
    {
        private readonly T testRunner;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunner{T}"/> class.
        /// </summary>
        public TestRunner()
        {
            this.testRunner = new T();
        }

        /// <summary>   Include runner settings for the test run. </summary>
        /// <param name="runnerSettings">   The runner settings. </param>
        /// <returns>   The current instance of this TestRunner. </returns>
        public ITestRunner<T> WithRunnerSettings(RunnerSettings runnerSettings)
        {
            return this.testRunner.WithRunnerSettings(runnerSettings);
        }

        /// <summary>   Runs the tests. </summary>
        /// <returns>   The Results of the test run. </returns>
        public RunnerResults Run()
        {
            return this.testRunner.Run();
        }
    }
}
