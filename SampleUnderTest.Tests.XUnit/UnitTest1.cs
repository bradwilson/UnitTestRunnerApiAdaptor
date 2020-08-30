namespace SampleUnderTest.Tests.XUnit
{
    using Prime.Services;
    using System;
    using Xunit;

    public class UnitTest1
    {
        [Fact]
        public void OneIsNotPrime()
        {
            var primeService = new PrimeService();

            var result = primeService.IsPrime(1);

            Assert.False(result, "1 should not be prime");
        }

        [Fact]
        public void TwoIsPrime()
        {
            var primeService = new PrimeService();

            var result = primeService.IsPrime(2);

            Assert.True(result, "2 should be prime");
        }
    }
}
