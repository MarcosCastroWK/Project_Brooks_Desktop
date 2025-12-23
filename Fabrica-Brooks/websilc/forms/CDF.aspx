<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CDF.aspx.cs" Inherits="CDF" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="MOTORISTA.ascx" tagname="MOTORISTA" tagprefix="uc7" %>
<%@ Register src="CAMINHAO.ascx" tagname="CAMINHAO" tagprefix="uc8" %>
<%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc9" %>
<%@ Register src="INTEIROMES.ascx" tagname="INTEIROMES" tagprefix="uc10" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 50%;
            height: 30px;
        }
        .auto-style4 {
            width: 800px;
        }
        .auto-style5 {
            width: 10px;
        }
        .auto-style6 {
            width: 800px;
        }
        .auto-style7 {
            width: 800px;
            height: 30px;
        }
    </style>
    </head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script lang="javascript" type="text/javascript">
    function Imprimir()
    {
        try
        {
            document.getElementById("btnMontaCDF").style.visibility = "hidden";
            document.getElementById("btnImprimir").style.visibility = "hidden";
            document.getElementById("butVoltar").style.visibility = "hidden";
            window.print();
        }
        finally
        {
            document.getElementById("btnMontaCDF").style.visibility = "visible";
            document.getElementById("btnImprimir").style.visibility = "visible";
            document.getElementById("butVoltar").style.visibility = "visible";
        }
    }
    function FazTotalPorResiduo()
    {
        document.getElementById("<%=btnMontaCDF.ClientID%>").click();
    }
