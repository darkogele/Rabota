'use strict';

app.config(function ($stateProvider, appBaseUrl) {
    $stateProvider.state("adminservices", {
        title: 'Административни услуги',
        url: '/adminservices/{pageIndex:int}/{itemsPerPage:int}/{sortDir:string}/{sortCol:string}',
        controller: "adminservicesController",
        templateUrl: appBaseUrl + 'Module/MIOAAdminServices',
        resolve: {
            adminserviceslist: ['adminServicesService', '$stateParams', function (adminServicesService, $stateParams) {
                return adminServicesService.getAdminServices($stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.sortDir, $stateParams.sortCol);
            }]
        }
    })
     .state("detailservices", {
         title: 'Детали',
         url: '/detailint/{id:string}/{pageIndex:int}/{itemsPerPage:int}/{sortDir:string}/{sortCol:string}',
         controller: "detailservicesController",
         templateUrl: 'App/MIOAAdminServices/Templates/ServicesDetails',
         resolve: {
             servicee: ['adminServicesService', '$stateParams', function (adminServicesService, $stateParams) {
                 return adminServicesService.getDetails($stateParams.id, $stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.sortDir, $stateParams.sortCol);
             }]
         }
     })
    .state("editservices", {
        title: 'Измени податоци',
        url: '/servicesint/{id:string}',
        controller: "editservicesController",
        templateUrl: 'App/MIOAAdminServices/Templates/EditServices',
        resolve: {
            servicce: ['adminServicesService', '$stateParams', function (adminServicesService, $stateParams) {
                return adminServicesService.getService($stateParams.id);
            }]
        }
    })
    .state('createservice', {
        title: 'Креирај сервис',
        controller: 'createserviceController',
        url: '/services/new',
        templateUrl: 'App/MIOAAdminServices/Templates/CreateService'
    });
});

angular.module("MIOAAdminServices", [])
.service('adminServicesService', ['$http', '$q', 'apiUri', 'handleResponseService', 'authInterceptorService',
    function ($http, $q, apiUri, handleResponseService, authInterceptorService) {
        return ({
            getAdminServices: getAdminServices,
            getDetails: getDetails,
            getService: getService,
            updateService: updateService,
            createService: createService
        });
        function getAdminServices(pageIndex, itemsPerPage, sortDir, sortCol) {
            var request = $http({
                method: "GET",
                url: apiUri + "MIOARecords/GetAdministrativeServices",
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
        function getDetails(id, pageIndex, itemsPerPage, sortDir, sortCol) {
            var request = $http({
                method: "GET",
                url: apiUri + "MIOARecords/GetServiceDetails",
                params: {
                    id: id,
                    pageIndex: pageIndex,
                    itemsPerPage: itemsPerPage,
                    sortDir: sortDir,
                    sortCol: sortCol
                }
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function getService(id) {
            var request = $http({
                method: "GET",
                url: apiUri + "MIOARecords/GetService",
                params: {
                    id: id
                }
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function updateService(ser) {
            var request = $http({
                method: "POST",
                url: apiUri + "MIOARecords/UpdateService",
                data: ser
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function createService(ser) {
            var request = $http({
                method: "POST",
                data: ser,
                url: apiUri + "MIOARecords/CreateService"
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
    }])
.controller("adminservicesController", ['$scope', '$location', 'adminServicesService', 'authService', 'alerts', 'adminserviceslist', '$stateParams', '$state',
    function ($scope, $location, adminServicesService, authService, alerts, adminserviceslist, $stateParams, $state) {

        var param = $stateParams;
        $scope.pageIndex = parseInt(param.pageIndex);
        $scope.totalSize = adminserviceslist.totalSize;
        $scope.services = adminserviceslist.items;
        $scope.itemsPerPage = adminserviceslist.pageSize;
        $scope.pageSize = adminserviceslist.pageSize;
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
.controller("detailservicesController", ['$scope', '$location', 'adminServicesService', 'authService', 'alerts', 'servicee', '$stateParams', '$state',
function ($scope, $location, adminServicesService, authService, alerts, servicee, $stateParams, $state) {


    var param = $stateParams;
    $scope.pageIndex = parseInt(param.pageIndex);
    $scope.totalSize = servicee.totalSize;
    $scope.logs = servicee.items;
    $scope.itemsPerPage = servicee.pageSize;
    $scope.pageSize = servicee.pageSize;
    $scope.sortDir = $stateParams.sortDir;
    $scope.sortCol = $stateParams.sortCol;

    for (var i = 0; i < $scope.logs.length; i++) {
        var res = $scope.logs[i].performedActivity.split(",");
        $scope.logs[i].itemNumber = res[0];
        $scope.logs[i].dateReceived = res[1];
        $scope.logs[i].applicantNameAddress = res[2];
        $scope.logs[i].administrativeServiceName = res[3];
        $scope.logs[i].administrativeServiceDesc = res[4];
        $scope.logs[i].legislationForAdminService = res[5];
        $scope.logs[i].legislationForAdminServicePayment = res[6];
        $scope.logs[i].adminServicePaymentAmount = res[7];
        $scope.logs[i].adminServiceSubmissionLeagalDeadline = res[8];
        $scope.logs[i].deliveringSpecialFormData = res[9];
        $scope.logs[i].previousSubmittedDocDependencyData = res[10];
        $scope.logs[i].electronicDBTypeVersion = res[11];
        $scope.logs[i].authorizedPersonData = res[12];
        $scope.logs[i].note = res[13];
        $scope.logs[i].optionalField1 = res[14];
        $scope.logs[i].optionalField2 = res[15];
    }

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
.controller("editservicesController", ['$scope', '$location', 'adminServicesService', 'authService', 'alerts', 'servicce', '$stateParams', '$state',
function ($scope, $location, adminServicesService, authService, alerts, servicce, $stateParams, $state) {

    $scope.ser = {};
    $scope.ser = servicce;


    $scope.savee = function (ser) {
        adminServicesService.updateService(ser).then(function (response) {
            alerts.addSuccess("Успешна промена на податоците");
            $state.go('adminservices', { pageIndex: 1, itemsPerPage: 10, sortDir: 'asc', sortCol: 'ItemNumber' });

        });
    };
}])
.controller("createserviceController", ['$scope', '$location', 'adminServicesService', 'authService', 'alerts', '$stateParams', '$state',
function ($scope, $location, adminServicesService, authService, alerts, $stateParams, $state) {

    $scope.ser = {};

    $scope.savee = function (ser) {
        adminServicesService.createService(ser).then(function (response) {
            alerts.addSuccess("Успешна промена на податоците");
            $state.go('adminservices', { pageIndex: 1, itemsPerPage: 10, sortDir: 'asc', sortCol: 'ItemNumber' });

        });
    };
}]);
