<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SerialNumberGenerating.aspx.cs" Inherits="FileZillaServerWeb.SerialNumberGenerating" %>
<%@ Register assembly="Brettle.Web.NeatUpload" namespace="Brettle.Web.NeatUpload" tagprefix="Upload" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/themes/base/ylyj/serialnumber.css?v=18426" rel="stylesheet" />
    <script src="Scripts/clipboard.min.js?v=18426"></script>
    <script src="Scripts/ylyj/serialnumbergenerating.js?v=18426"></script>
    <script src="http://cdn.bootcss.com/jqueryui/1.11.0/jquery-ui.min.js"></script>
    <script src="Scripts/jQuery-UI/jquery.multiselect.js"></script>
    <%--<link href="http://cdn.bootcss.com/jqueryui/1.11.0/jquery-ui.min.css" rel="stylesheet">--%>
    <link href="Scripts/jQuery-UI/jquery-ui.css" rel="stylesheet" />
    <link href="Scripts/jQuery-UI/jquery.multiselect.css" rel="stylesheet" />
    <script type="text/javascript">

        $(function(){
            var selctedValues = $("#hidDdlSpecialtySelectedValue").val();
            var arr=selctedValues.split(",");
            for(var i=0;i<arr.length;i++)
            {
                $("#ContentPlaceHolder1_ddlSpecialtyCategory").find("option[value='"+arr[i]+"']").attr("selected",true);
            }
            $("#ContentPlaceHolder1_ddlSpecialtyCategory").find("option[value='']").attr("disabled",true).attr("selected",false);
            $("#ContentPlaceHolder1_ddlSpecialtyCategory").find("option[value='0']").attr("disabled",true);
            $("#ContentPlaceHolder1_ddlSpecialtyCategory").find("option[value='1']").attr("disabled",true);
            $("#ContentPlaceHolder1_ddlSpecialtyCategory").find("option[value='2']").attr("disabled",true);
            $("#ContentPlaceHolder1_ddlSpecialtyCategory").find("option[value='3']").attr("disabled",true);
            $("#ContentPlaceHolder1_ddlSpecialtyCategory").find("option[value='4']").attr("disabled",true);
            $("#ContentPlaceHolder1_ddlSpecialtyCategory").find("option[value='5']").attr("disabled",true);
            $("#ContentPlaceHolder1_ddlSpecialtyCategory").find("option[value='6']").attr("disabled",true);
            $("#ContentPlaceHolder1_ddlSpecialtyCategory").multiselect({
                multiple:"multiple",
                checkAllText:"",
                uncheckAllText:"",
                noneSelectedText:"请选择",
                selectedText:"#项已选",
                selectedList: 2,
                height:540
            });

            $("#ContentPlaceHolder1_txtProjectName").blur(function(){ autoCompleteFillTaskBook();});
            $("#ContentPlaceHolder1_ddlValuateMode").change(function(){ autoCompleteFillTaskBook();});
            $("#ContentPlaceHolder1_ddlProvince").change(function(){ autoCompleteFillTaskBook();});
            $("#ContentPlaceHolder1_ddlModelingSoftware").change(function(){ autoCompleteFillTaskBook();});
            $("#ContentPlaceHolder1_txtValuateSoftware").blur(function(){ autoCompleteFillTaskBook();});
            $("#ContentPlaceHolder1_ddlSpecialtyCategory").change(function(){ autoCompleteFillTaskBook();});
        });

        var autoCompleteFillTaskBook=function(){
            //var arrVar=["工程名称：","计价模式：","省份：","建模软件：","计价软件：","专业："];
            //var inputIds=["ContentPlaceHolder1_txtProjectName","ContentPlaceHolder1_ddlValuateMode","ContentPlaceHolder1_ddlProvince",
            //    "ContentPlaceHolder1_ddlModelingSoftware","ContentPlaceHolder1_txtValuateSoftware","ContentPlaceHolder1_ddlSpecialtyCategory"];
            //var taskBookText;
            //var arrLength=arrVar.length;
            //for(var i=0;i<arrVar.length;i++){
            //    //taskBookText+=arrVar[i]
            //    var inputValue=$("#"+inputIds[i]+"").val();
            //    if(inputValue){
            //        taskBookText+=arrVar[i]+inputIds[i];
            //    }
            //}

            // 工程名称、计价模式、省份、算量软件（模式）、专业类别
            var projectName="",valuateMode="",province="",modelingSoftware="",valuateSoftware="",specialty="";
            if($("#ContentPlaceHolder1_txtProjectName").val()!=""){
                projectName="工程名称："+$("#ContentPlaceHolder1_txtProjectName").val() + "    ";
            }
            if($("#ContentPlaceHolder1_ddlValuateMode").find("option:selected").text()!="-请选择-"){
                valuateMode="计价模式："+$("#ContentPlaceHolder1_ddlValuateMode").find("option:selected").text()+ "   ";
            }
            if($("#ContentPlaceHolder1_ddlProvince").find("option:selected").text()!="-请选择-"){
                province="省份："+$("#ContentPlaceHolder1_ddlProvince").find("option:selected").text()+ "    ";
            }
            if($("#ContentPlaceHolder1_ddlModelingSoftware").find("option:selected").text()!="-请选择-"){
                modelingSoftware="建模软件："+$("#ContentPlaceHolder1_ddlModelingSoftware").find("option:selected").text()+ "    ";
            }
            if($("#ContentPlaceHolder1_txtValuateSoftware").val()!=""){
                valuateSoftware="计价软件："+$("#ContentPlaceHolder1_txtValuateSoftware").val()+ "    ";
            }
            if($("#ContentPlaceHolder1_ddlSpecialtyCategory").find("option:selected").text()!=""){
                specialty="专业："+$("#ContentPlaceHolder1_ddlSpecialtyCategory").find("option:selected").text();
            }

            $("#ContentPlaceHolder1_txtAssignmentBook").val(projectName+valuateMode+province+modelingSoftware+valuateSoftware+specialty);
        };
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
            <div id="container" class="container">
            <div style="display: none;">
                <asp:HiddenField ID="hidTaskType" runat="server" ClientIDMode="Static" />
                <!--任务类型-->
                <asp:HiddenField ID="hidProjectID" runat="server" ClientIDMode="Static" />
                <!--存储任务ID-->
                <asp:HiddenField ID="hidProjectOrModifyID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hidDeleteID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hidIsSuperAdmin" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hidFileReplaceId" runat="server"  ClientIDMode="Static"/>
                <asp:HiddenField ID="hidFileReplaceName" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hidDdlSpecialtySelectedValue" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hidDdlSpecialtyMinorSelectedValue" runat="server" ClientIDMode="Static" />
                <asp:Button ID="btnDelete" runat="server" CssClass="button" OnClick="btnDelete_Click" ClientIDMode="Static" />
                <!--任务删除-->
                <%--<asp:Button ID="btnModifyDelete" runat="server" OnClick="btnModifyDelete_Click"/>--%>
                <%--<asp:Button ID="btnDownload" runat="server" Text="下载" OnClick="btnDownload_Click" />--%>
                <asp:Button ID="btnFileReplace" runat="server" OnClick="btnFileReplace_Click" ClientIDMode="Static" />
            </div>
            <h1>任务<asp:Label ID="lblOperateType" runat="server" Text="生成"></asp:Label></h1>
            <hr />
            <div id="header" class="header">
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>完成期限：</label>
                    </div>
                    <asp:TextBox ID="txtExpireDate" runat="server" CssClass="Wdate" onFocus="WdatePicker({lang:'zh-cn',dateFmt:'yyyy-MM-dd HH'})" required="required" />
                    <span class="asterisk">*</span>
                </div>
                <div class="formSearch" style="width:260px; padding:0px 0px;">
                    <div class="formSearchLeft">
                        <label>下单时间：</label>
                    </div>
                    <asp:TextBox ID="txtOrderDate" runat="server" Width="130" required="required" CssClass="txtOrderDate" placeholder="可直接粘贴,快速选择请点击右侧图标" ToolTip="可直接粘贴，若需快速选择请点击右侧图标" />
                    <img onclick="WdatePicker({el:'ContentPlaceHolder1_txtOrderDate',lang:'zh-cn',dateFmt:'yyyy-MM-dd HH:mm:ss',errDealMode:0})" src="Scripts/My97DatePicker/skin/datePicker.gif" width="16" height="22" align="absmiddle"
                        title="单击选择日期时间" style="cursor:pointer;" />
                    <span class="asterisk">*</span>
                </div>
                <div class="formSearch" style="width:240px; padding:0px 0px; padding-right:10px;">
                    <div class="formSearchLeft">
                        <label>店铺：</label>
                    </div>
                    <asp:DropDownList ID="ddlShop" runat="server" required="required">
                    </asp:DropDownList>
                    <span class="asterisk">*</span>
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>金额：</label>
                    </div>
                    <asp:TextBox ID="txtOrderAmount" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="4" required="required" />
                    <span class="asterisk">*</span>
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>客户旺旺号：</label>
                    </div>
                    <asp:TextBox ID="txtWangwangName" runat="server" required="required" MaxLength="60" />
                    <span class="asterisk">*</span>
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft" style="width:80px;">
                        <label>专业类别：</label>
                    </div>
                    <asp:DropDownList ID="ddlSpecialtyCategory" runat="server" multiple="multiple" required="required" />
                    <%--<asp:DropDownList ID="ddlSpecialtyCategoryMinor" runat="server" multiple="multiple" required="required" Width="65" />--%>
                    <span class="asterisk">*</span>
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>所需时间：</label>
                    </div>
                    <asp:TextBox ID="txtTimeNeeded" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="3" Width="30" required="required" />
                    <asp:DropDownList ID="ddlTimeNeeded" runat="server" Width="55">
                        <asp:ListItem Value="H" Text="小时"></asp:ListItem>
                        <asp:ListItem Value="d" Text="天"></asp:ListItem>
                    </asp:DropDownList>
                    <span class="asterisk">*</span>
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft" style="width:96px;">
                        <label>资料是否上传：</label>
                    </div>
                    <asp:DropDownList ID="ddlMaterialIsUpload" runat="server" Width="120">
                        <asp:ListItem Text="未上传" Value="0"></asp:ListItem>
                        <asp:ListItem Text="已上传" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                    <span class="asterisk">*</span>
                </div>
                <img class="fulltext" src="Images/listdown.jpg" title="展开" />
                <div class="hidediv">
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>工程名称：</label>
                    </div>
                    <asp:TextBox ID="txtProjectName" runat="server" MaxLength="200" />
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>计价模式：</label>
                    </div>
                    <asp:DropDownList ID="ddlValuateMode" runat="server">
                    </asp:DropDownList>
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
                        <label>建模软件：</label>
                    </div>
                    <asp:DropDownList ID="ddlModelingSoftware" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>计价软件：</label>
                    </div>
                    <%--<asp:DropDownList ID="ddlValuateSoftware" runat="server" />--%>
                    <%--<div style="position:relative;">
                        <span style="margin-left:100px;width:18px;overflow:hidden;">
                        <asp:DropDownList ID="ddlValuateSoftware" runat="server" style="width:118px;margin-left:-100px" onchange="this.parentNode.nextSibling.value=this.value" AutoPostBack="false"> 
                        </asp:DropDownList>
                        </span>
                        <input name="box" type="text" style="width:100px; position:absolute;left:90px;"/>
                        </div>--%>
                    <asp:TextBox ID="txtValuateSoftware" runat="server" MaxLength="15" />
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>电子邮箱：</label>
                    </div>
                    <asp:TextBox ID="txtEmail" runat="server" type="email" MaxLength="100" />
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>层数：</label>
                    </div>
                    <asp:TextBox ID="txtFloors" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="3" />
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>建筑面积：</label>
                    </div>
                    <asp:TextBox ID="txtConstructionArea" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="8" Width="90" />
                    m&sup2;
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
                        <label>建筑类型：</label>
                    </div>
                    <asp:DropDownList ID="ddlBuildingType" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>交易状态：</label>
                    </div>
                    <asp:DropDownList ID="ddlTransactionStatus" runat="server" ClientIDMode="Static"></asp:DropDownList>
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>手机：</label>
                    </div>
                    <asp:TextBox ID="txtMobilePhone" runat="server" pattern="^1[3-9]\d{9}$" MaxLength="11" />
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>QQ：</label>
                    </div>
                    <asp:TextBox ID="txtQQ" runat="server" pattern="^[1-9][0-9]{4,9}$" MaxLength="10" />
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>支付方式：</label>
                    </div>
                    <asp:DropDownList ID="ddlPaymentMethod" runat="server"></asp:DropDownList>
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>完成人：</label>
                    </div>
                    <asp:TextBox ID="txtFinishedPerson" runat="server" ReadOnly="true" placeholder="无需录入" />
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>其他要求：</label>
                    </div>
                    <asp:TextBox ID="txtExtraRequirement" runat="server" />
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft" style="padding-right: 0px;">
                        <label>备注：</label>
                    </div>
                    <asp:TextBox ID="txtRemarks" runat="server" MaxLength="180" />
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>退款金额：</label>
                    </div>
                    <asp:TextBox ID="txtRefund" runat="server" ClientIDMode="Static" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="6" placeholder="&yen;" />
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>推荐人：</label>
                    </div>
                    <asp:TextBox ID="txtReferrer" runat="server" MaxLength="80" />
                </div>
                <div class="formSearch">
                    <div class="formSearchLeft">
                        <label>返现金额：</label>
                    </div>
                    <asp:TextBox ID="txtCashBack" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="4" />
                </div>
                <div class="formSearch" id="divSync">
                    <div class="formSearchLeft" style="width: 95px;">
                        <label>同步创建目录：</label>
                    </div>
                    <%--<asp:RadioButton ID="rdbSync" runat="server" Text="是" GroupName="sync" required="required" />
                    <asp:RadioButton ID="rdbNoSync" runat="server" Text="否" GroupName="sync" required="required" />--%>
                    <input id="rdbSync" type="radio" name="sync" value="rdbSync" /><label for="rdbSync">是</label>
                    <input id="rdbNoSync" type="radio" name="sync" value="rdbNoSync" /><label for="rdbNoSync">否</label>
                </div>
                <div class="formSearch" style="width: 500px;">
                    <div class="formSearchLeft">
                        <label>任务说明：</label>
                    </div>
                    <asp:TextBox ID="txtAssignmentBook" runat="server" TextMode="MultiLine" placeholder="请直接在此键入任务书内容，在“同步创建目录”选择“是”时，系统会同步生成任务书。" Width="380" Height="40" MaxLength="2200" />
                </div>
                <div class="formSearch" style="width:500px;">
                </div>
                <div class="formSearch" style="width: 150px;">
                    <asp:Button ID="btnSetIsFinished" runat="server" Text="置为完成" CssClass="button" Visible="false" OnClientClick="return confirm('确定要将此任务设置为 已完成 吗？')" OnClick="btnSetIsFinished_Click" />
                </div>
                    </div>
                <div class="formSearch" style="width: 350px; float:right;">
                    <asp:Button ID="btnGenerating" runat="server" Text="生成任务" CssClass="button" OnClick="btnGenerating_Click" ClientIDMode="Static" />
                    <input type="button" name="btnClearTextbox" onclick="ClearAllTextBox();" value="清空输入" class="button" />
                    <input type="button" id="btnDeleteClient" name="btnDeleteClient" value="删除任务" onclick="AlertConfirm();" class="button" />
                </div>
            </div>
            <div class="result">
                <label>任务编号：</label>
                <asp:TextBox ID="txtTaskName" runat="server" Text="生成的任务编号将在这里显示，请选中并复制。" ReadOnly="true" />
                <asp:Label ID="lblGenerateSuccess" runat="server" Visible="false" Text="&#10004" Font-Bold="true" Font-Size="X-Large" ForeColor="#00C600" />
                <input id="btnCopy" name="btnCopy" type="button" value="复制到剪贴板" data-clipboard-action="copy" data-clipboard-target="#txtTaskName" class="button" style="display:none;" />
            </div>
            <hr />
            <div id="middle">
                <!--============================== 任务资料 ==============================-->
                <div class="attach_bottom">
                    <div class="attach_title">
                        任务资料
                    </div>
                </div>
                <div class="rwzlList">
                    <asp:Label ID="lblTaskDataUploadTip" runat="server" Text="任务资料上传：" />
                    <%--<asp:FileUpload ID="fupTaskData" runat="server" ToolTip="上传任务资料" />--%>

                    <Upload:InputFile ID="InputFile1" runat="server" ClientIDMode="Static" />

                    <asp:Button ID="btnUploadTaskData" runat="server" Text="上传" OnClientClick="return ValidateUpload('InputFile1');" OnClick="btnUploadTaskData_Click" ToolTip="上传任务资料" ClientIDMode="Static" />
                    <br />
                    
                    <div id="progressbar">
                        <Upload:ProgressBar ID="ProgressBar1" runat="server" Inline="true" Width="580px" Height="30px" />
                    </div>
                    <asp:Image ID="imgTaskData" runat="server" ImageUrl="~/Images/folder.png" Visible="false" />
                    <asp:Label ID="lblTaskData" runat="server" Visible="false" />
                    <asp:Button ID="btnDeleteTaskData" runat="server" Text="删除" Visible="false" />
                    <div class="divGridView">
                        <asp:GridView ID="gvTaskData" runat="server" OnRowCommand="gvTaskData_RowCommand" AutoGenerateColumns="false" ShowHeader="false" GridLines="None" RowStyle-Height="30">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Image ID="img" runat="server" ImageUrl="~/Images/folder.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("FILENAME") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>' Text="删除" ForeColor="Red" OnClientClick="return confirm('确定要删除吗？');" />
                                        <%--<input id="btnDeleteTaskData" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>' value="删除" onclick="return confirm('确定要删除吗？');" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div>
                </div>
                <!--============================== 完成稿 ==============================-->
                <div class="attach_bottom">
                    <div class="attach_title">
                        完成稿
                    </div>
                    <div id="loadingProject">
                        <img src="Images/loading.gif" />
                        压缩中，请稍等...
                    </div>
                </div>
                <div class="rwzlList">
                    <%--<asp:Image ID="imgFinalData" runat="server" ImageUrl="~/Images/folder.png" Visible="false" />
                    <asp:Label ID="lblFinalData" runat="server" Text="暂无" Visible="false" />
                    <input type="button" runat="server" id="btnFinalDataCompress" name="btnCompress" value="压缩" onclick="Compress(0, null, null);" class="btnCompress" visible="false" />
                    <asp:Button ID="btnFinalDataDownload" runat="server" Text="下载" OnClick="btnFinalDataDownload_Click" CssClass="btnDownload" Enabled="false" ToolTip="请压缩后再下载" Visible="false" />--%>
                    <div class="divGridView">
                        <asp:GridView ID="gvFinalData" runat="server" OnRowCommand="gvTaskData_RowCommand" AutoGenerateColumns="false" ShowHeader="false" GridLines="None" RowStyle-Height="30">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Image ID="img" runat="server" ImageUrl="~/Images/folder.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("TASKNO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmployeeNo" runat="server" Text='<%# "　（" + Eval("EMPLOYEENO") + "） " %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <input type="button" id="btnCompress" name="btnCompress" value="压缩" onclick="Compress(0, '<%# Eval("ID") %>    ', null, '<%# Eval("FINISHEDPERSON")%>' );" />
                                        <asp:Button ID="btnDownload" runat="server" Text="下载" CommandName="download" CommandArgument='<%# Eval("ID") + "|" + Eval("FINISHEDPERSON") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <label>暂无</label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
                <div style="width: 50%; height: 40px; display: none;">
                    <%--<label id="progress"></label>--%>
                    <%--<div id="progressbar"></div>--%>
                    <div class="divFinalScriptLabel">
                        <asp:Image ID="imgFinalFolder" ImageUrl="~/Images/folder.png" runat="server" Visible="true" />
                        <asp:Label ID="lblFinalScript" runat="server" Text="任务暂未完成" />
                        <%--<label>请先压缩再下载。</label>--%>
                        <%--<asp:Button ID="btnCompress" runat="server" Text="压缩" OnClick="btnCompress_Click" />--%>
                        <div id="divProject" runat="server">&nbsp;</div>
                    </div>
                    <%--<input type="button" id="btnProjectCompress" name="btnProjectCompress" value="压缩" onclick="Compress();" />
                    <input type="button" id="btnProjectDownload" name="btnProjectDownload" value="下载" />--%>
                    <%--后台生成“压缩”和“下载”按钮--%>
                    <%--<div style="clear:both;"></div>--%>
                </div>

                <!--============================== 修改稿 ==============================-->
                <div class="attach_bottom">
                    <div class="attach_title" style="clear: left;">
                        修改稿
                    </div>
                </div>
                <div class="rwzlList">
                    <div>
                        <asp:Button ID="btnCreateModifyTask" runat="server" Text="创建修改任务" OnClick="btnCreateModifyTask_Click" />
                    </div>
                    <!--用GridView绑定修改稿列表-->
                    <asp:GridView ID="gvModify" runat="server" OnRowCommand="gvTaskData_RowCommand" AutoGenerateColumns="false" ShowHeader="false" GridLines="None">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="img" runat="server" ImageUrl="~/Images/folder.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="80">
                                <ItemTemplate>
                                    <asp:Label ID="lbl" runat="server" Text='<%# Eval("FOLDERNAME").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblIsUploadAttach" runat="server" Text='<%# Eval("ISUPLOADATTACH").ToString() == "1" ? "（附件已上传）" : "（附件未上传）" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblReviewStatus" runat="server" Text='<%# Eval("REVIEWSTATUS").ToString() == "1" ? "审核通过" : "待审核" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="240">
                                <ItemTemplate>
                                    <span class="file" style="vertical-align: middle; margin-bottom: 3px; display: <%# Eval("REVIEWSTATUS").ToString() == "1" ? "none" : "inline-block" %>">上传/替换
                                            <input type="file" name="fileReplace" onchange="FileReplace('<%# Eval("id") %>',this.value)" />
                                    </span>
                                    <%--<asp:Button ID="btnUpload" runat="server" Text="上传" CommandName="upload" CommandArgument='<%# Eval("ID") %>' />--%>
                                    <%--<asp:Button ID="btnDelete" runat="server" Text="删除" CommandName="del" CommandArgument='<%# Eval("ID") %>' />--%>
                                    <input type="button" id="btnCompress" name="btnCompress" value="压缩" onclick="Compress(1, '<%# Eval("ID") %>    ', null);" />
                                    <asp:Button ID="gvBtnDownload" runat="server" Text="下载" CommandName="download" CommandArgument='<%# Eval("ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <label>暂无</label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <%--                    <asp:GridView ID="gvModifyAttach" runat="server" OnRowCommand="gvTaskData_RowCommand" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="img" runat="server" ImageUrl="~/Images/folder.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lbl" runat="server" Text='<%# Eval("FILENAME") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnDelete" runat="server" Text="删除" CommandName="del" CommandArgument='<%# Eval("ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>--%>
                    <div style="display: none;">
                        <label>修改任务上传：</label>
                        <asp:FileUpload ID="fupModifyData" runat="server" ToolTip="上传修改资料" />
                        <asp:Button ID="btnUploadModifyData" runat="server" Text="上传" OnClientClick="return ValidateUpload('fupModifyData');" OnClick="btnUploadTaskData_Click" ToolTip="上传修改资料" />
                    </div>
                </div>
                <div id="divModifyList" runat="server" style="clear: left;"></div>
                <div class="modifyTaskAdd" style="display: none;">
                    <s>修改任务添加：
                <asp:FileUpload ID="fupd" runat="server" />
                        <asp:Button ID="btnAddModifyTask" runat="server" Text="添加" OnClientClick="return ValidateUpload(this);" OnClick="btnAddModifyTask_Click" /></s>
                </div>
            </div>
            <div id="bottom">&copy;<label id="lblCurrentYear" style="font-size: 12px;"></label>
                Soochow YiLiangYiJia Enterprise Consulting Management Co.,Ltd. All rights reserved.&nbsp;Please use a browser except IE.</div>
        </div>
  </form>
</asp:Content>