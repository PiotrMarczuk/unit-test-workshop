using System.Threading.Tasks;

namespace UnitTestMockingExamples.BusinessLogic.Services
{
    public interface ILuckyNumberService
    {
        Task<bool> AreLuckyNumbersEmptyAsync(int luckyId);
    }
}