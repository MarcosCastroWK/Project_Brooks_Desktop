using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.Relatorios
{
    
    
    public partial class Relatorios_PreviewRelatorio : System.Web.UI.Page
    {
        clsUsuarios oUsuario = new clsUsuarios();
        Parametros.Relatorio oRel = new Parametros.Relatorio();
        clsLancamentosDados oLancamentos = new clsLancamentosDados();
        clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
        DataTable _dt = new DataTable();
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Relatorio"] != null)
                oRel.Descricao = Request.QueryString["Relatorio"];
            if (Request.QueryString["Tipo"] != null)
                oRel.Tipo = Request.QueryString["Tipo"];
            if (Request.QueryString["Ordenacao"] != null)
                oRel.Ordenacao = Request.QueryString["Ordenacao"];
            if (Request.QueryString["Filtro"] != null)
                oRel.Filtro = Request.QueryString["Filtro"];
            if (Request.QueryString["CampoDoFiltro"] != null)
                oRel.CampoDoFiltro = Request.QueryString["CampoDoFiltro"];
    
        }
        protected void Grade_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
        }
        protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
        }
    }
}