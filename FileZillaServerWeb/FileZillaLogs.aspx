<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileZillaLogs.aspx.cs" Inherits="FileZillaServerWeb.FileZillaLogs" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="keywords" content="苏州,易量易价,金色经典,企业咨询,造价,邻宝" />
<meta name="description" content="苏州易量易价企业咨询管理有限公司" />
<meta name="author" content="Wang Yongkun" />
    <title>文件服务器操作日志</title>
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="Scripts/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        body {
             background-color:#FAF9DE;
        }
        .textbox
        {
            color:#333;
            line-height:16px;
            font-family:"Microsoft YaHei",Tahoma,Verdana,SimSun;
            font-style:normal;
            font-variant:normal;
            font-size-adjust:none;
            font-stretch:normal;
            font-weight:normal;
            margin-top:0px;
            margin-bottom:0px;
            margin-left:0px;
            padding-top:3px;
            padding-right:3px;
            padding-bottom:3px;
            padding-left:3px;
            font-size:12px;
            outline-width:medium;
            outline-style:none;
            outline-color:invert;
            border-top-left-radius:3px;
            border-top-right-radius:3px;
            border-bottom-left-radius:3px;
            border-bottom-right-radius:3px;
            text-shadow:0px 1px 2px #fff;
            background-attachment:scroll;
            background-repeat:repeat-x;
            background-position-x:left;
            background-position-y:top;
            background-size:auto;
            background-origin:padding-box;
            background-clip:border-box;
            background-color:rgb(255,255,255);
            margin-right:8px;
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
        .divLbl {            
            width:85px;
            height:40px;
            float:left;
            margin-left:15px;
            font-family:"Microsoft YaHei";
        }
        .divTxt {
            width:150px;
            height:40px;
            float:left;
        }
        .btn2 {
                color: #fff;
    border: 1px solid #09c;
    background-color: #09c;
    cursor:pointer;
        }
            .btn2:hover {
                                color: #fff;
    border: 1px solid #09c;
    background-color:#077db9;
    cursor:pointer;
            }
        .button {

/* text */
	text-decoration: 		none;
	font: 					24px/1em 'Droid Sans', sans-serif;
	font-weight: 			bold;
	text-shadow: 			rgba(255,255,255,.5) 0 1px 0;
	-webkit-user-select: 	none;
	-moz-user-select: 		none;
	user-select: 			none;
	
	
/* layout */
	padding: 				.5em .6em .4em .6em;
	margin: 				.5em;
	display: 				inline-block;
	position: 				relative;
	
	-webkit-border-radius: 	8px;
	-moz-border-radius: 	8px;
	border-radius: 	8px;
	
/* effects */
	border-top: 		1px solid rgba(255,255,255,0.8);
	border-bottom: 		1px solid rgba(0,0,0,0.1);
	
	background-image: 	-webkit-gradient(radial, 50% 0, 100, 50% 0, 0, from( rgba(255,255,255,0) ), to( rgba(255,255,255,0.7) )), url(noise.png);
	background-image: 	-moz-radial-gradient(top, ellipse cover, rgba(255,255,255,0.7) 0%, rgba(255,255,255,0) 100%), url(noise.png);
	/*background-image: 	gradient(radial, 50% 0, 100, 50% 0, 0, from( rgba(255,255,255,0) ), to( rgba(255,255,255,0.7) )), url(noise.png);*/

	-webkit-transition: background .2s ease-in-out;
	-moz-transition: 	background .2s ease-in-out;
	transition: 		background .2s ease-in-out;
	
/* color */
	color: 				hsl(0, 0%, 40%) !important;
	background-color: 	hsl(0, 0%, 75%);
	
	-webkit-box-shadow: inset rgba(255,254,255,0.6) 0 0.3em .3em, inset rgba(0,0,0,0.15) 0 -0.1em .3em, /* inner shadow */ 
						hsl(0, 0%, 60%) 0 .1em 3px, hsl(0, 0%, 45%) 0 .3em 1px, /* color border */
						rgba(0,0,0,0.2) 0 .5em 5px; /* drop shadow */
	-moz-box-shadow: 	inset rgba(255,254,255,0.6) 0 0.3em .3em, inset rgba(0,0,0,0.15) 0 -0.1em .3em, /* inner shadow */ 
						hsl(0, 0%, 60%) 0 .1em 3px, hsl(0, 0%, 45%) 0 .3em 1px, /* color border */
						rgba(0,0,0,0.2) 0 .5em 5px; /* drop shadow */
	box-shadow:		 	inset rgba(255,254,255,0.6) 0 0.3em .3em, inset rgba(0,0,0,0.15) 0 -0.1em .3em, /* inner shadow */ 
						hsl(0, 0%, 60%) 0 .1em 3px, hsl(0, 0%, 45%) 0 .3em 1px, /* color border */
						rgba(0,0,0,0.2) 0 .5em 5px; /* drop shadow */
}

/* -------------- button (tag) -------------- */

button.button {
	border-left: none;
	border-right: none;
}
.button:hover {
	cursor: pointer;
}
.tbl {
    width:100%;
    border-collapse:collapse;
    background-color:#fff;
}
#tbHead {
    height:28px;
    line-height:28px;
    padding:2px 0px 2px 4px;
    background-color:#CD1637;
    border-bottom:3px dotted groove;
    font-weight:500;
    color:#fff;
}
.tbl tr {
    height:23px;
    line-height:23px;
    border-bottom:solid 1px #d0dee5;
}
.tbl tr.over {
    background: #EBEBE8; /*这个将是鼠标高亮行的背景色*/
}
.tbl td {
        font-size:13px;
        height:23px;
        overflow:hidden;
        text-overflow:ellipsis;
}


