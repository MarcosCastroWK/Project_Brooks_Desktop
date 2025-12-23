<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CLIENTE.ascx.cs" Inherits="SILC.Web.forms.CLIENTE" %>

<style>
    .alinha
    {
        text-align:right;
    }
    .modalDialog
    {
        font-family: Arial;
        background: rgba(0, 0, 0, 0.8);
        z-index: 99999;
        opacity: 0;
        width: 800px;
    }
</style>
<script>
    function TextoOk(pId)
    {
        if (document.getElementById('btnOk') != null)
        {
            document.getElementById('btnOk').value = "Ok";
            document.getElementById('btnOk').innerHTML = "Ok";
            document.getElementById('btnOk').innerText = "Ok";
            document.getElementById('btnOk').text = "Ok";
            if (document.getElementById(pId).value == '')
                document.getElementById(pId).value = '0';
        }
    }
</script>
<form id="form2" runat="server">
<table>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="10px" Text="Código" Font-Bold="False"></asp:Label>
        </td>
        <td>/</td>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Cliente" Font-Bold="False" Font-Names="Arial" Font-Size="10px"></asp:Label>
         </td>
        <td>
            <asp:TextBox ID="txtCodigoNomes" runat="server" Width="170px"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="Ok" />
        </td>
    </tr>
</table>
</form>