using LSP.EventBus.Events;
using LSP.EventBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSP.EventBus
{
    internal class EventBus : IEventBus
    {
        private readonly Dictionary<string, Dictionary<Type, List<ISubscription>>> _subscriptions;
        private static readonly object SubscriptionsLock = new object();

        public EventBus()
        {
            _subscriptions = new Dictionary<string, Dictionary<Type, List<ISubscription>>>();
        }

        public void Publish<TEventBase>(string topic, TEventBase eventItem) where TEventBase : EventBase
        {
            if (eventItem == null)
                throw new ArgumentNullException(nameof(eventItem));

            var allSubscriptions = new List<ISubscription>();
            lock (SubscriptionsLock)
            {
                if (_subscriptions.ContainsKey(topic))
                {
                    var _tmpDict = _subscriptions[topic];
                    if (_tmpDict.ContainsKey(typeof(TEventBase)))
                    {
                        allSubscriptions = _tmpDict[typeof(TEventBase)].ToList();
                    }
                }
            }
            allSubscriptions.ForEach(subscription =>
            {
                subscription.Publish(eventItem);
            });
        }

        public void Publish<TEventBase>(string topic, TEventBase eventItem, TimeSpan dispatchDelay) where TEventBase : EventBase
        {
            if (eventItem == null)
                throw new ArgumentNullException(nameof(eventItem));

            var allSubscriptions = new List<ISubscription>();
            lock (SubscriptionsLock)
            {
                if (_subscriptions.ContainsKey(topic))
                {
                    var _tmpDict = _subscriptions[topic];
                    if (_tmpDict.ContainsKey(typeof(TEventBase)))
                    {
                        allSubscriptions = _tmpDict[typeof(TEventBase)].ToList();
                    }
                }
            }
            allSubscriptions.ForEach(subscription =>
            {
                subscription.Publish(eventItem, dispatchDelay);
            });
        }

        public SubscriptionToken Subscribe<TEventBase>(string topic, Action<TEventBase> action) where TEventBase : EventBase
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            lock (SubscriptionsLock)
            {
                if (!_subscriptions.ContainsKey(topic))
                {
                    Dictionary<Type, List<ISubscription>> tmpDict = new Dictionary<Type, List<ISubscription>>();
                    tmpDict.Add(typeof(TEventBase), new List<ISubscription>());
                    _subscriptions.Add(topic, tmpDict);
                }

                var token = new SubscriptionToken(typeof(TEventBase));
                _subscriptions[topic][typeof(TEventBase)].Add(new Subscription<TEventBase>(action, token));
                return token;
            }
        }

        public void Unsubscribe(SubscriptionToken token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            lock (SubscriptionsLock)
            {
                foreach (var item in _subscriptions.Values)
                {
                    if (item.ContainsKey(token.EventItemType))
                    {
                        List<ISubscription> list = item[token.EventItemType];
                        ISubscription subscriptionToRemove = list.FirstOrDefault(x => x.SubscriptionToken.Token == token.Token);
                        if (subscriptionToRemove != null)
                        {
                            item[token.EventItemType].Remove(subscriptionToRemove);
                            break;
                        }
                    }
                }
            }
        }
    }
}
