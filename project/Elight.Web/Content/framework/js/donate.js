/*!
 * Title: 捐赠小插件
 * Version: 1.0.0
 * Author: Gao Yang 
 * Github: https://github.com/esofar
 */
layui.define(['layer'], function (exports) {
    "use strict";

    var $ = layui.jquery;
    var layer = layui.layer;

    var Donate = function () {
        this.default = {
            title: '感谢您的支持，我会继续努力',
            elem: null,
            wechat_url: null,
            alipay_url: null
        }
    }

    Donate.prototype.version = '1.0.0';

    Donate.prototype.init = function (options) {

        $.extend(true, this.default, options);

        if (this.default.elem === null) {
            throwError('Donate Error:请配置触发节点参数elem!');
        }
        if (this.default.wechat_url === null) {
            throwError('Donate Error:请配置微信二维码参数w_pay_url!');
        }
        if (this.default.alipay_url === null) {
            throwError('Donate Error:请配置支付宝二维码参数z_pay_url!');
        }
        this.render();
    }

    Donate.prototype.render = function () {

        var html, config = this.default;
        html = "<div class=\"donate-box\">";
        html += "   <div class=\"donate-btn-close\">+</div>";
        html += "   <h2>" + config.title + "</h2>";
        html += "   <div class=\"wechat\">";
        html += "       <img src=\"" + config.wechat_url + "\" alt=\"微信支付\">";
        html += "   </div>";
        html += "   <div class=\"zhifubao\">";
        html += "       <img src=\"" + config.alipay_url + "\" alt=\"支付宝支付\">";
        html += "   </div>";
        html += "</div>";

        $('body').append(html);

        $(config.elem).on('click', function () {
            $(".donate-box").css('display', 'block');
        });

        $('.donate-btn-close').on('click', function () {
            $(".donate-box").css('display', 'none');
        });

        loadStyles(".donate-box{display:none;z-index:9998;position:fixed;top:50%;left:50%;transform:translate(-50%,-50%);width:600px;height:360px;border-radius:0;background:#fff;box-shadow:0 0 0 2000px rgba(0,0,0,.5);border-radius:10px}\
                    .donate-box h2{text-align:center;font-size:24px;color:#666;font-weight:normal;margin:22px 0}\
                    .donate-box .wechat,.donate-box .zhifubao{position:relative;float:left;width:240px;height:240px;padding:10px;border:5px solid #ff7300;border-radius:10px;box-sizing:border-box}\
                    .donate-box .wechat img,.donate-box .zhifubao img{width:100%}\
                    .donate-box p{text-align:center}.wechat::after,.zhifubao::after{content:'微信，扫一扫';position:absolute;color:#666;background:#fff;top:220px;padding:0 10px;font-size:16px;width:120px;left:50px;text-align:center}\
                    .zhifubao::after{content:'支付宝，扫一扫'}\
                    .wechat{margin-left:40px}.zhifubao{margin-left:40px}\
                    .donate-btn-close{position:absolute;font-size:30px;color:#999;top:0;right:0;cursor:pointer;padding:10px 20px;transform:rotate(45deg)}\
                    .donate-btn-close:hover{transition:all .2s;color:#bbb}\
                    .donate-btn-close:active{transition:all .2s;color:#666}");
    }

    /**
     * 动态加载样式。
     * @param {String} str
     */
    function loadStyles(str) {
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
    }

    /**
     * 抛出异常错误信息。
     * @param {String} msg
     */
    function throwError(msg) {
        if (layer && layer != undefined) {
            layer.msg(msg, { icon: 5 });
        }
        throw new Error(msg);
        return;
    }

    var donate = new Donate();

    exports('donate', function (options) {
        return donate.init(options);
    });
});