using System;

namespace AppHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Read to say Hello World!");
            var input = Console.ReadLine();

            var loader = new PluginLoader();
            var plugins = loader.LoadPluginsFromAssembly("plugins/SimplePlugin.dll");
            
            foreach (var plugin in plugins)
            {
                Console.WriteLine(plugin.SayHello(input));
            }
            
            Console.WriteLine("Goodbye");
        }
    }
}
