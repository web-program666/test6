function pass_verify() {     //验证
    var cardId = document.getElementById("cardId").value;
    var password = document.getElementById("password").value;
    if (cardId == '' || password == '') {
        alert("请输入用户名或密码！");
        document.getElementById("cardId").value = "";
        document.getElementById("password").value = "";
    }
    else {
        if (isNumber(password) && password.length == 6) {
            submit();
        }
        else if (password.length != 6) {
            alert("密码必须为6位！")
            document.getElementById("password").value = "";
        }
        else {
            alert("密码必须全为数字！");
            document.getElementById("password").value = "";

        }
    }
}
function isNumber(value) {         //验证是否为数字
    var patrn = /^(-)?\d+(\.\d+)?$/;
    if (patrn.exec(value) == null || value == "") {
        return false
    }
    else {
        return true
    }

}



function cookies() {
    //var oForm = document.getElementsById("button");
    var oUser = document.getElementById('cardId');
    var oPswd = document.getElementById('password');
    var oRemember = document.getElementById('remember');
    var oSubmit = document.getElementById("submit");
    var isLogin=false;

    //页面初始化时，如果帐号密码cookie存在则填充
    if (getCookie('cardId') && getCookie('password')) {
        oUser.value = getCookie('cardId');
        oPswd.value = getCookie('password');
        oRemember.checked = true;
    }
    //复选框勾选状态发生改变时，如果未勾选则清除cookie
    oRemember.onchange = function () {
        if (!this.checked) {
            delCookie('cardId');
            delCookie('password');
        }
    };
    //提交事件触发时，如果复选框是勾选状态则保存cookie
    oSubmit.onclick = function () {
        //var obj = event.srcElement;
        //if (obj.type == "submit") {
            if (oRemember.checked) {
                setCookie('cardId', oUser.value, 7); //保存帐号到cookie，有效期7天
                setCookie('password', oPswd.value, 7); //保存密码到cookie，有效期7天
        }
        pass_verify();   //点击登录按钮  执行ajax
 /*       }*/
    }
}
//设置cookie
function setCookie(name, value, day,isLogin) {
    var date = new Date();
    date.setDate(date.getDate() + day);
    document.cookie = name + '=' + value + ';expires=' + date;
};
//获取cookie
function getCookie(name) {
    var reg = RegExp(name + '=([^;]+)');
    var arr = document.cookie.match(reg);
    if (arr) {
        return arr[1];
    } else {
        return '';
    }
};
//删除cookie
function delCookie(name) {
    setCookie(name, null, -1);
};







