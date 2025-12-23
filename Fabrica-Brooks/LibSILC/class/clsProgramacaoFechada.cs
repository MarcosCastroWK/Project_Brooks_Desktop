namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsProgramacaoFechada
    {
        public int Codigo { get; set; }
        public string Data { get; set; }
        public int Fechada { get; set; }
        public int BloqueadaCodigoUsuario { get; set; }
        public string BloqueadaNomeUsuario { get; set; }
    }
}
