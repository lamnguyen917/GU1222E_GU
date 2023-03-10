using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventType
{
    PlayerDie
}

public class NewEventDispatcher : MonoBehaviour
{
    private Dictionary<EventType, UnityEvent> _dictionary = new Dictionary<EventType, UnityEvent>();

    public static NewEventDispatcher Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void AddEventLister(EventType type, UnityAction action)
    {
        if (!_dictionary.ContainsKey(type))
        {
            _dictionary[type] = new UnityEvent();
        }

        _dictionary[type].AddListener(action);
    }

    public void PostEEvent(EventType type)
    {
        if (_dictionary.ContainsKey(type))
        {
            _dictionary[type].Invoke();
        }
    }
}