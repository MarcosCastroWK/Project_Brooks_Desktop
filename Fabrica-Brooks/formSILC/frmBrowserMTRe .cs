using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace formSILC
{
    public partial class frmBrowserMTRe : Form
    {
        public LibSILC.clsSenhaIPMDados oSenhaIPM = new LibSILC.clsSenhaIPMDados();

        public frmBrowserMTRe()
        {
            InitializeComponent();
        }

        private void butConsultaNF_Click(object sender, EventArgs e)
        {
           // if (intNNF.VALOR.Text == "")
           // {
           //     MessageBox.Show("Número da MTR-e inválido!");
           //     intNNF.VALOR.Text = "";
           //     intNNF.Focus();
           // }

            //else
            //{
                string scons = "";

                scons = GeraArquivoParaConsulta();

                webBrowser1.DocumentText = PegaHTML_Consulta(scons);

                webBrowser1.Refresh();
            //}
        }

        private string GeraArquivoParaConsulta()
        {
            string sInfAssinar;
            sInfAssinar = "";
            sInfAssinar = sInfAssinar + "<nfse>";
            sInfAssinar = sInfAssinar + "<pesquisa>";
            sInfAssinar = sInfAssinar + "<numero>+ intNNF.VALOR.Text + </numero>";
            sInfAssinar = sInfAssinar + "<serie>1</serie>";
            sInfAssinar = sInfAssinar + "<cadastro>1609</cadastro>";
            sInfAssinar = sInfAssinar + "</pesquisa>";
            sInfAssinar = sInfAssinar + "</nfse>";

            StreamWriter writer = new StreamWriter(Path.GetDirectoryName("c:\\") + "ConsultaMTRe.xml");

            writer.WriteLine(sInfAssinar);

            //Fechando o arquivo
            writer.Close();

            //Limpando a referencia dele da memória
            writer.Dispose();

            return sInfAssinar;
        }

        private void frmBrowserNF_Load(object sender, EventArgs e)
        {
            if (butConsulta.Visible == false)
            {
                // quando for NFs
                webBrowser1.DocumentText = PegaHTML(); 
                webBrowser1.Refresh();
            }
        }

        private string pegaJSON_Consulta()
        {
            string sRet = "";
            sRet = sRet + "{";
            sRet = sRet + "    'manifestoCodigo': 1712036960, ";
            sRet = sRet + "'cnpGerador': '11111100000100', ";
            sRet = sRet + "'cnpTransportador': '22222000000200', ";
            sRet = sRet + "'cnpDestinador': '33333333000100', ";
            sRet = sRet + "'login': '11111100000100', ";
            sRet = sRet + "'senha': 'senha_fatma'";
            sRet = sRet + "}";
            return sRet;
        }

        private string PegaHTML_Consulta(string pCons)
        {
            string s = "";
            s = s + "<html>";
            s = s + "<head>";
            s = s + "  <title></title>";
            s = s + "  <style type='text/css'>";
            s = s + "  span{";
            s = s + "    color:red;";
            s = s + "    width:75px;";
            s = s + "  }";
            s = s + "  .titulo_table{";
            s = s + "    width:75px;";
            s = s + "    text-align:right;";
            s = s + "    padding-right:10px;";
            s = s + "  }";
            s = s + "  table{";
            s = s + "    border:1px #808080 solid;";
            s = s + "    background-color:#EEEEEE;";
            s = s + "    font-size:12px;";
            s = s + "    font-family:arial;";
            s = s + "  }";
            s = s + "  a{";
            s = s + "    text-decoration:none;";
            s = s + "    color:blue;";
            s = s + "  }";
            s = s + "  li{";
            s = s + "    list-style: none;";
            s = s + "    height:22px;";
            s = s + "    font-family:arial;";
            s = s + "    font-size:12px;";
            s = s + "    color:#ACACAC;";
            s = s + "  }";
            s = s + "  </style>";
            s = s + "<script>";
            s = s + "function x() ";
            s = s + "{";
            s = s + " var f=document.getElementById('f1');";
            //s = s + " if( (f.value == '') || (f.value != 'C:\\fakepath\\ConsultaNFSe.xml') ) ";
            //s = s + " { alert('Selecione o arquivo ConsultaNFSe.xml'); ";
            //s = s + "   var fn=document.getElementById('FormName');";
            //s = s + "   location.reload();";
            //s = s + "   return;";
            //s = s + " }";
            s = s + "}</script>";
            s = s + "</head>";
            s = s + "<body>";

            s = s + "  <form name='FormName' action='http://mtr.ima.sc.gov.br/webservice/verificaSituacaoManifesto' method='post' enctype='multipart/form-data'>";
            s = s + "    <table>";
            s = s + "      <tr>";
            s = s + "        <td colspan='2' height='10'> </td>";
            s = s + "      </tr>";
            s = s + "      <tr>";
            s = s + "        <td class='titulo_table'> Login </td>";
            s = s + "        <td><input name='login' type='text' value='03938048000133' /></td>";
            s = s + "      </tr>";
            s = s + "      <tr>";
            s = s + "        <td class='titulo_table'> Senha </td>";
            string usen = oSenhaIPM.PegaUltimaSenha();
            s = s + "        <td><input name='senha' type='password' value='" + usen + "' /></td>";
            s = s + "      </tr>";
            s = s + "      <tr>";
            s = s + "        <td class='titulo_table'> Arquivo XML de Consulta </td>";
            s = s + "        <td><input type='file' id='f1' name='f1'></input></td>";
            s = s + "      </tr>";
            s = s + "      <tr>";
            s = s + "        <td colspan='2' height='15'> </td>";
            s = s + "      </tr>";
            s = s + "      <tr>";
            s = s + "        <td colspan='2' height='1'> <hr size='1'> </td>";
            s = s + "      </tr>";
            s = s + "      <tr>";
            s = s + "        <td align='right' colspan='2'>";
            s = s + "        <input type='submit' value='Consultar' onClick='x()' style='width:87px;border:1px #808080 solid' /> &nbsp;";
            s = s + "        <input type='reset' value='Limpar'  style='width:87px;border:1px #808080 solid' />";
            s = s + "        </td>";
            s = s + "      </tr>";
            s = s + "    </table>";
            s = s + "  </form>";
            s = s + "</body>";

            s = s + "</html>";
            return s;
        }

        private string PegaHTML()
        {
            string s = "";
            s = s + "<html> \n";
            s = s + "<head> \n";
            s = s + "  <title></title> \n";
            s = s + "  <style type='text/css'> \n";
            s = s + "  span{ \n";
            s = s + "    color:red; \n";
            s = s + "    width:75px; \n";
            s = s + "  } \n";
            s = s + "  .titulo_table{ \n";
            s = s + "    width:75px; \n";
            s = s + "    text-align:right; \n";
            s = s + "    padding-right:10px; \n";
            s = s + "  } \n";
            s = s + "  table{ \n";
            s = s + "    border:1px #808080 solid; \n";
            s = s + "    background-color:#EEEEEE; \n";
            s = s + "    font-size:12px; \n";
            s = s + "    font-family:arial; \n";
            s = s + "  } \n";
            s = s + "  a{ \n";
            s = s + "    text-decoration:none; \n";
            s = s + "    color:blue; \n";
            s = s + "  } \n";
            s = s + "  li{ \n";
            s = s + "    list-style: none; \n";
            s = s + "    height:22px; \n";
            s = s + "    font-family:arial; \n";
            s = s + "    font-size:12px; \n";
            s = s + "    color:#ACACAC; \n";
            s = s + "  } \n";
            s = s + "  </style> \n";

            s = s + "<script> \n";
            s = s + "function x() ";
            s = s + "{";
            s = s + " var f=document.getElementById('f1'); \n";
            s = s + " var env=document.getElementById('btnEnviar'); \n";
            s = s + " var limpar=document.getElementById('btnLimpar'); \n";
            s = s + " if( (f.value == '') || (f.value != 'C:\\fakepath\\rps.xml') )  \n";
            s = s + " { alert('Selecione o arquivo rps.xml');  \n";
            s = s + "   var fn=document.getElementById('FormName'); \n";
            s = s + "   location.reload(); \n";
            s = s + "   return; \n";
            s = s + " } else  \n";
            s = s + " {   \n";
            s = s + "    var fn=document.getElementById('FormName'); \n";
            s = s + "    env.style.display='none'; \n";
            s = s + "    limpar.style.display='none'; \n";
            s = s + " }   \n";
            s = s + "}</script> \n";

            s = s + "</head> \n";
            s = s + "<body> \n";

            s = s + "  <form name='FormName' action='http://mtr.ima.sc.gov.br/webservice/verificaSituacaoManifesto' method='post' enctype='multipart/form-data'> \n";
            s = s + "    <table> \n";
            s = s + "      <tr> \n";
            s = s + "        <td colspan='2' height='10'> </td> \n";
            s = s + "      </tr> \n";
            s = s + "      <tr> \n";
            s = s + "        <td class='titulo_table'> Login </td> \n";
            s = s + "        <td><input name='login' type='text' value='03938048000133' /></td> \n";
            s = s + "      </tr> \n";
            s = s + "      <tr> \n";
            s = s + "        <td class='titulo_table'> Senha </td> \n";
            s = s + "        <td><input name='senha' type='password' value='" + oSenhaIPM.PegaUltimaSenha() + "' /></td> \n";
            s = s + "      </tr> \n";
            s = s + "      <tr> \n";
            s = s + "        <td class='titulo_table'> Arquivo </td> \n";
            s = s + "        <td><input type='file' value='Send' id='f1' name='f1' /></td> \n";
            s = s + "      </tr> \n";
            s = s + "      <tr> \n";
            s = s + "        <td colspan='2' height='15'> </td> \n";
            s = s + "      </tr> \n";
            s = s + "      <tr> \n";
            s = s + "        <td colspan='2' height='1'> <hr size='1'> </td> \n";
            s = s + "      </tr> \n";
            s = s + "      <tr> \n";
            s = s + "        <td align='center' colspan='2' width='100%'> \n";
            s = s + "          <input id='btnEnviar' name='btnEnviar' type='submit' value='Enviar NF para IPM' onClick='x();' style='width:150px;border:1px #808080 solid' /> &nbsp; \n";
            s = s + "          <input id='btnLimpar' name='btnLimpar' type='reset' value='Limpar'  style='width:87px;border:1px #808080 solid' /> \n";
            s = s + "        </td> \n";
            s = s + "      </tr> \n";
            s = s + "    </table> \n";
            s = s + "  </form> \n";
            s = s + "</body> \n";
            s = s + "</html> \n";
            return s;
        }
    }
}
