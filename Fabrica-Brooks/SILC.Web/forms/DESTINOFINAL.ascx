<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DESTINOFINAL.ascx.cs" Inherits="SILC.Web.forms.DESTINOFINAL" %>

<script>
    function isNumberKeyDestinoFinal(evt)
    {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode >= 48 && charCode <= 57 || charCode == 8 || charCode == 46)
            return true;
        if (charCode >= 96 && charCode <= 105)
            return true;
        return false;
    }
</script>
<style>
    .alinha
    {
        text-align:right;
    }
</style>
<table>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="10px" Text="Código" Font-Bold="False"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="alinha" MaxLength="4" onkeydown="return isNumberKeyDestinoFinal(event);" Width="30px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Destino Final" Font-Bold="False" Font-Names="Arial" Font-Size="10px"></asp:Label>
         </td>
        <td>
            <asp:Label ID="lblDestinoFinal" runat="server" BorderStyle="Groove" BorderWidth="1px" Width="210px" Height="16px" Font-Bold="False" Font-Names="Arial" Font-Size="12px"></asp:Label>
        </td>
    </tr>
</table>
