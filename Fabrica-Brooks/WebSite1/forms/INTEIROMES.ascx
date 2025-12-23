<%@ Control Language="C#" AutoEventWireup="true" CodeFile="INTEIROMES.ascx.cs" Inherits="forms_INTEIROMES" %>

<script>
    function isNumberKeyMes(evt, pId) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57) || charCode == 13) {
            return false;
        }             
        return true;
    }
    function ConfereMes(pId) {

        if (document.getElementById(pId).value > 12) {
            document.getElementById(pId).value = 12;
        }
        else if (document.getElementById(pId).value < 1) {
               document.getElementById(pId).value = '01';
        }
    }
</script>
<style>
    .alinha
    {
        text-align:right;
    }
</style>
<asp:TextBox ID="txtInteiro" runat="server" MaxLength="2" CssClass="alinha" onfocusout="ConfereMes(this.id);" onkeypress="return isNumberKeyMes(event);" Width="18px"></asp:TextBox>

