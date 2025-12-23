<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cabecalho.ascx.cs" Inherits="cabecalho" %>
<asp:Panel ID="Panel1" runat="server" BackColor="WhiteSmoke" Width="100%">
    <script>
        function AbreMenu()
        {
            window.location = '../Default.aspx';
        }
    </script>
    <table style="width: 100%;">
        <tr>
            <td>
                <Img id="imglogo" runat="server" src="~/Images/mediologobrooks.png" onclick="AbreMenu();" />
            </td>
            <td>
                <table style="text-align:right;width:100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblUsuario" runat="server" Width="200px" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDB" runat="server" Text="db: 1" Height="20px" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>