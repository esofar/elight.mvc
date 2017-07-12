/// <reference path="jquery/jquery.min.js" />
/**
 * Paging 组件
 * @description 基于laytpl 、laypage、form、layer 封装的组件
 * @author Van zheng_jinfan@126.com
 * @link http://m.zhengjinfan.cn
 * @license MIT
 * @version 1.0.1
 *
 */
layui.define(['layer', 'laypage', 'form', 'laytpl'], function (exports) {
    "use strict";
    var $ = layui.jquery;
    var form = layui.form();
    var laytpl = layui.laytpl;
    var layer = parent.layui.layer || layui.layer;


    var Paging = function () {
        this.config = {
            url: undefined, //数据远程地址
            type: 'POST', //数据的获取方式  get or post
            elem: undefined, //内容容器
            params: null, //获取数据时传递的额外参数
            openWait: false,//加载数据时是否显示等待框 
            tempElem: undefined, //模板容器
            paged: true,
            checkbox: true, //是否显示复选框
            singleSelected: true, //单选
            pageConfig: { //参数应该为object类型
                elem: undefined,
                pageSize: 15 //分页大小
            },
            complate: undefined, //执行完成回调函数
            success: undefined, //执行成功回调函数
            fail: undefined, //执行失败回调函数
            serverError: function (xhr, status, error) { //ajax的服务错误
                throwError("错误提示： " + xhr.status + " " + xhr.statusText);
            }
        };
    };

    /**
     * 版本号
     */
    Paging.prototype.v = '1.0.1';

    /**
	 * 设置
	 * @param {Object} options
	 */
    Paging.prototype.set = function (options) {
        var that = this;
        $.extend(true, that.config, options);
        return that;
    };
    /**
	 * 初始化
	 * @param {Object} options
	 */
    Paging.prototype.init = function (options) {
        var that = this;
        $.extend(true, that.config, options);
        var _config = that.config;
        if (_config.url === undefined) {
            throwError('Paging Error:请配置远程URL!');
        }
        if (_config.elem === undefined) {
            throwError('Paging Error:请配置参数elem!');
        }
        if ($(_config.elem).length === 0) {
            throwError('Paging Error:找不到配置的容器elem!');
        }
        if (_config.tempElem === undefined) {
            throwError('Paging Error:请配置参数tempElem!');
        }
        if ($(_config.tempElem).length === 0) {
            throwError('Paging Error:找不到配置的容器tempElem!');
        }
        if (_config.paged) {
            var _pageConfig = _config.pageConfig;
            if (_pageConfig.elem === undefined) {
                throwError('Paging Error:请配置参数pageConfig.elem!');
            }
        }
        if (_config.type.toUpperCase() !== 'GET' && _config.type.toUpperCase() !== 'POST') {
            throwError('Paging Error:type参数配置出错，只支持GET或都POST');
        }
        that.get({
            pageIndex: 1,
            pageSize: _config.pageConfig.pageSize
        });

        return that;
    };
    /**
	 * 获取数据
	 * @param {Object} options
	 */
    Paging.prototype.get = function (options) {
        var that = this;
        var _config = that.config;
        var loadIndex = undefined;
        if (_config.openWait) {
            loadIndex = layer.load();
        }
        //分页默认参数
        var df = {
            pageIndex: 1,
            pageSize: _config.pageConfig.pageSize
        };
        $.extend(true, _config.params, df, options);
        $.ajax({
            url: _config.url,
            data: _config.params,
            type: _config.type,
            async: false,
            dataType: 'json',
            success: function (result, status, xhr) {
                if (result.result == true) {
                    //获取模板
                    var tpl = $(_config.tempElem).html();
                    //渲染数据
                    laytpl(tpl).render(result, function (html) {
                        $(_config.elem).html(html);
                    });
                    if (_config.paged) {
                        if (result.count == null || result.count == undefined) {
                            throwError('Paging Error:请返回数据总数！');
                            return;
                        }

                        var _pageConfig = _config.pageConfig;
                        var pageSize = _pageConfig.pageSize;
                        var pages = result.count % pageSize == 0 ? (result.count / pageSize) : (result.count / pageSize + 1);

                        var defaults = {
                            cont: $(_pageConfig.elem),
                            curr: _config.params.pageIndex,
                            skin: '#1E9FFF',
                            pages: pages,
                            skip: true,
                            jump: function (obj, first) {
                                //得到了当前页，用于向服务端请求对应数据
                                var curr = obj.curr;
                                if (!first) {
                                    that.get({
                                        pageIndex: curr,
                                        pageSize: pageSize
                                    });
                                }
                            }
                        };
                        $.extend(defaults, _pageConfig);//参数合并
                        layui.laypage(defaults);
                    }
                    if (_config.success) {
                        _config.success(); //渲染成功
                    }
                } else {
                    if (_config.fail) {
                        _config.fail(result.msg); //获取数据失败
                    }
                }

                if (_config.checkbox) {
                    //注册全选事件。
                    var $first_th = $(_config.elem).parent('table').find('thead tr').children('th:first-child');
                    $first_th.css('width', '19px');
                    var $checkbox = $first_th.find('input[type=checkbox]');
                    $checkbox.attr('lay-filter', 'allselector');
                    form.render('checkbox');
                    form.on('checkbox(allselector)', function (data) {
                        var elem = data.elem;
                        $(_config.elem).parent('table').children('tbody').children('tr').each(function () {
                            var $that = $(this);
                            $that.children('td').eq(0).find('input[type=checkbox]')[0].checked = elem.checked;
                            if (elem.checked) {
                                $that.css('background-color', '#F3F7F9');
                            } else {
                                $that.removeAttr('style');
                            }
                            form.render('checkbox');
                        });
                    });
                    //注册选择行事件。
                    $(_config.elem).children('tr').each(function (e) {
                        var $that = $(this);
                        if (_config.singleSelected) {
                            $checkbox.attr('disabled', true);
                            form.render('checkbox');
                            $that.on('click', function () {
                                //单选。
                                $that.siblings().each(function () {
                                    $(this).children('td').eq(0).children('input[type=checkbox]')[0].checked = false;
                                    $(this).removeAttr('style');
                                });
                                $that.children('td').eq(0).children('input[type=checkbox]')[0].checked = true;
                                $that.css('background-color', '#F3F7F9');
                                form.render('checkbox');
                            });

                        } else {
                            $that.on('click', function () {
                                //多选。
                                var currState = $that.children('td').eq(0).children('input[type=checkbox]')[0].checked;
                                if (currState) {
                                    $that.children('td').eq(0).children('input[type=checkbox]')[0].checked = false;
                                    $that.removeAttr('style');
                                } else {
                                    $that.children('td').eq(0).children('input[type=checkbox]')[0].checked = true;
                                    $that.css('background-color', '#F3F7F9');
                                }
                                form.render('checkbox');
                            });
                        }
                    });
                }

                if (_config.complate) {
                    _config.complate(); //渲染完成
                }
                if (loadIndex !== undefined)
                    layer.close(loadIndex);//关闭等待层
            },
            error: function (xhr, status, error) {
                if (loadIndex !== undefined)
                    layer.close(loadIndex);//关闭等待层
                _config.serverError(xhr, status, error); //服务器错误
            }
        });
    };
    /**
	 * 抛出一个异常错误信息
	 * @param {String} msg
	 */
    function throwError(msg) {
        throw new Error(msg);
        return;
    };

    var paging = new Paging();
    exports('paging', function (options) {
        return paging.set(options);
    });
});