

app.config(function($stateProvider, appBaseUrl) {

    $stateProvider
        .state("soapfault", {
            title: 'Грешки',
            controller: "SoapFaultCtrl",
            url: "/soapfault/{pageIndex:int}/{itemsPerPage:int}/{filterTransaction}/:fromDateSoap/:toDateSoap",
            templateUrl: appBaseUrl + "Module/SoapFault",
            resolve: {
                pagedSoapFaults: ['soapfaultService', '$stateParams', function (soapfaultService, $stateParams) {
                    return soapfaultService.getpagedSoapFaults($stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.filterTransaction, $stateParams.fromDateSoap, $stateParams.toDateSoap);
                }]
            }
        });

});

angular.module("SoapFault", [])
    .service("soapfaultService", ['$http', '$q', 'handleResponseService', 'apiUri',
        function($http, $q, handleResponseService, apiUri) {
            return ({
                getpagedSoapFaults: getpagedSoapFaults,
                createPdf: createPdf,
                createExcel: createExcel
            });

            function getpagedSoapFaults(pageIndex, itemsPerPage, filterTransaction, fromDateSoap, toDateSoap) {
                var request = $http({
                    method: 'GET',
                    url: apiUri + "SoapFault/GetSoapFaultListPaged",
                    params: {
                        pageIndex: pageIndex,
                        itemsPerPage: itemsPerPage,
                        filterTransaction: filterTransaction,
                        fromDateSoap: fromDateSoap,
                        toDateSoap: toDateSoap
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }

            function createPdf(filterTransaction, fromDateSoap, toDateSoap) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "SoapFault/CreatePdf",
                    responseType: "arraybuffer",

                    params: {
                        filterTransaction: filterTransaction,
                        fromDateSoap: fromDateSoap,
                        toDateSoap: toDateSoap
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }

            function createExcel(filterTransaction, fromDateSoap, toDateSoap) {
                var request = $http({
                    method: "POST",
                    url: apiUri + "SoapFault/CreateExcel",
                    responseType: "arraybuffer",

                    params: {
                        filterTransaction: filterTransaction,
                        fromDateSoap: fromDateSoap,
                        toDateSoap: toDateSoap
            
        }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }

        }])
   
.service('sharedProperySoapFault', ['$http', function ($http) {
    var obj = [];
    return ({
        setMsgProp: setMsgProp,
        getMsgProp: getMsgProp
    });



    function setMsgProp(pageIndex, itemsPerPage, filterTransaction, fromDateSoap, toDateSoap) {
        obj.pageIndex = pageIndex;
        obj.itemsPerPage = itemsPerPage;
        obj.filterTransaction = filterTransaction;
        obj.fromDateSoap = fromDateSoap;
        obj.toDateSoap = toDateSoap;
    }

    function getMsgProp() {
        return obj;
    }

}])
.controller("SoapFaultCtrl", ['$scope', 'soapfaultService', '$state', 'alerts','sharedProperySoapFault','pagedSoapFaults','$stateParams',
function ($scope, soapfaultService, $state, alerts, sharedProperySoapFault, pagedSoapFaults, $stateParams) {
    
    var param = $stateParams;
    $scope.soapFaults = pagedSoapFaults.items;
    $scope.pageIndex = parseInt(param.pageIndex);
    $scope.totalSize = pagedSoapFaults.totalSize;
    $scope.itemsPerPage = pagedSoapFaults.pageSize;
    $scope.pageSize = pagedSoapFaults.pageSize;
    
    $scope.filterTransaction = param.filterTransaction;
    
    if (param.fromDateSoap == "") {
        $scope.fromDateSoap = param.fromDateSoap;
    } else {
        var datefrom = param.fromDateSoap.split(".");
        datefrom = datefrom[1] + '.' + datefrom[0] + '.' + datefrom[2];
        $scope.fromDateSoap = datefrom;
        //$scope.fromDateSoap = decodeURIComponent(param.fromDateSoap);
    }
    if (param.toDateSoap == "") {
        $scope.toDateSoap = param.toDateSoap;
    } else {
        var dateto = param.toDateSoap.split(".");
        dateto = dateto[1] + '.' + dateto[0] + '.' + dateto[2];
        $scope.toDateSoap = dateto;
        //$scope.toDateSoap = decodeURIComponent(param.toDateSoap);
    }
    

    $scope.maxToDateTimeSoap = new Date();
    $scope.maxFromDateTimeSoap = $scope.toDateSoap != null ? $scope.toDateSoap : new Date();
    $scope.status1 = {
        opened: false
    };
    $scope.status2 = {
        opened: false
    };

    $scope.open1 = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status2.opened = false;
        $scope.status1.opened = true;
    };
    $scope.open2 = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status1.opened = false;
        $scope.status2.opened = true;
    };




    $scope.itemsPage = [
       { itemId: 1, itemName: '1' },
       { itemId: 3, itemName: '3' },
       { itemId: 5, itemName: '5' },
       { itemId: 7, itemName: '7' },
       { itemId: 10, itemName: '10' }
    ];

    $scope.pageChangeHandler = function (currentPage) {
        
        var todatesoap, fromdatesoap;

        if (typeof $scope.fromDateSoap === "string") {
            if ($scope.fromDateSoap !== "") {
                var stringDateFrom = $scope.fromDateSoap.split('.');
                fromdatesoap = stringDateFrom[1] + '.' + stringDateFrom[0] + '.' + stringDateFrom[2];
                $scope.fromDateSoap = new Date(stringDateFrom[2], stringDateFrom[1] - 1, stringDateFrom[0]);
            } else {
                fromdatesoap = $scope.fromDateSoap;
            }
        } else if (typeof $scope.fromDateSoap === "object") {
            if ($scope.fromDateSoap) {
                var fromD = $scope.fromDateSoap;
                fromdatesoap = (fromD.getMonth() + 1) + "." + fromD.getDate() + "." + fromD.getFullYear();
                $scope.fromDateSoap = new Date(fromD.getFullYear().toString(), fromD.getMonth().toString(), fromD.getDate().toString());
            }
        } else {
            fromdatesoap = $scope.fromDateSoap;
        }

        if (typeof $scope.toDateSoap === "string") {
            if ($scope.toDateSoap !== "") {
                var stringDateTo = $scope.toDateSoap.split('.');
                todatesoap = stringDateTo[1] + '.' + stringDateTo[0] + '.' + stringDateTo[2];
                $scope.toDateSoap = new Date(stringDateTo[2], stringDateTo[1] - 1, stringDateTo[0]);
            } else {
                todatesoap = $scope.toDateSoap;
            }
        } else if (typeof $scope.toDateSoap === "object") {
            if ($scope.toDateSoap) {
                var toD = $scope.toDateSoap;
                todatesoap = (toD.getMonth() + 1) + "." + toD.getDate() + "." + toD.getFullYear();
                $scope.toDateSoap = new Date(toD.getFullYear().toString(), toD.getMonth().toString(), toD.getDate().toString());
            }
        } else {
            todatesoap = $scope.toDateSoap;
        }

        $state.go($state.current.name, { pageIndex: parseInt(currentPage), itemsPerPage: $scope.pageSize,filterTransaction:$scope.filterTransaction,fromDateSoap:fromdatesoap,toDateSoap:todatesoap});
    };
    $scope.pageSizeChanged = function (itemsPerPage) {
        var todatesoap, fromdatesoap;

        if (typeof $scope.fromDateSoap === "string") {
            if ($scope.fromDateSoap !== "") {
                var stringDateFrom = $scope.fromDateSoap.split('.');
                fromdatesoap = stringDateFrom[1] + '.' + stringDateFrom[0] + '.' + stringDateFrom[2];
                $scope.fromDateSoap = new Date(stringDateFrom[2], stringDateFrom[1] - 1, stringDateFrom[0]);
            } else {
                fromdatesoap = $scope.fromDateSoap;
            }
        } else if (typeof $scope.fromDateSoap === "object") {
            if ($scope.fromDateSoap) {
                var fromD = $scope.fromDateSoap;
                fromdatesoap = (fromD.getMonth() + 1) + "." + fromD.getDate() + "." + fromD.getFullYear();
                $scope.fromDateSoap = new Date(fromD.getFullYear().toString(), fromD.getMonth().toString(), fromD.getDate().toString());
            }
        } else {
            fromdatesoap = $scope.fromDateSoap;
        }

        if (typeof $scope.toDateSoap === "string") {
            if ($scope.toDateSoap !== "") {
                var stringDateTo = $scope.toDateSoap.split('.');
                todatesoap = stringDateTo[1] + '.' + stringDateTo[0] + '.' + stringDateTo[2];
                $scope.toDateSoap = new Date(stringDateTo[2], stringDateTo[1] - 1, stringDateTo[0]);
            } else {
                todatesoap = $scope.toDateSoap;
            }
        } else if (typeof $scope.toDateSoap === "object") {
            if ($scope.toDateSoap) {
                var toD = $scope.toDateSoap;
                todatesoap = (toD.getMonth() + 1) + "." + toD.getDate() + "." + toD.getFullYear();
                $scope.toDateSoap = new Date(toD.getFullYear().toString(), toD.getMonth().toString(), toD.getDate().toString());
            }
        } else {
            todatesoap = $scope.toDateSoap;
        }
        $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: parseInt(itemsPerPage), filterTransaction: $scope.filterTransaction, fromDateSoap: fromdatesoap, toDateSoap: todatesoap });
    };
    $scope.selectedRow = null;
    $scope.setClickedRow = function (index) {
        $scope.selectedRow = index;
    };
    

    $scope.filterHandlerSoap = function ()
    {

        var todatesoap, fromdatesoap;

        if (typeof $scope.fromDateSoap === "string") {
            if ($scope.fromDateSoap !== "") {
                var stringDateFrom = $scope.fromDateSoap.split('.');
                fromdatesoap = stringDateFrom[1] + '.' + stringDateFrom[0] + '.' + stringDateFrom[2];
                $scope.fromDateSoap = new Date(stringDateFrom[2], stringDateFrom[1] - 1, stringDateFrom[0]);
            } else {
                fromdatesoap = $scope.fromDateSoap;
            }
        } else if (typeof $scope.fromDateSoap === "object") {
            if ($scope.fromDateSoap) {
                var fromD = $scope.fromDateSoap;
                fromdatesoap = (fromD.getMonth() + 1) + "." + fromD.getDate() + "." + fromD.getFullYear();
                $scope.fromDateSoap = new Date(fromD.getFullYear().toString(), fromD.getMonth().toString(), fromD.getDate().toString());
            }
        } else {
            fromdatesoap = $scope.fromDateSoap;
        }

        if (typeof $scope.toDateSoap === "string") {
            if ($scope.toDateSoap !== "") {
                var stringDateTo = $scope.toDateSoap.split('.');
                todatesoap = stringDateTo[1] + '.' + stringDateTo[0] + '.' + stringDateTo[2];
                $scope.toDateSoap = new Date(stringDateTo[2], stringDateTo[1] - 1, stringDateTo[0]);
            } else {
                todatesoap = $scope.toDateSoap;
            }
        } else if (typeof $scope.toDateSoap === "object") {
            if ($scope.toDateSoap) {
                var toD = $scope.toDateSoap;
                todatesoap = (toD.getMonth() + 1) + "." + toD.getDate() + "." + toD.getFullYear();
                $scope.toDateSoap = new Date(toD.getFullYear().toString(), toD.getMonth().toString(), toD.getDate().toString());
            }
        } else {
            todatesoap = $scope.toDateSoap;
        }

        $state.go($state.current.name, { pageIndex: parseInt(1), itemsPerPage: $scope.pageSize, filterTransaction: $scope.filterTransaction, fromDateSoap: fromdatesoap, toDateSoap: todatesoap });

    };



    $scope.filterClearSoap = function () {
        $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: $scope.pageSize, filterTransaction: null, fromDateSoap: "", toDateSoap: "" });
    };




    $scope.exportAction = function () {
        var fromd = "", tod = "";
        if (typeof $scope.fromDateSoap === 'object') {
            fromd = $scope.fromDateSoap;
        }
        else if (typeof $scope.fromDateSoap === 'string' && $scope.fromDateSoap.length) {
            var fdate = $scope.fromDateSoap.split('.');
            fromd = new Date(fdate[2], fdate[1] - 1, fdate[0]);
        }
        if (typeof $scope.toDateSoap === 'object') {
            tod = $scope.toDateSoap;
        }
        else if (typeof $scope.toDateSoap === 'string' && $scope.toDateSoap.length) {
            var tdate = $scope.toDateSoap.split('.');
            tod = new Date(tdate[2], tdate[1] - 1, tdate[0]);
        }

        soapfaultService.createPdf($scope.filterTransaction,fromd,tod).then(function (result) {
            if (result) {
                var blob = new Blob([result], { type: "application/pdf" });
                var name = "Greshki.pdf";
                navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                if (navigator.saveBlob) {
                    navigator.saveBlob(blob, name);
                } else {
                    saveBlob(blob, name);
                }
            }

        });

    };
    $scope.exportActionToExcel = function () {
        var fromd = "", tod = "";
        if (typeof $scope.fromDateSoap === 'object') {
            fromd = $scope.fromDateSoap;
        }
        else if (typeof $scope.fromDateSoap === 'string' && $scope.fromDateSoap.length) {
            var fdate = $scope.fromDateSoap.split('.');
            fromd = new Date(fdate[2], fdate[1] - 1, fdate[0]);
        }
        if (typeof $scope.toDateSoap === 'object') {
            tod = $scope.toDateSoap;
        }
        else if (typeof $scope.toDateSoap === 'string' && $scope.toDateSoap.length) {
            var tdate = $scope.toDateSoap.split('.');
            tod = new Date(tdate[2], tdate[1] - 1, tdate[0]);
        }
        soapfaultService.createExcel($scope.filterTransaction, fromd, tod).then(function (response) {

            var blob = new Blob([response], { type: "application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            var name = "GreshkiExcel.xlsx";
            navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
            if (navigator.saveBlob) {
                navigator.saveBlob(blob, name);
            } else {
                saveBlob(blob, name);
            }


        });
    };
    function saveBlob(blob, name) {
        var a = document.createElement('a');
        a.href = (window.URL || window.webkitURL).createObjectURL(blob);
        a.download = name;
        a.style.display = 'none';
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
    }

}]);