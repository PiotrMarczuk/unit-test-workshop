using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UnitTestMockingExamples.Database.Context;
using UnitTestMockingExamples.Repository.BusinessModels;

namespace UnitTestMockingExamples.Repository.Repositories
{
    public class SimpleRepository : ISimpleRepository
    {
        private readonly SimpleDbContext _context;

        public SimpleRepository(SimpleDbContext context)
        {
            _context = context;
        }

        public async Task<SimpleWithFullSetOfData> GetSimpleWithFullSetOfDataAsync(int id)
        {
            var simpleWithFullSetOfData = await _context
                .SimpleDbModels
                .Select(x =>
                    new SimpleWithFullSetOfData
                    {
                        Description = x.Description,
                        Id = x.Id,
                        LuckyNumbers = x.LuckyNumbers,
                        Name = x.Name
                    }
                ).SingleOrDefaultAsync(x => x.Id == id);

            return simpleWithFullSetOfData;
        }

        public async Task<SimpleWithLuckyNumbers> GetSimpleWithLuckyNumbers(int id)
        {
            var simpleWithLuckyNumbers = await _context
                .SimpleDbModels
                .Select(x =>
                    new SimpleWithLuckyNumbers
                    {
                        Id = x.Id,
                        LuckyNumbers = x.LuckyNumbers,
                    }
                ).SingleOrDefaultAsync(x => x.Id == id);

            return simpleWithLuckyNumbers;
        }

        public async Task<SimpleWithName> GetSimpleWithName(int id)
        {
            var simpleWithLuckyNumbers = await _context
                .SimpleDbModels
                .Select(x =>
                    new SimpleWithName
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }
                ).SingleOrDefaultAsync(x => x.Id == id);

            return simpleWithLuckyNumbers;
        }
    }
}
