window.onload = function () {

    var check = false,
        checkInterval;
    if (typeof $ != 'undefined') {
        onLoaded();
    } else {
        checkInterval = setInterval(function () {
            if (typeof $ != 'undefined') {
                onLoaded();
                clearInterval(checkInterval);
            }
        }, 1000);
    }

    setTimeout(function () { clearInterval(checkInterval); }, 10000);
}

function onLoaded() {
    //菜单加载完显示页面内容
    var mainBox = $('#mainBox'),
        menuWidth = $('#menu').width();
    mainBox.removeClass('hidden');
    if (/MSIE 6|MSIE 7|MSIE 8/.test(navigator.userAgent)) {
        mainBox.parent().css({
            width: $(window).width() - menuWidth,
            marginLeft: menuWidth
        });

        $('body').append('<script src="' + script_path + 'html5shiv.min.js" type="text/javascript"></script>')
		    .append('<script src="' + script_path + 'respond.min.js" type="text/javascript"></script>');
    }

    //按样式启动日期控件
    if ($ && $.datepicker) {
        LoadDate();
    }

}
//加载
function LoadDate() {
    $(".inputDate").datepicker({
        //.datepicker('setDate', new Date());    默认设置日期为今天
        dateFormat: 'yy-mm-dd',
        showButtonPanel: true,
        closeText: '清除',
        onClose: function (dateText, inst) {
            var event = arguments.callee.caller.caller.arguments[0];
            if ($(event.delegateTarget).hasClass('ui-datepicker-close')) {
                $(this).val('');
            }
            // if ($(window.event.srcElement).hasClass('ui-datepicker-close')) {
        }
    });
    $(".inputDate").attr("readonly", true).attr("style", "cursor:pointer");

}

//获取站点虚拟目录 add by：liusha
function getRootPath() {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
    return (prePath + postPath);
}


//改变页面form action路径 add by：liusha
function CheckAction(action, controller) {
    $('form').attr('action', getRootPath() + '/' + controller + '/' + action);
}

//关闭分组
function closediv(id) {
    //关闭弹出层
    document.getElementById(id).style.display = "none";
    document.getElementById("bg").style.display = "none";
    var scrollstyle = scrolls();
    scrollstyle.style.overflowY = "auto";
    scrollstyle.style.overflowX = "hidden";
    $("#groupNameTr").hide();
    $("#groupNameEmpty").hide();
}

function promptHtml() {
    var html = '<div class="container">';
    html += '        <div class="panel panel-default">';
    html += '            <div class="panel-body">';
    html += '                <div class="panel panel-default">';
    html += '                    <div class="panel-heading">';
    html += '                        {0}';
    html += '                       <a class="common-link pull-right" href="{1}">{2}</a>';
    html += '                    </div>';
    html += '                    <div class="panel-body">';
    html += '                   <div class="form-group" style="text-align:center;">';
    html += '                       <div class="alert alert-danger">{3}</div><br />';
    html += '                   </div>';
    html += '               </div>';
    html += '           </div>';
    html += '       </div>';
    html += '</div>';
    html += '';

    return html;
}

/*
    title：页面左边的标题
    url：最右边a标签的url
    linkText：最右边a标签的文本
    msg：提示信息
*/
function alertError(title, url, linkText, msg) {

    var html = '        <div class="panel panel-default">';
    html += '            <div class="panel-body">';
    html += '                <div class="panel panel-default">';
    html += '                    <div class="panel-heading">';
    html += title;
    html += '                       <a class="common-link pull-right" href="' + url + '">' + linkText + '</a>';
    html += '                    </div>';
    html += '                    <div class="panel-body">';
    html += '                   <div class="form-group" style="text-align:center;">';
    html += '                       <div class="alert alert-danger">' + msg + '</div><br />';
    html += '                   </div>';
    html += '               </div>';
    html += '           </div>';
    html += '       </div>';

    $(".container").html(html);
    $(".modal").modal("hide");
}

