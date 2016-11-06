function check(id) {
    var canCallApi = true;
    var check_num = $("#check_num").val();
    if (check_num == "") {
        canCallApi = false;
        $("#check_num").next().text("验证码不能为空");
    }
    else {
        $("#check_num").next().text("");
    }
    var sex = $('input:radio[name="sex"]:checked').val();
    if (sex == null) {
        canCallApi = false;
        $("#sex").next().text("请选择性别");
    }
    else {
        $("#sex").next().text("");
        // layer.closeAll("page");
    }
    var myreg = /^1[\d]{10}$/;
    if (!myreg.test($("#phone").val())) {
        canCallApi = false;
        $("#phone").next().text('请输入有效的手机号码！');
    } else {
        $("#phone").next().text("");
    }
    var username = $("#username").val();
    var temp = username.replace(/\s+/g, "");

    if (username == "") {
        canCallApi = false;
        $("#username").next().text("姓名不能为空");
    }
    else if (temp == "") {
        canCallApi = false;
        $("#username").next().text("姓名不能为空");
    }
    else {
        $("#username").next().text("");
    }

    if (canCallApi == true) {
        var data =
            {
                QQorEmail: $("#email").val(),
                ValidCode: $("#check_num").val(),
                Phone: $("#phone").val(),
                Sex: $('input:radio[name=sex]:checked').val(),
                Name: $("#username").val(),
                ActivityId: id
            };
        $.ajaxPost("/PublicActivity/Sign", data, function (result) {
            if (result == true) {
                //window.location.href = '/Partner/Manage';
                alertSuccess("报名成功！");
                location.reload();
                //layer.closeAll("page");
            }
            else {
                alertWarning("报名失败！");
                layer.closeAll("page");
            }
        });
    }
}

function initNumberInput(event) {
    $("#check_num").css("backgroundColor", "#FFFF00");
    if ($("input.only-number").length) {
        $("input.only-number").off().on("input.only-number", function () {
            var regex;
            if ($(this).hasClass("integer-num")) {
                this.value = this.value.replace(/[^\d]/g, '');
            } else {
                this.value = this.value.replace(/[^0-9\.]/g, "");
            }
            var val = $.trim($(this).val());
            var min = null;
            var max = null;
            if (typeof ($(this).data("min")) != "undefined") {
                var min = $(this).data("min");
            } if (typeof ($(this).data("max")) != "undefined") {
                var max = $(this).data("max");
            }

            if (val == "") {
                $(this).next().removeClass("valid");
                $(this).next().addClass("error");
                $(this).next().text("不能为空");
            } else if (min != null && val < min) {
                $(this).next().removeClass("valid");
                $(this).next().addClass("error");
                $(this).next().text("至少输入" + min);
            } else if (max != null && val > max) {
                $(this).next().removeClass("valid");
                $(this).next().addClass("error");
                $(this).next().text("不得超过" + max);
            } else {
                $(this).next().removeClass("error");
                $(this).next().addClass("valid");
                $(this).next().text("");
            }
        });
        $("input.only-number").focus("input", function () {
            $(this).css("backgroundColor", "#FFFF00");
        });
        $("input.only-number").blur("input", function () {
            $(this).css("backgroundColor", "#FFFFFF");
            $(this).removeClass("valid");
        });
    }
}
function initTextInput(event) {
    $("#username").css("backgroundColor", "#FFFF00");
    if ($("input.control_length").length) {
        $("input.control_length").off().on("input.control_length", function () {
            var val = $.trim($(this).val());
            var minLength = null;
            var maxLength = null;
            if (typeof ($(this).data("min-length")) != "undefined") {
                var minLength = $(this).data("min-length");
            } if (typeof ($(this).data("max-length")) != "undefined") {
                var maxLength = $(this).data("max-length");
            }
            if (val == "") {
                $(this).next().addClass("error");
                $(this).next().removeClass("valid");
                $(this).next().text("不能为空");
            } else if (minLength != null && val.length < minLength) {
                $(this).next().addClass("error");
                $(this).next().removeClass("valid");
                $(this).next().text("至少输入" + minLength + "字");
            } else if (maxLength != null && val.length > maxLength) {
                $(this).next().addClass("error");
                $(this).next().removeClass("valid");
                $(this).next().text("不得超过" + maxLength + "字");
            } else {
                $(this).next().addClass("valid");
                $(this).next().removeClass("error");
                $(this).next().text("");
            }
        });
        $("input.control_length").focus("input", function () {
            $(this).css("backgroundColor", "#FFFF00");
        });
        $("input.control_length").blur("input", function () {
            $(this).css("backgroundColor", "#FFFFFF");
            $(this).removeClass("valid");
        });
    }
}
function initTextInputNolimit(event) {
    $("#email").css("backgroundColor", "#FFFF00");
    $("input").focus("input", function () {
        $(this).css("backgroundColor", "#FFFF00");
    });
    $("input").blur("input", function () {
        $(this).css("backgroundColor", "#FFFFFF");
        $(this).removeClass("valid");
    });
}
function checkRadio(event) {
    $("#sex").next().addClass("valid");
    $("#sex").next().removeClass("error");
    $("#sex").next().text("");
}
function checkPhone(event) {
    $("#phone").css("backgroundColor", "#FFFF00");
    var myreg = /^1[\d]{10}$/;
    if ($("input.only-number").length) {
        $("input.only-number").off().on("input.only-number", function () {
            var regex;
            if ($(this).hasClass("integer-num")) {
                this.value = this.value.replace(/[^\d]/g, '');
            } else {
                this.value = this.value.replace(/[^0-9\.]/g, "");
            }
            var val = $.trim($(this).val());
            var min = null;
            var max = null;
            if (typeof ($(this).data("min")) != "undefined") {
                var min = $(this).data("min");
            } if (typeof ($(this).data("max")) != "undefined") {
                var max = $(this).data("max");
            }

            if (val == "") {
                $(this).next().removeClass("valid");
                $(this).next().addClass("error");
                $(this).next().text("不能为空");
            } else if (!myreg.test($("#phone").val())) {
                $(this).next().removeClass("valid");
                $(this).next().addClass("error");
                $(this).next().text('请输入有效的手机号码');
            } else {
                $(this).next().removeClass("error");
                $(this).next().addClass("valid");
                $(this).next().text("");
            }
        });
        $("input.only-number").focus("input", function () {
            $(this).css("backgroundColor", "#FFFF00");
        });
        $("input.only-number").blur("input", function () {
            $(this).css("backgroundColor", "#FFFFFF");
            $(this).removeClass("valid");
        });
    }
}

