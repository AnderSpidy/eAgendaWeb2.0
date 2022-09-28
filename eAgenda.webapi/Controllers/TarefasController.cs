
using AutoMapper;
using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.webapi.ViewModels.Tarefa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace eAgenda.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TarefasController : eAgendaControllerBase
    {
        private readonly ServicoTarefa servicoTarefa;
        private readonly IMapper mapeadorTarefas;

        public TarefasController(ServicoTarefa servicoTarefa, IMapper mapeadorTarefas)
        {
            this.servicoTarefa = servicoTarefa;
            this.mapeadorTarefas = mapeadorTarefas;

        }
        //Action, Ação do Controlador, endpoint
        [HttpGet]
        public ActionResult<List<ListarTarefaViewModel>> SelecionarTodos()
        {

            //O método obtemId foi refatorado para eAgendaControllerBase na qual trazemos o UsuarioLogado "tratado"
            var tarefaResult = servicoTarefa.SelecionarTodos(StatusTarefaEnum.Todos, UsuarioLogado.Id);

            if (tarefaResult.IsFailed)
                return InternalError(tarefaResult);


            return Ok(new
            {
                sucesso = true,
                dados = mapeadorTarefas.Map<List<ListarTarefaViewModel>>(tarefaResult.Value)
                //mensagem = "Dados encontrados com Sucesso!",
                //total = tarefaResult.Value.Count 
            });
        }



        [HttpGet("visualizar-completa/{id:guid}")]
        public ActionResult<VisualizarTarefaViewModel> SelecionarCompletaPorId(Guid id)
        {
            var tarefaResult = servicoTarefa.SelecionarPorId(id);

            if (tarefaResult.IsFailed && RegistroNãoEncontrado(tarefaResult))
                return NotFound(tarefaResult);


            if (tarefaResult.IsFailed)
                return InternalError(tarefaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorTarefas.Map<VisualizarTarefaViewModel>(tarefaResult.Value)
                //mensagem = "Dados encontrados com Sucesso!",
                //total = tarefaResult.Value.Count 
            });
        }



        [HttpPost]
        public ActionResult<FormsTarefaViewModel> Inserir(InserirTarefaViewModel novaTarefaVM)
        {

            var tarefa = mapeadorTarefas.Map<Tarefa>(novaTarefaVM);


            //O método obtemId foi refatorado para eAgendaControllerBase na qual trazemos o UsuarioLogado "tratado"
            tarefa.UsuarioId = UsuarioLogado.Id;//precisamos pegar o usuario pelo Token


            var tarefaResult = servicoTarefa.Inserir(tarefa);

            if (tarefaResult.IsFailed)
                return InternalError(tarefaResult);

            return Ok(new
            {
                sucesso = true,
                dados = novaTarefaVM
            });

        }

      

        [HttpPut("{id:guid}")]
        public ActionResult<FormsTarefaViewModel> Editar(Guid id, EditarTarefaViewModel tarefaVM)
        {
            
            var tarefaResult = servicoTarefa.SelecionarPorId(id);

            if (tarefaResult.IsFailed && RegistroNãoEncontrado(tarefaResult))
                return NotFound(tarefaResult);


            var tarefa = mapeadorTarefas.Map(tarefaVM, tarefaResult.Value);



            tarefaResult = servicoTarefa.Editar(tarefa);

            if (tarefaResult.IsFailed)
                return InternalError(tarefaResult);

            return Ok(new
            {
                sucesso = true,
                dados = tarefaVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {

            var tarefaResult = servicoTarefa.Excluir(id);

            if (tarefaResult.IsFailed && RegistroNãoEncontrado<Tarefa>(tarefaResult))
                return NotFound(tarefaResult);

            if (tarefaResult.IsFailed)
                return InternalError<Tarefa>(tarefaResult);//como nesse caso este metodo só retorna um tipo ActionResult sem tipagem, nos devemos Tipar o InternalError


            //Caso de tudo certo, não precisa retornar nada
            return NoContent();
        }

    }
}
