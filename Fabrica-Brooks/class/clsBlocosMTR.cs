namespace SILCNegocios
{
    using System;
    using System.Collections.Generic;

    public partial class clsBlocosMTR
    {
        public int Sequencial  { get; set; }
        public string Data { get; set; }
        public int CodigoMotorista  { get; set; }
        public string NomeMotorista { get; set; }
        public int NumeroBloco  { get; set; }
        public int NuMTRInicial  { get; set; }
        public int NuMTRFinal  { get; set; }
        public int CoeficienteNumeracao { get; set; }
    }
}
