require('metismenu')
require('onoffcanvas')

var $sidebar = $('#sidebar'),
    ifull = 'format_align_center',
    imini = 'format_list_bulleted',
    iclose = 'close';

/*
 * Sidebar Menu 1.0
 * Copyright 2018 QsvProgram
 * Released under the MIT and GPL licenses.
 */
class Sidebar {
  constructor() {}

  static Toggle(){
    var $icon = $(this).find('.material-icons');

    if($(window).width()<768) {
      $('[data-target="#sidebar"]').trigger('click');
    }
    else {
      if($sidebar.hasClass('is-hoverable')) {
        $icon.text(ifull);
        $sidebar
          .addClass('is-open')
          .removeClass('is-hoverable');
      }
      else {
        $icon.text(imini);
        $sidebar
          .addClass('is-hoverable')
          .removeClass('is-open');
      }
    }

    return false;
  }

  static Resize(){
    var $icon = $sidebar.find('.toggler .material-icons');

    if($(window).width()<768) {
      $icon.text(iclose);
      $sidebar.removeClass('is-open is-hoverable');
    }
    else if($(window).width()<1024) {
      $icon.text(imini);
      $sidebar
        .addClass('is-hoverable')
        .removeClass('is-open');
    }
    else {
      $icon.text(ifull);
      $sidebar
        .addClass('is-open')
        .removeClass('is-hoverable');
    }

    var height = $(window).height()-90;
    $sidebar.find('.side-content').height(height);
  }
}

$sidebar.find('.metismenu').metisMenu({toggle:false});
$sidebar.find('.toggler').click(Sidebar.Toggle);
$(window).on('load resize', Sidebar.Resize);
Sidebar.Resize();



//Perfect Scrollbar
window.perfect = require('perfect-scrollbar');
//...