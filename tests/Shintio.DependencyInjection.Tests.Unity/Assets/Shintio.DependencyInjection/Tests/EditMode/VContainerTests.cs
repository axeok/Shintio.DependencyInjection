using System;
using NUnit.Framework;
using Shintio.DependencyInjection.Abstractions;
using Shintio.DependencyInjection.Adapter.Unity.VContainer;
using Shintio.DependencyInjection.Tests.Common;
using VContainer;

namespace Shintio.DependencyInjection.Unity.Tests
{
    [TestFixture]
    public sealed class VContainerTests : AdapterTestsBase
    {
        private ContainerBuilder _builder;

        [SetUp]
        public void SetUp()
        {
            _builder = new ContainerBuilder();
        }

        protected override void RegisterServices(Action<IServiceRegistrar> builder)
        {
            _builder.AddRegistrar(builder);
        }

        protected override ServiceProviderProxy BuildProvider()
        {
            var resolver = _builder.Build();
            _builder = new ContainerBuilder();

            return new ServiceProviderProxy(t => resolver.Resolve(t));
        }
    }
}