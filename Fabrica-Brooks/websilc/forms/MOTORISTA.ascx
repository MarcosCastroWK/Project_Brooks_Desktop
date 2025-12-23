<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MOTORISTA.ascx.cs" Inherits="forms_MOTORISTA" %>

<script>
    function isNumberKeyMotoristas(evt)
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
<table>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="10px" Text="Código" Font-Bold="False"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="alinha" MaxLength="4" onkeydown="return isNumberKeyMotoristas(event);" Width="30px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Motorista" Font-Bold="False" Font-Names="Arial" Font-Size="10px"></asp:Label>
         </td>
        <td>
            <asp:Label ID="lblMotorista" runat="server" BorderStyle="Groove" BorderWidth="1px" Width="198px" Height="16px" Font-Bold="False" Font-Names="Arial" Font-Size="12px"></asp:Label>
        </td>
        <td>
    </tr>
</table>