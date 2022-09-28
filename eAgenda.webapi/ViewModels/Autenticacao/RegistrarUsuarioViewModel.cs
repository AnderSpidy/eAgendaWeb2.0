using System.ComponentModel.DataAnnotations;

namespace eAgenda.webapi.ViewModels.Autenticacao
{
    public class RegistrarUsuarioViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [EmailAddress(ErrorMessage =" O campo {0} está em formato inválido")]
        public string Email { get; set; }
       
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(100,ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }

        [Compare("Senha",ErrorMessage = "As senhas não conferem")]
        public string ConfirmarSenha { get; set; }
    }
}
