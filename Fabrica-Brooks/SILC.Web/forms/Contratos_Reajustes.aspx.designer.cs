namespace SILC.Web.forms
{
    public partial class Contratos_Reajustes
    {
        protected global::System.Web.UI.HtmlControls.HtmlForm form1;
        protected global::SILC.Web.forms.cabecalho cabecalho1;
        protected global::SILC.Web.forms.menu menu1;
        protected global::System.Web.UI.WebControls.Label lblTitulo;
        protected global::System.Web.UI.WebControls.GridView Grade;
        protected global::System.Web.UI.WebControls.Label lblFiltro;
        protected global::System.Web.UI.WebControls.DropDownList ddlFiltro;
        protected global::System.Web.UI.WebControls.TextBox txtFiltro;
        protected global::System.Web.UI.WebControls.Button btnOk;
        protected global::System.Web.UI.WebControls.Button btnContratos;
        protected global::System.Web.UI.WebControls.Label lblTotal;
        protected global::System.Web.UI.WebControls.HiddenField hifCodigo;
        protected global::System.Web.UI.WebControls.HiddenField hifValorContrato;
        protected global::System.Web.UI.WebControls.HiddenField hifCodigoCliente;
        protected global::System.Web.UI.WebControls.HiddenField hifCodigoResiduo;
        protected global::System.Web.UI.WebControls.HiddenField hifResiduos;
        protected global::System.Web.UI.WebControls.GridView GradeReajustes;
        protected global::System.Web.UI.WebControls.HiddenField hifSequencialReajuste;
        protected global::System.Web.UI.WebControls.HiddenField hifDataReajuste;
        protected global::System.Web.UI.WebControls.DropDownList p_ddlSituacao;
        protected global::System.Web.UI.WebControls.DropDownList p_ddlTiposNegociacao;
        protected global::System.Web.UI.WebControls.Label lblTituloResiduos;
        protected global::System.Web.UI.WebControls.GridView GradeResiduos;
        protected global::System.Web.UI.WebControls.Label lblMensagemResiduos;
        protected global::System.Web.UI.WebControls.Panel PanelResiduos;
        protected global::System.Web.UI.WebControls.GridView GradePesquisa;
        protected global::System.Web.UI.WebControls.Label lblFiltroPesquisa;
        protected global::System.Web.UI.WebControls.DropDownList ddlFiltroPesquisa;
        protected global::System.Web.UI.WebControls.TextBox txtFiltroPesquisa;
        protected global::System.Web.UI.WebControls.Button btnOkPesquisa;
        protected global::System.Web.UI.WebControls.DropDownList p_ddlTiposDeCaixas;
        protected global::System.Web.UI.WebControls.DropDownList p_ddlFrequenciaColeta;
        protected global::System.Web.UI.WebControls.HiddenField hifDataReajusteSelecionado;

        /// <summary>
        /// ibnSelecionar control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.ImageButton ibnSelecionar;


        /// <summary>
        /// imgResiduosContratado control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.ImageButton imgResiduosContratado;


        /// <summary>
        /// ibnSalvarReajuste control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.ImageButton ibnSalvarReajuste;


        /// <summary>
        /// ibnExcluirReajustes control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.ImageButton ibnExcluirReajustes;


        /// <summary>
        /// datDataReajuste control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::SILC.Web.forms.DATA datDataReajuste;


        /// <summary>
        /// hifTipoNegociacao control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.HiddenField hifTipoNegociacao;


        /// <summary>
        /// ddlTipoNegociacao control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList ddlTipoNegociacao;


        /// <summary>
        /// moePercentualContrato control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::SILC.Web.forms.MOEDA moePercentualContrato;


        /// <summary>
        /// moePercentualUnitarios control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::SILC.Web.forms.MOEDA moePercentualUnitarios;


        /// <summary>
        /// moeValorContrato control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::SILC.Web.forms.MOEDA moeValorContrato;


        /// <summary>
        /// intNumeroContrato control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::SILC.Web.forms.INTEIRO7 intNumeroContrato;


        /// <summary>
        /// hifSituacao control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.HiddenField hifSituacao;


        /// <summary>
        /// ddlSituacao control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList ddlSituacao;


        /// <summary>
        /// ProximoReajuste control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::SILC.Web.forms.DATA ProximoReajuste;


        /// <summary>
        /// txtObservacao control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox txtObservacao;


        /// <summary>
        /// ibnSalvar control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.ImageButton ibnSalvar;


        /// <summary>
        /// ibnExcluirResiduo control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.ImageButton ibnExcluirResiduo;


        /// <summary>
        /// txtCodigoResiduo control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox txtCodigoResiduo;


        /// <summary>
        /// txtDescricaoResiduo control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox txtDescricaoResiduo;


        /// <summary>
        /// txtCxDisp control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox txtCxDisp;


        /// <summary>
        /// hifTipoCx control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.HiddenField hifTipoCx;


        /// <summary>
        /// ddlTipoCx control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList ddlTipoCx;


        /// <summary>
        /// txtQtFrequenciaColeta control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox txtQtFrequenciaColeta;


        /// <summary>
        /// ddlFrequenciaColeta control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList ddlFrequenciaColeta;


        /// <summary>
        /// ddlRoteiro control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList ddlRoteiro;


        /// <summary>
        /// hifRoteiro control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.HiddenField hifRoteiro;


        /// <summary>
        /// ddlexpressao1CobrancaMensal control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList ddlexpressao1CobrancaMensal;


        /// <summary>
        /// moeFranquiaCobrancaMensal control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::SILC.Web.forms.MOEDA moeFranquiaCobrancaMensal;


        /// <summary>
        /// ddlexpressao2CobrancaMensal control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList ddlexpressao2CobrancaMensal;


        /// <summary>
        /// ddlPeriodicidadeCobrancaMensal control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList ddlPeriodicidadeCobrancaMensal;


        /// <summary>
        /// moeValorExcedenteCobrancaMensal control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::SILC.Web.forms.MOEDA moeValorExcedenteCobrancaMensal;


        /// <summary>
        /// ddlexpressao1CobrancaPeso control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList ddlexpressao1CobrancaPeso;


        /// <summary>
        /// moeValorUnitario control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::SILC.Web.forms.MOEDA moeValorUnitario;


        /// <summary>
        /// ddlexpressao2CobrancaPeso control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList ddlexpressao2CobrancaPeso;


        /// <summary>
        /// txtUnidade control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox txtUnidade;


        /// <summary>
        /// ddlcondicaoCobrancaPeso control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList ddlcondicaoCobrancaPeso;


        /// <summary>
        /// moeFranquiaCobrancaPeso control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::SILC.Web.forms.MOEDA moeFranquiaCobrancaPeso;


        /// <summary>
        /// txtUnidadeCobrancaPeso control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox txtUnidadeCobrancaPeso;


        /// <summary>
        /// ddlexpressao3CobrancaPeso control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList ddlexpressao3CobrancaPeso;


        /// <summary>
        /// ddlexpressao4CobrancaPeso control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.DropDownList ddlexpressao4CobrancaPeso;


        /// <summary>
        /// txtOBS control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox txtOBS;


        /// <summary>
        /// txtDiasColeta control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox txtDiasColeta;


        /// <summary>
        /// txtParticularidade control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox txtParticularidade;


        /// <summary>
        /// txtMesAnoBase control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox txtMesAnoBase;


        /// <summary>
        /// ibnConsultar control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.ImageButton ibnConsultar;

    }
}