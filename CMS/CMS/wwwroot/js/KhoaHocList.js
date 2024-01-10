

new DataTable("#table-quyetdinhs");
function btnThemKhoaHocClick() {
    $("#new-khoahoc-form-container").slideDown();
}
function cancelThemKhoaHocForm() {
    $("#new-khoahoc-form-container").slideUp();
}

$("#input-start-date").daterangepicker({
    singleDatePicker: true,
    autoApply: true,
    applyButtonClasses: false,
    autoUpdateInput: true,
    startDate: moment().startOf('hour'),
    locale: {
        format: 'DD/MM/YYYY'
    }
}, function (start) {
    $("#input-start-date").val(start.format('DD/MM/YYYY'));
});

$("#input-end-date").daterangepicker({
    singleDatePicker: true,
    autoApply: true,
    applyButtonClasses: false,
    autoUpdateInput: true,
    startDate: moment().startOf('hour'),
    locale: {
        format: 'DD/MM/YYYY'
    }
}, function (start) {
    $("#input-end-date").val(start.format('DD/MM/YYYY'));
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

function openKhoaHocDetailPage(id){
    window.open(location.origin + `/KhoaHoc/KhoaHocDetail?idkh=${id}`, '_self');
}