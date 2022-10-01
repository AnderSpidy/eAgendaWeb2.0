﻿using AutoMapper;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using eAgenda.webapi.ViewModels.ModuloDespesa;

namespace eAgenda.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : eAgendaControllerBase
    {
        private readonly ServicoCategoria servicoCategoria;
        private readonly IMapper mapeadorCategorias;

        public CategoriaController(ServicoCategoria servicoCategoria, IMapper mapeadorCategorias)
        {
            this.servicoCategoria = servicoCategoria;
            this.mapeadorCategorias = mapeadorCategorias;
        }

        [HttpGet]
        public ActionResult<List<ListarCategoriaViewModel>> SelecionarTodos()
        {
            var categoriaResult = servicoCategoria.SelecionarTodos();

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCategorias.Map<List<ListarCategoriaViewModel>>(categoriaResult.Value)
            });
        }

        [HttpGet("visualizacao-completa/{id:guid}")]
        public ActionResult<VisualizarCategoriaViewModel> SelecionarCategoriaCompletoPorId(Guid id)
        {
            var categoriaResult = servicoCategoria.SelecionarPorId(id);

            if (categoriaResult.IsFailed && RegistroNãoEncontrado(categoriaResult))
                return NotFound(categoriaResult);

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCategorias.Map<VisualizarCategoriaViewModel>(categoriaResult.Value)
            });
        }

        [HttpGet("{id:guid}")]
        public ActionResult<FormsCategoriaViewModel> SelecionarCategoriaPorId(Guid id)
        {
            var categoriaResult = servicoCategoria.SelecionarPorId(id);

            if (categoriaResult.IsFailed && RegistroNãoEncontrado(categoriaResult))
                return NotFound(categoriaResult);

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCategorias.Map<FormsCategoriaViewModel>(categoriaResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsCategoriaViewModel> Inserir(FormsCategoriaViewModel categoriaVM)
        {
            var categoria = mapeadorCategorias.Map<Categoria>(categoriaVM);

            var categoriaResult = servicoCategoria.Inserir(categoria);

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = categoriaVM
            });
        }

        [HttpPut("{id:guid}")]
        public ActionResult<FormsCategoriaViewModel> Editar(Guid id, FormsCategoriaViewModel categoriaVM)
        {
            var categoriaResult = servicoCategoria.SelecionarPorId(id);

            if (categoriaResult.IsFailed && RegistroNãoEncontrado(categoriaResult))
                return NotFound(categoriaResult);

            var categoria = mapeadorCategorias.Map(categoriaVM, categoriaResult.Value);

            categoriaResult = servicoCategoria.Editar(categoria);

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = categoriaVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var categoriaResult = servicoCategoria.Excluir(id);

            if (categoriaResult.IsFailed && RegistroNãoEncontrado<Categoria>(categoriaResult))
                return NotFound<Categoria>(categoriaResult);

            if (categoriaResult.IsFailed)
                return InternalError<Categoria>(categoriaResult);

            return NoContent();
        }
    }
}
