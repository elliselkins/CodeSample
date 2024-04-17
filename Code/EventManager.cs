using System;
using System.Collections.Generic;

/// <summary>
/// Register listener functions here for events that are triggered.
/// </summary>
public class EventManager
{
    private Dictionary<Type, Action<EventBase>> eventDictionary =
        new Dictionary<Type, Action<EventBase>>();

    public void AddListener(Type eventType, Action<EventBase> listener)
    {
        Action<EventBase> action;
        bool found = eventDictionary.TryGetValue(eventType, out action);
        if (!found)
        {
            action = listener;
            eventDictionary.Add(eventType, action);
        }
        else
        {
            action += listener;
            eventDictionary[eventType] = action;
        }
    }

    public void RemoveListener(Type eventType, Action<EventBase> listener)
    {
        Action<EventBase> action;
        if (eventDictionary.TryGetValue(eventType, out action))
            action -= listener;
    }

    public void TriggerEvent(EventBase evt)
    {
        Action<EventBase> action;
        if (eventDictionary.TryGetValue(evt.GetType(), out action))
            action.Invoke(evt);
    }
}