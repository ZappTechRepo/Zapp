// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
var app = angular.module('starter', ['ionic', 'ngCordova', 'ngStorage', 'ionic-material', 'base64']);

app.run(function ($ionicPlatform, $rootScope, $ionicLoading) {
    $ionicPlatform.ready(function () {
        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)

        if (window.StatusBar) {
            //StatusBar.styleDefault();
            StatusBar.backgroundColorByHexString('#008987');
        }
         
        if (window.cordova && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
        }

    });

    $rootScope.$on('loading:show', function () {
        $ionicLoading.show();
    });

    $rootScope.$on('loading:hide', function () {
        $ionicLoading.hide();
    });
})