<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RGR.aspx.cs" Inherits="SILC.Web.forms.RGR" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="MOTORISTA.ascx" tagname="MOTORISTA" tagprefix="uc7" %>
<%@ Register src="CAMINHAO.ascx" tagname="CAMINHAO" tagprefix="uc8" %>
<%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc9" %>
<%@ Register src="INTEIRO2.ascx" tagname="INTEIRO2" tagprefix="uc1" %>
 
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
        .auto-style2 {
            height: 75px;
        }
    </style>
    </head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script lang="javascript" type="text/javascript">
    function Imprimir()
    {
        try
        {
            document.getElementById("btnMontaDDR").style.visibility = "hidden";
            document.getElementById("btnImprimir").style.visibility = "hidden";
            //document.getElementById("chkTotalResiduo").style.visibility = "hidden";
            //document.getElementById("lblTotalResiduo").style.visibility = "hidden";
            window.print();
        }
        finally
        {
            document.getElementById("btnMontaDDR").style.visibility = "visible";
            document.getElementById("btnImprimir").style.visibility = "visible";
            //document.getElementById("chkTotalResiduo").style.visibility = "visible";
            //document.getElementById("lblTotalResiduo").style.visibility = "visible";
        }
    }
    function FazTotalPorResiduo()
    {
        document.getElementById("<%=btnMontaDDR.ClientID%>").click();
        //window.open('PesquisaMotoristas.aspx', '_blank', 'modalDialog');
    }
</script>
<body>
     <form id="form1" runat="server">
        <div>
            <table style="width:1190px">
                <tr>
                    <td id="dadosCab" style="vertical-align:middle; height: 100px; align-content:center; width:1190px;word-spacing:0;">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="auto-style2">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logotipobrooks.jpg" />
                                </td>
                                <td class="auto-style2">
                                    <asp:Label ID="lblTitulo" runat="server" Text="RELATÓRIO DE GERENCIAMENTO DE RESÍDUOS" Font-Bold="True" CssClass="tituloFundoBranco" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height:30px;width:1190px; vertical-align:bottom; border:0px solid black; font-family:Arial; font-size:9pt;">
                                    <asp:Label ID="lblPeriodoDesejado" runat="server" Height="21px" ForeColor="Maroon" Text="Digite Mês/Ano desejado:" Width="163px"></asp:Label>
                                    <uc1:INTEIRO2 ID="intMes" runat="server" Valor="01" />
                                    <asp:Label ID="lblBarra" runat="server" Height="21px" ForeColor="Maroon" Text="/"></asp:Label>
                                    <uc1:INTEIRO2 ID="intAno" runat="server" Valor="20" />
                                    <asp:Button ID="btnMontaDDR" runat="server" Height="22px" OnClick="btnMontaDDR_Click" Text="Ok" />
                                    <input id="btnImprimir" style="height:22px;" type="button" value="Imprimir" onclick="Imprimir();" /> 
                                </td>
                            </tr>
                        </table>
                     </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;&nbsp;Informações do Gerador</td>
                </tr>
                <tr>
                    <td style="border:1px solid black; font-family:Arial; font-size:9pt;" class="auto-style1">
                        Código-Razão Social:<asp:TextBox ID="txtCodigoCliente" runat="server" MaxLength="6" Width="54px" BorderWidth="0px"></asp:TextBox>
                        <asp:Label ID="lblNomeCliente" runat="server" CssClass="tituloFundoBranco" Text="Nome cliente:"></asp:Label>
                        <asp:Label ID="lblCNPJ" runat="server" CssClass="tituloFundoBranco" Text="CNPJ: 00.000.000/0001-00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border:1px solid black; font-family:Arial; font-size:9pt; text-align: center;" >
                        <br />
                        <br />
                        <asp:Label ID="lblMesAnoReferente" runat="server" Text="referente a"></asp:Label>
                        <br />
                        <br />
                        <br />
                        <asp:Image ID="imgLogoCliente" runat="server" ImageAlign="Middle" />
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="lblPeriodoApuracao" runat="server" Text="apuracao"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td  style="height:20px;width:1190px; text-align:center;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;&nbsp;<asp:Label ID="lblDadosLAO" runat="server" Text="2. ANÁLISE QUANTITATIVA DOS RESÍDUOS COLETADOS"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="padding: 0; word-spacing: 0;">
                            <tr>
                                <td style="width:1190px; padding: 0; word-spacing: 0;">
                                    <asp:GridView ID="GradeMTR" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" OnRowDataBound="GradeMTR_RowDataBound" Width="1190px" GridLines="Vertical">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                                <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                                <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                            </asp:BoundField>                                            <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n2}">
                                                <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n2}">
                                                <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n2}">
                                                <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n2}">
                                                <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n2}">
                                                <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n2}">
                                                <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n2}">
                                                <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n2}">
                                                <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n2}">
                                                <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n2}">
                                                <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n2}">
                                                <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n2}">
                                                <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n2}" HeaderText="Total Ano">
                                                <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MediaAno" DataFormatString="{0:n2}" HeaderText="Média no Ano">
                                                <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                            </asp:BoundField>
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
                                <td></td>
                            </tr>
                            <tr>
                                <td style="width:1190px">
                                    <asp:Chart ID="Chart1" runat="server" Width="1240px" Palette="Excel">
                                        <Series>
                                            <asp:Series Name="Series1" XValueMember="DescricaoReduzida" YValueMembers="JanQuantidade">
                                            </asp:Series>
                                            <asp:Series Name="Series2" XValueMember="DescricaoReduzida" YValueMembers="FevQuantidade">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1">
                                                <AxisY Title="Quantidade">
                                                </AxisY>
                                                <AxisX Title="Ano">
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-family:Arial; font-size:9pt;">
                                    <asp:Label ID="lblCidadeEmpresa" runat="server" Text="Palhoça,"></asp:Label>
                                    <asp:Label ID="lblDataEmissao" runat="server" Text="Data de emissão"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/assinatura sergio.jpg" Visible="False" />
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
