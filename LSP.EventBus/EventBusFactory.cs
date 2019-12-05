using LSP.EventBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSP.EventBus
{
    public class EventBusFactory
    {
        public static IEventBus DefaultEventBus { get; } = new EventBus();
    }
}
