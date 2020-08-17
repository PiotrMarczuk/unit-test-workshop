using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using UnitTestMockingExamples.BusinessLogic.Services;
using UnitTestMockingExamples.Repository.BusinessModels;
using UnitTestMockingExamples.Repository.Repositories;

namespace UnitTestMockingExamples.UnitTests
{
    [TestFixture]
    public class LuckyNumberServiceTests
    {
        private LuckyNumberService _sut;
        private ISimpleRepository _repository;
        private ILogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = Substitute.For<ILogger>();
            _repository = Substitute.For<ISimpleRepository>();
            
            _sut = new LuckyNumberService(_logger, _repository);
        }
        
        [Test]
        public async Task When_LuckyNumbers_are_not_empty_AreLuckyNumbersNotEmpty_should_return_true()
        {
            // ARRANGE
            const bool expected = true;
            const int luckyId = 2;
            var simpleWithLuckyNumbers = new SimpleWithLuckyNumbers
            {
                Id = 1,
                LuckyNumbers = new List<int> { 1 }
            };

            _repository
                .GetSimpleWithLuckyNumbersAsync(luckyId)
                .Returns(simpleWithLuckyNumbers);
            
            // ACT
            var result = await _sut.AreLuckyNumbersEmptyAsync(luckyId);
            
            // ASSERT
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public async Task When_LuckyNumbers_are_empty_AreLuckyNumbersNotEmpty_should_return_false()
        {
            // ARRANGE
            const bool expected = false;
            const int luckyId = 1;
            var simpleWithLuckyNumbers = new SimpleWithLuckyNumbers
            {
                Id = 1,
                LuckyNumbers = new List<int>()
            };

            _repository
                .GetSimpleWithLuckyNumbersAsync(luckyId)
                .Returns(simpleWithLuckyNumbers);
            // ACT
            var result = await _sut.AreLuckyNumbersEmptyAsync(luckyId);
            
            // ASSERT
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}