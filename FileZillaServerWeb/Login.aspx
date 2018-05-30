<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FileZillaServerWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登录</title>
    <link href="Content/themes/base/LoginCss/animate-custom.css?v=18426" rel="stylesheet" />
    <link href="Content/themes/base/LoginCss/demo.css?v=18426" rel="stylesheet" />
    <link href="Content/themes/base/LoginCss/style.css?v=18426" rel="stylesheet" />
    <style type="text/css">
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display:none">
        </div>
        <div class="container">
            <div class="codrops-top">
            </div>
            <section>
                <div id="container_demo">
                    <a class="hiddenanchor" id="toregister"></a>
                    <a class="hiddenanchor" id="tologin"></a>
                    <div id="wrapper">
                        <div id="login" class="animate form">
                            <h2>Office Automation</h2>
                            <p>
                                <label for="username" class="uname" data-icon="u">你的用户名</label>
                                <asp:TextBox ID="txtUserName" required="required" runat="server" placeholder="请在这里键入你的用户名"></asp:TextBox>
                            </p>
                            <p>
                                <label for="password" class="youpasswd" data-icon="p">你的密码</label>
                                <asp:TextBox ID="txtPassword" required="required" runat="server" placeholder="请在这里键入你的密码" TextMode="Password"></asp:TextBox>
                            </p>
                            <p class="login button">
                                <asp:Button ID="btnLogin" runat="server" CssClass="but" Text="登录" OnClick="btnLogin_Click" />
                            </p>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
