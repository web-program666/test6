function submit() {   //提交页面上的用户信息，传给后台更新数据库
    var con = document.getElementById("infomation");
    var infos = document.getElementsByName("info");
    var names = [];
    var values = [];
    var data = "";

    //从页面中获取用户的基本信息
    for (var i = 0; i < infos.length; i++) {
        if (i < 4) {                 //infos[i].innerText.toString() == ""
            names.push(infos[i].getAttribute("id"));
            values.push(infos[i].innerText.toString());
            data += "&" + names[i] + "=" + values[i];
        }
        else {
            names.push(infos[i].getAttribute("id"));
            values.push(infos[i].value);
            data += "&" + names[i] + "=" + values[i];
        }
    }
    //获得状态的的选项值
    var radios = document.getElementsByName("state");
    var value;
    for (var i = 0; i < radios.length; i++) {
        if (radios[i].checked == true)
            value = radios[i].value;
    }
    data += "&state=" + value;
    var xht = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject('Microsoft.XMLHTTP');
    xht.open("POST", "../aspx/setInformation.aspx", true);
    xht.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xht.send(data);
    xht.onreadystatechange = function () {
        if (xht.readyState == 4 && xht.status == 200) {
            var str = xht.responseText;
            alert(str);

        }
    }
}
window.onload = function onload() {       //页面加载时进行操作，将从后台读取的用户信息显示
/* alert("来吧，展示！");*/
    if (get_cardId("cardId")) {
        var cardId =get_cardId("cardId");//cardId号
        var infos = document.getElementsByName("info");
        var names = [];
        var radios = document.getElementsByName("state");
        for (var i = 0; i < infos.length; i++) {
            if (i < 4) {                 //infos[i].innerText.toString() == ""
                names.push(infos[i].getAttribute("id"));
            }
            else {
                names.push(infos[i].getAttribute("id"));
            }
        }
        var xht = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject('Microsoft.XMLHTTP');
        xht.open("POST", "../aspx/setInformation.aspx", true);
        xht.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        var data = "msg=" + "0";

        data += "&cardId=" + cardId;
        xht.send(data);
        xht.onreadystatechange = function () {
            if (xht.readyState == 4 && xht.status == 200) {
                var str = xht.responseText;
                var obj = JSON.parse(str)
                for (var i = 0; i < infos.length; i++) {
                    if (i < 4) {
                        infos[i].innerText = obj[names[i]];
                    }
                    else {
                        infos[i].value = obj[names[i]];
                    }
                }
                if (obj["state"] == "1") {
                    radios[0].checked = true;
                    radios[1].checked = false;
                }
                else {
                    radios[0].checked = false;
                    radios[1].checked = true;
                }
            }
        }
    }
}
