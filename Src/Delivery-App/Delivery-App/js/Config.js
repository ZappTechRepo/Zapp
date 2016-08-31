app.config(function ($stateProvider, $urlRouterProvider, $ionicConfigProvider, $httpProvider) {
    $ionicConfigProvider.views.maxCache(1);
    //interceptor to show the ionic loading screen on any and all http requests in the system
    $httpProvider.interceptors.push(function ($rootScope) {
        return {
            request: function (config) {
                $rootScope.$broadcast('loading:show')
                return config
            },
            response: function (response) {
                $rootScope.$broadcast('loading:hide')
                return response
            }  
        } 
    }); 



    $stateProvider

    .state('app', {
        url: '/app',
        abstract: true,
        templateUrl: 'templates/menu.html',
        controller: 'AppCtrl'
    })

    .state('app.deliveries', {
        url: '/deliveries/:userId',
        views: {
            'menuContent': {
                templateUrl: 'templates/deliveries.html',
                controller: 'DeliveriesCtrl'
            }
        }
    })

        .state('app.deliveryDetail', {
            url: '/deliveryDetail/:Id',
            views: {
                'menuContent': {
                    templateUrl: 'templates/deliveryDetail.html',
                    controller: 'DeliveryDetailCtrl'
                }
            }
        })

        .state('app.login', {
            url: '/login',
            views: {
                'menuContent': {
                    templateUrl: 'templates/login.html',
                    controller: 'LoginCtrl'
                }
            }
        })

    .state('app.profile', {
        url: '/profile',
        views: {
            'menuContent': {
                templateUrl: 'templates/profile.html',
                controller: 'ProfileCtrl'
            }
        }
    })
    ;

    // if none of the above states are matched, use this as the fallback
    $urlRouterProvider.otherwise('/app/login');
});