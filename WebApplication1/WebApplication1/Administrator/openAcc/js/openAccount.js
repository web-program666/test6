//从后台获取一个可供选择的卡号数组（六位数），没有包含数据库中已有的卡号，并用下拉列表展示
function showCardID() {
        var identityID = document.getElementById("identityID").value;
        var xht = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft XMLHTTP");
        xht.open("POST", "../aspx/OpenAccount.aspx", true);
        xht.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        var data = "msg=" + "0" + "&identityID=" + identityID;    //给后台判断请求
        xht.send(data);
        xht.onreadystatechange = function () {
            if (xht.readyState == 4 && xht.status == 200) {
                var reg1 = new RegExp("ID");
                var str = xht.responseText;
                if (str.match(reg1)) {
                    alert(str);
                }
                else {
                    var cardIDArr = JSON.parse(str);
                    var objSelect = document.getElementById("select1");
                    objSelect.options.length = 0;    //清空下拉列表
                    for (var key in cardIDArr) {
                        objSelect.add(new Option(cardIDArr[key], cardIDArr[key]));
                    }
                    document.getElementById("identityID").setAttribute("readOnly", 'true');   //将identityID输入框改为只读，无法修改
                    document.getElementById("show").innerText = "换下一批";
                }
            }
        }

}
function submit3() {
        var name = document.getElementById("name").value;
        var identityID = document.getElementById("identityID").value;
        var Email = document.getElementById("Email").value;
        var phone = document.getElementById("phone").value;
        var password = document.getElementById("password").value;
        var confirmPassword = document.getElementById("confirmPassword").value;
        var money = document.getElementById("money").value;
        var sel = document.getElementById("select1");
        var index = sel.selectedIndex;
        var selCardID = sel.options[index].value;
        if (password != confirmPassword) {
            document.getElementById("confirmPassword").value = "";
            alert("两次密码不一致，请重新确认密码！");
        }
        else {
                var data = "msg=" + "1" + "&name=" + name + "&identityID=" + identityID + "&Email=" + Email + "&phone=" + phone + "&selCardID=" + selCardID + "&confirmPassword=" + confirmPassword + " &money=" + money;
                var xht1 = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft XMLHTTP");
                xht1.open("POST", "../aspx/OpenAccount.aspx", true);
                xht1.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                xht1.send(data);
                xht1.onreadystatechange = function () {
                    if (xht1.readyState == 4 && xht1.status == 200) {
                        reset("t1", "input");
                        document.getElementById("select1").options.length = 0;    //清空
                        alert(xht1.responseText);
                        if (xht1.responseText.substring(0,6)=="办理开户成功") window.location.href = "/人脸识别/Reg.html?cardId=" + selCardID;
                    }
                }
        }
}



