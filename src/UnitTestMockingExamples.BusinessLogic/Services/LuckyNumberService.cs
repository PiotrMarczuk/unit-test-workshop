using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnitTestMockingExamples.Repository.BusinessModels;
using UnitTestMockingExamples.Repository.Repositories;

namespace UnitTestMockingExamples.BusinessLogic.Services
{
    public class LuckyNumberService : ILuckyNumberService
    {
        private readonly ILogger _logger;
        private readonly ISimpleRepository _repository;

        public LuckyNumberService(ILogger logger, ISimpleRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> AreLuckyNumbersEmptyAsync(int luckyId)
        {
            SimpleWithLuckyNumbers simpleWithLuckyNumbers = await _repository
                .GetSimpleWithLuckyNumbersAsync(luckyId);

            return simpleWithLuckyNumbers.LuckyNumbers.Count > 0;
        }
    }
}