using System;

namespace Shintio.DependencyInjection.Abstractions
{
	public interface IServiceRegistrar
	{
		public IServiceRegistrar AddTransient(Type serviceType, Type implementationType);
		public IServiceRegistrar AddTransient<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
			where TService : class;

		public IServiceRegistrar AddScoped(Type serviceType, Type implementationType);
		public IServiceRegistrar AddScoped<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
			where TService : class;

		public IServiceRegistrar AddSingleton(Type serviceType, Type implementationType);
		public IServiceRegistrar AddSingleton<TService>(Func<ServiceProviderProxy, TService> implementationFactory)
			where TService : class;
		public IServiceRegistrar AddSingleton(Type serviceType, object implementationInstance);
	}
}