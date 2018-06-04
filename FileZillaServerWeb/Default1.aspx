<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        html, body
        {
            margin: 0;
            padding: 0;
        }

        .chartContainer
        {
            width: 250px;
            height: 190px;
            border: 1px solid pink;
            /*box-sizing: border-box;*/
        }
        input[type='button'],input[type='submit'],.mya
        {
            color:#000;
            border:2px solid #cf5555;
            background-color:#fff;
            font-size:12px;
            font-family:Consolas,sans-serif;
            padding:8px 10px;
            text-align:center;
            display:inline-block;
            border-radius:5px;
            text-decoration:none;
            cursor:pointer;
            transition-duration:0.3s;
            /*opacity:0.6;*/
        }
        input[type='button']:hover, input[type='submit']:hover, .mya:hover
        {
            color:#fff;
            background-color:#cf5555;
        }
        input[type='text']
        {
            border:0;
            border-bottom:1px solid #5f5e5e;
            outline:none;
            padding:4px 4px;
            font-family:'Consolas','Microsoft YaHei','sans-serif';
        }
        input[type='text']:focus
        {
            border:0;
            border-bottom:1px solid #cf5555;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="total">
            <div style="width: 100%; height: auto;">
                <asp:FileUpload ID="fup" runat="server" />
                <asp:Button ID="btnSave" runat="server" Text="Save, Show doPostBack Parameters" />
                <asp:LinkButton ID="lbtnTest" runat="server" Text="Do WebServer Method (Post Back)" CssClass="mya" />
                <input id="btn001" onclick="myDoPostBack();" type="button" value="Invoke Ajax Request" />
                <input id="btn002" onclick="" type="button" value="Get All Student" />
            </div>
            <div style="padding:10px 10px;">
                <div>
                    <asp:Label ID="lblTest" runat="server"></asp:Label>
                    <asp:GridView ID="gvFiles" runat="server"></asp:GridView>
                    <label>Please input score:</label>
                    <asp:TextBox ID="txt001" runat="server" Width="30px"></asp:TextBox>
                </div>
            </div>
            <!-- echarts.js -->
            <div class="chartContainer" id="chart1">
            </div>

            <div class="chartContainer1" id="chart2">

            </div>
            类型：
            <select>
                <option>修改完成</option>
            </select>
            &nbsp;
            <select>
                <option>修改1</option>
            </select>
        </div>
        <script type="text/javascript">
            //window.onload = function () {
            //    //doPostBack = null;
            //    //theForm.onsubmit() = false;
            //    __doPostBack = function () { alert("dpb02"); }
            //}
            //function doPostBack() {
            //    alert("dpb");
            //}
        </script>
    </form>
</body>
    
    <script src="../Scripts/jquery-1.7.1.min.js"></script>
    <%--<script type="text/javascript" src="http://echarts.baidu.com/dist/echarts.simple.min.js"></script>--%>
    <script src="../Scripts/echarts.js?v=1423"></script>
    <script type="text/javascript">
        var myData01 = '[{ "value": "573", "name": "剩余" }, { "value": "402", "name": "已发" },{ "value": "87", "name": "奖罚" }, { "value": "24", "name": "其他" } ]';
        var myData02 = JSON.parse(myData01);

        var checkIE = function () {
            var DEFAULT_VERSION = 8.0;
            var ua = navigator.userAgent.toLowerCase();
            var isIE = ua.indexOf("msie") > -1;
            var safariVersion;
            if (isIE) {
                safariVersion = ua.match(/msie ([\d.]+)/)[1];
            }
            if (safariVersion <= DEFAULT_VERSION) {
                return false;
            };
            return true;
        }
        //function GetAllStudent() {
        //    $.ajax({
        //        url: "/ylyj/fileUpload.aspx/GetAll",
        //        type: "get",
        //        contentType:"application/json;
        //    })
        //}
        function myDoPostBack() {
            $.ajax({
                url: "/ylyj/fileUpload.aspx/GetStudent",
                type: "Post",
                contentType: "application/json; charset=utf-8", //注意：WebMethod()必须加这项，否则客户端数据不会传到服务端
                dataType: "json",  //请求到服务器返回的数据类型
                data: {},

                success: function (result) {
                    //console.info(result);
                    var obj = $.parseJSON(result.d); //这个数据，不知道为什么 .NET 放到了 d 这个属性里，所以 result.d才是真正需要的返回数据
                    //console.info(obj);
                    var obj2 = obj.mylist;  // mylist 里存放了对象集合

                    var name = obj2[0].firstName;
                    var age = obj2[0].lastName;
                    var name2 = obj2[1].firstName;
                    var age2 = obj2[1].lastName;
                    console.info(name);
                    console.info(age);
                    console.log(name2);
                    console.log(age2);

                    //document.getElementById("name").innerHTML = name;  
                    //document.getElementById("age").innerHTML = age;  
                },
                error: function (data) {
                    console.log(data);
                }
            })
        }
        var LoadEcharts = function () {
            if (!checkIE()) {
                $(".total").css("display", "none");
                $("body").css("background-color", "#777777");
                alert("浏览器版本过低，请使用内核版本 IE9 及以上的浏览器或者更换其他浏览器访问此页面！");
                return;
            }
            var myChart = echarts.init(document.getElementById("chart1"));//   document.getElementById("chart1"));
            //var s_data = ["已发", "余额", "转账", "借出"];
            option = {
                tooltip: {
                    trigger: "item",
                    formatter: "{b}：￥{c}"
                },
                legend: {
                    x: "center",
                    y: "bottom",
                    data: ["剩余", "已发", "奖罚", "其他"]
                },

                series: {
                    type: "pie",
                    center: ["50%", "40%"],
                    radius: ['40%', '65%'],
                    label: {
                        normal: {
                            show: true,
                            position: "inner",
                            formatter: "{c}"
                        }
                    },
                    selectedMode: "single",
                    data: myData02
                    //    [
                    //    { value: 573, name: "剩余" },
                    //    { value: 402, name: "已发" },
                    //    { value: 87, name: "奖罚" },
                    //    { value: 24, name: "其他" }
                    //]
                }

                //title: {
                //    text: '我的账本'
                //},
                //tooltip: {},
                //legend: {
                //    data: ['销量']
                //},
                //xAxis: {
                //    data: ["衬衫", "羊毛衫", "雪纺衫", "裤子", "高跟鞋", "袜子"]
                //},
                //yAxis: {},
                //series: [{
                //    name: '销量',
                //    type: 'bar',
                //    data: [5, 20, 36, 10, 10, 20]
                //}]
            };
            myChart.setOption(option);
            myChart.on('click', function (param) {
                var index = param.dataIndex;
                alert(index);
            });
        }
        $(document).ready(function () {
            //LoadEcharts();
        });
    </script>
</html>