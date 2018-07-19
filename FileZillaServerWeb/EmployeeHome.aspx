<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="employeeHome.aspx.cs" Inherits="FileZillaServerWeb.employeeHome" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Import Namespace="Combres" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>我的主页</title>
    <%--<%= WebExtensions.CombresLink("employeehomeCss") %>--%>
<%--    <%= System.Web.Optimization.Styles.Render("~/bundles/employeehomecss") %>--%>
    <link href="/Scripts/bootstrap4/css/bootstrap.css" rel="stylesheet" />
    <link href="/Content/themes/base/ylyj/employeeHome.css?v=180610" rel="stylesheet" />
    <link href="/Scripts/webuploader/webuploader.css?v=180610" rel="stylesheet" />    
    <link href="/Scripts/dialog/jDialog/jDialog.css" rel="stylesheet" />
    <link href="/layui-master/src/css/layui.css" rel="stylesheet" />
    <style type="text/css">
        #imgCerficate {
            width:100%;height:216px; overflow:hidden; border-bottom-left-radius:7px;border-bottom-right-radius:7px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="layui-layout layui-layout-admin">
            <div class="layui-header">
                <div class="layui-logo">YLYJ OA</div>
                <!-- 头部区域（可配合layui已有的水平导航） -->
                <ul class="layui-nav layui-layout-left layui-bg-green">
                    <li class="layui-nav-item">
                        <%= EmployeeNo %>
                    </li>
                </ul>
                <%= UserName %>
                <ul class="layui-nav layui-layout-right">
                    <li class="layui-nav-item">
                        <a href="/UploadFiles/WebSiteDocs/gc.html" target="_blank" title="查看系统使用手册">使用手册</a>
                    </li>
                    <li class="layui-nav-item">
                        <a href="javascript:;">
                            <img src="/UploadFiles/beach.jpg" class="layui-nav-img" />
                            <%= UserName %>
                        </a>
                        <dl class="layui-nav-child">
                            <dd><a href="/PasswordModify.aspx" target="_blank">密码修改</a></dd>
                        </dl>
                    </li>
                    <%--<li class="layui-nav-item"><a href="">退了</a></li>--%>
                </ul>
            </div>

            <div class="layui-side layui-bg-black">
                <div class="layui-side-scroll">
                    <!-- 左侧导航区域（可配合layui已有的垂直导航） -->
                    <ul class="layui-nav layui-nav-tree" lay-filter="test">
                            <asp:TreeView ID="tvEmployees" runat="server" OnSelectedNodeChanged="tvEmployees_SelectedNodeChanged" CssClass="treeview" ForeColor="White"></asp:TreeView>
                    </ul>
                </div>
            </div>

            <div class="layui-body">
                <!-- 内容主体区域 -->
                <div style="padding: 15px;">
                    <div class="layui-row layui-col-space15">
                        <div class="layui-col-md5 topheight">
                            <div style="border: 1px; background-color: #dddddd; height: inherit;">
                                <div style="height: 260px; padding: 10px;">
                                    <div class="layui-row layui-col-md6">
                                        <h6 style="background-color:#007bff; height:28px;line-height:28px;cursor:pointer;color:white;font-size:20px;font-weight:400; border-top-left-radius:5px;border-top-right-radius:5px;padding:2px 0px 2px 10px;"
                                            onclick='SetQualityScore("<%= EmployeeID %>", "<%= EmployeeNo %>", "1");' title="点击可配置技能"><a>我的技能</a>
                                            <span style="float:right; margin-right:8px;">&gt;</span>
                                        </h6>
                                        <div id="divMySkills" runat="server" css="skill"></div>
                                    </div>
                                    <div class="layui-row layui-col-md6">
                                        <h6 style="background-color:#28a745;height:28px;line-height:28px;cursor:pointer;color:white;font-size:20px;font-weight:400;border-top-left-radius:5px;border-top-right-radius:5px;padding:2px 0px 2px 10px;"
                                             onclick='SetMyCertificate("<%= EmployeeID %>");' title="点击可上传证件"><a>我的证件</a>
                                            <span style="float:right; margin-right:8px;">&gt;</span>
                                        </h6>
                                        <%--<img id="imgCerficate" runat="server" style="width:100%;height:216px; overflow:hidden; border-bottom-left-radius:7px;border-bottom-right-radius:7px;" />--%>
                                        <asp:Image ID="imgCerficate" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="layui-col-md2 topheight">
                            <div style="background: #c3e4b1; height: 100%;">
                                <div style="height: 260px; padding: 10px;">
                                    <div id="chart1" style="width:95%; height:95%;float:left;">
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="layui-col-md1 topheight">
                            <div style="background-color: #a2c1a6; height: 100%;">
                                <div style="height: 260px; text-align: center; padding-top: 25px;">
                                    <span class="finishedCount">已完成项目</span>
                                    <br />
                                    <br />
                                    <span style="padding-top: 12px;">
                                        <asp:Label ID="lblFinishedTaskCount" runat="server" ToolTip="我手里的项目数"></asp:Label>
                                    </span>
                                    <br />
                                    <br />
                                    <br />
                                    <span style="display: inline-block; margin-top: 20px;">
                                        <a id="transactionRecords" href="javascript:void(0);" title="查看账户交易明细">明细</a>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="layui-col-md4 topheight">
                            <div style="background: #e2e2e2; height: inherit;">
                                <div style="height: 260px; padding: 10px;">
                                    <h6 style="background-color:#007bff; height:28px;line-height:28px;color:white;font-size:20px;font-weight:400; border-top-left-radius:5px;border-top-right-radius:5px;padding:2px 0px 2px 10px;"
                                            ><a>工作动态</a></h6>
                                    <div id="taskTrendApp">
                                        <ul>
                                            <li v-for="item in trends" style="list-style:circle; display:inline-block; height:28px; margin:5px 12px; padding:2px 4px; border-bottom:1px solid #c3e4b1;">
                                                {{item.FriendlyDate}}, {{item.TrendContent}}
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="layui-row" style="border:1px solid #eeeeee;">
                        <div class="layui-col-md12">
                            <div style="height:230px; padding: 1px 0px;">
                                <div class="layui-col-md9">
                                    <h3 style="height:34px; line-height:34px; padding:0px 0px 0px 10px; background-color:#eeeeee;"></h3>
                                </div>
                                <div class="layui-col-md3 layui-layout-right">
                                    <div class="layui-col-md9">
                                        <asp:TextBox ID="txtAnyCondition" runat="server" placeholder="可全文检索" CssClass="layui-input" style="height:34px;line-height:34px;" ></asp:TextBox>
                                    </div>
                                    <div class="layui-col-md3">
                                        <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click"  CssClass="layui-btn" style="width:100%; height:34px;line-height:34px;"/>
                                    </div>
                                </div>
                                <asp:GridView ID="gvProject" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvProject_RowDataBound" DataKeyNames="prjID,isFinished" OnRowCommand="gvProject_RowCommand" Width="100%" CssClass="layui-table tblproject" lay-size="sm" lay-even Style="margin-top: 0px; text-align: center;">
                                    <HeaderStyle ForeColor="White" BackColor="#666666" Font-Bold="true" />
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
                                        <asp:TemplateField HeaderText="任务状态">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaskStatus" runat="server" Text='<%# Eval("taskStatus") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="完成人">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFinishedPerson" runat="server" Text='<%# Eval("EMPLOYEENO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="是否完成">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIsFinished" runat="server" Text='<%# Convert.ToInt32(Eval("ISFINISHED")) == 1 ? "&#10004;" : "&#10007;" %>'></asp:Label>
                                                <asp:Button ID="btnSetFinished" runat="server" Text="我已完成" Visible='<%# Convert.ToInt32(Eval("ISFINISHED")) == 0 %>' CommandName="setFinished" CommandArgument='<%# Eval("prjId") %>' CssClass="taskmovebutton" OnClientClick="return confirm('确定置为完成？');" />
                                                <asp:HiddenField ID="hidIsFinished" runat="server" Value='<%# Eval("ISFINISHED") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="剩余时间">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTimeRemain" runat="server" Text="--"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="预计提成／&yen;">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidOrderAmount"  runat="server" Value='<%# Eval("orderAmount") %>' />
                                                <asp:Label ID="lblExpectAmount" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="提成金额／&yen;">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProportionAmount" runat="server" Text=''></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="修改剩余时间">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModifyTaskTimeRemain" runat="server" Text="--"></asp:Label>
                                                <asp:Button ID="btnSetModifyTasksFinished" runat="server" Text="我已完成" Visible="false" CommandName="setModifyFinished" CommandArgument='<%# Eval("prjId") %>' CssClass="taskmovebutton" OnClientClick="return confirm('确定置为完成？');" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="操作">
                                            <ItemTemplate>
                                                <input type="button" id="btnViewPrjFiles" value="查看资料" title="查看资料" class="taskmovebutton" style="float: left; margin-right: 10px;" onclick='ViewPrjFiles("<%# Eval("prjID") %>","<%# Eval("taskno") %>");' />
                                                <input type="button" id="btnTransfer" value="转移任务" title="任务转移" class="taskmovebutton" style=' display: <%# (Convert.ToInt32(Eval("ISFINISHED")) != 1 && IsBranchLeader) ? "block" : "none"%> ;' onclick='TransferTask("<%# Eval("prjID") %>", "<%= EmployeeID %>", "<%# Eval("orderAmount")%>", "<%# Eval("taskno")%>");' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div class="aspNetPager">
                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager_PageChanged"
                                        FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Never"
                                        AlwaysShow="true" UrlPaging="False" ReverseUrlPageIndex="True" TextBeforePageIndexBox="跳到第" TextAfterPageIndexBox="页"
                                        CssClass="pagination" PagingButtonLayoutType="UnorderedList" PagingButtonSpacing="0" CurrentPageButtonClass="active">
                                    </webdiyer:AspNetPager>
                                    <%--<div style="height:30px; line-height:30px;">
                                        <label>跳转到</label><asp:TextBox ID="tb_pageindex" runat="server"></asp:TextBox><label>页</label>
                                        <asp:Button ID="btnGoPage" runat="server" Text="转到" OnClick="btnGoPage_Click" ValidationGroup="pageGo" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必须输入页索引" ForeColor="Red" ControlToValidate="tb_pageindex" Display="Dynamic" ValidationGroup="pageGo" />
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tb_pageindex" Operator="DataTypeCheck" Type="Integer" ErrorMessage="页索引必须是整数" ForeColor="Red" Display="Dynamic" />
                                        <asp:Label ID="lbl_error" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="layui-row" style="border: 0px solid #ffd800;">
                     <!-- 文件列表部分 -->
                     <div id="project" class="container" style="clear:both; float:left; font-family:'Microsoft YaHei',sans-serif;">
                        <div id="meun">
                            <div class="row">
                                <div class="col -12" style="text-align: left;">
                                    <div class="btn-group btn-group-lg">
                                        <button type="button" id="fileTabTitle" class="btn btn-default btn-primary" @click="changeTab(false,true,false)" title="点击可查看任务资料">任务资料</button>
                                        <button type="button" class="btn btn-default btn-success" @click="changeTab(true,false,false)" title="查看文件操作记录">操作历史</button>
                                        <button type="button" class="btn btn-default" @click="changeTab(false,false,true)" title="添加一个新的资料标签，如 完成稿、修改完成1">&#10010;</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="projectfile" v-show="showfile">
                            <!-- Tab List -->
                            <div class="row">
                                <div class="col -12">
                                    <div id="divFileTabs" class="btn-group btn-group-sm">
                                        <!-- change file list -->
                                        <button type="button" :key="item.Id" :title="item.description" :filetype="item.Id" class="btn btn-default btn-primary" v-for="item in projectfile.filetabs"
                                            @click="changeFilesTab(item.Id)">
                                            {{item.title}}</button>
                                    </div>
                                </div>
                            </div>

                            <!-- File List -->
                            <div class="row">
                                <div class="col -12">
                                    <div id="showfiles" style="text-align: left">
                                        <table class="table table-bordered table-hover  table-striped">
                                            <tbody>
                                                <tr v-for=" file in projectfile.files" v-show="projectfile.parentId==file.categoryId">
                                                    <td :title="file.description">
                                                        <img :src="'/'+file.fileIconPath" style="margin-right:8px;" />{{file.fileName}}
                                                    </td>
                                                    <td>
                                                        <div class="btn-group btn-group-sm">
                                                            <button type="button" class="btn btn-default btn-danger" @click="deleteFile(file.fileHistoryId)">删除</button>
                                                            <a id="aDownload" :href="'/HttpHandler/FileHandler.ashx?FuncName=DownloadFile&fileHistoryId='+file.fileHistoryId" class="btn btn-success">下载</a>
                                                            <button type="button" class="btn btn-dark" @click="previewFile(file.fileHistoryId, file.fileExt)">预览</button>                                                            
                                                            <span id="clip_container" style="display:none;">
                                                                <input id="copyHref" type="button" class="btn btn-info" value="复制链接" />
                                                            </span>
                                                            <a id="aPreview" target="_blank" style="visibility:hidden"></a>
                                                            <img :id="'loadingimg'+file.fileHistoryId" src="/Images/loadingAnimation.gif" style="display:none;" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <!-- Upload File -->
                            <div class="row">
                                <div class="col-3">
                                    <span>描述：</span>
                                    <input type="text" name="desc" id="filedesc" v-model="projectfile.filedesc" placeholder="可输入文件的简单描述" title="可输入针对该文件的描述信息，上传后鼠标悬浮在文件名之上会显示该信息">
                                </div>
                                <div class="col-9">
                                    <div style="margin: 0 0 0 50px;">
                                        <div id="file1" style="float: left;" title="点击后选择一个文件可将选中文件上传到服务器">浏览</div>
                                        <span id="pfile1"></span>
                                        <div id="file1progress" class="progress" style="width: 500px; float: left; margin: 10px 0 0 20px;">
                                            <div id="file1progressbar" class="progress-bar progress-bar-striped active" role="progressbar" style="width: 0%;"></div>
                                        </div>
                                        <div style="clear: both;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="projecthistory" v-show="showhistory">
                            <div class="row">
                                <div class="col-12">
                                    <%--<h4>Project ID:{{projectid}}</h4>--%>
                                    <div>
                                        <table class="table table-bordered table-hover  table-striped">
                                            <!-- 表头 -->
                                            <thead>
                                                <th>
                                                    时间
                                                </th>
                                                <th>
                                                    操作人
                                                </th>
                                                <th>
                                                    内容
                                                </th>
                                            </thead>

                                            <!-- 内容 -->
                                            <tbody>
                                                <tr v-for=" item in projecthistory.data " :key="item.id ">
                                                    <td>
                                                        {{item.operateDate|convTime}}
                                                    </td>
                                                    <td>
                                                        {{item.operateUser}}
                                                    </td>
                                                    <td>
                                                        {{item.operateContent}}
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="addtab" v-show="showaddtab">
                             添加一个新的标签（如修改完成1、疑问答复1）
                            <div class="row">
                                <div class="col-12">
                                    <form class="form-inline" role="form">
                                        <div class="form-group">
                                            <label for="category">选择标签：</label>
                                            <select class="form-control" name="category" id="category" v-model="newtab.categoryselected" @change="categoryChange()">
                                                <option v-for="item in newtab.category" :value="item.key">{{item.value}}</option>
                                            </select>
                                        </div>

                                        <div class="form-group" v-show="showreply">
                                            <label for="replyto">回复：</label>
                                            <select class="form-control" name="replyto" id="replyto" v-model="newtab.replytoselected">
                                                <option v-for="item in newtab.replyto" :value="item.Id">
                                                    {{item.Title}}
                                                </option>
                                            </select>
                                        </div>

                                        <div class="form-group">
                                            <label for="category">描述：</label>
                                            <input type="text" name="tabdesc" id="tabdesc" v-model="newtab.desc" title="可输入该修改任务的描述性文字">
                                        </div>

                                        <div class="form-group">
                                            <label for="category">交稿时间：</label>
                                            <input type="text" name="tabexpiredate" id="tabexpiredate" class="Wdate" onFocus="WdatePicker({lang:'zh-cn',dateFmt:'yyyy-MM-dd HH:00:00'})">
                                        </div>

                                        <button type="button" class="btn btn-secondary" @click="addTab()" id="add">添加</button>
                                    </form>
                                    <div class="form-group">
                                        <p>{{this.newtab.returnmessage}}</p>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            </div>

            <div class="layui-footer" style="color:#666;">
                <!-- 底部固定区域 -->
                &copy; <label id="lblCurrentYear"></label> bimpan.iok.la - Yiliangyijia Consultation Co. Ltd.
            </div>
        </div>
        <div style="display:none;">
            <input type="hidden" id="hidEmployeeID" value="<%= EmployeeID %>" />
        </div>
    </form>

    <%--jQuery, bootStrap--%>
        <script src="<%= ResolveUrl("~/Scripts/jquery-3.3.1.min.js") %>"></script>
    <%= WebExtensions.CombresLink("jQueryAndBootStrapJs") %>

