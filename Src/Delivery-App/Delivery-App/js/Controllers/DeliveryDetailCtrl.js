app.controller('DeliveryDetailCtrl', function ($scope, $stateParams, Utils, baseUrl, $cordovaDialogs, profileService, $http, $state, deliveryService, $localStorage) {
    Utils.SetStatusBarColor('#00aba9');
    $scope.Delivery = {};
    $scope.DocID = $stateParams.Id;
    var doc = $scope.Delivery;

    //deliveryService.getDeliveries(1).success(function (data) {
    //    $scope.Delivery = data.results[0];
    //});
    if ($scope.DocID != null)
    {
        $scope.Delivery = _.where($localStorage.Documents, { ID: parseInt($scope.DocID) })[0];
        doc = $scope.Delivery;
    }

    $scope.GetDirections = function () {

        var url = '';
        if (device.platform === 'iOS' || device.platform === 'iPhone' || navigator.userAgent.match(/(iPhone|iPod|iPad)/)) {
            url = window.location.href = "maps://maps.apple.com/?daddr=" + doc.Customer.Addresses[0].Address1 + " " + doc.Customer.Addresses[0].Address2 + " " + (doc.Customer.Addresses[0].Address3 != null ? doc.Customer.Addresses[0].Address3 : "") + " " + (doc.Customer.Addresses[0].Country != null ? doc.Customer.Addresses[0].Country : "");
        }
        else {
            url = "geo:?q=" + doc.Customer.Addresses[0].Address1 + " " + doc.Customer.Addresses[0].Address2 + " " + (doc.Customer.Addresses[0].Address3 != null ? doc.Customer.Addresses[0].Address3 : "") + " " + (doc.Customer.Addresses[0].Country != null ? doc.Customer.Addresses[0].Country : "");
        }
        window.open(url, "_system", 'location=yes');
    }

    $scope.SignForDelivery = function () {

    }
});