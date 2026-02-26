using System;
using Shintio.DependencyInjection.Abstractions;
using Shintio.DependencyInjection.Container.Slim.Interfaces;

namespace Shintio.DependencyInjection.Adapter.Slim
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddRegistrar(this IServiceCollection services, Action<IServiceRegistrar> builder)
		{
			var registrar = new ServiceRegistrar(services);

			builder.Invoke(registrar);

			return services;
		}
	}
}