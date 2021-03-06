﻿namespace UnitTestRunnerApiAdaptor.Runners.NUnit.Serialization
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not required for XML Serialization")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Not required for XML Serialization")]
    [XmlRoot(ElementName = "test-run")]
    public class TestRun
    {
        [XmlElement(ElementName = "command-line")]
        public List<string> CommandLine { get; set; }

        [XmlElement(ElementName = "test-suite")]
        public List<TestSuite> TestSuites { get; set; }
    }
}