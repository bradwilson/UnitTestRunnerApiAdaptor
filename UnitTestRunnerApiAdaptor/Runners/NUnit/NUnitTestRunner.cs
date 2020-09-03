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
            var dllFolder = @"C:\Users\james\source\repos\jameswiseman76\UnitTestRunnerApiAdaptor\SampleUnderTest.NUnit\bin\Debug\netcoreapp3.1";
            var dllFile = "SampleUnderTest.Tests.NUnit.dll";
            var dllFullPath = Path.Combine(dllFolder, dllFile);

            using ITestEngine nunitEngine = TestEngineActivator.CreateInstance();
            nunitEngine.WorkDirectory = dllFolder;

            TestPackage package = new TestPackage(dllFullPath);
            package.AddSetting("WorkDirectory", $"{dllFolder} PATH");

            var filterService = nunitEngine.Services.GetService<ITestFilterService>();
            ITestFilterBuilder builder = filterService.GetTestFilterBuilder();

            var filter = builder.GetFilter();
            var testListener = new MyTestEventListener();

            // Get a runner for the test package
            using var runner = nunitEngine.GetRunner(package);

            // Run all the tests in the assembly
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
    }
}
