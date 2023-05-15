var listItemCard = [
    {
        id: 1,
        name : "C++",
        content : "Some quick example text to build on the card title and make up the bulk of the card's content.",
        image : "https://devmaster.edu.vn/uploads/images/devmaster-campaign/devmaster-lap-trinh-vien-website-php-va-mysql-chuyen-nghiep.png",
        price : 100000,
        limit : 5
    }
    ,{
        id: 2,
        name : "devmaster",
        content : "Some quick example text to build on the card title and make up the bulk of the card's content.",
        image : "https://devmaster.edu.vn/uploads/images/devmaster-campaign/devmaster-lap-trinh-vien-website-php-va-mysql-chuyen-nghiep.png",
        price : 80000,
        limit : 100,
    },
]

function setItemCard(arr){
    $("#list-card").html('');
    for(var  i = 0; i < arr.length ; i++){
        renderItemCard(arr[i]);
    }
}
function renderItemCard(obj){
    var item = ` <div class="col-12">
                    <div class="card mb-3" id="card-${obj.id}" data-id="${obj.id}">
                        <img class="card-img-top" src="${obj.image}" alt="Card image cap">
                        <div class="card-body">
                        <h5 class="card-title">${obj.name}</h5>
                        <p class="card-text">${obj.content}</p>
                        <div class="d-flex justify-content-between">
                            <div>
                                Giá thành sản phẩm: ${obj.price} <span>vnđ</span>
                            </div>
                            <input type="number" id="qlty-${obj.id}">
                        </div>
                        
                        <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
                        
                        </div>
                    </div>
                </div>`;
    $("#list-card").append(item);
}

$(document).ready(function(){
    setItemCard(listItemCard);
})

var result = 0;
function getQlty(){
    debugger;
   for(var j = 0; j< listItemCard.length ; j++){
        var number = $("#qlty-" + listItemCard[j].id).val();
        if(number > listItemCard[j].limit){
            alert(listItemCard[j].name + " chỉ còn " + listItemCard[j].limit + " sản phẩm");
        } else{
            var payItem = total(0, listItemCard[j].price, number);
            result = result + payItem;
        }
   }
   $("#total-price").text(result);
   result = 0;
}
function total(id, price, qlty) { 
    var sum = price * qlty;
    return parseInt(sum);
 }



 // b1: so sánh số lượng sp mua với số lượng sp còn lại của sp tương ứng 
 //     1.1: lấy ra số lượng
//      1.2: đem so sánh với sp tương ứng
 // b2: true : tính tổng tiền phải trả cho sp tương ứng
//      false: ....
// b3: (sau khi thực hiện xong với tất cả sp trong giỏ) in ra tổng tiền phải trả cho giỏ hàng


// lấy - tính - hiển thị