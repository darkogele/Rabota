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

angular.module("ForgotPassword", ['ui.bootstrap'])
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
        
        $scope.showPassword = false;
        $scope.showForgotPassword = false;

        $scope.passwordPattern = (function() {
            var regExpPasswordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()]).{6,}$/;

            return {
                test: function(value) {
                    if ($scope.password === false) {
                        return true;
                    }
                    var result = regExpPasswordPattern.test(value);
                    if (result) {
                        $scope.showForgotPassword = true;
                    }
                    return result;
                }
            };
        })();

       $scope.changePassword = function (changePasswordData) {
            if (!$scope.forgotPasswordForm.$invalid) {
                forgotPasswordService.changePassword(changePasswordData).then(function () {
                    alerts.addSuccess("Успешна промена на лозинката.");
                    $state.go("login");
                });
            } 
        };

        $scope.toggleShowPassword = function () {
            $scope.showPassword = !$scope.showPassword;
        };

        $scope.checkPassword = function() {
            if ($scope.forgotPasswordForm.confirmPassword === $scope.forgotPasswordForm.password) {
                return true;
            } else
                return false;
        }
    }]);