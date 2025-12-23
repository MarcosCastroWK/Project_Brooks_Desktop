<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CDFe_Importacao.aspx.cs" Inherits="forms_CDFe_Importacao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td colspan="3">
                    <table style="border:1px solid black">
                        <tr>
                            <td>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logotipobrooks.jpg" />
                            </td>
                            <td>
                                <asp:Label ID="lblTitulo" runat="server" Text="CDF-e - Importação de númeração e ajuste de quantidades" Font-Bold="True" CssClass="tituloFundoBranco" Font-Size="12pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td>
                    <asp:Button ID="btnImportar" runat="server" OnClick="btnImportar_Click" Text="Importar" />
                </td>
                <td>
                    <asp:Label ID="lblCaminho" runat="server" Text="Caminho: "></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="358px" />
                </td>
            </tr>
            <tr><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblMensagem" runat="server" CssClass="tituloFundoBranco" Text="Mensagem"></asp:Label>
                </td>
            </tr>
            <tr><td colspan="3">&nbsp;</td></tr>
            <tr><td colspan="3">Formato correto do arquivo.txt - Salvar texto em Unicode(*.txt)</td></tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="Label2" runat="server" CssClass="tituloFundoBranco" Text="Número MTRe&nbsp;|&nbsp;CNPJ/CPF Cliente&nbsp;|&nbsp;Data&nbsp;|&nbsp;CódigoIBAMA&nbsp;|&nbsp;QtdeTon&nbsp;|&nbsp;QtdeUnidade&nbsp;|&nbsp;CDFe Nº"></asp:Label>
                </td>
            </tr>

        </table>
        <asp:GridView ID="GradeCDFe" runat="server">
        </asp:GridView>
    </form>
</body>
</html>
