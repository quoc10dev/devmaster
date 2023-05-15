// alert('Chào mừng bạn đến với Devmaster -  Khóa học JS cơ bản cách thứ 3');


// Biến 
// Quy tắc khai báo
// C1:  data_type    name ; 
//   <kiểu_dữ liệu>  <tên biến>

// C2:   <data_type>  name = <value> ;

// data_type :  định nghĩa    : int number = 15; string str = "Devmaster";
//              tự định nghĩa : var x = 15;   ==> kiểu dữ liệu cảu x là dạng số
//                              let y = "devmaster"; ==> kiểu dữ liệu của y là chuỗi


// toán tử trong JS 

// số học: + ; -; * ; / ; %

var number1 = 10; // number
var number2 = "20"; // string
var str1 = "Devmaster";
var str2 = "50";

var totalNumber = number1 + number2;

var string = str1 + str2;
document.write(string);
console.log(totalNumber);
console.log(string);
// lưu ý:
// 1,       số + số = số
// 2,       chuỗi + chuỗi = chuỗi 
// 3,       chuỗi + số = chuỗi


var a = 10;
var b = "10";

var c = a + b;
var d = parseInt(a) + parseInt(b)
console.log("c = a + b = " + a + " + " + b + " = " + c);
console.log("d = a + b = " + a + " + " + b + " = " + d);


// toán tử so sánh
// so sánh về value : == ; > ; < ; >= ; <=

// so sánh cả value và data_type :  ===
//debugger;
if( a == b){
    console.log("a và b có giá trị bằng nhau");
    if(a === b){
        console.log("a và b cùng kiểu dữ liệu");

    }
}else{

}
console.log(typeof(a));
console.log(typeof(b));


var x = 10;
var y = 15;

var z = (x > y) ? (x - y) : (y - x);
// var z = (x - y);
console.log("z = " + z);

// biện luận số nghiệm của phương trình bậc nhất 1 ẩn có dạng: m*x + n = 0

var m = 0;
var n = 0;


if( m == 0){ 
    if(n == 0){
        // 0*x + 0 = 0
        console.log("phương trình có số nghiệm là: vô số nghiệm ")
    }else{
        // 0*x + n = 0
        console.log("phương trình có số nghiệm là: vô nghiệm")
    }
} else{
    console.log("phương trình có số nghiệm là: 1 nghiệm là x = -n/m")
}


// a*x^2 + b*x + c = 0

//console.log("phương trình có số nghiệm là: ")

for(var i = 0; i < 10; i++){
    console.log(i + 1);
}
console.log("đã hết for");

// bước 1: khởi tạo biến đếm
// bước 2: kiểm tra điều kiện
//      nếu true:  thực thi các câu lệnh trong khối {}
//                 tương tác biến đếm 
//                 trở lại bước 2
//      nếu false:  dừng vòng lặp

// khai bảo một số bất kỳ p > 0; log ra màn hình tất cả các giá trị  thỏa mãn điều kiệu là số chẵn và nhỏ hơn p

var p = 20;
for(var i = 1; i < p; i++){
    if(i % 2 == 0){
        console.log(i);
    }else{
        console.log("số lẻ: " + i);

    }
}

// mảng : array

var array_name = [1, 5, 8 , 15, 90, 300];
var array_str = ["a", "b", "dev", "string"];

console.log(array_name);
// độ dài mảng: length 
// vd :    array_str.length  ===> 4
// lấy phần tử: <array_name>[<index>]
// vd array_str[3] ===> string
//
for(var i = 0; i < array_str.length ; i++ ){
    console.log("phần tử thứ " + i + " trong mảng array_str là: " + array_str[i]);
}



// khai báo một mảng chưa các phần tử có kiểu dữ liệu dạng int
// 1, log tất cả phần tử trong mảng có vị trí là số lẻ
// 2, chỉ log các phần tử là số chẵn
// 3, tính tổng các phần tử có giá trị là số lẻ và log ra màn hình

// 4, tính tổng các phần tử có giá trị là số lẻ và có vị trí là số chẵn sau đó log ra màn hình
// 5, tính tổng các phần tử có giá trị chia hết cho n và log ra màn hình
// 6, tính tổng các phần tử có vị trí chia hết cho 3 và log ra màn hình

var arr = [3, 4, 5, 7, 8, 9, 10, 15, 30];
for(var i = 0; i < arr.length ; i++){
    if( i % 2 != 0){
        console.log("các phần tử có giá trị thỏa mãn yc 1 là: " + arr[i]);
    }
}

// yêu cầu 2
console.log("yc 2:")
for(var i = 0; i < arr.length ; i++){
    if( arr[i] % 2 == 0){
        console.log("các phần tử có giá trị thỏa mãn yc 2 là: " + arr[i]);
    }
}

// yêu cầu 3:
console.log("yc 3:")
var total = 0;
for(var i = 0; i < arr.length ; i++){
    if( arr[i] % 2 != 0){
        total = total + arr[i];
    }
}
console.log("kết quả của yc 3 là: " + total);
// 3  ==> 0 + 3 = 3
// 4  ==> 3
// 5  ==> 3 + 5 = 8
// 7  ==> 8 + 7 = 15
// 8  ==> 15
// 9  ==> 15 + 9
// 10 ==> 24
// 15 ==> 24 + 15
// 30 ==> 39
