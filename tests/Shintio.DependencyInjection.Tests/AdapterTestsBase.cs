using System;
using Shintio.DependencyInjection.Abstractions;
using Shintio.DependencyInjection.Tests.TestTypes;
using TestLib;
using Xunit;

namespace Shintio.DependencyInjection.Tests;

public abstract class AdapterTestsBase
{
    protected abstract void RegisterServices(Action<IServiceRegistrar> builder);
    protected abstract ServiceProviderProxy BuildProvider();

    #region Transient

    [Fact]
    public void TestSimpleTransient()
    {
        RegisterServices(b => b.AddTransient<TestConfigForFactory>());

        var provider = BuildProvider();

        var service = provider.GetRequiredService<TestConfigForFactory>();

        Assert.NotNull(service);
    }

    [Fact]
    public void TestTransientFactory()
    {
        RegisterServices(b => b.AddTransient(p => new TestConfigForFactory()
        {
            Message = "From factory",
        }));

        var provider = BuildProvider();

        var service = provider.GetRequiredService<TestConfigForFactory>();

        Assert.NotNull(service);
        Assert.Equal("From factory", service.Message);
    }

    [Fact]
    public void TestFullTransient()
    {
        RegisterServices(b => b
            .AddTransient<TestService>()
            .AddTransient<TestConfigForInstance>()
            .AddTransient(p => new TestConfigForFactory()
            {
                Message = "Hello",
            })
        );

        var provider = BuildProvider();

        var service = provider.GetRequiredService<TestService>();

        Assert.NotNull(service);
        Assert.Equal("Hello Test", service.TestMethod());
    }

    #endregion

    #region Scoped

    [Fact]
    public void TestSimpleScoped()
    {
        RegisterServices(b => b.AddScoped<TestConfigForFactory>());

        var provider = BuildProvider();

        var service = provider.GetRequiredService<TestConfigForFactory>();

        Assert.NotNull(service);
    }

    [Fact]
    public void TestScopedFactory()
    {
        RegisterServices(b => b.AddScoped(p => new TestConfigForFactory()
        {
            Message = "From factory",
        }));

        var provider = BuildProvider();

        var service = provider.GetRequiredService<TestConfigForFactory>();

        Assert.NotNull(service);
        Assert.Equal("From factory", service.Message);
    }

    [Fact]
    public void TestFullScoped()
    {
        RegisterServices(b => b
            .AddScoped<TestService>()
            .AddScoped<TestConfigForInstance>()
            .AddScoped(p => new TestConfigForFactory()
            {
                Message = "Hello",
            })
        );

        var provider = BuildProvider();

        var service = provider.GetRequiredService<TestService>();

        Assert.NotNull(service);
        Assert.Equal("Hello Test", service.TestMethod());
    }

    #endregion

    #region Singleton

    [Fact]
    public void TestSimpleSingleton()
    {
        RegisterServices(b => b.AddSingleton<TestConfigForFactory>());

        var provider = BuildProvider();

        var service = provider.GetRequiredService<TestConfigForFactory>();

        Assert.NotNull(service);
    }

    [Fact]
    public void TestSingletonFactory()
    {
        RegisterServices(b => b.AddSingleton(p => new TestConfigForFactory()
        {
            Message = "From factory",
        }));

        var provider = BuildProvider();

        var service = provider.GetRequiredService<TestConfigForFactory>();

        Assert.NotNull(service);
        Assert.Equal("From factory", service.Message);
    }

    [Fact]
    public void TestSingletonInstance()
    {
        var instance = new TestConfigForInstance()
        {
            Message = "From instance",
        };

        RegisterServices(b => b.AddSingleton(instance));

        var provider = BuildProvider();

        var service = provider.GetRequiredService<TestConfigForInstance>();

        Assert.NotNull(service);
        Assert.Equal("From instance", service.Message);
    }

    [Fact]
    public void TestFullSingleton()
    {
        var instance = new TestConfigForInstance()
        {
            Message = "World",
        };

        RegisterServices(b => b
            .AddSingleton<TestService>()
            .AddSingleton(instance)
            .AddSingleton(p => new TestConfigForFactory()
            {
                Message = "Hello",
            })
        );

        var provider = BuildProvider();

        var service = provider.GetRequiredService<TestService>();

        Assert.NotNull(service);
        Assert.Equal("Hello World", service.TestMethod());
    }

    #endregion
}