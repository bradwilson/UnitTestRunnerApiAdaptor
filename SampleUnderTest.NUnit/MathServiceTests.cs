namespace SampleUnderTest.Tests.NUnit
{
    using global::NUnit.Framework;
    using SampleUnderTest;

    public class MathServiceTests
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

        [Test]
        public void DoSomethingDoesABunchOfStuff()
        {
            // Arrange
            var expected = 50;
            var underTest = new MathService();

            // Act
            var actual = underTest.DoSomething(true, 10, 10);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}