using System;
using System.Collections.Generic;

public static class EventBus
{
    private static readonly Dictionary<Type, List<Delegate>> Subscribers = new();
    public static void Subscribe<T>(Action<T> callback) where T : IGameEvent
    {
        Type eventType = typeof(T);
        if (!Subscribers.ContainsKey(eventType))
        {
            Subscribers[eventType] = new List<Delegate>();
        }
        Subscribers[eventType].Add(callback);
    }

    public static void Unsubscribe<T>(Action<T> callback) where T : IGameEvent
    {
        Type eventType = typeof(T);
        if (Subscribers.ContainsKey(eventType))
        {
            Subscribers[eventType].Remove(callback);
        }
    }

    public static void UnsubscribeAll<T>() where T : IGameEvent
    {
        Type eventType = typeof(T);
        if (Subscribers.ContainsKey(eventType))
        {
            Subscribers[eventType].Clear();
        }
    }

    public static void UnsubscribeAll()
    {
        Subscribers.Clear();
    }

    private static readonly List<IGameEvent> EventLog = new();

    public static void Publish<T>(T gameEvent) where T : IGameEvent
    {
        if (gameEvent == null)
            throw new ArgumentNullException(nameof(gameEvent));

        Type eventType = typeof(T);
        EventLog.Add(gameEvent);

        if (!Subscribers.ContainsKey(eventType))
            return;

        var subscribersCopy = new List<Delegate>(Subscribers[eventType]);
        
        foreach (var callback in subscribersCopy)
        {
            try
            {
                if (callback is Action<T> typedCallback)
                {
                    typedCallback(gameEvent);
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"EventBus Error processing event {typeof(T).Name}: {ex.Message}");
                // Hata loglaması için event fırlatabiliriz
                PublishError(new EventProcessingError(ex, gameEvent));
            }
        }
    }

    private static void PublishError(EventProcessingError error)
    {
        UnityEngine.Debug.LogError($"Event Processing Error: {error.Exception.Message} for event {error.FailedEvent.GetType().Name}");
    }

    public static IReadOnlyList<IGameEvent> GetEventLog() => EventLog.AsReadOnly();
}