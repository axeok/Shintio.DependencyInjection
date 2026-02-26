using System;
using NUnit.Framework;
using Shintio.DependencyInjection.Abstractions;
using Shintio.DependencyInjection.Adapter.Slim;
using Shintio.DependencyInjection.Container.Slim.Common;

namespace Shintio.DependencyInjection.Tests.Standalone;

[TestFixture]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public class SlimTests : AdapterTestsBase
{
    private readonly ServiceCollection _serviceCollection;

    public SlimTests()
    {
        _serviceCollection = new ServiceCollection();
    }

    protected override void RegisterServices(Action<IServiceRegistrar> builder)
    {
        _serviceCollection.AddRegistrar(builder);
    }

    protected override ServiceProviderProxy BuildProvider()
    {
        var provider = _serviceCollection.BuildServiceProvider();

        return new ServiceProviderProxy(provider.GetService);
    }
}