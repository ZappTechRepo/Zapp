app.controller('ProfileCtrl', function ($scope, $stateParams, Utils, baseUrl, $cordovaDialogs, profileService, $http, $state) {
    Utils.SetStatusBarColor('#00aba9');
     
})
     
.controller('MenuCtrl', function ($scope, $stateParams, Utils, baseUrl, $cordovaDialogs, profileService, $http, $state) {
    Utils.SetStatusBarColor('#00aba9');

    $scope.Profile = profileService.getProfile();

    $scope.Logout = function () {
        //profileService.setProfile(null);
        $state.go("app.login");  
    }
});
