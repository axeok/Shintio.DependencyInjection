using System;
using Shintio.DependencyInjection.Abstractions;
using VContainer;

namespace Shintio.DependencyInjection.Adapter.Unity.VContainer
{
	public static class ContainerBuilderExtensions
	{
		public static IContainerBuilder AddRegistrar(this IContainerBuilder builder, Action<IServiceRegistrar> configure)
		{
			var registrar = new ServiceRegistrar(builder);

			configure.Invoke(registrar);

			return builder;
		}
	}
}
