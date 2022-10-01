using AutoMapper;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.webapi.ViewModels.ModuloDespesa;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace eAgenda.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : eAgendaControllerBase
    {
        private readonly ServicoDespesa servicoDespesa;
        private readonly IMapper mapeadorDespesas;

        public DespesaController(ServicoDespesa servicoDespesa, IMapper mapeadorDespesas)
        {
            this.servicoDespesa = servicoDespesa;
            this.mapeadorDespesas = mapeadorDespesas;
        }

        [HttpGet]
        public ActionResult<List<ListarDespesaViewModel>> SelecionarTodos()
        {
            var despesaResult = servicoDespesa.SelecionarTodos();

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorDespesas.Map<List<ListarDespesaViewModel>>(despesaResult.Value)
            });
        }

        [HttpGet("visualizacao-completa/{id:guid}")]
        public ActionResult<VisualizarDespesaViewModel> SelecionarDespesaCompletoPorId(Guid id)
        {
            var despesaResult = servicoDespesa.SelecionarPorId(id);

            if (despesaResult.IsFailed && RegistroNãoEncontrado(despesaResult))
                return NotFound(despesaResult);

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorDespesas.Map<VisualizarDespesaViewModel>(despesaResult.Value)
            });
        }

        [HttpGet("{id:guid}")]
        public ActionResult<FormsDespesaViewModel> SelecionarDespesaPorId(Guid id)
        {
            var tarefaResult = servicoDespesa.SelecionarPorId(id);

            if (tarefaResult.IsFailed && RegistroNãoEncontrado(tarefaResult))
                return NotFound(tarefaResult);

            if (tarefaResult.IsFailed)
                return InternalError(tarefaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorDespesas.Map<FormsDespesaViewModel>(tarefaResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsDespesaViewModel> Inserir(FormsDespesaViewModel despesaVM)
        {
            var despesa = mapeadorDespesas.Map<Despesa>(despesaVM);

            var despesaResult = servicoDespesa.Inserir(despesa);

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = despesaVM
            });
        }

        [HttpPut("{id:guid}")]
        public ActionResult<FormsDespesaViewModel> Editar(Guid id, FormsDespesaViewModel despesaVM)
        {
            var despesaResult = servicoDespesa.SelecionarPorId(id);

            if (despesaResult.IsFailed && RegistroNãoEncontrado(despesaResult))
                return NotFound(despesaResult);

            var despesa = mapeadorDespesas.Map(despesaVM, despesaResult.Value);

            despesaResult = servicoDespesa.Editar(despesa);

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = despesaVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var despesaResult = servicoDespesa.Excluir(id);

            if (despesaResult.IsFailed && RegistroNãoEncontrado<Despesa>(despesaResult))
                return NotFound<Despesa>(despesaResult);

            if (despesaResult.IsFailed)
                return InternalError<Despesa>(despesaResult);

            return NoContent();
        }
    }
}
