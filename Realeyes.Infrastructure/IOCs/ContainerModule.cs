using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyModel;

namespace Realeyes.Infrastructure.IOCs
{
    class ContainerModule: Autofac.Module
    {
        protected override  void Load(ContainerBuilder builder)
        {
            Assembly[] assemblies = GetAssemblies();
            builder.RegisterAssemblyModules(assemblies);
        }
        public  Assembly[] GetAssemblies()
        {

            HashSet<Assembly> assemblies = new HashSet<Assembly>();

            IReadOnlyList<CompilationLibrary> dependencies = DependencyContext.Default.CompileLibraries;
            foreach (CompilationLibrary library in dependencies)
            {
                if (library.Name.ToLower().Contains("Realeyes"))
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
            }

            return assemblies.ToArray();
        }
    }
}
