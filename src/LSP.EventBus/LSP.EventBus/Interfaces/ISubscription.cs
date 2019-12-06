using LSP.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSP.EventBus.Interfaces
{
    public interface ISubscription
    {
        SubscriptionToken SubscriptionToken { get; }

        void Publish(EventBase eventBase);

        void Publish(EventBase eventBase, TimeSpan timeDelay);
    }
}
