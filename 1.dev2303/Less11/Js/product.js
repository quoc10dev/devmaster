
var product4 = {
    id : 4,
    name : "Sản phẩm 4",
    sales: "-20%",
    category: "Sản phẩm nam",
    avatar : "https://cdn.tgdd.vn/Products/Images/42/153856/iphone-xi-den-600x600.jpg",
    priceBefore : "10.000.000",
    priceAfter : "8.000.000" 
}
var listProduct = [
        {
            id : 1,
            name : "Sản phẩm 1",
            sales: "-20%",
            category: "Sản phẩm nam",
            avatar : "https://cdn.tgdd.vn/Products/Images/42/153856/iphone-xi-den-600x600.jpg",
            priceBefore : "10.000.000",
            priceAfter : "8.000.000" 
        },
        {
            id : 2,
            name : "Sản phẩm 2",
            sales: "-20%",
            category: "Sản phẩm nam",
            avatar : "https://cdn.tgdd.vn/Products/Images/42/153856/iphone-xi-den-600x600.jpg",
            priceBefore : "10.000.000",
            priceAfter : "8.000.000" 
        },
        {
            id : 3,
            name : "Sản phẩm 3",
            sales: "-20%",
            category: "Sản phẩm nam",
            avatar : "https://cdn.tgdd.vn/Products/Images/42/153856/iphone-xi-den-600x600.jpg",
            priceBefore : "10.000.000",
            priceAfter : "8.000.000" 
        }
    ];

    var listProduct2 = [
        {
            id : 5,
            name : "Sản phẩm 5",
            sales: "-20%",
            category: "Sản phẩm nam",
            avatar : "https://cdn.tgdd.vn/Products/Images/42/153856/iphone-xi-den-600x600.jpg",
            priceBefore : "10.000.000",
            priceAfter : "8.000.000" 
        },
        {
            id : 6,
            name : "Sản phẩm 6",
            sales: "-20%",
            category: "Sản phẩm nam",
            avatar : "https://cdn.tgdd.vn/Products/Images/42/153856/iphone-xi-den-600x600.jpg",
            priceBefore : "10.000.000",
            priceAfter : "8.000.000" 
        },
        {
            id : 7,
            name : "Sản phẩm 7",
            sales: "-20%",
            category: "Sản phẩm nam",
            avatar : "https://cdn.tgdd.vn/Products/Images/42/153856/iphone-xi-den-600x600.jpg",
            priceBefore : "10.000.000",
            priceAfter : "8.000.000" 
        },
        {
            id : 8,
            name : "Sản phẩm 8",
            sales: "-20%",
            category: "Sản phẩm nam",
            avatar : "https://cdn.tgdd.vn/Products/Images/42/153856/iphone-xi-den-600x600.jpg",
            priceBefore : "10.000.000",
            priceAfter : "8.000.000" 
        }
    ];

listProduct.push(product4);
var a = 10;
var b = a;


// yêu cầu: log ra màn hình ra thông tin "Sản phẩm 3" từ listProduct : listProduct[2].name
//          log ra màn hình tất cả giá trị từng thuộc tính của từng đối tượng trong mảng listProduct
var arr = [1, 2, 3,4 ,5]

var view = `<div class="product-item position-rel w-25">
                <div class="top-product position-rel mb-2r">
                    <div class="product-image">
                        <img class="w-100" src="${product4.avatar}" alt="image">
                    </div>
                    <div class="product-sale">
                        <p class="font-15">${product4.sales}</p>
                    </div>
                </div>
                <div class="mid-product">
                    <h4 class="product-cate text-center mb-1r">${product4.category}</h4>
                    <h3 class="product-name text-center mb-1r">
                    ${product4.name}
                    </h3>
                    <div class="text-center mb-1r">
                        <span>${product4.priceAfter} </span>
                        <span>${product4.priceBefore}</span>
                    </div>
                </div>
                <div class="bot-product w-100">
                    <button class="btn-add w-100"> 
                        Thêm vào giỏ hàng
                    </button>
                </div>
            </div>`;


for(var i = 0; i < listProduct.length ; i++){
    console.log("thông tin sản phẩm thứ " + i + " là: ");
    console.log(listProduct[i].id);
    console.log(listProduct[i].name);
    console.log(listProduct[i].priceAfter);
    console.log(listProduct[i].category);
    console.log(listProduct[i].sales);
    // console.log("view: ", view);
}

// yêu cầu: gán biến view hiển thị các thông tin của product4 tương ứng
// yêu cầu: log ra view2 với thông tin từ các đối tượng trong listProduct
function logView(){
    for(var i = 0; i < listProduct.length ; i++){
        let view2 = `<div class="product-item position-rel w-25">
                        <div class="top-product position-rel mb-2r">
                            <div class="product-image">
                                <img class="w-100" src="${listProduct[i].avatar}" alt="image">
                            </div>
                            <div class="product-sale">
                                <p class="font-15">${listProduct[i].sales}</p>
                            </div>
                        </div>
                        <div class="mid-product">
                            <h4 class="product-cate text-center mb-1r">${listProduct[i].category}</h4>
                            <h3 class="product-name text-center mb-1r">
                            ${listProduct[i].name}
                            </h3>
                            <div class="text-center mb-1r">
                                <span>${listProduct[i].priceAfter} </span>
                                <span>${listProduct[i].priceBefore}</span>
                            </div>
                        </div>
                        <div class="bot-product w-100">
                            <button class="btn-add w-100"> 
                                Thêm vào giỏ hàng
                            </button>
                        </div>
                    </div>`;
        console.log("view2:... ", view2);
        $('.list-product').append(view2);
    }
}


function setView(arrProduct){
    $('.list-product').html('');
    for(var i = 0; i < arrProduct.length ; i++){
        renderHTML(arrProduct[i]);
    }
}


function renderHTML(obj){
    let view2 = `<div class="product-item position-rel w-25">
                        <div class="top-product position-rel mb-2r">
                            <div class="product-image">
                                <img class="w-100" src="${obj.avatar}" alt="image">
                            </div>
                            <div class="product-sale">
                                <p class="font-15">${obj.sales}</p>
                            </div>
                        </div>
                        <div class="mid-product">
                            <h4 class="product-cate text-center mb-1r">${obj.category}</h4>
                            <h3 class="product-name text-center mb-1r">
                            ${obj.name}
                            </h3>
                            <div class="text-center mb-1r">
                                <span>${obj.priceAfter} </span>
                                <span>${obj.priceBefore}</span>
                            </div>
                        </div>
                        <div class="bot-product w-100">
                            <button class="btn-add w-100"> 
                                Thêm vào giỏ hàng
                            </button>
                        </div>
                    </div>`;
    console.log("view2:... ", view2);
    $('.list-product').append(view2);
}

// + Chức năng: tìm kiếm
// b1: click vào "tìm kiếm" => lấy value trong ô input search 
// b2: so sánh giá trị trong ô input search với tất cả các thuộc tính "name" trong array danh sách sp
// b3: nếu có thuộc tính name thỏa mãn thì in ra màn hình phân tử có thuộc tính name đó

setView(listProduct2)


function search(){
    var text = $("#search").val();
    var str = text.trim().toLowerCase();
    $('.list-product').html('');
    for(var i = 0; i < listProduct2.length ; i++){
        // if(listProduct2[i].name == text){
        //     renderHTML(listProduct2[i]);
        // }
        if(listProduct2[i].name.trim().toLowerCase().includes(str)){
            renderHTML(listProduct2[i]);
        }

    }
}
// $('.btn-search').click(function(){
//     search();
// })