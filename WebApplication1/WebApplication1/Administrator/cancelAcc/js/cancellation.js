function cancel() {

        var card = document.getElementById("card").value;
        var password = document.getElementById("password").value;

        //判断是否为空
        if (card == "")
            alert("请输入卡号！");
        else if (password == "")
            alert("请输入密码!");
        else {
            //将数据传入后端
            var data = "card=" + card + "&password=" + password;
            var xmlhttp = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject('Microsoft.XMLHTTP');
            xmlhttp.open("POST", "../aspx/cancelAcc.aspx", false);
            xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            xmlhttp.send(data);
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                var str = xmlhttp.responseText;
                alert(str);
            }
        }
    
}
