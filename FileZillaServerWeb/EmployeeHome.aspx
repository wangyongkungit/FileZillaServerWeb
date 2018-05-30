<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeHome.aspx.cs" Inherits="FileZillaServerWeb.EmployeeHome" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="https://cdn.bootcss.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />    --%>
    <link href="Content/themes/base/ylyj/employeeHome.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Scripts/echarts/echarts.common.min.js"></script>
    <form id="form1" runat="server">
        <div style="display:none;">
            <%--<asp:HiddenField ID="hidEmployeeID" runat="server" Value='<%# EmployeeID %>' />--%>
            <input type="hidden" id="hidEmployeeID" value="<%= EmployeeID %>" />
        </div>
        <div class="total">
            <div class="navbar">
                <div class="pullLeft">
                    <%= EmployeeNo %>
                </div>
                <div class="pullRight">
                    欢迎你，<%= UserName %>
                </div>
            </div>
            <div class="sideNav">
                <asp:TreeView ID="tvEmployees" runat="server" OnSelectedNodeChanged="tvEmployees_SelectedNodeChanged" CssClass="treeview"></asp:TreeView>

            </div>
            <div class="content">
                <div class="content-left">
                    <div class="content-left one">
                        <div class="content-left one title" onclick='SetQualityScore("<%= EmployeeID %>", "<%= EmployeeNo %>", "1");' style="cursor:pointer;">我的技能</div>
                        <%--<asp:Label ID="lblMySkills" runat="server" CssClass="skill"></asp:Label>--%>
                        <div id="divMySkills" runat="server" css="skill"></div>
                    </div>
                    <div class="content-left one">
                        <div class="content-left one title" onclick='SetMyCertificate("<%= EmployeeID %>");' style="cursor:pointer;">我的证件</div>
                        <%--<asp:Image ID="imgCerficate" runat="server" CssClass="cerficate" />--%>
                        <img id="imgCerficate" runat="server" style="width:100%;height:96px;" />
                    </div>
                    <div class="content-left one">
                        <div class="content-left one title">我的职位</div>
                    </div>
                    <div class="content-left one">
                        <div class="content-left one title"></div>
                    </div>
                </div>
                <div class="content-middle">
                        <div class="moneyTitle">
                            我的账本
                        </div>
                    <div class="moneyLeft">
                        <div id="chart1" style="width:260px; height:260px;">
                            <%--<asp:Chart ID="ChartMoney" runat="server" Width="240px" Height="240px">
                                <Series>
                                    <asp:Series Name="Series1" ChartType="Pie" IsXValueIndexed="True" Legend="Legend1"></asp:Series>chart
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend Enabled="False" Name="Legend1">
                                    </asp:Legend>
                                </Legends>
                                <BorderSkin BackColor="142, 102, 102" PageColor="142, 102, 102" />
                            </asp:Chart>--%>

                        </div>
                    </div>
                    <div class="moneyRight">
                        <div class="moneyRight container">
                            <label class="lblfinished">已完成项目</label>
                            <asp:Label ID="lblFinishedTaskCount" runat="server"></asp:Label>
                            <br />
                            <label class="lblfinished">余额</label>
                            <asp:Label ID="lblCanWithdrawAmount" runat="server"></asp:Label>
                            <a id="withdraw" href="javascript:void(0);">提现</a>
                            <a id="withdrawRecords" href="javascript:void(0);">提现记录</a>
                        </div>
                    </div>
                </div>
                <div class="content-right">
                    <div class="trendTitle">
                            任务动态
                        </div>
                    <div>
                        <asp:GridView ID="gvTaskTrend" runat="server" AutoGenerateColumns="false" CssClass="tasktrendtable">
                            <Columns>
                                <asp:TemplateField HeaderText="时间">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreateDate" runat="server" Text=' <%# Convert.ToDateTime(Eval("createDate")).ToString("M-d HH:mm") %> '> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="内容">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# Eval( "description ") %>' ToolTip='<%# Eval( "description ") %>'> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <span>暂无动态</span>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
                <div class="content-center">
                    <div class="title">
                        项目概览
                        <div class="search">
                            <asp:TextBox ID="txtAnyCondition" runat="server" placeholder="全文检索，可输入项目编号等进行检索" CssClass="text"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" CssClass="button"/>
                        </div>
                    </div>
                    <asp:GridView ID="gvProject" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvProject_RowDataBound" DataKeyNames="prjID,isFinished" Width="100%" CssClass="projectList">
                        <Columns>
                            <asp:TemplateField HeaderText="项目编号">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectNo" runat="server" Text='<%# Eval("TASKNO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="完成期限">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hidExpireDate" runat="server" Value='<%# Eval("EXPIREDATE") %>' />
                                    <asp:Label ID="lblExpireDate" runat="server" Text='<%# Convert.ToDateTime(Eval("EXPIREDATE")).ToString("MM-dd HH:mm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="完成人">
                                <ItemTemplate>
                                    <asp:Label ID="lblFinishedPerson" runat="server" Text='<%# Eval("EMPLOYEENO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否完成">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsFinished" runat="server" Text='<%# Convert.ToInt32(Eval("ISFINISHED")) == 1 ? "&#10004" : "&#10007" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="剩余时间">
                                <ItemTemplate>
                                    <asp:Label ID="lblTimeRemain" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="提成金额">
                                <ItemTemplate>
                                    <asp:Label ID="lblProportionAmount" runat="server" Text=''></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改剩余时间">
                                <ItemTemplate>
                                    <asp:Label ID="lblModifyTaskTimeRemain" runat="server" Text=''></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <input type="button" id="btnTransfer" value="&#8658;" title="任务转移" class="taskmovebutton" onclick='TransferTask("<%# Eval("prjID") %>", "<%= EmployeeID %>", 500);' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="aspNetPager">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager_PageChanged"
                            FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Never"
                            AlwaysShow="true" UrlPaging="False" ReverseUrlPageIndex="True" TextBeforePageIndexBox="跳到第" TextAfterPageIndexBox="页"
                            CssClass="pagination"  PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active" >
                        </webdiyer:AspNetPager>
                        <div style="height:30px; line-height:30px; float:right;">
                        <label>跳转到</label><asp:TextBox ID="tb_pageindex" runat="server"></asp:TextBox><label>页</label>
                        <asp:Button ID="btnGoPage" runat="server" Text="转到" OnClick="btnGoPage_Click" ValidationGroup="pageGo" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必须输入页索引" ForeColor="Red" ControlToValidate="tb_pageindex" Display="Dynamic" ValidationGroup="pageGo" />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tb_pageindex" Operator="DataTypeCheck" Type="Integer" ErrorMessage="页索引必须是整数" ForeColor="Red" Display="Dynamic" />
                        <asp:Label ID="lbl_error" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                            </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="Scripts/ylyj/employeeHome.js"></script>
</asp:Content>
