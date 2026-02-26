using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shintio.DependencyInjection.Abstractions;
using Shintio.DependencyInjection.Adapter.Microsoft;

namespace Shintio.DependencyInjection.Tests;

public class MicrosoftTests : AdapterTestsBase
{
	private readonly HostApplicationBuilder _builder;

	public MicrosoftTests()
	{
		_builder = Host.CreateApplicationBuilder();
	}

	protected override void RegisterServices(Action<IServiceRegistrar> builder)
	{
		_builder.Services.AddRegistrar(builder);
	}

	protected override ServiceProviderProxy BuildProvider()
	{
		var app = _builder.Build();
		var scope = app.Services.CreateScope();

		return new ServiceProviderProxy(scope.ServiceProvider.GetService);
	}
}
