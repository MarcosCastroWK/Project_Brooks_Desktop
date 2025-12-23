<%@ Page Language="C#" AutoEventWireup="true" CodeFile="layout.aspx.cs" Inherits="forms_layout" %>

<!DOCTYPE html>
<html>
<head>
    <title>Splitter</title>
    <script language="javascript" type="text/javascript" src="JSFuncoes.js"></script>
</head>
<body onmousemove="resizePanel();" onmouseup="setResizePanelFalse();">    
    <form id="form1" runat="server">
        <pre>
            <table cellpadding="0" cellspacing="0" border="0" style="width:100%">
                <tr id="mainTR">
                    <td id="panelLeftTD" style="width:200px;background-color:#DBDBDB;cursor:default;" valign="top"></td>
                    <td style="width:8px;background-color:#A0C0E7;cursor:e-resize;" onmousedown="setResizePanelHTrue();"></td>
                    <td id="panelRightTD" style="width:200px;background-color:#DBDBDB;" valign="top">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr id="panelTopRightTR" style="height:200px;cursor:default;">
                                <td>
                                    <div id="contentTopRightDiv" style="width:100%;overflow-y:scroll;">
                                            Conteúdo do painel direito/topo. 
                                    </div>
                                </td>
                            </tr>
                            <tr style="height:8px;background-color:#A0C0E7;cursor:n-resize;" onmousedown="setResizePanelVTrue();">
                                <td></td>
                            </tr>
                            <tr id="panelBottomRightTR" style="height:800PX;cursor:default;">
                                <td>
                                    <div id="contentBottomRightDiv" style="width:100%;height:100%;overflow-y:scroll;">
                                        Conteúdo do painel direito/fundo. 
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </pre>
    </form>
</body>