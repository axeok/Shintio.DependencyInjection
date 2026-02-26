using System;

namespace Shintio.DependencyInjection.Abstractions
{
	public readonly struct ServiceProviderProxy
	{
		private readonly Func<Type, object?> _getServiceFunc;

		public ServiceProviderProxy(Func<Type, object?> getServiceFunc)
		{
			_getServiceFunc = getServiceFunc;
		}

		public object? GetService(Type serviceType)
		{
			return _getServiceFunc.Invoke(serviceType);
		}
	}
}