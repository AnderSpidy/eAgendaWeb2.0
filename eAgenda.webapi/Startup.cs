using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using eAgenda.webapi.Config;

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
            //services.AddAutoMapper(config =>
            //{
            //    config.AddProfile<UsuarioProfile>(); //inclusive o USUARIO

            //    config.AddProfile<TarefaProfile>();//TAREFA
            //    config.AddProfile<ContatoProfile>();//CONTATO
            //});
            //ULTIMA REFATORAÇÃO 27/09/2022 ^^

            services.AddAutoMapper(typeof(Startup));
            //A principio, toda classe de configuração de dependencia, se estiver herdando de uma classe profile, ela não precisa mais ser expecificada na classe Startup

            //injeção de dependencia Bd e Configuração 
            services.ConfigurarInjecaoDependencia();

            // Configuração da Autenticação NÃO ENTENDI COMO FUNCIONA/SERVE 
            services.ConfigurarAutenticacao();

            //CONFIGURAÇÃO DOS FILTROS
            services.ConfigurarFiltros();

            //CONFIGURAÇÃO DO SWAGGER -DOCUMENTAÇÃO
            services.ConfigurarSwagger();
            //---------------------------------------------------------
            //CONFIGURAÇÃO DO JWT "TOKEN"
            services.ConfigurarJWT();
        }
        //This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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


            app.UseAuthentication();

            app.UseAuthorization();


           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

      
        
        
       
       
      
    }
}
