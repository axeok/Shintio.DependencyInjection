using System;
using Shintio.DependencyInjection.Abstractions;
using Shintio.DependencyInjection.Container.Slim.Interfaces;

namespace Shintio.DependencyInjection.Adapter.Slim
{
	public class ServiceRegistrar : IServiceRegistrar
	{
		private readonly IServiceCollection _serviceCollection;

		public ServiceRegistrar(IServiceCollection serviceCollection)
		{
			_serviceCollection = serviceCollection;
		}

		public IServiceRegistrar AddTransient(Type serviceType, Type implementationType)
		{
			throw new NotImplementedException();
		}

		public IServiceRegistrar AddTransient<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
			where TService : class
		{
			throw new NotImplementedException();
		}

		public IServiceRegistrar AddScoped(Type serviceType, Type implementationType)
		{
			throw new NotImplementedException();
		}

		public IServiceRegistrar AddScoped<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
			where TService : class
		{
			throw new NotImplementedException();
		}

		public IServiceRegistrar AddSingleton(Type serviceType, Type implementationType)
		{
			_serviceCollection.AddSingleton(serviceType, implementationType);

			return this;
		}

		public IServiceRegistrar AddSingleton<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
			where TService : class
		{
			throw new NotImplementedException();
		}

		public IServiceRegistrar AddSingleton(Type serviceType, object implementationInstance)
		{
			_serviceCollection.AddSingleton(serviceType, implementationInstance);

			return this;
		}
	}
}