using AutoMapper;
using eAgenda.Aplicacao.ModuloCompromisso;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.webapi.ViewModels.ModuloCompromisso;

namespace eAgenda.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CompromissoController : eAgendaControllerBase
    {
        private readonly ServicoCompromisso servicoCompromisso;
        private readonly IMapper mapeadorCompromisso;

        public CompromissoController(ServicoCompromisso servicoCompromisso, IMapper mapeadorCompromisso)
        {
            this.servicoCompromisso = servicoCompromisso;
            this.mapeadorCompromisso = mapeadorCompromisso;
        }

        //Action, Ação do Controlador, endpoint
        [HttpGet]
        public ActionResult<List<ListarCompromissoViewModel>> SelecionarTodos()
        {
            var compromissoResult = servicoCompromisso.SelecionarTodos();

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);


            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCompromisso.Map<List<ListarCompromissoViewModel>>(compromissoResult.Value)
            });
        }



        [HttpGet("visualizar-completa/{id:guid}")]
        public ActionResult<VisualizarCompromissoViewModel> SelecionarCompletaPorId(Guid id)
        {
            var compromissoResult = servicoCompromisso.SelecionarPorId(id);

            if (compromissoResult.IsFailed && RegistroNãoEncontrado(compromissoResult))
                return NotFound(compromissoResult);


            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCompromisso.Map<VisualizarCompromissoViewModel>(compromissoResult.Value)
                //mensagem = "Dados encontrados com Sucesso!",
                //total = tarefaResult.Value.Count 
            });
        }
        [HttpGet, Route("entre/{dataInicial:datetime}/{dataFinal:datetime}")]
        public ActionResult<List<ListarCompromissoViewModel>> SelecionarCompromissosFuturos(DateTime dataInicial, DateTime dataFinal)
        {
            var compromissoResult = servicoCompromisso.SelecionarCompromissosFuturos(dataInicial, dataFinal);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCompromisso.Map<List<ListarCompromissoViewModel>>(compromissoResult.Value)
            });
        }

        [HttpGet, Route("passados/{dataAtual:datetime}")]
        public ActionResult<List<ListarCompromissoViewModel>> SelecionarCompromissosPassados(DateTime dataAtual)
        {
            var compromissoResult = servicoCompromisso.SelecionarCompromissosPassados(dataAtual);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCompromisso.Map<List<ListarCompromissoViewModel>>(compromissoResult.Value)
            });
        }



        [HttpPost]
        public ActionResult<FormsCompromissoViewModel> Inserir(InserirCompromissoViewModel novoCompromissoVM)
        {

            var compromisso = mapeadorCompromisso.Map<Compromisso>(novoCompromissoVM);

            var compromissoResult = servicoCompromisso.Inserir(compromisso);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = novoCompromissoVM
            });
        }

        [HttpPut("{id:guid}")]
        public ActionResult<FormsCompromissoViewModel> Editar(Guid id, EditarCompromissoViewModel compromissoVM)
        {

            var compromissoResult = servicoCompromisso.SelecionarPorId(id);

            if (compromissoResult.IsFailed && RegistroNãoEncontrado(compromissoResult))
                return NotFound(compromissoResult);

            var compromisso = mapeadorCompromisso.Map(compromissoVM, compromissoResult.Value);

            compromissoResult = servicoCompromisso.Editar(compromisso);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = compromissoVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {

            var compromissoResult = servicoCompromisso.Excluir(id);

            if (compromissoResult.IsFailed && RegistroNãoEncontrado<Compromisso>(compromissoResult))
                return NotFound(compromissoResult);

            if (compromissoResult.IsFailed)
                return InternalError<Compromisso>(compromissoResult);//como nesse caso este metodo só retorna um tipo ActionResult sem tipagem, nos devemos Tipar o InternalError


            //Caso de tudo certo, não precisa retornar nada
            return NoContent();
        }
    }
}
