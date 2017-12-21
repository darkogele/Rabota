'use strict';

app.config(function ($stateProvider, appBaseUrl) {
    $stateProvider.state("register", {
        title: 'Регистрирација',
        url: '/register',
        controller: "registerController",
        templateUrl: appBaseUrl + 'Module/Register'
    });
});

angular.module("Register", [])
.service('registerService', ['$http', '$q', 'apiUri', 'handleResponseService', 'authInterceptorService',
    function ($http, $q, apiUri, handleResponseService, authInterceptorService) {
        return ({
            getRegistered: getRegistered,
            getUserRoles: getUserRoles,
            getServiceRoles: getServiceRoles
        });

        function getRegistered(data) {
            var request = $http({
                method: "POST",
                url: apiUri + "Auth/RegisterUser",
                data: data
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function getUserRoles() {
            var request = $http({
                method: "GET",
                url: apiUri + "Auth/GetUserRoles",
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function getServiceRoles() {
            var request = $http({
                method: "GET",
                url: apiUri + "Auth/GetServiceRoles",
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
    }])
    .service('fileUpload', ['$http', 'apiUri', 'handleResponseService', function ($http, apiUri, handleResponseService) {
        return ({
            uploadFileToUrl: uploadFileToUrl
        });
        function uploadFileToUrl(file) {

            var fd = new FormData();
            fd.append('file', file);
            var uploadUrl = apiUri + "Auth/GetCertPublicKey";
            var request = $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            });

            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }

    }])
.controller('registerController', ['$scope', '$location', 'registerService', 'authService', 'alerts', 'fileUpload', '$state',
    function ($scope, $location, registerService, authService, alerts, fileUpload, $state) {

        angular.element("input[type='file']").val(null);
        $scope.selectedServiceRoles = [];
        $scope.registration = {
            userName: "",
            password: "",
            confirmPassword: ""
        };

        $scope.passwordPattern = (function () {
            var regExpPasswordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()]).{6,}$/;

            return {
                test: function (value) {
                    if ($scope.password === false) {
                        return true;
                    }
                    return regExpPasswordPattern.test(value);
                }
            };
        })();


        $scope.uploadFile = function () {
            fileUpload.uploadFileToUrl($scope.registration.myFile).then(function (publicKey) {
                $scope.registration.publicKey = publicKey;
            });
        };
        registerService.getUserRoles().then(function (response) {
            $scope.userRoles = response
        });
        registerService.getServiceRoles().then(function (response) {
            $scope.serviceRoles = response
        });
        $scope.signUp = function () {
            //authService.logOut();
            $scope.registration.selectedServiceRoles = $scope.selectedServiceRoles;
            registerService.getRegistered($scope.registration).then(function (response) {
                alerts.addSuccess("Регистриран е корисник со следниве податоци:  корисничко име: '" + response.username + "' и улога: '" + response.userRole + "'");
                $scope.registration = {};
                angular.element("input[type='file']").val(null);
                $state.go('users', { pageIndex: 1, itemsPerPage: 10 });
            });
        };
        $scope.toggleSelection = function toggleSelection(id) {
            var idx = $scope.selectedServiceRoles.indexOf(id);

            // is currently selected
            if (idx > -1) {
                $scope.selectedServiceRoles.splice(idx, 1);
            }

                // is newly selected
            else {
                $scope.selectedServiceRoles.push(id);
            }
        };
    }]);