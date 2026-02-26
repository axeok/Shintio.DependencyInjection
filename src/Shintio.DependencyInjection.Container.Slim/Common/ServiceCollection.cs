using System;
using System.Collections.Generic;
using Shintio.DependencyInjection.Container.Slim.Interfaces;

namespace Shintio.DependencyInjection.Container.Slim.Common
{
	public class ServiceCollection : IServiceCollection
	{
		private readonly Dictionary<Type, ServiceDescriptor> _descriptors = new Dictionary<Type, ServiceDescriptor>();

		public ServiceCollection()
		{
			AddSingleton(typeof(IServiceCollection), this);
		}

		public IServiceCollection AddTransient(Type serviceType, Type implementationType)
		{
			_descriptors[serviceType] = new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Transient);

			return this;
		}

		public IServiceCollection AddTransient(Type serviceType, Func<ServiceProvider, object> implementationFactory)
		{
			_descriptors[serviceType] = new ServiceDescriptor(serviceType, implementationFactory, ServiceLifetime.Transient);

			return this;
		}

		public IServiceCollection AddScoped(Type serviceType, Type implementationType)
		{
			_descriptors[serviceType] = new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Scoped);

			return this;
		}

		public IServiceCollection AddScoped(Type serviceType, Func<ServiceProvider, object> implementationFactory)
		{
			_descriptors[serviceType] = new ServiceDescriptor(serviceType, implementationFactory, ServiceLifetime.Scoped);

			return this;
		}

		public IServiceCollection AddSingleton<TService, TImplementation>()
			where TService : class
			where TImplementation : class, TService
		{
			return AddSingleton(typeof(TService), typeof(TImplementation));
		}

		public IServiceCollection AddSingleton(Type serviceType, Type implementationType)
		{
			_descriptors[serviceType] = new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Singleton);

			return this;
		}

		public IServiceCollection AddSingleton(Type serviceType, Func<ServiceProvider, object> implementationFactory)
		{
			_descriptors[serviceType] = new ServiceDescriptor(serviceType, implementationFactory, ServiceLifetime.Singleton);

			return this;
		}

		public IServiceCollection AddSingleton(Type serviceType, object implementationInstance)
		{
			_descriptors[serviceType] = new ServiceDescriptor(serviceType, implementationInstance);

			return this;
		}

		public IServiceCollection AddSingleton<TService>(TService implementation)
			where TService : class
		{
			return AddSingleton(typeof(TService), implementation);
		}

		public IEnumerable<Type> GetAllServices()
		{
			return _descriptors.Keys;
		}

		public ServiceProvider BuildServiceProvider()
		{
			return new ServiceProvider(this);
		}

		public ServiceDescriptor? GetDescriptor(Type serviceType)
		{
			return _descriptors.TryGetValue(serviceType, out var descriptor) ? descriptor : null;
		}
	}
}
