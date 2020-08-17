using NUnit.Framework;

namespace UnitTestMockingExamples.UnitTests
{
    [TestFixture]
    public class LuckyNumberServiceTests
    {
        [SetUp]
        public void SetUp()
        {
          _sut = new LuckyNumberService();
        }
        
        [Test]
        public void When_LuckyNumbers_are_not_empty_should_return_true()
        {
            
        }
    }
}