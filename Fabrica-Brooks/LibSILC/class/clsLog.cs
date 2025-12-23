namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsLog
    {
        public int Codigo { get; set; }
        public int CodigoUsuario { get; set; }
        public string Log { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public string Operacao { get; set; }
        public string LocalOperacao  { get; set; }
    }
}
