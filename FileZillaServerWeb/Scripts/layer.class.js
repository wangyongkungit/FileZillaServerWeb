/************************************* layer.class Javascript Library  *************************** 
* Using jQuery 1.7.1 
* Version : 1.0.0 
* Name : layer.class.js 
* Create by Angle.Yang on 2012/03/07 
*******************************************************************************************/

$.extend({
    layer: {
        name: "layer.class.js",
        globalVar: {}, // 内部变量， 外部不得使用(document.body 未初始化时使用；内部变量)  

        setMaskTitle: function (title) {
            /// <summary>  
            /// 修改遮罩层的内容 Angle.Yang 2012.03.07 16:35 Add  
            /// </summary>  
            /// <param name="title" type="string">遮罩层中的提示信息</param>  
            /// <returns type="void" />  
            $.fn.masklayer({ title: title, action: "setTitle" });
        },

        openMask: function (title) {
            /// <summary>  
            /// 显示遮罩层DIV Angle.Yang 2012.03.07 16:35 Add  
            /// </summary>  
            /// <param name="title" type="string">遮罩层中的提示信息</param>  
            /// <returns type="void" />  
            $.fn.masklayer({ title: title, action: "open" });
        },

        closeMask: function () {
            /// <summary>  
            /// 关闭遮罩层DIV Angle.Yang 2012.03.07 16:35 Add  
            /// </summary>  
            /// <returns type="void" />  
            $.fn.masklayer({ action: "close" });
        }

    }
});

cks.using("kits/ck.layer.js");