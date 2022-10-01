using eAgenda.webapi.ViewModels.ModuloCompromisso;
using System.Collections.Generic;

namespace eAgenda.webapi.ViewModels.ModuloContato
{
    public class VisualizarContatoViewModel
    {
        public VisualizarContatoViewModel() { }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
    }
}
