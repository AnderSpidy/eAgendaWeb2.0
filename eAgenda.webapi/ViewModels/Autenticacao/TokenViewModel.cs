using System;

namespace eAgenda.webapi.ViewModels.Autenticacao
{
    public class TokenViewModel
    {
        public string Chave { get; set; }
        public UsuarioTokenViewModel UsuarioToken { get; set; }

        public DateTime DataExpiracacao { get; set; }//Não entendi o porque da existencia 
    }
}
