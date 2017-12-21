'use strict';
app.config(function ($stateProvider) {
    $stateProvider
        .state("forgotpassword", {
            title: 'Заборавена лозинка',
            url: '/forgotpassword',
            controller: "forgotPasswordController",
            templateUrl: 'App/ForgotPassword/Templates/ForgotPassword'
        });
});

angular.module("ForgotPassword", [])
    .service('forgotPasswordService', ['$http', '$q', 'apiUri', 'handleResponseService', 'authInterceptorService',
        function ($http, $q, apiUri, handleResponseService, authInterceptorService) {
            return ({
                changePassword: changePassword
            });
            function changePassword(changePasswordData) {
                var request = $http({
                    method: "POST",
                    url: apiUri + "Auth/ChangePassword",
                    data: changePasswordData
                });
                return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
            }
        }])
    .controller('forgotPasswordController', ['$scope', '$location', '$state', 'alerts', 'forgotPasswordService', function ($scope, $location, $state, alerts, forgotPasswordService) {
        $scope.changePassword = function (changePasswordData) {
            forgotPasswordService.changePassword(changePasswordData).then(function () {
                alerts.addSuccess("Успешна промена на лозинката.");
                $state.go("login");
            });
        };
    }]);