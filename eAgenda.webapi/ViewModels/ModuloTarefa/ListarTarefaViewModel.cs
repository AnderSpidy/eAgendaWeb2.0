using System;

namespace eAgenda.webapi.ViewModels.ModuloTarefa
{
    public class ListarTarefaViewModel
    {
        public Guid id { get; set; }
        public string Titulo { get; set; }
        public string Prioridade { get; set; }
        public string Situacao { get; set; }
    }
}