/*
    title：页面左边的标题
    url：最右边a标签的url
    linkText：最右边a标签的文本
    msg：提示信息
*/
function alertSuccess(title, url, linkText, msg) {
    //var html = '<div class="form-group" style="text-align:center;">';
    //html += "<div class='alert alert-success'>" + msg + "</div><br />";
    //if (btnText != ""){
    //    html += "<div><a class='btn btn-primary btn-sm' href='/" + controllerName + "/" + actionName + "' >" + btnText + "</a><div/>";
    //}
    //html += "</div>";
    //$(".form-group.form-horizontal").html(html);

    var html = '        <div class="panel panel-default">';
    html += '            <div class="panel-body">';
    html += '                <div class="panel panel-default">';
    html += '                    <div class="panel-heading">';
    html += title;
    html += '                       <a class="common-link pull-right" href="' + url + '">' + linkText + '</a>';
    html += '                    </div>';
    html += '                    <div class="panel-body">';
    html += '                   <div class="form-group" style="text-align:center;">';
    html += '                       <div class="alert alert-success">' + msg + '</div><br />';
    html += '                   </div>';
    html += '               </div>';
    html += '           </div>';
    html += '       </div>';

    $(".container").html(html);
    $(".modal").modal("hide");
}
// ----------------------------------------------------------------------
// <summary>
// 只能输入数字和一个点，且输入的第一个字符不能为点，可按退格键删除数字或点
//--吴健龙--20150714
// </summary>
// ----------------------------------------------------------------------
$.fn.onlyNumPoint = function () {
    $(this).keypress(function (event) {
        var eventObj = event || window.event;
        var keyCode = eventObj.keyCode || eventObj.which;
        if (($(this).val().length == 0 || $(this).val().indexOf('.') != -1) && keyCode == 46)
            return false;
        if ((keyCode >= 48 && keyCode <= 57) || keyCode == 46 || keyCode == 8)
            return true;
        else {
            return false;
        }
    }).focus(function () {
        this.style.imeMode = 'disabled';
    }).bind("blur", function () {
        if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
            this.value = this.value.substr(0, this.value.length - 1);
        } else if (isNaN(this.value)) {
            this.value = "";
        }
    });
};

/**
   * 验证并限制输入框的小数位数
   * fixed:精确到小数点后几位
   * 吴健龙--20150720
   */
$.fn.validateInputAsFlt = function (fixed) {
    $(this).keyup(function (event) {
        var eventObj = event || window.event;
        var k = eventObj.keyCode || eventObj.which;
        if (k == 37 || k == 38 || k == 39 || k == 40 || k == 8 || k == 46) {
            return;
        }
        var start = this.selectionStart,
            end = this.selectionEnd;
        $(this).val($(this).val().replace(/[^0-9.]/g, ''));
        $(this).val($(this).val().replace(/^\./g, ""));
        $(this).val($(this).val().replace(/\.{2,}/g, "."));
        $(this).val($(this).val().replace(".", "$#$").replace(/\./g, "").replace("$#$", "."));
        var str = ($(this).val()).substr(($(this).val()).indexOf('.'));
        if (str == '') {
            this.value = this.value.substr(0, this.value.length - 1);
        }
        if (str.length >= (fixed + 2)) {
            var num = new Number($(this).val());
            $(this).val(num.toFixed(fixed));
        }
        this.setSelectionRange(start, end);
    }).bind("paste", function () { //CTR+V
        $(this).val($(this).val().replace(/[^0-9.]/g, ''));
        $(this).val($(this).val().replace(/^\./g, ""));
        $(this).val($(this).val().replace(/\.{2,}/g, "."));
        $(this).val($(this).val().replace(".", "$#$").replace(/\./g, "").replace("$#$", "."));
        var str = ($(this).val()).substr(($(this).val()).indexOf('.'));
        if (str.length >= (fixed + 2)) {
            var num = new Number($(this).val());
            $(this).val(num.toFixed(fixed));
        }
    }).bind("blur", function () {
        if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
            this.value = this.value.substr(0, this.value.length - 1);
        }
    }).css("ime-mode", "disabled");
};

function num() {
    alert("abc");
}




