using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Realeyes.Infrastructure.IOCs
{
    public static class Boostrap
    {
        private static Assembly[] assemblies;

        public static void Populate(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            assemblies = GetAssemblies();
            services.AddMediatR(assemblies);
            builder.Populate(services);
            //    builder.RegisterAssemblyModules(assemblies);

        }

        public static void RegisterAssemblyModulesen(ContainerBuilder builder)
        {
            builder.RegisterAssemblyModules(assemblies);

        }
        public static Assembly[] GetAssemblies()
        {
            var result = new List<Assembly>();
            var loadedAssemblies = new HashSet<string>();
            var assembliesToCheck = new Queue<Assembly>();

            assembliesToCheck.Enqueue(Assembly.GetEntryAssembly());

            while (assembliesToCheck.Any())
            {

                var assemblyToCheck = assembliesToCheck.Dequeue();
                foreach (var reference in assemblyToCheck
                    .GetReferencedAssemblies()
                    .Where(a => a.FullName.Contains("Realeyes", StringComparison.OrdinalIgnoreCase)))
                {
                    if (!loadedAssemblies.Contains(reference.FullName))
                    {
                        var assembly = Assembly.Load(reference);
                        assembliesToCheck.Enqueue(assembly);
                        loadedAssemblies.Add(reference.FullName);
                        result.Add(assembly);
                    }
                }
            }

            return result.ToArray();
        }
    }
}
