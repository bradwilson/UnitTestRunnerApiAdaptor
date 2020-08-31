namespace UnitTestRunnerApiAdaptor
{
    using System;
    using System.IO;

    /// <summary>
    /// Contains functionality to deply a test project and dependencies to a dedicated location for execution.
    /// </summary>
    public class TestDeployer
    {
        private const string TestRunnerFolder = "TestRunner";

        private string deploymentPathRoot;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDeployer"/> class.
        /// </summary>
        /// <param name="deploymentPathRoot">
        /// The location to deploy the test project and assemblies.
        /// Defaults to the path of the current user's temporary folder.
        /// </param>
        public TestDeployer(string deploymentPathRoot = null)
        {
            this.deploymentPathRoot = deploymentPathRoot ?? Path.GetTempPath();
        }

        /// <summary>
        /// Deploy files from the given folder to the Test Runner Folder.
        /// </summary>
        /// <param name="source">The source location to deploy.</param>
        /// <returns>The location of where the files have been deployed to.</returns>
        public string DeployItems(string source)
        {
            var deploymentPathFull = Path.Combine(this.deploymentPathRoot, TestRunnerFolder);
            if (!Directory.Exists(deploymentPathFull))
            {
                Directory.CreateDirectory(deploymentPathFull);
            }

            var directoryFullName = CreateDeploymentSubDirectory(deploymentPathFull);

            DirectoryCopy(source, directoryFullName);

            return directoryFullName;
        }

        private static string CreateDeploymentSubDirectory(string deploymentPathFull)
        {
            var dt = DateTime.Now;
            var directorySubfolder = $"Deploy_{Environment.UserName} {dt.Year:D2}-{dt.Month:D2}-{dt.Day:D2} {dt.Hour:D2}_{dt.Minute:D2}_{dt.Second:D2}_{dt.Millisecond}";
            var dirInfo = Directory.CreateDirectory(Path.Combine(deploymentPathFull, directorySubfolder));
            return dirInfo.FullName;
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName)
        {
            // Get the subdirectories for the specified directory.
            var currentDirectory = new DirectoryInfo(sourceDirName);

            if (!currentDirectory.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            var dirs = currentDirectory.GetDirectories();

            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            var files = currentDirectory.GetFiles();
            foreach (var file in files)
            {
                var temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // Copy subdirectories, copy them and their contents to new location.
            foreach (var subdir in dirs)
            {
                var temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath);
            }
        }
    }
}