<%--    <%= System.Web.Optimization.Scripts.Render("~/bundles/jQueryAndBootStrap") %>--%>
<%--    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/jquery-ui-1.8.20.min.js"></script>
    <script src="Scripts/dialog/jDialog.js"></script>
    <script src="Scripts/bootstrap4/js/bootstrap.js"></script>--%>

    <%--Vue--%>
    <script src="<%= ResolveUrl("~/Scripts/vue/vue.min.js?v=187151") %>"></script>
<%--    <%= WebExtensions.CombresLink("customeVueJs") %>--%>
<%--    <%= System.Web.Optimization.Scripts.Render("~/bundles/vuejs") %>--%>    
    <script src="<%= ResolveUrl("~/Scripts/ylyj/employeehome/func.js?v=18070701") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/ylyj/employeehome/settings.js?v=18070701") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/ylyj/employeehome/vuepage.js?v=18070705") %>"></script>

    <%--web uploader, datepicker--%>
    <script src="<%= ResolveUrl("~/Scripts/webuploader/webuploader.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/ylyj/employeehome/uploadfile.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/My97DatePicker/WdatePicker.js") %>"></script>

    <%--layui--%>
    <script src="<%= ResolveUrl("~/layui-master/src/layui.js") %>"></script>
    <script type="text/javascript">
        layui.use("element", function () {
            var element = layui.element;
        });
    </script>
    
    <script src="<%= ResolveUrl("~/Scripts/echarts/echarts.common.min.js") %>"></script>
    <%--<script src="Scripts/zeroclipboard/ZeroClipboard.js"></script>--%>
    <script src="<%= ResolveUrl("~/Scripts/ylyj/employeeHome.js?v=18070701") %>"></script>
</body>
</html>
