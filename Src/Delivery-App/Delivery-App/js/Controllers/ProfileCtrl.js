app.controller('ProfileCtrl', function ($scope, $stateParams, Utils, baseUrl, $cordovaDialogs, profileService, $http, $state, deliveryService) {
    Utils.SetStatusBarColor('#00aba9');
    $scope.profile = profileService.getProfile();
    $scope.DocumentsStats = deliveryService.getCacheDeliveries();

    $scope.$parent.showHeader();
    $scope.$parent.clearFabs();
    $scope.isExpanded = false;
    $scope.$parent.setExpanded(false);
    $scope.$parent.setHeaderFab(false);

})
     
.controller('MenuCtrl', function ($scope, $stateParams, Utils, baseUrl, $cordovaDialogs, profileService, $http, $state) {
    Utils.SetStatusBarColor('#00aba9');

    $scope.Profile = profileService.getProfile();

    $scope.Logout = function () {
        profileService.SetProfile(null);
        $state.go("app.login");  
    }
});
