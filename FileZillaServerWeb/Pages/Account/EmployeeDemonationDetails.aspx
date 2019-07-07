<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDemonationDetails.aspx.cs" Inherits="FileZillaServerWeb.EmployeeDemonationDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Content/themes/base/ylyj/employeeDemonationDetails.css?v=18519" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:90%;">
        <div class="buttons">
            <input type="button" onclick="checkAll()" value="全选" />
            <input type="button" onclick="ReverseAll()" value="反选" id="Button1" />
            <input type="button" onclick="deleteAll()" value="取消" />
            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
        </div>
        <asp:CheckBoxList ID="cblEmployeeDemonation" runat="server" RepeatColumns="4" CssClass="tbl" OnDataBound="cblEmployeeDemonation_DataBound"></asp:CheckBoxList>
    </div>
    </form>
    <script src="/Scripts/ylyj/employeeDemonationDetails.js"></script>
</body>
</html>