/*
* 身份证15位编码规则：dddddd yymmdd xx p
* dddddd：6位地区编码
* yymmdd: 出生年(两位年)月日，如：910215
* xx: 顺序编码，系统产生，无法确定
* p: 性别，奇数为男，偶数为女
* 
* 身份证18位编码规则：dddddd yyyymmdd xxx y
* dddddd：6位地区编码
* yyyymmdd: 出生年(四位年)月日，如：19910215
* xxx：顺序编码，系统产生，无法确定，奇数为男，偶数为女
* y: 校验码，该位数值可通过前17位计算获得
* 
* 前17位号码加权因子为 Wi = [ 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 ]
* 验证位 Y = [ 1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2 ]
* 如果验证码恰好是10，为了保证身份证是十八位，那么第十八位将用X来代替
* 校验位计算公式：Y_P = mod( ∑(Ai×Wi),11 )
* i为身份证号码1...17 位; Y_P为校验码Y所在校验码数组位置
*/
function validateIdCard(idCard) {
    //15位和18位身份证号码的正则表达式
    var regIdCard = /^(^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$)|(^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[Xx])$)$/;
    //如果通过该验证，说明身份证格式正确，但准确性还需计算
    if (regIdCard.test(idCard)) {
        if (idCard.length == 18) {
            var idCardWi = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2); //将前17位加权因子保存在数组里
            var idCardY = new Array(1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2); //这是除以11后，可能产生的11位余数、验证码，也保存成数组
            var idCardWiSum = 0; //用来保存前17位各自乖以加权因子后的总和
            for (var i = 0; i < 17; i++) {
                idCardWiSum += idCard.substring(i, i + 1) * idCardWi[i];
            }
            var idCardMod = idCardWiSum % 11;//计算出校验码所在数组的位置
            var idCardLast = idCard.substring(17);//得到最后一位身份证号码
            //如果等于2，则说明校验码是10，身份证号码最后一位应该是X
            if (idCardMod == 2) {
                if (idCardLast == "X" || idCardLast == "x") {
                    //alert("恭喜通过验证啦！");
                    return true;
                } else {
                    //alert("身份证号码错误！");
                    return false;
                }
            } else {
                //用计算出的验证码与最后一位身份证号码匹配，如果一致，说明通过，否则是无效的身份证号码
                if (idCardLast == idCardY[idCardMod]) {
                    //alert("恭喜通过验证啦！");
                    return true;
                } else {
                    //alert("身份证号码错误！");
                    return false;
                }
            }
        }
    } else {
        //alert("身份证格式不正确!");
        return false;
    }
}

/*
*   验证姓名
*   只能输入2-10个汉字或"·", 且首尾必须为汉字
*   add by: hesj 
*   date : 2015-07-28
*/
function validateName(txtName) {
    // 只能输入2-10个汉字或"·", 且首尾必须为汉字
    var reg = /^[\u4E00-\u9FA5][\u4E00-\u9FA5\·]{0,8}[\u4E00-\u9FA5]$/;

    if (reg.test(txtName)) {
        return true;
    }
    return false;
}

/*
     * 说明：验证手机号码
     * add by :hesj
     * date : 2015-07-29
     */
function validatePhone(txtPhone) {
    var myreg = /^1[3|4|5||7|8][0-9]{9}$/;
    if (!myreg.test(txtPhone)) {
        return false;
    }
    return true;
}


