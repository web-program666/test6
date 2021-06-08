function withdrawal() {
        //获取用户输入的取款金额、卡密码和邮箱发送意愿
    if (get_cookie("cardId")) {
        var cardId = get_cookie("cardId");
        var password = document.getElementById("password").value;
        var withdraw = document.getElementById("withdraw").value;
        var radios = document.getElementsByName("email");
        var value = 0;
        for (var i = 0; i < radios.length; i++) {
            if (radios[i].checked == true) {
                value = radios[i].value;
            }
        }
        //用AJAX技术将数据传回后台
        var data = "password=" + password + "&withdraw=" + withdraw + "&email=" + value + "&cardId1=" + cardId;
        var xht = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject('Microsoft.XMLHTTP');
        xht.open("POST", "../aspx/withdraw.aspx", false);
        xht.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xht.send(data);
        if (xht.readyState == 4 && xht.status == 200) {
            alert(xht.responseText);
            window.location.href = "/User/welcome/html/welcome.html";
            window.event.returnValue = false;
        }
    }
    else {
        window.location.href = "/User/userOperation/html/index.html";   //判断用户是否登录
        window.event.returnValue = false;
    }


}

