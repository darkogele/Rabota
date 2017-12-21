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
                url: apiUri + "Auth/GetRoles",
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
    }])
    .service('fileUploadUser', ['$http', 'apiUri', 'handleResponseService', function ($http, apiUri, handleResponseService) {
        return ({
            uploadFileToUrl: uploadFileToUrl
        });
        function uploadFileToUrl(file) {


            var fd = new FormData();
            fd.append('file', file);
            var uploadUrl = apiUri + "Auth/Upload";
            var request = $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                  ));
        }
    }])
.controller('registerController', ['$scope', '$location', 'registerService', 'authService', 'alerts', 'fileUploadUser','$state',
    function ($scope, $location, registerService, authService, alerts, fileUploadUser,$state) {

        angular.element("input[type='file']").val(null);

        $scope.registration = {
            userName: "",
            password: "",
            confirmPassword: ""
        };
        registerService.getUserRoles().then(function (response) {
            $scope.userRoles = response
        });
        $scope.uploadFile = function () {
            fileUploadUser.uploadFileToUrl($scope.registration.myFile).then(function (publicKey) {
                $scope.registration.publicKey = publicKey;
            });
        };

        $scope.signUp = function () {
            //authService.logOut();
            registerService.getRegistered($scope.registration).then(function (response) {
                alerts.addSuccess("Регистриран е корисник со следниве податоци:  корисничко име: '" + response.username + "' и ролја: '" + response.userRole + "'");
                $scope.registration = {};
                angular.element("input[type='file']").val(null);
                $state.go('users', { pageIndex: 1, itemsPerPage: 10 });
            });
        };

    }]);