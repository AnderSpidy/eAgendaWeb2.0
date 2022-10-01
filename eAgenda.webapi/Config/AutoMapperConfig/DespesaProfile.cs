using AutoMapper;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.webapi.Config.AutoMapperConfig.ModuloCompartilhado;
using eAgenda.webapi.ViewModels.ModuloDespesa;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace eAgenda.webapi.Config.AutoMapperConfig
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            //-----------------------------------------------------------------------------------------------------------------------------
            //da Entidade para o ViewModel
            ConverterDeEntidadeParaViewModel();
            //-----------------------------------------------------------------------------------------------------------------------------
            //do ViewModela para Entidade
            ConverterDeViewModelParaEntidade();
        }

        private void ConverterDeViewModelParaEntidade()
        {
            CreateMap<FormsDespesaViewModel, Despesa>()
                 .ForMember(destino => destino.UsuarioId, opt => opt.MapFrom<UsuarioResolver>())
                 .AfterMap<ConfigurarCategoriasMappingAction>();
        }

        private void ConverterDeEntidadeParaViewModel()
        {
            CreateMap<Despesa, ListarDespesaViewModel>()
                .ForMember(destino => destino.FormaPagamento, opt => opt.MapFrom(origem => origem.FormaPagamento.GetDescription()));

            CreateMap<Despesa, VisualizarDespesaViewModel>()
                .ForMember(destino => destino.FormaPagamento, opt => opt.MapFrom(origem => origem.FormaPagamento.GetDescription()))
                .ForMember(destino => destino.Categorias, opt =>
                    opt.MapFrom(origem => origem.Categorias.Select(x => x.Titulo)));
        }
    }

    public class ConfigurarCategoriasMappingAction : IMappingAction<FormsDespesaViewModel, Despesa>
    {
        private readonly IRepositorioCategoria repositorioCategoria;

        public ConfigurarCategoriasMappingAction(IRepositorioCategoria repositorioCategoria)
        {
            this.repositorioCategoria = repositorioCategoria;
        }

        public void Process(FormsDespesaViewModel despesaVM, Despesa despesa, ResolutionContext context)
        {
            foreach (var categoriaVM in despesaVM.CategoriasSelecionadas)
            {
                var categoria = repositorioCategoria.SelecionarPorId(categoriaVM.Id);

                if (categoriaVM.Selecionada)
                    despesa.AtribuirCategoria(categoria);
                else
                    despesa.RemoverCategoria(categoria);
            }
        }
    }
}
