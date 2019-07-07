function SetEmployeeDemonation(parameters, employeeNo) {
    var dialog = jDialog.iframe("/Pages/Account/EmployeeDemonationDetails.aspx?employeeID=" + parameters, {
        title: '员工 ' + employeeNo + ' 权限设置',
        width: 1100,
        height: 620,
        top: 200,
        left: 370
    });
};