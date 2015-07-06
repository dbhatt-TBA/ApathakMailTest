F2.Apps["com_your_app_id"] = (function() {
    var App_Class = function(appConfig, appContent, root) {
    	// constructor
    	this.appConfig = appConfig;
    	this.appContent = appContent;
    	this.$root = $(root); //if you're using jQuery.
    }

    App_Class.prototype.init = function() {
        // perform init actions
        F2.log("Init called.")
    }

    return App_Class;
})();