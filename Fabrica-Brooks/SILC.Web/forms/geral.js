function LimpaErro()
{
    document.getElementById('lblMensagem').innerText = '';
}
function FechaJanela()
{
    window.close();
    window.opener.location.reload();
}
function FechaJanelaGrupo()
{
    alert('teset');
    //window.close();
    //window.opener.location.href = "Residuos.aspx?view=1";
    window.opener.location.reload();
    window.close();
}