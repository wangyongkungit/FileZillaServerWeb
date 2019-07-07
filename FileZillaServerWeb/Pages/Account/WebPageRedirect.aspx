<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebPageRedirect.aspx.cs" Inherits="FileZillaServerWeb.WebPageRedirect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>我的菜单</title>
    <style type="text/css">
        *{
            margin: 0;
            padding: 0;
            /*background-color: #EAEAEA;*/
        }
        body {
            background:#fff url(/Images/bg-grey-stripe.png) repeat top left;
        }
        div{
            width: 1100px;
            height: 100px;
            /*background-color: #1E90FF;*/
        }
        .center-in-center{
            position: absolute;
            top: 20%;
            left: 50%;
            -webkit-transform: translate(-50%, -50%);
            -moz-transform: translate(-50%, -50%);
            -ms-transform: translate(-50%, -50%);
            -o-transform: translate(-50%, -50%);
            transform: translate(-50%, -50%);
        }
        .center-in-center h1 {
            font-size:40px;
            font-family:黑体,'Microsoft YaHei',sans-serif;
            text-align:center;
            letter-spacing:5px;
            color:#CD1637;
        }
        .center-in-center h1 span {
            color:#ccc;
            font-family:'Microsoft YaHei',sans-serif;
            font-size:24px;
            letter-spacing:normal;
        }
        /*水波样式*/
        * {margin: 0; padding: 0;}
        /*nav styles*/
        .nav ul {
	        background: white; border-top: 6px solid hsl(0, 86%, 48%);
	        width: 930px; margin: 5em auto;
        }
        .nav ul li {
            width:300px;
            height:130px;
	        list-style-type: none;
	        /*relative positioning for list items along with overflow hidden to contain the overflowing ripple
	        position: relative;*/
	        overflow: hidden;
            font-family:'Microsoft YaHei';
            font-size:16px;
            text-align:center;
        }
        .nav ul li a {
	        /*font: normal 14px/28px Montserrat;*/
            font:normal 500 24px 'Microsoft Yahei';
	        color: white;
	        /*display: block;*/
	        padding: 10px 15px;
	        text-decoration: none;
	        cursor: pointer; /*since the links are dummy without href values*/
	        /*prevent text selection*/
	        user-select: none;
	        /*static positioned elements appear behind absolutely positioned siblings(.ink in this case) hence we will make the links relatively positioned to bring them above .ink*/
	        position: relative;
        }

        /*.ink styles - the elements which will create the ripple effect. The size and position of these elements will be set by the JS code. Initially these elements will be scaled down to 0% and later animated to large fading circles on user click.*/
        .nav .ink {
	        display: block; 
	        position: absolute;
	        background: hsl(180, 40%, 80%);
	        border-radius: 100%;
	        transform: scale(0);
        }
        /*animation effect*/
        .nav .ink.animate {animation: ripple 0.65s linear;}
        @keyframes ripple {
	        /*scale the element to 250% to safely cover the entire link and fade it out*/
	        100% {opacity: 0; transform: scale(2.5);}
        }
        .diva {
            width:300px; height:120px; float:left; background-color:#0072AA; position:relative;
        }
        .diva:hover {
            background-color:#0072CC;
            transform:scale(1.1);
        }
        .ovrly {
            position: absolute;
            background: rgba(0, 0, 0, 0.5);
            height: 100%;
            left: -100%;
            top: 0;
            width: 300px;
            height:200px;
            -webkit-transition: all 0.5s;
            -moz-transition: all 0.5s;
            -o-transition: all 0.5s;
            transition: all 0.5s;
            z-index:999;
        }
         .buttons {
            position: absolute;
            width:300px;
            height:200px;
            top: 50%;
            left: -100%;
            -webkit-transition: all 0.5s;
            -moz-transition: all 0.5s;
            -o-transition: all 0.5s;
            transition: all 0.5s;
            -webkit-transform: translate(-50%, -50%);
            -moz-transform: translate(-50%, -50%);
            -ms-transform: translate(-50%, -50%);
            -o-transform: translate(-50%, -50%);
            transform: translate(-50%, -50%);
        }
        .pwdModify {
            position:fixed;
            right:100px;
            top:120px;
            bottom:0px;
            padding-top:5px;
            width:20px;
            height:20px;
            z-index:999;
            font-size:12px;
            text-align:center;
        }
        .pwdModify a {
            width:20px;
            height:20px;
        }
    </style>
    <script src="/Scripts/jquery.min.js"></script>
    <script src="/Scripts/WaterWave/prefixfree.min.js"></script>
    <script type="text/javascript">
        $(".nav ul li a").click(function (e) {
            parent = $(this).parent();
            //create .ink element if it doesn't exist
            if (parent.find(".ink").length == 0)
                parent.prepend("<span class='ink'></span>");

            ink = parent.find(".ink");
            //incase of quick double clicks stop the previous animation
            ink.removeClass("animate");

            //set size of .ink
            if (!ink.height() && !ink.width()) {
                //use parent's width or height whichever is larger for the diameter to make a circle which can cover the entire element.
                d = Math.max(parent.outerWidth(), parent.outerHeight());
                ink.css({ height: d, width: d });
            }

            //get click coordinates
            //logic = click coordinates relative to page - parent's position relative to page - half of self height/width to make it controllable from the center;
            x = e.pageX - parent.offset().left - ink.width() / 2;
            y = e.pageY - parent.offset().top - ink.height() / 2;

            //set the position and add class .animate
            ink.css({ top: y + 'px', left: x + 'px' }).addClass("animate");
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="pwdModify">
            <a href="/pages/account/PasswordModify.aspx" title="密码修改"><img src="/Images/key40.png" style="width:40px; height:40px;" /></a>
        </div>
        <div class="center-in-center">
            <h1>我的菜单<span>/MY MENU</span></h1>
         <div class="nav">
	        <ul>
                <asp:Repeater ID="rptMenu" runat="server">
                    <ItemTemplate>
                        <li style="width:300px; height:120px; line-height:120px; margin:5px 5px; float:left;">
                            <div class="diva">
                            <a href='<%# Eval("Path") %>' target="_blank" style="width:280px;height:100px;line-height:100px; display:block;" title='<%# Eval("Remarks") %>'><%# Eval("Name") %>
                                <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/hot.gif" Visible='<%# Eval("Path").ToString() == "LogList.aspx" %>' />--%>
                            </a>
                                <div class="ovrly">
                                    ovrly
                                </div>
                                <div class="buttons">
                                    buttons
                                </div>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
	        </ul>
        </div>
        </div>
    </form>
</body>
</html>
