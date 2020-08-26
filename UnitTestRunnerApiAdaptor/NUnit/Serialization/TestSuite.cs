namespace TestApiRunner.NUnit.Serialization
{
    using System.Xml.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not required for XML Serialization")]
    [XmlRoot(ElementName = "test-suite")]
    public class Testsuite
    {
        [XmlElement(ElementName = "test-case")]
        public TestCase TestCase { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "fullname")]
        public string Fullname { get; set; }

        [XmlAttribute(AttributeName = "classname")]
        public string Classname { get; set; }

        [XmlAttribute(AttributeName = "runstate")]
        public string Runstate { get; set; }

        [XmlAttribute(AttributeName = "testcasecount")]
        public string Testcasecount { get; set; }

        [XmlAttribute(AttributeName = "result")]
        public string Result { get; set; }

        [XmlAttribute(AttributeName = "start-time")]
        public string Starttime { get; set; }

        [XmlAttribute(AttributeName = "end-time")]
        public string Endtime { get; set; }

        [XmlAttribute(AttributeName = "duration")]
        public string Duration { get; set; }

        [XmlAttribute(AttributeName = "total")]
        public string Total { get; set; }

        [XmlAttribute(AttributeName = "passed")]
        public string Passed { get; set; }

        [XmlAttribute(AttributeName = "failed")]
        public string Failed { get; set; }

        [XmlAttribute(AttributeName = "warnings")]
        public string Warnings { get; set; }

        [XmlAttribute(AttributeName = "inconclusive")]
        public string Inconclusive { get; set; }

        [XmlAttribute(AttributeName = "skipped")]
        public string Skipped { get; set; }

        [XmlAttribute(AttributeName = "asserts")]
        public string Asserts { get; set; }
    }
}
