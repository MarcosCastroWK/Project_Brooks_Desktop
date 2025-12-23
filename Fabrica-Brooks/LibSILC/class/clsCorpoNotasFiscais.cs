namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsCorpoNotasFiscais
    {
        public int SequencialNotaFiscal { get; set; }
        public int Linha { get; set; }
        public string Descricao { get; set; }
        public string Unidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Valor { get; set; }
        public decimal qt3Aux { get; set; }
        public decimal Quantidade { get; set; }
    }
}
