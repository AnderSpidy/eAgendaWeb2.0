using System;

namespace eAgenda.webapi.ViewModels.Tarefa
{
    public class ListarTarefaViewModel
    {
        public Guid id { get; set; }
        public string Titulo { get; set; }
        public string Prioridade { get; set; }
        public string Situacao { get; set; }
    }
}
