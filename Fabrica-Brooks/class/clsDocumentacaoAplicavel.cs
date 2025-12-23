namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsDocumentacaoAplicavel
    {
        public int Sequencial { get; set; }
        public int CodigoCliente { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string PeriodoApuracao { get; set; }
        public int EnviarPlanFatAteDia { get; set; }
        public int AguardarAprovacaoPlanFat { get; set; }
        public int PlanFatEnviada { get; set; }
        public int AguardarOrdemCompra { get; set; }
        public int EnviarCDFBrooks { get; set; }
        public int ConferirDDRAteDia { get; set; }
        public int ConferindoDDR { get; set; }
        public int DDRConferida { get; set; }
        public int EnviarRGRAteDia { get; set; }
        public int EnviarRelGer { get; set; }
        public int RelGerEnviado { get; set; }
    }
}
        
