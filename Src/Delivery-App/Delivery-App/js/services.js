app

.factory('profileService', ['$localStorage', '$http', 'baseUrl', '$log', '$cordovaDialogs', '$state', '$base64',
	function ($localStorage, $http, baseUrl, $log, $cordovaDialogs, $state, $base64) {

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

	            var base64EncodedString = $base64.encode(LoginData.username + ':' + LoginData.password);

	            $http.defaults.headers.common['Authorization'] = 'Basic ' + base64EncodedString;

	            var promise = null;
	            promise = $http.post(baseUrl + "api/authenticate/login").success(function (data) {
	                if (!data.Success) {
	                    $cordovaDialogs.alert("Invalid username and password combination", 'Login', 'OK');
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
	            if (profileData != undefined || profileData != null) {
	                $storage.profile = profileData;
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

            $http.defaults.headers.common['token'] = $localStorage.token;
            $http.get(baseUrl + 'api/document/GetDocuments/' + $localStorage.userId).then(function (response) {
                if (response.data) {
                    var completed = [], process = [];
                    for (var i = 0; i < response.data.length; i++) {
                        if (response.data[i].orderDelivered)
                            completed[completed.length] = response.data[i];
                        else
                            process[process.length] = response.data[i];
                    }

                    $localStorage.Documents = process;
                    $localStorage.CompletedDocuments = completed;
                    if (callback) {
                        callback(process, completed);
                    }
                }
            }, function (response) {
                if (response.status === 401) {
                    LoginService.LogOutUser();
                    $state.go('login');
                }
                alert('An error occurred while trying to retrieve the delivery list');
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