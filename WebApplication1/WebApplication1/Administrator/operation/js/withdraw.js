function withdrawal() {
        //获取用户输入的取款金额、卡密码和邮箱发送意愿
    if (get_cardId("cardId")) {
        var cardId = get_cardId("cardId");
        var password = document.getElementById("password").value;
        var withdraw = document.getElementById("withdraw").value;
        var value = 1;
        //用AJAX技术将数据传回后台
        var data = "password=" + password + "&withdraw=" + withdraw + "&email=" + value + "&cardId=" + cardId;
        var xht = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject('Microsoft.XMLHTTP');
        xht.open("POST", "../aspx/withdraw.aspx", false);
        xht.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xht.send(data);
        if (xht.readyState == 4 && xht.status == 200) {
            alert(xht.responseText);
            if (xht.responseText == "取款成功") {
                window.location.href = "/Administrator/setInformation/html/setInfo.html";
                window.event.returnValue = false;
            }
        }
    }
    else {
        window.location.href = "/Administrator/cardIdInput/html/cardIdInput.html";   //判断是否输入卡号
        window.event.returnValue = false;
    }


}

