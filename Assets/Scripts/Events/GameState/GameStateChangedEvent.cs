using System;

public class GameStateChangedEvent : IGameEvent
{
    public DateTime TimeStamp { get; }
    public Guid EventId { get; }
    
    public GameState PreviousState { get; }
    public GameState NewState { get; }
    
    // Yeni özellikler
    public float TransitionDuration { get; set; }
    public bool IsTransitioning { get; set; }

    public GameStateChangedEvent(GameState previousState, GameState newState)
    {
        TimeStamp = DateTime.UtcNow;
        EventId = Guid.NewGuid();
        PreviousState = previousState;
        NewState = newState;
        TransitionDuration = 0f;
        IsTransitioning = false;
    }
} 