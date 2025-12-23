<%@ Page Language="C#" AutoEventWireup="true" CodeFile="programacao.aspx.cs" Inherits="programacao" %>
<%@ Register Src="cabecalho.ascx" TagPrefix="uc1" TagName="cabecalho" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="GRUPORESIDUO.ascx" tagname="GRUPORESIDUO" tagprefix="uc7" %>
<%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc8" %>
<%@ Register Src="~/forms/INTEIRO7.ascx" TagPrefix="uc1" TagName="INTEIRO7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style22 {
            height: 1%;
            width: 88%;
        }
    </style>
    <script src="../Scripts/funcaoGeral.js"></script>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<body>
     <form id="form1" runat="server">
        <div id="Operacoes">
            <table class="form2">
                <tr>
                    <td style="height:1%;" colspan="2">
                        <uc1:cabecalho ID="cabecalho1" runat="server" ClientIDMode="Inherit" EnableViewState="True" />
                    </td>
                </tr>
                <tr>
                    <td class="Menu" colspan="2">
                        <uc2:menu ID="menu" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" valign="top" style="font-size:9pt;">
                        <asp:TextBox ID="txtDataProgramacaoAberta" runat="server" Width="88px"></asp:TextBox>
                     </td>
                    <td>
                        <table style="word-spacing: 0; padding: 0; border: 0;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Serviços Executados" Font-Bold="True" Font-Names="Arial" CssClass="titulo2"></asp:Label>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imbExcel1" runat="server" ImageUrl="~/Images/excel2010.png" OnClick="imbExcel1_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top" rowspan="6" style="width:280px;">
                        <div id="Div2" style="width:100px;height:460px; overflow-y:scroll;border:ridge 3px;font-size:10pt;" >
                            <asp:TreeView ID="trvPeriodo" runat="server" Width="92%" Height="10px" Font-Names="Arial" Font-Size="10px" NodeIndent="20" OnSelectedNodeChanged="trvPeriodo_SelectedNodeChanged" OnLoad="trvPeriodo_Load">
                                <Nodes>
                                    <asp:TreeNode Text="Ano" Value="Ano">
                                        <asp:TreeNode Text="Mes" Value="Mes">
                                            <asp:TreeNode Text="1" Value="1"></asp:TreeNode>
                                            <asp:TreeNode Text="2" Value="2"></asp:TreeNode>
                                        </asp:TreeNode>
                                    </asp:TreeNode>
                                </Nodes>
                                <ParentNodeStyle VerticalPadding="0px" NodeSpacing="2" HorizontalPadding="1px" />
                                <RootNodeStyle ChildNodesPadding="0px" NodeSpacing="2" HorizontalPadding="1px" />
                            </asp:TreeView>
                        </div>
                        <div id="divLinhas" style="width: 100px; text-align:center; font-weight: bold;">
                            <asp:Label ID="lblLinhas" runat="server" CssClass="LetrasLabel" Text="Linhas: 000/000"></asp:Label>
                        </div>
                    </td>
                    <td valign="top" style="width:90%;">
                        <div id="contentTopRightDiv" style="width:100%; height:250px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade1" runat="server" CellPadding="4" Width="2000px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" Font-Bold="False" PageSize="5" OnRowDataBound="Grade_RowDataBound" Height="171px" >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="Sequencial" HeaderText="Sequencial" SortExpression="Sequencial">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoCliente" HeaderText="Código Cliente" SortExpression="CodigoCliente" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeFantasiaCliente" HeaderText="Nome Fantasia" SortExpression="NomeFantasiaCliente">
                                    <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Data" DataFormatString="{0:dd/MM/yy}" HeaderText="Data" SortExpression="Data" />
                                    <asp:BoundField DataField="Hora" HeaderText="Hora" SortExpression="Hora" />
                                    <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" SortExpression="Solicitante" />
                                    <asp:BoundField DataField="ExecutarServico" HeaderText="Executar Serviço" SortExpression="ExecutarServico" />
                                    <asp:BoundField DataField="DataProgramada" DataFormatString="{0:dd/MM/yy}" HeaderText="DataProgramada" SortExpression="DataProgramada" />
                                    <asp:BoundField DataField="HoraProgramada" HeaderText="Serviço Executado" SortExpression="HoraProgramada" />
                                    <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" SortExpression="Quantidade" />
                                    <asp:BoundField DataField="CodigoCaminhao" HeaderText="Código Caminhao" SortExpression="CodigoCaminhao" />
                                    <asp:BoundField DataField="ModeloCaminhao" HeaderText="Caminhão" SortExpression="ModeloCaminhao" />
                                    <asp:BoundField DataField="CodigoMotorista" HeaderText="Código Motorista" SortExpression="CodigoMotorista" />
                                    <asp:BoundField DataField="NomeMotorista" HeaderText="Motorista" SortExpression="NomeMotorista" />
                                    <asp:BoundField DataField="Observacao" HeaderText="Observacao" SortExpression="Observacao" />
                                    <asp:BoundField DataField="Unidade" HeaderText="Unidade" SortExpression="Unidade" />
                                    <asp:BoundField DataField="TipoProgramacao" HeaderText="Tipo Programação" SortExpression="TipoProgramacao" />
                                    <asp:BoundField DataField="RotaMapa" HeaderText="Rota Mapa" SortExpression="RotaMapa" />
                                    <asp:BoundField DataField="Origem" HeaderText="Origem" SortExpression="Origem" />
                                    <asp:BoundField DataField="StatusCor" HeaderText="StatusCor" SortExpression="StatusCor" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                         </div>
                    </td>
                </tr>
                <tr>
                    <td valign="center" style="height:1%; width:100%;">
                        <table>
                            <tr>
                                <td><asp:Label ID="lblFiltro" CssClass="LetrasLabel" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10px" >&nbsp;Procurar:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFiltro" runat="server" Width="200px">
                                        <asp:ListItem>NomeFantasia</asp:ListItem>
                                        <asp:ListItem Value="Nome"></asp:ListItem>
                                        <asp:ListItem>CodigoCliente</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFiltro" runat="server" Width="400px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" ToolTip="Máximos 1000 registros" OnClientClick="DesabilitaOperacoes(this.id, '', '', 'Programação');" />
                                </td>
                                
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="dadosForm" valign="top" style="height:90%; width:100%;">
                        <table style="height:90%; width:100%;">
                            <tr>
                                <td>
                                    <table style="word-spacing: 0; padding: 0; border: 0;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTitulo0" runat="server" Text="Serviços Programados" Font-Bold="True" Font-Names="Arial" CssClass="titulo2"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imbExcel0" runat="server" ImageUrl="~/Images/excel2010.png" OnClick="imbExcel0_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="Div1" style="width:100%;height:480px; overflow-y:scroll;border:ridge 5px;font-size:10pt;">
                                        <asp:GridView ID="Grade2" runat="server" CellPadding="3" Width="2000px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" GridLines="Vertical" Font-Bold="False" PageSize="5" OnRowCommand="Grade2_RowCommand" OnRowDataBound="Grade2_RowDataBound" >
                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                            <Columns>
                                                <asp:BoundField DataField="Sequencial" HeaderText="Sequencial" SortExpression="Sequencial">
                                                <HeaderStyle CssClass="padItemGrade" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CodigoCliente" HeaderText="Código Cliente" SortExpression="CodigoCliente" >
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NomeFantasiaCliente" HeaderText="Nome Fantasia" SortExpression="NomeFantasiaCliente">
                                                <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Data" DataFormatString="{0:dd/MM/yy}" HeaderText="Data" SortExpression="Data" />
                                                <asp:BoundField DataField="Hora" HeaderText="Hora" SortExpression="Hora" />
                                                <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" SortExpression="Solicitante" />
                                                <asp:BoundField DataField="ExecutarServico" HeaderText="Executar Serviço" SortExpression="ExecutarServico" />
                                                <asp:BoundField DataField="DataProgramada" DataFormatString="{0:dd/MM/yy}" HeaderText="DataProgramada" SortExpression="DataProgramada" />
                                                <asp:BoundField DataField="HoraProgramada" HeaderText="Serviço Executado" SortExpression="HoraProgramada" />
                                                <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" SortExpression="Quantidade" />
                                                <asp:BoundField DataField="CodigoCaminhao" HeaderText="Código Caminhao" SortExpression="CodigoCaminhao" />
                                                <asp:BoundField DataField="ModeloCaminhao" HeaderText="Caminhão" SortExpression="ModeloCaminhao" />
                                                <asp:BoundField DataField="CodigoMotorista" HeaderText="Código Motorista" SortExpression="CodigoMotorista" />
                                                <asp:BoundField DataField="NomeMotorista" HeaderText="Motorista" SortExpression="NomeMotorista" />
                                                <asp:BoundField DataField="Observacao" HeaderText="Observacao" SortExpression="Observacao" />
                                                <asp:BoundField DataField="Unidade" HeaderText="Unidade" SortExpression="Unidade" />
                                                <asp:BoundField DataField="TipoProgramacao" HeaderText="Tipo Programação" SortExpression="TipoProgramacao" />
                                                <asp:BoundField DataField="RotaMapa" HeaderText="Rota Mapa" SortExpression="RotaMapa" />
                                                <asp:BoundField DataField="Origem" HeaderText="Origem" SortExpression="Origem" />
                                                <asp:BoundField DataField="StatusCor" HeaderText="StatusCor" SortExpression="StatusCor" />
                                            </Columns>
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divImprimir" style="top:30%; left:30%; position:absolute">
            <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden;" />
            <asp:Panel ID="Panel1" runat="server" Height="500px"></asp:Panel>
        </div>
    </form>
</body>
</html>
