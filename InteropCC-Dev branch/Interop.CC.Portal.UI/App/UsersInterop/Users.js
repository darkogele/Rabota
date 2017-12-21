'use strict';

app.config(function ($stateProvider, appBaseUrl) {
    $stateProvider.state("users", {
        title: 'Корисници',
        url: '/users/{pageIndex:int}/{itemsPerPage:int}/{sortDir:string}/{sortCol:string}/{userName:string}/{userRole:string}',
        controller: "usersinterController",
        templateUrl: appBaseUrl + 'Module/UsersInterop',
        resolve: {
            usersinteroplist: [
                'usersInteropService', '$stateParams', function(usersInteropService, $stateParams) {
                    return usersInteropService.getUsers($stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.sortDir, $stateParams.sortCol, $stateParams.userName, $stateParams.userRole);
                }
            ]
        }
    })
        .state("edituserinter", {
            title: 'Измени улога',
            url: '/userint/{id:string}',
            controller: "usereditController",
            templateUrl: 'App/UsersInterop/Templates/EditUserInterop',
            resolve: {
                user: [
                    'usersInteropService', '$stateParams', function(usersInteropService, $stateParams) {
                        return usersInteropService.getUser($stateParams.id);
                    }
                ]
            }
        })
        .state("editcurrentuserinter", {
            title: 'Измени улога',
            url: '/currentuserint',
            controller: "usereditController",
            templateUrl: 'App/UsersInterop/Templates/EditUserInterop',
            resolve: {
                user: [
                    'usersInteropService', function(usersInteropService) {
                        return usersInteropService.getCurrentUser("own");
                    }
                ]
            }
        });

});

