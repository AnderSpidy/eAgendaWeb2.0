using AutoMapper;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.webapi.Config.AutoMapperConfig.ModuloCompartilhado;
using eAgenda.webapi.ViewModels.ModuloDespesa;
using System;

namespace eAgenda.webapi.Config.AutoMapperConfig
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
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
            CreateMap<FormsCategoriaViewModel, Categoria>()
                .ForMember(destino => destino.UsuarioId, opt => opt.MapFrom<UsuarioResolver>());
        }

        private void ConverterDeEntidadeParaViewModel()
        {
            CreateMap<Categoria, ListarCategoriaViewModel>();

            CreateMap<Categoria, VisualizarCategoriaViewModel>();
        }
    }
}
