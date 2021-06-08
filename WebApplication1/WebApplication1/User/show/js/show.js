function show1() {
    if (get_cookie("cardId")) {
        var cardId = get_cookie("cardId");
        var xht = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject('Microsoft.XMLHTTP');
        form = "cardId=" + cardId;
        xht.open("POST", "../../show/aspx/show.aspx", true);
        xht.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xht.send(form)
        xht.onreadystatechange = function () {
            if (xht.readyState == 4 && xht.status == 200) {
                document.getElementById("balance").innerHTML = xht.responseText;
            }
        }
    }
    else {
        window.location.href = "/User/userOperation/html/index.html";   //判断用户是否登录
        window.event.returnValue = false;
    }
}