<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeAdd.aspx.cs" Inherits="FileZillaServerWeb.EmployeeAdd" %>

<%@ Register Src="~/UserControl/SiteMenu.ascx" TagPrefix="uc1" TagName="SiteMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/themes/base/ylyj/employeeadd.css?v=18426" rel="stylesheet" />
    <script src="/Scripts/ylyj/employeeadd.js?v=18426"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="container">
        <form id="form1" runat="server">
            <div style="width:200px; height: 30px; float:right; position:absolute; right:50px; top: 50px;">
                <a href="javascript:void()" onclick="location.href='/pages/admin/employeeadd.aspx'">刷新</a>
            </div>
            <h1>员工<asp:Label ID="lblNewOrEdit" runat="server" Text="添加"></asp:Label></h1>
            <div class="empadd">
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>用户类型：</label>
                    </div>
                    <asp:RadioButton ID="rdbGMO" runat="server" Text="总经办" GroupName="userType" AutoPostBack="true" OnCheckedChanged="rdbUserType_CheckedChanged" required="required" />
                    <asp:RadioButton ID="rdbCustomerService" runat="server" Text="客服" GroupName="userType" AutoPostBack="true" OnCheckedChanged="rdbUserType_CheckedChanged" required="required" />
                    <asp:RadioButton ID="rdbEmployee" runat="server" Text="造价员" GroupName="userType" AutoPostBack="true" OnCheckedChanged="rdbUserType_CheckedChanged" required="required" />
                    <asp:RadioButton ID="rdbOtherUserType" runat="server" Text="其他" GroupName="userType" AutoPostBack="true" OnCheckedChanged="rdbUserType_CheckedChanged" required="required" />                   
                    <span class="asterisk">*</span>
                </div>
                <div id="divOtherUserType" runat="server" visible="false" class="valGroup">
                    <div class="groupLeft">
                        <label>&nbsp;</label>
                    </div>
                    <asp:TextBox ID="txtUserType" runat="server" Visible="false" MaxLength="1" placeholder="指定一个数值类型，1，客服；2，工程师" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" required="required"></asp:TextBox>
                    <span class="asterisk">*</span>
                </div>
                <div class="valGroup" runat="server" id="divNoPrefix">
                    <div class="groupLeft">
                        <label>编号前缀：</label>
                    </div>
                    <asp:TextBox ID="txtUserPrefix" runat="server" MaxLength="1" CssClass="txtUpper" required="required" OnTextChanged="txtUserPrefix_TextChanged" AutoPostBack="true" placeholder="员工编号前缀" ToolTip="输入完成且光标离开此文本框会自动生成编号"></asp:TextBox>
                    <span class="asterisk">*</span>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>编号：</label>
                    </div>
                    <asp:TextBox ID="txtEmployeeNo" runat="server" required="required" />
                    <span class="asterisk">*</span>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>姓名：</label>
                    </div>
                    <asp:TextBox ID="txtName" runat="server" required="required" />
                    <span class="asterisk">*</span>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>性别：</label>
                    </div>
                    <asp:RadioButton ID="rdbMale" runat="server" Text="男" GroupName="gender" Width="50" />
                    <asp:RadioButton ID="rdbFemale" runat="server" Text="女" GroupName="gender" Width="50" />
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>出生日期：</label>
                    </div>
                    <asp:TextBox ID="txtBirthDate" runat="server" CssClass="Wdate" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:new Date()})"/>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>手机号码：</label>
                    </div>
                    <asp:TextBox ID="txtMobilePhone" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" MaxLength="11" required="required" />
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>电子邮箱：</label>
                    </div>
                    <asp:TextBox ID="txtEmail" runat="server" type="email" />
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>转正日期：</label>
                    </div>
                    <asp:TextBox ID="txtToRegularDate" runat="server" CssClass="Wdate" onFocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:new Date()})" required="required" ></asp:TextBox>
                    <span class="asterisk">*</span>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>是否分部领导：</label>
                    </div>
                    <asp:RadioButtonList ID="rblIsBranchLeader" runat="server" RepeatDirection="Horizontal" required="required">
                        <asp:ListItem Value="0" Text="否"></asp:ListItem>
                        <asp:ListItem Value="1" Text="是"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>是否离职：</label>
                    </div>
                    <asp:RadioButtonList ID="rblIsDisable" runat="server" RepeatDirection="Horizontal" required="required">
                        <asp:ListItem Value="1" Text="否"></asp:ListItem>
                        <asp:ListItem Value="0" Text="是"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>是否外部员工：</label>
                    </div>
                    <asp:RadioButtonList ID="rblIsExternal" runat="server" RepeatDirection="Horizontal" required="required">
                        <asp:ListItem Value="0" Text="否"></asp:ListItem>
                        <asp:ListItem Value="1" Text="是"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>钉钉同步创建：</label>
                    </div>
                    <input id="rdbSync" type="radio" name="sync" value="rdbSync" required="required" checked="checked" /><label for="rdbSync">是</label>
                    <input id="rdbNoSync" type="radio" name="sync" value="rdbNoSync" required="required" /><label for="rdbNoSync">否</label>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                        <label>同步添加角色：</label>
                    </div>
                    <%--<input id="rdbAddRole" type="radio" name="addrole" value="addRole" required="required" checked="checked" /><label for="rdbAddRole">是</label>
                    <input id="rdbNoAddRole" type="radio" name="addrole" value="noAddRole" required="required" /><label for="rdbNoAddRole">否</label>--%>
                    <asp:RadioButtonList ID="rblAddRole" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1" Text="是"></asp:ListItem>
                        <asp:ListItem Value="0" Text="否"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                    </div>
                    <asp:Button ID="btnOK" runat="server" Text="添加" OnClick="btnOK_Click" />
                </div>
                <div class="valGroup" style="border-top:solid 2px #ff6a00">
                    <div class="groupLeft">
                        <label>钉钉UserId：</label>
                    </div>
                    <asp:TextBox ID="txtDingtalkUserId" runat="server" ClientIDMode="Static" />
                </div>
                <div class="valGroup">
                    <div class="groupLeft">
                    </div>
                    <asp:Button ID="btnAddDingtalkUserId" runat="server" Text="关联钉钉UserId" OnClientClick="return CheckInputDdId();" OnClick="btnAddDingtalkUserId_Click" />
                    <asp:Button ID="btnInvokeWebService" runat="server" Text="Invoke" OnClick="btnInvokeWebService_Click" Visible="false" />
                </div>
            </div>
            <uc1:SiteMenu runat="server" ID="SiteMenu" />
        </form>
    </div>
</asp:Content>
