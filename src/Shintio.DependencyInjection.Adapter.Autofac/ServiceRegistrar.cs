using System;
using Autofac;
using Shintio.DependencyInjection.Abstractions;

namespace Shintio.DependencyInjection.Adapter.Autofac;

public class ServiceRegistrar : IServiceRegistrar
{
	private readonly ContainerBuilder _builder;

	public ServiceRegistrar(ContainerBuilder builder)
	{
		_builder = builder;
	}

	public IServiceRegistrar AddTransient(Type serviceType, Type implementationType)
	{
		_builder.RegisterType(implementationType).As(serviceType).InstancePerDependency();

		return this;
	}

	public IServiceRegistrar AddTransient<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
		where TService : class
	{
		_builder.Register(c => implementationFactory.Invoke(new ServiceProviderProxy(t => c.ResolveOptional(t))))
			.As<TService>()
			.InstancePerDependency();

		return this;
	}

	public IServiceRegistrar AddScoped(Type serviceType, Type implementationType)
	{
		_builder.RegisterType(implementationType).As(serviceType).InstancePerLifetimeScope();

		return this;
	}

	public IServiceRegistrar AddScoped<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
		where TService : class
	{
		_builder.Register(c => implementationFactory.Invoke(new ServiceProviderProxy(t => c.ResolveOptional(t))))
			.As<TService>()
			.InstancePerLifetimeScope();

		return this;
	}

	public IServiceRegistrar AddSingleton(Type serviceType, Type implementationType)
	{
		_builder.RegisterType(implementationType).As(serviceType).SingleInstance();

		return this;
	}

	public IServiceRegistrar AddSingleton<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
		where TService : class
	{
		_builder.Register(c => implementationFactory.Invoke(new ServiceProviderProxy(t => c.ResolveOptional(t))))
			.As<TService>()
			.SingleInstance();

		return this;
	}

	public IServiceRegistrar AddSingleton(Type serviceType, object implementationInstance)
	{
		_builder.RegisterInstance(implementationInstance).As(serviceType).SingleInstance();

		return this;
	}
}
