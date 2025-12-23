using System;
using System.Net;

namespace SILC.Web.forms.brooks
{
    public partial class ima : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _url = "http://mtr.ima.sc.gov.br";
            string _page = "";

            WebClient webClient = new WebClient();
            _page = webClient.DownloadString(_url);

            _page = _page.Replace("/css/redmond/jquery-ui-1.10.3.custom.css", _url + "/css/redmond/jquery-ui-1.10.3.custom.css");

            _page = _page.Replace("/js/validaForm.js", _url + "/js/validaForm.js");
            _page = _page.Replace("/js/jquery-1.9.1.js", _url + "/js/jquery-1.9.1.js");
            _page = _page.Replace("/js/jquery-ui-1.10.3.custom.js", _url + "/js/jquery-ui-1.10.3.custom.js");
            _page = _page.Replace("/js/mascara.js", _url + "/js/mascara.js");
            _page = _page.Replace("/js/jquery.validate.js", _url + "/js/jquery.validate.js");
            _page = _page.Replace("/js/util.js", _url + "/js/util.js");
            _page = _page.Replace("/js/jquery.meio.mask.js", _url + "/js/jquery.meio.mask.js");

            _page = _page.Replace("/css/estilo.css", _url + "/css/estilo.css");
            _page = _page.Replace("img/favicon.ico", "http://mtr.ima.sc.gov.br/img/favicon.ico");

            _page = _page.Replace("Se jÃ¡ Ã© usuÃ¡rio", "Se já é usuário");

            _page = _page.Replace("Se nÃ£o Ã© usuÃ¡rio", "Se não é usuário");
            _page = _page.Replace("UsuÃ¡rio. vocÃª receberÃ¡", "Usuário. Você receberá");
            _page = _page.Replace("VocÃª receberÃ¡", "Você receberá");
            _page = _page.Replace("OrientaÃ§Ã£o", "Orientação");
            _page = _page.Replace("cadastro de usuÃ¡rio", "cadastro de usuário");
            _page = _page.Replace("USUÃRIO", "USUÁRIO");
            _page = _page.Replace("FlorianÃ³polis/SC", "Florianópolis/SC");
            _page = _page.Replace("disponí­vel na página", "disponí­vel na página");
            _page = _page.Replace("Novo UsuÃ¡rio", "Novo Usuário");
            _page = _page.Replace("vocÃª receberÃ¡", "vocé receberá");
            _page = _page.Replace("pÃ¡gina do IMA", "página do IMA");


            _page = _page.Replace("<p><a href=", "<p><a href='http://mtr.ima.sc.gov.br/#'");
            //_page = _page.Replace("function buscaPessoaCnpj(cnpj, campo, form) {", "function buscaPessoaCnpj(cnpj, campo, form) {");

            _page = _page.Replace("js/jquery.dataTables.min.js", "http://mtr.ima.sc.gov.br/js/jquery.dataTables.min.js");


            _page = _page.Replace("/img/home/logo1.png", _url + "/img/home/logo1.png");
            _page = _page.Replace("/img/home/NOVA_MARCA_GOVERNO_ESTADO_SC_2019.png", _url + "/img/home/NOVA_MARCA_GOVERNO_ESTADO_SC_2019.png");
            _page = _page.Replace("/img/home/logo_brd.png", _url + "/img/home/logo_brd.png");

            //
            Response.Write(_page);
        }
    }
}