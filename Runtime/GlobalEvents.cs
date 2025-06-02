using System;
using System.Collections.Generic;

namespace SimpleEventBus.SimpleEventBus.Runtime
{
    public static class GlobalEvents
    {
        private static readonly Dictionary<Type, Delegate> _eventListeners = new();

        public static void AddListener<T>(Action<T> listener) where T : IEvent
        {
            if (_eventListeners.TryGetValue(typeof(T), out var existingListeners))
                _eventListeners[typeof(T)] = Delegate.Combine(existingListeners, listener);
            else
                _eventListeners[typeof(T)] = listener;
        }

        public static void RemoveListener<T>(Action<T> listener) where T : IEvent
        {
            if (_eventListeners.TryGetValue(typeof(T), out var existingListeners))
            {
                var newDelegate = Delegate.Remove(existingListeners, listener);

                if (newDelegate == null)
                    _eventListeners.Remove(typeof(T));
                else
                    _eventListeners[typeof(T)] = newDelegate;
            }
        }

        public static void Publish<T>(T @event) where T : IEvent
        {
            if (_eventListeners.TryGetValue(typeof(T), out var listeners))
            {
                ((Action<T>)listeners).Invoke(@event);
            }
        }
    }
}