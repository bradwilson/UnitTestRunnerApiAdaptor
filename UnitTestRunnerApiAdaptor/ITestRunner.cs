namespace UnitTestRunnerApiAdaptor
{
    /// <summary>   Interface for test runner types. </summary>
    /// <typeparam name="T">    The type of test suite to run. </typeparam>
    public interface ITestRunner<T>
        where T : ITestRunner<T>, new()
    {
        /// <summary>   Include runner settings for the test run. </summary>
        /// <param name="runnerSettings">   The runner settings. </param>
        /// <returns>   The current instance of this TestRunner. </returns>
        ITestRunner<T> WithRunnerSettings(RunnerSettings runnerSettings);

        /// <summary>   Runs the tests. </summary>
        /// <returns>   The Results of the test run. </returns>
        RunnerResults Run();
    }
}
