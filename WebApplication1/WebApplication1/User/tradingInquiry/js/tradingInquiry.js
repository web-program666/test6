function submit() {     //ajax传表单
    var cardId =1;             //cardId数值
    var XmlHttp = new XMLHttpRequest();
    var form = new FormData();
    form.append("cardId", cardId);    //创建表单
    XmlHttp.open("Post", "../aspx/tradingInquiry.aspx",false);  //同步
    XmlHttp.send(form);
    text = XmlHttp.responseText;
    print(text);//处理返回的json
    //XmlHttp.onreadystatechange = function () {
    //    if (XmlHttp.readyState == 4 && XmlHttp.status == 200) {
    //        text = XmlHttp.responseText;
    //        return print(text);//处理返回的json
    //    }
    //};   //异步
    //XmlHttp.send(form);
}


function print(text) {    //数据显示
    data = eval("(" + text + ")"); //将字符串转换为json
    str = "<tr class='hide'><th width=350>交易时间</th><th>转入</th><th>转出</th><th width=350>被转账人卡号</th></tr>";
    for (var i in data) {     //i下标
        str = str + "<tr class='hide'>";
        for (var key in data[i]) {    //key键名
            if (key != "exchangeMoney") {
                str = str + "<td>" + data[i][key] + "</td>";
            }
            else {
                if (data[i][key] > 0) str = str + "<td>" + data[i][key] + "</td><td></td>";
                else str = str + "<td></td>" + "<td>"+(-1)*data[i][key] + "</td>";
            }
        }
        str = str + "</tr>";
    }
    document.getElementById("m").innerHTML = str;


}