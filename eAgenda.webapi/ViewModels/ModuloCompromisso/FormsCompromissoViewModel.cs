using eAgenda.Dominio.ModuloCompromisso;
using System.ComponentModel.DataAnnotations;
using System;

namespace eAgenda.webapi.ViewModels.ModuloCompromisso
{
    public class FormsCompromissoViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Assunto { get; set; }

        public string Local { get; set; }// não precisa do decorator Required pois não é obrigatorio, só consultar o mapeador da infra
        public string Link { get; set; }// não precisa do decorator Required pois não é obrigatorio, só consultar o mapeador da infra

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime Data { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public TimeSpan HoraInicio { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public TimeSpan HoraTermino { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public TipoLocalizacaoCompromissoEnum TipoLocal { get; set; }

        public Guid ContatoId { get; set; }//Como no banco de dados fica armazenado apenas o Id do contato 
        
       
        

       
    }
    public class InserirCompromissoViewModel : FormsCompromissoViewModel { }
    public class EditarCompromissoViewModel : FormsCompromissoViewModel { }
}
