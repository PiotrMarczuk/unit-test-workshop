using System.Threading.Tasks;
using UnitTestMockingExamples.Repository.BusinessModels;

namespace UnitTestMockingExamples.Repository.Repositories
{
    public interface ISimpleRepository
    {
        Task<SimpleWithFullSetOfData> GetSimpleWithFullSetOfDataAsync(int id);

        Task<SimpleWithLuckyNumbers> GetSimpleWithLuckyNumbers(int id);

        Task<SimpleWithName> GetSimpleWithName(int id);
    }
}