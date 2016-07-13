app.controller('LoginCtrl', function ($scope, $stateParams, Utils, baseUrl, $cordovaDialogs, $localStorage, profileService, $http, $state) {
    Utils.SetStatusBarColor('#00aba9');

    $scope.Login = function () { 

        $scope.data = {
            username: "jason",
            password: "Jasket123"
        };


        profileService.DoLogin($scope.data).success(function (data) {
            console.log(data);
            $localStorage.Profile = data;
        });

        $state.go('app.deliveries');
    }  
});