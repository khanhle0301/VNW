var TopCompaniesAll = {
    exec_request : function () {
        var requestURL = 'vclick/index.php?group=common&zone=3';
        var wrapper = jQuery('#ads_FEATURED_EMPLOYERS_FULL');
        TopCompaniesAll.display_animation(wrapper, true);

        jQuery.ajax({
            url: requestURL,
            success: function(result)
            {
                TopCompaniesAll.display_animation(wrapper, false);

                if(result.code != 200) {
                    return;
                }

                TopCompaniesAll.render_ads(wrapper, result.data.result);
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

        var adswrapper = '<div style="position:relative;">';
        adswrapper += '<table id="adc_FEATURED_EMPLOYERS_FULL" class="logo_feature" style="margin-left:auto; margin-right:auto">';
        adswrapper += '<tbody>';

        var row = Math.ceil(length / 7);
        var currentindex = 0;

        for(var index = 0; index < row; index++) {
            var tr = '<tr>';
            var tds = '';
            var limit = currentindex + 7;

            for(var val = currentindex; val < limit; val++) {
                var ads = data[val];

                if(ads == undefined) {
                    break;
                }

                var td = '<td>';

                td += '<a target="_blank" href="' + ads['destinationURL'] + '">';
                td += '<img src="' + ads['imageURL'] + '" width="88" height="43" alt="" title="" border="0">';

                td += '</td>';
                tds += td;

                currentindex ++;
            }

            tr += tds;
            tr += '</tr>';
            adswrapper += tr;
        }

        adswrapper += '</tbody></table>';
        wrapper.html(adswrapper);
    }
};

jQuery(function() {
    TopCompaniesAll.exec_request();
});