// ----------------------------------------------------------------------
// <summary>
// 限制只能输入数字
//--吴健龙--20150714
// </summary>
// ----------------------------------------------------------------------
$.fn.onlyNum = function () {
    $(this).keypress(function (event) {
        var eventObj = event || e;
        var keyCode = eventObj.keyCode || eventObj.which;
        if ((keyCode >= 48 && keyCode <= 57))
            return true;
        else
            return false;
    }).focus(function () {
        //禁用输入法
        this.style.imeMode = 'disabled';
    }).bind("paste", function () {
        //获取剪切板的内容
        var clipboard = window.clipboardData.getData("Text");
        if (/^\d+$/.test(clipboard))
            return true;
        else
            return false;
    }).bind("blur", function () {
        if (!(/^\d+$/.test(this.value))) {
            this.value = "";
            return false;
        } else if (isNaN(this.value)) {
            this.value = "";
            return false;
        } else {
            return true;
        }
    });
};
// ----------------------------------------------------------------------
// <summary>
// 限制只能输入字母
//--吴健龙--20150714
// </summary>
// ----------------------------------------------------------------------
$.fn.onlyAlpha = function () {
    $(this).keypress(function (event) {
        var eventObj = event || e;
        var keyCode = eventObj.keyCode || eventObj.which;
        if ((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122))
            return true;
        else
            return false;
    }).focus(function () {
        this.style.imeMode = 'disabled';
    }).bind("paste", function () {
        var clipboard = window.clipboardData.getData("Text");
        if (/^[a-zA-Z]+$/.test(clipboard))
            return true;
        else
            return false;
    }).bind("blur", function () {
        if (!(/^[a-zA-Z]+$/.test(this.value))) {
            this.value = "";
            return false;
        } else {
            return true;
        }
    });
};
// ----------------------------------------------------------------------
// <summary>
// 限制只能输入数字和字母
//--吴健龙--20150714
// </summary>
// ----------------------------------------------------------------------
$.fn.onlyNumAlpha = function () {
    $(this).keypress(function (event) {
        var eventObj = event || e;
        var keyCode = eventObj.keyCode || eventObj.which;
        if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122))
            return true;
        else
            return false;
    }).focus(function () {
        this.style.imeMode = 'disabled';
    }).bind("paste", function () {
        var clipboard = window.clipboardData.getData("Text");
        if (/^(\d|[a-zA-Z])+$/.test(clipboard))
            return true;
        else
            return false;
    }).bind("blur", function () {
        if (!(/^(\d|[a-zA-Z])+$/.test(this.value))) {
            this.value = "";
            return false;
        } else {
            return true;
        }
    });
};
// ----------------------------------------------------------------------
// <summary>
// 只能输入数字和一个点，且输入的第一个字符不能为点，可按退格键删除数字或点
//--吴健龙--20150714
// </summary>
// ----------------------------------------------------------------------
$.fn.onlyNumPoint = function () {
    $(this).keypress(function (event) {
        var eventObj = event || window.event;
        var keyCode = eventObj.keyCode || eventObj.which;
        if (($(this).val().length == 0 || $(this).val().indexOf('.') != -1) && keyCode == 46)
            return false;
        if ((keyCode >= 48 && keyCode <= 57) || keyCode == 46 || keyCode == 8)
            return true;
        else {
            return false;
        }
    }).focus(function () {
        this.style.imeMode = 'disabled';
    }).bind("blur", function () {
        if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
            this.value = this.value.substr(0, this.value.length - 1);
        } else if (isNaN(this.value)) {
            this.value = "";
        }
    });
};

// ----------------------------------------------------------------------
// <summary>
// 屏蔽非法字符,只允许汉字、英文字母、数字、下划线
//--吴健龙--20150715
// </summary>
// ----------------------------------------------------------------------
$.fn.ignoreIllegalChar = function () {
    //.match(/^[\u4E00-\u9FA5a-zA-Z0-9_]{3,20}$/) //{3,20}$表示是长度3-20
    var pattern = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\]<>/?~！%-@#￥……&*（）——|{}【】‘；：”“'。，、？]");
    $(this).keypress(function (event) {
        var eventObj = event || window.event;
        var keyCode = eventObj.keyCode || eventObj.which;
        var inputVal = String.fromCharCode(keyCode);
        if (!inputVal.match(/^[\u4E00-\u9FA5a-zA-Z0-9_]{0,}$/))
            return false;
        else
            return true;
    }).bind("paste", function () {
        var clipboard = window.clipboardData.getData("Text");
        if (/^[\u4E00-\u9FA5a-zA-Z0-9_]{0,}$/.test(clipboard))
            return true;
        else {
            var rs = "";
            var s = this.value;
            for (var i = 0; i < s.length; i++) {
                rs = rs + s.substr(i, 1).replace(pattern, '');
            }
            this.value = rs;
            return false;
        }
    }).bind("blur", function () {
        if (!(/^[\u4E00-\u9FA5a-zA-Z0-9_]{0,}$/.test(this.value))) {
            //this.value = "";
            var rs = "";
            var s = this.value;
            for (var i = 0; i < s.length; i++) {
                rs = rs + s.substr(i, 1).replace(pattern, '');
            }
            this.value = rs;
            return false;
        } else {
            return true;
        }
    });
};

