using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SILC.Web.forms.brooks
{
    public partial class contato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //
        }
        protected void btnProcurar_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text != "")
            {
                string eMailQuemEnvia = "";
                string eMailQuemEnvia2 = "";
                string eMailQueLoga = "comercial3@brooksambiental.com.br";
                string SenhaQueLoga = "gef*8855";
                if (ddlAreaContato.Text == "Geral")
                {
                    eMailQuemEnvia = "brooks@brooksambiental.com.br";
                }
                else if (ddlAreaContato.Text == "Comercial")
                {
                    eMailQuemEnvia = "brooks@brooksambiental.com.br";
                }
                else if (ddlAreaContato.Text == "Tecnica")
                {
                    eMailQuemEnvia = "brooks@brooksambiental.com.br";
                }
                else if (ddlAreaContato.Text == "Coletas")
                {
                    eMailQuemEnvia = "logistica@brooksambiental.com.br";
                    eMailQuemEnvia2 = "cris@brooksambiental.com.br";
                }
                else if (ddlAreaContato.Text.ToLower() == "financeiro")
                {
                    eMailQuemEnvia = "financeiro@brooksambiental.com.br";
                }
                else if (ddlAreaContato.Text == "Diretoria")
                {
                    eMailQuemEnvia = "sergio@brooksambiental.com.br";
                }
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential(eMailQueLoga, SenhaQueLoga);

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                mail.Sender = new System.Net.Mail.MailAddress(eMailQueLoga, "Contato Home BROOKS");
                mail.From = new System.Net.Mail.MailAddress(eMailQuemEnvia, "Contato Home BROOKS");
                
                mail.To.Add(new System.Net.Mail.MailAddress(eMailQuemEnvia, "Contato Home BROOKS"));
                if (eMailQuemEnvia2 != "")
                    mail.To.Add(new System.Net.Mail.MailAddress(eMailQuemEnvia2, "Contato2 Home BROOKS"));

                mail.Subject = "Contato cliente: " + ddlAreaContato.Text;
                mail.Body = "e-mail: " + txtEmail.Text + "<br /> Cliente: " + txtNome.Text + "<br />" + txtMensagem.Text + "<br />Fone: " + txtTelefone.Text;
                mail.IsBodyHtml = true;
                try
                {
                    client.Send(mail);
                }
                catch (System.Exception erro)
                {
                    txtMensagem.Text = erro.Message;
                }
                finally
                {
                    mail = null;
                }
            }
            else
                txtMensagem.Text = "e-mail inválido!";
        }
    }
}
