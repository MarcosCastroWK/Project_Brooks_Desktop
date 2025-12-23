<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="INTEIRO7.ascx.cs" Inherits="SILC.Web.forms.INTEIRO7" %>

<script>
    function isNumberKeyInteiro7(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57) || charCode == 13)
            return false;
        return true;
    }
    function HabilitaSalvarReajuste(pId) {
        if (document.getElementById(pId.substring(0, 21) + 'ibnSalvarReajuste') != null) {
            document.getElementById(pId.substring(0, 21) + 'ibnSalvarReajuste').src = '../Images/salvar.png';
            document.getElementById(pId.substring(0, 21) + 'ibnSalvarReajuste').disabled  = false;
        }            
    }
</script>
<style>
    .alinha
    {
        text-align:right;
    }
</style>
<asp:TextBox ID="txtInteiro" runat="server" MaxLength="9" CssClass="alinha" onfocus="HabilitaSalvarReajuste(this.id);" onkeypress="return isNumberKeyInteiro7(event);" Width="64px"></asp:TextBox>

