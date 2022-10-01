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

            //Inje��o de dependencia do AutoMapper(ou seja, a transi��o dos dados das propriedades da Tarefa)
            //services.AddAutoMapper(config =>
            //{
            //    config.AddProfile<UsuarioProfile>(); //inclusive o USUARIO

            //    config.AddProfile<TarefaProfile>();//TAREFA
            //    config.AddProfile<ContatoProfile>();//CONTATO
            //});
            //ULTIMA REFATORA��O 27/09/2022 ^^

            services.AddAutoMapper(typeof(Startup));
            //A principio, toda classe de configura��o de dependencia, se estiver herdando de uma classe profile, ela n�o precisa mais ser expecificada na classe Startup

            //inje��o de dependencia Bd e Configura��o 
            services.ConfigurarInjecaoDependencia();

            // Configura��o da Autentica��o N�O ENTENDI COMO FUNCIONA/SERVE 
            services.ConfigurarAutenticacao();

            //CONFIGURA��O DOS FILTROS
            services.ConfigurarFiltros();

            //CONFIGURA��O DO SWAGGER -DOCUMENTA��O
            services.ConfigurarSwagger();
            //---------------------------------------------------------
            //CONFIGURA��O DO JWT "TOKEN"
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
