namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsLancamentos
    {
        public int NumeroLancamento { get; set; }
        public int CodigoCliente { get; set; }
        public string Data { get; set; }
        public int CodigoEnderecoObra { get; set; }
        public string NumeroCaixa { get; set; }
        public int CodigoCaminhaoColoca { get; set; }
        public string DataColocacao { get; set; }
        public string HorasColocacao { get; set; }
        public string DataARetirar { get; set; }
        public string HorasARetirar { get; set; }
        public string DataRetirada { get; set; }
        public string HoraRetirada { get; set; }
        public int CodigoCaminhoRetirada { get; set; }
        public int CodigoMotoristaColocou { get; set; }
        public int CodigoMotoristaRetirou { get; set; }
        public decimal ValorLocacao { get; set; }
        public string OBS { get; set; }
        public int NuLancColocacao { get; set; }
        public int TipoOperacao { get; set; }
        public int Horas { get; set; }
    }
}
