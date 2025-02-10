using UnityEngine;
using System;

public class GameStateMachine : MonoBehaviour
{
    private static GameStateMachine instance;
    public static GameStateMachine Instance => instance;

    private GameState previousState;
    private GameState currentState;
    private GameState nextState;
    private bool isTransitioning;
    private float transitionStartTime;
    private float transitionDuration;

    public GameState CurrentState => currentState;
    public GameState PreviousState => previousState;
    public bool IsTransitioning => isTransitioning;

    [SerializeField] private float defaultTransitionDuration = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Başlangıç state'ini ayarla
        ChangeState(GameState.MainMenu);
    }

    private void Update()
    {
        if (isTransitioning)
        {
            UpdateTransition();
        }
    }

    public void ChangeState(GameState newState, float transition = 0f)
    {
        if (isTransitioning)
        {
            Debug.LogWarning("State transition already in progress!");
            return;
        }

        previousState = currentState;
        nextState = newState;
        transitionDuration = transition;

        if (transition > 0)
        {
            isTransitioning = true;
            transitionStartTime = Time.time;
            PublishStateTransitionStarted();
        }
        else
        {
            CompleteStateChange();
        }
    }

    private void UpdateTransition()
    {
        float progress = (Time.time - transitionStartTime) / transitionDuration;
        
        if (progress >= 1f)
        {
            CompleteStateChange();
        }
    }

    private void CompleteStateChange()
    {
        previousState = currentState;
        currentState = nextState;
        isTransitioning = false;
        PublishStateChanged();
    }

    private void PublishStateTransitionStarted()
    {
        EventBus.Publish(new GameStateChangedEvent(currentState, nextState)
        {
            TransitionDuration = transitionDuration,
            IsTransitioning = true
        });
    }

    private void PublishStateChanged()
    {
        EventBus.Publish(new GameStateChangedEvent(currentState, currentState)
        {
            TransitionDuration = 0,
            IsTransitioning = false
        });
    }

    public void PauseGame() => ChangeState(GameState.Paused, defaultTransitionDuration);
    public void ResumeGame() => ChangeState(GameState.Playing, defaultTransitionDuration);
    public void GoToMainMenu() => ChangeState(GameState.MainMenu, defaultTransitionDuration);
    public void StartLoading() => ChangeState(GameState.Loading);
    public void StartPlaying() => ChangeState(GameState.Playing, defaultTransitionDuration);
}