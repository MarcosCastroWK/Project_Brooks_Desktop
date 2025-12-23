<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pesquisasatisfacao.aspx.cs" Inherits="pesquisasatisfacao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>Pesquisa de Satisfação - Nível de 1 a 10</div>
        <table>
            <tr>
                <td>
                    <asp:RadioButton ID="RadioButton1" runat="server" Text="1 - Instatisfeito" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="RadioButton2" runat="server" Text="2 - Muito a melhorar" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="RadioButton3" runat="server" Text="3 - " />
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:RadioButton ID="RadioButton4" runat="server" Text="4 - Melhorar" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="RadioButton5" runat="server" Text="5 - Regular" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="RadioButton6" runat="server" Text="6" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="RadioButton7" runat="server" Text="7 - Satisfário" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="RadioButton8" runat="server" Text="8" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="RadioButton9" runat="server" Text="9 - Muito Bom" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="RadioButton10" runat="server" Text="10 - Satisfeito" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
