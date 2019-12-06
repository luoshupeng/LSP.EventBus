using System;
using System.Collections.Generic;
using System.Text;

namespace LSP.EventBus.Events
{
    public class PayloadEvent<T> : EventBase
    {
        public T Payload { get; protected set; }

        public PayloadEvent(T payload)
        {
            Payload = payload;
        }
    }
}
