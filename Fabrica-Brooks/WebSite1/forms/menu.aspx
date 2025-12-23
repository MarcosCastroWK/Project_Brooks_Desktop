<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="forms_menu" %>

<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>

<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="table.css" type="text/css" rel="stylesheet" />
    <style  type="text/css">
     .CabecalhoMenu
     {
         height: 50px;
         width:100%;
     }
 </style>
</head>
 <body>
     <form id="form1" runat="server">
        <div>
            <table class="form2">
                <tr>
                    <td class="CabecalhoMenu">
                        <uc1:cabecalho ID="cabecalho1" runat="server" />
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <uc2:menu ID="menu1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
