namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;
    public partial class clsModeloMTRe
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string CNPJ_CPF_Armazenador { get; set; }
        public string CNPJ_CPF_Transportador { get; set; }
        public string CNPJ_CPF_Destinador { get; set; }
        public string CNPJ_CPF_Gerador { get; set; }
        public int CodigoDestinoFinal { get; set; }
    }
}
