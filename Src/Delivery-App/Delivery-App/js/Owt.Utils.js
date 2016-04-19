app.factory('Utils', ['$log', '$window',
function ($log, $window) {

    var GlobalMethods = function () {
        var self = this;

        self.SetStatusBarColor = function (color) {
            if (window.StatusBar) {
                //StatusBar.styleDefault();
                StatusBar.backgroundColorByHexString(color);
            }
        }
    }
    return new GlobalMethods();
}]); 