var SaveJobManager = (function () {
    function SaveJobManager(settingObj) {
        this.maxNumberOfSavedJobToGet = 10;
        this.savedJobInfoURL = settingObj.savedJobInfoURL;
        this.saveThisJobURL = settingObj.saveThisJobURL;
        this.unsaveThisJobURL = settingObj.unsaveThisJobURL;
        this.initMovingHeart();
        this.initToggleSaveJob();
    }
    SaveJobManager.prototype.pulseThisItem = function (item) {
        var animateClass = 'animated rubberBand';
        $(item).addClass(animateClass);
        setTimeout(function () {
            $(item).removeClass(animateClass);
        }, 400);
    };
    SaveJobManager.prototype.moveThisItem = function (elementToMove, startElement, endElement, moveDuration) {
        var startElementOffset = $(startElement).offset();
        var endElementOffset = $(endElement).offset();
        $(elementToMove).css({
            'top': startElementOffset.top,
            'left': startElementOffset.left
        });
        $(elementToMove).show();
        setTimeout(function () {
            $(elementToMove).css({ 'top': endElementOffset.top, 'left': endElementOffset.left, 'opacity': 0 });
            setTimeout(function () {
                $(elementToMove).removeAttr('style');
            }, moveDuration);
        }, 10);
    };
    SaveJobManager.prototype.initMovingHeart = function () {
        var self = this;
        var heartTemplate = "<div class=\"heart-moving-effect inline\">\n                                <i class=\"fa fa-heart text-primary\"></i>\n                            </div>";
        $('body').append(heartTemplate);
        $('#main-job-list').on('click', '.job-search__save-job .fa', function () {
            if ($('body').width() >= 768) {
                if (!$(this).next('input').is(':checked')) {
                    var thisHeart_1 = this;
                    $('#job-search').one('saveJobCounterRendered', function () {
                        var endElement = $('#job-search__saved-jobs').find('.fa');
                        self.moveThisItem('.heart-moving-effect', thisHeart_1, endElement, 300);
                        setTimeout(function () {
                            self.pulseThisItem('.saved-heart-wrapper');
                        }, 200);
                    });
                }
            }
        });
    };
    SaveJobManager.prototype.getCurrentJobIDArrOnPage = function () {
        var $jobItems = $('#job-list').find('.job-item');
        if ($jobItems.length > 0) {
            return (function () {
                var $currentJobs = $('#job-list').find('.job-item').map(function () {
                    var thisJobID = $(this).data('job-id');
                    if (typeof thisJobID !== "undefined") {
                        return thisJobID.toString();
                    }
                });
                return $currentJobs.get();
            })();
        }
        return [];
    };
    SaveJobManager.prototype.getSavedJobFromServer = function () {
        console.log('Will get saved jos from server');
        var self = this;
        var currentJobsOnPageArr = self.getCurrentJobIDArrOnPage();
        // Get these numbers from server
        // TODO: change GET to POST
        $.ajax({
            url: self.savedJobInfoURL,
            method: 'POST',
            header: { 'Content-Type': 'application/json' },
            data: {
                currentJobsOnPage: currentJobsOnPageArr
            },
            success: function (data) {
                self.currentTotalNumberOfSavedJob = parseInt(data.currentTotalNumberOfSavedJob);
                self.savedJobsOnCurrentPageArray = data.savedJobsOnCurrentPageArray;
                self.checkSavedJob();
                self.updateCounterNumberState(self.currentTotalNumberOfSavedJob);
                console.log('Got saved jobs from server');
            },
            dataType: 'json'
        });
    };
    SaveJobManager.prototype.reactHandlersBound = function () {
        // Anything that needs React should be call here.
        // This is connected with componentWillMount, execute once
        this.getSavedJobFromServer();
    };
    SaveJobManager.prototype.checkSavedJob = function () {
        var self = this;
        // Get the list of all jobs
        var currentJobsOnPageArr = self.getCurrentJobIDArrOnPage();
        // If this jobID exists in the saved job ID array, mark it saved, otherwise, mark as unsaved
        if (typeof self.savedJobsOnCurrentPageArray !== "undefined" && self.savedJobsOnCurrentPageArray.length > 0) {
            $.each(currentJobsOnPageArr, function (index, jobID) {
                var $targetInput = $('#checkbox' + jobID);
                if (self.savedJobsOnCurrentPageArray.indexOf(jobID) > -1) {
                    // Mark as saved
                    $targetInput.attr('checked', 'checked').prop('checked', true);
                    $targetInput.closest('.job-search__save-job').find('.fa').removeClass('fa-heart-o').addClass('fa-heart');
                }
                else {
                    $targetInput.removeAttr('checked').prop('checked', false);
                    $targetInput.closest('.job-search__save-job').find('.fa').removeClass('fa-heart').addClass('fa-heart-o');
                }
            });
        }
        else {
            // Mark all as unsaved 
            var $targetInput = $('#job-list').find('input.save-job');
            $targetInput.each(function () {
                $(this).removeAttr('checked').prop('checked', false);
                $(this).closest('.job-search__save-job').find('.fa').removeClass('fa-heart').addClass('fa-heart-o');
            });
        }
    };
    SaveJobManager.prototype.initToggleSaveJob = function () {
        var self = this;
        if (isLoggedIn) {
            $('#job-list').on('change', '.job-search__save-job input', function () {
                // Toggle the heart icon & increase the number of total saved job
                var $targetInput = $(this);
                var $targetIcon = $(this).siblings('.fa');
                var jobID = $(this).closest('.job-item').data('job-id');
                // Add save job if logged in
                if ($targetInput.is(':checked')) {
                    $targetIcon.removeClass('fa-heart-o').addClass('fa-heart');
                    self.currentTotalNumberOfSavedJob = self.currentTotalNumberOfSavedJob + 1;
                    // Add this item in the current save job array
                    self.savedJobsOnCurrentPageArray.push(jobID.toString());
                    // Send added job ID to server
                    $.ajax({
                        url: self.saveThisJobURL,
                        method: 'POST',
                        header: { 'Content-Type': 'application/json' },
                        data: {
                            jobID: jobID
                        },
                        success: function () {
                            console.log('Job is saved successfully');
                        },
                        dataType: 'json'
                    });
                }
                else {
                    $targetIcon.removeClass('fa-heart').addClass('fa-heart-o');
                    self.currentTotalNumberOfSavedJob = self.currentTotalNumberOfSavedJob - 1;
                    // Remove this item in the current save job array
                    var removeIndex = self.savedJobsOnCurrentPageArray.indexOf(jobID);
                    self.savedJobsOnCurrentPageArray.splice(removeIndex, 1);
                    // Send removed job ID to server
                    $.ajax({
                        url: self.unsaveThisJobURL,
                        method: 'POST',
                        header: { 'Content-Type': 'application/json' },
                        data: {
                            jobID: jobID
                        },
                        success: function () {
                            console.log('Job is removed successfully');
                        },
                        dataType: 'json'
                    });
                }
                // Sync with React State To Update the Counter
                self.updateCounterNumberState(self.currentTotalNumberOfSavedJob);
            });
        }
        else {
            var $jobList = $('body');
            $jobList.on('mouseenter', '.job-search__save-job label', function () {
                $(this).popover({
                    'placement': 'left',
                    'trigger': 'manual',
                    'html': true,
                    'delay': 100,
                    'container': 'body',
                    'content': saveJobTranslationObj.logInToUse
                }).popover('show');
            }).on('mouseleave', '.job-search__save-job label, .popover', function () {
                var self = this;
                setTimeout(function () {
                    if ($('.popover:hover').length < 1) {
                        $(self).popover('hide');
                    }
                }, 100);
            });
        }
    };
    return SaveJobManager;
}());
var saveJobManager = new SaveJobManager(saveJobURLs);
//# sourceMappingURL=saveJobManager.js.map