function report() {   //挂失
    var r = confirm("确定冻结吗？");
    if (r == true) submit_resport();
}

function submit_resport() {    
    var cardId = 1;             //cardId数值
    var XmlHttp = new XMLHttpRequest();
    var form = new FormData();
    form.append("cardId", cardId);    //创建表单
    XmlHttp.open("Post", "../../accFreeze/aspx/accFreeze.aspx", false);  //这个路径要以html路径为基准！
    XmlHttp.send(form);
    text = XmlHttp.responseText;
    //-1 数据库连接错误
    //-2  数据库语句执行错误
    //0 修改失败
    //1 修改成功
    if (text == 1) {
        alert("挂失成功！");
        //此处要退出登录
    }
    else {
        alert("挂失失败！");
    }
}