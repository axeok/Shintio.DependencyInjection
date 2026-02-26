using System;
using Shintio.DependencyInjection.Adapter.Slim;
using Shintio.DependencyInjection.Container.Slim.Common;
using Shintio.DependencyInjection.Container.Slim.Extensions;
using TestLib;

var services = new ServiceCollection();

services
	.AddRegistrar(b => b.AddTestLib());

var app = services.BuildServiceProvider();

var testService = app.GetRequiredService<TestService>();

Console.WriteLine("Hello from Slim DI");
Console.WriteLine(testService.TestMethod());

// await app.RunAsync();