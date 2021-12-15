using Moq;
using NUnit.Framework;

namespace BoligWebTestt
{
    public class Tests
    {
        var paymentServiceMock = new Mock<PostController>();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}