function deposit2() {
    if (get_cardId("cardId")) {
        var cardId = get_cardId("cardId");
        var depo_amount = document.getElementById("deposit").value;
        var password = document.getElementById("password").value;
        var value ="yes";  //默认发送邮件
        var data = "depo_amount=" + depo_amount + "&password=" + password + "&email=" + value + "&cardId="+cardId;
        var xht = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject('Microsoft.XMLHTTP');
        xht.open("POST", "../aspx/deposit.aspx", false);
        xht.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xht.send(data);
        if (xht.readyState == 4 && xht.status == 200) {
            var str = xht.responseText;
            alert(str);
            if (str == "存款成功") window.location.href = "/Administrator/setInformation/html/setInfo.html";   
        }
    }
    else {
        window.location.href = "/Administrator/cardIdInput/html/cardIdInput.html";   //判断是否输入卡号
        window.event.returnValue = false;
    }
}
