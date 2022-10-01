using AutoMapper;
using eAgenda.Dominio.ModuloAutenticacao;
using eAgenda.webapi.ViewModels.Autenticacao;

namespace eAgenda.webapi.Config.AutoMapperConfig.ModuloCompartilhado
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<RegistrarUsuarioViewModel, Usuario>()
                 .ForMember(destino => destino.EmailConfirmed, opt => opt.MapFrom(origem => true))
                .ForMember(destino => destino.UserName, opt => opt.MapFrom(origem => origem.Email));
        }
    }
}
