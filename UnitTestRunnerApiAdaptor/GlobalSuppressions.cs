using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Name needed for XML Serialization", Scope = "type", Target = "~T:TestApiRunner.NUnit.Serialization.Property")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Required for XML Serialization", Scope = "member", Target = "~P:TestApiRunner.NUnit.Serialization.Properties.Property")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Required for XML Serialization", Scope = "member", Target = "~P:TestApiRunner.NUnit.Serialization.Settings.Setting")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Required for XML Serialization", Scope = "member", Target = "~P:TestApiRunner.NUnit.Serialization.Testrun.CommandLine")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Required for XML Serialization", Scope = "member", Target = "~P:TestApiRunner.NUnit.Serialization.Testrun.TestSuite")]
