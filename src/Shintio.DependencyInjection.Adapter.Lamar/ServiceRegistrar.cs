using System;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Shintio.DependencyInjection.Abstractions;

namespace Shintio.DependencyInjection.Adapter.Lamar;

public class ServiceRegistrar : IServiceRegistrar
{
	private readonly ServiceRegistry _serviceRegistry;

	public ServiceRegistrar(ServiceRegistry serviceRegistry)
	{
		_serviceRegistry = serviceRegistry;
	}

	public IServiceRegistrar AddTransient(Type serviceType, Type implementationType)
	{
		_serviceRegistry.AddTransient(serviceType, implementationType);

		return this;
	}

	public IServiceRegistrar AddTransient<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
		where TService : class
	{
		_serviceRegistry.AddTransient<TService>(p =>
			implementationFactory.Invoke(new ServiceProviderProxy(p.GetService))
		);

		return this;
	}

	public IServiceRegistrar AddScoped(Type serviceType, Type implementationType)
	{
		_serviceRegistry.AddScoped(serviceType, implementationType);

		return this;
	}

	public IServiceRegistrar AddScoped<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
		where TService : class
	{
		_serviceRegistry.AddScoped<TService>(p =>
			implementationFactory.Invoke(new ServiceProviderProxy(p.GetService))
		);

		return this;
	}

	public IServiceRegistrar AddSingleton(Type serviceType, Type implementationType)
	{
		_serviceRegistry.AddSingleton(serviceType, implementationType);

		return this;
	}

	public IServiceRegistrar AddSingleton<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
		where TService : class
	{
		_serviceRegistry.AddSingleton<TService>(p =>
			implementationFactory.Invoke(new ServiceProviderProxy(p.GetService))
		);

		return this;
	}

	public IServiceRegistrar AddSingleton(Type serviceType, object implementationInstance)
	{
		_serviceRegistry.AddSingleton(serviceType, implementationInstance);

		return this;
	}
}
