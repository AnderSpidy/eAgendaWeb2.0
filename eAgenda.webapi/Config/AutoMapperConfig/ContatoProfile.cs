using AutoMapper;
using eAgenda.Dominio.ModuloContato;
using eAgenda.webapi.Config.AutoMapperConfig.ModuloCompartilhado;
using eAgenda.webapi.ViewModels.ModuloContato;

namespace eAgenda.webapi.Config.AutoMapperConfig
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            ConverterDeEntidadeParaViewModel();

            ConverterDeViewModelParaEntidade();
        }

        private void ConverterDeViewModelParaEntidade()
        {
            //Como contato não tem nenhuma dependência, não precisamos dos "ForMamber" para dar o "caminho" para o Mapper

            CreateMap<InserirContatoViewModel, Contato>();
            CreateMap<EditarContatoViewModel, Contato>();
        }

        private void ConverterDeEntidadeParaViewModel()
        {
            //Como contato não tem nenhuma dependência, não precisamos dos "ForMamber" para dar o "caminho" para o Mapper

            CreateMap<Contato, ListarContatoViewModel>();

            CreateMap<Contato, VisualizarContatoViewModel>();

            CreateMap<FormsContatoViewModel, Contato>()
                .ForMember(destino => destino.UsuarioId, opt => opt.MapFrom<UsuarioResolver>());
        }
    }
}
