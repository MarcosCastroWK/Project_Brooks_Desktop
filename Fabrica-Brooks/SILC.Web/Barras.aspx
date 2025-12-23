<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Barras.aspx.cs" Inherits="SILC.Web.forms_ewvs_Barras" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Chart ID="Chart1" runat="server" Width="654px">
                <Series>
                    <asp:Series Name="Series1" XValueMember="NomeConta" YValueMembers="ValorGrupo" ChartType="Column">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <AxisY Title="Valor grupo">
                        </AxisY>
                        <AxisX Title="Nome conta">
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>
    </form>
</body>
</html>
