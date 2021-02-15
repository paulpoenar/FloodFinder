using System;
using System.Threading.Tasks;
using FloodFinder.Application.Contracts;
using FloodFinder.Application.Shared.Models;
using FloodFinder.Core.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FloodFinder.Infrastructure.Events
{
  public class DomainEventsPublisher: IDomainEventsPublisher
  {
    private readonly ILogger<DomainEventsPublisher> _logger;
    private readonly IPublisher _mediator;

    public DomainEventsPublisher(ILogger<DomainEventsPublisher> logger, IPublisher mediator)
    {
      _logger = logger;
      _mediator = mediator;
    }

    public async Task Publish(DomainEvent domainEvent)
    {
      _logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
      await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
    }

    private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
    {
      return (INotification)Activator.CreateInstance(
        typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
    }
  }
}