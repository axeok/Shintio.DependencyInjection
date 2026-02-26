using System;

namespace Shintio.DependencyInjection.Container.Slim.Common
{
	public sealed class ServiceDescriptor
	{
		public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
		{
			ServiceType = serviceType;
			ImplementationType = implementationType;
			Lifetime = lifetime;
		}

		public ServiceDescriptor(Type serviceType, Func<ServiceProvider, object> factory, ServiceLifetime lifetime)
		{
			ServiceType = serviceType;
			Factory = factory;
			Lifetime = lifetime;
		}

		public ServiceDescriptor(Type serviceType, object implementationInstance)
		{
			ServiceType = serviceType;
			ImplementationInstance = implementationInstance;
			Lifetime = ServiceLifetime.Singleton;
		}

		public Type ServiceType { get; }

		public Type? ImplementationType { get; }

		public Func<ServiceProvider, object>? Factory { get; }

		public object? ImplementationInstance { get; }

		public ServiceLifetime Lifetime { get; }
	}
}
