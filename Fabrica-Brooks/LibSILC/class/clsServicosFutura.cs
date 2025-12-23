namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsServicosFutura
    {
        public int Sequencial { get; set; }
        public int CodigoCliente { get; set; }
        public string NomeCliente { get; set; }
        public int CodigoResiduo { get; set; }
        public string DescricaoResiduo { get; set; } // com join tabela de residuos
        public string DataProgramada { get; set; }
        public string Observacao { get; set; }
        public string DestinoFinal { get; set; }
        public int CodigoCaminhao { get; set; }
        public string ModeloCaminhao { get; set; }
        public int CodigoMotorista { get; set; }
        public string NomeMotorista { get; set; }
        public int MapaMarcado { get; set; }
        public string ServicoAExecutar { get; set; }
        public string Hora { get; set; }
        public string Solicitante { get; set; }
        public string NumeroMTRe { get; set; }
    }
}
