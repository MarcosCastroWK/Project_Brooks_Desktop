<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RelatorioControleAterro.aspx.cs" Inherits="RelatorioControleAterro" %>

<%@ Register src="../forms/cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>

<%@ Register src="../forms/CLIENTESCONTROL.ascx" tagname="CLIENTESCONTROL" tagprefix="uc2" %>

<%@ Register src="../forms/DATA.ascx" tagname="DATA" tagprefix="uc3" %>

<%@ Register src="../forms/DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../forms/table.css" type="text/css" rel="stylesheet" />
    <link href="../forms/aspx.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/funcaoGeral.js"></script>
    <script>
        function Imprime() {
            try {
                document.getElementById('Operacoes').style.visibility = "hidden";
                document.getElementById('Panel1').style.position = "absolute";
                document.getElementById('Panel1').style.top = 0;
                window.print();
            }
            finally {
                document.getElementById('Operacoes').style.visibility = "visible";
                document.getElementById('Panel1').style.position = "initial";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div id="Operacoes">

            <uc1:cabecalho ID="cabecalho1" runat="server" />
    
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatório Controle de Aterro"></asp:Label>
    
            <br />
            <table>
                <tr>
                    <td colspan="8">
                        <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprime();" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancelar" runat="server" Text="Voltar" OnClick="btnCancelar_Click" />
                    </td>
                </tr>
            </table>
            <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden; height:1px;" />
        </div>
        <div id="divImprimir">
            <asp:Panel ID="Panel1" runat="server" Width="1300px"></asp:Panel>
        </div>
    </form>
</body>
</html>
