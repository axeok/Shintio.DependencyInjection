using System;
using Microsoft.Extensions.DependencyInjection;
using Shintio.DependencyInjection.Abstractions;

namespace Shintio.DependencyInjection.Adapter.Microsoft;

public class ServiceRegistrar : IServiceRegistrar
{
	private readonly IServiceCollection _serviceCollection;

	public ServiceRegistrar(IServiceCollection serviceCollection)
	{
		_serviceCollection = serviceCollection;
	}

	public IServiceRegistrar AddTransient(Type serviceType, Type implementationType)
	{
		_serviceCollection.AddTransient(serviceType, implementationType);

		return this;
	}

	public IServiceRegistrar AddTransient<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
		where TService : class
	{
		_serviceCollection.AddTransient<TService>(p =>
			implementationFactory.Invoke(new ServiceProviderProxy(p.GetService))
		);

		return this;
	}

	public IServiceRegistrar AddScoped(Type serviceType, Type implementationType)
	{
		_serviceCollection.AddScoped(serviceType, implementationType);

		return this;
	}

	public IServiceRegistrar AddScoped<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
		where TService : class
	{
		_serviceCollection.AddScoped<TService>(p =>
			implementationFactory.Invoke(new ServiceProviderProxy(p.GetService))
		);

		return this;
	}

	public IServiceRegistrar AddSingleton(Type serviceType, Type implementationType)
	{
		_serviceCollection.AddSingleton(serviceType, implementationType);

		return this;
	}

	public IServiceRegistrar AddSingleton<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
		where TService : class
	{
		_serviceCollection.AddSingleton<TService>(p =>
			implementationFactory.Invoke(new ServiceProviderProxy(p.GetService))
		);

		return this;
	}

	public IServiceRegistrar AddSingleton(Type serviceType, object implementationInstance)
	{
		_serviceCollection.AddSingleton(serviceType, implementationInstance);

		return this;
	}
}