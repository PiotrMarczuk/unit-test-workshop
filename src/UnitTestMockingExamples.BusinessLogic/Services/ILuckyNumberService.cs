using System.Threading.Tasks;
using UnitTestMockingExamples.BusinessLogic.Core;

namespace UnitTestMockingExamples.BusinessLogic.Services
{
    public interface ILuckyNumberService
    {
        public Task<Result<int>> GetSumOfLuckyNumbersForGivenIdAsync(int id);
    }
}