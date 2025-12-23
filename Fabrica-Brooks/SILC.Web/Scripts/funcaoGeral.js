function DesabilitaOperacoes(pNomeCampoOk, pNomeCampoDataInicial, pNomeCampoDataFinal, pDescricaoTitulo)
{
    document.getElementById(pNomeCampoOk).click();
    document.getElementById('Operacoes').style.visibility = "hidden";
    var sm = '';
    sm = sm + pDescricaoTitulo + '<br /><br />';
    if (pNomeCampoDataInicial != '') {
        if (document.getElementById(pNomeCampoDataInicial).value.substr(0, 2) == '20') {
            if (pNomeCampoDataInicial != '') {
                sm = sm + 'Período de: ';
                sm = sm + document.getElementById(pNomeCampoDataInicial).value.substr(8, 2) + '/';
                sm = sm + document.getElementById(pNomeCampoDataInicial).value.substr(5, 2) + '/';
                sm = sm + document.getElementById(pNomeCampoDataInicial).value.substr(0, 4);
            }
            if (pNomeCampoDataFinal != '') {
                sm = sm + ' a ';
                sm = sm + document.getElementById(pNomeCampoDataFinal).value.substr(8, 2) + '/';
                sm = sm + document.getElementById(pNomeCampoDataFinal).value.substr(5, 2) + '/';
                sm = sm + document.getElementById(pNomeCampoDataFinal).value.substr(0, 4);
            }
        }
        else {
            if (pNomeCampoDataInicial != '') {
                sm = sm + 'Período de: ';
                sm = sm + document.getElementById(pNomeCampoDataInicial).value;
            }
            if (pNomeCampoDataFinal != '') {
                sm = sm + ' a ';
                sm = sm + document.getElementById(pNomeCampoDataFinal).value;
            }
        }
        sm = sm + '<br /><br />';
    }
    if (document.getElementById('CLIENTESCONTROL1_txtCodigo') != null)
    {
        if (document.getElementById('CLIENTESCONTROL1_txtCodigo').value == '' ||
            document.getElementById('CLIENTESCONTROL1_txtCodigo').value == '0')
            sm = sm + 'Gerando todos clientes <br /><br />';
    }
    document.getElementById('Panel1').innerHTML = sm;
    document.getElementById('Panel1').style.textAlign = 'center';
    document.getElementById('Panel1').style.border = '0px';
    document.getElementById('Operacoes').style.textAlign = 'center';
    document.getElementById('Operacoes').style.width = document.getElementById('Panel1').style.width;
    document.getElementById('imgGirando').style.visibility = 'visible';
    document.getElementById('imgGirando').style.height = '100%';
}
function CompletaHHMMss(pId) {
    var sHHMMss = document.getElementById(pId).value.replace(/:/g, '');
    var sHH = 0;
    var sMM = 0;
    var sss = 0;
    if (sHHMMss.length >= 1) {
        sHH = sHHMMss.substring(0, 2);
        if (sHH > 24) {
            alert('hora inválida!');
            document.getElementById(pId).value = '';
            return;
        }
    }
    if (sHHMMss.length >= 3) {
        sMM = sHHMMss.substring(2, 4);
        if (sMM > 59) {
            alert('minuto inválido!');
            document.getElementById(pId).value = '';
            return;
        }
    }
    if (sHHMMss.length >= 5) {
        sss = sHHMMss.substring(4, 6);
        if (sss > 59) {
            alert('segundos inválido!');
            document.getElementById(pId).value = '';
            return;
        }
    }
    document.getElementById(pId).value = ('00' + sHH.toString()).slice(-2) + ':' + ('00' + sMM.toString()).slice(-2) + ':' + ('00' + sss.toString()).slice(-2);
    //alert(sHH + ':' + sMM + ':' + sss);
}
function isNumberKeyHora(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode == 58)
        return true;
    if (charCode > 31 && (charCode < 48 || charCode > 57) || charCode == 13)
        return false;
    return true;
}
