var BrowseJobManager = (function () {
    function BrowseJobManager(queries, lastResults, callback) {
        this.lastCursors = {};
        this.ignoreResetCursor = false;
        this.loadMorePage = false;
    }
    BrowseJobManager.prototype.addCursor = function (cursor) {
        var self = this;
        var cursorNotExist = (function () {
            var existed = false;
            $.each(self.lastCursors, function (index, value) {
                if (cursor === value) {
                    existed = true;
                    return false;
                }
            });
            return !existed;
        })();
        if (cursorNotExist) {
            // TODO: make number "19" a setting parameter;
            // Page 20 is actually page 21 on the UI
            var currentKey = Object.keys(this.lastCursors).length + 19;
            var nextKey = currentKey + 1;
            self.lastCursors[nextKey] = cursor;
            return self.lastCursors;
        }
    };
    BrowseJobManager.prototype.makeBrowseResponseLikeSearchResponse = function (content, state) {
        if (typeof content.results === 'undefined' && !!browseJobManager.lastResults) {
            content.results = {};
            content.results[0] = content;
            content.results[0].facets = browseJobManager.lastResults.facets;
            content.results[0].facets_stats = browseJobManager.lastResults.facets_stats;
            content.results[0].index = browseJobManager.lastResults.index;
            content.results[0].hitsPerPage = browseJobManager.lastResults.hitsPerPage;
            content.results[0].nbHits = browseJobManager.lastResults.nbHits;
            content.results[0].nbPages = browseJobManager.lastResults.nbPages;
            content.results[0].page = state.page;
            content.results[1] = content.results[0];
        }
    };
    BrowseJobManager.prototype.getLastLoadedPage = function () {
        // TODO: make number "19" a setting parameter; currently 19 is the number of pages that has no cursor
        return Object.keys(this.lastCursors).length + 19;
    };
    BrowseJobManager.prototype.getCursorOfThisPage = function (page) {
        return this.lastCursors[page];
    };
    BrowseJobManager.prototype.getLastCursor = function () {
        var cursorSize = _.size(this.lastCursors);
        var lastCursorKey = cursorSize + 19;
        return this.lastCursors[lastCursorKey];
    };
    BrowseJobManager.prototype.resetCursor = function () {
        this.lastCursors = {};
    };
    BrowseJobManager.prototype.resetCursorIfSet = function () {
        if (this.ignoreResetCursor === false) {
            this.resetCursor();
        }
        this.ignoreResetCursor = false;
    };
    BrowseJobManager.prototype.setIgnoreResetCursor = function () {
        this.ignoreResetCursor = true;
    };
    BrowseJobManager.prototype.setLastSearchMethod = function (method) {
        // Search Method: "browse" or "search"
        this.lastSearchMethod = method;
    };
    return BrowseJobManager;
}());
var browseJobManager = new BrowseJobManager();
//# sourceMappingURL=browseJobManager.js.map