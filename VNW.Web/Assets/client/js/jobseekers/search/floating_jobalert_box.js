var floatingJobAlertBox = {
	settings: {
		topThreshold: 600,
		botThreshold: 5000
	},
	initEl: function () {
		"use strict";
		this.$jobAlertFloatBanner = $('#job-alert-float-banner');
		this.$form = this.$jobAlertFloatBanner.find('form');
	},
	initFloatingBanner: function () {
		"use strict";

		// Setup float job alert layout
		var self = this;
		var settings = this.settings;

		$(window).scroll(function () {
			var winTop = $(document).scrollTop();
			if (winTop > settings.topThreshold && winTop < settings.botThreshold) {
				self.$jobAlertFloatBanner.fadeIn('slow');
			} else {
				self.$jobAlertFloatBanner.fadeOut('slow')
			}
		});

	},

	checkFormValid: function () {
		"use strict";
		return this.$form.valid();
	},

	initValidation: function () {
		"use strict";

		this.$validator = this.$form.validate({
			messages: {
				floatBannerJobAlertEmail: {
					required: floatingJobAlertBox__translation.requiredErrorMessage,
					email: floatingJobAlertBox__translation.emailErrorMessage
				}
			}
		});
	},

	init: function () {
		"use strict";
		this.initEl();
		this.initFloatingBanner();
		this.initValidation();
		this.initPopup();
	},

	initPopup: function () {
		var button = jQuery('#jobalert_open');

		button.click(function() {
			if(!floatingJobAlertBox.checkFormValid()) {
				return;
			}
            var thisValue = $('#floatBannerJobAlertEmail').val();
            var jobAlertError = $('#job-alert-float-banner .error');
            $.ajax({
                url: jobAlertSettings.jobAlertCheckEmailURL,
                method: 'POST',
                data: {"email": thisValue},
                success: function(data) {
                    if(data == false)
					{
                        jobAlertError.hide();
                        $('.job-search__btn-create-job-alert').trigger('click');

                        var emailElement = jQuery('#floatBannerJobAlertEmail');
                        var jobAlertEmailElement = jQuery('#jobalert_email');
                        jobAlertEmailElement.val(emailElement.val());

                        if(emailElement.attr('readonly') != undefined) {
                            jobAlertEmailElement.prop('readonly',true);
                        }

                        jQuery('#fromWindow').val(1);
					}
					else
					{
                        jobAlertError.text(translationJA.disposableJA);
                        jobAlertError.show();
					}
                },
                error: function (data) {

                }
            });
		});
	}
};