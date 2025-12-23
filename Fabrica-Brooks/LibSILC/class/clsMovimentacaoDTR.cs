namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsMovimentacaoDTR
    {
        public int Codigo { get; set; }
        public string Data { get; set; }
        public string MoviCxDe { get; set; }
        public string MoviCxPara { get; set; }
        public int NumeroLancamento { get; set; }
        public int CodigoResiduo { get; set; }
        public int CodigoCliente { get; set; }
    }
}
