<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MovimentacaoDTR.aspx.cs" Inherits="SILC.Web.forms.MovimentacaoDTR" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="MOTORISTA.ascx" tagname="MOTORISTA" tagprefix="uc7" %>
<%@ Register src="CAMINHAO.ascx" tagname="CAMINHAO" tagprefix="uc8" %>
<%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc9" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        ._visibility {
            visibility: hidden;
            font-size: 0px;
            width: 0px;
        }
        .BotaoSelecao {
            background-color: white;
            text-align: center;
        }
        .styeDivBotoes {
            width: 493px;
        }
    </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script>
    function Imprimir() {
        try {
            document.getElementById('tableLegenda').style.visibility = "hidden"; 
            document.getElementById('tdmenu').style.visibility = "hidden";
            document.getElementById('tableBotoes').style.visibility = "hidden";
            document.getElementById('contentTopRightDiv').style = "height: 100%";
            window.print();
        }
        finally {
            document.getElementById('tableLegenda').style.visibility = "visible";
            document.getElementById('tdmenu').style.visibility = "visible";
            document.getElementById('tableBotoes').style.visibility = "visible";
        }
    }
</script>
<body>
     <form id="form1" runat="server">
            <table class="form2">
                <tr>
                    <td>
                        <uc1:cabecalho ID="cabecalho1" runat="server" ClientIDMode="Inherit" EnableViewState="True" />
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td id="tdmenu" class="Menu">
                        <uc2:menu ID="menu1" runat="server" Visible="false" />                        
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <table id="tableLegenda">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Movimentação DTR" Font-Bold="True" CssClass="titulo2"></asp:Label>        
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <div id="contentTopRightDiv" style="width:98%;height:400px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="370px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data" DataFormatString=" {0:dd/MM/yyyy}">
                                    <HeaderStyle CssClass="padItemGrade" Width="60"/>
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="De" SortExpression="MoviCxDe">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hifMoviCxDe" runat="server" Value='<%# Bind("MoviCxDe") %>'></asp:HiddenField>
                                            <asp:DropDownList ID="ddlMoviCxDe" runat="server" Width="104px" style="border: 0px; border-spacing: 2px;" CssClass="LetrasLabel" BackColor="#ffffcc" Enabled="false">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Para" SortExpression="MoviCxPara">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hifMoviCxPara" runat="server" Value='<%# Bind("MoviCxPara") %>'></asp:HiddenField>
                                            <asp:DropDownList ID="ddlMoviCxPara" runat="server" Width="104px" style="border: 0px; border-spacing: 2px;" CssClass="LetrasLabel" BackColor="#ffffcc">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NumeroLancamento" HeaderText="">
                                    <HeaderStyle CssClass="_visibility" Width="1" />
                                    <ItemStyle CssClass="_visibility" Width="1" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoResiduo" HeaderText="">
                                    <HeaderStyle CssClass="_visibility" Width="1" />
                                    <ItemStyle CssClass="_visibility" Width="1" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoCliente" HeaderText="">
                                    <HeaderStyle CssClass="_visibility" Width="1" />
                                    <ItemStyle CssClass="_visibility" Width="1" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="Black" BackColor="#999999" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tableBotoes">
                            <tr>
                                <td>
                                    <div class="styeDivBotoes">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Salvar" ID="Salvar" OnClick="Salvar_Click" TabIndex="1" />
                                        <input id="inpImprimir" type="button" value="Imprimir" onclick="Imprimir();" />&nbsp;
                                    </div>
                                </td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMensagem" CssClass="LetrasLabel" runat="server"></asp:Label>
                                </td>
                                <td style="width:100%">
                                    <asp:Button ID="btnProcurar" Visible="true" BackColor="ButtonFace" ForeColor="ButtonFace" BorderWidth="0" runat="server" Text="P" Width="1px"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlContaineres" runat="server" style="visibility: hidden">
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
    </form>
</body>
</html>
