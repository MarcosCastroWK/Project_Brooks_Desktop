<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Residuos.aspx.cs" Inherits="SILC.Web.forms.webResiduos" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="GRUPORESIDUO.ascx" tagname="GRUPORESIDUO" tagprefix="uc7" %>
<%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc8" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style6
        {
            width: 90px;
            font-size: 10px;
            height: 26px;
        }
        .auto-style8
        {
            width: 10px;
            font-size: 9px;
            height: 24px;
        }
        .auto-style10
        {
            width: 10px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style11
        {
            width: 10px;
            font-size: 9px;
            height: 28px;
        }
        </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>    
<script>
    function AbrePesquisaGrupoResiduos()
    {
        document.getElementById("<%=btnProcurar.ClientID%>").click();    
        window.open('PesquisaGrupoResiduos.aspx', '_blank', 'modalDialog');
    }
    function AbrePesquisaDestinoFinal()
    {
        document.getElementById("<%=btnProcurar.ClientID%>").click();
        window.open('PesquisaDestinoFinal.aspx', '_blank', 'modalDialog');
    }
    function AbrePesquisaIBAMA()
    {
        document.getElementById("<%=btnProcurar.ClientID%>").click();
        window.open('PesquisaIBAMA.aspx', '_blank', 'modalDialog');
    }
</script>
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
                        <uc2:menu ID="menu1" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Cadastro&amp;nbsp;de&amp;nbsp;Resíduos" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:98%; min-height:20vh; max-height:42vh; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="0" Width="90%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" >
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudarSernha" runat="server" ImageUrl="~/Images/mudarmostrar.png" ToolTip="Alterar" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" OnClick="ibnExcluir_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Grupo" HeaderText="Grupo" SortExpression="Grupo">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DescricaoReduzida" HeaderText="Resíduo" SortExpression="DescricaoReduzida">
                                    <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoIBAMA_Analitico" HeaderText="Código IBAMA" SortExpression="CodigoIBAMA_Analitico">
                                    <ItemStyle CssClass="padItemGrade" Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Classe" HeaderText="Classe" SortExpression="Classe" >
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DataCadastro" DataFormatString=" {0: dd/MM/yyyy}" HeaderText="Data Cadastro" SortExpression="DataCadastro" />
                                    <asp:BoundField DataField="Ativo" HeaderText="Ativo" SortExpression="Ativo" >
                                    <ItemStyle CssClass="padItemGrade" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DestinoFinal" HeaderText="Destino Final" SortExpression="DestinoFinal">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TecnologiaAplicada" HeaderText="Tecnologia Aplicada" SortExpression="TecnologiaAplicada">
                                    <ItemStyle Width="250px" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="Black" HorizontalAlign="Center" BackColor="#999999" />
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
                    <td valign="center" style="height:1%;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFiltro" CssClass="LetrasLabel" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10px" >&nbsp;Procurar:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFiltro" runat="server" Width="200px">
                                       <asp:ListItem Value="Descrição Reduzida">Residuo</asp:ListItem>
                                       <asp:ListItem>Ativos</asp:ListItem>                                       
                                       <asp:ListItem>Grupo</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFiltro" runat="server" Width="400px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" />
                                </td>                                
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="dadosForm" valign="top" style="height:90%">
                        <table style="width:400px">
                            <tr>
                                <td>
                                    <asp:Label ID="lblCodigo" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" Font-Names="Arial" Font-Size="10px" >&nbsp;Código do Resíduo:</asp:Label>
                                </td>
                                <td colspan="4">
                                    <asp:Label ID="lblCodigoDescricao" CssClass="LetrasLabel" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="14px" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style10">
                                    <asp:Label ID="lblDataCadastro" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" Font-Names="Arial" Font-Size="10px" >&nbsp;Data Cadastro</asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <div style="width:300px;">
                                        <uc6:DATA ID="datDataCadastro" runat="server" />
                                        <asp:CheckBox ID="chkAtivo" runat="server" Text="Ativo" TabIndex="17" Width="100px" onclick="TiraCheckTerceiro();" Font-Bold="False" CssClass="LetrasLabel" />
                                    </div>
                               </td>
                               <td class="LetrasLabel" colspan="4">
                                   <table style="padding:0;border-spacing:0;">
                                       <tr>
                                           <td><uc7:GRUPORESIDUO ID="GRUPORESIDUO1" runat="server" IndiceTab="2" /></td>
                                           <td><img id="imagem" alt="x" src="../Images/procura.png" onclick="AbrePesquisaGrupoResiduos()"/></td>
                                       </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style10">
                                    <asp:Button ID="btnProcurar" Visible="true" BackColor="ButtonFace" ForeColor="ButtonFace" BorderWidth="0" runat="server" Text="P" Width="1px" OnClick="btnProcurar_Click" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkEhReciclavel" runat="server" Text="É Reciclável" CssClass="LetrasLabel" />
                                    <asp:CheckBox ID="chkEhServico" runat="server" Text="É Serviço" CssClass="LetrasLabel" />
                                </td>
                                <td class="LetrasLabel" colspan="4">
                                   <table style="padding:0;border-spacing:0;">
                                       <tr>
                                           <td><uc8:DESTINOFINAL ID="DESTINOFINAL1" runat="server" IndiceTab="2" /></td>
                                           <td>&nbsp;<img id="Img1" alt="x" src="../Images/procura.png" onclick="AbrePesquisaDestinoFinal()"/></td>
                                       </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style10">
                                    <asp:Label ID="lblDescricaoReduzida" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False" >&nbsp;Descrição Reduzida</asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtDescricaoReduzida" runat="server" TabIndex="1" Width="300px"></asp:TextBox>
                                </td>
                                <td class="auto-style10">
                                    <asp:Label ID="Label5" runat="server" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Unidade</asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtUnidade" onfocus="LimpaErro()" runat="server" TabIndex="2" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Código IBAMA" Width="70px" Font-Bold="False" CssClass="LetrasLabel"></asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <table>
                                        <tr>
                                            <td>
                                                <uc4:INTEIRO7 ID="intCodigoIBAMA" runat="server" Enabled="False" IndiceTab="2" />
                                            </td>
                                            <td><img id="Img2" alt="x" src="../Images/procura.png" onclick="AbrePesquisaIBAMA()"/></td>
                                            <td>
                                                <asp:Label ID="lblDescricaoIBAMA" runat="server" CssClass="LetrasLabel" Width="532px" Font-Bold="False"></asp:Label>
                                            </td>
                                         </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style11">
                                    <asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False">&nbsp;Descrição</asp:Label>
                                </td>
                                <td class="auto-style11">
                                    <asp:TextBox ID="txtDescricao" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="300px" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Font-Bold="False" CssClass="LetrasLabel" Font-Size="8pt">&nbsp;ton/Unidade (densidade)</asp:Label>
                                </td>
                                <td class="auto-style11" colspan="3">
                                    <uc5:MOEDA ID="moeM3PorTon" runat="server" IndiceTab="1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasLabel">
                                    <asp:Label ID="Label2" runat="server" Width="110px" Font-Bold="False">&nbsp;Tecnologia Aplicada</asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtTecnologiaAplicada" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="200px" ></asp:TextBox>
                                </td>
                                <td class="auto-style6">
                                    </td>
                                <td class="auto-style6">
                                    &nbsp;</td>
                                <td class="auto-style6">
                                    &nbsp;</td>
                                <td class="auto-style6">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label ID="Label3" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">&nbsp;Código Manifesto</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="txtCodigoResiduoManifesto" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="250px"></asp:TextBox>
                                </td>
                                <td class="LetrasTD">
                                    <asp:Label ID="Label7" runat="server" Font-Bold="False" CssClass="LetrasLabel">&nbsp;Classe</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="txtClasse" onfocus="LimpaErro()" runat="server" TabIndex="2" Width="80px"></asp:TextBox>
                                </td>
                                <td class="LetrasTD">
                                    <asp:Label ID="Label8" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">&nbsp;Estado Físico</asp:Label>
                                    </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="txtEstadoFisico" onfocus="LimpaErro()" runat="server" TabIndex="2" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                    &nbsp;</td>
                                <td class="auto-style8">
                                    &nbsp;</td>
                                <td class="auto-style8">
                                    </td>
                                <td class="auto-style8">
                                    </td>
                                <td class="auto-style8">
                                    </td>
                                <td class="auto-style8">
                                    <div style="width:160px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="2" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Novo" OnClick="btnCancelar_Click" TabIndex="2" />
                                    </div>
                                    </td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lblMensagem" CssClass="LetrasLabel" runat="server"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:HiddenField ID="hidCodigoIBAMA" runat="server" />
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
