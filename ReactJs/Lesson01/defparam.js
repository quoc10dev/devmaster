// Tham số mặc định, khai báo ở phần  danh sách tham só

function add(num1, num2=200) {

    let res = num1 + num2;
    console.log(num1 + "+" + num2 + "=" + res);
}

add(100,300);
add(100);
var fn_add = (num3,num4) => {
    return num3 + num4;
}

console.log("ketqua:",fn_add(300,400));
fn_add(300,400);