using System;

namespace Shintio.DependencyInjection.Abstractions
{
	public static class ServiceRegistrarExtensions
	{
		#region Transient

		public static IServiceRegistrar AddTransient<TService, TImplementation>(this IServiceRegistrar registrar)
			where TService : class
			where TImplementation : class, TService
		{
			return registrar.AddTransient(typeof(TService), typeof(TImplementation));
		}

		public static IServiceRegistrar AddTransient(this IServiceRegistrar registrar, Type serviceType)
		{
			return registrar.AddTransient(serviceType, serviceType);
		}

		public static IServiceRegistrar AddTransient<TService>(this IServiceRegistrar registrar)
			where TService : class
		{
			return registrar.AddTransient(typeof(TService));
		}

		#endregion

		#region Scoped

		public static IServiceRegistrar AddScoped<TService, TImplementation>(this IServiceRegistrar registrar)
			where TService : class
			where TImplementation : class, TService
		{
			return registrar.AddScoped(typeof(TService), typeof(TImplementation));
		}

		public static IServiceRegistrar AddScoped(this IServiceRegistrar registrar, Type serviceType)
		{
			return registrar.AddScoped(serviceType, serviceType);
		}

		public static IServiceRegistrar AddScoped<TService>(this IServiceRegistrar registrar)
			where TService : class
		{
			return registrar.AddScoped(typeof(TService));
		}

		#endregion

		#region Singleton

		public static IServiceRegistrar AddSingleton<TService, TImplementation>(this IServiceRegistrar registrar)
			where TService : class
			where TImplementation : class, TService
		{
			return registrar.AddSingleton(typeof(TService), typeof(TImplementation));
		}

		public static IServiceRegistrar AddSingleton(this IServiceRegistrar registrar, Type serviceType)
		{
			return registrar.AddSingleton(serviceType, serviceType);
		}

		public static IServiceRegistrar AddSingleton<TService>(this IServiceRegistrar registrar)
			where TService : class
		{
			return registrar.AddSingleton(typeof(TService));
		}

		public static IServiceRegistrar AddSingleton<TService>(
			this IServiceRegistrar registrar,
			TService implementationInstance
		)
			where TService : class
		{
			return registrar.AddSingleton(typeof(TService), implementationInstance);
		}

		#endregion
	}
}