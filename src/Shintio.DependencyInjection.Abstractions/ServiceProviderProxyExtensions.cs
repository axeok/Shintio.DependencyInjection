using System;

namespace Shintio.DependencyInjection.Abstractions
{
	public static class ServiceProviderProxyExtensions
	{
		public static TService? GetService<TService>(this ServiceProviderProxy provider)
		{
			return (TService?)provider.GetService(typeof(TService));
		}

		public static object GetRequiredService(this ServiceProviderProxy provider, Type serviceType)
		{
			var service = provider.GetService(serviceType);
			if (service == null)
			{
				throw new InvalidOperationException($"Service of type \"{serviceType.FullName}\" is not registered.");
			}

			return service;
		}

		public static TService GetRequiredService<TService>(this ServiceProviderProxy provider)
		{
			return (TService)provider.GetRequiredService(typeof(TService));
		}
	}
}