namespace UnitTestRunnerApiAdaptor
{
    /// <summary> Data cass that represents a single test run item. </summary>
    public class TestRunItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunItem"/> class.
        /// </summary>
        /// <param name="testClassName"> The name of the test class to run. </param>
        /// <param name="testName">   The runner settings. </param>
        public TestRunItem(string testClassName, string testName)
        {
            this.TestClassName = testClassName;
            this.TestName = testName;
            this.FullyQualifiedTestName = $"{testClassName}.{testName}";
        }

        /// <summary>
        /// Gets the value denoting the name of the test class to run.
        /// </summary>
        public string TestClassName { get; private set; }

        /// <summary>
        /// Gets the value denoting the name of the test to run.
        /// </summary>
        public string TestName { get; private set; }

        /// <summary>
        /// Gets the value denoting the name of the test to run.
        /// </summary>
        public string FullyQualifiedTestName { get; private set; }
    }
}
