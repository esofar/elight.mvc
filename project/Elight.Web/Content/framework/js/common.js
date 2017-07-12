/** common.js By Beginner Emain:zheng_jinfan@126.com HomePage:http://www.zhengjinfan.cn */
layui.define(['layer'], function (exports) {
    "use strict";

    var $ = layui.jquery,
		layer = layui.layer;

    var common = {

        /**
         * 动态加载内联样式。
         * @param {String} str
         */
        loadStyles: function (str) {
            loadStyles.mark = 'load';
            var style = document.createElement("style");
            style.type = "text/css";
            try {
                style.innerHTML = str;
            } catch (ex) {
                style.styleSheet.cssText = str;
            }
            var head = document.getElementsByTagName('head')[0];
            head.appendChild(style);
        },

        /**
         * 抛出一个异常错误信息。
         * @param {String} msg
         */
        throwError: function (msg) {
            if (layer && layer != undefined) {
                layer.msg(msg, { icon: 5 });
            }
            throw new Error(msg);
            return;
        }
    };
    exports('common', common);
});