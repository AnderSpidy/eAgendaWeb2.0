using eAgenda.Dominio.ModuloDespesa;
using System.Collections.Generic;
using System;

namespace eAgenda.webapi.ViewModels.ModuloDespesa
{
    public class FormsDespesaViewModel
    {
        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public FormaPgtoDespesaEnum FormaPagamento { get; set; }

        public List<CategoriaSelecionadaViewModel> CategoriasSelecionadas { get; set; }
    }
}
