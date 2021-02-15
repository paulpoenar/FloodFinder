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
    public static GenericResponseModel Success()
    {
      return new GenericResponseModel() { Success = true };
    }

    public static GenericResponseModel Failed(string error)
    {
      return new GenericResponseModel() { Success = false, Errors = new List<string>() { error } };
    }

    public static GenericResponseModel<T> Success<T>(T data)
    {
      return new GenericResponseModel<T>() { Success = true, Data = data };
    }

    public static GenericResponseModel<T> Failed<T>(string error)
    {
      return new GenericResponseModel<T>() { Success = false, Errors = new List<string>() { error } };
    }

    public static GenericResponseModel<T> Failed<T>(List<string> errors)
    {
      return new GenericResponseModel<T>() { Success = false, Errors = errors };
    }
  }
}