angular.module("UsersInterop", [])
.service('usersInteropService', ['$http', '$q', 'apiUri', 'handleResponseService', 'authInterceptorService',
    function ($http, $q, apiUri, handleResponseService, authInterceptorService) {
        return ({
            getUsers: getUsers,
            getUser: getUser,
            getCurrentUser: getCurrentUser,
            updateUser: updateUser,
            deleteUser: deleteUser,
            createAllUserRoles: createAllUserRoles,
            getUserRoles: getUserRoles
        });

        function createAllUserRoles() {
            var request = $http({
                method: "POST",
                url: apiUri + "Auth/CreateAllUserRoles"
            });
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }

        function getUsers(pageIndex, itemsPerPage, sortDir, sortCol, userName, userRole) {
            var request = $http({
                method: "GET",
                url: apiUri + "Auth/GetUsers",
                params: {
                    pageIndex: pageIndex,
                    itemsPerPage: itemsPerPage,
                    sortDir: sortDir,
                    sortCol: sortCol,
                    userName: userName,
                    userRole: userRole
                }
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function getUser(id) {
            var request = $http({
                method: "GET",
                url: apiUri + "Auth/GetUserDetails",
                params: {
                    userId: id
                }
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function getCurrentUser(id) {
            var request = $http({
                method: "GET",
                url: apiUri + "Auth/GetUserDetails",
                params: {
                    userId: id
                }
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }

        function updateUser(user) {
            var request = $http({
                method: "POST",
                url: apiUri + "Auth/UpdateUser",
                data: user
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function deleteUser(userId) {
            var request = $http({
                method: "GET",
                url: apiUri + "Auth/DeleteUser",
                params: {
                    userId: userId
                }
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function getUserRoles() {
            var request = $http({
                method: "GET",
                url: apiUri + "Auth/GetUserRolesList",
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
    }])
    .service('sharedPropertiesUsers', function () {

        var parameters = [];

        return ({
            getParameters: getParameters,
            setParameters: setParameters,
        });

        function getParameters() {
            return parameters;
        }

        function setParameters(itemsPerPage, currentPage, sortDir, sortCol, userName, userRole) {
            parameters.itemsPerPage = itemsPerPage;
            parameters.currentPage = currentPage;
            parameters.sortDir = sortDir;
            parameters.sortCol = sortCol;
            parameters.userName = userName;
            parameters.userRole = userRole;
        }


    })
.controller("usersinterController", ['$scope', '$location', 'usersInteropService', 'authService', 'alerts', 'usersinteroplist', '$stateParams', '$state', 'sharedPropertiesUsers',
    function ($scope, $location, usersInteropService, authService, alerts, usersinteroplist, $stateParams, $state, sharedPropertiesUsers) {

        var param = $stateParams;
        $scope.pageIndex = parseInt(param.pageIndex);
        $scope.totalSize = usersinteroplist.totalSize;
        $scope.users = usersinteroplist.items;
        $scope.itemsPerPage = usersinteroplist.pageSize;
        $scope.pageSize = usersinteroplist.pageSize;
        $scope.sortDir = $stateParams.sortDir;
        $scope.sortCol = $stateParams.sortCol;
        $scope.search = {};

        $scope.search.user = $stateParams.userName;
        $scope.search.role = $stateParams.userRole;

        //$scope.search = [];
        //$scope.search.user = "";
        //$scope.search.role = "";

        //$scope.searchUserName = "";


        $scope.userRoles = [];



        function initRoles() {
            usersInteropService.getUserRoles().then(function (response) {
                $scope.userRoles = response;
            });

        };
        initRoles();
        $scope.sort = function (columnName) {
            if (columnName == $scope.sortCol) {
                if ($scope.sortDir == "asc") {
                    $scope.sortDir = "desc";
                } else {
                    $scope.sortDir = "asc";
                }
            } else {
                $scope.sortCol = columnName;
            }
            $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: $scope.pageSize, sortDir: $scope.sortDir, sortCol: $scope.sortCol, userName: $scope.search.user, userRole: $scope.search.role });
        };


        $scope.itemsPage = [
            { itemId: 10, itemName: '10' },
            { itemId: 30, itemName: '30' },
            { itemId: 50, itemName: '50' },
            { itemId: 70, itemName: '70' },
            { itemId: 100, itemName: '100' }
        ];
        $scope.deleteUser = function (userId) {
            usersInteropService.deleteUser(userId).then(function () {
                alerts.addSuccess("Корисникот е избришан");
                usersInteropService.getUsers($stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.sortDir, $stateParams.sortCol).then(function (users) {
                    $scope.users = users.items;
                });

            });
        };
        $scope.pageChangeHandler = function (currentPage) {
            $state.go($state.current.name, { pageIndex: parseInt(currentPage), itemsPerPage: $scope.pageSize, sortDir: $scope.sortDir, sortCol: $scope.sortCol, userName: $scope.search.user, userRole: $scope.search.role });
        };
        $scope.pageSizeChanged = function (itemsPerPage) {
            $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: parseInt(itemsPerPage), sortDir: $scope.sortDir, sortCol: $scope.sortCol, userName: $scope.search.user, userRole: $scope.search.role });
        };
        $scope.selectedRow = null;
        $scope.setClickedRow = function (index) {
            $scope.selectedRow = index;
        };

        $scope.createAllUserRoles = function () {
            usersInteropService.createAllUserRoles().then(function (responseMsg) {
                alerts.addInfo(responseMsg + "<br/>");
            });
        };
        $scope.changeUser = function (itemsPerPage, currentPage, sortDir, sortCol, userName, userRole) {
            sharedPropertiesUsers.setParameters(itemsPerPage, currentPage, sortDir, sortCol, userName, userRole);
        };
        $scope.filterUsers = function () {
            $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: $scope.pageSize, sortDir: $scope.sortDir, sortCol: $scope.sortCol, userName: $scope.search.user, userRole: $scope.search.role });
        };
        $scope.filterClear = function () {
            $scope.search = [];
            $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: $scope.pageSize, sortDir: $scope.sortDir, sortCol: $scope.sortCol, userName: $scope.search.user, userRole: $scope.search.role });
        }

    }])
    .controller("usereditController", ['$scope', '$location', 'usersInteropService', 'sharedPropertiesUsers', 'authService', 'alerts', 'user', '$stateParams', '$state',
    function ($scope, $location, usersInteropService, sharedPropertiesUsers, authService, alerts, user, $stateParams, $state) {

        $scope.user = {};

        $scope.user.password = "";
        $scope.user.confirmPassword = "";
        $scope.user.oldpassword = "";
        $scope.user.userRole = "";
        $scope.user.email = "";
        $scope.selectedServiceRoles = [];
        $scope.user = user;


        for (var i = 0; i < user.userRoles.length; i++) {
            if (user.userRoles[i].isSelected) {
                $scope.user.userRole = user.userRoles[i].id;
            }
        }
        for (var i = 0; i < user.serviceRoles.length; i++) {
            if (!user.serviceRoles[i].isAvailableForSelecting) {
                $scope.selectedServiceRoles.push(user.serviceRoles[i].id);
            }
        }
        $scope.save = function (user) {
            $scope.user.selectedServiceRoles1 = $scope.selectedServiceRoles;
            usersInteropService.updateUser(user).then(function (response) {
                if (user.userId == response.id) {
                    authService.logOut();
                    alerts.addSuccess("Успешна промена на сопствените податоците. Потребно е повторно да се најавите.");
                    $state.go("login");
                } else {
                    alerts.addSuccess("Успешна промена на податоците на корисникот '" + user.userName + "'");
                    $state.go('users', { pageIndex: sharedPropertiesUsers.getParameters().itemsPerPage, itemsPerPage: sharedPropertiesUsers.getParameters().currentPage, sortDir: sharedPropertiesUsers.getParameters().sortDir, sortCol: sharedPropertiesUsers.getParameters().sortCol, userName: sharedPropertiesUsers.getParameters().userName, userRole: sharedPropertiesUsers.getParameters().userRole });
                }
            });
        };
        $scope.discardChange = function() {
            $state.go('users', { pageIndex: sharedPropertiesUsers.getParameters().itemsPerPage, itemsPerPage: sharedPropertiesUsers.getParameters().currentPage, sortDir: sharedPropertiesUsers.getParameters().sortDir, sortCol: sharedPropertiesUsers.getParameters().sortCol, userName: sharedPropertiesUsers.getParameters().userName, userRole: sharedPropertiesUsers.getParameters().userRole });
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