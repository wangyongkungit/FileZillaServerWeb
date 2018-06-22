<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="employeeHome.aspx.cs" Inherits="FileZillaServerWeb.employeeHome" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>我的主页</title>
    <link href="Scripts/bootstrap4/css/bootstrap.css" rel="stylesheet" />
    <link href="Content/themes/base/ylyj/employeeHome.css?v=180610" rel="stylesheet" />
    <link href="Scripts/webuploader/webuploader.css?v=180610" rel="stylesheet" />    
    <link href="<%= ResolveUrl("~/Scripts/dialog/jDialog/jDialog.css") %>" rel="stylesheet" />
    <link href="layui-master/src/css/layui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="layui-layout layui-layout-admin">
            <div class="layui-header">
                <div class="layui-logo">YLYJ OA</div>
                <!-- 头部区域（可配合layui已有的水平导航） -->
                <ul class="layui-nav layui-layout-left layui-bg-green">
                    <li class="layui-nav-item">
                        <%--<a href="">前台</a>--%>
                        <%= EmployeeNo %>
                    </li>
                    <!--<li class="layui-nav-item">
                    <a href="javascript:;">其它系统</a>
                    <dl class="layui-nav-child">
                        <dd><a href="">邮件管理</a></dd>
                        <dd><a href="">消息管理</a></dd>
                        <dd><a href="">授权管理</a></dd>
                    </dl>
                </li>-->
                </ul>
                <%= EmployeeNo %>
                <ul class="layui-nav layui-layout-right">
                    <li class="layui-nav-item">
                        <a href="javascript:;">
                            <img src="http://t.cn/RCzsdCq" class="layui-nav-img">
                            <%= UserName %>
                        </a>
