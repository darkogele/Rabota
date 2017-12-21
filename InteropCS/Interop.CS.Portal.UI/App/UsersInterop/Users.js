'use strict';

app.config(function ($stateProvider, appBaseUrl) {
    $stateProvider.state("users", {
        title: 'Корисници',
        url: '/users/{pageIndex:int}/{itemsPerPage:int}/{sortDir:string}/{sortCol:string}',
        controller: "usersinterController",
        templateUrl: appBaseUrl + 'Module/UsersInterop',
        resolve: {
            usersinteroplist: ['usersInteropService', '$stateParams', function (usersInteropService, $stateParams) {
                return usersInteropService.getUsers($stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.sortDir, $stateParams.sortCol);
            }]
        }
    })
        .state("edituserinter", {
            title: 'Измени улога',
            url: '/userint/{id:string}',
            controller: "usereditController",
            templateUrl: 'App/UsersInterop/Templates/EditUserInterop',
            resolve: {
                user: ['usersInteropService', '$stateParams', function (usersInteropService, $stateParams) {
                    return usersInteropService.getUser($stateParams.id);
                }]
            }
        }).state("editcurrentuserinter", {
            title: 'Измени улога',
            url: '/currentuserint',
            controller: "usereditController",
            templateUrl: 'App/UsersInterop/Templates/EditUserInterop',
            resolve: {
                user: [
                    'usersInteropService', function (usersInteropService) {
                        return usersInteropService.getCurrentUser();
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
            getRoles: getRoles,
            updateUser: updateUser,
            deleteUser: deleteUser

        });

        function getUsers(pageIndex, itemsPerPage, sortDir, sortCol) {
            var request = $http({
                method: "GET",
                url: apiUri + "Auth/GetUsers",
                params: {
                    pageIndex: pageIndex,
                    itemsPerPage: itemsPerPage,
                    sortDir: sortDir,
                    sortCol: sortCol
                }
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function getUser(id) {
            var request = $http({
                method: "GET",
                url: apiUri + "Auth/GetUserRoles",
                params: {
                    id: id
                }
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function getCurrentUser(id) {
            var request = $http({
                method: "GET",
                url: apiUri + "Auth/GetUserRolesForCurrentUser",
                params: {
                    id: id
                }
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function getRoles() {
            var request = $http({
                method: "GET",
                url: apiUri + "Auth/GetRoles",
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function updateUser(userId, email, roleNewId, roleOldId, userName,oldpassword, password, confirmPassword) {
            var request = $http({
                method: "GET",
                url: apiUri + "Auth/UpdateUser",
                params: {
                    userId: userId,
                    email: email,
                    roleOldId: roleOldId,
                    roleNewId: roleNewId,
                    userName: userName,
                    oldpassword: oldpassword,
                    password: password,
                    confirmPassword: confirmPassword
                }
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
    }])
.controller("usersinterController", ['$scope', '$location', 'usersInteropService', 'authService', 'alerts', 'usersinteroplist', '$stateParams', '$state',
    function ($scope, $location, usersInteropService, authService, alerts, usersinteroplist, $stateParams, $state) {

        var param = $stateParams;
        $scope.pageIndex = parseInt(param.pageIndex);
        $scope.totalSize = usersinteroplist.totalSize;
        $scope.users = usersinteroplist.items;
        $scope.itemsPerPage = usersinteroplist.pageSize;
        $scope.pageSize = usersinteroplist.pageSize;
        $scope.sortDir = $stateParams.sortDir;
        $scope.sortCol = $stateParams.sortCol;

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
            $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: $scope.pageSize, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
        };

        $scope.itemsPage = [
            { itemId: 1, itemName: '1' },
            { itemId: 3, itemName: '3' },
            { itemId: 5, itemName: '5' },
            { itemId: 7, itemName: '7' },
            { itemId: 10, itemName: '10' }
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
            $state.go($state.current.name, { pageIndex: parseInt(currentPage), itemsPerPage: $scope.pageSize, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
        };
        $scope.pageSizeChanged = function (itemsPerPage) {
            $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: parseInt(itemsPerPage), sortDir: $scope.sortDir, sortCol: $scope.sortCol });
        };
        $scope.selectedRow = null;
        $scope.setClickedRow = function (index) {
            $scope.selectedRow = index;
        };

    }])
    .controller("usereditController", ['$scope', '$location', 'usersInteropService', 'authService', 'alerts', 'user', '$stateParams', '$state',
    function ($scope, $location, usersInteropService, authService, alerts, user, $stateParams, $state) {

        $scope.password = "";
        $scope.confirmPassword = "";
        $scope.oldpassword = "";
        $scope.savee = function(usernewrole, email, userName,oldpassword, password, confirmPassword) {
            usersInteropService.updateUser(user.id, email, usernewrole, user.role[0], userName, oldpassword, password, confirmPassword).then(function (response) {
                if (user.id == response.id) {
                    authService.logOut();
                    alerts.addSuccess("Успешна промена на сопствените податоците. Потребно е повторно да се најавите.");
                    $state.go("login");
                } else {
                    alerts.addSuccess("Успешна промена на податоците на корисникот '" + userName + "'");
                    $state.go('users', { pageIndex: 1, itemsPerPage: 10 });
                }
            });
        };
        usersInteropService.getRoles().then(function (response) {
            var id = user.role[0];
            for (var i = 0; i < response.length; i++) {
                if (response[i].id == id) {
                    $scope.userroleddl = response[i].id;
                }
            }
            $scope.userroleddl = user.role[0];
            $scope.user = user.role;
            $scope.userName = user.userName;
            $scope.email = user.email;
            $scope.roles = response;
        });
        
    }]);