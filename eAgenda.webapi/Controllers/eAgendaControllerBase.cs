using eAgenda.webapi.ViewModels.Autenticacao;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace eAgenda.webapi.Controllers
{
    [ApiController]
    public abstract class eAgendaControllerBase : ControllerBase
    {
        //Refatorando para utilizar os tokens autenticados 
        private UsuarioTokenViewModel usuario;// essa propriedade serve para que depois das verificações se o usuario esta autenticado, nos possamos encapsular nessa propriedade para passar adiante o usuario Autenticado ou não

        public UsuarioTokenViewModel UsuarioLogado //Não podemos mandar esta configuração dentro do contrutor da classe Base, pois os dados precisam estar disponiveis em tempo de execução e dentro do contrutor, nós ainda não chegamos a ter a classe para que possamos usar o método para conseguir os dados do Token
        {
            get
            {
                //esta autenticado é a primeira parte do antigo metodo ObtemId
                if (EstaAutenticado())
                {
                    usuario = new UsuarioTokenViewModel();
                    //pegando o Id do token
                    var id = Request?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    //apenas um filtro para nao alojar propriedades nulas
                    if (!string.IsNullOrEmpty(id))
                        usuario.Id = Guid.Parse(id);
                    //Pegando o nome do token
                    var nome = Request?.HttpContext?.User?.FindFirst(ClaimTypes.GivenName)?.Value;
                    //apenas um filtro para nao alojar propriedades nulas
                    if (!string.IsNullOrEmpty(nome))
                        usuario.Nome = nome;
                    //pegando o email do token
                    var email = Request.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
                    //apenas um filtro para nao alojar propriedades nulas
                    if (!string.IsNullOrEmpty(email))
                        usuario.Email = email;

                    return usuario;
                }
            
                return null;
            }
        }
       
        protected ActionResult InternalError<T>(Result<T> registroResult)
        {
            return StatusCode(500, new
            {
                sucesso = false,
                erros = registroResult.Errors.Select(x => x.Message) //wrapper - ActionResult "encaplsula o método"
                                                                     //mensagem = "Erro interno no servidor!",
                                                                     //total = 0
            });
        }
        protected ActionResult BadRequest<T>(Result<T> registroResult)
        {
            return StatusCode(300, new
            {
                sucesso = false,
                erros = registroResult.Errors.Select(x => x.Message)
            });
        }
        protected ActionResult NotFound<T>(Result<T> registroResult)
        {
            return StatusCode(404, new
            {
                sucesso = false,
                erros = registroResult.Errors.Select(x => x.Message)
            });
        }

        protected static bool RegistroNãoEncontrado<T>(Result<T> registroResult)
        {
            return registroResult.Errors.Any(x => x.Message.Contains(" - não encontrada"));
        }

        private bool EstaAutenticado()
        {
            //Autentica o token filtrando se está valido ou não, caso não entra no get do UsuarioLogado
            if (Request?.HttpContext?.User?.Identity != null)
                return true;
            return false;
        }

    }
}
