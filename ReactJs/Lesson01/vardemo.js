// Demo var
// local / global

var x=100;
console.log("Gia tri bien x:", x);

function varDemo() {
    var y=200;
    console.log("Gia tri X trong ham",x);
    console.log("Gia tri y trong ham",y);

}

varDemo();
console.log("Gia tri X ngoai ham: ", x);
console.log("Gia tri y ngoai ham: ", y);