/*.tbl1
        {
            width:1100px;
            border: 1px solid #c9dae4;
        }
        .tbl1 tr th
        {
            color: #0d487b;
            background: #f2f4f8 url(../CSS/Table/images/sj_title_pp.jpg) repeat-x left bottom;
            height:22px;
            line-height: 22px;
            border-bottom: 1px solid #9cb6cf;
            border-top: 1px solid #e9edf3;
            font-family:'Microsoft YaHei';
            font-weight: normal;
            text-shadow: #e6ecf3 1px 1px 0px;
            padding-left: 5px;
            padding-right: 5px;
            overflow:hidden;
        }
        .tbl1 tr td
        {
            height:22px;
            line-height:22px;
            border-bottom: 1px solid #e9e9e9;
            padding-bottom: 2px;
            padding-top: 2px;
            color: #444;
            border-top: 1px solid #FFFFFF;
            padding-left: 5px;
            padding-right: 5px;
            font-family:'Microsoft YaHei';
            overflow:hidden;
            white-space:nowrap;
            position:relative;
        }
            .tbl1 tr td.nr {
                width:600px;
                height:22px;
            }
            .tbl1 tr td.wjm {
                width:260px;
                height:22px;
            }*/
        /* white-space:nowrap; text-overflow:ellipsis; */
        tr.alt td
        {
            background: #ecf6fc; /*这行将给所有的tr加上背景色*/
        }
        tr.alt2 td {
            background-color: rgba(255,255,255,0.8);
        }
        tr.over td
        {
            background: #bcd4ec; /*这个将是鼠标高亮行的背景色*/
        }
        .tdCenter {
            text-align:center;
        }
        .title {
            margin: 0;
            text-align:center;
            font: bold 45px/1 "Microsoft YaHei", Helvetica, Arial, sans-serif;
            letter-spacing:6px;
            color: #538bd0;
            text-shadow: 0 1px 0 #cccccc, 0 2px 0 #c9c9c9, 0 3px 0 #bbbbbb, 0 4px 0 #b9b9b9, 0 5px 0 #aaaaaa, 0 6px 1px rgba(0, 0, 0, 0.1), 0 0 5px rgba(0, 0, 0, 0.1), 0 1px 3px rgba(0, 0, 0, 0.3), 0 3px 5px rgba(0, 0, 0, 0.2), 0 5px 5px rgba(0, 0, 0, 0.25), 0 5px 5px rgba(0, 0, 0, 0.2), 0 10px 10px rgba(0, 0, 0, 0.15);
            -webkit-transition: .2s all linear;
            height:60px;
        }

        #rightsead{width:60px;height:60px;position:fixed;top:20%;right:0px;}
    </style>
    <script type="text/javascript">
        $(document).ready(function () { //这个就是传说的ready  
            $(".tbl1 tr").mouseover(function () {
                //如果鼠标移到class为stripe的表格的tr上时，执行函数  
                $(this).addClass("over");
            }).mouseout(function () {
                //给这行添加class值为over，并且当鼠标一出该行时执行函数  
                $(this).removeClass("over");
            }) //移除该行的class  
            $(".tbl1 tr:even").addClass("alt");
            //给class为stripe的表格的偶数行添加class值为alt
            $(".tbl1 tr:odd").addClass("alt2");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 1100px; margin: 0 auto;">
            <div class="title">文件服务器操作日志</div>
            <div style="width: 1100px; height: 80px; background-color: aliceblue; padding-top: 18px; padding-bottom: 18px;">
                <div style="width: 800px; margin: 0 auto;">
                    <div class="divLbl">员工姓名：</div>
                    <div class="divTxt">
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox"></asp:TextBox></div>
                    <div class="divLbl">操作类型：</div>
                    <div class="divTxt">
                        <asp:DropDownList ID="ddlOperateType" runat="server" Width="100">
                        </asp:DropDownList>
                    </div>
                    <div class="divLbl">内容：</div>
                    <div class="divTxt">
                        <asp:TextBox ID="txtContent" runat="server" CssClass="textbox"></asp:TextBox></div>
                </div>

                <div style="clear: both; width: 800px; margin: 0 auto;">
                    <div class="divLbl" style="">文件名：</div>
                    <div class="divTxt">
                        <asp:TextBox ID="txtFileName" runat="server" CssClass="textbox"></asp:TextBox></div>
                    <div class="divLbl">时间范围：</div>
                    <div class="divTxt">
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="Wdate" Width="140px" onFocus="WdatePicker({lang:'zh-cn',dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox></div>
                    <div class="divTxt" style="width: 165px;">-<asp:TextBox ID="txtEndDate" runat="server" CssClass="Wdate" Width="140px" onFocus="WdatePicker({lang:'zh-cn',dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox></div>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" CssClass="btn2" />
                    <asp:Button ID="btnSync" runat="server" Text="立即同步" OnClick="btnSync_Click" CssClass="btn2" />
                </div>
            </div>
            <div style="width: 1100px; height: 100px;">
                <div style="width: 1100px; margin: 0 auto;">
                    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" Width="1100" Font-Size="13px" CssClass="tbl">
                        <Columns>
                            <asp:BoundField DataField="OPERATEDATE" HeaderText="操作时间" HeaderStyle-Width="14%" ItemStyle-Width="14%" ItemStyle-CssClass="tdCenter" />
                            <asp:BoundField DataField="OPERATEUSERID" HeaderText="操作人" ItemStyle-Width="5%" ItemStyle-CssClass="tdCenter" />
                            <asp:BoundField DataField="IPADDRESS" HeaderText="IP" />
                            <asp:BoundField DataField="OPERATETYPE" HeaderText="操作类型" ItemStyle-Width="5%" ItemStyle-CssClass="tdCenter" />
                            <asp:BoundField DataField="CONTENT" HeaderText="内容" />
                            <asp:BoundField DataField="FILENAME" HeaderText="文件（夹）名" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div style="width: 960px; text-align: center;">暂无记录</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div style="text-align: right; float: right; padding-right: 100px;">
                    <webdiyer:AspNetPager ID="AspNetPager" runat="server" OnPageChanged="AspNetPager_PageChanged"
                        FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Never"
                        AlwaysShow="true" UrlPaging="False" ReverseUrlPageIndex="True">
                    </webdiyer:AspNetPager>
                </div>
            </div>
            <div style="position:absolute;right:250px;top:350px">
                <a href="TaskMonitoring.aspx">查看任务情况</a>
            </div>
        </div>

        <div id="rightsead">
            <%--<ul>
                <li>
                    <a href="tencent://message/?uin=763687776&Site=bimpan.iok.la:8&Menu=yes" target="_blank" title="在线客服">
                        <img src="images/ll04.png" class="hides"/>
                        <img src="Images/l04.png" class="shows" />
                    </a>
                </li>
            </ul>--%>
            <a href="LogList.aspx" title="查看任务管理系统操作日志">
                <img src="Images/log_list.jpg" alt="任务管理系统操作日志查看" />
            </a>
        </div>
    </form>
</body>
</html>