app.controller('DeliveriesCtrl', function ($scope, $stateParams, Utils, baseUrl, $cordovaDialogs, profileService, $http, $state, deliveryService) {
    Utils.SetStatusBarColor('#00aba9');
    $scope.Deliveries = [];


    deliveryService.getDeliveries(15).success(function (data) {
        $scope.Deliveries = data.results;
    });

    $scope.doRefresh = function () {
        deliveryService.getDeliveries(15).success(function (data) {
            $scope.Deliveries = data.results;
        }).finally(function () {
            // Stop the ion-refresher from spinning
            $scope.$broadcast('scroll.refreshComplete');
        });
    }

    $scope.GotoDelivery = function () {
        $state.go('app.deliveryDetail');
    }
});