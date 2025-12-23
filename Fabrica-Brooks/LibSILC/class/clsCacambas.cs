namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsCacambas
    {
        public int Codigo { get; set; }
        public string Tipo { get; set; }
        public decimal Capacidade { get; set; }
        public string Cor { get; set; }
        public int EhLocal { get; set; }
        public string Numero { get; set; }
        public int EhTerceiro { get; set; }
        public string DataCadastro { get; set; }
        public int Inativo { get; set; }
    }
}