function get_security_code(id) {
    debugger;
    var get_security_code = document.getElementById("get_security_code");
    if (get_security_code.innerText == "获取验证码") {
        var myreg = /^1[\d]{10}$/;
        if (!myreg.test($("#phone").val())) {
            $("#phone").next().removeClass("valid");
            $("#phone").next().addClass("error");
            $("#phone").next().text('请输入有效的手机号码');
        }
        else {
            send(id);//发短信判断
        }
    }
    function time(wait, id) {
        if (wait == 0) {
            var get_security_code = document.getElementById("get_security_code");
            get_security_code.removeAttribute("disabled");
            get_security_code.innerText = "获取验证码";
            get_security_code.style.color = "#2CA4DE";
            // o.value="免费获取验证码";

            $("#get_security_code").bind("click", function () {
                var myreg = /^1[\d]{10}$/;
                if (!myreg.test($("#phone").val())) {
                    $("#phone").next().removeClass("valid");
                    $("#phone").next().addClass("error");
                    $("#phone").next().text('请输入有效的手机号码');
                } else {
                    send(id);//发短信判断
                }
            })
        }
        else {
            var get_security_code = document.getElementById("get_security_code");
            get_security_code.setAttribute("disabled", true);
            get_security_code.style.color = "#9A9A9A";
            get_security_code.innerText = "重新发送(" + wait + ")";
            //o.value = "重新发送(" + wait + ")";
            wait--;
            setTimeout(function () {
                time(wait, id)
            }, 1000)
            $("#get_security_code").unbind();
        }
    }

    function send(id) {
        var wait = 60;
        time(wait, id);//开始60s计时
        //这里写发短信的方法
        var paramData = {
            ActivityId: id,
            Name: $('#username').val(),
            Sex: $('input:radio[name=sex]:checked').val(),
            Phone: $('#phone').val(),
            ValidCode: $('#check_num').val(),
            QQorEmail: $('#email').val()
        };
        $.ajax({
            url: '/Common/SendVerifyMobileCode',
            type: 'post',
            data: JSON.stringify(paramData),
            contentType: 'application/json',
            success: function (result) {
                if (result != "") {
                    alertSuccess("发送成功!");
                } else {
                    alertWarning("发送失败!");
                }
            },
            failure: function (result) {
                alertWarning("发送失败!" + result);
            }
        });
    }
}
$(function () {
    Tab(2);
})