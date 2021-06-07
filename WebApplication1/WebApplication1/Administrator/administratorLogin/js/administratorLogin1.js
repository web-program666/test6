
function login() {
    var password1 = document.getElementById("regpass").value;
    var password2 = document.getElementById("reregpass").value;
    var adminName = document.getElementById("regname").value;
    var confirmText = document.getElementById("verify").value;
    if (password1 == '' || password2 == '' || adminName == '' || confirmText == '') {
        alert("请输入用户名、密码或者验证码")
        document.getElementById("regpass").value = "";
        document.getElementById("reregpass").value = "";
        document.getElementById("regname").value = "";
        document.getElementById("verify").value = "";
    }
    else {
        if (password2 == password1 & password1 != "") {
            var data = "adminName=" + adminName + "&password2=" + password2 + "&confirmText=" + confirmText;
            var xht = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject('Microsoft.XMLHTTP');
            xht.open("POST", "../aspx/AdminLog.aspx", true);
            xht.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            xht.send(data);
            xht.onreadystatechange = function () {
                if (xht.readyState == 4 && xht.status == 200) {
                    var str = xht.responseText;
                    if (str == "1") {
                        alert("注册成功！");
                        location.reload();
                    }
                    else {
                        alert(str);
                        document.getElementById("regpass").value = "";
                        document.getElementById("reregpass").value = "";
                        document.getElementById("regname").value = "";
                        document.getElementById("verify").value = "";
                    }


                }
            }
        }
        else {
            alert("两次密码不一致，请重新确认密码！");
            document.getElementById("regpass").value = "";
            document.getElementById("reregpass").value = "";
        }
    }
}