<%--                        <dl class="layui-nav-child">
                            <dd><a href="">基本资料</a></dd>
                            <dd><a href="">安全设置</a></dd>
                        </dl>--%>
                    </li>
                    <%--<li class="layui-nav-item"><a href="">退了</a></li>--%>
                </ul>
            </div>

            <div class="layui-side layui-bg-black">
                <div class="layui-side-scroll">
                    <!-- 左侧导航区域（可配合layui已有的垂直导航） -->
                    <ul class="layui-nav layui-nav-tree" lay-filter="test">
                        <%--<li class="layui-nav-item layui-nav-itemed">--%>
                            <%--<a class="" href="javascript:;">我自己</a>
                            <dl class="layui-nav-child">
                                <dd><a href="javascript:;">C004</a></dd>
                                <dd><a href="javascript:;">C005</a></dd>
                                <dd><a href="javascript:;">C006</a></dd>
                                <dd><a href="javascript:;">C011</a></dd>
                            </dl>--%>
                            <asp:TreeView ID="tvEmployees" runat="server" OnSelectedNodeChanged="tvEmployees_SelectedNodeChanged" CssClass="treeview" ForeColor="White"></asp:TreeView>
                        <%--</li>--%>
                        <!--<li class="layui-nav-item">
                        <a href="javascript:;">解决方案</a>
                        <dl class="layui-nav-child">
                            <dd><a href="javascript:;">列表一</a></dd>
                            <dd><a href="javascript:;">列表二</a></dd>
                            <dd><a href="">超链接</a></dd>
                        </dl>
                    </li>
                    <li class="layui-nav-item"><a href="">云市场</a></li>
                    <li class="layui-nav-item"><a href="">发布商品</a></li>-->
                    </ul>
                </div>
            </div>

            <div class="layui-body">
                <!-- 内容主体区域 -->
                <div style="padding: 15px;">
                    <div class="layui-row layui-col-space15">
                        <div class="layui-col-md5" style="height: inherit;">
                            <div style="border: 1px; background-color: #dddddd; height: inherit;">
                                <div style="height: 260px; padding: 10px;">
                                    <div class="layui-row layui-col-md6">
                                        <h6 style="background-color:#007bff; height:28px;line-height:28px;cursor:pointer;color:white;font-size:20px;font-weight:400; border-radius:5px;padding:2px 0px 2px 10px;"
                                            onclick='SetQualityScore("<%= EmployeeID %>", "<%= EmployeeNo %>", "1");'><a>我的技能</a></h6>
                                        <div id="divMySkills" runat="server" css="skill"></div>
                                    </div>
                                    <div class="layui-row layui-col-md6">
                                        <h6 style="background-color:#28a745;height:28px;line-height:28px;cursor:pointer;color:white;font-size:20px;font-weight:400; border-radius:5px;padding:2px 0px 2px 10px;"
                                             onclick='SetMyCertificate("<%= EmployeeID %>");'><a>我的证件</a></h6>
                                        <img id="imgCerficate" runat="server" style="width:100%;height:216px; overflow:hidden; border-radius:7px;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="layui-col-md2" style="height: inherit;">
                            <div style="background: #c3e4b1; height: 100%;">
                                <div style="height: 260px; padding: 10px;">
                                    <div id="chart1" style="width:95%; height:95%;float:left;">
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="layui-col-md1" style="height: inherit;">
                            <div style="background-color: #a2c1a6; height: 100%;">
                                <div style="height: 260px; text-align: center; padding-top: 25px;">
                                    <span>已完成项目</span>
                                    <br />
                                    <br />
                                    <span style="padding-top: 12px;">
                                        <asp:Label ID="lblFinishedTaskCount" runat="server" Font-Size="XX-Large" ForeColor="White"></asp:Label>
                                    </span>
                                    <br />
                                    <br />
                                    <br />
                                    <span style="display: inline-block; margin-top: 20px;">
                                        <a id="transactionRecords" href="javascript:void(0);">交易记录</a>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="layui-col-md4" style="height: inherit;">
                            <div style="background: #e2e2e2; height: inherit;">
                                <div style="height: 260px; padding: 10px;">
                                    <h6 style="background-color:#007bff; height:28px;line-height:28px;cursor:pointer;color:white;font-size:20px;font-weight:400; border-radius:5px;padding:2px 0px 2px 10px;"
                                            ><a>任务动态</a></h6>
                                    <asp:GridView ID="gvTaskTrend" runat="server" AutoGenerateColumns="false" CssClass="layui-table" lay-even ShowHeader="false" >
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreateDate" runat="server" Text=' <%# Convert.ToDateTime(Eval("createDate")).ToString("M-d HH:mm") %> '> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
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
<%--                                <asp:GridView ID="gvList" runat="server" CssClass="layui-table" lay-size="sm" lay-even style="margin-top:0px;">
                                    <HeaderStyle ForeColor="White" BackColor="#666666" Font-Bold="true" />
                                </asp:GridView>--%>
                                <asp:GridView ID="gvProject" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvProject_RowDataBound" DataKeyNames="prjID,isFinished" Width="100%" CssClass="layui-table" lay-size="sm" lay-even style="margin-top:0px; text-align:center;">
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
                                    <asp:Label ID="lblModifyTaskTimeRemain" runat="server" Text="暂无任务"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <input type="button" id="btnViewPrjFiles" value="查看资料" title="查看资料" class="taskmovebutton" onclick='ViewPrjFiles("<%# Eval("prjID") %>","<%# Eval("taskno") %>");' />
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
<%--                        <div style="height:30px; line-height:30px;">
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
                     <div id="project" class="container" style="clear:both; float:left;">
                     <p>{{taskno}}</p>
                        <div id="meun">
                            <div class="row">
                                <div class="col -12" style="text-align: left;">
                                    <div class="btn-group btn-group-lg">
                                        <button type="button" class="btn btn-default btn-primary" @click="changeTab(false,true,false)">文件列表</button>
                                        <button type="button" class="btn btn-default btn-success" @click="changeTab(true,false,false)">操作历史</button>
                                        <button type="button" class="btn btn-default" @click="changeTab(false,false,true)">&#10010;</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="projectfile" v-show="showfile">
                            文件列表
                            <!-- Tab List -->
                            <div class="row">
                                <div class="col -12">
                                    <div class="btn-group btn-group-sm">
                                        <!-- change file list -->
                                        <button type="button" :key="item.Id" :title="item.description" :filetype="item.Id" class="btn btn-default btn-primary " v-for="item in projectfile.filetabs"
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
                                                    <td>
                                                        {{file.operateContent}}
                                                        <!-- 图标: -->
                                                        <!-- <span :title="file.filedesc">{{file.fileName}}</span>
                                                        <span :title="">{{file.filePath}}</span>
                                                        <a href="dotPeek.rar">rar</a>
                                                        <a href="uploadfile.html">html</a> -->
                                                    </td>
                                                    <td :title="file.description">
                                                        {{file.fileName}}
                                                    </td>
                                                    <td>
                                                        {{file.operateUser}}
                                                    </td>
                                                    <td>
                                                        <div class="btn-group btn-group-sm">
                                                            <button type="button" class="btn btn-default btn-danger" @click="deleteFile(file.fileHistoryId)">删除</button>
                                                            <button type="button" class="btn btn-default btn-success" @click="downloadFile(file.fileHistoryId)">下载</button>
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
                                    <span>描述:</span>
                                    <input type="text" name="desc" id="filedesc" v-model="projectfile.filedesc">
                                </div>
                                <div class="col-9">
                                    <div style="margin: 0 0 0 50px;">
                                        <div id="file1" style="float: left;">请选择</div>
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
                            操作历史
                            <div class="row">
                                <div class="col-12">
                                    <%--<h4>Project ID:{{projectid}}</h4>--%>
                                    <div>
                                        <table class="table table-bordered table-hover  table-striped">
                                            <!-- 表头 -->
                                            <thead>
                                                <td>
                                                    时间
                                                </td>
                                                <td>
                                                    操作人
                                                </td>
                                                <td>
                                                    内容
                                                </td>
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
                            添加新标签
                            <div class="row">
                                <div class="col-12">
                                    <form class="form-inline" role="form">
                                        <div class="form-group">
                                            <label for="category">选择列表 : </label>
                                            <select class="form-control" name="category" id="category" v-model="newtab.categoryselected" @change="categoryChange()">
                                                <option v-for="item in newtab.category" :value="item.key">{{item.value}}</option>
                                            </select>
                                            <span> {{newtab.categoryselected}}</span>
                                        </div>

                                        <div class="form-group" v-show="showreply">
                                            <label for="replyto">回复 : </label>
                                            <select class="form-control" name="replyto" id="replyto" v-model="newtab.replytoselected">
                                                <option v-for="item in newtab.replyto" :value="item.Id">
                                                    {{item.Title}}
                                                </option>
                                            </select>
                                            <span> {{newtab.replytoselected}}</span>
                                        </div>

                                        <div class="form-group">
                                            <label for="category">描述 : </label>
                                            <input type="text" name="tabdesc" id="tabdesc" v-model="newtab.desc">
                                        </div>

                                        <div class="form-group">
                                            <label for="category">交稿时间：</label>
                                            <input type="text" name="tabexpiredate" id="tabexpiredate" class="Wdate" onFocus="WdatePicker({lang:'zh-cn',dateFmt:'yyyy-MM-dd HH:00:00'})">
                                        </div>

                                        <button type="button" class="btn btn-default" @click="addTab()" id="add">新增</button>
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

            <div class="layui-footer">
                <!-- 底部固定区域 -->
                &copy; bimpan.iok.la - Yiliangyijia Consultation Co. Ltd.
            </div>
        </div>
        <div style="display:none;">
            <%--<asp:HiddenField ID="hidEmployeeID" runat="server" Value='<%# EmployeeID %>' />--%>
            <input type="hidden" id="hidEmployeeID" value="<%= EmployeeID %>" />
        </div>
    </form>

    <%--jQuery, bootStrap--%>
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/jquery-ui-1.8.20.js"></script>
    <script src="Scripts/dialog/jDialog.js"></script>
    <script src="Scripts/bootstrap4/js/bootstrap.js"></script>

    <%--Vue--%>
    <script src="Scripts/vue/vue.js"></script>
    <script src="Scripts/ylyj/employeehome/func.js"></script>
    <script src="Scripts/ylyj/employeehome/settings.js"></script>
    <script src="Scripts/ylyj/employeehome/vuepage.js"></script>

    <%--web uploader, datepicker--%>
    <script src="Scripts/webuploader/webuploader.js"></script>
    <script src="Scripts/ylyj/employeehome/uploadfile.js"></script>
    <script src="Scripts/My97DatePicker/WdatePicker.js"></script>

    <%--layui--%>
 <%--   <script src="layui-master/src/layui.js"></script>
    <script type="text/javascript">
        layui.use("element", function () {
            var element = layui.element;
        })
    </script>--%>
    
   <script src="Scripts/echarts/echarts.common.min.js"></script>
    <script src="Scripts/ylyj/employeeHome.js"></script>
</body>
</html>
