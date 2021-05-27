using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Wikify.Integration
{
    public class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File($"Logs/{nameof(Main)}Log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var serviceProvider = new ServiceCollection()
                .AddLogging(x => x.AddSerilog(dispose: true))
                .BuildServiceProvider();
        }
    }

    public class MwParserClient
    {

    }
}
