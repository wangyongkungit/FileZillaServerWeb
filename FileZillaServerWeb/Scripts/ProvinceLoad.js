
//省份，城市 
var provinceDom = null;
function InItDom(fname) {
    var tempdom;
    //try {
    //    tempdom = new ActiveXObject("Microsoft.XMLDOM");
    //} catch (e) {
    //    try {
    //        //tempdom = document.implementation.createDocument("", "", null);
    //        //var oXmlHttp = new XMLHttpRequest();
    //        //oXmlHttp.open("GET", fname, false);
    //        //oXmlHttp.send(null);
    //        //tempdom = oXmlHttp.responseXML;

    //    }
    //    catch (e) {
    //        alert(e.message);
    //    }
    //}
    //try {
    //    tempdom.async = false;
    //    tempdom.load(fname);
    //    //alert(tempdom.childNodes.length); 
    //}
    //catch (e) {
    //}
        //var xmlDom = null;
        //if (window.ActiveXObject) {
        //    xmlDom = new ActiveXObject("Microsoft.XMLDOM");
        //    //xmlDom.loadXML(xmlFile);//如果用的是XML字符串  
        //    xmlDom.load(fname); //如果用的是xml文件。  
        //} else if (document.implementation && document.implementation.createDocument) {
        //    var xmlhttp = new window.XMLHttpRequest();
        //    xmlhttp.open("GET", fname, false);
        //    xmlhttp.send(null);
        //    xmlDom = xmlhttp.responseXML.documentElement;//一定要有根节点(否则google浏览器读取不了)  
        //} else {
        //    xmlDom = null;
        //}
        //return xmlDom;
    //return tempdom;
    var xmlDoc = null;
    var agent = navigator.userAgent.toLowerCase();

    //判断浏览器的类型  
    //支持IE浏览器  
    if (agent.indexOf("msie") > 0) {
        alert("22");
        var xmlDomVersions = ['MSXML.2.DOMDocument.6.0', 'MSXML.2.DOMDocument.3.0', 'Microsoft.XMLDOM'];
        for (var i = 0; i < xmlDomVersions.length; i++) {
            try {
                xmlDoc = new ActiveXObject(xmlDomVersions[i]);
                break;
            } catch (e) {
            }
        }
    }
        //支持firefox浏览器  
    else if (agent.indexOf("firefox") > 0) {
        try {
            xmlDoc = document.implementation.createDocument('', '', null);
        } catch (e) {
        }
    } else {//谷歌浏览器  
        alert("111111111");
        //var oXmlHttp = new XMLHttpRequest();
        //oXmlHttp.open("GET", fname, false);
        //oXmlHttp.send(null);
        //xmlDoc = oXmlHttp.responseXML;
        //return oXmlHttp.responseXML;
        var parser = new DOMParser;
        xmlDoc = parser.parseFromString(fname, "text/xml");
        //return xmlDom;
    }
    if (xmlDoc != null) {
        xmlDoc.async = false;
        xmlDoc.load(fname);
    }
    return xmlDoc;
}
function parseXml(fileName) {
    try {//Internet Explorer    
        xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
        xmlDoc.async = "false";
        //加载 XML文档,获取XML文档对象  
        xmlDoc.load(fileName);
    } catch (e) {//Firefox, Mozilla, Opera, etc.    
        try {
            xmlDoc = document.implementation.createDocument("", "", null);
            xmlDoc.async = "false";
            //加载 XML文档,获取XML文档对象  
            xmlDoc.load(fileName);
        } catch (e) {
            try {//Google Chrome    
                var xmlhttp = new window.XMLHttpRequest();
                xmlhttp.open("get", fileName, false);
                xmlhttp.send(null);
                xmlDoc = xmlhttp.responseXML.documentElement;
            } catch (e) {
                alert("您的浏览器不能正常加载文件。请切换到兼容模式，或者更换浏览器");
            }
        }
    }
    return xmlDoc;
}
function InitProvince(provinceid) {
    var province = document.getElementById(provinceid);
    province.length = 0;
    if (provinceDom == null)
        provinceDom = parseXml("../config/Province.xml");        //-----------------------------------------
    if (provinceDom != null) {
        var proNodes = provinceDom.childNodes[1].childNodes;
        alert(proNodes.length); 
        for (var i = 0; i < proNodes.length; i++) {
            var tempOption = document.createElement("option");
            var agent = navigator.userAgent.toLowerCase();
            if (agent.indexOf("msie") > 0) {
                tempOption.value = proNodes[i].getAttribute("Name");
                tempOption.text = proNodes[i].getAttribute("Name");
                province.options.add(tempOption);
            }
            else if (agent.indexOf("firefox") > 0) {
                tempOption.value = proNodes[i].getElementsByTagName_r("Name");
                tempOption.text = proNodes[i].getElementsByTagName_r("Name");
            }
            else {
                tempOption.value = proNodes[i].text;
                tempOption.text = proNodes[i].text;
            }
            province.options.add(tempOption);
        }
        alert(proNodes[1].getAttribute("Name")); 
    }
}
function ResetCity(province, cityname) {
    var pname = province.value;
    var city = document.getElementById(cityname);
    city.length = 0;
    if (provinceDom == null)
        provinceDom = parseXml("../config/Province.xml");       //-----------------------------------------
    if (provinceDom != null) {
        // alert(provinceDom.childNodes[1].childNodes.length); 
        var root = provinceDom.selectNodes("Root")[0];
        //Nodes = objXMLDoc.selectNodes("test/test1/test1"); 
        // alert(root.childNodes.length); 
        for (var i = 0; i < root.childNodes.length; i++) {
            if (root.childNodes[i].getAttribute("Name") == pname) {
                for (var j = 0; j < root.childNodes[i].childNodes.length; j++) {
                    var tempOption = document.createElement("option");
                    tempOption.value = root.childNodes[i].childNodes[j].text;
                    tempOption.text = root.childNodes[i].childNodes[j].text;
                    city.options.add(tempOption);

                }
                break;
            }
        }
    }
}