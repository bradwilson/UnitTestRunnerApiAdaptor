﻿/// <summary>
/// Main MStest test runner class
/// </summary>
namespace UnitTestRunnerApiAdaptor.MSTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.VisualStudio.TestPlatform.MSTestFramework;

    /// <summary>
    /// Entry point to the main MStest test runner.
    /// </summary>
    public static class MSTestRunner
    {
        /// <summary>
        /// Main entry point into the MSTest test runner.
        /// </summary>
        public static void Run()
        {
            //// https://social.msdn.microsoft.com/Forums/vstudio/en-US/ccd08ce1-a86b-4965-af68-1db8cd7d6715/how-to-execute-test-methods-programmatically-using-c-may-be-using-reflection?forum=vsunittest

            var dllFolder =  @"C:\Users\james\source\repos\jameswiseman76\UnitTestRunnerApiAdaptor\SampleUnderTest.Test.MSTest\bin\Debug\netcoreapp3.1";
            var deploymentPath = DeployItems(dllFolder);

            var dllFile = "SampleUnderTest.Test.MSTest.dll";
            var dllFullPath = Path.Combine(dllFolder, dllFile);

            ////var dllName = "Name_Assembly_ThatHas_TestMethods";
            var nameOfTestClass = "SampleUnderTest.Test.MSTest.MathServiceTests";
            var nameOfTestMethod = "AddWithGivenInputsReturnsExpectedResults";

            var testRunner = new UnitTestRunner(deploymentPath, dllFile, false, 1000);

            // Set the test run properties
            var properties = PrepareTestRunProperties(deploymentPath, nameOfTestClass, nameOfTestMethod);

            var testMethod = new TestMethod(nameOfTestMethod, nameOfTestClass, dllFile, false);
            var testElement = new UnitTestElement(testMethod);
            // var testResults = testRunner.RunDataDrivenTest(nameOfTestMethod, nameOfTestClass, false, properties);
            // var testResults = testRunner.RunDataDrivenTest(nameOfTestMethod, nameOfTestClass, false);

            var testResults = new[] { testRunner.RunSingleTest(nameOfTestMethod, nameOfTestClass, false) };

            if (testResults[0].Outcome == UnitTestOutcome.Passed)
            {
                Console.WriteLine("Test Case Passed");
            }
            else
            {
                Console.WriteLine("Test Case Failed with error " + testResults[0].ErrorMessage);
            }
        }

        private static string DeployItems(string source)
        {
            string deploymentPath = Path.Combine(@"c:\temp", "TestRunner");
            if (Directory.Exists(deploymentPath) == false)
            {
                Directory.CreateDirectory(deploymentPath);
            }

            DateTime dt = DateTime.Now;
            var dirName = $"Deploy_{Environment.UserName} {dt.Day}-{dt.Month}-{dt.Year} {dt.Hour}_{dt.Minute}_{dt.Second}_{dt.Millisecond}";

            var dirInfo = Directory.CreateDirectory(Path.Combine(deploymentPath, dirName));
            var dirIn = Directory.CreateDirectory(Path.Combine(dirInfo.FullName, "In"));
            Directory.CreateDirectory(Path.Combine(dirIn.FullName, Environment.MachineName));
            var dirOut = Directory.CreateDirectory(Path.Combine(dirInfo.FullName, "Out"));

            DirectoryCopy(source, dirOut.FullName, true);

            return dirInfo.FullName;
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            var dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            var dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (var subdir in dirs)
                {
                    var temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private static Dictionary<string, object> PrepareTestRunProperties(string deploymentPath, string nameOfTestClass, string nameOfTestMethod)
        {
            return new Dictionary<string, object>
            {
                { "TestRunDirectory", deploymentPath },
                { "DeploymentDirectory", Path.Combine(deploymentPath, "Out") },
                { "ResultsDirectory", Path.Combine(deploymentPath, "In") },
                { "TestRunResultsDirectory", Path.Combine(deploymentPath, "In", Environment.MachineName) },
                { "TestResultsDirectory", Path.Combine(deploymentPath, "In") },
                { "TestDir", Path.Combine(deploymentPath) },
                { "TestDeploymentDir", Path.Combine(deploymentPath, "Out") },
                { "TestLogsDir", Path.Combine(deploymentPath, "In", Environment.MachineName) },
                { "FullyQualifiedTestClassName", nameOfTestClass },
                { "TestName", nameOfTestMethod },
            };
        }
    }
}