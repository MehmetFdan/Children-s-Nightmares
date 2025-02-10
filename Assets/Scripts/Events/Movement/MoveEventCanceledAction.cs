using System;
using UnityEngine;

public class MoveEventCanceledAction : IGameEvent
{
    public DateTime TimeStamp { get; }
    public Guid EventId { get; }
    public Vector2 MoveInput { get; }

    public MoveEventCanceledAction(Vector2 moveInput)
    {
        TimeStamp = DateTime.UtcNow;
        EventId = Guid.NewGuid();
        MoveInput = moveInput;
    }
} 