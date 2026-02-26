using System;
using Reflex.Core;
using Shintio.DependencyInjection.Abstractions;

namespace Shintio.DependencyInjection.Adapter.Unity.Reflex
{
	public static class ContainerBuilderExtensions
	{
		public static ContainerBuilder AddRegistrar(this ContainerBuilder builder, Action<IServiceRegistrar> configure)
		{
			var registrar = new ServiceRegistrar(builder);

			configure.Invoke(registrar);

			return builder;
		}
	}
}
