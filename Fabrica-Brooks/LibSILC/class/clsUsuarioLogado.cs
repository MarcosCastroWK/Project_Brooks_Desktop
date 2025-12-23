namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsUsuarioLogado
    {
        public int    Codigo { get; set; }
        public string Maquina { get; set; }
        public string UsuarioWindows { get; set; }
        public string UsuarioNome { get; set; }
        public int    CodigoEmpresa { get; set; }
        public bool   Aplicativo { get; set; }
    }
}
