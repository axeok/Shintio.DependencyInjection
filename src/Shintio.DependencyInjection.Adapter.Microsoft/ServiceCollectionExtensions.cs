using System;
using Microsoft.Extensions.DependencyInjection;
using Shintio.DependencyInjection.Abstractions;

namespace Shintio.DependencyInjection.Adapter.Microsoft;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddRegistrar(this IServiceCollection services, Action<IServiceRegistrar> builder)
	{
		var registrar = new ServiceRegistrar(services);

		builder.Invoke(registrar);

		return services;
	}
}