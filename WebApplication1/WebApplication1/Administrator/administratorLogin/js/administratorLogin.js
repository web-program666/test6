function cookies() {
    //var oForm = document.getElementsById("button");
    var oUser = document.getElementById('name');
    var oPswd = document.getElementById('pass');
    var oRemember = document.getElementById('remember');
    //页面初始化时，如果帐号密码cookie存在则填充
    if (getCookie('username') && getCookie('password')) {
        oUser.value = getCookie('username');
        oPswd.value = getCookie('password');
        oRemember.checked = true;
    }
    //复选框勾选状态发生改变时，如果未勾选则清除cookie
    oRemember.onchange = function () {
        if (!this.checked) {
            delCookie('username');
            delCookie('password');
        }
    };
    //提交事件触发时，如果复选框是勾选状态则保存cookie
    document.onclick = function () {
        var obj = event.srcElement;
        if (obj.type == "button") {
            if (oRemember.checked) {
                setCookie('username', oUser.value, 7); //保存帐号到cookie，有效期7天
                setCookie('password', oPswd.value, 7); //保存密码到cookie，有效期7天
            }
        }
    }
}
//设置cookie
function setCookie(name, value, day) {
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

function pass_verify() {     //验证
    var name = document.getElementById("name").value;
    var password = document.getElementById("pass").value;
    if (name == '' || password == '' || name == undefined || password == undefined || name == null || password == null)
    {
        alert("请输入用户名或密码！");
        document.getElementById("name").value = "";
        document.getElementById("pass").value="";
    }
    else {
        if (isNumber(password)) {
            submit();
        }
        else {
            
            alert("密码必须全为数字！");
            document.getElementById("pass").value = "";

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
function submit() {     //ajax传表单
    var name = document.getElementById("name").value;
    var password = document.getElementById("pass").value;
    var XmlHttp = new XMLHttpRequest();
    var form = new FormData();
    form.append("name", name);    //创建表单
    form.append("password", password);//创建表单
    XmlHttp.open("Post", "../aspx/administratorLogin.aspx", true);
    XmlHttp.onreadystatechange = function () {
        if (XmlHttp.readyState == 4 && XmlHttp.status == 200) {
            flag = XmlHttp.responseText;
            result(flag);
        }
    };   //异步
    XmlHttp.send(form);
}

function result(flag) {
    if (flag == 1) {
        alert("登陆成功！");
        window.location.href = "/Administrator/cardIdInput/html/cardIdInput.html"; //跳转到管理员操作界面
    }
    else if (flag == -1) {
        alert("账户不存在!");
        document.getElementById("name").value = "";
        document.getElementById("pass").value = "";
    }
    else {
        alert("账户或密码错误！");
        document.getElementById("name").value = "";
        document.getElementById("pass").value = "";
    }
}

