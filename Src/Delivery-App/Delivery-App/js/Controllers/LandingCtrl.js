app.controller('LandingCtrl', function ($scope, $stateParams, Utils, baseUrl, $cordovaDialogs, profileService, $http, $state) {
    Utils.SetStatusBarColor('#00aba9');

    

    $scope.doLoginFromToken = function (token) {

        $http.post(baseUrl + "/Auth/doLoginFromToken", { token: token }).success(function (data) {
             
            profileService.SetProfile(data);
            $state.go('app.profile'); 

        }).error(function (data) {
            console.dir(data);
            $cordovaDialogs.alert('Unable to log you in. ' + data, 'Login', 'OK');
        });
    };

    //initialization
    var profile = profileService.getProfile();
    if (profile != null) {
        $scope.doLoginFromToken(profileService.getToken());
    }
});