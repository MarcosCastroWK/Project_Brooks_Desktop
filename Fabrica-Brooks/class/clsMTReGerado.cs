namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsMTReGerado
    {
        public int Sequencial { get; set; }
        public string DataEmissao { get; set; }
        public string CNPJ_CPF_Armazenador { get; set; }
        public string CNPJ_CPF_Transportador { get; set; }
        public string CNPJ_CPF_Destinador { get; set; }
        public string CNPJ_CPF_Gerador { get; set; }
        public int CodigoCliente { get; set; }
        public string NumeroMTRe { get; set; }
        public string NomeResponsavel { get; set; }
        public string CargoResponsavel { get; set; }
        public string DataTransporte { get; set; }
        public int CodigoMotorista { get; set; }
        public string PlacaVeiculo { get; set; }
        public string Observacao { get; set; }
    }
}
