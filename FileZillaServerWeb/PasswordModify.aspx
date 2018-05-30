<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordModify.aspx.cs" Inherits="FileZillaServerWeb.PasswordModify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>密码修改</title>
    <!--CSS-->
    <link href="Scripts/dialog/jDialog/jDialog.css" rel="stylesheet" />
    <style type="text/css">
        body {
            font-family:'Microsoft YaHei';
            background:#fff url(/Images/bg_login.jpg) repeat top left;
        }
        .container {
            width:1100px;
            margin:0 auto;
        }
        .header {
            width:100%;
            height:215px;
            margin-bottom:10px;
        }
        .divTitle {
            width:100%;
            height:50px;
        }
        .divTitle h1{
            color:#CD1637;
            text-align:center;
            letter-spacing:4px;
        }
        .formInput {
            width:inherit;
            height:28px;
            margin:5px 0px 0px 5px;
            float:left;
            font-size:13px;
            font-family:'Microsoft YaHei';
        }
        .formInputLeft {
            width:540px;
            height:28px;
            margin:2px 0px 0px 5px;
            float:left;
            text-align:right;
        }
        .formInputRight {
            width:165px;
            height:28px;
            margin:5px 0px 0px 5px;
            float:left;
        }
        .formInput input[type="text"],input[type="password"]{
            width:160px;
            color:#333;
            line-height:normal;
            font-family:"Microsoft YaHei",Tahoma,Verdana,SimSun;
            font-style:normal;
            font-size-adjust:none;margin-top:0px;
            margin-bottom:0px;
            margin-left:0px;
            padding-top:4px;
            padding-right:4px;
            padding-bottom:4px;
            padding-left:4px;
            outline-width:medium;
            outline-style:none;
            outline-color:invert;
            /*border-top-left-radius:3px;
            border-top-right-radius:3px;
            border-bottom-left-radius:3px;
            border-bottom-right-radius:3px;*/
            text-shadow:0px 1px 2px #fff;
            background-attachment:scroll;
            /*background-repeat:repeat-x;
            background-position-x:left;
            background-position-y:top;
            background-size:auto;
            background-origin:padding-box;
            background-clip:border-box;
            background-color:rgb(255,255,255);
            margin-right:8px;*/
            border-top-color:#ccc;
            border-right-color:#ccc;
            border-bottom-color:#ccc;
            border-left-color:#ccc;
            border-top-width:1px;
            border-right-width:1px;
            border-bottom-width:1px;
            border-left-width:1px;
            border-top-style:solid;
            border-right-style:solid;
            border-bottom-style:solid;
            border-left-style:solid;
        }
        .formInput input:focus {
            border:1px solid #fafafa;
            -webkit-box-shadow:0px 0px 6px #007eff;
            -moz-box-shadow:0px 0px 5px #007eff;
            box-shadow:0px 0px 5px #007eff;
        }
        .formInput select {
            border:1px solid #ccc;
            width:130px;
            height:30px;
            font-family:'Microsoft YaHei';
        }
        .button {
            width:70px;
            height:28px;
            border:1px solid #808080;
            background-color:#f0f0f0;
            font-family:'Microsoft YaHei';
            cursor:pointer;
        }

    </style>
    <!--JS-->
    <script src="Scripts/dialog/jquery.js"></script>
    <script src="Scripts/dialog/jDialog.js"></script>
    <script type="text/javascript">
        function AlertDialog(msg, result) {
            var dialog = jDialog.alert(msg, {
                handler: function (button, dialog) {
                    dialog.close();
                    if (result == 'True') {
                        window.location.href = 'Login.aspx';
                    }
                }
            }, {
                showShadow: false,// 不显示对话框阴影
                //buttonAlign: 'center',
                events: {
                    show: function (evt) {
                        var dlg = evt.data.dialog;
                    },
                    close: function (evt) {
                        var dlg = evt.data.dialog;
                    },
                    enterKey: function (evt) {
                        alert('enter key pressed!');
                    },
                    escKey: function (evt) {
                        alert('esc key pressed!');
                        evt.data.dialog.close();
                    }
                }
            });
        }
        $(function () {
            $("#test1").click(function () {
                var dialog = jDialog.alert('欢迎使用jDialog组件，我是alert！', {}, {
                    showShadow: false,// 不显示对话框阴影
                    buttonAlign: 'center',
                    events: {
                        show: function (evt) {
                            var dlg = evt.data.dialog;
                        },
                        close: function (evt) {
                            var dlg = evt.data.dialog;
                        },
                        enterKey: function (evt) {
                            alert('enter key pressed!');
                        },
                        escKey: function (evt) {
                            alert('esc key pressed!');
                            evt.data.dialog.close();
                        }
                    }
                });
            });
            $("#test2").click(function () {
                var dialog = jDialog.confirm('欢迎使用jDialog组件，我是confirm！', {
                    handler: function (button, dialog) {
                        alert('你点击了确定！');
                        dialog.close();
                    }
                }, {
                    handler: function (button, dialog) {
                        alert('你点击了取消！');
                        dialog.close();
                    }
                });
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
                <div class="header">
                    <div class="divTitle">
                        <h1>密码修改</h1>
                    </div>
                </div>
                <div class="formInput">
                    <div class="formInputLeft">
                        <label>请输入原密码：</label>
                    </div>
                    <asp:TextBox ID="txtFormerPwd" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rfvFormerPwd" runat="server" ControlToValidate="txtFormerPwd" ErrorMessage="请输入原密码" ValidationGroup="changePwd" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblFormerTip" runat="server" />
                </div>
                <div class="formInput">
                    <div class="formInputLeft">
                        <label>请输入新密码：</label>
                    </div>
                    <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rfvNewPwd" runat="server" ControlToValidate="txtNewPwd" ErrorMessage="请输入新密码" ForeColor="Red" ValidationGroup="changePwd" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="formInput">
                    <div class="formInputLeft">
                        <label>请再次输入新密码：</label>
                    </div>
                    <asp:TextBox ID="txtNewPwdAgain" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rfvPwdAgain" runat="server" ControlToValidate="txtNewPwdAgain" ErrorMessage="请再次输入新密码" ForeColor="Red" ValidationGroup="changePwd"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvFormerPwd" runat="server" ControlToValidate="txtNewPwdAgain" ControlToCompare="txtNewPwd" ErrorMessage="两次密码输入不一致" ForeColor="Red" ValidationGroup="changePwd"></asp:CompareValidator>
                </div>
                <div class="formInput">
                    <div class="formInputLeft">
                    </div>
                    <asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" CssClass="button" ValidationGroup="changePwd" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
