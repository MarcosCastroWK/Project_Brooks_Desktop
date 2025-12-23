<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CLIENTESCONTROL.ascx.cs" Inherits="forms_CLIENTESCONTROL" %>

<script>
    function isNumberKeyClientes(evt)
    {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode >= 48 && charCode <= 57 || charCode == 8 || charCode == 9 || charCode == 46)
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
    .modalDialog
    {
        /*
        position: fixed;*/
        font-family: Arial;
        /*top: 0;
        right: 0;
        bottom: 0;
        left: 0;*/
        background: rgba(0, 0, 0, 0.8);
        z-index: 99999;
        opacity: 0;/*
        -webkit-transition: opacity 400ms ease-in;
        -moz-transition: opacity 400ms ease-in;
        transition: opacity 400ms ease-in;
        pointer-events: none;*/
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
<table>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="10px" Text="Código" Font-Bold="False"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="alinha" MaxLength="4" onkeydown="return isNumberKeyClientes(event);" onfocusout="TextoOk(this.id);" Width="30px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Cliente" Font-Bold="False" Font-Names="Arial" Font-Size="10px"></asp:Label>
         </td>
        <td>
            <asp:Label ID="lblCliente" runat="server" BorderStyle="Groove" BorderWidth="1px" Width="251px" Height="16px" Font-Bold="False" Font-Names="Arial" Font-Size="12px">Nome fantasia</asp:Label>
        </td>
        <td>
    </tr>
</table>