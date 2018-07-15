<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskList.aspx.cs" Inherits="FileZillaServerWeb.TaskList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/ylyj/tasklist.js?v=18426"></script>
    <link href="Content/themes/base/ylyj/tasklist.css?v=18070101" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div style="display: none;">
        <input type="hidden" id="sortTitle" name="sortTitle" />
        <input type="hidden" runat="server" id="sortOrder" name="sortOrder" value=" ASC" />
    </div>
    <div id="container" class="container">
        <div id="header" class="header">
            <div class="divTitle">
                <h1>任务查看</h1>
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>全文检索：</label>
                </div>
                <asp:TextBox ID="txtFullTextSearch" runat="server" placeholder="键入任意值均可查询" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>任务编号：</label>
                </div>
                <asp:TextBox ID="txtTaskNo" runat="server" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>工程名称：</label>
                </div>
                <asp:TextBox ID="txtProjectName" runat="server" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>店铺：</label>
                </div>
                <asp:DropDownList ID="ddlShop" runat="server" />
            </div>
            <div class="formSearchDate">
                <div class="formSearchLeft">
                    <label>截止时间：</label>
                </div>
                <asp:TextBox ID="txtExpireDateStart" runat="server" ClientIDMode="Static" CssClass="Wdate" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:00',maxDate:'#F{$dp.$D(\'txtExpireDateEnd\')}'})" placeholder="开始时间" />
                <asp:TextBox ID="txtExpireDateEnd" runat="server" ClientIDMode="Static" CssClass="Wdate" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:59',minDate:'#F{$dp.$D(\'txtExpireDateStart\')}'})" placeholder="结束时间" />
                <label style="margin-left: 5px;">月份：</label>
                <asp:TextBox ID="txtExpireDateMonth" runat="server" ClientIDMode="Static" CssClass="Wdate" Width="65" onFocus="WdatePicker({dateFmt:'yyyy-MM'})" placeholder="快速选择" />
            </div>
            <div class="formSearchDate">
                <div class="formSearchLeft">
                    <label>下单时间：</label>
                </div>
                <asp:TextBox ID="txtOrderDateStart" runat="server" ClientIDMode="Static" CssClass="Wdate" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:00',maxDate:'#F{$dp.$D(\'txtOrderDateEnd\')}'})" placeholder="开始时间" />
                <asp:TextBox ID="txtOrderDateEnd" runat="server" ClientIDMode="Static" CssClass="Wdate" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:59',minDate:'#F{$dp.$D(\'txtOrderDateStart\')}'})" placeholder="结束时间" />
                <label style="margin-left: 5px;">月份：</label>
                <asp:TextBox ID="txtOrderDateMonth" runat="server" ClientIDMode="Static" CssClass="Wdate" Width="65" onFocus="WdatePicker({dateFmt:'yyyy-MM'})" placeholder="快速选择" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>建筑类型：</label>
                </div>
                <asp:DropDownList ID="ddlBuildingType" runat="server">
                </asp:DropDownList>
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>结构类型：</label>
                </div>
                <asp:DropDownList ID="ddlStructureForm" runat="server">
                </asp:DropDownList>
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>建筑面积：</label>
                </div>
                <asp:TextBox ID="txtConstructionAreaMin" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="8" Width="45" />&nbsp;至
                    <asp:TextBox ID="txtConstructionAreaMax" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="8" Width="45" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>层数：</label>
                </div>
                <asp:TextBox ID="txtFloorsMin" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="3" Width="45" />&nbsp;至
                    <asp:TextBox ID="txtFloorsMax" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="3" Width="45" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>省份：</label>
                </div>
                <asp:DropDownList ID="ddlProvince" runat="server">
                </asp:DropDownList>
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>交易状态：</label>
                </div>
                <asp:DropDownList ID="ddlTransactionStatus" runat="server"></asp:DropDownList>
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>客户旺旺号：</label>
                </div>
                <asp:TextBox ID="txtWangwangName" runat="server" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>手机：</label>
                </div>
                <asp:TextBox ID="txtMobilePhone" runat="server" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>QQ：</label>
                </div>
                <asp:TextBox ID="txtQQ" runat="server" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>建模软件：</label>
                </div>
                <asp:DropDownList ID="ddlModelingSoftware" runat="server">
                </asp:DropDownList>
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>计价软件：</label>
                </div>
                <asp:TextBox ID="txtValuateSoftware" runat="server" MaxLength="15" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>金额：</label>
                </div>
                <asp:TextBox ID="txtOrderAmountMin" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="6" Width="45" placeholder="&yen;" />&nbsp;至
                    <asp:TextBox ID="txtOrderAmountMax" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="6" Width="45" placeholder="&yen;" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>完成人：</label>
                </div>
                <asp:TextBox ID="txtFinishedPerson" runat="server" MaxLength="6" placeholder="完成人编号" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label>完成状态：</label>
                </div>
                <asp:DropDownList ID="ddlFinishedStatus" runat="server">
                    <asp:ListItem Value="" Text="-请选择-"></asp:ListItem>
                    <asp:ListItem Value="1" Text="已完成"></asp:ListItem>
                    <asp:ListItem Value="0" Text="未完成"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <%--<img src="Images/hot.gif" style="width:25.2px;height:11.2px;" title="新增功能" />--%><label>录入人：</label>
                </div>
                <asp:TextBox ID="txtEnteringPerson" runat="server" placeholder="录入人编号" />
            </div>
            <div class="formSearch">
                <div class="formSearchLeft">
                    <label></label>
                </div>
                <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" CssClass="button" ToolTip="查询" />
                <asp:Button ID="btnExport" runat="server" Text="导出" OnClick="btnExport_Click" CssClass="button" ToolTip="将当前查询内容导出到Excel" />
            </div>
        </div>
        <div id="content">
            <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_ItemDataBound">
                <HeaderTemplate>
                    <table class="tbl">
                        <tr id="tbHead">
                            <td>
                                <span class="chk">
                                    <input type="checkbox" id="chkAll" name="chkAll" onclick="CheckAll(this, 'Item');" />
                                </span>
                                <span class="xmbh">项目编号</span>
                                <span class="wcqx">完成期限</span>
                                <span class="wcr">完成人</span>
                                <span class="lrr">录入人</span>
                                <span class="sfwc">是否完成</span>
                                <span class="sysj">剩余时间</span>
                                <span class="dp">店铺</span>
                                <span class="wwm">旺旺名</span>
                                <span class="jyzt">交易状态</span>
                                <span class="rwzt">任务状态</span>
                                <span class="expand">&nbsp;</span>
                                <span class="edit">&nbsp;</span>
                                <span class="proportion">&nbsp;</span>
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <span class="chk">
                                <input type="checkbox" id="chk" name="Item" /></span>
                            <span class="xmbh">
                                <asp:Label ID="lblTaskNO" runat="server" Text='<%# Eval("taskNo") %>' ToolTip='<%# Eval("taskNo") %>'></asp:Label></span>
                            <span class="wcqx">
                                <asp:Label ID="lblExpireDate" runat="server" Text='<%# string.Format("{0:MM-dd HH:mm}", Eval("expireDate")) %>' ToolTip='<%# string.Format("{0:yyyy-MM-dd HH:mm}", Eval("expireDate")) %>' /></span>
                            <span class="wcr">
                                <asp:Label ID="lblFinishedPerson" runat="server" Text='<%# Eval("finishedPerson").ToString() == string.Empty ? "--" : Eval("finishedPerson") %>' ToolTip="任务暂未分配" /></span>
                            <span class="lrr">
                                <asp:Label ID="lblEnteringPerson" runat="server" Text='<%# Eval("enteringPerson") %>' />
                            </span>
                            <span class="sfwc">
                                <asp:Label ID="lblFinish" runat="server" Text='<%# Eval("isfinished").ToString() == "1" ? "&#10004" : "&#10007" %>' Font-Size="Medium"
                                    ForeColor='<%# Eval("isfinished").ToString() == "1" ? System.Drawing.ColorTranslator.FromHtml("#00C600") : System.Drawing.ColorTranslator.FromHtml("#FA6356") %>'
                                    ToolTip='<%# Eval("isfinished").ToString() == "1" ? "已完成" : "未完成" %>' />
                            </span>
                            <span class="sysj">
                                <asp:Label ID="lblRemainTime" runat="server"></asp:Label>
                            </span>
                            <span class="dp">
                                <asp:Label ID="lblShop" runat="server" Text='<%# Eval("shop") %>' /></span>
                            <span class="wwm">
                                <asp:Label ID="lblWangwangName" runat="server" Text='<%# Eval("wangwangName") %>' /></span>
                            <span class="jyzt">
                                <asp:Label ID="lblTransactionStatus" runat="server" Text='<%# Eval("transactionStatus") %>' /></span>
                            <span class="rwzt">
                                <asp:Label ID="lblTaskStatus" runat="server" Text='<%# Eval("taskStatus") %>'></asp:Label>
                            </span>
                            <span class="expand" id='<%# "ex"+ Container.ItemIndex %>' onclick="Expand('<%# Container.ItemIndex %>','<%# "cls"+ Container.ItemIndex %>')" title="点击展开">
                                <img src="Images/listdown.jpg" />
                            </span>
                            <span class="edit">
                                <a href="SerialNumberGenerating.aspx?projectID=<%# Eval("ID") %>" target="_blank" title="任务编辑">
                                    <img src="Images/edit.png" style="width: 19px; height: 19px;" />
                                </a>
                            </span>
                            <span class="proportion">
                                <%--<a id="aTaskProportion" runat="server" href='TaskProportionManagement.aspx?projectID=<%# Eval("ID") %>' target="_blank">
                                    分成
                                </a>--%>
                                <asp:HyperLink ID="hlTaskProportion" runat="server" NavigateUrl='<%# "TaskProportionManagement.aspx?projectID=" + Eval("ID") %>' Target="_blank"
                                     Text="&#37" ForeColor="#4E8DD4" Font-Size="Medium" Font-Bold="true" ToolTip="任务分成管理" Font-Underline="false"></asp:HyperLink>
                            </span>
                            <br />
                            <div class="test" id='<%# "cls"+ Container.ItemIndex %>' style="display: none;">
                                <span class="item"><span class="itemtitle">工程名称：</span><%# Eval("projectname") %></span><span class="item"><span class="itemtitle">下单时间：</span><%# string.Format("{0:yyyy-MM-dd HH:mm:ss}", Eval("orderdate")) %></span><span class="item"><span class="itemtitle">金额：</span>&yen;<%# Eval("orderamount") %></span><span class="item"><span class="itemtitle">省份：</span><%# Eval("provincename") %></span><br /><span class="item"><span class="itemtitle">层高：</span><%# Eval("floors") %></span><span class="item"><span class="itemtitle">建筑面积：</span><%# Eval("constructionarea") %></span><span class="item"><span class="itemtitle">结构类型：</span><%# Eval("structureform") %></span><span class="item"><span class="itemtitle">建筑类型：</span><%# Eval("buildingtype") %></span><span class="item"><span class="itemtitle">计价模式：</span><%# Eval("VALUATEMODE") %></span><span class="item"><span class="itemtitle">计价软件：</span><%# Eval("VALUATESOFTWARE") %></span><br /><span class="item"><span class="itemtitle">手机：</span><%# Eval("mobilephone") %></span><span class="item"><span class="itemtitle">旺旺名：</span><%# Eval("wangwangname") %></span><span class="item"><span class="itemtitle">邮箱：</span><%# Eval("email") %></span><span class="item"><span class="itemtitle">QQ：</span><%# Eval("qq") %></span><span class="item"><span class="itemtitle">交易状态：</span><%# Eval("transactionstatus") %></span><span class="item"><span class="itemtitle">支付方式：</span><%# Eval("paymentmethod") %></span><br /><span class="item"><span class="itemtitle">其他要求：</span><%# Eval("extrarequirement") %></span><span class="item"><span class="itemtitle">备注：</span><%# Eval("remarks") %></span></div>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div>
            <label>订单总额：</label>
            <asp:Label ID="lblTotalOrderAmount" runat="server" />
            <label style="padding-left: 15px;">退款总额：</label>
            <asp:Label ID="lblTotalRefund" runat="server" />
            <label style="padding-left: 15px; display:none;">参考薪资：</label>
            <asp:Label ID="lblSalary" runat="server" Visible="false" />
            <%--<label style="padding-left: 15px;">客服薪资：</label>--%>
            <span id="spnCsSalary" style="padding-left: 15px; display:none;" runat="server" visible="false">客服薪资：</span>
            <asp:Label ID="lblCsSalary" runat="server" Visible="false" />
            <label style="padding:0px 2px 0px 10px;">共</label>
            <asp:Label ID="lblTotalRecordAmount" runat="server" />
            <label style="padding:0px 0px;">条记录</label>
            <div class="aspNetPager">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager_PageChanged"
                    FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Never"
                    AlwaysShow="true" UrlPaging="False" ReverseUrlPageIndex="True" TextBeforePageIndexBox="跳到第" TextAfterPageIndexBox="页">
                </webdiyer:AspNetPager>

                <label>跳转到</label><asp:TextBox ID="tb_pageindex" runat="server" Width="20"></asp:TextBox><label>页</label>
                <asp:Button ID="btnGoPage" runat="server" Text="转到" OnClick="btnGoPage_Click" ValidationGroup="pageGo" />
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必须输入页索引" ForeColor="Red" ControlToValidate="tb_pageindex" Display="Dynamic" ValidationGroup="pageGo" />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tb_pageindex" Operator="DataTypeCheck" Type="Integer" ErrorMessage="页索引必须是整数" ForeColor="Red" Display="Dynamic" />
                <asp:Label ID="lbl_error" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
            </div>
        </div>
    </div></form>
</asp:Content>
