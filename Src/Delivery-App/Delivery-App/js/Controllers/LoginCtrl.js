app.controller('LoginCtrl', function ($scope, $stateParams, Utils, baseUrl, $cordovaDialogs, profileService, $http, $state) {
    Utils.SetStatusBarColor('#00aba9');

    $scope.Login = function () {

        $scope.data = {
            username: "jason@live.com",
            password: "test123"
        };

        profileService.DoLogin($scope.data).success(function (data) {
            console.log(data);
        });

        $state.go('app.deliveries');
    } 
});