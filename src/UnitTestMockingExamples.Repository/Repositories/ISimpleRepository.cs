using System.Threading.Tasks;
using UnitTestMockingExamples.Repository.BusinessModels;

namespace UnitTestMockingExamples.Repository.Repositories
{
    public interface ISimpleRepository
    {
        Task<SimpleWithFullSetOfData> GetSimpleWithFullSetOfDataAsync(int id);

        Task<SimpleWithLuckyNumbers> GetSimpleWithLuckyNumbersAsync(int id);

        Task<SimpleWithName> GetSimpleWithNameAsync(int id);
    }
}