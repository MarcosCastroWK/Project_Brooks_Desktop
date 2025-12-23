<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioMovimentacaoResiduosPorDestinoFinalDivergentes.aspx.cs" Inherits="SILC.Web.Relatorios.RelatorioMovimentacaoResiduosPorDestinoFinalDivergentes" %>
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
        function _linkMTRe(pX)
        {
            window.open('http://mtr.ima.sc.gov.br/ControllerServlet?acao=relatorio&nomeRelatorio=manifesto&manifesto=' + pX + '&condicao=N');
        }
        function _linkCDFe(pX)
        {
            window.open('http://mtr.ima.sc.gov.br/ControllerServlet?acao=relatorio&nomeRelatorio=certificado_destinacao_final&condicao=cdf&manifesto=' + pX);
        }
        function AbrePesquisaClientes()
        {
            document.getElementById("<%=btnProcurar.ClientID%>").click();
            window.open('../forms/PesquisaClientes.aspx');
        }
        function AbrePesquisaDestinoFinal() {
            var navegador = navigator.appName.toLowerCase();
            if (navegador.indexOf("internet") == -1)
            {
                document.getElementById("<%=btnProcurar.ClientID%>").click();
                window.open('../forms/PesquisaDestinoFinal.aspx');
            }
            else if (navegador.indexOf("internet") > -1)
            {
                document.getElementById("<%=btnProcurar.ClientID%>").click();
                location = '../forms/PesquisaDestinoFinal.aspx?explorer=../Relatorios/RelatorioMovimentacaoResiduosPorDestinoFinalDivergentes.aspx';
            }
        }
        function Imprime()
        {
            try {
                document.getElementById('Operacoes').style.visibility = "hidden"; 
                document.getElementById('Panel1').style.position = "absolute";
                document.getElementById('Panel1').style.top = 0;
                window.print();
            }
            finally {
                document.getElementById('Operacoes').style.visibility = "visible";
                document.getElementById('Panel1').style.position = "";
            }
        }
        function DischecarDivergentes(pId)
        {
            if (pId != 'chkCNPJ')
                document.getElementById('chkCNPJ').checked = false;
            if (pId != 'chkIBAMA')
                document.getElementById('chkIBAMA').checked = false;
            if (pId != 'chkCDFe')
                document.getElementById('chkCDFe').checked = false;
            if (pId != 'chkPlacas')
                document.getElementById('chkPlacas').checked = false;
            if (pId != 'chkPeso')
                document.getElementById('chkPeso').checked = false;
        }
        function ChecaTodos() {
            document.getElementById('chkCNPJ').checked = true;
            document.getElementById('chkIBAMA').checked = true;
            document.getElementById('chkCDFe').checked = true;
            document.getElementById('chkPlacas').checked = true;
            document.getElementById('chkPeso').checked = true;
            document.getElementById('chkMostrarCDFe').checked = true;
        }
        function NaoChecados()
        {
            if (document.getElementById('chkMostrarCDFe').checked == false)
            {
                document.getElementById('chkCNPJ').checked = false;
                document.getElementById('chkIBAMA').checked = false;
                document.getElementById('chkCDFe').checked = false;
                document.getElementById('chkPlacas').checked = false;
                document.getElementById('chkPeso').checked = false;
            }
        }
        function Checados()
        {
            if (document.getElementById('chkCDFe').checked || document.getElementById('chkPeso').checked)
            {
                document.getElementById('chkMostrarCDFe').checked = true;
                document.getElementById('chkCNPJ').checked = false;
                document.getElementById('chkIBAMA').checked = false;
                document.getElementById('chkPlacas').checked = false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div id="Operacoes">

            <uc1:cabecalho ID="cabecalho1" runat="server" />    
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatório de Movimentação de Resíduos por Destino Final - Divergentes"></asp:Label>
    
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblData" runat="server" CssClass="LetrasLabel" Text="Período:" Width="40px" Font-Overline="False"></asp:Label>
                    </td>
                    <td>
                        <uc3:DATA ID="Data1" runat="server" />
                    </td>
                    <td style="text-align:center">
                        <asp:Label ID="Label1" runat="server" CssClass="LetrasLabel" Text=" a " Width="20px"></asp:Label>
                    </td>
                    <td>
                        <uc3:DATA ID="Data2" runat="server" />
                    </td>
                    <td colspan="2">
                        <table border="0">
                            <tr>
                                <td>
                                    <uc4:DESTINOFINAL ID="DESTINOFINAL1" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnMostraDestinoFinal" runat="server" Text="Ok" OnClick="btnMostraDestinoFinal_Click" />
                                </td>
                                <td>
                                    
                                    <img id="imagem1" alt="x" src="../Images/procura.png" onclick="AbrePesquisaDestinoFinal()"/>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:CheckBox ID="chkSoEmDTR" runat="server" CssClass="LetrasLabel" Text="Só em DTR" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <uc2:CLIENTESCONTROL ID="CLIENTE1" runat="server" />
                    </td>
                    <td>
                        <img id="imagem2" alt="x" src="../Images/procura.png" onclick="AbrePesquisaClientes()"/>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkMostrarCDFe" runat="server" CssClass="LetrasLabel" Text="Mostrar CDFe" onclick="NaoChecados();" />
                        <br />
                        <asp:Label ID="lblLinhas" runat="server" CssClass="LetrasLabel" Text="linhas"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" OnClientClick="DesabilitaOperacoes(this.id, 'Data1_txtData', 'Data2_txtData', 'Relatório de Movimentação de Resíduos por Destino Final - Divergentes');" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelar" runat="server" Text="Voltar" OnClick="btnCancelar_Click" />
                                </td>
                                <td>
                                    <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprime();" />
                                </td>
                                <td>
                                    <asp:Button ID="btnEnviarEmail" runat="server" Text="Enviar e-mail" OnClick="btnEnviarEmail_Click" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imbExcel" runat="server" ImageUrl="~/Images/excel2010.png" OnClick="imbExcel_Click" />
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" CssClass="LetrasLabel" Text="O botão Enviar p/e-mail - quando acionado - gera o relatório do Destino Final e envia o e-mail para o Destino Final informado. Sem a informação do destino final a ação será de gerar o relatório e enviar o e-mail para todos selecionados no cadastro de Destino Final." Width="627px"></asp:Label>
                                    <asp:Button ID="btnProcurar" runat="server" BackColor="White" BorderWidth="0" ForeColor="White" Text="P" Visible="true" Width="1px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">            
                                    <asp:Button ID="btnConfirmar" runat="server" OnClick="btnConfirmar_Click" Text="Confirmar envio e-mail" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table style="padding: 0;word-spacing:0; border: 1px solid;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblDivergentes" runat="server" CssClass="tituloFundoBranco" Text="Divergentes:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input id="butTodos" class="LetrasLabel" type="button" style="width: 100px;" value="Seleciona todos" onclick="ChecaTodos();"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkCNPJ" runat="server" CssClass="LetrasLabel" Text="CNPJ" onclick="DischecarDivergentes(this.id);" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkIBAMA" runat="server" CssClass="LetrasLabel" Text="Código IBAMA" onclick="DischecarDivergentes(this.id);" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkCDFe" runat="server" CssClass="LetrasLabel" Text="CDFe / nula" onclick="Checados();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkPlacas" runat="server" CssClass="LetrasLabel" Text="Placas" onclick="DischecarDivergentes(this.id);" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkPeso" runat="server" CssClass="LetrasLabel" Text="Peso" onclick="Checados();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden; height:1px;" />
        </div>
        <div id="divImprimir">
            <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel"></asp:Label>
            <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Width="1750px" Font-Bold="False">
            </asp:Panel>
        </div>
    </form>
</body>
</html>
