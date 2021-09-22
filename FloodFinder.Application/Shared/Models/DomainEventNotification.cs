using System.Collections.Generic;
using FloodFinder.Core.Shared;
using MediatR;

namespace FloodFinder.Application.Shared.Models
{
  public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
  {
    public DomainEventNotification(TDomainEvent domainEvent)
    {
      DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
  }

  public class GenericResponse
  {
    public static ApplicationResponse Success()
    {
      return new ApplicationResponse() { Success = true };
    }

    public static ApplicationResponse Failed(string error)
    {
      return new ApplicationResponse() { Success = false, Errors = new List<string>() { error } };
    }

    public static ApplicationResponse<T> Success<T>(T data)
    {
      return new ApplicationResponse<T>() { Success = true, Data = data };
    }

    public static ApplicationResponse<T> Failed<T>(string error)
    {
      return new ApplicationResponse<T>() { Success = false, Errors = new List<string>() { error } };
    }

    public static ApplicationResponse<T> Failed<T>(List<string> errors)
    {
      return new ApplicationResponse<T>() { Success = false, Errors = errors };
    }
  }
}