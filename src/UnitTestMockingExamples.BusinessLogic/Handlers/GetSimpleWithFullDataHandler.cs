using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using UnitTestMockingExamples.Repository.Repositories;
using UnitTestMockingExamples.Requests;
using UnitTestMockingExamples.Responses;

namespace UnitTestMockingExamples.BusinessLogic.Handlers
{
    public class GetSimpleWithFullDataHandler : IRequestHandler<GetSimpleWithFullSetOfDataRequest, SimpleWithFullSetOfDataResponse>
    {
        private readonly ISimpleRepository _repository;
        private readonly IMapper _mapper;

        public GetSimpleWithFullDataHandler(ISimpleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SimpleWithFullSetOfDataResponse> Handle(GetSimpleWithFullSetOfDataRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var simpleWithFullData = await _repository.GetSimpleWithFullSetOfDataAsync(request.Id);

            if (simpleWithFullData == null)
            {
                return new SimpleWithFullSetOfDataResponse
                {
                    HasError = true
                };
            }

            var response =  _mapper.Map<SimpleWithFullSetOfDataResponse>(simpleWithFullData);

            // Just example for test purposes :)
            response.Name = "smth";
            response.HasError = false;
            response.LuckyNumbers = new List<int>{ 1 };

            return response;
        }
    }
}
