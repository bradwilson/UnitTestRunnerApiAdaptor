// <copyright file="MyTestEventListener.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TestApiRunner.NUnit
{
    using global::NUnit.Engine;

    /// <summary>
    /// Sample Test Event Listener class.
    /// </summary>
    public class MyTestEventListener : ITestEventListener
    {
        /// <summary>
        /// Implementation of ITestEventListener.OnTestEvent method.
        /// </summary>
        /// <param name="report">The test event report.</param>
        public void OnTestEvent(string report)
        {
            /// System.Console.WriteLine(report);
        }
    }
}