using System;
using Shintio.DependencyInjection.Abstractions;
using SimpleInjector;

namespace Shintio.DependencyInjection.Adapter.SimpleInjector;

public class ServiceRegistrar : IServiceRegistrar
{
	private readonly Container _container;

	public ServiceRegistrar(Container container)
	{
		_container = container;
	}

	public IServiceRegistrar AddTransient(Type serviceType, Type implementationType)
	{
		_container.Register(serviceType, implementationType, Lifestyle.Transient);

		return this;
	}

	public IServiceRegistrar AddTransient<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
		where TService : class
	{
		_container.Register(
			typeof(TService),
			() => implementationFactory.Invoke(new ServiceProviderProxy(GetService)),
			Lifestyle.Transient
		);

		return this;
	}

	public IServiceRegistrar AddScoped(Type serviceType, Type implementationType)
	{
		_container.Register(serviceType, implementationType, Lifestyle.Scoped);

		return this;
	}

	public IServiceRegistrar AddScoped<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
		where TService : class
	{
		_container.Register(
			typeof(TService),
			() => implementationFactory.Invoke(new ServiceProviderProxy(GetService)),
			Lifestyle.Scoped
		);

		return this;
	}

	public IServiceRegistrar AddSingleton(Type serviceType, Type implementationType)
	{
		_container.Register(serviceType, implementationType, Lifestyle.Singleton);

		return this;
	}

	public IServiceRegistrar AddSingleton<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
		where TService : class
	{
		_container.Register(
			typeof(TService),
			() => implementationFactory.Invoke(new ServiceProviderProxy(GetService)),
			Lifestyle.Singleton
		);

		return this;
	}

	public IServiceRegistrar AddSingleton(Type serviceType, object implementationInstance)
	{
		_container.RegisterInstance(serviceType, implementationInstance);

		return this;
	}

	private object? GetService(Type serviceType)
	{
		try
		{
			return _container.GetInstance(serviceType);
		}
		catch (ActivationException)
		{
			return null;
		}
	}
}
