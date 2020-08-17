using System;
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

        public async Task<bool> AreLuckyNumbersNotEmptyAsync(int luckyId)
        {
            if (luckyId < 0)
            {
                _logger.LogError($"{luckyId} should be greater than zero");
                
                throw new ArgumentException(nameof(luckyId));
            }
            
            SimpleWithLuckyNumbers simpleWithLuckyNumbers = await _repository
                .GetSimpleWithLuckyNumbersAsync(luckyId);

            return simpleWithLuckyNumbers.LuckyNumbers.Count > 0;
        }

        public async Task<SimpleWithLuckyNumbers> GetSimpleWithLuckyNumbersAsync(int luckyId)
        {
            return await _repository
                .GetSimpleWithLuckyNumbersAsync(luckyId);
        }
    }
}