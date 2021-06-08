function deposit1() {
    if (get_cookie("cardId")) {
        var cardId = get_cookie("cardId");
        var depo_amount = document.getElementById("deposit").value;
        var password = document.getElementById("password").value;
        var radios = document.getElementsByName("email");
        var value = 0;
        for (var i = 0; i < radios.length; i++) {
            if (radios[i].checked == true) {
                value = radios[i].value;
                break;
            }
        }
        var data = "depo_amount=" + depo_amount + "&password=" + password + "&email=" + value + "&cardId"+cardId;
        var xht = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject('Microsoft.XMLHTTP');
        xht.open("POST", "../aspx/deposit.aspx", false);
        xht.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xht.send(data);
        if (xht.readyState == 4 && xht.status == 200) {
            var str = xht.responseText;
            alert(str);
            window.location.href = "/User/welcome/html/welcome.html";
            window.event.returnValue = false;
        }
    }
    else {
        window.location.href = "/User/userOperation/html/index.html";   //判断用户是否登录
        window.event.returnValue = false;
    }
}
