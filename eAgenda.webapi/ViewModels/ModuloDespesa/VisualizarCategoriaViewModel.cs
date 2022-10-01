using System.Collections.Generic;

namespace eAgenda.webapi.ViewModels.ModuloDespesa
{
    public class VisualizarCategoriaViewModel
    {
        public string Titulo { get; set; }

        public List<ListarDespesaViewModel> Despesas { get; set; }
    }
}
