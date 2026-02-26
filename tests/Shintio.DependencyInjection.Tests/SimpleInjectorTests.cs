using System;
using Shintio.DependencyInjection.Abstractions;
using Shintio.DependencyInjection.Adapter.SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Shintio.DependencyInjection.Tests;

public class SimpleInjectorTests : AdapterTestsBase
{
	private readonly global::SimpleInjector.Container _container;
	private global::SimpleInjector.Scope? _scope;

	public SimpleInjectorTests()
	{
		_container = new global::SimpleInjector.Container();
		_container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
	}

	protected override void RegisterServices(Action<IServiceRegistrar> builder)
	{
		_container.AddRegistrar(builder);
	}

	protected override ServiceProviderProxy BuildProvider()
	{
		_scope = AsyncScopedLifestyle.BeginScope(_container);

		return new ServiceProviderProxy(t =>
		{
			try
			{
				return _container.GetInstance(t);
			}
			catch (global::SimpleInjector.ActivationException)
			{
				return null;
			}
		});
	}
}
