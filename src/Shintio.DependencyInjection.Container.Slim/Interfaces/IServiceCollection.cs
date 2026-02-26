using System;
using System.Collections.Generic;
using Shintio.DependencyInjection.Container.Slim.Common;

namespace Shintio.DependencyInjection.Container.Slim.Interfaces
{
	public interface IServiceCollection
	{
		public IServiceCollection AddTransient(Type serviceType, Type implementationType);

		public IServiceCollection AddTransient(Type serviceType, Func<ServiceProvider, object> implementationFactory);

		public IServiceCollection AddScoped(Type serviceType, Type implementationType);

		public IServiceCollection AddScoped(Type serviceType, Func<ServiceProvider, object> implementationFactory);

		public IServiceCollection AddSingleton<TService, TImplementation>()
			where TService : class where TImplementation : class, TService;

		public IServiceCollection AddSingleton(Type serviceType, Type implementationType);

		public IServiceCollection AddSingleton(Type serviceType, Func<ServiceProvider, object> implementationFactory);

		public IServiceCollection AddSingleton(Type serviceType, object implementationInstance);

		public ServiceDescriptor? GetDescriptor(Type serviceType);
		public IEnumerable<Type> GetAllServices();

		public ServiceProvider BuildServiceProvider();
	}
}
