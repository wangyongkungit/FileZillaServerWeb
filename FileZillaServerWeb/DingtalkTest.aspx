<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DingtalkTest.aspx.cs" Inherits="FileZillaServerWeb.DingtalkTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="http://g.alicdn.com/dingding/open-develop/1.5.1/dingtalk.js"></script>
    <script type="text/javascript">
        function pickConversation() {
            alert(2);
            dd.biz.chat.pickConversation({
                corpId: 'ding08a708c5272bc85d35c2f4657eb6378f', //企业id
                isConfirm: 'true', //是否弹出确认窗口，默认为true
                onSuccess: function () {
                    //onSuccess将在选择结束之后调用
                    // 该cid和服务端开发文档-普通会话消息接口配合使用，而且只能使用一次，之后将失效
                    /*{
                        cid: 'xxxx',
                        title:'xxx'
                    }*/
                    alert(1);
                },
                onFail: function () {
                    alert(333);
                }
            })
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="button" value="Invoke" onclick="pickConversation();" />
    </div>
    </form>
</body>
</html>
