using MediatR;

namespace UnitTestMockingExamples.Requests
{
    public class GetSimpleWithLuckyNumbersRequest : IRequest<GetSimpleWithLuckyNumbersRequest>
    {
        public int Id { get; }

        public GetSimpleWithLuckyNumbersRequest(int id)
        {
            Id = id;
        }
    }
}