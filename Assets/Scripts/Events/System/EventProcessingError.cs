using System;
using System.Collections.Generic;
using UnityEngine;

public class EventProcessingError : IGameEvent
{
    public DateTime TimeStamp { get; }
    public Guid EventId { get; }
    public Exception Exception { get; }
    public IGameEvent FailedEvent { get; }

    public EventProcessingError(Exception exception, IGameEvent failedEvent)
    {
        TimeStamp = DateTime.UtcNow;
        EventId = Guid.NewGuid();
        Exception = exception;
        FailedEvent = failedEvent;
    }
} 