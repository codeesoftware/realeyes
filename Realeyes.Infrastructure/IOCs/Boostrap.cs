using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace Realeyes.Infrastructure.IOCs
{
    public static class Boostrap
    {
        public static void Populate(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            Assembly[] assemblies = GetAssemblies();

            builder.RegisterAssemblyModules(assemblies);

        }

        public static Assembly[] GetAssemblies()
        {

            HashSet<Assembly> assemblies = new HashSet<Assembly>();

            IReadOnlyList<CompilationLibrary> dependencies = DependencyContext.Default.CompileLibraries;
            foreach (CompilationLibrary library in dependencies)
            {
                if (library.Name.Contains("Realeyes",StringComparison.OrdinalIgnoreCase))
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
            }

            return assemblies.ToArray();
        }
    }
}
