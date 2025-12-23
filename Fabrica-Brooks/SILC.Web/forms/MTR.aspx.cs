using System;
using System.Web;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.forms
{
    public partial class MTR : System.Web.UI.Page
    {
        clsUsuarios oUsuario = new clsUsuarios();
        clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
        clsDestinoFinal oDestinoFinal = new clsDestinoFinal();
        clsFuncionarioDados oMotoristaDados = new clsFuncionarioDados();
        clsFuncionarios oMotorista = new clsFuncionarios();
        clsCaminhoes oCaminhao = new clsCaminhoes();
        clsCaminhoesDados oCaminhaoDados = new clsCaminhoesDados();

        DataTable _dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            if (oUsuario == null)
            {
                Response.Redirect("brooks/login.aspx", true);
            }
            if (!IsPostBack)
            {
                Relatorio();
            }
        }
        private void Relatorio()
        {
            ddlMotorista.Items.Clear();
            ddlMotorista.Items.Add("");
            DataTable _dtMotoristas = new DataTable();
            _dtMotoristas = oMotoristaDados.PreencheDataTableFuncionarios("Nome", "", "NaoDemitidos");
            foreach (DataRow _dr in _dtMotoristas.Rows)
            {
                ListItem _li = new ListItem();
                _li.Value = _dr["Codigo"].ToString();
                _li.Text = _dr["Nome"].ToString();
                ddlMotorista.Items.Add(_li);
            }

            ddlPlacas.Items.Clear();
            ddlPlacas.Items.Add("");
            DataTable _dtCaminhoes = new DataTable();
            _dtCaminhoes = oCaminhaoDados.PreencheDataTableCaminhoes("Modelo");
            foreach (DataRow _dr in _dtCaminhoes.Rows)
            {
                ListItem _li = new ListItem();
                _li.Value = _dr["Codigo"].ToString();
                _li.Text = _dr["Placas"].ToString();
                if (_dr["Placas"].ToString() != "")
                {
                    ddlPlacas.Items.Add(_li);
                }
            }

            if (Session["dtRelacaoDTR"] != null)
                _dt = (DataTable)Session["dtRelacaoDTR"];

            if (_dt.Columns.Count >= 14)
            {
                ddlMotorista.Items.Clear();
                ddlMotorista.Items.Add(HttpUtility.HtmlDecode(_dt.Rows[0]["Motorista"].ToString()));
                ddlPlacas.Items.Clear();
                ddlPlacas.Items.Add(_dt.Rows[0]["Placas"].ToString());
            }

            DateTime _dataEnvio = DateTime.Now;
            string _localentrega = "";
            int _codigoresiduo = 0;
            if (_dt.Rows.Count > 0)
            {
                _dataEnvio = Convert.ToDateTime(_dt.Rows[0][8]);
                _localentrega = _dt.Rows[0][9].ToString();
                _codigoresiduo = Convert.ToInt32(_dt.Rows[0][10]);
            }

            lblDestinoFinal.Text = oDestinoFinalDados.PegaRazaoSocial(_localentrega);

            clsResiduos oResiduo = new clsResiduos();
            clsResiduoDados oResiduoDados = new clsResiduoDados();
            oResiduo = oResiduoDados.PegaDados(oResiduo, _codigoresiduo);
            lblTipoResiduo.Text = oResiduo.DescricaoGrupo + "/" + oResiduo.DescricaoReduzida;
            lblCodigo.Text = oResiduo.CodigoResiduoManifesto;
            lblClasse.Text = oResiduo.Classe;
            lblEstadoFisico.Text = oResiduo.EstadoFisico;
            if (_dt.Rows.Count > 0)
            {
                decimal _PesoKgTotal = 0;
                foreach (DataRow dr in _dt.Rows)
                {
                    _PesoKgTotal = _PesoKgTotal + Convert.ToDecimal(dr["PesoKg"]);
                }
                lblPeso.Text = _PesoKgTotal.ToString() + " KG";
            }
            string _codigoDestino = oDestinoFinalDados.PegaCodigo(_localentrega);
            if (_codigoDestino != "")
            {
                // pegar dados do destino e mudar no campos do [8 destino]
                oDestinoFinal = oDestinoFinalDados.PegaDados(oDestinoFinal, Convert.ToInt32(_codigoDestino));
                lblDestinoFinal.Text = oDestinoFinal.Nome;
                lblCNPJ_Destino.Text = oDestinoFinal.CNPJ;
                lblEnderecoDestino.Text = oDestinoFinal.Endereco;
                lblUFDestino.Text = oDestinoFinal.UF;
                lblMunicipioDestino.Text = oDestinoFinal.Cidade;
            }
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Session["dtRelacaoDTR"] != null)
                _dt = (DataTable)Session["dtRelacaoDTR"];
            try
            {
                bool bNaoSalvar = false;
                if (Session["NaoSalvarRelacaoDTR"] != null)
                    bNaoSalvar = true;
                // salvar imprimido
                if (_dt.Rows.Count > 0 && !bNaoSalvar)
                {
                    clsDTR oDTR = new clsDTR();
                    clsDTRDados oDTRDados = new clsDTRDados();
                    int _ultimoNumeroImpressao = oDTRDados.PegaUltimoNumeroImpressao() + 1;
                    foreach (DataRow dr in _dt.Rows)
                    {
                        oDTRDados.SalvarComoImprimido(Convert.ToInt32(dr["Sequencial"]), Convert.ToInt32(dr["NumeroLancamento"]),
                                                      Convert.ToInt32(dr["CodigoResiduo"]), Convert.ToInt32(ddlPlacas.Items[ddlPlacas.SelectedIndex].Value),
                                                      Convert.ToInt32(ddlMotorista.Items[ddlMotorista.SelectedIndex].Value), _ultimoNumeroImpressao);
                    }
                }
            }
            finally
            {
                Response.Write("<script>window.close();</script>");
            }
        }
    }
}