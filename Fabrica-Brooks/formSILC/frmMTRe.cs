using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Gecko;

namespace formSILC
{
    public partial class frmMTRe : Form
    {
        public frmMTRe()
        {
            InitializeComponent();
            //Xpcom.Initialize("Firefox");
            //geckoWebBrowser1.Navigate("mtr.ima.sc.gov.br");
        }
        private void PrintDom()
        {
            //GeckoHtmlElement elemHtml = geckoWebBrowser1.Document.GetHtmlElementById("txtCnpj");
            //if (elemHtml != null)
            //{
            //    if (elemHtml.GetAttribute("id") == "txtCnpj")
            //    {
            //        elemHtml.SetAttribute("value", "03.938.048/0001-33");
            //    }

            //    elemHtml = geckoWebBrowser1.Document.GetHtmlElementById("txtSenha");
            //    if (elemHtml.GetAttribute("id") == "txtSenha")
            //    {
            //        elemHtml.SetAttribute("value", "b10757");
            //    }
            //}
        }

        //private void geckoWebBrowser1_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        //{
        //    PrintDom();
        //}
    }
}
