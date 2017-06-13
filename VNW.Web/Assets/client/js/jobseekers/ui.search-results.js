var JobAlertResponses = (function () {
    function JobAlertResponses() {
        this.$badModal = $('.survey-response_bad');
        this.$neutralModal = $('.survey-response_neutral');
        this.$goodModal = $('.survey-response_good');
    }
    JobAlertResponses.prototype.showBadMessage = function () {
        this.$badModal.modal('show');
        this.$neutralModal.modal('hide');
        this.$goodModal.modal('hide');
    };
    JobAlertResponses.prototype.hideBadMessage = function () {
        this.$badModal.modal('hide');
    };
    JobAlertResponses.prototype.showNeutralMessage = function () {
        this.$badModal.modal('hide');
        this.$neutralModal.modal('show');
        this.$goodModal.modal('hide');
    };
    JobAlertResponses.prototype.hideNeutralMessage = function () {
        this.$neutralModal.modal('hide');
    };
    JobAlertResponses.prototype.showGoodMessage = function () {
        this.$badModal.modal('hide');
        this.$neutralModal.modal('hide');
        this.$goodModal.modal('show');
    };
    JobAlertResponses.prototype.hideGoodMessage = function () {
        this.$goodModal.modal('hide');
    };
    return JobAlertResponses;
}());
var jobAlertResponses = new JobAlertResponses();
//# sourceMappingURL=ui.search-results.js.map