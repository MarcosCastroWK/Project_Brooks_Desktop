namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;
    
    public partial class clsParametros
    {
        public int Numero { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CNPJ_CPF { get; set; }
        public string IE_RG { get; set; }
        public string Site { get; set; }
        public string Email { get; set; }
        public string Telefones { get; set; }
        public string CodigoMunicipio { get; set; }
        public int FormularioContinuo { get; set; }
        public int AtualizaRoteiroSemanal { get; set; }
        public int AtualizaRoteiroMensal { get; set; }
        public decimal PercentualISS { get; set; }
    }
}
