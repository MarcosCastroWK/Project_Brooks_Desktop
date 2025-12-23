<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RGR2.aspx.cs" Inherits="RGR" %>
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
        .auto-style3 
        {
            width: 1190px;
            height: 23px;
        }
        .auto-style4 <asp:HyperLink runat="server">HyperLink</asp:HyperLink>
        {
            height: 23px;
        }
    </style>
    <script src="geral.js" lang="javascript" type="text/javascript"></script>
    <script src="../Scripts/funcaoGeral.js"></script>
    <script lang="javascript" type="text/javascript">
        function Imprimir()
        {   
            try
            {
                document.getElementById("btnMontaRGR").style.visibility = "hidden";
                document.getElementById("btnImprimir").style.visibility = "hidden";
                //document.getElementById("lblTotalResiduo").style.visibility = "hidden";
                window.print();
            }
            finally
            {
                document.getElementById("btnMontaRGR").style.visibility = "visible";
                document.getElementById("btnImprimir").style.visibility = "visible";
            }
        }
        function FazTotalPorResiduo()
        {
            document.getElementById("<%=btnMontaRGR.ClientID%>").click();
        }
    </script>
</head>
<body>
     <form id="form1" runat="server">
        <div id="Operacoes">
            <table style="width:1190px">
                <tr>
                    <td id="dadosCab" style="vertical-align:middle; height: 100px; align-content:center; width:1190px;word-spacing:0;">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logotipobrooks.jpg" Height="45px" Width="133px" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height:30px;width:1190px; vertical-align:bottom; border:0px solid black; font-family:Arial; font-size:9pt;">
                                    <asp:Label ID="lblPeriodoDesejado" runat="server" Height="21px" ForeColor="Maroon" Text="Digite Mês/Ano desejado:" Width="163px"></asp:Label>
                                    <uc1:INTEIRO2 ID="intMes" runat="server" Valor="01" />
                                    <asp:Label ID="lblBarra" runat="server" Height="21px" ForeColor="Maroon" Text="/"></asp:Label>
                                    <uc1:INTEIRO2 ID="intAno" runat="server" Valor="21" />
                                    <asp:Button ID="btnMontaRGR" runat="server" Height="22px" OnClick="btnMontaRGR_Click" Text="Ok" OnClientClick="DesabilitaOperacoes(this.id, '', '', 'RGR');"/>
                                    <input id="btnImprimir" style="height:22px;" type="button" value="Imprimir" onclick="Imprimir();" /> Teste de gráficos
                                    <asp:DropDownList ID="DropDownList1" runat="server" Height="24px" Width="101px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height:30px;width:1190px;border:0px solid black; text-align:center; font-family:Arial; font-size:9pt;">
                                    <asp:Label ID="lblTitulo" runat="server" Text="RELATÓRIO DE GERENCIAMENTO DE RESÍDUOS" Font-Bold="True" CssClass="tituloFundoBranco" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>
                        </table>
                     </td>
                </tr>
                <tr>
                    <td>                       
                        <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden; height:1px;" />
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;&nbsp;Informações do Gerador</td>
                </tr>
                <tr>
                    <td style="border:1px solid black; font-family:Arial; font-size:9pt;">
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
                    <td style="width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="1. ETAPAS DO GERENCIEMANTO"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td  style="height:20px;width:1190px; text-align:center;">
                        <asp:Image ID="imgEtapas" runat="server" ImageUrl="~/Images/etapas.png" ImageAlign="Middle" />
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;&nbsp;<asp:Label ID="lblDadosLAO" runat="server" Text="2. ANÁLISE QUANTITATIVA DOS RESÍDUOS COLETADOS"></asp:Label>
                        <br />
                        &nbsp; 2.1 Resíduos Recicláveis
                    </td>
                </tr>
                <tr>
                    <td style="width:1190px; padding: 0; word-spacing: 0;">
                        <asp:GridView ID="GradeMTR" runat="server" CellPadding="4" BackColor="#999999" BorderColor="WhiteSmoke" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="CodigoResiduo" HeaderText="Código" HeaderStyle-BorderColor="White" HeaderStyle-BackColor="#33cccc" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Tipo/Mês" HeaderStyle-BorderColor="White" HeaderStyle-BackColor="#33cccc" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}" HeaderStyle-BackColor="#33cccc" ItemStyle-BackColor="#c0c0c0" HeaderStyle-BorderColor="White">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle Wrap="False" />
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="White" BorderWidth="1"/>
                            <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="#F7F7DE" BorderColor="White" />
                            <RowStyle BackColor="#F7F7DE" BorderColor="White" BorderWidth="1"/>
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
                    <td style="width:1300px">
                        <asp:Chart ID="Chart1" runat="server" Width="1300px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;&nbsp; ANÁLISE % RECICLAGEM e DESVIO ATERRO
                    </td>
                </tr>
                <tr>
                    <td style="width:1190px; padding: 0; word-spacing: 0;">
                        <asp:GridView ID="GradeReciclagemDesvioAterro" runat="server" CellPadding="4" BackColor="#999999" BorderColor="WhiteSmoke" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Tipo/Mês" HeaderStyle-BorderColor="White" HeaderStyle-BackColor="#33cccc" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n2}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n2}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n2}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n2}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n2}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n2}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n2}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n2}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n2}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n2}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n2}" HeaderStyle-BackColor="#33cccc" ItemStyle-BackColor="#c0c0c0" HeaderStyle-BorderColor="White">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n2}" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n2}" HeaderText="Total Ano" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n2}" HeaderText="Média no Ano" HeaderStyle-BackColor="#33cccc" HeaderStyle-BorderColor="White" ItemStyle-BackColor="#c0c0c0">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle Wrap="False" />
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="White" BorderWidth="1"/>
                            <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="#F7F7DE" BorderColor="White" />
                            <RowStyle BackColor="#F7F7DE" BorderColor="White" BorderWidth="1"/>
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </td>
                </tr>                            
                <tr>
                    <td>
                        <asp:Chart ID="ChartReciclagemDesvioAterro" runat="server" Width="800px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="2.2 Resíduos Orgânicos - Compostagem (kg)"></asp:Label>
                    </td>
                </tr>   
                <tr>
                    <td>
                        <asp:GridView ID="GradeCompostagem" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>                                            <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano">
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
                </tr>                            
                <tr>
                    <td style="width:960px">
                        <asp:Chart ID="Chart2" runat="server" Width="1160px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="2.3 -  Resíduo Comum"></asp:Label>
                    </td>
                </tr>   
                <tr>
                    <td>
                        <asp:GridView ID="GradeResiduoComum" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>                                            <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano">
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
                </tr>                            
                <tr>
                    <td style="width:1190px">
                        <asp:Chart ID="Chart3" runat="server" Width="960px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="2.4 RCD - Resíduo da Construção e Demolição"></asp:Label>
                    </td>
                </tr>   
                <tr>
                    <td>
                        <asp:GridView ID="GradeRCD" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano">
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
                </tr>                            
                <tr>
                    <td>
                        <asp:Chart ID="Chart4" runat="server" Width="960px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;&nbsp;<asp:Label ID="Label5" runat="server" Text="2.5 Sólidos Contaminados (kg)"></asp:Label>
                    </td>
                </tr>   
                <tr>
                    <td>
                        <asp:GridView ID="GradeSolidosContaminados" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano">
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
                </tr>                            
                <tr>
                    <td style="width:1190px;font-family:Arial; font-size:9pt;">
                        <asp:Chart ID="Chart5" runat="server" Width="960px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp; 2.6 Resíduos Eletroeletrônicos Recicláveis (kg)
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GradeEletroeletronicosReciclaveis" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano">
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
                </tr>
                <tr>
                    <td style="width:1190px;font-family:Arial; font-size:9pt;">
                        <asp:Chart ID="Chart6" runat="server" Width="960px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;2.7 Resíduos Eletroeletrônicos Não Recicláveis (Kg)
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GradeNaoReciclaveis" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano">
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
                </tr>
                <tr>
                    <td style="width:1190px;font-family:Arial; font-size:9pt;">
                        <asp:Chart ID="Chart7" runat="server" Width="960px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;2.8 Lâmpadas Fluorescentes
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GradeLampadasFluorescentes" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano">
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
                </tr>
                <tr>
                    <td style="width:1190px;font-family:Arial; font-size:9pt;">
                        <asp:Chart ID="Chart8" runat="server" Width="960px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;2.9 Pilhas e Baterias (kg)
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GradePilhasBaterias" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano">
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
                </tr>
                <tr>
                    <td style="width:1190px;font-family:Arial; font-size:9pt;">
                        <asp:Chart ID="Chart9" runat="server" Width="960px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;2.10 RSS - Resíduo de Serviço de Saúde
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GradeServicoSaude" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano">
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
                </tr>
                <tr>
                    <td style="width:1190px;font-family:Arial; font-size:9pt;">
                        <asp:Chart ID="Chart10" runat="server" Width="960px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;2.11 Tintas e Afins (kg)
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GradeTintasAfins" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano">
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
                </tr>
                <tr>
                    <td style="width:1190px;font-family:Arial; font-size:9pt;">
                        <asp:Chart ID="Chart11" runat="server" Width="960px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;2.12 Resto de Produto Químico (kg)
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GradeRestoProdutoQuimico" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano">
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
                </tr>
                <tr>
                    <td style="width:1190px;font-family:Arial; font-size:9pt;">
                        <asp:Chart ID="Chart12" runat="server" Width="960px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;2.13 Óleo Lubrificante Usado
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GradeOleoLubrificanteUsado" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano">
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
                </tr>
                <tr>
                    <td style="width:1190px;font-family:Arial; font-size:9pt;">
                        <asp:Chart ID="Chart13" runat="server" Width="960px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;2.14 Efluentes (m³)
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GradeEfluentes" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano">
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
                </tr>
                <tr>
                    <td style="width:1190px;font-family:Arial; font-size:9pt;">
                        <asp:Chart ID="Chart14" runat="server" Width="960px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;2.15 Lodos (kg)
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GradeLodos" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" Width="1190px" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="DescricaoReduzida" HeaderText="Descrição">
                                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>                                            
                                <asp:BoundField DataField="JanQuantidade" HeaderText="Janeiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="FevQuantidade" HeaderText="Fevereiro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MarQuantidade" HeaderText="Março" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AbrQuantidade" HeaderText="Abril" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaiQuantidade" HeaderText="Maio" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JunQuantidade" HeaderText="Junho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="JulQuantidade" HeaderText="Julho" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="AgoQuantidade" HeaderText="Agosto" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="SetQuantidade" HeaderText="Setembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OutQuantidade" HeaderText="Outubro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="NovQuantidade" HeaderText="Novembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="DezQuantidade" HeaderText="Dezembro" DataFormatString="{0:n0}">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQuantidadeAno" DataFormatString="{0:n0}" HeaderText="Total Ano">
                                    <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="MediaAno" DataFormatString="{0:n0}" HeaderText="Média no Ano">
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
                </tr>
                <tr>
                    <td style="width:1190px;font-family:Arial; font-size:9pt;">
                        <asp:Chart ID="Chart15" runat="server" Width="960px" Height="44px">
                            <MapAreas>
                                <asp:MapArea Coordinates="0,0,0,0" />
                            </MapAreas>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Quantidade">
                                    </AxisY>
                                    <AxisX>
                                        <LabelStyle Interval="1" />
                                        <ScaleBreakStyle Spacing="1" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td style="height:50px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;3. CONSIDERAÇÕES FINAIS
                    </td>
                </tr>
                <tr>
                    <td style="height:50px;width:1190px;font-family:Arial; font-size:9pt;">
                        &nbsp;Havendo dúvidas, sugestões ou novas necessidades, estaremos a seu dispor.
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2" style="height:50px;font-family:Arial; font-size:9pt;">
                        &nbsp;<asp:Label ID="lblCidadeEmpresa" runat="server" Text="Palhoça,"></asp:Label>
                        <asp:Label ID="lblDataEmissao" runat="server" Text="Data de emissão"></asp:Label>
                        <br />
                        <br />
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/assinatura sergio.jpg" Visible="False" />
                    </td>
                </tr>
                <asp:HiddenField ID="hifCodigo" runat="server" />
            </table>
            
        </div>
         <div id="divImprimir">
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
         </div>
    </form>
</body>
</html>