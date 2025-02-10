using System;
using UnityEngine;

public class MoveEventPerformedAction : IGameEvent
{
    public DateTime TimeStamp { get; }
    public Guid EventId { get; }
    public Vector2 MoveEventActionInput { get; }

    public MoveEventPerformedAction(Vector2 moveEventActionInput)
    {
        TimeStamp = DateTime.UtcNow;
        EventId = Guid.NewGuid();
        MoveEventActionInput = moveEventActionInput;
    }
} 