// PIGNOSE Calendar
import 'pg-calendar';

$(()=>{
  $('.calendar').pignoseCalendar({
    format: 'DD/MM/YYYY',
    week: 1
  });
});


// Sweet Alert
window.swal = require('sweetalert2');
//...