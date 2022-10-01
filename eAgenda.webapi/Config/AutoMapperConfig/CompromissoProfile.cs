using AutoMapper;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.webapi.Config.AutoMapperConfig.ModuloCompartilhado;
using eAgenda.webapi.ViewModels.ModuloCompromisso;
using System;

namespace eAgenda.webapi.Config.AutoMapperConfig
{
    public class CompromissoProfile : Profile
    {
        public CompromissoProfile()
        {
            //-----------------------------------------------------------------------------------------------------------------------------
            //da Entidade para o ViewModel
            ConverterDeEntidadeParaViewModel();
            //-----------------------------------------------------------------------------------------------------------------------------
            //do ViewModela para Entidade
            ConverterDeViewModelParaEntidade();
        }
        private void ConverterDeEntidadeParaViewModel()
        {
            CreateMap<FormsCompromissoViewModel, Compromisso>()
                .ForMember(destino => destino.UsuarioId, opt => opt.MapFrom<UsuarioResolver>());

            CreateMap<Compromisso, ListarCompromissoViewModel>()
                .ForMember(d => d.Data, opt => opt.MapFrom(o => o.Data.ToShortDateString()))
                //.ForMember(d => d.HoraInicio, opt => opt.MapFrom(o => o.HoraInicio.ToString(@"hh:mm:ss")))
                //.ForMember(d => d.HoraTermino, opt => opt.MapFrom(o => o.HoraTermino.ToString(@"hh:mm:ss")))
                .ForMember(d => d.NomeContato, opt => opt.MapFrom(o => o.Contato.Nome));

            CreateMap<Compromisso, VisualizarCompromissoViewModel>();
                

            
                    
        }
        private void ConverterDeViewModelParaEntidade()
        {
            CreateMap<InserirCompromissoViewModel, Compromisso>();
             
            CreateMap<EditarCompromissoViewModel, Compromisso>();
                
        }

       
    }
}
