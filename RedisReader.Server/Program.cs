using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RedisReader.Server;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .Build()
    .Run();