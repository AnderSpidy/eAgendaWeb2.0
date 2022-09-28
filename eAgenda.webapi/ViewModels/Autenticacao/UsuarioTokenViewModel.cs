using System;

namespace eAgenda.webapi.ViewModels.Autenticacao
{
    public class UsuarioTokenViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
    }
}
