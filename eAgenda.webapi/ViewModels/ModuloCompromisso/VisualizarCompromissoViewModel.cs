using eAgenda.Dominio.ModuloContato;
using eAgenda.webapi.ViewModels.ModuloContato;
using System;

namespace eAgenda.webapi.ViewModels.ModuloCompromisso
{
    public class VisualizarCompromissoViewModel
    {
        VisualizarCompromissoViewModel() { }
        public string Assunto { get; set; }
        public string Local { get; set; }
        public string Link { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public ListarContatoViewModel Contato { get; set; }
    }
}
