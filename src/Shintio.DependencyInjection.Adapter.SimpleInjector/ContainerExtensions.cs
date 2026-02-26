using System;
using Shintio.DependencyInjection.Abstractions;
using SimpleInjector;

namespace Shintio.DependencyInjection.Adapter.SimpleInjector;

public static class ContainerExtensions
{
	public static Container AddRegistrar(this Container container, Action<IServiceRegistrar> configure)
	{
		var registrar = new ServiceRegistrar(container);

		configure.Invoke(registrar);

		return container;
	}
}
