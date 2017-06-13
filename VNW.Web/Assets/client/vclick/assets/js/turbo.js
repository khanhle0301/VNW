var Turbo = {
    init : function () {
        var wrapper = jQuery('#ads_LEFT');
        var new_div = '<div style="position:relative;">';
        var table = '<table id="adc_LEFT"><tbody>';

        table += '<tr class="ads_LEFT_FIRST"></tr>';
        table += '<tr class="ads_LEFT_SECOND"></tr>';
        table += '</tbody></table>';

        new_div += table + '</div>';
        wrapper.html(new_div);
    },

    display : function() {
        var first_turbo = jQuery('.ads_LEFT_FIRST');
        var second_turbo = jQuery('.ads_LEFT_SECOND');

        Turbo.exec_request(12, first_turbo);
        Turbo.exec_request(13, second_turbo);
    },

    rotateTurboBanner : function (parentEl, rotationTime) {
        parentEl.each(function () {
            var $self = $(this);
            var firstItem = $(this).find('a:first');

            setInterval(function () {
                var currentShownItem = $self.find('a:visible');
                var nextItem = currentShownItem.next('a');

                currentShownItem.hide().promise().done(function () {
                    if (nextItem.length > 0) {
                        nextItem.css('display','block');
                    } else {
                        firstItem.css('display','block');
                    }
                });
            }, rotationTime);
        })
    },

    exec_request : function (zone, wrapper) {
        var domain = window.location.origin;
        var requestURL = domain + '/vclick/index.php?group=common&zone=' + zone;
        Turbo.display_animation(wrapper, true);

        jQuery.ajax({
            url: requestURL,
            success: function(result)
            {
                Turbo.display_animation(wrapper, false);

                if(result.code != 200) {
                    return;
                }

                Turbo.render_ads(wrapper, result.data.result);
                Turbo.rotateTurboBanner(wrapper, result.data.rotate_time);
            }
        });
    },

    display_animation : function (wrapper, flag) {
        if(!flag) {
            wrapper.empty();
            return;
        }

        var subwrapper = '<div class="text-center m-t-md m-b-md"><i class="fa fa-spinner fa-pulse fa-3x fa-fw margin-bottom"></i></div>';
        wrapper.html(subwrapper);
    },

    render_ads : function(wrapper, data) {
        var length = data.length;

        if(length <= 0) {
            return;
        }

        var adswrapper = '<td style="padding-top:5px;">';

        for(var index = 0; index < length; index++) {
            var hidden = (index == 0) ? '' : 'hidden';

            var ads = data[index];
            var logowrapper = '';

            logowrapper += '<a ' + hidden + ' class="cusLogo" target="_blank" href="' + ads['destinationURL'] + '">';
            logowrapper += '<img src="' +  ads['imageURL'] + '" width="220" height="165">';

            logowrapper += '</a>';
            adswrapper += logowrapper;
        }

        adswrapper += '</td>';
        wrapper.html(adswrapper);
    }
};

jQuery(function() {
    Turbo.init();
    Turbo.display();
});