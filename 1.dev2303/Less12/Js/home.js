var listItem = [
    {
        id: 1,
        name : "C++",
        content : "Some quick example text to build on the card title and make up the bulk of the card's content.",
        image : "https://devmaster.edu.vn/uploads/images/course/java_web.jpg",
    },{
        id: 2,
        name : "devmaster",
        content : "Some quick example text to build on the card title and make up the bulk of the card's content.",
        image : "https://devmaster.edu.vn/uploads/images/course/android.jpg",
    },{
        id: 3,
        name : "IT",
        content : "Some quick example text to build on the card title and make up the bulk of the card's content.",
        image : "https://devmaster.edu.vn/uploads/images/course/java_web.jpg",
    },{
        id: 4,
        name : "ASP NET",
        content : "Some quick example text to build on the card title and make up the bulk of the card's content.",
        image : "https://devmaster.edu.vn/uploads/images/course/android.jpg",
    },{
        id: 5,
        name : "NET CORE",
        content : "Some quick example text to build on the card title and make up the bulk of the card's content.",
        image : "https://devmaster.edu.vn/uploads/images/course/java_web.jpg",
    },{
        id: 6,
        name : "Salary",
        content : "Some quick example text to build on the card title and make up the bulk of the card's content.",
        image : "https://devmaster.edu.vn/uploads/images/course/android.jpg",
    },
]

function setItem(arr){
    $("#list-items").html('');
    for(var  i = 0; i < arr.length ; i++){
        renderItem(arr[i]);
    }
}
function renderItem(obj){
    var item = `<div class="col-3 mb-4">
                    <div class="card">
                        <img class="card-img-top" src="${obj.image}" alt="Card image cap">
                        <div class="card-body">
                        <h5 class="card-title">${obj.name}</h5>
                        <p class="card-text">${obj.content}</p>
                        <a href="#" class="btn btn-primary">Go somewhere</a>
                        </div>
                    </div>
                </div>`;
    $("#list-items").append(item);
}

$(document).ready(function(){
    setItem(listItem);

})
