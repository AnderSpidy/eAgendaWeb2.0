using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.webapi.ViewModels.ModuloContato;

namespace eAgenda.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ContatoController : eAgendaControllerBase
    {
        private readonly ServicoContato servicoContato;
        private readonly IMapper mapeadorContatos;

        public ContatoController(ServicoContato servicoContato, IMapper mapeadorContatos)
        {
            this.servicoContato = servicoContato;
            this.mapeadorContatos = mapeadorContatos;

        }
        //Action, Ação do Controlador, endpoint
        [HttpGet]
        public ActionResult<List<ListarContatoViewModel>> SelecionarTodos()
        {
            var contatoResult = servicoContato.SelecionarTodos();

            if (contatoResult.IsFailed)
                return InternalError(contatoResult);


            return Ok(new
            {
                sucesso = true,
                dados = mapeadorContatos.Map<List<ListarContatoViewModel>>(contatoResult.Value)
            });
        }



        [HttpGet("visualizar-completa/{id:guid}")]
        public ActionResult<VisualizarContatoViewModel> SelecionarCompletaPorId(Guid id)
        {
            var contatoResult = servicoContato.SelecionarPorId(id);

            if (contatoResult.IsFailed && RegistroNãoEncontrado(contatoResult))
                return NotFound(contatoResult);


            if (contatoResult.IsFailed)
                return InternalError(contatoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorContatos.Map<VisualizarContatoViewModel>(contatoResult.Value)
                //mensagem = "Dados encontrados com Sucesso!",
                //total = tarefaResult.Value.Count 
            });
        }



        [HttpPost]
        public ActionResult<FormsContatoViewModel> Inserir(InserirContatoViewModel novoContatoVM)
        {

            var contato = mapeadorContatos.Map<Contato>(novoContatoVM);

            var contatoResult = servicoContato.Inserir(contato);

            if (contatoResult.IsFailed)
                return InternalError(contatoResult);

            return Ok(new
            {
                sucesso = true,
                dados = novoContatoVM
            });

        }

        [HttpPut("{id:guid}")]
        public ActionResult<FormsContatoViewModel> Editar(Guid id, EditarContatoViewModel contatoVM)
        {

            var contatoResult = servicoContato.SelecionarPorId(id);

            if (contatoResult.IsFailed && RegistroNãoEncontrado(contatoResult))
                return NotFound(contatoResult);

            var contato = mapeadorContatos.Map(contatoVM, contatoResult.Value);

            contatoResult = servicoContato.Editar(contato);

            if (contatoResult.IsFailed)
                return InternalError(contatoResult);

            return Ok(new
            {
                sucesso = true,
                dados = contatoVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {

            var contatoResult = servicoContato.Excluir(id);

            if (contatoResult.IsFailed && RegistroNãoEncontrado<Contato>(contatoResult))
                return NotFound(contatoResult);

            if (contatoResult.IsFailed)
                return InternalError<Contato>(contatoResult);//como nesse caso este metodo só retorna um tipo ActionResult sem tipagem, nos devemos Tipar o InternalError


            //Caso de tudo certo, não precisa retornar nada
            return NoContent();
        }
    }
}
