﻿using LSP.EventBus.Events;
using LSP.EventBus.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LSP.EventBus
{
    internal class Subscription<TEventBase> : ISubscription where TEventBase : EventBase
    {
        private readonly Action<TEventBase> _action;
        public SubscriptionToken SubscriptionToken { get; }

        public Subscription(Action<TEventBase> action, SubscriptionToken subscriptionToken)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            SubscriptionToken = subscriptionToken ?? throw new ArgumentNullException(nameof(subscriptionToken));
        }

        public void Publish(EventBase eventItem)
        {
            if (!(eventItem is TEventBase))
                throw new ArgumentException("Event Item is not the correct type.");

            _action.Invoke(eventItem as TEventBase);
        }

        public void Publish(EventBase eventItem, TimeSpan timeDelay)
        {
            if (!(eventItem is TEventBase))
                throw new ArgumentException("Event Item is not the correct type.");

            int dispatchDelayMs = (int)timeDelay.TotalMilliseconds;

            if (dispatchDelayMs > 0)
            {
#if NET40
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        Thread.Sleep(dispatchDelayMs);
                    }
                    catch (Exception e)
                    {
                    }
                    _action.Invoke(eventItem as TEventBase);
                });
#else
                Task.Delay(dispatchDelayMs).ContinueWith(task => _action.Invoke(eventItem as TEventBase));
#endif
            }
            else
            {
#if NET40
                Task.Factory.StartNew(() =>
                {
                    _action.Invoke(eventItem as TEventBase);
                });
#else
                Task.Run(() => _action.Invoke(eventItem as TEventBase));
#endif
            }
        }
    }
}
