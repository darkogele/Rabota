
app.config(function($stateProvider, appBaseUrl) {
    $stateProvider
        .state('home', {
            title: "Почетна",
            controller: 'HomeCtrl',
            url: '/home',
            templateUrl: appBaseUrl + 'Module/Home'
        });
});

angular.module("Home", ['ngTable', 'ui.bootstrap'])
 .service('homeService', ['$http', '$q', 'apiUri', 'handleResponseService',
      function ($http, $q, apiUri, handleResponseService) {


      }])
.controller("HomeCtrl", ['$scope', '$modal', 'ngTableParams', 'ngProgress',
    function ($scope, $modal, ngTableParams, ngProgress) {


    }]);


