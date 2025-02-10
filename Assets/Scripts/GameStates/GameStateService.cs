using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateService
{
    private GameState _currentState;
    public GameState CurrentState => _currentState;

    public event Action<GameState> OnGameStateChanged;

    public void ChangeState(GameState newState)
    {
        if (_currentState == newState)
            return;

        var oldState = _currentState;
        _currentState = newState;
        
        EventBus.Publish(new GameStateChangedEvent(oldState, newState));
        OnGameStateChanged?.Invoke(newState);
    }
} 