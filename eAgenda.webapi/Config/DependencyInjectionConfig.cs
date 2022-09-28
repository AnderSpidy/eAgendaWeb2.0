using eAgenda.Aplicacao.ModuloAutenticacao;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Dominio;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.Infra.Orm.ModuloTarefa;
using eAgenda.Infra.Orm;
using Microsoft.Extensions.DependencyInjection;
using eAgenda.Infra.Configs;

namespace eAgenda.webapi.Config
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigurarInjecaoDependencia(this IServiceCollection services)
        {
            services.AddSingleton((x) => new ConfiguracaoAplicacaoeAgenda().ConnectionStrings);
            services.AddScoped<eAgendaDbContext>(); //tambem nao entendi o porque trazer o escopo de eagendaDBContext
            services.AddScoped<IContextoPersistencia, eAgendaDbContext>();

            //Injeção de Repositorio e camada de serviço -TAREFA
            services.AddScoped<IRepositorioTarefa, RepositorioTarefaOrm>();
            services.AddTransient<ServicoTarefa>();
            //Injeção de Repositorio e camada de serviço -CONTATO
            services.AddScoped<IRepositorioContato, RepositorioContatoOrm>();
            services.AddTransient<ServicoContato>();

            //Injeção Serviço Autenticação
            services.AddTransient<ServicoAutenticacao>();

        }

    }
}
