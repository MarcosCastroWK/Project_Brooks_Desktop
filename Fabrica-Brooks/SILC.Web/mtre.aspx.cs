using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;

namespace SILC.Web
{
    public partial class mtre : System.Web.UI.Page
    {
        HttpClient client;
        Uri usuarioUri;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }
        public mtre()
        {
            if (client == null)
            {
                client = new HttpClient();
                //client.GetStreamAsync  
                client.BaseAddress = new Uri("http://mtr.ima.sc.gov.br");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            }
        }
        private async System.Threading.Tasks.Task getAllAsync()
        {
            //chamando a api pela url
            System.Net.Http.HttpResponseMessage response = client.GetAsync("api/usuario").Result;

            //se retornar com sucesso busca os dados
            //if (response.IsSuccessStatusCode)
            //{
            //pegando o cabeçalho
            usuarioUri = response.Headers.Location;

            //Pegando os dados do Rest e armazenando na variável usuários

            string data = await response.Content.ReadAsStringAsync();

            //preenchendo a lista com os dados retornados da variável
            //GridView1.DataSource = data;
            //GridView1.DataBind();
            // }

            //Se der erro na chamada, mostra o status do código de erro.
            //else
            //    Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase);
        }


    }
}