using LSP.EventBus.Events;
using System;

namespace LSP.EventBus.Demo.Netcore31.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            EventBusFactory.DefaultEventBus.Subscribe<PayloadEvent<int>>("default", OnIntEvent);
            EventBusFactory.DefaultEventBus.Subscribe<CustomEvent>("custom", OnCustomEvent);

            EventBusFactory.DefaultEventBus.Publish("default", new PayloadEvent<int>(1234));
            EventBusFactory.DefaultEventBus.Publish("custom", new CustomEvent());

            EventBusFactory.DefaultEventBus.Subscribe<PayloadEvent<string>>("new", s => {
                Console.WriteLine(s.Payload);
            });
            EventBusFactory.DefaultEventBus.Publish("new", new PayloadEvent<string>("this is new message"));

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
