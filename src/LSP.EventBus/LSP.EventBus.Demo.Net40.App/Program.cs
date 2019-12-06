using LSP.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSP.EventBus.Demo.Net40.App
{
    class Program
    {
        static void Main(string[] args)
        {
            EventBusFactory.DefaultEventBus.Subscribe<PayloadEvent<int>>("default", OnIntEvent);
            EventBusFactory.DefaultEventBus.Subscribe<CustomEvent>("custom", OnCustomEvent);

            EventBusFactory.DefaultEventBus.Publish("default", new PayloadEvent<int>(1234));
            EventBusFactory.DefaultEventBus.Publish("custom", new CustomEvent());

            EventBusFactory.DefaultEventBus.Subscribe<PayloadEvent<string>>("new", s => {
                Console.WriteLine(s.Payload);
            });
            EventBusFactory.DefaultEventBus.Publish("new", new PayloadEvent<string>("this is new message"));

            EventBusFactory.DefaultEventBus.Publish("new", new PayloadEvent<string>("this is async message"), /*TimeSpan.Zero*/TimeSpan.FromSeconds(10));

            Console.ReadKey();
        }

        public static void OnIntEvent(PayloadEvent<int> intEvent)
        {
            Console.WriteLine(intEvent.Payload);
        }

        public static void OnCustomEvent(CustomEvent customEvent)
        {
            Console.WriteLine("Received Custom Event");
        }
    }

    class CustomEvent : EventBase
    {
    }
}
