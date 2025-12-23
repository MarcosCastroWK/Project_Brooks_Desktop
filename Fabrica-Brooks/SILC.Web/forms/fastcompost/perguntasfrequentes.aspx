<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="perguntasfrequentes.aspx.cs" Inherits="SILC.Web.forms.fastcompost.perguntasfrequentes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title></title>
     <style>
        .body 
        {              
            background-image:url('fundobrooks.jpg');
        }
        .divcentral
        {
            background-image: url('sedeperguntasfrequentes.jpg'); width: 1000px; height:334px;margin:auto;
        }
        .divmenuservicos
        {
            background-image: url('menuservicos.jpg'); width: 1000px; height:185px;margin:auto;
        }
        .divinstalacoes
        {
            background-image: url('perguntasfrequentes2.jpg'); width: 1000px; height:701px;margin:auto;
            font-size: 10pt;
        }
        .login 
        {
            width: 360px;
            background: white;
            position:relative;
            left: 30%;
            top: 26%;
            color:black;
            font-size:8pt;
            font-family:Arial;
            color: black;
            border:ridge 8px ActiveBorder;
        }
        .cabecbrooks
        {
            background: transparent center center repeat-y; width: 1000px; auto;margin: auto;
            color:black;            
            font-size:11pt;
        }
        .rodapebrooks
        {
            background: transparent center center repeat-y; width: 1000px;margin: auto;
            color:black;            
            font-size:11pt;
        }
    </style>
    <script type="text/javascript" src="imagemvermelha.js"> </script>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<body class="body">
    <form id="form1" runat="server">
        <div class="cabecbrooks">
            <table style="text-align:center;" border:"0"; cellpadding="0" cellspacing="0">
                <tr>
                    <td><asp:Image ID="Image1" runat="server" ImageAlign="Middle" ImageUrl="~/Images/cabec1brooks.jpg" /></td>
                    <td style="position: relative; width: 280px; background-image: url('../../Images/backareacliente.jpg')" >
                        <asp:HyperLink ID="lnkAreaCliente" runat="server" NavigateUrl="~/forms/brooks/login.aspx" Font-Bold="true" ForeColor="Brown">Área do Cliente</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divcentral">
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <a href="default.aspx"><asp:Image ID="Image5" runat="server" ImageUrl="~/forms/brooks/home.jpg" BorderWidth="0" /></a>
                    </td>
                    <td style="position:relative; left:80px;">
                        <a href="brooksambiental.aspx"><img id="Image6" src="brooksambiental.jpg" style="left:200px;border:0px;" /></a>
                    </td>
                    <td style="position:relative; left:150px;">
                        <a href="equipamentos.aspx"><img id="Img1" src="equipamentos.jpg" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; left:240px;">
                        <a href="legislacao.aspx"><img id="Img2" src="legislacao.jpg" style="left:200px;border:0px;" /></a>
                    </td>
                    <td style="position:relative; left:310px;">
                        <a href="perguntasfrequentes.aspx"><img id="Img3" src="perguntasfrequentes.jpg" style="left:200px;border:0px;" /></a>
                    </td>
                    <td style="position:relative; left:384px;">
                        <a href="contato.aspx"><img id="Img4" src="contato.jpg" style="left:200px;border:0px;" /></a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divmenuservicos">        
            <table>
                <tr>
                    <td style="position:relative; top: 35px; left: 62px;">
                        <a href="assessoria.aspx"><img id="Img5" src="assessoriafundo.jpg" onmouseover="assessoria();" style="border:0px;"/></a>
                    </td>
                    <td style="position:relative; top: 26px; left:62px;">
                        <a href="coletaseletiva.aspx"><img id="Img6" src="coletaseletiva.jpg" onmouseover="coletaseletiva();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 34px; left:70px;">
                        <a href="coletaresiduos.aspx"><img id="Img7" src="coletaresiduosfundo.jpg" onmouseover="coletaresiduos();" style="border:0px;" /></a>
                    </td>                
                    <td style="position:relative; top: 36px; left:74px;">
                        <a href="RCD.aspx"><img id="Img8" src="RCDfundo.jpg" onmouseover="RCD();" style="border:0px;" /></a>
                    </td>                
                    <td style="position:relative; top: 36px; left:76px;">
                        <a href="RSS.aspx"><img id="Img9" src="RSSfundo.jpg" onmouseover="RSS();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 32px; left:68px;">
                        <a href="hidrojateamento.aspx"><img id="Img10" src="hidrojateamentofundo.jpg" onmouseover="hidrojateamento();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 32px; left:64px;">
                        <a href="gestaoglobal.aspx"><img id="Img11" src="gestaoglobalfundo.jpg" onmouseover="gestaoglobal();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 30px; left:60px;">
                        <a href="USTE.aspx"><img id="Img12" src="USTEfundo.jpg" onmouseover="USTE();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 28px; left:62px;">
                        <a href="DTR.aspx"><img id="Img13" src="dtrfundo.jpg" onmouseover="DTR();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 28px; left:62px;">
                        <a href="destinacaofinal.aspx"><img id="Img14" src="destinacaofinalfundo.jpg" onmouseover="destinacaofinal();" style="border:0px;" /></a>
                    </td>
                    <td style="position:relative; top: 34px; left:67px;">
                        <a href="logistica.aspx"><img id="Img15" src="logisticafundo.jpg" onmouseover="logistica();" style="border:0px;" /></a>
                    </td>
                </tr>
            </table>
        </div>
        <div id="instalacoes" class="divinstalacoes">
            <div style="position:relative; left: 392px; top: 30px;">
                <span style="font-size:10pt;">
                Selecione as perguntas abaixo e conheça mais sobre os serviços, métodos, curiosidades <br />
                e como a Brooks Ambiental tem um serviço que cabe e agrega valor a sua empresa.<br />
                </span>
                <br />
                <div id="question-01" onclick="question('q1');">
                    <span id="q0" style="font-size:10pt;" onmouseover="selecionar('q0');" onmouseout="deselecionar('q0');">O que são resíduos?</span><br />
                    <span id="q1" style="position:relative;left:22px;display: none;">Material considerado inútil, sem valor gerado pela atividade humana, que precisa ser eliminado.</span>
                </div>
                <div id="question-02" onclick="question('q12');">
                    <br />
                    <span id="q02" style="font-size:10pt;" onmouseover="selecionar('q02');" onmouseout="deselecionar('q02');">Como são classificados os resíduos?</span>
                    <br />
                    <span id="q12"style="position:relative;left:22px;display: none;" >
                        Os resíduos são classificados de acordo com a ABNT (Associação Brasileira de Normas Técnicas) na sua<br /> 
                        norma NBR 10.004 – Classificação de resíduos, em resíduos Classe I (perigosos) e Classe IIA (não <br />
                        perigosos e não inertes) e Classe IIB (não perigosos e inertes).<br />
                        Para se conhecer a classificação de um resíduo específico, deve ser realizado um Ensaio de  <br />
                        Caracterização de Resíduo, em laboratório especializado, de acordo com as normas NBR.<br />
                        NBR 10005 – Lixiviação de Resíduos – Procedimento<br />
                        NBR 10006 – Solubilização de Resíduos – Procedimento<br />
                        NBR 10007 – Amostragem de Resíduos – Procedimento
                    </span>
                </div>
                <div id="question-03" onclick="question('q13');">
                    <br />
                    <span id="q03" style="font-size:10pt;" onmouseover="selecionar('q03');" onmouseout="deselecionar('q03');">O que são resíduos Classe I (perigosos)?</span>
                    <br />
                    <span id="q13" style="position:relative;left:22px;display: none;">
                        São aqueles que de acordo com as suas propriedades físicas, químicas e biológicas<br />
                        podem provocar danos a saúde pública e ao meio ambiente. As características que<br />
                        conferem periculosidade a um resíduo são: inflamabilidade, corrosividade, reatividade,<br />
                        toxicidade e patogenicidade.<br />
                        Exemplos: estopas e embalagens contaminadas com óleo, tintas, thinner, pilhas e<br />
                        baterias, lâmpadas fluorescentes, resíduos de serviço de saúde, etc.
                    </span>
                </div>
                <div id="question-82" onclick="question('q14');">
                    <br />
                    <span id="q04" style="font-size:10pt;" onmouseover="selecionar('q04');" onmouseout="deselecionar('q04');">O que são resíduos Classe IIA (não perigosos e não inertes)?</span>
                    <br />
                    <br />
                    <span id="q14" style="position:relative;left:22px;display: none;">
                        São os resíduos que não se enquadram na Classe I (perigosos) nem na Classe IIB (não<br />
                        perigosos e inertes), conforme NBR 10004. Esses resíduos podem ter propriedade como:<br />
                        combustibilidade, solubilidade em água e biodegradabilidade.<br />
                        Exemplo: papel, papelão, isopor, pneus, resíduo orgânico, etc.
                    </span>
                </div>
                <div id="question-81" onclick="question('q15');">
                    <span id="q05" style="font-size:10pt;" onmouseover="selecionar('q05');" onmouseout="deselecionar('q05');">O que são resíduos Classe IIB (não perigosos e inertes)?</span>
                    <br /><br />
                    <span id="q15" style="position:relative;left:22px;display: none;">
                        São resíduos que quando amostrados de forma representativa, conforme NBR 10007, e <br />
                        submetidos a um contato estático e dinâmico com água deionizada, não apresentam <br />
                        nenhum constituinte solubilizado em concentrações superiores aos da potabilidade da<br />
                        água. Exemplo: tijolo, rocha, vidro, etc.
                    </span>
                </div>
                <div id="question-38" onclick="question('q16');">
                    <span id="q06" style="font-size:10pt;" onmouseover="selecionar('q06');" onmouseout="deselecionar('q06');">De quem é a responsabilidade pela gestão dos resíduos gerados?</span>
                    <br /><br />
                    <span id="q16" style="display: none;">
                        De acordo com a Política Nacional dos Resíduos Sólidos, o gerador é responsável pela<br />
                        gestão dos resíduos desde a geração até o destino final dos mesmos.<br />
                        Importante salientar que a empresa contratada para gerenciar os resíduos deve ser uma<br />
                        empresa responsável e idônea para fazer o gerenciamento dos resíduos de acordo com<br />
                        as legislações vigentes e destinar os resíduos, de acordo com a tipologia, em destinos<br />
                        corretos.
                    </span>
                </div>
                <div id="question-37" onclick="question('q17');">
                    <span id="q07" style="font-size:10pt;" onmouseover="selecionar('q07');" onmouseout="deselecionar('q07');">Qual empresa pode transportar resíduos?</span>
                    <br /><br />
                    <span id="q17"  style="display: none;">
                        Aquela empresa que está devidamente licenciada para transporte de<br />
                        resíduos. Vale salientar que para coleta de resíduos Classe I (perigosos), o motorista<br />
                        deve ter o Certificado do MOPP – Movimentação Operacional de Produtos Perigosos.
                    </ /span>
                </div>
                <div id="question-36" onclick="question('q18');">
                    <span id="q08" style="font-size:10pt;" onmouseover="selecionar('q08');" onmouseout="deselecionar('q08');">Quais as documentações a empresa deve ter e deixar com o cliente quando o  serviço for contratado?</span>
                    <br /><br />
                    <span id="q18" style="display: none;">
                        Os documentos necessários para coleta dos resíduos Classe I – Perigosos são os<br />
                        seguintes:<br />
                        - Licença de Operação de Transporte;<br />
                        - Licença de Operação de Armazenamento temporário;<br />
                        - Licença do Aterro Industrial;<br />
                        - MOPP –  Movimentação Operacional de Produtos Perigosos;<br />
                        - MTR – Manifesto de Transporte de Cargas;<br />
                        - DDF – Declaração de Destinação Final;<br />
                        - CDF – Certificado de Destinação Final.<br />
                        Os documentos necessários para coleta dos resíduos Classe IIA (não inertes) são os<br />
                        seguintes:<br />
                        - Licença de Operação de Transporte;<br />
                        - Licença de Operação de Armazenamento Temporário;<br />
                        - Licença do Aterro Industrial, Sanitários ou Empresas Recicladoras;<br />
                        - MTR – Manifesto de Transporte de Cargas;<br />
                        - DDF – Declaração de Destinação Final;<br />
                        - CDF – Certificado de Destinação Final.<br />
                        Os documentos necessários para coleta dos resíduos Classe IIB (inertes) são os<br />
                        seguintes:<br />
                        - Licença de Operação de Transporte;<br />
                        - Licença de Operação de Armazenamento temporário;<br />
                        - Licença do Aterro Industrial, sanitários ou empresas recicladoras;
                    </span>
                </div>
                <div id="question-35" onclick="question('q19');">
                    <span id="q09" style="font-size:10pt;" onmouseover="selecionar('q09');" onmouseout="deselecionar('q09');">Quais as formas de tratamento ou destinação final dos resíduos coletados?</span>
                    <br />
                    <span id="q19"  style="display: none;">
                        Existem várias formas de destinação final para os resíduos, dependendo exclusivamente<br />
                        da característica e classificação de cada resíduo:<br />
                        - aterro sanitário;<br />
                        - aterro de inertes;<br />
                        - aterro industrial;<br />
                        - coprocessamento;<br />
                        - reciclagem;<br />
                        - tratamento físico-químico<br />
                        - leitos de secagem de lodo.
                        Qual área de atuação e tipo de resíduos coletados pela empresa Brooks Ambiental?<br />
                        A Brooks Ambiental atua em todo estado de Santa Catarina, coletando todos os tipos de<br />
                        resíduos sólidos e líquidos gerados por grande e pequenos geradores.
                    </span>
                </div>
            </div>
        </div>
    </form>
    <div class="rodapebrooks">
        <asp:Image ID="Image2" runat="server" ImageAlign="Middle" ImageUrl="~/Images/rodapebrooks.jpg" />
    </div>
</body>
</html>
