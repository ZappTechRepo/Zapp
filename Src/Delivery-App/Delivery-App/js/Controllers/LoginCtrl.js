app.controller('LoginCtrl', function ($scope, $stateParams, Utils, baseUrl, $cordovaDialogs, profileService, $http, $state) {
    Utils.SetStatusBarColor('#00aba9');

    $scope.Login = function () {
        $state.go('app.deliveries');
    }
});