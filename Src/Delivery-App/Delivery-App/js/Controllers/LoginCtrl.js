app.controller('LoginCtrl', function ($scope, $stateParams, Utils, baseUrl, $cordovaDialogs, $localStorage, profileService, $http, $state) {
    Utils.SetStatusBarColor('#00aba9');
    $scope.loginData = {
        username: "",
        password: ""
    };

    $scope.Login = function () { 

        if ($scope.loginData.username != "" && $scope.loginData.password != "") {

            profileService.DoLogin($scope.loginData).success(function (data) {
                console.log(data);
                $localStorage.profile = data;
                $state.go('app.deliveries', { userId: $localStorage.profile.UserID });
            }).error(function (data) {
                $cordovaDialogs.alert(data.Message, 'Login', 'OK');
            });

        } else {
            $cordovaDialogs.alert('Enter your full login details. ', 'Login', 'OK');
        }
    }


    //initialization
    var profile = profileService.getProfile();
    if (profile != null) {
        $state.go('app.deliveries', { userId: profile.UserID, reload: true });
        //profileService.LoginFromToken(profileService.getToken());
    }
});