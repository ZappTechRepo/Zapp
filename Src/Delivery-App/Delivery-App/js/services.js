app

.factory('profileService', ['$localStorage', '$http', 'baseUrl', '$log', '$cordovaDialogs', '$state',
	function ($localStorage, $http, baseUrl, $log, $cordovaDialogs, $state) {

	    var profileManager = function () {
	        var self = this;
	        var $storage = $localStorage;
	        var currentProfile = null;

	        self.getRandomUser = function (num) {

	            $log.debug('Getting User(s) results:' + num);

	            var promise = $http.post(baseUrl + "?results=" + null);

	            promise.error(function (data) { $log.debug(data); });

	            return promise;
	        }


	        self.DoLogin = function (LoginData) {
	            var promise = null;
	            promise = $http.post(baseUrl + "/Auth/doLogin", LoginData).success(function (data) {
	                if (!data.Success) {
	                    $cordovaDialogs.alert(data.AuthResponse, 'Login', 'OK');
	                } else {
	                    self.SetProfile(data);
	                }
	            }).error(function (data) {
	                console.log(data);
	            });

	            return promise;
	        }

	        self.logout = function () {

	            delete $storage.profile;

	            currentProfile = null;

	            $state.go('app.landing');
	        }

	        self.SetProfile = function (profileData) {
	            if (profileData.User != undefined || profileData.User != null) {
	                $storage.profile = profileData.User;
	                $storage.profile.Token = profileData.Token;
	            }

	            currentProfile = $storage.profile;
	        }

	        self.getProfile = function () {
	            if (currentProfile == null) {
	                currentProfile = $storage.profile;
	                if (currentProfile == null) {
	                    self.logout();
	                } else {
	                    $storage.profile.Token = currentProfile.Token;
	                }
	            }
	            return currentProfile == null ? null : currentProfile;
	        };

	        self.getToken = function () {
	            return currentProfile != undefined ? currentProfile.Token : null;
	        }
	    }

	    return new profileManager();
	}])

.factory('deliveryService', function ($localStorage, $http, baseUrl, $log, $cordovaDialogs, profileService) {

    var eventManager = function () {
        var self = this;
        var $storage = $localStorage;
        var DeliveryDetail;
        var currentDeliveries;

        self.getDeliveries = function (rows) {
            var promise = null;
            promise = $http.post(baseUrl + "?results=" + rows).success(function (data) {
                $storage.Deliveries = data.results;
                currentDeliveries = $storage.results;
            }).error(function (data) {
                console.log(data);
            });

            return promise;
        }

        self.getDeliveryById = function (id) {
            DeliveryDetail = _.where(currentDeliveries, { ID: parseInt(id) });
            return DeliveryDetail;
        }
    }

    return new eventManager();
});