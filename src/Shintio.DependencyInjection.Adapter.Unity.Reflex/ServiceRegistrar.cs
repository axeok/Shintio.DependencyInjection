using System;
using Reflex.Core;
using Reflex.Enums;
using Shintio.DependencyInjection.Abstractions;

namespace Shintio.DependencyInjection.Adapter.Unity.Reflex
{
	public class ServiceRegistrar : IServiceRegistrar
	{
		private readonly ContainerBuilder _builder;

		public ServiceRegistrar(ContainerBuilder builder)
		{
			_builder = builder;
		}

		public IServiceRegistrar AddTransient(Type serviceType, Type implementationType)
		{
			_builder.RegisterType(implementationType, new[] { serviceType }, Lifetime.Transient, Resolution.Lazy);

			return this;
		}

		public IServiceRegistrar AddTransient<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
			where TService : class
		{
			_builder.RegisterFactory(
				container => implementationFactory.Invoke(new ServiceProviderProxy(t => container.Resolve(t))),
				typeof(TService),
				new[] { typeof(TService) },
				Lifetime.Transient,
				Resolution.Lazy
			);

			return this;
		}

		public IServiceRegistrar AddScoped(Type serviceType, Type implementationType)
		{
			_builder.RegisterType(implementationType, new[] { serviceType }, Lifetime.Scoped, Resolution.Lazy);

			return this;
		}

		public IServiceRegistrar AddScoped<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
			where TService : class
		{
			_builder.RegisterFactory(
				container => implementationFactory.Invoke(new ServiceProviderProxy(t => container.Resolve(t))),
				typeof(TService),
				new[] { typeof(TService) },
				Lifetime.Scoped,
				Resolution.Lazy
			);

			return this;
		}

		public IServiceRegistrar AddSingleton(Type serviceType, Type implementationType)
		{
			_builder.RegisterType(implementationType, new[] { serviceType }, Lifetime.Singleton, Resolution.Lazy);

			return this;
		}

		public IServiceRegistrar AddSingleton<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
			where TService : class
		{
			_builder.RegisterFactory(
				container => implementationFactory.Invoke(new ServiceProviderProxy(t => container.Resolve(t))),
				typeof(TService),
				new[] { typeof(TService) },
				Lifetime.Singleton,
				Resolution.Lazy
			);

			return this;
		}

		public IServiceRegistrar AddSingleton(Type serviceType, object implementationInstance)
		{
			_builder.RegisterValue(implementationInstance, new[] { serviceType });

			return this;
		}
	}
}
