using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using UnitTestMockingExamples.Database.Context;
using UnitTestMockingExamples.Database.DbModels;
using UnitTestMockingExamples.Repository.BusinessModels;
using UnitTestMockingExamples.Repository.Repositories;

namespace UnitTestMockingExamples.UnitTests
{
    [TestFixture]
    public class SimpleRepositoryTests
    {
        private ISimpleRepository _sut;
        private SimpleDbContext _dbContext;

        private List<SimpleDbModel> simpleList = new List<SimpleDbModel>
        {
            new SimpleDbModel
            {
                Name = "Test",
            },
            new SimpleDbModel
            {
                Name = "Test2",
            }
        };

        [SetUp]
        public void SetUp()
        {
            _dbContext = CreateDbContext();

            _sut = new SimpleRepository(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext?.Dispose();
            _dbContext = null;
        }

        [Test]
        public async Task When_called_GetSimpleWithFullSetOfDataAsync_with_existing_id_should_return_correct_object()
        {
            AddDataToDbContext(simpleList[0]);
            AddDataToDbContext(simpleList[1]);
            
            SimpleWithFullSetOfData result = await _sut.GetSimpleWithFullSetOfDataAsync(simpleList.First().Id);
            
            AssertThatAreEqualByJson(result, simpleList.First());
        }


        private static void AssertThatAreEqualByJson(object actual, object expected)
        {
            string expectedJson = JsonConvert.SerializeObject(expected);
            string actualJson = JsonConvert.SerializeObject(actual);

            Assert.That(actualJson, Is.EqualTo(expectedJson));
        }

        private static SimpleDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<SimpleDbContext>()
                .UseInMemoryDatabase("SimpleDb")
                .Options;

            var dbContext = new SimpleDbContext(options);
            return dbContext;
        }

        private void AddDataToDbContext(SimpleDbModel simpleDbModel)
        {
            _dbContext.SimpleDbModels.Add(new SimpleDbModel
            {
                Description = simpleDbModel.Description,
                Name = simpleDbModel.Name,
                LuckyNumbers = new List<int>()
            });
            _dbContext.SaveChanges();
        }
    }
}