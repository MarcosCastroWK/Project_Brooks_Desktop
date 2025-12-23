using System;
using System.Linq;
using SILCNegocios;
using System.Collections;
namespace LibSILC
{
    public class geral
    {
        public static string NomeSistema = "SILC - Sistema Integrado de Locação de Containeres";
        public static int BancoUsado = 3;  // 1 - producao  ou  2 - teste servidor  ou  3 - teste localhost home office

        public static bool BancoDadosDefinido = true;
        public static int CodigoCliente { get; set; }
        public static string NomeClienteFantasia { get; set; }
        public static int CodigoCaminhao { get; set; }
        public static int CodigoMotorista { get; set; }
        public static int CodigoEmpresa { get; set; }
        public static int CodigoResiduo { get; set; }
        public static int CodigoDESTINOFINAL { get; set; }
        public static int NumeroNF { get; set; }
        public static string Ordem { get; set; }
        public static bool Demonstracao { get; set; }
        public static string cnpj_cpf { get; set; }
        public static string senhamtre { get; set; }
        public static string DataProgramada { get; set; }
        public static bool NotaFiscalEnviada { get; set; }
        public static bool AcessoPrincipal { get; set; }
        public static string UsuarioAtual { get; set; }
        public static int CodigoUsuarioAtual { get; set; }

        public static bool AcessoPermitido { get; set; }

        public static string VoltaForm { get; set; } = "";
        public static clsParametros oEmpresa { get; set; }  = new clsParametros();
        public static clsParametrosDados oEmpresaDados { get; set; } = new clsParametrosDados();

