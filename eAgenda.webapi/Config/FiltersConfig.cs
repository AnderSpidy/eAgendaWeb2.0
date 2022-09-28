using eAgenda.webapi.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace eAgenda.webapi.Config
{
    public static class FiltersConfig
    {
        public static void ConfigurarFiltros(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.Filters.Add(new ValidarViewModelActionFilter());//é a classe na qual usamos para retornar asmensagens que os protocolos http nos manda (la encapsulamos da forma que prefirirmos)
                //lembrando que poderiamos fazer diferentes ActionFilters para coisas com diferentes padões(Se for fazer, tem o detalhe de colocar os decorators em cima dos metodos que ira usar como escopo)
            });
        }

    }
}
