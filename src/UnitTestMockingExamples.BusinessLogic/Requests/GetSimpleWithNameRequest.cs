using MediatR;

using UnitTestMockingExamples.Responses;

namespace UnitTestMockingExamples.Requests
{
    public class GetSimpleWithNameRequest : IRequest<SimpleWithNameResponse>
    {
        public int Id { get; }

        public GetSimpleWithNameRequest(int id)
        {
            Id = id;
        }
    }
}
