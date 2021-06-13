function submit() {     //ajax传表单
    if (get_cookie("cardId")) {
        var cardId = get_cookie("cardId");             //cardId数值
        var XmlHttp = new XMLHttpRequest();
        var form = new FormData();
        form.append("cardId", cardId);    //创建表单
        XmlHttp.open("Post", "../aspx/userInfo.aspx", false);  //同步
        XmlHttp.send(form);
        text = XmlHttp.responseText;
        print(text);
        //-1 数据库连接错误
        //-2  数据库语句执行错误
    } 
    else {
        window.location.href = "/User/userOperation/html/index.html";   //判断用户是否登录
        window.event.returnValue = false;
    }

}
function print(text) {    //数据显示
    data = eval("(" + text + ")"); //将字符串转换为json
    str = "";
    a = ["卡号", "姓名", "身份证号", "邮箱", "电话"];
    j = 0;
     for (var key in data[0]) {    //key键名
            
            str = str + '<tr class="hide"><th width=200>'+a[j]+'</th><th width=350 style="border - bottom - width: 30px">'+data[0][key]+'</th></tr>';
            j=j+1;
        }
    document.getElementById("m").innerHTML = str;
}