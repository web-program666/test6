function submit() {     //ajax传表单
    var cardId = document.getElementById("cardId").value;
    var password = document.getElementById("password").value;
    var XmlHttp = new XMLHttpRequest();
    var form = new FormData();
    form.append("cardId", cardId);    //创建表单
    form.append("password", password);//创建表单
    XmlHttp.open("Post", "../aspx/user_login.aspx", false);
    XmlHttp.send(form);
    flag = XmlHttp.responseText;
    result(flag);
    //XmlHttp.onreadystatechange = function () {
    //    if (XmlHttp.readyState == 4 && XmlHttp.status == 200) {
    //        flag = XmlHttp.responseText;
    //        result(flag);
    //    }
    //};   //异步
    //XmlHttp.send(form);
}
function result(flag) {
    if (flag == 1) {
        alert("登陆成功！");
        window.location.href = "/User/welcome/html/welcome.html";//跳转
        window.event.returnValue = false;  
        
    }
    else if (flag == -1) {
        alert("账户不存在!");
        document.getElementById("cardId").value = "";
        document.getElementById("password").value = "";
    }
    else if (flag == -2) {
        alert("账户冻结，禁止登陆！");
    }
    else {
        alert("账户或密码错误！");
        document.getElementById("cardId").value = "";
        document.getElementById("password").value = "";
    }
}
