namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsMTRCanceladaTransbordo
    {
        public int Sequencial  { get; set; }
        public string Data { get; set; }
        public int CodigoMotorista  { get; set; }
        public string NomeMotorista { get; set; }
        public int NumeroMTR  { get; set; }
        public int EhTransbordo { get; set; }
    }
}
