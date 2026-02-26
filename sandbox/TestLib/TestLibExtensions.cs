using Shintio.DependencyInjection.Abstractions;

namespace TestLib;

public static class TestLibExtensions
{
	public static IServiceRegistrar AddTestLib(this IServiceRegistrar registrar)
	{
		var config = new TestConfigForInstance
		{
			Message = "World",
		};

		return registrar
			.AddSingleton(config)
			.AddSingleton(_ => new TestConfigForFactory { Message = "Hello" })
			.AddSingleton<TestService>();
	}
}