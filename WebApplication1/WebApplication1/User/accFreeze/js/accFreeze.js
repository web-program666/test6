function report() {   //挂失 
    cardId = get_cookie("cardId");  //判断是否处于登录状
    var r = confirm("确定冻结吗？");
    if (r == true) submit_resport(cardId);
}

function submit_resport(cardId) {    
    var XmlHttp = new XMLHttpRequest();
    var form = new FormData();
    form.append("cardId", cardId);    //创建表单
    XmlHttp.open("Post", "/User/accFreeze/aspx/accFreeze.aspx", false);  //这个路径要以html路径为基准！
    XmlHttp.send(form);
    text = XmlHttp.responseText;
    //-1 数据库连接错误
    //-2  数据库语句执行错误
    //0 修改失败
    //1 修改成功
    if (text == 1) {
        window.document.cookie = "cardId" + '=' + "";//把cookie置空  起删除作用
        alert("挂失成功！");
        window.location.href = "/User/userOperation/html/index.html";
        window.event.returnValue = false; 
        
    }
    else {
        alert("挂失失败！");
    }
}