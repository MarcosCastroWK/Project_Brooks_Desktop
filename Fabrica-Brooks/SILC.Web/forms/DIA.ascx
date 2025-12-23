<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DIA.ascx.cs" Inherits="SILC.Web.forms.DIA" %>

<style>
    .alinha
    {
        text-align:right;
    }
</style>
<script>
    function isNumberKeyDia(evt) {

        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;

    }
</script>
<asp:TextBox ID="txtDia" name="txtDia" runat="server" MaxLength="2" CssClass="alinha" onkeypress="return isNumberKeyDia(event);" Width="16px"></asp:TextBox>