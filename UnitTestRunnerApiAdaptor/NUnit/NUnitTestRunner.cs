namespace TestApiRunner.NUnit
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using global::NUnit.Engine;
    using TestApiRunner.NUnit.Serialization;

    /// <summary>
    /// Entry point to the main NUnit test runner.
    /// </summary>
    public static class NUnitTestRunner
    {
        /// <summary>
        /// Main entry point into the NUnit test runner.
        /// </summary>
        public static void Run()
        {
            /*
            Bit of a pain, but got this working!
            Seems there's a problem with versions up to 3.11 of the engine.
            3.12.0-beta1 fixes this.

            docs
            https://www.nuget.org/packages/NUnit.Engine/
            https://docs.nunit.org/articles/nunit/technical-notes/nunit-internals/Test-Engine-API.html

            problem running tests programmatically:
            https://stackoverflow.com/questions/62038734/
            */

            var dllFolder = @"C:\Users\james\source\repos\UnitTestRunnerApiAdaptor\SampleUnderTest.NUnit\bin\Debug\netcoreapp3.1";
            var dllFile = "SampleUnderTest.Tests.NUnit.dll";
            var dllFullPath = Path.Combine(dllFolder, dllFile);

            using (ITestEngine nunitEngine = TestEngineActivator.CreateInstance())
            {
                nunitEngine.WorkDirectory = dllFolder;

                TestPackage package = new TestPackage(dllFullPath);
                package.AddSetting("WorkDirectory", $"{dllFolder} PATH");

                var filterService = nunitEngine.Services.GetService<ITestFilterService>();
                ITestFilterBuilder builder = filterService.GetTestFilterBuilder();

                TestFilter emptyFilter = builder.GetFilter();
                var testListener = new MyTestEventListener();

                // Get a runner for the test package
                using (ITestRunner runner = nunitEngine.GetRunner(package))
                {
                    // Run all the tests in the assembly
                    var testResult = runner.Run(testListener, emptyFilter);
                    var deserialized = Deserialize<TestRun>(testResult);
                }
            }
        }

        private static T Deserialize<T>(XmlNode xmlNode)
            where T : class
        {
            using (var memoryStream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var xmlNodeReader = new XmlNodeReader(xmlNode))
                {
                    return serializer.Deserialize(xmlNodeReader) as T;
                }
            }
        }
    }
}
