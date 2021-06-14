function transfer() {
    if (get_cardId("cardId")) {
        var cardID1 = get_cardId("cardId");
        var password = document.getElementById("password").value;
        var transferAmount = document.getElementById("transfer_money").value;
        var cardID2 = document.getElementById("transfer_account").value;
        var value="yes";
        var data = "password=" + password + "&transferAmount=" + transferAmount + "&cardID2=" + cardID2 + "&email=" + value + "&cardID1=" + cardID1;
        var xht = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft XMLHTTP");
        xht.open("POST", "../aspx/transfer.aspx", false);
        xht.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xht.send(data);
        /* xht.onreadystatechange = function () {*/
        if (xht.readyState == 4 && xht.status == 200) {
            var str = xht.responseText;
            var reg1 = new RegExp("密码错误");
            var reg2 = new RegExp("金额不足");
            var reg3 = new RegExp("收款方");

            if (str.match(reg1)) {
                document.getElementById("password").value = "";
            }
            else if (str.match(reg1)) {
                document.getElementById("transfer_money").value = "";
            }
            else if (str.match(reg3)) {
                document.getElementById("transfer_account").value = "";
            }
            alert(str);
            if (str.substring(0, 4) == "转账成功") window.location.href = "/Administrator/setInformation/html/setInfo.html";

        }
    }
    else {
        window.location.href = "/Administrator/cardIdInput/html/cardIdInput.html";   //判断是否输入卡号
        window.event.returnValue = false;
    }
}