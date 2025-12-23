<%@ Control Language="C#" AutoEventWireup="true" CodeFile="INTEIRO.ascx.cs" Inherits="forms_INTEIRO" %>

<script>
    function isNumberKeyInteiro(evt) {
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
<asp:TextBox ID="txtInteiro" runat="server" MaxLength="4" CssClass="alinha" onkeypress="return isNumberKeyInteiro(event);" Width="30px"></asp:TextBox>

