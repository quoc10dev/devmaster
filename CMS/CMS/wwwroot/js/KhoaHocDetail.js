$(document).ready(function () {
    if (invalidEditModel) {
        $("#edit-card").parent().css("display", "block");
        $("#edit-card").CardWidget("expand");
        $("#add-card").CardWidget("collapse");
        $('html, body').scrollTop($("#edit-card").parent().offset().top - 330);
    }
});

new DataTable("#table-capccs");

const nvChoices = new Choices($("#nhanvien-list-select")[0], { searchEnabled: true });
$("#nhanvien-list-select").on('change', function (e) {
    $("#input-id-nhanvien").val(e.detail.value);
});
const loaidtChoices = new Choices($("#loaidaotao-list-select")[0], { searchEnabled: true });
$("#loaidaotao-list-select").on('change', function (e) {
    $("#input-id-loaidaotao").val(e.detail.value);
});
const loaidtEditChoices = new Choices($("#loaidaotao-list-select-edit")[0], { searchEnabled: true });
$("#loaidaotao-list-select-edit").on('change', function (e) {
    $("#input-id-loaidaotao-edit").val(e.detail.value);
});
const gvChoice = new Choices($("#giaovien-list-select")[0], { searchEnabled: true });
$("#giaovien-list-select").on('change', function (e) {
    $("#input-id-giaovien").val(e.detail.value);
});

const qdChoice = new Choices($("#quyetdinh-list-select")[0], { searchEnabled: true });
$("#quyetdinh-list-select").on('change', function (e) {
    $("#input-id-quyetdinh").val(e.detail.value);
});

const ccChoice = new Choices($("#chungchi-list-select")[0], { searchEnabled: true });
$("#chungchi-list-select").on('change', function (e) {
    $("#input-id-chungchi").val(e.detail.value);
});

var prefileName = "";
var isEdit = false;
function editCapCC(idnv,name,ghichu,filePathCC,idLoaiDT){
    $("#input-tennhanvien-edit").val(name);
    $("#input-id-nhanvien-edit").val(idnv);
    prefileName = filePathCC.split("/")[4];
    $("#input-ghichu-edit").val(ghichu);
    $("#input-filename-chungchi-edit").val(prefileName);
    $("#button-xemfilecc a").attr('href', `..${filePathCC}`);
    loaidtEditChoices.setChoiceByValue(`${idLoaiDT}`);
    $("#input-id-loaidaotao-edit").val(idLoaiDT);
    $("#filecc-path").val(filePathCC);

    $("#add-card").CardWidget("collapse");
    if ($("#edit-card").parent().css("display") === "none") {    
        setTimeout(function () {
            $("#edit-card").parent().css("display", "block");
            $("#edit-card").CardWidget("expand");
            $('html, body').scrollTop($("#edit-card").parent().offset().top - 80);
            isEdit = true;
        }, 400);
    }
    
}
$('#add-card').on('collapsed.lte.cardwidget', function () {
    if (isEdit) {
        $('html, body').scrollTop($("#edit-card").parent().offset().top - 80);
    }
});
$('#add-card').on('expanded.lte.cardwidget', function () {
    isEdit = false;
    $("#edit-card").parent().css("display", "none");
    $('html, body').scrollTop($("#add-card").parent().offset().top);
});

$("#input-file-chungchi-edit").on('change', function () {
    if (this.files.length !== 0) {
        const filePath = this.value;
        const fileName = filePath.split("\\")[filePath.split("\\").length - 1];
        $("#input-filename-chungchi-edit").val(fileName);
        $("#cbx-remove-filecc").prop("checked", false);
    } else {
        $("#cbx-remove-filecc").prop("checked", true);
        $("#input-filename-chungchi-edit").val("");
    }
    $("#button-xemfilecc").css("display", "none");
});

function emptyFileIfNoFileSelected() {
    $("#cbx-remove-filecc").prop("checked", true);
    $("#input-filename-chungchi-edit").val("");
    $("#button-xemfilecc").css("display", "none");
}

function huydoifile() {
    $("#cbx-remove-filecc").prop("checked", false);
    $("#button-xemfilecc").css("display", "block");
    $("#input-filename-chungchi-edit").val(prefileName);
}

function removeNvOutKh(idNV, idKH, name) {
    Swal.fire({
        title: 'Bạn có chắc?',
        text: `Xóa nhân viên: ${name} ra khỏi khóa học`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: "Hủy",
        confirmButtonText: 'Đồng ý xóa'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/KhoaHoc/DeleteNhanVienOut',
                contentType: 'application/x-www-form-urlencoded',
                data: { idnv: idNV, idkh: idKH },
                success: function (result) {
                    if (result) {
                        Swal.fire({
                            title: 'Đã xóa!',
                            text: `Nhân viên ${name} đã được xóa khỏi khóa học`,
                            icon: 'success',
                            didClose: function () {
                                location.reload();
                            }
                        });
                    }
                },
                error: function (res) {
                }
            });

        }
    });
}

