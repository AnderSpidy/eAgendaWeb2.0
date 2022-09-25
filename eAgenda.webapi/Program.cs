using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using eAgenda.Infra.Logging;
using Serilog;
using System;

namespace eAgenda.webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfiguracaoLogseAgenda.ConfigurarEscritaLogs();

            Log.Logger.Information("Iniciando o servidor da aplicação e-Agenda...");

            try
            {
                CreateHostBuilder(args).Build().Run();
            }catch(Exception exc)
            {
                Log.Logger.Error(exc, "O servidor da aplicação e-Agenda parou inesperadamente.");
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
