namespace UnitTestRunnerApiAdaptor.Runners.NUnit
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using global::NUnit.Engine;
    using UnitTestRunnerApiAdaptor;
    using UnitTestRunnerApiAdaptor.Runners.NUnit.Serialization;

    /// <summary>
    /// Entry point to the main NUnit test runner.
    /// </summary>
    public class NUnitTestRunner : ITestRunner<NUnitTestRunner>
    {
        private TestRunnerSettings runnerSettings;

        /// <summary>   Include runner settings for the test run. </summary>
        /// <param name="runnerSettings">   The runner settings. </param>
        /// <returns>   The current instance of this TestRunner. </returns>
        public ITestRunner<NUnitTestRunner> WithRunnerSettings(TestRunnerSettings runnerSettings)
        {
            this.runnerSettings = runnerSettings;
            return this;
        }

        /// <summary>   Runs the tests. </summary>
        /// <returns>   The Results of the test run. </returns>
        public TestRunnerResults Run()
        {
            using ITestEngine nunitEngine = TestEngineActivator.CreateInstance();
            nunitEngine.WorkDirectory = this.runnerSettings.TestAssemblyFullPath;

            var package = new TestPackage(this.runnerSettings.TestAssemblyFullName);
            package.AddSetting("WorkDirectory", $"{this.runnerSettings.TestAssemblyFullPath} PATH");

            var filter = this.GetTestFiter(nunitEngine);

            using var runner = nunitEngine.GetRunner(package);

            // Run all the tests in the assembly
            var testListener = new DefaultTestEventListener();
            var testResult = runner.Run(testListener, filter);
            var deserializedTestResults = Deserialize<TestRun>(testResult);

            System.Console.WriteLine(deserializedTestResults.TestSuites[0].Total);

            return new TestRunnerResults(true, TestRunnerType.NUnit);
        }

        private static T Deserialize<T>(XmlNode xmlNode)
            where T : class
        {
            using var memoryStream = new MemoryStream();
            var serializer = new XmlSerializer(typeof(T));

            using var xmlNodeReader = new XmlNodeReader(xmlNode);
            return serializer.Deserialize(xmlNodeReader) as T;
        }

        private TestFilter GetTestFiter(ITestEngine nunitEngine)
        {
            var filterService = nunitEngine.Services.GetService<ITestFilterService>();
            var builder = filterService.GetTestFilterBuilder();

            this.runnerSettings.TestsToRun.ForEach(t => builder.AddTest(t.FullyQualifiedTestName));
            var filter = builder.GetFilter();
            return filter;
        }
    }
}
