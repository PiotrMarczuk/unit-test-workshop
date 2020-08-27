using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using UnitTestMockingExamples.BusinessLogic.Services;
using UnitTestMockingExamples.Repository.BusinessModels;
using UnitTestMockingExamples.Repository.Repositories;

namespace UnitTestMockingExamples.UnitTests
{
    [TestFixture]
    public class ServiceTests
    {
        private LuckyNumberService _sut;
        private ISimpleRepository _repository;

        private static readonly object[] SourceLists =
        {
            new object[] {new List<int> {1}},
            new object[] {new List<int> {1, 2}},
            new object[] {new List<int> {1, 2, 3}},
            new object[] {new List<int> {1, 2, 3, 4}}
        };


        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<ISimpleRepository>();
            _sut = new LuckyNumberService(_repository);
        }

        [Test]
        [TestCaseSource(nameof(SourceLists))]
        public async Task When_called_GetSumOfLuckyNumbersForGivenIdAsync_should_return_correct_value(List<int> numbers)
        {
            // ARRANGE
            const int id = 1;

            var expectedSimpleWithLuckyNumbers = new SimpleWithLuckyNumbers
            {
                Id = id,
                LuckyNumbers = numbers
            };

            _repository
                .GetSimpleWithLuckyNumbersAsync(1)
                .Returns(expectedSimpleWithLuckyNumbers);

            // ACT
            var result = await _sut.GetSumOfLuckyNumbersForGivenIdAsync(id);

            // ASSERT
            Assert.That(result.Value, Is.EqualTo(numbers.Sum()));
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public async Task When_called_GetSumOfLuckyNumberForGivenId_with_not_existing_id_should_return_Result_Fail()
        {
            // ARRANGE
            const int notExistingId = 3;

            // ACT
            var result = await _sut.GetSumOfLuckyNumbersForGivenIdAsync(notExistingId);

            // ASSERT
            Assert.That(result.IsFailure, Is.True);
        }

        [Test]
        [TestCase(int.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        public async Task When_called_GetSumOfLuckyNumberForGivenId_with_non_positive_id_should_return_Result_Fail(int id)
        {
            // ACT
            var result = await _sut.GetSumOfLuckyNumbersForGivenIdAsync(id);
            
            // Assert
            await _repository.DidNotReceive().GetSimpleWithLuckyNumbersAsync(id);
            
            Assert.That(result.IsFailure, Is.True);
        }

        [Test]
        public async Task When_called_GetSumOfLuckyNumberForGivenId_returns_Simple_with_null_LuckyNumbers_should_return_Result_Fail()
        {
            // ARRANGE
            const int id = 1;

            var expectedSimpleWithLuckyNumbers = new SimpleWithLuckyNumbers
            {
                Id = id,
                LuckyNumbers = null
            };

            _repository
                .GetSimpleWithLuckyNumbersAsync(1)
                .Returns(expectedSimpleWithLuckyNumbers);

            // ACT
            var result = await _sut.GetSumOfLuckyNumbersForGivenIdAsync(id);

            // ASSERT
            Assert.That(result.IsFailure, Is.True);
        }
        
        [Test]
        public async Task When_called_GetSumOfLuckyNumberForGivenId_returns_Simple_with_empty_LuckyNumbers_should_return_0()
        {
            // ARRANGE
            const int id = 1;

            var expectedSimpleWithLuckyNumbers = new SimpleWithLuckyNumbers
            {
                Id = id,
                LuckyNumbers = new List<int>()
            };

            _repository
                .GetSimpleWithLuckyNumbersAsync(1)
                .Returns(expectedSimpleWithLuckyNumbers);

            // ACT
            var result = await _sut.GetSumOfLuckyNumbersForGivenIdAsync(id);

            // ASSERT
            Assert.That(result.Value, Is.Zero);
        }
    }
}