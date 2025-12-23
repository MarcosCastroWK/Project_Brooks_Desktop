using System;
using System.Web.UI.WebControls;
using SILCNegocios;
using LibSILC;


namespace SILC.Web.forms
{
    public partial class menu : System.Web.UI.UserControl
    {
        clsUsuarios oUsuario = new clsUsuarios();
        clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strIPUsuario = Request.UserHostAddress.Replace(".", "");
                LerSessao(strIPUsuario);

                if (LibSILC.geral.BancoUsado > 1)
                {
                    for (int i = 0; i < Menu1.Items.Count; i++)
                    {
                        if (Menu1.Items[i].Text == "Sair")
                        {
                            if (LibSILC.geral.BancoUsado != 4)
                                Menu1.Items[i].NavigateUrl = "~/forms/brooks/login.aspx?db=" + LibSILC.geral.BancoUsado;
                        }
                    }
                }
                if (oUsuario != null)
                {
                    if (oUsuario.Nome.ToLower() != "teixeira")
                    {
                        if (Menu1.Items[0].Text == "Cadastro")
                        {
                            for (int j = 0; j < Menu1.Items[0].ChildItems[3].ChildItems.Count; j++)
                            {
                                if (Menu1.Items[0].ChildItems[3].ChildItems[j].Value == "contratosresiduosteste")
                                {
                                    Menu1.Items[0].ChildItems[3].ChildItems[j].Text = "Uso exclusivo do desenvolvedor";
                                    Menu1.Items[0].ChildItems[3].ChildItems[j].Enabled = false;
                                }
                            }
                        }
                        if (Menu1.Items[7].Value == "Parametros")
                        {
                            if (Menu1.Items[7].ChildItems[8].Value == "ItensMenu")
                            {
                                Menu1.Items[7].ChildItems[8].Text = "Uso exclusivo do administrador";
                                Menu1.Items[7].ChildItems[8].Enabled = false;
                            }
                        }
                    }
                }
            }
        }
        protected void LerSessao(string pIp = "")
        {
            if (Session["oUsuario"] == null)
            {
                if (hifCodigo.Value != "" && hifCodigo.Value != null)
                {
                    oUsuario.Codigo = Convert.ToInt16(hifCodigo.Value);
                    oUsuario.CodigoEmpresa = Convert.ToInt16(hifCodigoEmpresa.Value);
                    oUsuario.Nome = hifNome.Value;
                    oUsuario.Senha = oUsuarioDados.PegaSenha(oUsuario.Nome, oUsuario.CodigoEmpresa);
                    Session["oUsuario"] = oUsuario;
                    geral.CodigoUsuarioAtual = oUsuario.Codigo;
                    geral.UsuarioAtual = hifNome.Value;
                }
            }
            if (Session["oUsuario"] != null)
            {
                oUsuario = (clsUsuarios)Session["oUsuario"];
                if (hifCodigo.Value == "")
                {
                    hifCodigo.Value = oUsuario.Codigo.ToString();
                    hifCodigoEmpresa.Value = oUsuario.CodigoEmpresa.ToString();
                    hifNome.Value = oUsuario.Nome;
                    geral.CodigoUsuarioAtual = oUsuario.Codigo;
                    geral.UsuarioAtual = hifNome.Value;
                }
            }
            if (Session[pIp] != null)
            {
                oUsuario = (clsUsuarios)Session[pIp];
                hifCodigo.Value = oUsuario.Codigo.ToString();
                hifCodigoEmpresa.Value = oUsuario.CodigoEmpresa.ToString();
                hifNome.Value = oUsuario.Nome;
                geral.CodigoUsuarioAtual = oUsuario.Codigo;
                geral.UsuarioAtual = hifNome.Value;
            }
        }

        protected void Menu1_MenuItemClick(object sender, System.Web.UI.WebControls.MenuEventArgs e)
        {
            string _path = @"\\servidor\WinSILC\temp\";
            string _pathArquivo = "";
            string strIPUsuario = Request.UserHostAddress.Replace(".", "");
            LerSessao(strIPUsuario);
            _pathArquivo = _path + strIPUsuario.Replace(":", "") + ".txt";
            if (Menu1.Items[0].ChildItems[0].Selected)
            {
                Response.Write(AbreNovaAba("Caminhoes.aspx"));
            }
            else if (Menu1.Items[0].ChildItems[1].ChildItems[0].Selected)
            {
                Response.Write(AbreNovaAba("Clientes_Incluir.aspx"));
            }
            else if (Menu1.Items[0].ChildItems[1].ChildItems[1].Selected)
            {
                Response.Write(AbreNovaAba("Clientes.aspx"));
            }
            else if (Menu1.Items[0].ChildItems[1].ChildItems[2].Selected)
            {
                Response.Write(AbreNovaAba("BloqueioFinanceiro.aspx"));
            }
            else if (Menu1.Items[0].ChildItems[2].Selected)
            {
                Response.Write(AbreNovaAba("Containeres.aspx"));
            }
            else if (Menu1.Items[0].ChildItems[3].ChildItems[0].Selected)
            {
                Response.Write(AbreNovaAba("Contratos_Cadastro.aspx"));
            }
            else if (Menu1.Items[0].ChildItems[3].ChildItems[1].Selected)
            {
                Response.Write(AbreNovaAba("Contratos_Alterar.aspx"));
            }
            else if (Menu1.Items[0].ChildItems[3].ChildItems[2].Selected)
            {
                Response.Write(AbreNovaAba("Contratos_Reajustes.aspx"));
            }
            else if (Menu1.Items[0].ChildItems[3].ChildItems[3].Selected)
            {
                Response.Write(AbreNovaAba("Contratos_Rescisao.aspx"));
            }
            else if (Menu1.Items[0].ChildItems[3].ChildItems[4].Selected)
            {
                Response.Write(AbreNovaAba("Contratos.aspx"));
            }
            else if (Menu1.Items[0].ChildItems[4].Selected)
            {
                Response.Write(AbreNovaAba("DestinoFinal.aspx"));
            }
            else if (Menu1.Items[0].ChildItems[5].Selected)
            {
                Response.Write(AbreNovaAba("IBAMA.aspx"));
            }
            else if (Menu1.Items[0].ChildItems[6].Selected)
            {
                Response.Write(AbreNovaAba("Motoristas.aspx"));
            }
            else if (Menu1.Items[0].ChildItems[7].Selected)
            {
                Response.Write(AbreNovaAba("Municipios.aspx"));
            }
            else if (Menu1.Items[0].ChildItems[8].Selected)
            {
                Response.Write(AbreNovaAba("Residuos.aspx"));
            }
            else if (Menu1.Items[1].ChildItems[1].Selected)
            {
                //Programacao
                //[codigousuario-usuario-opcaomenu].txt            
                System.IO.StreamWriter sw = new System.IO.StreamWriter(_pathArquivo);
                sw.WriteLine(RetorneDadoArquivo("", Menu1.Items[1].ChildItems[1].Value.ToString()));
                sw.Close();
            }
            else if (Menu1.Items[1].ChildItems[2].Selected)
            {
                //Programacao a partir do comercial
                Response.Write(AbreNovaAba("programacaoInsercao.aspx"));
            }
            else if (Menu1.Items[2].ChildItems[0].ChildItems[0].Selected)
            {
                //Locacao
                //[codigousuario-usuario-opcaomenu].txt            
                System.IO.StreamWriter sw = new System.IO.StreamWriter(_pathArquivo);
                sw.WriteLine(RetorneDadoArquivo("", Menu1.Items[2].ChildItems[0].ChildItems[0].Value.ToString()));
                sw.Close();
            }
            else if (Menu1.Items[2].ChildItems[0].ChildItems[1].Selected)
            {
                //[codigousuario-usuario-opcaomenu].txt
                //LocacaoProgramacao
                System.IO.StreamWriter sw = new System.IO.StreamWriter(_pathArquivo);
                sw.WriteLine(RetorneDadoArquivo("", Menu1.Items[2].ChildItems[0].ChildItems[1].Value.ToString()));
                sw.Close();
            }
            else if (Menu1.Items[2].ChildItems[0].ChildItems[2].Selected)
            {
                Response.Write(AbreNovaAba("TicketsPendentes.aspx"));
            }
            else if (Menu1.Items[2].ChildItems[0].ChildItems[3].Selected)
            {
                Response.Write(AbreNovaAba("MTRsPendentes.aspx"));
            }
            else if (Menu1.Items[2].ChildItems[0].ChildItems[4].Selected)
            {
                //[codigousuario-usuario-opcaomenu].txt            
                System.IO.StreamWriter sw = new System.IO.StreamWriter(_pathArquivo);
                sw.WriteLine(RetorneDadoArquivo("", Menu1.Items[2].ChildItems[0].ChildItems[4].Value.ToString()));
                sw.Close();
            }
            else if (Menu1.Items[2].ChildItems[2].Selected)
            {
                // notas fiscais
                //[codigousuario-usuario-opcaomenu].txt            
                System.IO.StreamWriter sw = new System.IO.StreamWriter(_pathArquivo);
                sw.WriteLine(RetorneDadoArquivo("", Menu1.Items[2].ChildItems[2].Value.ToString()));
                sw.Close();
            }
            else if (Menu1.Items[2].ChildItems[3].Selected)
            {
                Response.Write(AbreNovaAba("BlocosMTR.aspx"));
            }
            else if (Menu1.Items[2].ChildItems[4].Selected)
            {
                Response.Write(AbreNovaAba("MTRCanceladaTransbordo.aspx"));
            }
            else if (Menu1.Items[3].ChildItems[0].Selected)
            {
                Response.Write(AbreNovaAba("DTR.aspx"));
            }
            else if (Menu1.Items[3].ChildItems[1].Selected)
            {
                Response.Write(AbreNovaAba("DTR_Enviados.aspx"));
            }
            else if (Menu1.Items[3].ChildItems[2].Selected)
            {
                //DTR app
                //[codigousuario-usuario-opcaomenu].txt            
                System.IO.StreamWriter sw = new System.IO.StreamWriter(_pathArquivo);
                sw.WriteLine(RetorneDadoArquivo("", Menu1.Items[3].ChildItems[2].Value.ToString()));
                sw.Close();
            }
            else if (Menu1.Items[4].ChildItems[0].Selected)
            {
                //Importações
                //Relatório da MTR-e - IMA
                //[codigousuario-usuario-opcaomenu].txt            
                System.IO.StreamWriter sw = new System.IO.StreamWriter(_pathArquivo);
                sw.WriteLine(RetorneDadoArquivo("", Menu1.Items[4].ChildItems[0].Value.ToString()));
                sw.Close();
            }
            else if (Menu1.Items[4].ChildItems[1].Selected)
            {
                //Importações
                //Sistema IMA - Atualizar senhas
                //[codigousuario-usuario-opcaomenu].txt
                System.IO.StreamWriter sw = new System.IO.StreamWriter(_pathArquivo);
                sw.WriteLine(RetorneDadoArquivo("", Menu1.Items[4].ChildItems[1].Value.ToString()));
                sw.Close();
            }
            else if (Menu1.Items[4].ChildItems[2].Selected)
            {
                //Importações
                //Importar dados Radar
                //[codigousuario-usuario-opcaomenu].txt
                System.IO.StreamWriter sw = new System.IO.StreamWriter(_pathArquivo);
                sw.WriteLine(RetorneDadoArquivo("", Menu1.Items[4].ChildItems[2].Value.ToString()));
                sw.Close();
            }
            else if (Menu1.Items[4].ChildItems[3].Selected)
            {
                //Importações
                //Relatório da MTR-e - IMA para Conferência diária
                //[codigousuario-usuario-opcaomenu].txt
                System.IO.StreamWriter sw = new System.IO.StreamWriter(_pathArquivo);
                sw.WriteLine(RetorneDadoArquivo("", Menu1.Items[4].ChildItems[3].Value.ToString()));
                sw.Close();
            }
            else if (Menu1.Items[5].Selected)
            {
                //Atualização de Dados
                //[codigousuario-usuario-opcaomenu].txt
                if (geral.CodigoUsuarioAtual == 10)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(_pathArquivo);
                    sw.WriteLine(RetorneDadoArquivo("", Menu1.Items[5].Value.ToString()));
                    sw.Close();
                }
                else
                {
                    Response.Write("<script>alert('Acesso negado. Disponível somente para o desenvolvedor.');</script>");
                    Response.Flush();
                }
            }
            else if (Menu1.Items[6].Selected)
            {
                //Documentação Aplicável
                //[codigousuario-usuario-opcaomenu].txt
                System.IO.StreamWriter sw = new System.IO.StreamWriter(_pathArquivo);
                sw.WriteLine(RetorneDadoArquivo("", Menu1.Items[6].Value.ToString()));
                sw.Close();
            }
            else if (Menu1.Items[8].ChildItems[7].Selected)
            {
                //Relatórios
                //Relatório de Indicadores
                //[codigousuario-usuario-opcaomenu].txt
                System.IO.StreamWriter sw = new System.IO.StreamWriter(_pathArquivo);
                sw.WriteLine(RetorneDadoArquivo("", Menu1.Items[8].ChildItems[7].Value.ToString()));
                sw.Close();
            }
        }
        private string RetorneDadoArquivo(string _dadoArquivo, string _OpcaoMenu)
        {
            _dadoArquivo = _dadoArquivo + geral.CodigoUsuarioAtual + "-";
            _dadoArquivo = _dadoArquivo + geral.UsuarioAtual + "-";
            _dadoArquivo = _dadoArquivo + _OpcaoMenu;
            return _dadoArquivo;
        }
        protected string AbreNovaAba(string pHome)
        {
            string sRet = "<script>";
            sRet = sRet + "window.open('" + pHome + "', '_blank')";
            sRet = sRet + "</script>";
            return sRet;
        }
    }

}