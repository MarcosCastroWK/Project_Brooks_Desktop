using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gecko;
using LibSILC;

namespace formSILC
{
    public partial class frmMTReGecko45 : Form
    {
        public frmMTReGecko45()
        {
            InitializeComponent();
            Xpcom.Initialize("Firefox");
            geckoWebBrowser1.Navigate("http://mtr.ima.sc.gov.br");
        }

        private void PrintDom()
        {
            GeckoHtmlElement elemHtml = geckoWebBrowser1.Document.GetHtmlElementById("txtCnpj");
            if (elemHtml != null)
            {
                if (elemHtml.GetAttribute("id") == "txtCnpj")
                {
                    elemHtml.SetAttribute("value", geral.RetiraLetras(LibSILC.geral.cnpj_cpf));
                }

                elemHtml = geckoWebBrowser1.Document.GetHtmlElementById("txtSenha");
                if (elemHtml.GetAttribute("id") == "txtSenha")
                {
                    elemHtml.SetAttribute("value", geral.senhamtre);
                    try
                    {
                        elemHtml = geckoWebBrowser1.Document.GetHtmlElementById("txtCnpj");
                        elemHtml.Focus();
                    }
                    finally
                    {
                        elemHtml = geckoWebBrowser1.Document.GetHtmlElementById("txtSenha");
                        elemHtml.Focus();
                    }
                }
            }
        }

        private void PrintMTReModelo()
        {
            SILCNegocios.clsClientes oCliente = new SILCNegocios.clsClientes();
            clsClienteDados oClienteDados = new clsClienteDados();
            oCliente = oClienteDados.PegaDados(oCliente, geral.CodigoCliente);

            SILCNegocios.clsEnderecos oEndereco = new SILCNegocios.clsEnderecos();
            clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
            oEndereco = oEnderecoDados.PegaDados(oEndereco, oCliente.Codigo, 2, 0);

            SILCNegocios.clsFuncionarios oFuncionario = new SILCNegocios.clsFuncionarios();
            clsFuncionarioDados oFuncionarioDados = new clsFuncionarioDados();
            oFuncionario = oFuncionarioDados.PegaDados(oFuncionario, geral.CodigoMotorista);

            SILCNegocios.clsCaminhoes oCaminhao = new SILCNegocios.clsCaminhoes();
            clsCaminhoesDados oCaminhaoDados = new clsCaminhoesDados();
            oCaminhao = oCaminhaoDados.PegaDados(oCaminhao, geral.CodigoCaminhao);

            GeckoHtmlElement elemHtml = geckoWebBrowser1.Document.GetHtmlElementById("txtGeradorRespExpedicao");
            if (elemHtml != null)
            {             
                
                if (elemHtml.GetAttribute("id") == "txtGeradorRespExpedicao")
                {
                    elemHtml.SetAttribute("value", oEndereco.Contato);
                }
                elemHtml = geckoWebBrowser1.Document.GetHtmlElementById("txtGeradorRespCargo");
                if (elemHtml.GetAttribute("id") == "txtGeradorRespCargo")
                {
                    elemHtml.SetAttribute("value", ""); 
                }
                elemHtml = geckoWebBrowser1.Document.GetHtmlElementById("txtTransportadorNomeMotorista");
                if (elemHtml.GetAttribute("id") == "txtTransportadorNomeMotorista")
                {
                    elemHtml.SetAttribute("value", oFuncionario.Nome); 
                }
                elemHtml = geckoWebBrowser1.Document.GetHtmlElementById("txtTransportadorPlacaVeiculo");
                if (elemHtml.GetAttribute("id") == "txtTransportadorPlacaVeiculo")
                {
                    elemHtml.SetAttribute("value", oCaminhao.Placas); 
                }
                elemHtml = geckoWebBrowser1.Document.GetHtmlElementById("txtTransportadorDataExpedicao");
                if (elemHtml.GetAttribute("id") == "txtTransportadorDataExpedicao")
                {
                    elemHtml.SetAttribute("value", geral.DataProgramada);
                }
            }
        }

        private void GeckoWebBrowser1_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            PrintDom();
            PrintMTReModelo();
        }
    }
}
