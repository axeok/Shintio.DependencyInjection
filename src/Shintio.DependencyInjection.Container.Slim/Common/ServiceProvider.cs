using System;
using System.Collections.Generic;
using System.Linq;
using Shintio.DependencyInjection.Container.Slim.Interfaces;

namespace Shintio.DependencyInjection.Container.Slim.Common
{
	public sealed class ServiceProvider
	{
		private readonly IServiceCollection _serviceCollection;
		private readonly Dictionary<Type, object> _singletonServices;
		private readonly Dictionary<Type, object> _scopedServices;

		public ServiceProvider(IServiceCollection serviceCollection)
			: this(serviceCollection, new Dictionary<Type, object>(), new Dictionary<Type, object>())
		{
		}

		private ServiceProvider(
			IServiceCollection serviceCollection,
			Dictionary<Type, object> singletonServices,
			Dictionary<Type, object> scopedServices
		)
		{
			_serviceCollection = serviceCollection;
			_singletonServices = singletonServices;
			_scopedServices = scopedServices;

			_singletonServices[typeof(IServiceCollection)] = _serviceCollection;
			_scopedServices[typeof(ServiceProvider)] = this;
		}

		public IEnumerable<Type> GetAllServicesTypes()
		{
			return _serviceCollection.GetAllServices();
		}

		public object? GetService(Type serviceType)
		{
			var descriptor = _serviceCollection.GetDescriptor(serviceType);
			if (descriptor == null)
			{
				return null;
			}

			switch (descriptor.Lifetime)
			{
				case ServiceLifetime.Transient:
					return CreateService(serviceType, descriptor);
				case ServiceLifetime.Scoped:
					return GetOrCreateFromScope(serviceType, descriptor);
				case ServiceLifetime.Singleton:
					return GetOrCreateFromSingleton(serviceType, descriptor);
				default:
					throw new InvalidOperationException($"Unsupported lifetime '{descriptor.Lifetime}'.");
			}
		}

		public ServiceProvider CreateScope()
		{
			return new ServiceProvider(_serviceCollection, _singletonServices, new Dictionary<Type, object>());
		}

		private object GetOrCreateFromScope(Type serviceType, ServiceDescriptor descriptor)
		{
			if (_scopedServices.TryGetValue(serviceType, out var service))
			{
				return service;
			}

			service = CreateService(serviceType, descriptor);
			_scopedServices[serviceType] = service;

			return service;
		}

		private object GetOrCreateFromSingleton(Type serviceType, ServiceDescriptor descriptor)
		{
			if (_singletonServices.TryGetValue(serviceType, out var service))
			{
				return service;
			}

			service = CreateService(serviceType, descriptor);
			_singletonServices[serviceType] = service;

			return service;
		}

		private object CreateService(Type serviceType, ServiceDescriptor descriptor)
		{
			if (descriptor.ImplementationInstance != null)
			{
				return descriptor.ImplementationInstance;
			}

			if (descriptor.Factory != null)
			{
				return descriptor.Factory.Invoke(this);
			}

			if (descriptor.ImplementationType == null)
			{
				throw new InvalidOperationException($"No implementation configured for {serviceType.Name}.");
			}

			var implementationType = descriptor.ImplementationType;
			var constructors = implementationType.GetConstructors()
				.OrderByDescending(c => c.GetParameters().Length);

			foreach (var constructor in constructors)
			{
				var services = new List<object>();
				foreach (var parameter in constructor.GetParameters())
				{
					var parameterService = GetService(parameter.ParameterType);
					if (parameterService == null)
					{
						break;
					}

					services.Add(parameterService);
				}

				if (services.Count == constructor.GetParameters().Length)
				{
					return constructor.Invoke(services.ToArray());
				}
			}

			throw new Exception($"No constructor found for {serviceType.Name}");
		}
	}
}
