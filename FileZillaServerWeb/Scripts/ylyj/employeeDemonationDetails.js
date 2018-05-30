function checkAll() {
    for (var i = 0; i < document.getElementById("cblEmployeeDemonation").getElementsByTagName("input").length; i++) {
        if (!document.getElementById("cblEmployeeDemonation_" + i).disabled) {
            document.getElementById("cblEmployeeDemonation_" + i).checked = true;
        }
    }
}

function deleteAll() {
    for (var i = 0; i < document.getElementById("cblEmployeeDemonation").getElementsByTagName("input").length; i++) {
        document.getElementById("cblEmployeeDemonation_" + i).checked = false;
    }
}

function ReverseAll() {
    for (var i = 0; i < document.getElementById("cblEmployeeDemonation").getElementsByTagName("input").length; i++) {
        var objCheck = document.getElementById("cblEmployeeDemonation_" + i);
        if (objCheck.checked)
            objCheck.checked = false;
        else if (!objCheck.disabled) {
            objCheck.checked = true;
        }
    }
}
