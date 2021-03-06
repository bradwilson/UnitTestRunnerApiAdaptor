namespace SampleUnderTest.Test.MSTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MathServiceTests
    {
        [TestMethod]
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

        [TestMethod]
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
