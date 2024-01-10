
function assignDatepicker(element) {
    element.daterangepicker({
        singleDatePicker: true,
        autoApply: true,
        applyButtonClasses: false,
        autoUpdateInput: true,
        startDate: moment().startOf('hour'),
        locale: {
            format: 'DD/MM/YYYY'
        }
    }, function (start) {
        element.val(start.format('DD/MM/YYYY'));
    });
}

export { assignDatepicker };