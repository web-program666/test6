function submit() {     //ajax传表单
    var cardId =1;             //cardId数值
    var XmlHttp = new XMLHttpRequest();
    var form = new FormData();
    form.append("cardId", cardId);    //创建表单
    XmlHttp.open("Post", "../aspx/userInfo.aspx", false);  //同步
    XmlHttp.send(form);
    text = XmlHttp.responseText;
    alert(text);
    //-1 数据库连接错误
    //-2  数据库语句执行错误
}