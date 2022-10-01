
using System;

namespace eAgenda.webapi.ViewModels.ModuloCompromisso
{
    public class ListarCompromissoViewModel
    {
        public string Assunto { get; set; }
        public string Local { get; set; }
        public string Link { get; set; }
        public DateTime Data { get; set; }
        public string HoraInicio { get; set; }
        public string HoraTermino { get; set; }
        public string NomeContato { get; set; }
    }   
}
