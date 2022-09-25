using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Dominio;
using eAgenda.Infra.Configs;
using eAgenda.Infra.Orm.ModuloTarefa;
using eAgenda.Infra.Orm;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eAgenda.webapi.Config.AutoMapperConfig;
using eAgenda.webapi.Filters;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.Aplicacao.ModuloContato;

namespace eAgenda.webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = true;
            });
            //---------------------------------------------------------------------------------------------

            //Injeção de dependencia do AutoMapper(ou seja, a transição dos dados das propriedades da Tarefa)
            services.AddAutoMapper(config =>
            {
                config.AddProfile<TarefaProfile>();//TAREFA
                config.AddProfile<ContatoProfile>();//CONTATO
            });
            
            //injeção de dependencia Bd e Configuração 
            services.AddSingleton((x) => new ConfiguracaoAplicacaoeAgenda().ConnectionStrings);
            services.AddScoped<IContextoPersistencia, eAgendaDbContext>();
            //Injeção de Repositorio e camada de serviço -TAREFA
            services.AddScoped<IRepositorioTarefa, RepositorioTarefaOrm>();
            services.AddTransient<ServicoTarefa>();
            //Injeção de Repositorio e camada de serviço -CONTATO
            services.AddScoped<IRepositorioContato, RepositorioContatoOrm>();
            services.AddTransient<ServicoContato>();



            //-----------------------------------------------------------------------------------------------------------------------------
            services.AddControllers(config =>
            {
                config.Filters.Add(new ValidarViewModelActionFilter());//é a classe na qual usamos para retornar asmensagens que os protocolos http nos manda (la encapsulamos da forma que prefirirmos)
                //lembrando que poderiamos fazer diferentes ActionFilters para coisas com diferentes padões(Se for fazer, tem o detalhe de colocar os decorators em cima dos metodos que ira usar como escopo)
            });




            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eAgenda.webapi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "eAgenda.webapi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
