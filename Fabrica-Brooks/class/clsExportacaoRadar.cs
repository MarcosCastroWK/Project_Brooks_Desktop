namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsExportacaoRadar
    {
        public int Sequencial { get; set; }
        public string Tabela { get; set; }
        public int CodigoNumero { get; set; }
        public string DataSolicitacao { get; set; }
        public Int16 Gerado { get; set; }
    }
}