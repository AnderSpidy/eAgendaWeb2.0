﻿using System;

namespace eAgenda.webapi.ViewModels.Contato
{
    public class ListarContatoViewModel
    {
        public Guid id { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
    }
}
