<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskTransfer.aspx.cs" Inherits="FileZillaServerWeb.TaskTransfer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">select {
            border:1px solid #ccc;
            width:130px;
            height:30px;
            font-size:14px;
            font-family:'Microsoft YaHei';
        }
        input[type='text'] {
            width:60px;
            height:22px;
            padding:3px 8px;
            border:none;
            border-bottom:1px solid #CD1637;
            background-color:#fff;
            font-family:Consolas,'Microsoft YaHei',sans-serif;
            font-size:18px;
            outline:none;
        }
        input[type='submit'],input[type='button']
        {
            color:#000;
            border:2px solid #cf5555;
            background-color:#fff;
            font-size:13px;
            font-family:'Microsoft YaHei','sans-serif';
            padding:6px 18px;
            text-align:center;
            display:inline-block;
            border-radius:2px;
            text-decoration:none;
            cursor:pointer;
            transition-duration:0.3s;
        }
        input[type='submit']:hover,input[type='button']:hover
        {
            color:#fff;
            background-color:#cf5555;
        }
    </style>
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script type="text/javascript">
        var checkInput = function () {
            if ($("#txtProportion").val() > 100) {
                alert("比例不能超过100%");
                return false;
            }
            else if ($("#txtAmount").val() > $("#lblAmount").text()) {
                alert("金额不能超过原始金额！");
                return false;
            }
            if (!$("#txtProportion").val() || !$("#txtAmount").val()) {
                alert("请先点击转换按钮计算后再保存！");
                return false;
            }
            return true;
        }
        $().ready(function () {
            $("#btnCalculate").bind("click", function () {
                var originalAmount = $("#lblAmount").text();
                var proportion = $("#txtProportion").val();
                var amount = $("#txtAmount").val();
                if (proportion) {
                    var result = originalAmount * proportion * 0.01;
                    $("#txtAmount").val(result);
                }
                else if (amount) {
                    var result = (amount / originalAmount) * 100;
                    $("#txtProportion").val(result);
                }
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:90%; text-align:center;">
        <asp:HiddenField ID="hidAmount" runat="server" />
        <div style="width:100%; height:38px; margin:0 auto;">
            <label style="margin-right:10px;">您的金额</label>
            <asp:Label ID="lblAmount" runat="server"></asp:Label>
            <label style="margin-right:10px;">，转移给</label>
            <asp:DropDownList ID="ddlCanTransferEmp" runat="server"></asp:DropDownList>
        </div>
        <div class="buttons" style="width:100%; height:38px;">
            <label>请设置比例</label>
            <asp:TextBox ID="txtProportion" runat="server"></asp:TextBox>
            <label>&#37;&nbsp;或金额</label>
            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
            <label>￥</label>
            <input id="btnCalculate" type="button" value="转换" title="点击将根据您输入的比例或金额计算出相应的金额或比例" />
        </div>
        <div style="height:38px;">            
            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" OnClientClick="return checkInput();" />
        </div>
    </div>
    </form>
</body>
</html>
