'use strict';
app.config(function ($stateProvider, appBaseUrl) {
    $stateProvider.
        state("login", {
            title: 'Најава',
            url: '/login',
            controller: "loginController",
            templateUrl: appBaseUrl + 'Module/Login'
        });
});

angular.module("Login", []).
    controller('loginController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

        $scope.loginData = {
            userName: "",
            password: ""
        };

        //$scope.message = "";

        $scope.login = function () {
            authService.login($scope.loginData).then(function(response) {
                $location.path('/home');

            });
        };

    }]);