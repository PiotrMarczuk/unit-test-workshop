using MediatR;
using UnitTestMockingExamples.Responses;

namespace UnitTestMockingExamples.Requests
{
    public class GetSimpleWithFullSetOfDataRequest : IRequest<SimpleWithFullSetOfDataResponse>
    {
        public int Id { get; }

        public GetSimpleWithFullSetOfDataRequest(int id)
        {
            Id = id;
        }
    }
}
