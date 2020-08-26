namespace TestApiRunner.NUnit.Serialization
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not required for XML Serialization")]
    [XmlRoot(ElementName = "settings")]
    public class Settings
    {
        [XmlElement(ElementName = "setting")]
        public List<Setting> Setting { get; set; }
    }
}
