new DataTable("#table-khoahocs");

$(document).ready(function () {
    if (invalidEditModel) {
        $("#cbx-remove-fileqd").prop("checked", false);
    }
});

const donvis_choices_editqd = new Choices($("#donvi-list-select-editqd")[0], {
    searchEnabled: true
});
$("#donvi-list-select-editqd").on('change', function (e) {
    $("#input-iddonvi-editqd").val(e.detail.value);
});

//-------------------

function btnThemKhoaHocClick(idqd) {
    window.open(location.origin + `/KhoaHoc/KhoaHocList?idqd=${idqd}`, "_self");
}

function openKhoaHocDetailPage(idkh) {

}
$("#input-file-quyetdinh-edit").on('change', function () {
    if (this.files.length !== 0) {
        const filePath = this.value;
        const fileName = filePath.split("\\")[filePath.split("\\").length - 1];
        $("#input-filename-quyetdinh-edit").val(fileName);
        $("#cbx-remove-fileqd").prop("checked", false);
    } else {
        $("#cbx-remove-fileqd").prop("checked", true);
        $("#input-filename-quyetdinh-edit").val("");
    }
    $("#button-xemfileqd").css("display", "none");
    $("#btn-huydoifile").css("display", "block");
});
function huydoifile() {
    $("#cbx-remove-fileqd").prop("checked", false);
    $("#button-xemfileqd").css("display", "block");
    $("#input-filename-quyetdinh-edit").val(prefileName);
    $("#btn-huydoifile").css("display", "none");
}
function emptyFileIfNoFileSelected() {
    $("#cbx-remove-fileqd").prop("checked", true);
    $("#input-filename-quyetdinh-edit").val("");
    $("#button-xemfileqd").css("display", "none");
}