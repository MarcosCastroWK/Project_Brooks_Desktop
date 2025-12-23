namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;
    public partial class clsAvisoInsercaoProgramacaoFechada
    {
        public int Codigo { get; set; }
        public string Data { get; set; }
        public string DataExecutado { get; set; }
        public string Usuario { get; set; }
        public string Mensagem { get; set; }
        public int Visto { get; set; }
    }
}
