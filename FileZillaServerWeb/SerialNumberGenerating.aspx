<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SerialNumberGenerating.aspx.cs" Inherits="FileZillaServerWeb.SerialNumberGenerating" %>
<%@ Register assembly="Brettle.Web.NeatUpload" namespace="Brettle.Web.NeatUpload" tagprefix="Upload" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
        

    <link href="Scripts/jQuery-UI/jquery-ui.css" rel="stylesheet" />
    <link href="Scripts/jQuery-UI/jquery.multiselect.css" rel="stylesheet" />
    <link href="Scripts/webuploader/webuploader.css?v=180610" rel="stylesheet" />   
    
    <script src="Scripts/jQuery-UI/jquery-ui.min.js"></script>
    <script src="Scripts/jQuery-UI/jquery.multiselect.js"></script>

    <script src="Scripts/ylyj/serialnumbergenerating.js?v=1860902"></script>
    
    <link href="Scripts/bootstrap4/css/bootstrap.css" rel="stylesheet" />    
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
                height: 540
            });
            $(".ui-corner-all").css("top", "140px");

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
                <asp:HiddenField ID="hidProjectID2" runat="server" ClientIDMode="Static" />
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
                <%--<asp:Button ID="btnFileReplace" runat="server" OnClick="btnFileReplace_Click" ClientIDMode="Static" />--%>
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
            <div class="result" style="width:auto; margin: 0 auto; text-align:center;" id="divAssign" runat="server" visible="false">
                <div>
                    <label>分配给：</label>
                    <asp:DropDownList ID="ddlAssignTo" runat="server" OnSelectedIndexChanged="ddlAssignTo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    <asp:TextBox ID="txtDefaultProportion" runat="server" Width="80" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtNewProportion" runat="server" Width="80"></asp:TextBox>
                    <asp:Button ID="btnAssign" runat="server" Text="分配" OnClick="btnAssign_Click" CssClass="button"/>
                </div>
            </div>
            <hr />

            <div id="bottom">&copy;<label id="lblCurrentYear" style="font-size: 12px;"></label>
                Soochow YiLiangYiJia Enterprise Consulting Management Co.,Ltd. All rights reserved.&nbsp;Please use a browser except IE.</div>
        </div>
  </form>


    <div id="project" class="container" style="clear:both;">
     <%--<p>{{taskno}}</p>--%>
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
            <!-- Tab List -->
            <div class="row">
                <div class="col -12">
                    <div id="divFileTabs" class="btn-group btn-group-sm">
                        <!-- change file list -->
                        <button :key="item.Id" :title="item.description" :filetype="item.Id" class="btn btn-default btn-primary " v-for="item in projectfile.filetabs"
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
                                <tr v-for=" file in projectfile.files" v-show="projectfile.parentId == file.categoryId">
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
                                            <%--<button type="button" class="btn btn-default btn-success">下载</button>--%>
                                            <a :href="'HttpHandler/FileHandler.ashx?FuncName=DownloadFile&fileHistoryId='+file.fileHistoryId" class="btn btn-success">下载</a>
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
            添加一个新目录
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

                        <div class="form-group" v-show="newtab.categoryselected == 3">
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
    
<%--    <script src="Scripts/clipboard.min.js?v=18426"></script>--%>
    <%--<script src="http://cdn.bootcss.com/jqueryui/1.11.0/jquery-ui.min.js"></script>--%>

    <script src="Scripts/bootstrap4/js/bootstrap.js"></script>

    <link href="Content/themes/base/ylyj/serialnumber.css?v=18426" rel="stylesheet" />

    <script src="Scripts/vue/vue.js"></script>
    <script src="Scripts/ylyj/employeehome/func.js"></script>
    <script src="Scripts/ylyj/employeehome/settings.js"></script>
    <script src="Scripts/ylyj/employeehome/vuepage.js"></script>    

    <script src="Scripts/webuploader/webuploader.js"></script>
    <script src="Scripts/ylyj/employeehome/uploadfile.js"></script>
</asp:Content>