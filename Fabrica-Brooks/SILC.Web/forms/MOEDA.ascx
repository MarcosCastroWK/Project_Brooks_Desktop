<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MOEDA.ascx.cs" Inherits="SILC.Web.forms.MOEDA" %>
<script>
    var sim = true;
    function isNumberKeyMoeda(evt, pId)
    {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (document.getElementById(pId).value.indexOf(',') > -1 && charCode == 44 || charCode == 13) {
            return false;
        }
        if (charCode == 44)
        {            
            if (!sim)
                return false;
            sim = true;
        }
        if (charCode > 31 && charCode != 44 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }
    /*
    function RetornaComDecimal(pId) {
        if (document.getElementById(pId).value == "NaN" || document.getElementById(pId).value == null || document.getElementById(pId).value == "")
            document.getElementById(pId).value = "0,00";
        if (document.getElementById(pId).value != "" && pId.indexOf("moePercentualReajuste") == -1) {
            document.getElementById(pId).value = parseFloat(document.getElementById(pId).value.replace(",", ".")).toFixed(2).replace(".", ",");
        }
        else {
            document.getElementById(pId).value = Number(document.getElementById(pId).value.replace(",", ".")).toFixed(6).replace(".", ",");            
        }
        
        if (document.getElementById(pId.substring(0, 21) + 'ibnSalvarReajuste') != null) {
            document.getElementById(pId.substring(0, 21) + 'ibnSalvarReajuste').src = '../Images/salvar.png';
            document.getElementById(pId.substring(0, 21) + 'ibnSalvarReajuste').disabled  = false;
        }
        
        if (pId.indexOf("02_moePercentualReajuste") > -1) {            
            var vca1 = Number(document.getElementById('hifValorContrato').value.replace(",", "."));
            var vperc1 = Number(document.getElementById(pId).value.replace(",", "."));
            var vc = vca1 * vperc1 / 100;
            document.getElementById(pId.substring(0, 21) + 'moeValorContrato_txtMoeda').value = (vc + vca1).toFixed(2).replace(".", ",");
            if (vperc1 > 0) {
                document.getElementById(pId.substring(0, 21) + 'ddlTipoNegociacao').value = 'REAJUSTE';
                var _ano = document.getElementById('hifDataReajuste').value.substring(6);
                var _dtreaj = document.getElementById('hifDataReajuste').value.substring(0, 6) + (parseFloat(_ano) + 1);
                document.getElementById(pId.substring(0, 21) + 'datDataReajuste_txtData').value = _dtreaj;
                _dtreaj = document.getElementById('hifDataReajuste').value.substring(0, 6) + (parseFloat(_ano) + 2);
                document.getElementById(pId.substring(0, 21) + 'ProximoReajuste_txtData').value = _dtreaj;
            }
            else {
                var data = new Date(),
                    dia = data.getDate().toString(),
                    diaF = (dia.length == 1) ? '0' + dia : dia,
                    mes = (data.getMonth() + 1).toString(), //+1 pois no getMonth Janeiro começa com zero.
                    mesF = (mes.length == 1) ? '0' + mes : mes,
                    anoF = data.getFullYear();
                document.getElementById(pId.substring(0, 21) + 'datDataReajuste_txtData').value = diaF + "/" + mesF + "/" + anoF;                
                document.getElementById(pId.substring(0, 21) + 'ddlTipoNegociacao').value = 'REPACTUAÇÃO';
                document.getElementById(pId.substring(0, 21) + 'ProximoReajuste_txtData').value = document.getElementById('hifDataReajuste').value;
            }
        }
        else if (pId.substring(0, 21).indexOf("02") > -1) {
            
            var vc  = Number(document.getElementById(pId).value.replace(",", "."));
            var vca = Number(document.getElementById('hifValorContrato').value.replace(",", "."))
            var vperc = 0.00;
            if (pId.indexOf("moeValorContrato") > -1) {
                vperc = (((vc - vca) * 100) / vca).toFixed(6).replace("-", "");                
            }
            document.getElementById(pId.substring(0, 21) + 'moePercentualReajuste_txtMoeda').value = vperc.replace(".", ",");
            if (vperc > 0) {
                document.getElementById(pId.substring(0, 21) + 'datDataReajuste_txtData').value = document.getElementById('hifDataReajuste').value;
                document.getElementById(pId.substring(0, 21) + 'ddlTipoNegociacao').value = 'REAJUSTE';
                var _ano = document.getElementById('hifDataReajuste').value.substring(6);
                var _dtreaj = document.getElementById('hifDataReajuste').value.substring(0, 6) + (parseFloat(_ano) + 1);
                document.getElementById(pId.substring(0, 21) + 'datDataReajuste_txtData').value = _dtreaj;
                _dtreaj = document.getElementById('hifDataReajuste').value.substring(0, 6) + (parseFloat(_ano) + 2);
                document.getElementById(pId.substring(0, 21) + 'ProximoReajuste_txtData').value = _dtreaj;
            }
            else {
                var data = new Date(),
                    dia = data.getDate().toString(),
                    diaF = (dia.length == 1) ? '0' + dia : dia,
                    mes = (data.getMonth() + 1).toString(), //+1 pois no getMonth Janeiro começa com zero.
                    mesF = (mes.length == 1) ? '0' + mes : mes,
                    anoF = data.getFullYear();
                document.getElementById(pId.substring(0, 21) + 'datDataReajuste_txtData').value = diaF + "/" + mesF + "/" + anoF;                
                document.getElementById(pId.substring(0, 21) + 'ddlTipoNegociacao').value = 'REPACTUAÇÃO';
                document.getElementById(pId.substring(0, 21) + 'ProximoReajuste_txtData').value = document.getElementById('hifDataReajuste').value;
            }
        }
    }
    */
    function HabilitaSalvarReajusteMoeda(pId) {
        if (document.getElementById(pId.substring(0, 21) + 'ibnSalvarReajuste') != null) {
            document.getElementById(pId.substring(0, 21) + 'ibnSalvarReajuste').src = '../Images/salvar.png';
            document.getElementById(pId.substring(0, 21) + 'ibnSalvarReajuste').disabled  = false;
        }            
    }
    function CalculaValorContratoPeloPercentual(pId) {
        if (pId.indexOf('moePercentualContrato') > -1) {
            var sCampoValorContrato = pId.replace('moePercentualContrato', 'moeValorContrato');
            var sCampoPercentualUnitarios = pId.replace('moePercentualContrato', 'moePercentualUnitarios');
            var vPercetual = Number(document.getElementById(pId).value.replace(",", "."));
            var vContrato = Number(document.getElementById('hifValorContrato').value.replace(",", "."));
            var vReajuste = vContrato * vPercetual / 100;
            document.getElementById(sCampoValorContrato).value = (vContrato + vReajuste).toFixed(2).replace(".", ",");
            document.getElementById(sCampoPercentualUnitarios).value = vPercetual.toFixed(2).replace(".", ",");
        }
    }

</script>
<link href="aspx.css" type="text/css" rel="stylesheet" />
<style>
    .alinha
    {
        text-align:right;
    }
</style>
<asp:TextBox ID="txtMoeda" runat="server" MaxLength="11" Width="76px" style="text-align:right;" onfocus="HabilitaSalvarReajusteMoeda(this.id);" onfocusout="CalculaValorContratoPeloPercentual(this.id);" onkeypress="return isNumberKeyMoeda(event, this.id);"></asp:TextBox>
