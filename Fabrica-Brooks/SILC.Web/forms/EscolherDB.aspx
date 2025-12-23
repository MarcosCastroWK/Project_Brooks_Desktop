<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EscolherDB.aspx.cs" Inherits="SILC.Web.forms.EscolherDB" %>
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
                       <table style="text-align:center; width:300px; height:70px;border:ridge 8px ActiveBorder;">
                           <tr>
                                <td style="text-align:left;">
                                    <asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False">&nbsp;Escolher Banco de dados</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlDB" runat="server" AutoPostBack="true" CssClass="LetrasLabel" Width="300px" OnSelectedIndexChanged="ddlDB_SelectedIndexChanged">
                                        <asp:ListItem>1 - Produção</asp:ListItem>
                                        <asp:ListItem>2 - Teste - Servidor</asp:ListItem>
                                        <asp:ListItem>3 - Teste localhost</asp:ListItem>
                                    </asp:DropDownList>

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