        public static string DataFormatada(string pData)
        {
            if (Left(pData, 10) == "0001-01-01" || Left(pData, 10) == "0100-01-01" || Left(pData, 10) == "1900-01-01" || 
                Left(pData, 10) == "01-01-0001" || Left(pData, 10) == "01-01-0100" || Left(pData, 10) == "01-01-1900" ||
                Left(pData, 10) == "0001/01/01" || Left(pData, 10) == "0100/01/01" || Left(pData, 10) == "1900/01/01" ||
                Left(pData, 10) == "01/01/0001" || Left(pData, 10) == "01/01/0100" || Left(pData, 10) == "01/01/1900")
                pData = "";
            else if (pData != "")
                pData = Convert.ToDateTime(pData).ToString("dd/MM/yyyy");
            return pData;
        }
        public static string Left(string str, int length)
        {
            string sRet = "";
            if (length > 0)
                sRet = str.Substring(0, Math.Min(length, str.Length));
            else if (str.Length > 0)
                sRet = str.Substring(0, str.Length);
            return sRet;
        }
        public enum DOCUMENTO
        {
            RGR, DDR, CDF, CERTISO, ALVARA, LAO, LAUDO, CTFIBAMA, Nenhum
        }
        public static string TratarString(object str)
        {
            try
            {
                return (string)str;
            }
            catch
            {
                return "";
            }
        }
        public static DateTime TratarData(object dt)
        {
            try
            {
                if (Convert.ToDateTime(dt).Year <= 1900) { dt = "01/01/1900 00:00:00"; }
                return Convert.ToDateTime(dt);
            }
            catch
            {
                return new DateTime();
            }
        }
        public static string RetiraLetras(string pNumero)
        {
            pNumero = pNumero.Replace("A", "");
            pNumero = pNumero.Replace("Á", "");
            pNumero = pNumero.Replace("Ã", "");
            pNumero = pNumero.Replace("B", "");
            pNumero = pNumero.Replace("C", "");
            pNumero = pNumero.Replace("Ç", "");
            pNumero = pNumero.Replace("D", "");
            pNumero = pNumero.Replace("E", "");
            pNumero = pNumero.Replace("É", "");
            pNumero = pNumero.Replace("Ê", "");
            pNumero = pNumero.Replace("F", "");
            pNumero = pNumero.Replace("G", "");
            pNumero = pNumero.Replace("H", "");
            pNumero = pNumero.Replace("I", "");
            pNumero = pNumero.Replace("J", "");
            pNumero = pNumero.Replace("K", "");
            pNumero = pNumero.Replace("L", "");
            pNumero = pNumero.Replace("M", "");
            pNumero = pNumero.Replace("N", "");
            pNumero = pNumero.Replace("O", "");
            pNumero = pNumero.Replace("Ó", "");
            pNumero = pNumero.Replace("P", "");
            pNumero = pNumero.Replace("Q", "");
            pNumero = pNumero.Replace("R", "");
            pNumero = pNumero.Replace("S", "");
            pNumero = pNumero.Replace("T", "");
            pNumero = pNumero.Replace("U", "");
            pNumero = pNumero.Replace("V", "");
            pNumero = pNumero.Replace("W", "");
            pNumero = pNumero.Replace("X", "");
            pNumero = pNumero.Replace("Y", "");
            pNumero = pNumero.Replace("Z", "");
            pNumero = pNumero.Replace("'", "");
            pNumero = pNumero.Replace("a", "");
            pNumero = pNumero.Replace("á", "");
            pNumero = pNumero.Replace("ã", "");
            pNumero = pNumero.Replace("b", "");
            pNumero = pNumero.Replace("c", "");
            pNumero = pNumero.Replace("ç", "");
            pNumero = pNumero.Replace("d", "");
            pNumero = pNumero.Replace("e", "");
            pNumero = pNumero.Replace("é", "");
            pNumero = pNumero.Replace("ê", "");
            pNumero = pNumero.Replace("f", "");
            pNumero = pNumero.Replace("g", "");
            pNumero = pNumero.Replace("h", "");
            pNumero = pNumero.Replace("i", "");
            pNumero = pNumero.Replace("j", "");
            pNumero = pNumero.Replace("k", "");
            pNumero = pNumero.Replace("l", "");
            pNumero = pNumero.Replace("m", "");
            pNumero = pNumero.Replace("n", "");
            pNumero = pNumero.Replace("o", "");
            pNumero = pNumero.Replace("ó", "");
            pNumero = pNumero.Replace("p", "");
            pNumero = pNumero.Replace("q", "");
            pNumero = pNumero.Replace("r", "");
            pNumero = pNumero.Replace("s", "");
            pNumero = pNumero.Replace("t", "");
            pNumero = pNumero.Replace("u", "");
            pNumero = pNumero.Replace("v", "");
            pNumero = pNumero.Replace("w", "");
            pNumero = pNumero.Replace("x", "");
            pNumero = pNumero.Replace("y", "");
            pNumero = pNumero.Replace("z", "");
            return pNumero;
        }

        public static string RetiraCharsCNPJCPF(string pStr)
        {
            if (pStr != "" && pStr != null)
            {
                pStr = pStr.Replace(".", "");
                pStr = pStr.Replace("/", "");
                pStr = pStr.Replace("-", "");
                pStr = pStr.Replace("'", "");
                pStr = pStr.Replace("!", "");
                pStr = pStr.Replace("@", "");
                pStr = pStr.Replace("#", "");
                pStr = pStr.Replace("$", "");
                pStr = pStr.Replace("¨", "");
                pStr = pStr.Replace("%", "");
                pStr = pStr.Replace("&", "");
                pStr = pStr.Replace("*", "");
                pStr = pStr.Replace("(", "");
                pStr = pStr.Replace(")", "");
            }
            return pStr;
        }
        public static string ColocaPontosCPF(string pStr)
        {
            try
            {
                pStr = pStr[0].ToString() + pStr[1].ToString() + pStr[2].ToString() + "." +
                       pStr[3].ToString() + pStr[4].ToString() + pStr[5].ToString() + "." +
                       pStr[6].ToString() + pStr[7].ToString() + pStr[8].ToString() + "-" +
                       pStr[9].ToString() + pStr[10].ToString();
                return pStr;
            }
            catch
            {
                return "";
            }
        }
        public static string ColocaPontosCNPJ(string pStr)
        {
            try
            {
                pStr = pStr[0].ToString() + pStr[1].ToString() + "." +
                       pStr[2].ToString() + pStr[3].ToString() + pStr[4].ToString() + "." +
                       pStr[5].ToString() + pStr[6].ToString() + pStr[7].ToString() + "/" +
                       pStr[8].ToString() + pStr[9].ToString() + pStr[10].ToString() + pStr[11].ToString() + "-" +
                       pStr[12].ToString() + pStr[13].ToString();
                return pStr;
            }
            catch
            {
                return "";
            }
            
        }
        public static string ColocaTracoCEP(string pStr)
        {
            try
            {
                pStr = pStr[0].ToString() + pStr[1].ToString() + pStr[2].ToString() + pStr[3].ToString() + pStr[4].ToString() + "-" +
                pStr[5].ToString() + pStr[6].ToString() + pStr[7].ToString();
                return pStr;
            }
            catch
            {
                return "";
            }
        }

