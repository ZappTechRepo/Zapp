app.controller('DeliveryDetailCtrl', function ($scope, $stateParams, Utils, baseUrl, $cordovaDialogs, profileService, $http, $state, deliveryService, $localStorage, ngDialog) {
    Utils.SetStatusBarColor('#00aba9');
    $scope.Delivery = {};
    $scope.DocID = $stateParams.Id;
    
    $scope.Person = {
        FullName: "",
        Signature: ""
    };

    var doc = $scope.Delivery;

    //deliveryService.getDeliveries(1).success(function (data) {
    //    $scope.Delivery = data.results[0];
    //});
    if ($scope.DocID != null)
    {
        $scope.Delivery = _.where($localStorage.Documents, { ID: parseInt($scope.DocID) })[0];
        doc = $scope.Delivery;
    }

    //DIRECTIONS TO ADDRESS
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

    //WHEN DIALOG OPENS
    $scope.$on('ngDialog.opened', function (e, $dialog) {
        if ($dialog.hasClass('SignaturePad')) {
            var canvas = document.getElementById('cvsSignature');
            $scope.signaturePad = new SignaturePad(canvas);

            canvas.onfocus = function () {
                console.log('canvas.onfocus');
                cordova.plugins.Keyboard.close();
            };
        }
    });

    $scope.SignForDelivery = function () {

    }


    //IF DELIVERY NOT COMPLETED
    if (true) {
        //#region SignaturePad
        $scope.signaturePad = {};

        $scope.showSignPanel = function () {
            dialog = ngDialog.open({
                template: 'sigTemplate',
                //pass in scope context
                scope: $scope,
                className: 'SignaturePad ngdialog-theme-default'
            });
        };

        $scope.hideSignPanel = function () {
            $scope.signaturePad.clear();
            dialog.close();
        };

        $scope.SubmitSignature = function () {
            if (!($scope.Person.FullName || $scope.$$childTail.Person.FullName)) {
                return false;
            }
            if (!$scope.signaturePad.isEmpty()) {
                var fullimageData = $scope.signaturePad.toDataURL("image/png"), imgData = fullimageData.replace('data:image/png;base64,', '');

                deliveryService.SubmitSuccessfulDelivery($scope.DocID, imgData, $scope.Person.FullName).success(function (data) {
                    $localStorage.currentDocument.orderDelivered = true;
                    $state.go('app.deliveries');
                }).error(function (data) {
                    console.log(data);
                });

                dialog.close();
            }
        };
    }
});