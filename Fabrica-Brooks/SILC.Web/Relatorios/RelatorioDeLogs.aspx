<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioDeLogs.aspx.cs" Inherits="SILC.Web.Relatorios.Relatorios_RelatorioDeLogs" %>

<%@ Register src="../forms/cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>

<%@ Register src="../forms/CLIENTESCONTROL.ascx" tagname="CLIENTESCONTROL" tagprefix="uc2" %>

<%@ Register src="../forms/DATA.ascx" tagname="DATA" tagprefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../forms/table.css" type="text/css" rel="stylesheet" />
    <link href="../forms/aspx.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/funcaoGeral.js"></script>
    <script>
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
                document.getElementById('Panel1').style.position = "initial";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div id="Operacoes">
            <uc1:cabecalho ID="cabecalho1" runat="server" />
    
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatório de Logs"></asp:Label>
    
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblDataInicio" runat="server" CssClass="LetrasLabel" Text="Data início:"></asp:Label>
                    </td>
                    <td>
                        <uc3:DATA ID="datDataInicio" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblDataFinal" runat="server" CssClass="LetrasLabel" Text="Data Final:"></asp:Label>
                    </td>
                    <td>
                        <uc3:DATA ID="datDataFinal" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOpcaoMenu" runat="server" CssClass="LetrasLabel" Text="Opção(Menu):"></asp:Label>
                    </td>
                    <td colspan="3">

                        <asp:DropDownList ID="ddlOpcaoDoMenu" runat="server" Width="236px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Alteração-Contratos</asp:ListItem>
                            <asp:ListItem>Bloqueio Financeiro</asp:ListItem>
                            <asp:ListItem>Cadastro de Caminhões</asp:ListItem>
                            <asp:ListItem>Cadastro de Clientes</asp:ListItem>
                            <asp:ListItem>Cadastro Clientes. Endereço</asp:ListItem>
                            <asp:ListItem>Cadastro Containeres</asp:ListItem>
                            <asp:ListItem>Cadastro Destino Final</asp:ListItem>
                            <asp:ListItem>Cadastro Empresa</asp:ListItem>
                            <asp:ListItem>Cadastro e-mail padrão</asp:ListItem>
                            <asp:ListItem>Cadastro do IBAMA</asp:ListItem>
                            <asp:ListItem>Cadastro de Motoristas</asp:ListItem>
                            <asp:ListItem>Cadastro de Municípios</asp:ListItem>
                            <asp:ListItem>Cadastro de Resíduos</asp:ListItem>
                            <asp:ListItem>Cadastro de Usuário</asp:ListItem>
                            <asp:ListItem>Códigos Serviços Prefeitura</asp:ListItem>
                            <asp:ListItem>Contratos-Resíduos</asp:ListItem>
                            <asp:ListItem>Controle de Aterro</asp:ListItem>
                            <asp:ListItem>Descrição Serviços NF</asp:ListItem>
                            <asp:ListItem>Distribuição Blocos MTR</asp:ListItem>
                            <asp:ListItem>Documentos página</asp:ListItem>
                            <asp:ListItem>DTR</asp:ListItem>
                            <asp:ListItem>Escolheu outro DB</asp:ListItem>
                            <asp:ListItem>Inclusão-Contratos</asp:ListItem>
                            <asp:ListItem>Exclusão-Lançamento</asp:ListItem>
                            <asp:ListItem>Lançamento de Locações</asp:ListItem>
                            <asp:ListItem>Locações-Resíduos</asp:ListItem>
                            <asp:ListItem>Lançamento Notas Fiscais</asp:ListItem>
                            <asp:ListItem>Liberação Acesso Programação</asp:ListItem>
                            <asp:ListItem>Movimentação DTR</asp:ListItem>
                            <asp:ListItem>MTR Cancelada/Transbordo</asp:ListItem>
                            <asp:ListItem>MTRs pendentes</asp:ListItem>
                            <asp:ListItem>Permissões</asp:ListItem>
                            <asp:ListItem>Programação de Serviços</asp:ListItem>
                            <asp:ListItem>Reajustar/Repactuar-Contratos</asp:ListItem>
                            <asp:ListItem>Rescisão-Contratos</asp:ListItem>
                            <asp:ListItem>Retenções Impostos NF</asp:ListItem>
                            <asp:ListItem>Serviços desenvolvedor</asp:ListItem>
                            <asp:ListItem>Tickets pendentes</asp:ListItem>
                            <asp:ListItem>UpLoad RGR/DDR/CDF</asp:ListItem>
							<asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnImprimir_Click" OnClientClick="DesabilitaOperacoes(this.id, '', '', 'Relatório de Logs');" />
            <asp:Button ID="btnCancelar" runat="server" Text="Voltar" PostBackUrl="~/forms/Menu.aspx" />    
            <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprime();" />
            <br />
            <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden; height:1px;" />
        </div>
        <asp:Panel ID="Panel1" runat="server">
            <asp:Label ID="lblTitulo0" runat="server" BackColor="White" CssClass="titulo2" Text="Relatório de "></asp:Label>
            <br />
        </asp:Panel>
    </form>
</body>
</html>
