using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FloodFinder.Application.Shared.Behaviours;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Moq;
using Xunit;

namespace FloodFinder.Tests.UnitTests.Application
{
    public class ValidationBehaviourTests
    {
        [Fact]
        public async Task ShouldThrowException_OnNullValue()
        {
            var list = new List<IValidator<RandomRequest>>() { new RandomValidator()};

            var system = new ValidationBehaviour<RandomRequest, RandomResponse>(list);

            var request = new RandomRequest
            {
                Name = null
            };

            Func<Task> act = () => system.Handle(request, default, null);
      
            await act
            .Should()
            .ThrowExactlyAsync<ValidationException>();
        }

        [Fact]
        public async Task ShouldCallNext_OnNoValidationErrors()
        {
            var list = new List<IValidator<RandomRequest>>() { new RandomValidator() };

            var system = new ValidationBehaviour<RandomRequest, RandomResponse>(list);

            var request = new RandomRequest
            {
                Name = "aa"
            };

            var delegateMock = new Mock<RequestHandlerDelegate<RandomResponse>>();

            await system.Handle(request, default, delegateMock.Object);

            delegateMock.Verify(next => next.Invoke(), Times.Once);
        }
    }

    public class RandomRequest: IRequest<RandomResponse>
    {
        public string Name { get; set; }
    }

    public class RandomValidator: AbstractValidator<RandomRequest>
    {
      
        public RandomValidator()
        {
            RuleFor(x => x.Name).NotNull();
        }
    }

    public class RandomResponse
    {

    }
}
