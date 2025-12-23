namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsContratosReajustes
    {
        public int Sequencial { get; set; }
        public int CodigoContrato {get; set;}
        public string Data { get; set; }
        public decimal Valor { get; set; }
        public string NumeroContrato { get; set; }
        public string Situacao { get; set; }
        public string TipoNegociacao { get; set; }
        public string Observacao { get; set; }
    }
}
