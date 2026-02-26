using System;
using NUnit.Framework;
using Reflex.Core;
using Shintio.DependencyInjection.Abstractions;
using Shintio.DependencyInjection.Adapter.Unity.Reflex;
using Shintio.DependencyInjection.Tests.Common;

namespace Shintio.DependencyInjection.Tests.Unity
{
	[TestFixture]
	public sealed class ReflexTests : AdapterTestsBase
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
			var container = _builder.Build();
			_builder = new ContainerBuilder();

			return new ServiceProviderProxy(container.Resolve);
		}
	}
}
