<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentacaoAplicavel.aspx.cs" Inherits="forms_DocumentacaoAplicavel" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>

<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>

<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>

<%@ Register src="INTEIRO2.ascx" tagname="INTEIRO2" tagprefix="uc5" %>

<%@ Register src="DIA.ascx" tagname="DIA" tagprefix="uc6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style2 {
            height: 26px;
        }
        .auto-style3 {
            height: 28px;
        }
    </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="form2" style="vertical-align:top;">
                <tr>
                    <td style="height:1%;">
                        <uc1:cabecalho ID="cabecalho1" runat="server" ClientIDMode="Inherit" EnableViewState="True" />
                    </td>
                </tr>
                <tr>
                    <td class="Menu">
                        <uc2:menu ID="menu" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" style="font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text="Documentação Aplicável" Font-Bold="True" CssClass="titulo2"></asp:Label>
                    </td>
                </tr>   
                <tr>
                    <td style="vertical-align:top;height:100%";>
                        <table style="vertical-align:top;height:1%";>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Código Cliente:" Font-Bold="True" CssClass="LetrasLabel"></asp:Label>
                                </td>
                                <td>
                                    <uc4:INTEIRO7 ID="intCodigoCliente" runat="server" Enabled="False" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCliente" runat="server" Text="Cliente:" Font-Bold="True" CssClass="LetrasLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCliente" runat="server" Enabled="False" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="Label2" runat="server" Text="Período:" Font-Bold="True" CssClass="LetrasLabel"></asp:Label>
                                </td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtPeriodo" runat="server" Width="50px"></asp:TextBox>
                                    &nbsp; do dia / até dia - ex. 01/30</td>
                            </tr>
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="lblEnviarPlanFatAteDia" runat="server" Text="Enviar PlanFat Até Dia:" Font-Bold="True" CssClass="LetrasLabel"></asp:Label>
                                </td>
                                <td class="auto-style3">
                                    <uc6:DIA ID="intEnviarPlanFatAteDia" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAguardarAprovacaoPlanFat" runat="server" Text="Aguardar Aprovação PlanFat:" Font-Bold="True" CssClass="LetrasLabel"></asp:Label>
                                </td>
                                <td>
                                    <uc5:INTEIRO2 ID="intAguardarAprovacaoPlanFat" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Aguardar Ordem de Compra:" Font-Bold="True" CssClass="LetrasLabel"></asp:Label>
                                </td>
                                <td>
                                    <uc5:INTEIRO2 ID="intAguardarOrdemCompra" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Conferir DDR Até Dia:" Font-Bold="True" CssClass="LetrasLabel"></asp:Label>
                                </td>
                                <td>
                                    <uc5:INTEIRO2 ID="intConferirDDRAteDia" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Enviar RGR Até Dia:" Font-Bold="True" CssClass="LetrasLabel"></asp:Label>
                                </td>
                                <td>
                                    <uc5:INTEIRO2 ID="intEnviarRGRAteDia" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>       
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <div style="width:160px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" TabIndex="2" OnClick="Salvar_Click" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"  TabIndex="2" OnClick="btnCancelar_Click" />
                                    </div>
                                </td>                                   
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblMensagem" CssClass="LetrasLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                                                                      
                        </table>
                    </td>
                </tr>            
            </table>
        </div>
    </form>
</body>
</html>
