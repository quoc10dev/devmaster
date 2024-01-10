new DataTable('#example2');

const phongban_choices_newnv = new Choices($('#phongban-list-select-newnv')[0],
    {
        searchEnabled: true
    });
$('#phongban-list-select-newnv').on('change', function (e) {
    $("#input-id-phongban").val(e.detail.value);
});
const chucdanh_choices_newnv = new Choices($('#chucdanh-list-select-newnv')[0],
    {
        searchEnabled: true
    });
$('#chucdanh-list-select-newnv').on('change', function (e) {
    $("#input-id-chucdanh").val(e.detail.value);
});

const phongban_choices_search = new Choices($('#phongban-list-select-search')[0],
    {
        searchEnabled: true
    });
$('#phongban-list-select-search').on('change', function (e) {
    $("#input-idphongban-search").val(e.detail.value);
});
const chucdanh_choices_search = new Choices($('#chucdanh-list-select-search')[0],
    {
        searchEnabled: true
    });
$('#chucdanh-list-select-search').on('change', function (e) {
    $("#input-idchucdanh-search").val(e.detail.value);
});

$("#avt-nhanvien-new").on("change", function () {
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
            $("#preview-avt-nhanvien-new").prop("src", event.target.result);
        }
        fileReader.readAsDataURL(img);
    }
});

function btnThemNhanVienClick() {
    $("#new-nhanvien-form-container").slideDown();
    $("html, body").scrollTop(0);
}
function cancelThemNhanVienForm() {
    $("#new-nhanvien-form-container").slideUp();
}

function openPageNVProfile(idNV) {
    window.open(location.origin + `/NhanVien/NhanVienProfile?idnv=${idNV}`,'_blank');
}

function deleteNhanVien(id,name,filePath) {
    Swal.fire({
        title: 'Bạn có chắc?',
        text: `Xóa nhân viên: ${name}`,
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
                url: '/NhanVien/DeleteNhanVien',
                contentType: 'application/x-www-form-urlencoded',
                data: {idnv:id, avtPath: filePath},
                success: function (result) {
                    if (result) {
                        Swal.fire({
                            title: 'Đã xóa!',
                            text: `Nhân viên ${name} đã được xóa`,
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

function clearSearchNhanVien() {
    window.open(location.origin + "/NhanVien/NhanVienList", "_self");
}

try {

    $('#nhanvien-birthday-input').daterangepicker({
        singleDatePicker: true,
        autoApply: true,
        applyButtonClasses: false,
        autoUpdateInput: true,
        startDate: moment().startOf('hour'),
        locale: {
            format: 'DD/MM/YYYY'
        }
    }, function (start) {
        $('#nhanvien-birthday-input').val(start.format('DD/MM/YYYY'));
    });


} catch (er) { console.log(er); }
/*function showModalSetRole(user_name, choicesList) {
    var newNhanVienForm = $('<div class="gene-choices-container" style="width:400px;">' +
        '<select id="gene-input-select" multiple></select></div>');

    Swal.fire({
        title: `<strong>Setting Role User ${user_name} </strong>`,
        html: input,
        showCloseButton: true,
        focusConfirm: false,
        confirmButtonText: 'Save',
        confirmButtonColor: '#212529',
        allowOutsideClick: false,
        confirmButtonAriaLabel: 'Lưu lại thay đổi.',
        didOpen: () => {
            const choices = new Choices($('#gene-input-select')[0], {
                removeItemButton: true,
                choices: choicesList,
                allowHTML: true
            });
            var searchInput = $(".gene-choices-container .choices .choices__inner .choices__input");
            searchInput.focus();
            searchInput.attr("placeholder", "Chọn Role...");
            $('#gene-input-select').on('addItem', function (e) {
                choicesList.find((c) => c.value === e.detail.value).selected = true;
            });
            $('#gene-input-select').on('removeItem', function (e) {
                choicesList.find((c) => c.value === e.detail.value).selected = false;
            });
        },
        preConfirm: () => {
            saveSettingRoleUser();
            return false;
        }
    });

    function saveSettingRoleUser() {
        const dataToSend = {
            user_name: user_name,
            selected_roles: choicesList
        };
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/Setting/SaveRoleForUser",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(dataToSend),
            success: function (result) {
                location.reload();
            },
            error: function (res) {

            }
        });
    }
}*/
