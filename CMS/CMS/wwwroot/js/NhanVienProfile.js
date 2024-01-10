$(document).ready(function () {
    if (invalidNVModel) {
        $(".texts-info-nhanvien").css("display", "none");
        $(".inputs-edit-nhanvien").attr("style", "display:block !important");
    }
});

const chucdanh_choices = new Choices($("#chucdanh-list-select")[0], { searchEnabled: true });
$("#chucdanh-list-select").on('change', function (e) {
    $("#input-idchucdanh-nvedit").val(e.detail.value);
});
const phongban_choices = new Choices($("#phongban-list-select")[0], { searchEnabled: true });
$("#phongban-list-select").on('change', function (e) {
    $("#input-idphongban-nvedit").val(e.detail.value);
});
function enableEditProfile() {
    $(".texts-info-nhanvien").css("display", "none");
    $(".inputs-edit-nhanvien").attr("style", "display:block !important");
}

function viewAvt(imgPath) {
    window.open(location.origin + imgPath, "_blank");
}

function renewFile() {
    $("#input-file-avt").val(null);
    if ($("#origin-avt").val() !== "") {
        $("#img-avt").prop("src", "../images/avatar-placeholder.png");
        $("#cbx-isRemoveFile").prop("checked", true);
        $("#btn-cancelAvtChange").css("display", "block");
    }
}

$("#input-file-avt").on("change", function () {
    if (this.files.length > 0) {
        $("#img-extension-validate").html("");
        const validExtension = ["png", "jpeg", "jpg"];
        const imageName = this.value;
        const imgExtension = imageName.replace(/^.*\./, '');
        if (!validExtension.includes(imgExtension)) {
            $("#img-extension-validate").html("Vui lòng chọn ảnh có định dạng: png, jpeg hoặc jpg.");
        } else {
            const img = this.files[0];
            const fileReader = new FileReader();
            fileReader.onload = function (event) {
                $("#img-avt").prop("src", event.target.result);
                $("#btn-cancelAvtChange").css("display", "block");
            }
            fileReader.readAsDataURL(img);
        }
        $("#cbx-isRemoveFile").prop("checked", false);
    }
});

function cancelAvtChange() {
    var originAvtPath = $("#origin-avt").val();
    if (originAvtPath === "") {
        $("#img-avt").prop("src", "../images/avatar-placeholder.png");
    } else {
        $("#img-avt").prop("src", `..${originAvtPath}`);
    }   
    $("#btn-cancelAvtChange").css("display", "none");
}
