app.controller('DeliveryDetailCtrl', function ($scope, $stateParams, Utils, baseUrl, $cordovaDialogs, profileService, $http, $state, deliveryService) {
    Utils.SetStatusBarColor('#00aba9');
    $scope.Delivery = {};

    deliveryService.getDeliveries(1).success(function (data) {
        $scope.Delivery = data.results[0];
    });


});