		public static bool ValidaCNPJ(string pCNPJ)
		{
			int[] multiplicador1 = new int[12] {5,4,3,2,9,8,7,6,5,4,3,2};
			int[] multiplicador2 = new int[13] {6,5,4,3,2,9,8,7,6,5,4,3,2};
			int soma;
			int resto;
			string digito;
			string tempCNPJ;
			pCNPJ = pCNPJ.Trim();
			pCNPJ = pCNPJ.Replace(".", "").Replace("-", "").Replace("/", "");
			if (pCNPJ.Length != 14)
			   return false;
			tempCNPJ = pCNPJ.Substring(0, 12);
			soma = 0;
			for(int i=0; i<12; i++)
			   soma += int.Parse(tempCNPJ[i].ToString()) * multiplicador1[i];
			resto = (soma % 11);
			if ( resto < 2)
			   resto = 0;
			else
			   resto = 11 - resto;
			digito = resto.ToString();
			tempCNPJ = tempCNPJ + digito;
			soma = 0;
			for (int i = 0; i < 13; i++)
			   soma += int.Parse(tempCNPJ[i].ToString()) * multiplicador2[i];
			resto = (soma % 11);
			if (resto < 2)
			    resto = 0;
			else
			   resto = 11 - resto;
			digito = digito + resto.ToString();
			return pCNPJ.EndsWith(digito);
		}

        public static bool ValidaCPF(string pCPF)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            pCPF = pCPF.Trim();
            pCPF = pCPF.Replace(".", "").Replace("-", "");
            if (pCPF.Length != 11)
                return false;
            tempCpf = pCPF.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return pCPF.EndsWith(digito);
        }
        public static string NomeEmpresa(int pCodigoEmpresa)
        {
            oEmpresa = oEmpresaDados.PegaDados(oEmpresa, pCodigoEmpresa);
            return oEmpresa.Nome;
        }       
        public static bool IsNumeric(string data)
        {
            bool eh_numerico = true;
            char[] datachars = data.ToCharArray();

            foreach (char _char in datachars)
            if (_char <= 43 || _char >= 58 || _char == 47)
            {
                eh_numerico = false;
                break;
            }

            return eh_numerico;
        }
        public static string RemoverAcentos(string pTexto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇçº";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCco";

            if (pTexto != null)
            {
                for (int i = 0; i < comAcentos.Length; i++)
                {
                    pTexto = pTexto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
                }
            }
            return pTexto;
        }
        public static bool ContemLetras(string texto)
        {
            if (texto.Where(c => char.IsLetter(c)).Count() > 0)
                return true;
            else
                return false;
        }

