<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Balanca.aspx.cs" Inherits="SILC.Web.forms.Balanca" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>

<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>

<%@ Register src="INTEIRO2.ascx" tagname="INTEIRO2" tagprefix="uc7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 20px;
        }
    </style>
    </head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<body>
     <form id="form1" runat="server">
        <div>
            <table class="form2">
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
                   <td align="center">
                       <table style="text-align:center; width:30%; height:40%;border:ridge 8px ActiveBorder;">
                            <tr>
                                <td id="dadosCab" valign="center" style="text-align:center; font-size:9pt;height:80px;">
                                    <asp:Label ID="lblTitulo" runat="server" Text="Dados Balança" Font-Bold="True" Font-Names="Arial"></asp:Label>
                                 </td>
                            </tr>
                            <tr>
                                <td id="dadosCab" valign="center" style="text-align:center; font-size:9pt;">
                                    <table>
                                        <tr>
                                            <td style="text-align:left;">
                                                <asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False" Width="90px">&nbsp;Peso Total</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPesoTotal" runat="server" TabIndex="1" Width="310px" TextMode="MultiLine" Height="148px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:left;">
                                                <asp:Label CssClass="LetrasLabel" ID="Label2" runat="server" Font-Bold="False">&nbsp;ref</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRef1" runat="server" TabIndex="1" Width="250px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:left;">
                                                <asp:Label CssClass="LetrasLabel" ID="Label3" runat="server" Font-Bold="False" Width="110px">&nbsp;ref 2</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRef2" runat="server" TabIndex="1" Width="250px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center;" class="auto-style1">
                                    </td>
                            </tr>
                            <tr>
                                <td style="text-align:center;height:20px">
                                    <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ler Dados Balança" ID="LerDadosBalanca" OnClick="LerDadosBalanca_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center;height:80px">
                                    <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel"></asp:Label>
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
