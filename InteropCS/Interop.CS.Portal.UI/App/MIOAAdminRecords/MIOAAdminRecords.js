'use strict';

app.config(function ($stateProvider, appBaseUrl) {
    $stateProvider.state("adminrecords", {
        title: 'Административни податоци',
        url: '/adminrecords/{pageIndex:int}/{itemsPerPage:int}/{sortDir:string}/{sortCol:string}',
        controller: "adminrecordsController",
        templateUrl: appBaseUrl + 'Module/MIOAAdminRecords',
        resolve: {
            adminrecordslist: ['adminRecordsService', '$stateParams', function (adminRecordsService, $stateParams) {
                return adminRecordsService.getAdminRecords($stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.sortDir, $stateParams.sortCol);
            }]
        }
    })
        .state("detailrecords", {
            title: 'Детали',
            url: '/detailint/{id:string}/{pageIndex:int}/{itemsPerPage:int}/{sortDir:string}/{sortCol:string}',
            controller: "detailrecordsController",
            templateUrl: 'App/MIOAAdminRecords/Templates/RecordsDetails',
            resolve: {
                recordloglist: ['adminRecordsService', '$stateParams', function (adminRecordsService, $stateParams) {
                    return adminRecordsService.getDetails($stateParams.id, $stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.sortDir, $stateParams.sortCol);
                }]
            }
        })
    .state("editrecords", {
        title: 'Измени податоци',
        url: '/recordsint/{id:string}',
        controller: "editrecordsController",
        templateUrl: 'App/MIOAAdminRecords/Templates/EditRecords',
        resolve: {
            record: ['adminRecordsService', '$stateParams', function (adminRecordsService, $stateParams) {
                return adminRecordsService.getRecord($stateParams.id);
            }]
        }
    })
    .state('createrecord', {
        title: 'Креирај податок',
        controller: 'createrecordController',
        url: '/records/new',
        templateUrl: 'App/MIOAAdminRecords/Templates/CreateRecord'
    });
});

angular.module("MIOAAdminRecords", [])
.service('adminRecordsService', ['$http', '$q', 'apiUri', 'handleResponseService', 'authInterceptorService',
    function ($http, $q, apiUri, handleResponseService, authInterceptorService) {
        return ({
            getAdminRecords: getAdminRecords,
            getDetails: getDetails,
            getRecord: getRecord,
            updateRecord: updateRecord,
            createRecord: createRecord
        });
        function getAdminRecords(pageIndex, itemsPerPage, sortDir, sortCol) {
            var request = $http({
                method: "GET",
                url: apiUri + "MIOARecords/GetAdministrativeRecords",
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
                url: apiUri + "MIOARecords/GetRecordDetails",
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
        function getRecord(id) {
            var request = $http({
                method: "GET",
                url: apiUri + "MIOARecords/GetRecord",
                params: {
                    id: id
                }
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function updateRecord(rec) {
            var request = $http({
                method: "POST",
                url: apiUri + "MIOARecords/UpdateRecord",
                data: rec
            });
            //return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
        function createRecord(rec) {

            var request = $http({
                method: "POST",
                data: rec,
                url: apiUri + "MIOARecords/CreateRecord"
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
    }])
.controller("adminrecordsController", ['$scope', '$location', 'adminRecordsService', 'authService', 'alerts', 'adminrecordslist', '$stateParams', '$state',
    function ($scope, $location, adminRecordsService, authService, alerts, adminrecordslist, $stateParams, $state) {

        var param = $stateParams;
        $scope.pageIndex = parseInt(param.pageIndex);
        $scope.totalSize = adminrecordslist.totalSize;
        $scope.records = adminrecordslist.items;
        $scope.itemsPerPage = adminrecordslist.pageSize;
        $scope.pageSize = adminrecordslist.pageSize;
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
.controller("detailrecordsController", ['$scope', '$location', 'adminRecordsService', 'authService', 'alerts', 'recordloglist', '$stateParams', '$state',
function ($scope, $location, adminRecordsService, authService, alerts, recordloglist, $stateParams, $state) {

    
    var param = $stateParams;
    $scope.pageIndex = parseInt(param.pageIndex);
    $scope.totalSize = recordloglist.totalSize;
    $scope.logs = recordloglist.items;
    $scope.itemsPerPage = recordloglist.pageSize;
    $scope.pageSize = recordloglist.pageSize;
    $scope.sortDir = $stateParams.sortDir;
    $scope.sortCol = $stateParams.sortCol;

    for (var i = 0; i < $scope.logs.length; i++) {
        var res = $scope.logs[i].performedActivity.split(",");
        $scope.logs[i].itemNumber = res[0];
        $scope.logs[i].dateReceived = res[1];
        $scope.logs[i].applicantNameAddress = res[2];
        $scope.logs[i].electronicDBName = res[3];
        $scope.logs[i].electronicDBTypeVersion = res[4];
        $scope.logs[i].dataType = res[5];
        $scope.logs[i].legislationData = res[6];
        $scope.logs[i].authorizedPersonData = res[7];
        $scope.logs[i].note = res[8];
        $scope.logs[i].optionalField1 = res[9];
        $scope.logs[i].optionalField2 = res[10];
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
.controller("editrecordsController", ['$scope', '$location', 'adminRecordsService', 'authService', 'alerts', 'record', '$stateParams', '$state',
function ($scope, $location, adminRecordsService, authService, alerts, record, $stateParams, $state) {

    $scope.rec = {};
    $scope.rec = record;


    $scope.savee = function (rec) {
        adminRecordsService.updateRecord(rec).then(function (response) {
                alerts.addSuccess("Успешна промена на податоците");
                $state.go('adminrecords', { pageIndex: 1, itemsPerPage: 10, sortDir:'asc', sortCol:'ItemNumber' });
        
        });
    };
}])
.controller("createrecordController", ['$scope', '$location', 'adminRecordsService', 'authService', 'alerts',  '$stateParams', '$state',
function ($scope, $location, adminRecordsService, authService, alerts, $stateParams, $state) {

    $scope.rec = {};

    $scope.savee = function (rec) {
       // $scope.rec.dateReceived = $scope.rec.dateReceived + 1;
        adminRecordsService.createRecord(rec).then(function (response) {
            alerts.addSuccess("Успешна промена на податоците");
            $state.go('adminrecords', { pageIndex: 1, itemsPerPage: 10, sortDir: 'asc', sortCol: 'ItemNumber' });

        });
    };
}]);