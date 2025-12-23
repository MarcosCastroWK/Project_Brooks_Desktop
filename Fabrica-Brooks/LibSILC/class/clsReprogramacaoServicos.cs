namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsReprogramacaoServicos
    {
        public int Sequencial { get; set; }
        public int SequencialProgramacaoDiaria { get; set; }
        public int AnoMesDia { get; set; }
        public int Linha { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public string Solicitante { get; set; }
        public int CodigoCliente { get; set; }
        public string NomeCliente { get; set; }
        public int CodigoResiduo { get; set; }
        public string DescricaoResiduo { get; set; }
        public string ExecutarServico { get; set; }
        public string DataProgramada { get; set; }
        public string HoraProgramada { get; set; }
        public decimal Quantidade { get; set; }
        public int CodigoCaminhao { get; set; }
        public string ModeloCaminhao { get; set; }
        public int CodigoMotorista { get; set; }
        public string NomeMotorista { get; set; }
        public string Observacao { get; set; }
        public string DestinoFinal { get; set; }
        public string Unidade { get; set; }
        public string StatusCor { get; set; }
        public int TipoProgramacao { get; set; }
        public int MapaMarcado { get; set; }
    }
}
