function get_photo() {
    var cardId = get_cardId("cardId");
    var xht = new XMLHttpRequest();
    var form = new FormData();
    form.append("cardId", cardId);
    xht.open("Post", "/Administrator/photo/photo.aspx", false);
    xht.send(form);
    if (xht.readyState == 4 && xht.status == 200) {
        var photo = xht.responseText;
        if (photo.toString() == "-1") alert("图片不存在");
        else {
            document.getElementById("img").setAttribute("src", "data:image/png;base64," + photo.toString());
            document.getElementById("img1").setAttribute("src", "data:image/png;base64," + photo.toString());
            document.getElementById("img2").setAttribute("src", "data:image/png;base64," + photo.toString());

            /*return "data:image/png;base64," + photo.toString();*/
        }
    }
}
get_photo();