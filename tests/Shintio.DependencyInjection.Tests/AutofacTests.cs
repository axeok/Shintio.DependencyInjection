using System;
using Autofac;
using Shintio.DependencyInjection.Abstractions;
using Shintio.DependencyInjection.Adapter.Autofac;

namespace Shintio.DependencyInjection.Tests;

public class AutofacTests : AdapterTestsBase
{
	private readonly ContainerBuilder _builder;

	public AutofacTests()
	{
		_builder = new ContainerBuilder();
	}

	protected override void RegisterServices(Action<IServiceRegistrar> builder)
	{
		_builder.AddRegistrar(builder);
	}

	protected override ServiceProviderProxy BuildProvider()
	{
		var container = _builder.Build();
		var scope = container.BeginLifetimeScope();

		return new ServiceProviderProxy(t => scope.ResolveOptional(t));
	}
}
