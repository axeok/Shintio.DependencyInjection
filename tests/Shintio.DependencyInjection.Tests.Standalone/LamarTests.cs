using System;
using Lamar;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Shintio.DependencyInjection.Abstractions;
using Shintio.DependencyInjection.Adapter.Lamar;

namespace Shintio.DependencyInjection.Tests.Standalone;

[TestFixture]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public class LamarTests : AdapterTestsBase
{
    private readonly ServiceRegistry _serviceRegistry;

    public LamarTests()
    {
        _serviceRegistry = new ServiceRegistry();
    }

    protected override void RegisterServices(Action<IServiceRegistrar> builder)
    {
        _serviceRegistry.AddRegistrar(builder);
    }

    protected override ServiceProviderProxy BuildProvider()
    {
        var provider = _serviceRegistry.BuildServiceProvider();
        var scope = provider.CreateScope();

        return new ServiceProviderProxy(scope.ServiceProvider.GetService);
    }
}