        public static string RetornaCodigoCor(string pCor)
        {
            string _cor = "";
            if (pCor == "Amarelo")
                _cor = "65535";
            else if (pCor == "Verde")
                _cor = "65280";
            else if (pCor == "Cinza")
                _cor = "8427929";
            else if (pCor == "Vermelho")
                _cor = "8421631";
            else if (pCor == "AzulClaro")
                _cor = "15000000";
            else if (pCor == "Branco")
                _cor = "-2147483647";
            else if (pCor == "Azul")
                _cor = "16761024";
            else if (pCor == "Magenta")
                _cor = "064444";
            return _cor;
        }
        public static string VerificaHora(string pHora)
        {
            pHora = geral.RetiraLetras(pHora).Replace(":", "");
            pHora = geral.RetiraCharsCNPJCPF(pHora).Trim();
            int _hh = 0;
            int _mm = 0;
            int _ss = 0;
            if (pHora.Length == 1)
                _hh = Convert.ToInt16(pHora.Substring(0, 1));
            else if (pHora.Length >= 2)
                _hh = Convert.ToInt16(pHora.Substring(0, 2));
            if (pHora.Length >= 4)
                _mm = Convert.ToInt16(pHora.Substring(2, 2));
            if (pHora.Length == 5)
                _ss = Convert.ToInt16(pHora.Substring(4, 1));
            else if (pHora.Length == 6)
                _ss = Convert.ToInt16(pHora.Substring(4, 2));
            if (_mm > 59)
                pHora = "Hora inválida!";
            else if (_ss > 59)
                pHora = "Hora inválida!";
            else if (pHora == "000000")
                pHora = "";
            else if (pHora.Length > 6 || _hh == 0 && pHora != "")  
                pHora = "Hora inválida!";
            else if (pHora.Length >= 2 && _hh > 24)
                pHora = "Hora inválida!";
            else if (pHora.Length == 1)
                pHora = "0" + pHora + ":00:00";
            else if (pHora.Length == 2)
                pHora = pHora + ":00:00";
            else if (pHora.Length == 3)
                pHora = pHora.Substring(0, 2) + ":0" + pHora.Substring(2, 1) + ":00";
            else if (pHora.Length == 4)
                pHora = pHora.Substring(0, 2) + ":" + pHora.Substring(2, 2) + ":00";
            else if (pHora.Length == 5)
                pHora = pHora.Substring(0, 2) + ":" + pHora.Substring(2, 2) + ":0" + pHora.Substring(4, 1);
            else if (pHora.Length == 6)
                pHora = pHora.Substring(0, 2) + ":" + pHora.Substring(2, 2) + ":" + pHora.Substring(4, 2);
            return pHora;
        }
        public static string Texto_email_DestinoFinal()
        {
            string s = "";
            s = s + "Prezado Fornecedor. <br /><br />";
            s = s + "Segue, em anexo, Relatório de Resíduos enviados pela Brooks Ambiental para conferência da baixa das respectivas MTRe no sistema MTR FATMA. <br /><br />";
            s = s + "No campo 'MTRe' caso conste o código    ' -1  '  , significa que a MTRe não foi emitida. <br /><br /> ";
            s = s + "Solicitamos que os CDF´s sejam emitidos impreterivelmente até o dia 05 de cada mês. <br /><br /> ";
            s = s + "Colocamo-nos à disposição para os esclarecimentos necessários. <br /><br /><br />";
            s = s + "Atenciosamente <br /><br /><br />";
            s = s + "Logística <br /><br />";
            s = s + "48-33441515 ";
            return s;
        }
        public static string PegaMesExtenso(int pMes)
        {
            string sRet = "";
            if (pMes == 1)
                sRet = "Janeiro";
            else if (pMes == 2)
                sRet = "Fevereiro";
            else if (pMes == 3)
                sRet = "Março";
            else if (pMes == 4)
                sRet = "Abril";
            else if (pMes == 5)
                sRet = "Maio";
            else if (pMes == 6)
                sRet = "Junho";
            else if (pMes == 7)
                sRet = "Julho";
            else if (pMes == 8)
                sRet = "Agosto";
            else if (pMes == 9)
                sRet = "Setembro";
            else if (pMes == 10)
                sRet = "Outubro";
            else if (pMes == 11)
                sRet = "Novembro";
            else if (pMes == 12)
                sRet = "Dezembro";
            return sRet;
        }

