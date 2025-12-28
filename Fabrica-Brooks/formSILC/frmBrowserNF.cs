using System;
using System.Windows.Forms;
using System.IO;
using LibSILC;
using SILCNegocios;
using System.Text;
using System.Net;
using RestSharp;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace formSILC
{
    public partial class frmBrowserNF : Form
    {
        private LibSILC.clsSenhaIPMDados oSenhaIPM = new LibSILC.clsSenhaIPMDados();
        private clsClientes oCliente = new clsClientes();
        private clsClienteDados oClienteDados = new clsClienteDados();
        private clsEnderecos oEndereco = new clsEnderecos();
        private clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
        private clsMunicipios oMunicipio = new clsMunicipios();
        private clsMunicipiosDados oMunicipioDados = new clsMunicipiosDados();

        public string cliente1Codigo;
        public string MotivoCancelamento;
        public string ValorLiquido;
        public string Parcelas;
        public string DataVencimento;
        public string DiasEntreVctos;
        public string CodigoClienteQueVaiEmail;

        string _Url = "";

        public frmBrowserNF()
        {
            InitializeComponent();
        }
        private void butConsultaNF_Click(object sender, EventArgs e)
        {
            if (intNNF.VALOR.Text == "")
            {
                MessageBox.Show("Número da Nota Fiscal inválido!");
                intNNF.VALOR.Text = "";
                intNNF.Focus();
            }
            else
            {
                try
                {
                    if (butConsultaNF.Text == "Enviar RPS IPM")
                        butConsultaNF.Enabled = false;

                    if (butConsultaNF.Text.IndexOf("Consulta") > -1)
                    {
                        GeraArquivoParaConsultaNF();
                    }
                }
                finally
                {
                    //Aqui dispara requisição para integração com a prefeitura
                    //butConsultaNF.Enabled = false;
                    //ChamadaParaIPM();
                }
            }
        }

        private string GeraArquivoParaConsultaNF()
        {
            string sInfAssinar;
            sInfAssinar = "";
            sInfAssinar = sInfAssinar + "<nfse>";
            sInfAssinar = sInfAssinar + "<pesquisa>";
            sInfAssinar = sInfAssinar + "<numero>" + intNNF.VALOR.Text + "</numero>";
            sInfAssinar = sInfAssinar + "<serie_nfse>1</serie_nfse>";
            sInfAssinar = sInfAssinar + "<cadastro>1609</cadastro>";
            sInfAssinar = sInfAssinar + "</pesquisa>";
            sInfAssinar = sInfAssinar + "</nfse>";

            StreamWriter writer = new StreamWriter("C:\\eletron\\work\\ConsultaNFSe.xml");
            // C:\eletron\work
            writer.WriteLine(sInfAssinar);

            //Fechando o arquivo
            writer.Close();

            //Limpando a referencia dele da memória
            writer.Dispose();


            return sInfAssinar;
        }

        private void frmBrowserNF_Load(object sender, EventArgs e)
        {
            if (butConsultaNF.Visible == false)
            {

            }
        }
        private string PegaHTML_Consulta_Ipm(string pCons)
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
            //s = s + " if( (f.value == '') || (f.value != 'C:\\eletron\\work\\ConsultaNFSe.xml') ) ";
            //s = s + " { alert('Selecione o arquivo ConsultaNFSe.xml'); ";
            //s = s + "   var fn=document.getElementById('FormName');";
            //s = s + "   location.reload();";
            //s = s + "   return;";
            //s = s + " }";
            s = s + "}</script>";
            s = s + "</head>";
            s = s + "<body>";

            //https://ws-palhoca.atende.net:7443/atende.php?pg=rest&service=WNERestServiceNFSe&Cidade=padrao
            //http://sync.nfs-e.net/datacenter/include/nfw/importa_nfw/nfw_import_upload.php
            s = s + "  <form name='FormName' action='https://ws-palhoca.atende.net:7443/atende.php?pg=rest&service=WNERestServiceNFSe&Cidade=padrao' method='post' enctype='multipart/form-data'>";
            s = s + "    <table>";
            s = s + "      <tr>";
            s = s + "        <td colspan='2' height='10'> </td>";
            s = s + "      </tr>";
            s = s + "      <tr>";
            s = s + "        <td class='titulo_table'> Login </td>";
            s = s + "        <td><input id='login' name='login' type='text' value='03938048000133' /></td>";
            s = s + "      </tr>";
            s = s + "      <tr>";
            s = s + "        <td class='titulo_table'> Senha </td>";
            string usen = oSenhaIPM.PegaUltimaSenha();
            s = s + "        <td><input id='senha' name='senha' type='password' value='" + usen + "' /></td>";
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
            s = s + "        <input type='submit'value='Consultar' onClick='x()' style='width:87px;border:1px #808080 solid' /> &nbsp;";
            s = s + "        <input type='reset' value='Limpar' style='width:87px;border:1px #808080 solid' />";
            s = s + "        </td>";
            s = s + "      </tr>";
            s = s + "    </table>";
            s = s + "  </form>";
            s = s + "</body>";
            s = s + "</html>";
            return s;
        }

        private string PegaHTML_Ipm()
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
            s = s + "}</script>";
            s = s + "</head>";
            s = s + "<body>";

            s = s + "  <form name='FormName' action='http://sync.nfs-e.net/datacenter/include/nfw/importa_nfw/nfw_import_upload.php' method='post' enctype='multipart/form-data'>";
            s = s + "    <table>";
            s = s + "      <tr>";
            s = s + "        <td colspan='2' height='10'> </td>";
            s = s + "      </tr>";
            s = s + "      <tr>";
            s = s + "        <td class='titulo_table'> Login </td>";
            s = s + "        <td><input id='login' name='login' type='text' value='03938048000133' /></td>";
            s = s + "      </tr>";
            s = s + "      <tr>";
            s = s + "        <td class='titulo_table'> Senha </td>";
            string usen = oSenhaIPM.PegaUltimaSenha();
            s = s + "        <td><input id='senha' name='senha' type='password' value='" + usen + "' /></td>";
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
            s = s + "        <input type='submit'value='Enviar NF para IPM' onClick='x()' style='width:150px;border:1px #808080 solid' /> &nbsp;";
            s = s + "        <input type='reset' value='Limpar' style='width:87px;border:1px #808080 solid' />";
            s = s + "        </td>";
            s = s + "      </tr>";
            s = s + "    </table>";
            s = s + "  </form>";
            s = s + "</body>";

            s = s + "</html>";
            return s;
        }
       
        private void btnEnviarParaRadar_Click(object sender, EventArgs e)
        {
            try
            {
                // enviar emissão para o Radar
                geral.NotaFiscalEnviada = true;
                try
                {

                    EnviarEmailNotaFiscalParaCliente(false, Convert.ToInt32(intNNF.VALOR.Text));
                }
                finally
                {
                    //excluir rps.xml
                    File.Delete("c:\\eletron\\work\\rps.xml");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {                
                this.Close();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // excluir NF e fechar form e dar um refresh no form de NF
            DialogResult drresult = new DialogResult();
            drresult = MessageBox.Show("Têm certeza que a Nota NÃO foi para o IPM?", "Nota não foi para o IPM (Sim).", MessageBoxButtons.YesNo);
            if (drresult == DialogResult.Yes)
            {
                geral.NotaFiscalEnviada = false;
                this.Close();
            }
        }
        private string EnviarEmailNotaFiscalParaCliente(bool pCancelamento, int pNumeroNF)
        {
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587; // 587; // 465;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("financeiro@brooksambiental.com.br", "rzwfddiceigxofqe");
            //smtp.Credentials = new System.Net.NetworkCredential("financeiro@brooksambiental.com.br", "edidfdimasqccvva");

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            if (pCancelamento)
                mail.Subject = "Cancelamento Nota Fiscal nº " + pNumeroNF + " BROOKS "; // Assunto da mensagem
            else
                mail.Subject = "Nota Fiscal nº " + pNumeroNF + " BROOKS "; // Assunto da mensagem

            string sAutenticidade = "";
            string s = "";
            if (txtRetorno.Text.IndexOf("cod_verificador_autenticidade") > -1)
            {
                string x = txtRetorno.Text.Substring(txtRetorno.Text.IndexOf("cod_verificador_autenticidade: "), 71);
                sAutenticidade = x.Replace("cod_verificador_autenticidade: ", "");
            }
            s = s + "CLICK NO LINK ABAIXO PARA VISUALIZAR SUA NOTA FISCAL \n";
            s = s + "https://palhoca.atende.net/autoatendimento/servicos/consulta-de-autenticidade-de-nota-fiscal-eletronica-nfse/detalhar/1/identificador/" + sAutenticidade + " \n";

            s = s + "Dados do Emissor \n";
            geral.oEmpresa = geral.oEmpresaDados.PegaDados(geral.oEmpresa, 1);
            s = s + geral.oEmpresa.Nome + " \n";
            s = s + geral.oEmpresa.Endereco + " - " + geral.oEmpresa.Cidade + " - " + geral.oEmpresa.UF + " \n";
            s = s + geral.oEmpresa.Telefones + " \n";
            s = s + geral.oEmpresa.CNPJ_CPF + " \n";
            s = s + " \n";
            if (cliente1Codigo != "")
            {
                if (cliente1Codigo != CodigoClienteQueVaiEmail && CodigoClienteQueVaiEmail != "" && CodigoClienteQueVaiEmail != "0")
                {
                    oCliente = oClienteDados.PegaDados(oCliente, Convert.ToInt32(CodigoClienteQueVaiEmail));
                    oEndereco = oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(CodigoClienteQueVaiEmail), 1, 0);
                    oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);
                }
                else
                {
                    oCliente = oClienteDados.PegaDados(oCliente, Convert.ToInt32(cliente1Codigo));
                    oEndereco = oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(cliente1Codigo), 1, 0);
                    oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);
                }
                s = s + "Dados do cliente \n";
                s = s + oCliente.Nome2 + " \n";
                s = s + oEndereco.endereco;
                if (oEndereco.Numero != "" && oEndereco.Numero != null)
                    s = s + ", n° " + oEndereco.Numero.ToString();
                if (oEndereco.Complemento != "")
                    s = s + " " + oEndereco.Complemento;
                s = s + " \n";
                if (oEndereco.CEP != "" && oEndereco.CEP != null)
                    s = s + oEndereco.CEP + " - ";
                s = s + oMunicipio.Nome + " - " + oMunicipio.UF + " \n";
                if (oEndereco.Fone1.Length > 0)
                {
                    if (oEndereco.DDD1 > 0)
                    {
                        s = s + "(" + oEndereco.DDD1 + ") ";
                        s = s + oEndereco.Fone1.ToString() + " \n";
                    }
                    else
                        s = s + oEndereco.Fone1.ToString() + " \n";
                }
                if (oEndereco.Fone2.Length > 0)
                {
                    if (oEndereco.DDD2 > 0)
                    {
                        s = s + "(" + oEndereco.DDD2 + ") ";
                        s = s + oEndereco.Fone2.ToString() + " \n";
                    }
                    else
                        s = s + oEndereco.Fone2.ToString() + " \n";
                }
                s = s + geral.RetiraLetras(geral.oEmpresa.CNPJ_CPF) + " \n";

                s = s + " \n";

                string[] oSplitMail;
                if (geral.UsuarioAtual.ToLower() == "teixeira")
                {
                    mail.To.Add("megasis.edson@gmail.com");
                }

                if (cliente1Codigo != CodigoClienteQueVaiEmail && CodigoClienteQueVaiEmail != "")
                {
                    clsEnderecos oEnderecoDiferente = new clsEnderecos();
                    oEnderecoDiferente = oEnderecoDados.PegaDados(oEnderecoDiferente, Convert.ToInt32(CodigoClienteQueVaiEmail), 2, 0);
                    // informar e-mail(s) do cliente cadastrado - quando o faturamento é em outro cnpj - endereço de coleta
                    oSplitMail = oEnderecoDiferente.email.Replace(",", ";").Split(";"[0]);
                    for (int i = 0; i <= oSplitMail.Length - 1; i++)
                    {
                        mail.To.Add(oSplitMail[i].Trim()); //e-mail
                    }
                }
                else if (CodigoClienteQueVaiEmail == "" || CodigoClienteQueVaiEmail == null)
                {
                    // informar e-mail(s) do cliente cadastrado - quando é faturado no MESMO cnpj
                    oSplitMail = oEndereco.email.Replace(",", ";").Split(";"[0]);
                    for (int i = 0; i <= oSplitMail.Length - 1; i++)
                    {
                        mail.To.Add(oSplitMail[i].Trim()); //e-mail
                    }
                }
            }

            if (pCancelamento)
            {
                s = s + " \n";
                s = s + "Cancelamos Nota Fiscal nº " + pNumeroNF + " \n";
                s = s + " \n";
                s = s + "Motivo: " + MotivoCancelamento + " \n";
                s = s + " \n";
            }

            decimal ValorParcela = 0;
            DateTime Vcto2Parcela;
            decimal SomaTotalSemImpostos = 0;

            SomaTotalSemImpostos = Convert.ToDecimal(ValorLiquido);

            s = s + "Valor total .: " + SomaTotalSemImpostos.ToString("N2") + " \n";
            s = s + " \n";

            if (Parcelas != "")
                ValorParcela = SomaTotalSemImpostos / Convert.ToInt16(Parcelas);
            else
                ValorParcela = SomaTotalSemImpostos;

            Vcto2Parcela = Convert.ToDateTime(DataVencimento).AddDays(Convert.ToInt32(DiasEntreVctos));

            string sParcs = "";
            sParcs = sParcs + " Parcela     Vencimento    Valor \n";

            for (int _ip = 1; _ip <= Convert.ToDecimal(Parcelas); _ip++)
            {
                sParcs = sParcs + " \n";

                sParcs = sParcs + Convert.ToInt32(pNumeroNF).ToString("00000");

                sParcs = sParcs + "-" + _ip.ToString("0");
                if (_ip == 2)
                    sParcs = sParcs + " - " + Vcto2Parcela;
                else if (_ip >= 3)
                    sParcs = sParcs + " - " + Vcto2Parcela.AddDays(Convert.ToInt16(DiasEntreVctos));
                else if (Parcelas == "1")
                    sParcs = sParcs + " - " + DataVencimento;
                else if (_ip == 1)
                    sParcs = sParcs + " - " + DataVencimento;

                sParcs = sParcs + " - " + ValorParcela.ToString("N2").Replace(".", "");

                sParcs = sParcs + " \n";
            }
            sParcs = sParcs + " \n";

            s = s + sParcs + " \n";

            s = s + " \n";
            s = s + "Obrigado. \n";

            mail.From = new System.Net.Mail.MailAddress("financeiro@brooksambiental.com.br", "Financeiro BROOKS", Encoding.UTF8);

            mail.Body = s;
            mail.Priority = System.Net.Mail.MailPriority.Normal;

            try
            {
                smtp.Send(mail);
                //if (MotivoCancelamento == "tstedson" || geral.UsuarioAtual.ToLower() == "teixeira")
                //    MessageBox.Show("e-mail enviado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            return "";
        }

        private void frmBrowserNF_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MotivoCancelamento == "tstedson" || geral.UsuarioAtual.ToLower() == "teixeira")
            {
                //EnviarEmailNotaFiscalParaCliente(false, Convert.ToInt32(intNNF.VALOR.Text));
            }
        }

        private void ChamadaParaIPM()
        {
            var username = "03.938.048/0001-33";
            var password = "Brooks24!";

            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));

            var client = new RestClient("https://ws-palhoca.atende.net:7443/atende.php?pg=rest&service=WNERestServiceNFSe&cidade=padrao");
            client.Timeout = -1;
            //client.Timeout = 120;
            var request = new RestRequest(Method.POST);
            //request.Method = Method.POST; 
            
            //request.AddHeader("Authorization", "Basic MDMuOTM4LjA0OC8wMDAwMS0zMzpCUipvb2tz");
            request.AddHeader("Authorization", "Basic " + encoded);
            request.AddHeader("ContentType", "multipart/form-data");
            request.AddHeader("Cookie", "PHPSESSID=bhqqlfh5l682i04b1thdfsn4m2; cidade=padrao");
            
            if (File.Exists(@"C:\eletron\work\RPS.xml") && butConsultaNF.Text.IndexOf("Consulta") == -1) 
               request.AddFile("f1", @"C:\eletron\work\RPS.xml");

            if (File.Exists(@"C:\eletron\work\ConsultaNFSe.xml") && butConsultaNF.Text.IndexOf("Consulta") > -1)
                request.AddFile("f1", @"C:\eletron\work\ConsultaNFSe.xml");

            IRestResponse response = client.Execute(request);

            txtRetorno.Text = response.Content;

            try
            {
                txtRetorno.SaveFile("c:\\formSILC\\nf.xml", RichTextBoxStreamType.PlainText);
            }
            finally
            {
                txtRetorno.Clear();
                XmlTextReader xmlReader = new XmlTextReader("c:\\formSILC\\nf.xml");
                string _no = "";
                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            //txtRetorno.Text += xmlReader.Name;
                            _no = xmlReader.Name;
                            break;
                        case XmlNodeType.Text:
                            txtRetorno.Text += (_no + ": " + xmlReader.Value + Environment.NewLine);
                            if (_no == "link_nfse")
                            {
                                _Url = xmlReader.Value;
                            }
                            break;
                    }
                }
                xmlReader.Close();
            }
        }

        private void butNavegadorChrome_Click(object sender, EventArgs e)
        {
            if (_Url != "" && _Url != null)
            {
                System.Diagnostics.Process.Start("chrome.exe", _Url);
            }
            else if (_Url == "" || _Url == null)
            {
                MessageBox.Show("Consultar primeiro.");
            }
        }
    }
}