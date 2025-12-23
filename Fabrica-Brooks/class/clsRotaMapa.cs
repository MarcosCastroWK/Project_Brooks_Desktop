namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsRotaMapa
    {
        public int Sequencial { get; set; }
        public int AnoMesDia { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoCaminhao { get; set; }
        public int CodigoMotorista { get; set; }
        public int CodigoResiduo { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public string Solicitante { get; set; }
        public string ExecutarServico { get; set; }
        public string DataProgramada { get; set; }
        public string HoraProgramada { get; set; }
        public string Franquia { get; set; }
        public string Unidade { get; set; }
        public string Observacao { get; set; }
        public int Ordem { get; set; }
    }
}