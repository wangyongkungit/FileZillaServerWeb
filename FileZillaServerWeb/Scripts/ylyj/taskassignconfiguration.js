function SetQualityScore(parameters, employeeNo, type) {
    var dialog = jDialog.iframe("SpecialtyQualityConfig.aspx?employeeID=" + parameters + "&type=" + type, {
        title: '员工 ' + employeeNo + ' 专业质量分设置',
        width: 1100,
        height: 610
    });
};