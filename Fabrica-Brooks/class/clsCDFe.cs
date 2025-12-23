namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsCDFe
    {
        public int Sequencial { get; set; }
        public Int64 NumeroMTRe { get; set; }
        public string CodigoIBAMA { get; set; }
        public DateTime Data { get; set; }
        public decimal Quantidade { get; set; }
        public decimal QtdeUnidade { get; set; }
        public Int64 NumeroCDFe { get; set; }
        public string MensagemErro { get; set; }
        public string CNPJ_CPF_Cliente { get; set; }
        public string Situacao { get; set; }
        public string Placas { get; set; }
    }
}
