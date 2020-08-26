namespace SampleUnderTest.Tests.NUnit
{
    using global::NUnit.Framework;
    using SampleUnderTest;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddWithGivenInputsReturnsExpectedResults()
        {
            // Arrange
            var expected = 4;
            var underTest = new MathService();

            // Act
            var actual = underTest.Add(2, 2);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}