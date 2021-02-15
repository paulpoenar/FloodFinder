using System;
using System.Collections.Generic;

namespace FloodFinder.Core.Shared
{
  public class DomainEntity
  {
    public int Id { get; private set; }
    public DateTime Created { get; set; }
    public int CreatedBy { get; set; }

    private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;

    protected DomainEntity()
    {
    }
    
    protected void AddDomainEvent(DomainEvent domainEvent)
    { _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
      _domainEvents.Clear();
    }

  }

  public abstract class DomainEvent
  {
    protected DomainEvent()
    {
      DateOccurred = DateTimeOffset.UtcNow;
    }
    public DateTimeOffset DateOccurred { get; protected set; }
  }
}