// ----------------------------------------------------------------------
// <summary>
// 屏蔽非法字符,只允许汉字、英文字母、数字、下划线、中横线、小括号
//--吴健龙--20150715
// </summary>
// ----------------------------------------------------------------------
$.fn.addressIllegalChar = function () {
    //.match(/^[\u4E00-\u9FA5a-zA-Z0-9_]{3,20}$/) //{3,20}$表示是长度3-20
    var pattern = new RegExp("[`~!@#$^&*=|{}':;',\\[\\]<>/?~！%@#￥……&*——|{}【】‘；：”“'。，、？]");
    $(this).keypress(function (event) {
        var eventObj = event || window.event;
        var keyCode = eventObj.keyCode || eventObj.which;
        var inputVal = String.fromCharCode(keyCode);
        if (!inputVal.match(/^[\u4E00-\u9FA5a-zA-Z0-9_()（）\-]{0,}$/))
            return false;
        else
            return true;
    }).bind("paste", function () {
        var clipboard = window.clipboardData.getData("Text");
        if (/^[\u4E00-\u9FA5a-zA-Z0-9_()（）\-]{0,}$/.test(clipboard))
            return true;
        else {
            var rs = "";
            var s = this.value;
            for (var i = 0; i < s.length; i++) {
                rs = rs + s.substr(i, 1).replace(pattern, '');
            }
            this.value = rs;
            return false;
        }
    }).bind("blur", function () {
        if (!(/^[\u4E00-\u9FA5a-zA-Z0-9_()（）\-]{0,}$/.test(this.value))) {
            //this.value = "";
            var rs = "";
            var s = this.value;
            for (var i = 0; i < s.length; i++) {
                rs = rs + s.substr(i, 1).replace(pattern, '');
            }
            this.value = rs;
            return false;
        } else {
            return true;
        }
    });
};

/**
   * 验证并限制输入框的小数位数
   * fixed:精确到小数点后几位
   * 吴健龙--20150720
   */
$.fn.validateInputAsFlt = function (fixed) {
    $(this).keyup(function (event) {
        var eventObj = event || window.event;
        var k = eventObj.keyCode || eventObj.which;
        if (k == 37 || k == 38 || k == 39 || k == 40 || k == 8 || k == 46) {
            return;
        }
        var start = this.selectionStart,
            end = this.selectionEnd;
        $(this).val($(this).val().replace(/[^0-9.]/g, ''));
        $(this).val($(this).val().replace(/^\./g, ""));
        $(this).val($(this).val().replace(/\.{2,}/g, "."));
        $(this).val($(this).val().replace(".", "$#$").replace(/\./g, "").replace("$#$", "."));
        var str = ($(this).val()).substr(($(this).val()).indexOf('.'));
        if (str == '') {
            this.value = this.value.substr(0, this.value.length - 1);
        }
        if (str.length >= (fixed + 2)) {
            var num = new Number($(this).val());
            $(this).val(num.toFixed(fixed));
        }
        this.setSelectionRange(start, end);
    }).bind("paste", function () { //CTR+V
        var txt = $(this).val();

        $(this).val().replace(/[^0-9.]/g, '');
        $(this).val().replace(/^\./g, "");
        $(this).val().replace(/\.{2,}/g, ".");
        $(this).val().replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
        var str = ($(this).val()).substr(($(this).val()).indexOf('.'));
        if (str.length >= (fixed + 2)) {
            var num = new Number($(this).val());
            $(this).val(num.toFixed(fixed));
        }
    }).bind("blur", function () {
        if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
            this.value = this.value.substr(0, this.value.length - 1);
        }
    }).css("ime-mode", "disabled");
};

