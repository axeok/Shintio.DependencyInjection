using System;
using NUnit.Framework;
using Shintio.DependencyInjection.Abstractions;
using Shintio.DependencyInjection.Tests.Common.TestTypes;

namespace Shintio.DependencyInjection.Tests.Common
{
    public abstract class AdapterTestsBase
    {
        protected abstract void RegisterServices(Action<IServiceRegistrar> builder);
        protected abstract ServiceProviderProxy BuildProvider();

        #region Transient

        [Test]
        public void TestSimpleTransient()
        {
            RegisterServices(b => b.AddTransient<TestConfigForFactory>());

            var provider = BuildProvider();

            var service = provider.GetRequiredService<TestConfigForFactory>();

            Assert.NotNull(service);
        }

        [Test]
        public void TestTransientFactory()
        {
            RegisterServices(b => b.AddTransient(p => new TestConfigForFactory
            {
                Message = "From factory",
            }));

            var provider = BuildProvider();

            var service = provider.GetRequiredService<TestConfigForFactory>();

            Assert.NotNull(service);
            Assert.AreEqual("From factory", service.Message);
        }

        [Test]
        public void TestFullTransient()
        {
            RegisterServices(b => b
                .AddTransient<TestService>()
                .AddTransient<TestConfigForInstance>()
                .AddTransient(p => new TestConfigForFactory
                {
                    Message = "Hello",
                })
            );

            var provider = BuildProvider();

            var service = provider.GetRequiredService<TestService>();

            Assert.NotNull(service);
            Assert.AreEqual("Hello Test", service.TestMethod());
        }

        #endregion

        #region Scoped

        [Test]
        public void TestSimpleScoped()
        {
            RegisterServices(b => b.AddScoped<TestConfigForFactory>());

            var provider = BuildProvider();

            var service = provider.GetRequiredService<TestConfigForFactory>();

            Assert.NotNull(service);
        }

        [Test]
        public void TestScopedFactory()
        {
            RegisterServices(b => b.AddScoped(p => new TestConfigForFactory
            {
                Message = "From factory",
            }));

            var provider = BuildProvider();

            var service = provider.GetRequiredService<TestConfigForFactory>();

            Assert.NotNull(service);
            Assert.AreEqual("From factory", service.Message);
        }

        [Test]
        public void TestFullScoped()
        {
            RegisterServices(b => b
                .AddScoped<TestService>()
                .AddScoped<TestConfigForInstance>()
                .AddScoped(p => new TestConfigForFactory
                {
                    Message = "Hello",
                })
            );

            var provider = BuildProvider();

            var service = provider.GetRequiredService<TestService>();

            Assert.NotNull(service);
            Assert.AreEqual("Hello Test", service.TestMethod());
        }

        #endregion

        #region Singleton

        [Test]
        public void TestSimpleSingleton()
        {
            RegisterServices(b => b.AddSingleton<TestConfigForFactory>());

            var provider = BuildProvider();

            var service = provider.GetRequiredService<TestConfigForFactory>();

            Assert.NotNull(service);
        }

        [Test]
        public void TestSingletonFactory()
        {
            RegisterServices(b => b.AddSingleton(p => new TestConfigForFactory
            {
                Message = "From factory",
            }));

            var provider = BuildProvider();

            var service = provider.GetRequiredService<TestConfigForFactory>();

            Assert.NotNull(service);
            Assert.AreEqual("From factory", service.Message);
        }

        [Test]
        public void TestSingletonInstance()
        {
            var instance = new TestConfigForInstance
            {
                Message = "From instance",
            };

            RegisterServices(b => b.AddSingleton(instance));

            var provider = BuildProvider();

            var service = provider.GetRequiredService<TestConfigForInstance>();

            Assert.NotNull(service);
            Assert.AreEqual("From instance", service.Message);
        }

        [Test]
        public void TestFullSingleton()
        {
            var instance = new TestConfigForInstance
            {
                Message = "World",
            };

            RegisterServices(b => b
                .AddSingleton<TestService>()
                .AddSingleton(instance)
                .AddSingleton(p => new TestConfigForFactory
                {
                    Message = "Hello",
                })
            );

            var provider = BuildProvider();

            var service = provider.GetRequiredService<TestService>();

            Assert.NotNull(service);
            Assert.AreEqual("Hello World", service.TestMethod());
        }

        #endregion
    }
}