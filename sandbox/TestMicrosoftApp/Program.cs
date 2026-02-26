using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shintio.DependencyInjection.Adapter.Microsoft;
using TestLib;

var appBuilder = Host.CreateApplicationBuilder();

appBuilder.Services
	.AddRegistrar(b => b.AddTestLib());

var app = appBuilder.Build();

var testService = app.Services.GetRequiredService<TestService>();

Console.WriteLine("Hello from Microsoft DI");
Console.WriteLine(testService.TestMethod());

// await app.RunAsync();