</script>
<body>
     <form id="form1" runat="server">
        <div>
            <table class="auto-style4">
                <tr>
                    <td id="dadosCab" style="vertical-align:middle; height: 100px; align-content:center; width:800px;word-spacing:0;">
                        <table cellpadding="0" cellspacing="0" style="word-spacing:0;" class="auto-style6">
                            <tr>
                                <td>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logotipobrooks.jpg" />
                                </td>
                                <td>
                                    <asp:Label ID="lblTitulo" runat="server" Text="CDF - CERTIFICADO DE DESTINAÇÃO DE FINAL" Font-Bold="True" CssClass="tituloFundoBranco" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height:20px; border:1px solid black; font-family:Arial; font-size:9pt;" >
                                    &nbsp;BROOKS AMBIENTAL LTDA, CNPJ: 03.938.048/0001-33, situada a Av. Ivo Lucchi, 729
                                    - Área Industrial, Palhoça - SC, declara que recebeu os&nbsp;<br />&nbsp;resíduos listados a seguir coletados no Gerador identificado abaixo, no período de <asp:Label ID="lblPeriodoDesejado" runat="server" ForeColor="Maroon" Text="Selecione mês e ano desejado:"></asp:Label>
                                    <uc10:INTEIROMES ID="intMes" runat="server" IndiceTab="0" />
                                    <uc3:INTEIRO ID="intAno" runat="server" IndiceTab="0" />, os Resíduos listados a seguir:&nbsp;<br />
                                    <asp:Button ID="btnMontaCDF" runat="server" OnClick="btnMontaCDF_Click" Text="Ok" />
                                    <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprimir();" class="letralabel" />
                                    <asp:Button ID="butVoltar" runat="server" Text="Voltar" OnClick="butVoltar_Click" />
                                </td>
                            </tr>
                        </table>
                     </td>
                </tr>
                <tr>
                    <td style="height:20px;width:800px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;Informações do Gerador</td>
                </tr>
                <tr>
                    <td style="border:1px solid black; font-family:Arial; font-size:9pt;" class="auto-style1">
                        Código-Razão Social:<asp:TextBox ID="txtCodigoCliente" runat="server" MaxLength="6" Width="54px" BorderWidth="0px"></asp:TextBox>
                        <asp:Label ID="lblNomeCliente" runat="server" CssClass="tituloFundoBranco" Text="Nome cliente:"></asp:Label>
                        <asp:Label ID="lblCNPJ" runat="server" CssClass="tituloFundoBranco" Text="CNPJ: 00.000.000/0001-00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border:1px solid black; font-family:Arial; font-size:9pt;" class="auto-style7">
                        <asp:Label ID="lblEnderecoT" runat="server" Text="Endereço:"></asp:Label>
                        <asp:Label ID="lblEndereco" runat="server" CssClass="tituloFundoBranco" Text="Endereço"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:800px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;<asp:Label ID="lblDadosLAO" runat="server" Text="Informações do(s) Resíduo(s)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width:800px" cellpadding="0" cellspacing="0">
                                    <asp:GridView ID="GradeMTR" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" OnRowDataBound="GradeMTR_RowDataBound" Width="800px" GridLines="Vertical">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="DescricaoResiduo" HeaderText="Descrição">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CodigoIBAMA" HeaderText="Código">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DescricaoResiduo" HeaderText="Descrição no IBAMA">
                                            <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="300px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Classe" HeaderText="Classe">
                                            <HeaderStyle CssClass="padItemGrade"  HorizontalAlign="Center" Width="35"/>
                                            <ItemStyle CssClass="padItemGrade"  HorizontalAlign="Center"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QtdeDestinada" HeaderText="Quantidade" DataFormatString="{0:n2}">
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unidade" HeaderText="Un">
                                            <HeaderStyle CssClass="padItemGrade"  HorizontalAlign="Center" Width="20" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Tecnologia Aplicada" DataField="TecnologiaAplicada" />
                                        </Columns>
                                        <EditRowStyle Wrap="False" />
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="DimGray" BorderWidth="1"/>
                                        <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="#F7F7DE" />
                                        <RowStyle BackColor="#F7F7DE" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                    </asp:GridView>
                                </td>
                                <td class="auto-style5"></td>
                            </tr>                            
                            <tr><td style="height: 4px;"></td></tr>
                            <tr>
                                <td style="height:20px;width:800px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                                    &nbsp;<asp:Label ID="lblDestinador" runat="server" Text="Dados do Licenciamento Ambiental do Destinador "></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:800px" cellpadding="0" cellspacing="0">
                                    <asp:GridView ID="GradeDestinador" runat="server" CellPadding="4" BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="800px" OnRowDataBound="GradeDestinador_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="NomeDestinoFinal" HeaderText="Destinador Final">
                                            <HeaderStyle HorizontalAlign="Left" Width="300PX"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumeroLicenca" HeaderText="Licença Ambiental Operação/Arquivo">
                                            <HeaderStyle CssClass="padItemGrade" Width="220px" HorizontalAlign="Left" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CodigoAtividade" HeaderText="Código Atividade Principal">
                                            <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PrazoValidade" HeaderText="Prazo de Validade" >
                                            <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Observacao" HeaderText="Observação">
                                            <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="200px" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" Width="400px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CodigoAterro" HeaderText="cd">
                                            <HeaderStyle ForeColor="White" Width="1px" />
                                            <ItemStyle ForeColor="White" Width="1px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                                        <PagerStyle ForeColor="Black" HorizontalAlign="Center" BackColor="#999999" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                </td>
                                <td class="auto-style5">
                                </td>
                            </tr>
                            <tr><td>&nbsp;</td></tr>
                            <tr>
                                <td colspan="2" style="font-family:Arial; font-size:9pt;">
                                    <asp:Label ID="lblDeclaracao1" runat="server" Text="Declaração"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-family:Arial; font-size:9pt;">
                                    <asp:Label ID="lblDeclaracao2" runat="server" Text="Este documento certifica o recebimento dos resíduos acima relacionados, utilizando-se as tecnologias mencionadas."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-family:Arial; font-size:9pt;">
                                    <asp:Label ID="lblDeclaracao3" runat="server" Text="A validade desta informação está restrita aos resíduos declarados e as suas respectivas quantidades."></asp:Label>
                                </td>
                            </tr>
                            <tr><td>&nbsp;</td></tr>
                            <tr><td>&nbsp;</td></tr>
                            <tr>
                                <td colspan="2" style="font-family:Arial; font-size:9pt;">
                                    <asp:Label ID="lblCidadeEmpresa" runat="server" Text="Palhoça,&nbsp;"></asp:Label>
                                    <asp:Label ID="lblDataEmissao" runat="server" Text="Data de emissão"></asp:Label>                                   
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-family:Arial; font-size:9pt;">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/assinatura sergio.jpg" />
                                </td>
                            </tr>
                        </table>                       
                        <asp:HiddenField ID="hifCodigo" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
