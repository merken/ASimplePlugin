using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Contract;

namespace AppHost
{
    public class PluginLoader
    {
        internal IHelloPlugin[] LoadPluginsFromAssembly(string assembly)
        {
            var localPath = GetLocalExecutionPath();
            var pluginPath = Path.Combine(localPath, assembly);
            var pluginAssembly = ReadAssemblyFromPath(pluginPath);
            var pluginTypes = GetPlugins(typeof(IHelloPlugin), pluginAssembly);
            return CreateInstances(pluginTypes);
        }

        private IHelloPlugin[] CreateInstances(Type[] types)
        {
            return types.Select(t => Activator.CreateInstance(t) as IHelloPlugin).ToArray();
        }

        private Type[] GetPlugins(Type contract, Assembly pluginAssembly)
        {
            return pluginAssembly.GetTypes()
                .Where(t => t.GetInterfaces()[0].Name == contract.Name).ToArray();
        }

        private Assembly ReadAssemblyFromPath(string assemblyPath)
        {
            return Assembly.Load(File.ReadAllBytes(assemblyPath));
        }

        private string GetLocalExecutionPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}