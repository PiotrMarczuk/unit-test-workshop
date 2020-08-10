using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using NSubstitute;
using NUnit.Framework;
using UnitTestMockingExamples.BusinessLogic.Handlers;
using UnitTestMockingExamples.Repository.BusinessModels;
using UnitTestMockingExamples.Repository.Repositories;
using UnitTestMockingExamples.Requests;
using UnitTestMockingExamples.Responses;

namespace UnitTestMockingExamples.UnitTests
{
    public class GetSimpleWithFullDataHandlerTests
    {
        private GetSimpleWithFullDataHandler _sut;
        private ISimpleRepository _repository;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<ISimpleRepository>();
            _mapper = Substitute.For<IMapper>();

            _sut = new GetSimpleWithFullDataHandler(_repository, _mapper);
        }

        [Test]
        public void When_called_Handle_with_null_GetSimpleWithFullSetOfDataRequest_should_throw_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _sut.Handle(null, CancellationToken.None));
        }

        [Test]
        public async Task When_called_Handle_with_id_in_request_that_does_not_exist_in_database_should_return_response_with_HasError_flag_set_to_true()
        {
            const int id = 1;
            var request = new GetSimpleWithFullSetOfDataRequest(id);

            var result = await _sut.Handle(request, CancellationToken.None);

            Assert.That(result.HasError, Is.EqualTo(true));
        }

        [Test]
        public async Task When_called_Handle_with_id_in_request_that_exists_in_database_should_return_correct_response()
        {
            // This test is not really valuable
            // because we're mocking actual behavior of some code which is crucial for our result, so normally
            // we're returning _mapper.Map<> and if we check the result, we're testing Mapper library :).
            // Especially in line 81 we're mocking mapper which was actual result, so this test is just for showing
            // how to mock :).
            // When our logic becomes more complex, it's really useful to mock other parts that we don't really want to test.
            // For example, our repository or external services. We just precise, what we want to get from those
            // in our execution of test without any need to really run them.
            
            // ### ONLY FOR LEARNING PURPOSES ###
            // Just to check, that we've mocked something and we're testing our real handle method, we're doing something more
            // than just returning result of mapping.

            const int id = 1;
            var simpleBusinessObject = new SimpleWithFullSetOfData
            {
                Description = "something",
                Id = 1,
                LuckyNumbers = new List<int> {1, 2, 3, 4},
                Name = "Simple"
            };

            _repository
                .GetSimpleWithFullSetOfDataAsync(id)
                .Returns(simpleBusinessObject);

            _mapper
                .Map<SimpleWithFullSetOfDataResponse>(simpleBusinessObject)
                .Returns(new SimpleWithFullSetOfDataResponse
                {
                    Description = string.Empty,
                    Id = 93939,
                    HasError = true,
                    LuckyNumbers = new List<int>{1, 2, 3, 4},
                    Name = "wow, I'm mocked"
                });

            var request = new GetSimpleWithFullSetOfDataRequest(1);

            var result = await _sut
                .Handle(request, CancellationToken.None);

            Assert.That(result.HasError, Is.EqualTo(false));
            Assert.That(result.LuckyNumbers, Is.EqualTo(new List<int> {1}));
            Assert.That(result.Name, Is.EqualTo("smth"));
            Assert.That(result.Id, Is.EqualTo(93939));
            Assert.That(result.Description, Is.EqualTo(string.Empty));
        }
    }
}

