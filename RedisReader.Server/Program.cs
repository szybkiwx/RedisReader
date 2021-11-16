using System;
using System.Net.Http;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RedisReader.Server;
using RedisReader.Server.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IStoreConnections, ConnectionStore>();
builder.Services.AddSingleton<IStoreConnections, ConnectionStore>();
builder.Services.AddSingleton<IReadFromRedis, DataReader>();
builder.Services.AddSingleton<IManageRedisConnections, ManageRedisConnections>();
builder.Services.AddSingleton<ConnectionStateContainer>();
builder.Services.AddSingleton<KeyStateContainer>();

builder.Services.AddBlazoredModal();
builder.Services.AddBlazorContextMenu();


await builder.Build().RunAsync();