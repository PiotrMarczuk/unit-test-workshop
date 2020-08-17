using System.Threading.Tasks;
using UnitTestMockingExamples.Repository.BusinessModels;

namespace UnitTestMockingExamples.BusinessLogic.Services
{
    public interface ILuckyNumberService
    {
        Task<bool> AreLuckyNumbersNotEmptyAsync(int luckyId);

        Task<SimpleWithLuckyNumbers> GetSimpleWithLuckyNumbersAsync(int luckyId);
    }
}