        public static string Extenso_Valor(decimal pdbl_Valor)
        {
            string strValorExtenso = ""; //Variável que irá armazenar o valor por extenso do número informado
            string strNumero = "";       //Irá armazenar o número para exibir por extenso 
            string strCentena = "";
            string strDezena = "";
            string strDezCentavo = "";

            decimal dblCentavos = 0;
            decimal dblValorInteiro = 0;
            int intContador = 0;
            bool bln_Bilhao = false;
            bool bln_Milhao = false;
            bool bln_Mil = false;
            bool bln_Unidade = false;

            //Verificar se foi informado um dado indevido 
            if (pdbl_Valor == 0 || pdbl_Valor <= 0)
            {
                throw new Exception("Valor não suportado pela Função. Verificar se há valor negativo ou nada foi informado");
            }
            if (pdbl_Valor > (decimal)9999999999.99)
            {
                throw new Exception("Valor não suportado pela Função. Verificar se o Valor está acima de 9999999999.99");
            }
            else //Entrada padrão do método
            {
                //Gerar Extenso Centavos 
                pdbl_Valor = (Decimal.Round(pdbl_Valor, 2));
                dblCentavos = pdbl_Valor - (Int64)pdbl_Valor;

                //Gerar Extenso parte Inteira
                dblValorInteiro = (Int64)pdbl_Valor;
                if (dblValorInteiro > 0)
                {
                    if (dblValorInteiro > 999)
                    {
                        bln_Mil = true;
                    }
                    if (dblValorInteiro > 999999)
                    {
                        bln_Milhao = true;
                        bln_Mil = false;
                    }
                    if (dblValorInteiro > 999999999)
                    {
                        bln_Mil = false;
                        bln_Milhao = false;
                        bln_Bilhao = true;
                    }

                    for (int i = (dblValorInteiro.ToString().Trim().Length) - 1; i >= 0; i--)
                    {
                        // strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) + 1, 1);
                        strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 1);
                        switch (i)
                        {            /*******/
                            case 9:  /*Bilhão*
                                     /*******/
                                {
                                    strValorExtenso = fcn_Numero_Unidade(strNumero) + ((int.Parse(strNumero) > 1) ? " Bilhões e" : " Bilhão e");
                                    bln_Bilhao = true;
                                    break;
                                }
                            case 8: /********/
                            case 5: //Centena*
                            case 2: /********/
                                {
                                    if (int.Parse(strNumero) > 0)
                                    {
                                        strCentena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 3);

                                        if (int.Parse(strCentena) > 100 && int.Parse(strCentena) < 200)
                                        {
                                            strValorExtenso = strValorExtenso + " Cento e ";
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + " " + fcn_Numero_Centena(strNumero);
                                        }
                                        if (intContador == 8)
                                        {
                                            bln_Milhao = true;
                                        }
                                        else if (intContador == 5)
                                        {
                                            bln_Mil = true;
                                        }
                                    }
                                    break;
                                }
                            case 7: /*****************/
                            case 4: //Dezena de Milhão*
                            case 1: /*****************/
                                {
                                    if (int.Parse(strNumero) > 0)
                                    {
                                        strDezena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 2);//

                                        if (int.Parse(strDezena) > 10 && int.Parse(strDezena) < 20)
                                        {
                                            strValorExtenso = strValorExtenso + (Right(strValorExtenso, 5).Trim() == "entos" ? " e " : " ")
                                            + fcn_Numero_Dezena0(Right(strDezena, 1));//corrigido

                                            bln_Unidade = true;
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + (Right(strValorExtenso, 5).Trim() == "entos" ? " e " : " ")
                                            + fcn_Numero_Dezena1(Left(strDezena, 1));//corrigido 

                                            bln_Unidade = false;
                                        }
                                        if (intContador == 7)
                                        {
                                            bln_Milhao = true;
                                        }
                                        else if (intContador == 4)
                                        {
                                            bln_Mil = true;
                                        }
                                    }
                                    break;
                                }
                            case 6: /******************/
                            case 3: //Unidade de Milhão* 
                            case 0: /******************/
                                {
                                    if (int.Parse(strNumero) > 0 && !bln_Unidade)
                                    {
                                        if ((Right(strValorExtenso, 5).Trim()) == "entos"
                                        || (Right(strValorExtenso, 3).Trim()) == "nte"
                                        || (Right(strValorExtenso, 3).Trim()) == "nta")
                                        {
                                            strValorExtenso = strValorExtenso + " e ";
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + " ";
                                        }
                                        strValorExtenso = strValorExtenso + fcn_Numero_Unidade(strNumero);
                                    }
                                    if (i == 6)
                                    {
                                        if (bln_Milhao || int.Parse(strNumero) > 0)
                                        {
                                            strValorExtenso = strValorExtenso + ((int.Parse(strNumero) == 1) && !bln_Unidade ? " Milhão" : " Milhões");
                                            strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 1000000) ? " " : " e");
                                            bln_Milhao = true;
                                        }
                                    }
                                    if (i == 3)
                                    {
                                        if (bln_Mil || int.Parse(strNumero) > 0)
                                        {
                                            strValorExtenso = strValorExtenso + " Mil";
                                            strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 1000) ? " " : " e");
                                            bln_Mil = true;
                                        }
                                    }
                                    if (i == 0)
                                    {
                                        if ((bln_Bilhao && !bln_Milhao && !bln_Mil
                                        && Right((dblValorInteiro.ToString().Trim()), 3) == "0")
                                        || (!bln_Bilhao && bln_Milhao && !bln_Mil
                                        && Right((dblValorInteiro.ToString().Trim()), 3) == "0"))
                                        {
                                            strValorExtenso = strValorExtenso + " e ";
                                        }
                                        strValorExtenso = strValorExtenso + ((Int64.Parse(dblValorInteiro.ToString())) > 1 ? " Reais" : " Real");
                                    }
                                    bln_Unidade = false;
                                    break;
                                }
                        }
                    }//
                }
                if (dblCentavos > 0)
                {

                    if (dblCentavos > 0 && dblCentavos < 0.1M)
                    {
                        strNumero = Right((Decimal.Round(dblCentavos, 2)).ToString().Trim(), 1);
                        strValorExtenso = strValorExtenso + ((dblCentavos > 0) ? " e " : " ")
                        + fcn_Numero_Unidade(strNumero) + ((dblCentavos > 0.01M) ? " Centavos" : " Centavo");
                    }
                    else if (dblCentavos > 0.1M && dblCentavos < 0.2M)
                    {
                        strNumero = Right(((Decimal.Round(dblCentavos, 2) - (decimal)0.1).ToString().Trim()), 1);
                        strValorExtenso = strValorExtenso + ((dblCentavos > 0) ? " " : " e ")
                        + fcn_Numero_Dezena0(strNumero) + " Centavos ";
                    }
                    else
                    {
                        strNumero = Right(dblCentavos.ToString().Trim(), 2);
                        strDezCentavo = Mid(dblCentavos.ToString().Trim(), 2, 1);

                        strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 0) ? " e " : " ");
                        strValorExtenso = strValorExtenso + fcn_Numero_Dezena1(Left(strDezCentavo, 1));

                        if ((dblCentavos.ToString().Trim().Length) > 2)
                        {
                            strNumero = Right((Decimal.Round(dblCentavos, 2)).ToString().Trim(), 1);
                            if (int.Parse(strNumero) > 0)
                            {
                                if (dblValorInteiro <= 0)
                                {
                                    if (Mid(strValorExtenso.Trim(), strValorExtenso.Trim().Length - 2, 1) == "e")
                                    {
                                        strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                    }
                                    else
                                    {
                                        strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                    }
                                }
                                else
                                {
                                    strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                }
                            }
                        }
                        strValorExtenso = strValorExtenso + " Centavos ";
                    }
                }
                if (dblValorInteiro < 1) strValorExtenso = Mid(strValorExtenso.Trim(), 2, strValorExtenso.Trim().Length - 2);
            }

            return strValorExtenso.Trim();
        }

        private static string fcn_Numero_Dezena0(string pstrDezena0)
        {
            ArrayList array_Dezena0 = new ArrayList();
            array_Dezena0.Add("Onze");
            array_Dezena0.Add("Doze");
            array_Dezena0.Add("Treze");
            array_Dezena0.Add("Quatorze");
            array_Dezena0.Add("Quinze");
            array_Dezena0.Add("Dezesseis");
            array_Dezena0.Add("Dezessete");
            array_Dezena0.Add("Dezoito");
            array_Dezena0.Add("Dezenove");

            return array_Dezena0[((int.Parse(pstrDezena0)) - 1)].ToString();
        }
        private static string fcn_Numero_Dezena1(string pstrDezena1)
        {
            ArrayList array_Dezena1 = new ArrayList();
            array_Dezena1.Add("Dez");
            array_Dezena1.Add("Vinte");
            array_Dezena1.Add("Trinta");
            array_Dezena1.Add("Quarenta");
            array_Dezena1.Add("Cinquenta");
            array_Dezena1.Add("Sessenta");
            array_Dezena1.Add("Setenta");
            array_Dezena1.Add("Oitenta");
            array_Dezena1.Add("Noventa");
            return array_Dezena1[Int16.Parse(pstrDezena1) - 1].ToString();
        }

        private static string fcn_Numero_Centena(string pstrCentena)
        {
            ArrayList array_Centena = new ArrayList();
            array_Centena.Add("Cem");
            array_Centena.Add("Duzentos");
            array_Centena.Add("Trezentos");
            array_Centena.Add("Quatrocentos");
            array_Centena.Add("Quinhentos");
            array_Centena.Add("Seiscentos");
            array_Centena.Add("Setecentos");
            array_Centena.Add("Oitocentos");
            array_Centena.Add("Novecentos");

            return array_Centena[((int.Parse(pstrCentena)) - 1)].ToString();
        }
        private static string fcn_Numero_Unidade(string pstrUnidade)
        {
            ArrayList array_Unidade = new ArrayList();

            array_Unidade.Add("Um");
            array_Unidade.Add("Dois");
            array_Unidade.Add("Três");
            array_Unidade.Add("Quatro");
            array_Unidade.Add("Cinco");
            array_Unidade.Add("Seis");
            array_Unidade.Add("Sete");
            array_Unidade.Add("Oito");
            array_Unidade.Add("Nove");

            return array_Unidade[(int.Parse(pstrUnidade) - 1)].ToString();
        }

        public static string Right(string param, int length)
        {
            if (param == "")
                return "";
            string result = param.Substring(param.Length - length, length);
            return result;
        }

        public static string Mid(string param, int startIndex, int length)
        {
            string result = param.Substring(startIndex, length);
            return result;
        }

        public static string Mid(string param, int startIndex)
        {
            string result = param.Substring(startIndex);
            return result;
        }
        public static int UltimoDiaMes(string pData)
        {
            int iRet = 30;
            if (Convert.ToDateTime(pData).Month == 2)
                iRet = Convert.ToDateTime("01/03/" + Convert.ToDateTime(pData).ToString("yyyy")).AddDays(-1).Day;
            else if (Convert.ToDateTime(pData).Month == 1 || Convert.ToDateTime(pData).Month == 3 || Convert.ToDateTime(pData).Month == 5 || 
                     Convert.ToDateTime(pData).Month == 7 || Convert.ToDateTime(pData).Month == 8 || Convert.ToDateTime(pData).Month == 10 ||
                     Convert.ToDateTime(pData).Month == 12)
                iRet = 31;
            return iRet;
        }
    }
}
