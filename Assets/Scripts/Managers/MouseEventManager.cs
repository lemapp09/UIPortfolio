using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class MouseEventManager
{
    private static MouseEventManager _instance;

    // Dictionary to store event handlers and their types
    private Dictionary<VisualElement, List<Delegate>> _eventHandlers = new Dictionary<VisualElement, List<Delegate>>();

    // Private constructor to prevent instantiation
    private MouseEventManager() { }

    // Public property to get the singleton instance
    public static MouseEventManager Instance => _instance ??= new MouseEventManager();

    // Register event with a source
    public void RegisterEvent<TEventType>(VisualElement element, EventCallback<TEventType> callback) where TEventType : EventBase<TEventType>, new()
    {
        if (!_eventHandlers.ContainsKey(element))
        {
            _eventHandlers[element] = new List<Delegate>();
        }
        _eventHandlers[element].Add(callback);
        element.RegisterCallback(callback);
    }

    // Unregister event with a specific callback
    public void UnregisterEvent<TEventType>(VisualElement element, EventCallback<TEventType> callback) where TEventType : EventBase<TEventType>, new()
    {
        if (_eventHandlers.ContainsKey(element))
        {
            _eventHandlers[element].Remove(callback);
            if (_eventHandlers[element].Count == 0)
            {
                _eventHandlers.Remove(element);
            }
        }
        element.UnregisterCallback(callback);
    }

    // Unregister all events associated with a specific element
    public void UnregisterAllEvents()
    {
        foreach (var kvp in _eventHandlers)
        {
            var element = kvp.Key;
            var handlers = kvp.Value;

            foreach (var handler in handlers)
            {
                // Use reflection to call UnregisterCallback with the correct event type
                var eventType = handler.GetType().GetGenericArguments()[0];
                var unregisterMethod = typeof(CallbackEventHandler).GetMethod("UnregisterCallback", new Type[] { typeof(EventCallback<>).MakeGenericType(eventType), typeof(TrickleDown) });
                if (unregisterMethod != null)
                {
                    unregisterMethod.Invoke(element, new object[] { handler, TrickleDown.NoTrickleDown });
                }
            }
        }

        // Clear the dictionary after unregistering all events
        _eventHandlers.Clear();
    }

    // Check if an element has registered events
    public bool HasRegisteredEvents(VisualElement element)
    {
        return _eventHandlers.ContainsKey(element) && _eventHandlers[element].Count > 0;
    }
}
