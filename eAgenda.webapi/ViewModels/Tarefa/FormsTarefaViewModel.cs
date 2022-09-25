using eAgenda.Dominio.ModuloTarefa;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.webapi.ViewModels.Tarefa
{
    public class FormsTarefaViewModel
    {
        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public string Titulo { set; get; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public PrioridadeTarefaEnum Prioridade { get; set; }
        public List<FormsItemTarefaViewModel> Itens { set; get; }
    }

    public class InserirTarefaViewModel : FormsTarefaViewModel { }
    public class EditarTarefaViewModel : FormsTarefaViewModel { }
}
