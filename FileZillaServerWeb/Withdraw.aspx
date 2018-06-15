<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Withdraw.aspx.cs" Inherits="FileZillaServerWeb.Withdraw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>提现</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>提现</h1>
            <div class="row">
                <div class="left">
                    <label>当前可提现金额：</label>
                </div>
                <div class="right">
                    <asp:Label ID="lblCanWithdrawAmount" runat="server"></asp:Label>
                    <label>元</label>
                </div>
            </div>
            <div class="row">
                <div class="left">
                    <label>请输入提现金额：</label>
                </div>
                <div class="right">
                    <asp:TextBox ID="txtWithdrawAmount" runat="server"></asp:TextBox>
                    <label>元</label>
                </div>
            </div>
            <div class="row">
                <div class="left">&nbsp;</div>
                <div class="right">
                    <input id="withdraw" type="button" value="提现" />
                </div>
            </div>
        </div>
        <div style="display:none;">
            <div id="dialog-confirm" class="dialogDiv" title="提现确认">
                <p><span class="ui-icon ui-icon-alert" style="float: left;"></span>确定要提现吗？</p>
            </div>
            <div id="cantWithdraw" class="dialogDiv">
                <p><span class="ui-icon ui-icon-alert" style="float: left;"></span>余额不足！</p>
            </div>
            <div id="withdrawAmountValid" class="dialogDiv">
                <p><span class="ui-icon ui-icon-alert" style="float: left;"></span>请输入大于〇的提现金额！</p>
            </div>
        </div>
        <div style="display:none;">
            <asp:HiddenField ID="hidEmployeeID" runat="server" />
            <asp:Button ID="btnWithdrwa" runat="server" OnClick="btnWithdrwa_Click" />
        </div>
    </form>
    <link href="Content/themes/base/ylyj/withdraw.css" rel="stylesheet" />
    <link rel="stylesheet" href="//apps.bdimg.com/libs/jqueryui/1.10.4/css/jquery-ui.min.css" />
    <%--<script src="Scripts/jquery-1.7.1.min.js"></script>--%>
    <script src="Scripts/jquery-ui-1.8.20.min.js"></script>
    <script src="Scripts/ylyj/withdraw.js"></script>
</body>
</html>
