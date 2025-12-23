<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="INTEIRO2.ascx.cs" Inherits="SILC.Web.forms.INTEIRO2" %>

<script>
    function isNumberKeyInteiro2(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57) || charCode == 13)
            return false;
        return true;
    }
</script>
<style>
    .alinha
    {
        text-align:right;
    }
</style>
<asp:TextBox ID="txtInteiro" runat="server" CssClass="alinha" onkeypress="return isNumberKeyInteiro2(event);" Width="16px"></asp:TextBox>