$.fn.numeral = function (bl) {//限制金额输入、兼容浏览器、屏蔽粘贴拖拽等
    $(this).keypress(function (e) {
        var keyCode = e.keyCode ? e.keyCode : e.which;
        if (bl) {//浮点数
            if ((this.value.length == 0 || this.value.indexOf(".") != -1) && keyCode == 46) return false;
            return keyCode >= 48 && keyCode <= 57 || keyCode == 46 || keyCode == 8;
        } else {//整数
            return keyCode >= 48 && keyCode <= 57 || keyCode == 8;
        }
    });
    $(this).bind("copy cut paste", function (e) { // 通过空格连续添加复制、剪切、粘贴事件
        if (window.clipboardData)//clipboardData.setData('text', clipboardData.getData('text').replace(/\D/g, ''));
            return !clipboardData.getData('text').match(/\D/);
        else
            event.preventDefault();
    });
    $(this).bind("blur", function () {
        if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
            this.value = this.value.substr(0, this.value.length - 1);
        } else if (isNaN(this.value)) {
            this.value = "";
        }
    });
    $(this).bind("dragenter", function () { return false; });
    $(this).css("-webkit-ime-mode", "disabled");
    $(this).bind("focus", function () {
        if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
            this.value = this.value.substr(0, this.value.length - 1);
        } else if (isNaN(this.value)) {
            this.value = "";
        }
    });
}

/*
设置Bootstrap-select下拉框的值
provinceId：省份下拉框的Id
proviceValue：省份Value
cityId：市下拉框的Id
cityValue：市Value
countyId：区下拉框的Id
countyValue：区Value
*/
function setBootstrapSelectValue(provinceId, proviceValue, cityId, cityValue, countyId, countyValue) {
    $("#" + provinceId).selectpicker("val", proviceValue);
    setTimeout("setCityValue('" + cityId + "','" + cityValue + "','" + countyId + "','" + countyValue + "')", 500);
}

function setCityValue(cityId, cityValue, countyId, countyValue) {
    $("#" + cityId).selectpicker("val", cityValue);
    setTimeout("setCountyValue('" + countyId + "','" + countyValue + "')", 500);
}

function setCountyValue(countyId, countyValue) {
    $("#" + countyId).selectpicker("val", countyValue);
}

// 除法
function accDiv(arg1, arg2) {
    var t1 = 0, t2 = 0, r1, r2;
    try { t1 = arg1.toString().split(".")[1].length } catch (e) { }
    try { t2 = arg2.toString().split(".")[1].length } catch (e) { }
    with (Math) {
        r1 = Number(arg1.toString().replace(".", ""));
        r2 = Number(arg2.toString().replace(".", ""));
        return accMul((r1 / r2), pow(10, t2 - t1));
    }
}
// 乘法  
function accMul(arg1, arg2) {
    var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
    try { m += s1.split(".")[1].length } catch (e) { }
    try { m += s2.split(".")[1].length } catch (e) { }
    return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m);
}
// 加法   
function accAdd(arg1, arg2) {
    var r1, r2, m;
    try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
    try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
    m = Math.pow(10, Math.max(r1, r2));
    return (arg1 * m + arg2 * m) / m;
}
// 减法   
function Subtr(arg1, arg2) {
    var r1, r2, m, n;
    try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
    try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
    m = Math.pow(10, Math.max(r1, r2));
    n = (r1 >= r2) ? r1 : r2;
    return ((arg1 * m - arg2 * m) / m).toFixed(n);
}

/**
   * 弹窗提示信息
   * Add by 李友明 2016.05.19
   * msg：提示信息（可换行和包含文字大小等样式）
   * btnText：确认按钮显示的文字
   */
function showMessage(msg, width, height, btnText) {
    var dg = $.dialog({
        id: 'MessageItem',
        title: '温馨提示',
        content: msg,
        width: width,
        height: height,
        cancelVal: btnText,
        cancel: true,
        max: false,
        min: false,
        lock: true
    });

    dg.ShowDialog;
}
//此函数返回当前dialog对象
//需自行调用ShowDialog()弹出
function showDialogMsg(title, content, closedUrl, height, width, canceled) {
    debugger;
    var h = height ? 220 : height;
    var w = width ? 360 : width;
    var $dialog = $.dialog({
        title: title,
        content: content,
        max: false,
        min: false,
        width: w,
        height: h,
        drag: true,
        lock: true,
        cancel: canceled,
        close: function () {
            if (closedUrl != "") {
                location.href = getRootPath() + closedUrl;
            }
        }
    });
    
    return $dialog;
}

function showSuccessMsg(title, content, closedUrl, height, width) { 
    showDialogMsg(title, content, closedUrl, height, width, false).ShowDialog();
   
}