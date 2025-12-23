namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsCaminhoes
    {
        public int Codigo { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public string Placas { get; set; }
        public string Cidade { get; set; }
        public int Kilometragem { get; set; }
        public decimal ValorFranquia { get; set; }
        public string VctoIPVA { get; set; }
        public string VctoLicenciamento { get; set; }
        public string Cor { get; set; }
        public string Chassi { get; set; }
        public string Renavam { get; set; }
        public string dtVctoSegr { get; set; }
        public string TipoVeiculo { get; set; }
        public string DataAquisicao { get; set; }
        public decimal ValorAquisicao { get; set; }
        public string VencimentoSeguroFrota { get; set; }
        public int EhProprio { get; set; }
        public string DataCadastro { get; set; }
        public int Inativo { get; set; }
    }
}
