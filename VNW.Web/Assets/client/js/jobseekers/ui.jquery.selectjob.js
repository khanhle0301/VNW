(function ($) {
    'use strict';
    $.fn.selectJob = function (options, jobObject) {


        // Setup
        var $self = $(this),
            jobID = jobObject,
            headerHeight = 35,
            currentHeight,
            MAC = /Mac/.test(navigator.userAgent),
            cases,
            opts,
            maxJobs;

        // Merge options with defaults
        opts = $.extend({}, $.fn.selectJob.defaults, options);


        // Overwrite default options
        $.fn.selectJob.defaults = opts;

        // Set Selector
        $.fn.selectJob.defaults.element = this.selector;

        // Cases
        cases = {
            "remove": function () {
                $.fn.selectJob.removeJob(jobID);
            },
            "add": function () {
                $.fn.selectJob.addJob(jobObject);
            },
            "init": function () {
                // Bind click event to "Delete All Job"
                $self.find('.delete-all-job').click(function () {
                    $self.find('.delete-selected-job').click();
                }).hover(function () {
                    $self.find('.delete-selected-job').addClass('hover');
                }, function () {
                    $self.find('.delete-selected-job').removeClass('hover');
                });

                // Minimize Panel
                $self.find('.btn-minimize-panel').click(function () {
                    if (!$(this).hasClass('is-minimized')) {
                        $(this).removeClass('fa-chevron-down').addClass('fa-chevron-up is-minimized');
                        currentHeight = $self.parent('.selected-table-wrapper').outerHeight();
                        $self.parent('.selected-table-wrapper').animate({
                            'height': headerHeight
                        }, 'fast');
                    } else {
                        $(this).removeClass('fa-chevron-up is-minimized').addClass('fa-chevron-down');
                        $self.parent('.selected-table-wrapper').animate({
                            'height': currentHeight
                        }, 'fast').promise().done(function () {
                            $(this).removeAttr('style');
                        });
                    }
                });
            }
        };

        // If Mac OS, move the box to the left to avoid the scroll bar
        if (MAC) {
            $self.parents('.float-table-wrapper').addClass('mac-os');
        }

        // Invoke
        (cases[options] || cases.init)();


    };

    /* Default Options */
    $.fn.selectJob.defaults = {
        element: null,
        closeBtn: '.delete-selected-job',
        addBtn: '.select-job',
        animationDuration: 500,
        iconAdd: '.fa',
        maxJobs: 5
    };

    /* Reorder */
    $.fn.selectJob.reOrder = function () {
        var $el = $($.fn.selectJob.defaults.element);
        $el.find('tbody tr').each(function (index) {
            $(this).find('.order').text(index); // Minus the template row
        });

        // Disable the "compare button" if there is only 1 job (minus the template row)
        if ($el.find('tbody tr').length < 3) {
            $el.find('.btn-compare-job').addClass('disabled').attr('title', $el.find('.btn-compare-job').data('disabled-message'));
        } else {
            $el.find('.btn-compare-job').removeClass('disabled').removeAttr('title');
        }
    };

    /* Add Job */
    $.fn.selectJob.addJob = function (jobObject) {
        var $thisAddButton = $('.select-job[data-jobid=' + jobObject.job_id + ']'),
            $el = $($.fn.selectJob.defaults.element),
            elHeight,
            txtJobTitle = jobObject.job_title,
            jobLink = jobObject.job_url,
            $thisRow,
            $template = $el.find('.row-template').clone().removeClass('hidden row-template');

        // Check the number of jobs
        if ($el.find('tbody').find('tr').length > $.fn.selectJob.defaults.maxJobs ){
            $thisAddButton.prop('checked',false).tooltip({
                'placement':'top',
                'title': function(){
                    return $(this).data('limit-error-message');
                },
                'trigger':'manual',
                'html':true
            }).tooltip('show');

            setTimeout(function(){
                $thisAddButton.tooltip('hide');
            },3000);

        } else {
            $thisAddButton.addClass('added-job');
        $thisAddButton.prop('checked',true);

            // Add to "selected-job-table"
            $thisRow = $template.find('[data-name="jobTitle"]').text(txtJobTitle).attr('href', jobLink)
                .end().find('i.fa').attr('data-jobid', jobObject.job_id)
                .end();
            $el.find('tbody').append($thisRow).find('tr:last-child').hide().addClass('animated fadeInLeft').show();

            // Reorder after adding
            $.fn.selectJob.reOrder();

            // Open the "selected-job-table" in case the first job is added
            if ($el.find('tbody tr').length > 1) {
                $el.parent('.selected-table-wrapper').removeClass('fadeOutDown').addClass('animated fadeInUp').show();
                elHeight = $el.outerHeight(true);
                $el.parent('.selected-table-wrapper').animate({
                    height: elHeight,
                    opacity: 1
                }, 200).promise().done(function () {
                    $(this).removeAttr('style');
                });
            }
        }

    };

    /* Remove Job */
    $.fn.selectJob.removeJob = function (jobID) {

        var $closeBtn = $('.delete-selected-job[data-jobid=' + jobID + ']'),
            $el = $($.fn.selectJob.defaults.element),
            $selectJob = $('.select-job[data-jobid=' + jobID + ']'),
            thisIconAdd = $.fn.selectJob.defaults.iconAdd;
        $selectJob.removeProp('checked');
        $selectJob.removeClass('added-job');

        //if (thisIconAdd === ".fa") {
        //    $selectJob.find(thisIconAdd).removeClass('fa-check-circle').addClass('fa-plus-circle');
        //} else {
        //    $(thisIconAdd).removeClass('fa-check-circle').addClass('fa-plus-circle');
        //}

        $closeBtn.parents('tr').addClass('animated fadeOutRight');
        setTimeout(function () {
            $closeBtn.parents('tr').remove();
            // Reorder after deleting
            $.fn.selectJob.reOrder();

            // Remove the whole table if there is no job selected
            if ($el.find('tbody tr').length < 2) {
                $el.parent('.selected-table-wrapper').height($el.outerHeight(true)).delay(300).animate({
                    height: 0,
                    margin: 0,
                    opacity: 0
                }, 200).promise().done(function () {
                    $(this).hide();
                });
            }
        }, $.fn.selectJob.defaults.animationDuration);
    };
}(jQuery));
