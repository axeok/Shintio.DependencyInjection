using System;
using Shintio.DependencyInjection.Abstractions;
using VContainer;

namespace Shintio.DependencyInjection.Adapter.Unity.VContainer
{
	public class ServiceRegistrar : IServiceRegistrar
	{
		private readonly IContainerBuilder _builder;

		public ServiceRegistrar(IContainerBuilder builder)
		{
			_builder = builder;
		}

		public IServiceRegistrar AddTransient(Type serviceType, Type implementationType)
		{
			_builder.Register(serviceType, implementationType, Lifetime.Transient);

			return this;
		}

		public IServiceRegistrar AddTransient<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
			where TService : class
		{
			_builder.Register<TService>(
				container => implementationFactory.Invoke(new ServiceProviderProxy(t => container.Resolve(t))),
				Lifetime.Transient
			);

			return this;
		}

		public IServiceRegistrar AddScoped(Type serviceType, Type implementationType)
		{
			_builder.Register(serviceType, implementationType, Lifetime.Scoped);

			return this;
		}

		public IServiceRegistrar AddScoped<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
			where TService : class
		{
			_builder.Register<TService>(
				container => implementationFactory.Invoke(new ServiceProviderProxy(t => container.Resolve(t))),
				Lifetime.Scoped
			);

			return this;
		}

		public IServiceRegistrar AddSingleton(Type serviceType, Type implementationType)
		{
			_builder.Register(serviceType, implementationType, Lifetime.Singleton);

			return this;
		}

		public IServiceRegistrar AddSingleton<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
			where TService : class
		{
			_builder.Register<TService>(
				container => implementationFactory.Invoke(new ServiceProviderProxy(t => container.Resolve(t))),
				Lifetime.Singleton
			);

			return this;
		}

		public IServiceRegistrar AddSingleton(Type serviceType, object implementationInstance)
		{
			_builder.RegisterInstance(implementationInstance, serviceType);

			return this;
		}
	}
}
