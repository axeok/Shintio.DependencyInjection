using System;
using Autofac;
using Shintio.DependencyInjection.Abstractions;

namespace Shintio.DependencyInjection.Adapter.Autofac;

public static class ContainerBuilderExtensions
{
	public static ContainerBuilder AddRegistrar(this ContainerBuilder builder, Action<IServiceRegistrar> configure)
	{
		var registrar = new ServiceRegistrar(builder);

		configure.Invoke(registrar);

		return builder;
	}
}
