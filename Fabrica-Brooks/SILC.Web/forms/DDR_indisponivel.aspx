<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DDR_indisponivel.aspx.cs" Inherits="SILC.Web.forms.DDR_indisponivel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td colspan="2">
                    <table style="border:1px solid black">
                        <tr>
                            <td>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logotipobrooks.jpg" />
                            </td>
                            <td>
                                <asp:Label ID="lblTitulo" runat="server" Text="DDR - DECLARAÇÃO DE DESTINAÇÃO DE RESÍDUOS" Font-Bold="True" CssClass="tituloFundoBranco" Font-Size="12pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Label ID="lblMensagem" runat="server" CssClass="tituloFundoBranco" Text="Mensagem"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblHome" runat="server" CssClass="tituloFundoBranco" Text="Home contato"></asp:Label>
                </td>
            </tr>

        </table>
    </form>
</body>
</html>
