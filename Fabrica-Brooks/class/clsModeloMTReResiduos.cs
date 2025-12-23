namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsModeloMTReResiduos
    {
        public int Codigo { get; set; }
        public int CodigoModeloMTRe { get; set; }
        public int CodigoResiduo { get; set; }
        public int CodigoEstadoFisico { get; set; }
        public int CodigoClasse { get; set; }
        public int CodigoAcondicionamento { get; set; }
        public int CodigoTecnologia { get; set; }
        public string NumeroONU { get; set; }        
        public string ClasseRisco { get; set; }
        public string NomeEmbarque { get; set; }
        public string GrupoEmbalagem { get; set; }
        public int CodigoUnidade { get; set; }
    }
}