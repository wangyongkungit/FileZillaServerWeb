<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialConfig.aspx.cs" Inherits="FileZillaServerWeb.MaterialConfig" %>
<%@ Import Namespace="System.IO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Content/themes/base/ylyj/materialConfig.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:FileUpload ID="fup" runat="server" />
            <label>证件名称：</label>
            <asp:TextBox ID="txtCerficateName" runat="server"></asp:TextBox>
            <label>证件描述：</label>
            <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
            <label>设为主图：</label>
            <asp:CheckBox ID="cbxIsMain" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="上传添加" OnClick="btnUpload_Click" />
        </div>
        <div class="cerficateList">
            <% if (lstCerficate != null && lstCerficate.Count > 0)
               {
                   foreach (var cerficate in lstCerficate)
                   { %>
               <div class="cerficateList item">
                   <img src='<%= Convert.ToString(ConfigurationManager.AppSettings["fileSavePath"]) + "/" + cerficate.FILEPATH %>' title="<%= cerficate.DESCRIPTION %>" style="width:800px; height:600px;" />
                   <%--<span style="display:table-cell;vertical-align:middle;"><%= cerficate.DESCRIPTION %></span>--%>
               </div>
               <%}
               }
                 %>
        </div>
    </div>
    </form>
</body>
</html>
