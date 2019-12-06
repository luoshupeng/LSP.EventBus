using LSP.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSP.EventBus.Interfaces
{
    public interface IEventBus
    {
        SubscriptionToken Subscribe<TEventBase>(string topic, Action<TEventBase> action) where TEventBase : EventBase;

        void Unsubscribe(SubscriptionToken token);
 
        void Publish<TEventBase>(string topic, TEventBase eventItem, TimeSpan dispatchDelay) where TEventBase : EventBase;

        void Publish<TEventBase>(string topic, TEventBase eventItem) where TEventBase : EventBase;
    }
}
