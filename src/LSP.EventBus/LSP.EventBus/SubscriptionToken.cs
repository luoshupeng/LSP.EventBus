using System;
using System.Collections.Generic;
using System.Text;

namespace LSP.EventBus
{
    public class SubscriptionToken
    {
        public Type EventItemType { get; }

        public Guid Token { get; }

        public SubscriptionToken(Type eventItemType)
        {
            Token = Guid.NewGuid();
            EventItemType = eventItemType;
        }
    }
}
