window.$ = window.jQuery = require('jquery');
window.Popper = require('popper.js');
require('bootstrap');
require('daemonite-material');

// Config
import 'script-loader!js-cookie';
import '../ext/config.js';

// Styles
import './app.scss';

// Form Control
import './form.js';

// Sidebar menu
import './sidebar.js';

// Dashboard
import './dashboard.js';



// Back to top
var $gotop = $('#gotop');
$gotop.find('a').click(function () {
  $('html, body').animate({scrollTop: 0}, 600);
  return false;
});

$(window).scroll(function () {
	if ($(this).scrollTop() >= 200) $gotop.fadeIn(200);
  else $gotop.fadeOut(200);
  	
	var gtop = $(window).height()-50,
			gright = 10,
			chat = $('#uhchat');
	if(chat.length>0) {
		gtop = chat.position().top-50;
		gright = 20;
  }
  
	$gotop.css({
		'top' : gtop+'px',
    'right' : gright+'px',
    'bottom' : 'auto'
	});
});
