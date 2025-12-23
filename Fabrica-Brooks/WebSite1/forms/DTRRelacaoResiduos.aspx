<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DTRRelacaoResiduos.aspx.cs" Inherits="DTRRelacaoResiduos" %>

<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %><%@ Register src="CLIENTESCONTROL.ascx" tagname="CLIENTESCONTROL" tagprefix="uc2" %><%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc3" %><%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc4" %>

<%@ Register src="INTEIRO2.ascx" tagname="INTEIRO2" tagprefix="uc5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../forms/table.css" type="text/css" rel="stylesheet" />
    <link href="../forms/aspx.css" type="text/css" rel="stylesheet" />
    <style>
        .tituloRelNegrito
        {
            font-family: Tahoma;
            font-size: 11pt;
            font-weight: bold;
        }
        .destinoRel
        {
            font-family: Tahoma;
            font-size: 11pt;
        }
        .invisivel
        {
            background-color: white;
            color: white;
            width: 1px;
            font-size: 1px;
            visibility: hidden;
        }
    </style>
    <script>
        function Imprime()
        {
            try {
                document.getElementById('Operacoes').style.visibility = "hidden"; 
                document.getElementById('Panel1').style.position = "absolute";
                document.getElementById('Panel1').style.top = 0;
                var _vias = 0;
                _vias = document.getElementById('intVias_txtInteiro').value;
                var i = 0;
                for (i = 1; i <= _vias; i++) {
                    window.print();
                }
                window.close();
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
            <table>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relação de Resíduos"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="btnImprimir" type="button" value="Imprimir" class="LetrasLabel" onclick="Imprime();" />
                    </td>
                    <td>
                        <asp:Label ID="lblVias" runat="server" class="LetrasLabel" Text="Vias: "></asp:Label>
                    </td>
                    <td>
                        <uc5:INTEIRO2 ID="intVias" runat="server" class="LetrasLabel" Valor="2" />
                    </td>
                    <td>
                        <asp:Label ID="lblPagina" runat="server" class="LetrasLabel" Text="Pag: "></asp:Label>
                    </td>
                    <td>
                        <uc5:INTEIRO2 ID="intPagina" runat="server" class="LetrasLabel" Valor="1"/>
                    </td>
                    <td>
                        <asp:Button ID="btnSalvarDestino" runat="server" Text="Salvar destino" CssClass="LetrasLabel" OnClick="btnSalvarDestino_Click" Width="80px" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divImprimir">
            <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Width="830px">
                <table>
                    <tr>
                        <td>
                            <img id="imglogo" runat="server" src="~/Images/logotipobrooks.jpg" />
                        </td>
                        <td>
                            <asp:Label ID="lblTituloRel" runat="server" CssClass="tituloRelNegrito" Text="RELAÇÃO DE RESÍDUOS ENCAMINHADOS A"></asp:Label>
                            <br />
                            <asp:Label ID="lblDestinoFinal" runat="server" CssClass="destinoRel" Text="destino final"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>        
    </form>
</body>
</html>
