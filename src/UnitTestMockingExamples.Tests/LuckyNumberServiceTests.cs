using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using NSubstitute;

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
            var result = await _sut.AreLuckyNumbersNotEmptyAsync(luckyId);
            
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
            var result = await _sut.AreLuckyNumbersNotEmptyAsync(luckyId);
            
            // ASSERT
            Assert.That(result, Is.EqualTo(expected));
        }
        
        [Test]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        [TestCase(-1000)]
        public void When_called_AreLuckyNumbersNotEmpty_with_negative_parameter_should_throw_ArgumentException(int parameter)
        {
            Assert
                .ThrowsAsync<ArgumentException>(() => 
                    _sut.AreLuckyNumbersNotEmptyAsync(parameter));
        }
        
        [Test]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        [TestCase(-1000)]
        public async Task When_called_AreLuckyNumbersNotEmpty_with_negative_parameter_logger_should_LogError(int parameter)
        {
            try
            {
                await _sut.AreLuckyNumbersNotEmptyAsync(parameter);
            }
            catch (ArgumentException)
            {
                // suppress
            }
            
            _logger
                .Received()
                .LogError($"{parameter} should be greater than zero");
        }

        [Test]
        public async Task When_called_GetSimpleWithLuckyNumbersAsync_with_correct_id_should_return_expected_object()
        {
            // ARRANGE
            const int luckyId = 1;

            var expected = new SimpleWithLuckyNumbers()
            {
                Id = 1,
                LuckyNumbers = new List<int> {1, 2}
            };
            
            var dbMock = new SimpleWithLuckyNumbers()
            {
                Id = 1,
                LuckyNumbers = new List<int> {1, 2}
            };

            _repository
                .GetSimpleWithLuckyNumbersAsync(luckyId)
                .Returns(dbMock);
            
            // ACT
            SimpleWithLuckyNumbers result = await _sut.GetSimpleWithLuckyNumbersAsync(luckyId);
            
            // ASSERT
            
            TestHelper.AreEqualByJson(expected, result);
        }
    }

    public static class TestHelper
    {
        public static void AreEqualByJson(object expected, object actual)
        {
            var expectedJson = JsonConvert.SerializeObject(expected);
            var actualJson = JsonConvert.SerializeObject(actual);

            Assert.That(actualJson, Is.EqualTo(expectedJson));
        }
    }
}