$(function () {
    ////验证优化（remote）功能点（已验证可再次触发验证）
    jQuery.validator.addMethod("remote1", function (value, element, param) {
        if (this.optional(element))
            return "dependency-mismatch";
        var previous = this.previousValue(element);
        if (!this.settings.messages[element.name])
            this.settings.messages[element.name] = {};
        previous.originalMessage = this.settings.messages[element.name].remote;
        this.settings.messages[element.name].remote = previous.message;

        param = typeof param == "string" && { url: param } || param;

        //if ( this.pending[element.name] ) {
        //	return "pending";
        //}
        //if ( previous.old == value ) {
        //	return previous.valid;
        //}  
        previous.old = value;
        var validator = this;
        this.startRequest(element);
        var data = {};
        data[element.name] = value;

        $.ajax($.extend(true, {
            url: param,
            mode: "abort",
            port: "validate" + element.name,
            dataType: "json",
            data: data,
            success: function (response) {
                validator.settings.messages[element.name].remote = previous.originalMessage;
                var valid = response === true;
                if (valid) {
                    var submitted = validator.formSubmitted;
                    validator.prepareElement(element);
                    validator.formSubmitted = submitted;
                    validator.successList.push(element);
                    validator.showErrors();
                } else {
                    var errors = {};
                    var message = response || validator.defaultMessage(element, "remote");
                    errors[element.name] = previous.message = $.isFunction(message) ? message(value) : message;
                    validator.showErrors(errors);
                }
                previous.valid = valid;
                validator.stopRequest(element, valid);
            }
        }, param));
        return "pending";
    }, "存在");
    jQuery.validator.addMethod("Wage1", function (value, element, param) {
        if (value <= 0) {
            return false;
        }
        else
            return true;
    }, $.validator.format("薪资水平应大于0"));
    jQuery.validator.addMethod("CompanyName", function (value, element, param) {
        if (value.indexOf("汇教") >= 0 || value.indexOf("心恒思途") >= 0)
            return false;
        return true;
    }, $.validator.format("抱歉，不能以汇教或心恒思途名义发布岗位"));
    jQuery.validator.addMethod("CompanyID", function (value, element, param) {
        if ($("#CompanyId").val().length == 0)
            return false;
        return true;
    }, $.validator.format("企业不存在"));
    // 中文字两个字节
    jQuery.validator.addMethod("byteRangeLength", function (value, element, param) {
        var length = value.length;
        for (var i = 0; i < value.length; i++) {
            if (value.charCodeAt(i) > 127) {
                length++;
            }
        }
        return this.optional(element) || (length >= param[0] && length <= param[1]);
    }, $.validator.format("请确保输入的值在{0}-{1}个字节之间(一个中文字算2个字符)"));
    // 手机号码验证    
    jQuery.validator.addMethod("isMobile", function (value, element) {
        var length = value.length;
        return this.optional(element) || (length == 11);
    }, "请正确填写您的手机号码。");

    // 电话号码验证    
    jQuery.validator.addMethod("isPhone", function (value, element) {
        var tel = /^(\d{3,4}-?)?\d{7,9}$/g;
        return this.optional(element) || (tel.test(value));
    }, "请正确填写您的电话号码。");

    // 联系电话(手机/电话皆可)验证   
    jQuery.validator.addMethod("isTel", function (value, element) {
        var length = value.length;
        var mobile = /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1}))+\d{8})$/;
        var tel = /^(\d{3,4}-?)?\d{7,9}$/g;
        return this.optional(element) || tel.test(value) || (length == 11 && mobile.test(value));
    }, "请正确填写您的联系方式");
    $.validator.addClassRules({
        title: { //岗位标题  // 关键词判断 
            required: true,
            remote1: function () {
                $("label[for='Title']").remove();// 移除错误标签 
                $('#Title').removeClass("error");
                var r = {
                    url: "/EnterpriseUser/TitleIsBlackword",
                    type: "POST",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{title:'" + $('#Title').val() + "'}",
                    dataFilter: function (data) {
                        var jsonobj = eval('(' + data + ')');
                        if (jsonobj.status == true) {
                        }
                        else {////////////////////////////////////////////////////////////////////错误 
                            $("<label for=\"Title\" class=\"error\">" + "关键字(" + jsonobj.blockkeyword + ")已被系统屏蔽，请去除</label>").insertAfter($("#Title"));//错误提示 
                        }
                        return $.parseJSON(jsonobj.status);//存在应报错
                    }
                }
                return r;
            }
        },
        existsusername: {
            required: true,
            remote: function () {
                var r = {
                    url: "/account/existsusername",
                    type: "POST",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{username:'" + $('#username').val() + "'}",
                    dataFilter: function (data) {
                        return $.parseJSON(data);
                    }
                }
                return r;
            }
        },
        existspromotername: {
            required: true,
            remote: function () {
                var r = {
                    url: "/promoter/existspromotername",
                    type: "POST",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{existspromotername:'" + $('#existspromotername').val() + "'}",
                    dataFilter: function (data) {
                        return !$.parseJSON(data);
                    }
                }
                return r;
            }
        },
        checkpromotername: {
            required: true,
            remote: function () {
                var r = {
                    url: "/promoter/checkpromotername",
                    type: "POST",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{checkpromotername:'" + $('#checkpromotername').val() + "', oldpromotername:'" + $('#checkpromotername').attr('rel') + "'}",
                    dataFilter: function (data) {
                        return !$.parseJSON(data);
                    }
                }
                return r;
            }
        },
        password: {
            required: true,
            rangelength: [6, 20]
        },
        oldpassword: {
            required: true,
            rangelength: [6, 20]
        },
        newpassword: {
            required: true,
            rangelength: [6, 20]
        },
        confirm_password: {
            required: true,
            rangelength: [6, 20],
            equalTo: "#newpassword"
        },
        accountname: {
            required: true,
            byteRangeLength: [4, 18]
        },
        Wage: {
            Wage1: true,
            required: true
        }
        ,
        companyname: {
            CompanyName: true,
            required: true,
            CompanyID: true
        }
    });

    $('form').each(function () {
        $(this).validate({
            messages: { 
                title: {
                    required: "输入岗位名称！", 
                    remote1: "标题中存在非法关键词！"
                },
                username: {
                    required: "请输入用户名",
                    remote: "用户名不存在"
                },
                existspromotername: {
                    required: "请输入联盟名称",
                    remote: "用户名已存在"
                },
                checkpromotername: {
                    required: "请输入联盟名称",
                    remote: "用户名已存在"
                },
                password: {
                    required: "请输入密码",
                    rangelength: "密码长度应在{0}和{1}之间"
                },
                oldpassword: {
                    required: "请输入旧密码",
                    rangelength: "密码长度应在{0}和{1}之间"
                },
                newpassword: {
                    required: "请输入新密码",
                    rangelength: "密码长度应在{0}和{1}之间"
                },
                confirm_password: {
                    required: "请输入确认密码",
                    rangelength: "密码长度应在{0}和{1}之间",
                    equalTo: "请再次输入相同的确认密码"
                },
                accountname: {
                    required: "请输入账户名称",
                    rangelength: "账户长度应在{0}和{1}之间",
                },
                Wage: {
                    Wage1: "薪资水平应大于0",
                    required: "请输入转出余额"
                },
                companyname: {
                    CompanyName: "抱歉，不能以汇教或心恒思途名义发布岗位",
                    required: "请输入企业名称",
                    CompanyID: "企业不存在"
                }
            },
            errorPlacement: function (label, element) {
                if (element.attr("type") === "checkbox" || element.attr("type") === "radio") {
                    element.parent().append(label); // this would append the label after all your checkboxes/labels (so the error-label will be the last element in <div class="controls"> )
                }
                    //else if (element.attr("type") == "text" && element.attr("id") == "CompanyName") { ////////////////////////////////////忘记密码--用户企业手机号验证 
                    //    //if (label[0].innerHTML.length > 0)
                    //    //    $("<label for=\"CompanyName\" class=\"error\">" + label[0].innerHTML + "</label>").insertAfter($("#CompanyName").parent());
                    //}
                else {
                    label.insertAfter(element); // standard behaviour
                }
            },
            success: function (element) {
                $(element).addClass("valid").append("");
            },
            //use the default handler, so comment the bottom line as it will conflict with Obout Editor, which sucks me.
            submitHandler: function (form) {
                form.submit();
            }
        });
    });

});

