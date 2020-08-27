using System.Linq;
using System.Threading.Tasks;

using UnitTestMockingExamples.BusinessLogic.Core;
using UnitTestMockingExamples.Repository.BusinessModels;
using UnitTestMockingExamples.Repository.Repositories;

namespace UnitTestMockingExamples.BusinessLogic.Services
{
    public class LuckyNumberService : ILuckyNumberService
    {
        private readonly ISimpleRepository _simpleRepository;

        public LuckyNumberService(ISimpleRepository simpleRepository)
        {
            _simpleRepository = simpleRepository;
        }
        
        public async Task<Result<int>> GetSumOfLuckyNumbersForGivenIdAsync(int id)
        {
            if (id <= 0)
            {
                return Result.Fail<int>($"{id} should be greater than zero.");
            }
            
            SimpleWithLuckyNumbers simpleWithLuckyNumbers = await _simpleRepository.GetSimpleWithLuckyNumbersAsync(id);

            if (simpleWithLuckyNumbers == null)
            {
                return Result.Fail<int>($"Simple with lucky numbers with {id} was not found.");
            }

            if (simpleWithLuckyNumbers.LuckyNumbers == null)
            {
                return Result.Fail<int>($"LuckyNumbers was null in Simple with id: {id}");
            }
            
            int sum =  simpleWithLuckyNumbers.LuckyNumbers.Sum();

            return Result.Ok(sum);
        }
    }
}