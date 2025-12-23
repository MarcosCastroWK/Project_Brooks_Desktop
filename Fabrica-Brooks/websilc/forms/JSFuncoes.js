function disableEnabledSelection(target, isEnabled) {
    if (typeof target.onselectstart != "undefined") //IE route
    {
        if (isEnabled == false) {
            target.onselectstart = function () { return false }
            target.style.cursor = "default";
        }
        else {
            target.onselectstart = null;
            target.style.cursor = null;
        }
    }
    else if (typeof target.style.MozUserSelect != "undefined") //Firefox route
    {
        if (isEnabled == false) {
            target.style.MozUserSelect = "none"
            target.style.cursor = "default";
        }
        else {
            target.style.MozUserSelect = null;
            target.style.cursor = null;
        }
    }
    else { //All other route (ie: Opera)
        if (isEnabled == false) {
            target.onmousedown = function () { return false }
            target.style.cursor = "default";
        }
        else {
            target.onmousedown = null;
            target.style.cursor = null;
        }
    }
}
function getWidthScreen() {
    var myWidth = 0;

    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myWidth = document.documentElement.clientWidth;
    }
    else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myWidth = document.documentElement.clientWidth;
    }
    else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myWidth = document.body.clientWidth;
    }
    return myWidth;
}

function getHeightScreen() {
    var myHeight = 0;

    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myHeight = window.innerHeight;
    }
    else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myHeight = document.documentElement.clientHeight;
    }
    else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myHeight = document.body.clientHeight;
    }

    return myHeight;
}
function setSize() {
    var objMainTR = document.getElementById('mainTR');
    var objPanelLeftTD = document.getElementById('panelLeftTD');
    var objPanelRightTD = document.getElementById('panelRightTD');
    var objPanelTopRightTR = document.getElementById('panelTopRightTR');
    var objPanelBottomRightTR = document.getElementById('panelBottomRightTR');

    var objContentLefDiv = document.getElementById('contentLefDiv');
    var objContentTopRightDiv = document.getElementById('contentTopRightDiv');
    var objContentBottomRightDiv = document.getElementById('contentBottomRightDiv');


    var myWidth = getWidthScreen();
    var myHeight = getHeightScreen();

    if (myHeight > 0) {
        objMainTR.style.height = myHeight + 'px';

        objPanelRightTD.style.width = (myWidth - parseInt(objPanelLeftTD.style.width) - 2) + 'px';

        objPanelBottomRightTR.style.height = (myHeight - parseInt(objPanelTopRightTR.style.height) - 2) + 'px';

        objContentLefDiv.style.height = (myHeight) + 'px';
        objContentTopRightDiv.style.height = parseInt(objPanelTopRightTR.style.height) + 'px';
        objContentBottomRightDiv.style.height = parseInt(objPanelBottomRightTR.style.height) + 'px';
    }
}
var isResizePanelV;

function setResizePanelVTrue() {
    isResizePanelV = true;
}
var isResizePanelH;

function setResizePanelHTrue() {
    isResizePanelH = true;
}
function setResizePanelFalse() {
    isResizePanelV = false;
    isResizePanelH = false;

    disableEnabledSelection(document.body, true);
}
function resizePanel(evt) {
    if (isResizePanelV) {
        resizePanelV(evt);
    }
    else if (isResizePanelH) {
        resizePanelH(evt);
    }
}
function resizePanelV(evt) {
    if (isResizePanelV) {
        var objPanelTopRightTR = document.getElementById('panelTopRightTR');

        var e = null;
        var isMz = false; //Indica que o browser é Mozila
        var valueWidth = 0;

        if (window.event) {
            e = window.event; //Dados do evento.
        }
        else {
            e = evt; //Dados do evento.
            isMz = true;
        }

        disableEnabledSelection(document.body, false); //Desabilita a seleção de texto;

        if (isMz == false && (e.pageX || e.pageY)) {
            valueHeight = e.pageY;
        }
        else if (isMz == false && (e.clientX || e.clientY)) {
            valueHeight = (e.clientY + document.body.scrollTop + document.documentElement.scrollTop);
        }
        else if (isMz) {
            valueHeight = e.pageY;
        }

        var myHeight = getHeightScreen();

        if ((myHeight - 200) > valueHeight) {
            if (valueHeight > 200) {
                objPanelTopRightTR.style.height = valueHeight + 'px';
            }
            else {
                objPanelTopRightTR.style.height = '200px';
            }

            setSize();
        }
    }
}

function resizePanelH(evt) {
    if (isResizePanelH) {
        var objPanelLeftTD = document.getElementById('panelLeftTD');
        var e = null;
        var isMz = false;//Indica que o browser é Mozila.
        var valueWidth = 0;

        if (window.event) {
            e = window.event;//Dados do evento.
        }
        else {
            e = evt;//Dados do evento.
            isMz = true;
        }
        
        disableEnabledSelection(document.body, false);//Desabilita a seleção de texto;

        if (isMz == false && (e.pageX || e.pageY)) {
            valueWidth = e.pageX;
        }
        else if (isMz == false && (e.clientX || e.clientY)) {
            valueWidth = (e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft);
        }
        else if (isMz) {
            valueWidth = e.pageX;
        }
        var myWidth = getWidthScreen();

        if ((myWidth - 300) > valueWidth) {
            if (valueWidth > 200) {
                objPanelLeftTD.style.width = valueWidth + 'px';
            }
            else {
                objPanelLeftTD.style.width = '200px';
            }
            setSize();
        }
    }
}
