﻿using System;
using System.Collections.Generic;

namespace eAgenda.webapi.ViewModels.ModuloTarefa
{
    public class VisualizarTarefaViewModel
    {
        public VisualizarTarefaViewModel()
        {
            Itens = new List<VisualizarItemTarefaViewModel>();
        }
        public string Titulo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public int QuantidaDeItens { get; set; }
        public decimal PercentualConcluido { get; set; }
        public string Prioridade { get; set; }
        public string Situacao { get; set; }
        public List<VisualizarItemTarefaViewModel> Itens { set; get; }
    }
}