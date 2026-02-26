using System;
using Lamar;
using Shintio.DependencyInjection.Abstractions;

namespace Shintio.DependencyInjection.Adapter.Lamar;

public static class ServiceRegistryExtensions
{
	public static ServiceRegistry AddRegistrar(this ServiceRegistry registry, Action<IServiceRegistrar> configure)
	{
		var registrar = new ServiceRegistrar(registry);

		configure.Invoke(registrar);

		return registry;
	}
}
