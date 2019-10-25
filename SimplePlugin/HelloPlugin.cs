using System;
using Contract;

namespace SimplePlugin
{
    public class HelloPlugin : IHelloPlugin
    {
        public string SayHello(string input)
        {
            return $"Hello {input}";
        }
    }

    public class BonjourPlugin : IHelloPlugin
    {
        public string SayHello(string input)
        {
            return $"Bonjour {input}";
        }
    }

    public class AlohaPlugin : IHelloPlugin
    {
        public string SayHello(string input)
        {
            return $"Aloha {input}";
        }
    }
}
