using eAgenda.Dominio.ModuloTarefa;
using System;

namespace eAgenda.webapi.ViewModels.ModuloTarefa
{
    public class FormsItemTarefaViewModel
    {
        public Guid Id { set; get; }
        public string Titulo { set; get; }
        public StatusItemTarefa Status { set; get; }
        public bool Concluido { get;  set; }
    }
}
