using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManagerValentijn
{
    private static Dictionary<UnityEngine.EventType, System.Action> eventDictionary = new Dictionary<UnityEngine.EventType, System.Action>();

    public static void Register(UnityEngine.EventType _eventType, System.Action _listener)
    {
        if (!eventDictionary.ContainsKey(_eventType))
        {
            eventDictionary.Add(_eventType, null);
        }
        eventDictionary[_eventType] += _listener;
    }

    public static void UnRegister(UnityEngine.EventType _eventType, System.Action _listener)
    {
        if (eventDictionary.ContainsKey(_eventType))
        {
            System.Action result = eventDictionary[_eventType];
            if (result != null)
            {
                result -= _listener;
            }
            else
            {
                Debug.LogWarning("Somehting went wrong with event: " + _eventType);
            }
        }
    }

    public static void Invoke(UnityEngine.EventType _eventType)
    {
        if (eventDictionary.ContainsKey(_eventType))
        {
            eventDictionary[_eventType]?.Invoke();
        }
    }


}

public static class EventManagerValentijn<T>
{
    private static Dictionary<UnityEngine.EventType, System.Action<T>> eventDictionary = new Dictionary<UnityEngine.EventType, System.Action<T>>();

    public static void Register(UnityEngine.EventType _eventType, System.Action<T> _listener)
    {
        if (!eventDictionary.ContainsKey(_eventType))
        {
            eventDictionary.Add(_eventType, null);
        }
        eventDictionary[_eventType] += _listener;
    }

    public static void UnRegister(UnityEngine.EventType _eventType, System.Action<T> _listener)
    {
        if (eventDictionary.ContainsKey(_eventType))
        {
            System.Action<T> result = eventDictionary[_eventType];
            if (result != null)
            {
                result -= _listener;
            }
            else
            {
                Debug.LogWarning("Somehting went wrong with event: " + _eventType);
            }
        }
    }

    public static void Invoke(UnityEngine.EventType _eventType, T _arg1)
    {
        if (eventDictionary.ContainsKey(_eventType))
        {
            eventDictionary[_eventType]?.Invoke(_arg1);
        }
    }
}


//EventManager.Register(EventType.ON_PLAYER_HIT, DoSomethingCool);
//EventManager.UnRegister(EventType.ON_PLAYER_HIT, DoSomethingCool);
//EventManager.Invoke(EventType.ON_PLAYER_HIT);