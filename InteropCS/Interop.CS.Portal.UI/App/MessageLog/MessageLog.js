
app.config(function ($stateProvider, appBaseUrl) {
    $stateProvider
        .state('messagelogs', {
            title: "Логови",
            controller: 'MessageLogCtrl',
            url: '/messageLogs/{pageIndex:int}/{itemsPerPage:int}/{filterConsumer:string}/{filterProvider:string}/{filterDir:string}/{filterService:string}/{filterMethod:string}/:fromDate/:toDate/{sortDir:string}/{sortCol:string}',
            templateUrl: appBaseUrl + 'Module/MessageLog',
            resolve: {
                pagedMessageLogs: ['messageLogService', '$stateParams', function (messageLogService, $stateParams) {
                    return messageLogService.getPagedMessageLogs($stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.filterConsumer, $stateParams.filterProvider,
                        $stateParams.filterDir, $stateParams.filterService, $stateParams.filterMethod, $stateParams.fromDate, $stateParams.toDate, $stateParams.sortDir, $stateParams.sortCol);
                }
                ]
            }
        })
        .state("messageLogDetails", {
            title: 'Детали за избраниот лог',
            url: "/messageLogDetails/:transactionId/:dir",
            templateUrl: 'App/MessageLog/Templates/MessageLogDetails',
            controller: 'MessageLogDetailsCtrl',
            resolve: {
                messageLog: ['messageLogService', '$stateParams', function (messageLogService, $stateParams) {
                    return messageLogService.getMessageLog($stateParams.transactionId, $stateParams.dir);
                }]
            }
        });
    ;
});

angular.module("MessageLog", ['ngTable', 'ui.bootstrap'])
 .service('messageLogService', ['$http', '$q', 'apiUri', 'handleResponseService',
        function ($http, $q, apiUri, handleResponseService) {
            return ({
                getPagedMessageLogs: getPagedMessageLogs,
                getMessageLog: getMessageLog,
                createPdf: createPdf,
                createExcel: createExcel
            });

            function getPagedMessageLogs(pageIndex, itemsPerPage, filterConsumer, filterProvider, filterDir, filterService, filterMethod, fromDate, toDate, sortDir, sortCol) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "MessageLog/GetMessageLogListPaged",
                    params: {
                        pageIndex: pageIndex,
                        itemsPerPage: itemsPerPage,
                        filterConsumer: filterConsumer,
                        filterProvider: filterProvider,
                        filterDir: filterDir,
                        filterService: filterService,
                        filterMethod: filterMethod,
                        fromDate: fromDate,
                        toDate: toDate,
                        sortDir: sortDir,
                        sortCol: sortCol
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }
            function createPdf(filterConsumer, filterProvider, filterDir, filterService, filterMethod, fromDate, toDate, sortDir, sortCol) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "MessageLog/CreatePdf",
                    responseType: "arraybuffer",

                    params: {
                        filterConsumer: filterConsumer,
                        filterProvider: filterProvider,
                        filterDir: filterDir,
                        filterService: filterService,
                        filterMethod: filterMethod,
                        fromDate: fromDate,
                        toDate: toDate,
                        sortDir: sortDir,
                        sortCol: sortCol
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
            }
            function createExcel(filterConsumer, filterProvider, filterDir, filterService, filterMethod, fromDate, toDate, sortDir, sortCol) {
                var request = $http({
                    method: "POST",
                    url: apiUri + "MessageLog/CreateExcel",
                    responseType: "arraybuffer",

                    params: {
                        filterConsumer: filterConsumer,
                        filterProvider: filterProvider,
                        filterDir: filterDir,
                        filterService: filterService,
                        filterMethod: filterMethod,
                        fromDate: fromDate,
                        toDate: toDate,
                        sortDir: sortDir,
                        sortCol: sortCol

                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
            }

            function getMessageLog(transactionId, dir) {
                var request = $http({
                    method: 'GET',
                    url: apiUri + 'MessageLog/GetMessageLogByTid',
                    params: {
                        transactionId: transactionId,
                        dir: dir
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }
        }])
    .service('sharedPropertyMessageLog', ['$http', function ($http) {
        var obj = [];
        return ({
            setMsgProp: setMsgProp,
            getMsgProp: getMsgProp
        });


        function setMsgProp(pageIndex, itemsPerPage, filterConsumer, filterProvider, filterDir, filterService, filterMethod, fromDate, toDate, sortDir, sortCol) {
            obj.pageIndex = pageIndex;
            obj.itemsPerPage = itemsPerPage;
            obj.filterConsumer = filterConsumer;
            obj.filterProvider = filterProvider;
            obj.filterDir = filterDir;
            obj.filterService = filterService;
            obj.filterMethod = filterMethod;
            obj.fromDate = fromDate;
            obj.toDate = toDate;
            obj.sortDir = sortDir;
            obj.sortCol = sortCol;
        }

        function getMsgProp() {
            return obj;
        }

    }])
    .controller("MessageLogCtrl", ['$scope', 'pagedMessageLogs', '$state', '$stateParams', 'sharedPropertyMessageLog', 'messageLogService',
        function ($scope, pagedMessageLogs, $state, $stateParams, sharedPropertyMessageLog, messageLogService) {

            var param = $stateParams;
            $scope.pageIndex = parseInt(param.pageIndex);
            $scope.totalSize = pagedMessageLogs.totalSize;
            $scope.messageLogs = pagedMessageLogs.items;
            $scope.itemsPerPage = pagedMessageLogs.pageSize;
            $scope.pageSize = pagedMessageLogs.pageSize;
            $scope.filterConsumer = param.filterConsumer;
            $scope.filterProvider = param.filterProvider;
            $scope.filterDir = param.filterDir;
            $scope.filterService = param.filterService;
            $scope.filterMethod = param.filterMethod;
            $scope.sortDir = $stateParams.sortDir;
            $scope.sortCol = $stateParams.sortCol;

            if (param.fromDate == "") {
                $scope.fromDate = param.fromDate;
            } else {
                var datefrom = param.fromDate.split(".");
                datefrom = datefrom[1] + '.' + datefrom[0] + '.' + datefrom[2];
                $scope.fromDate = datefrom;
            }
            if (param.toDate == "") {
                $scope.toDate = param.toDate;
            } else {
                var dateto = param.toDate.split(".");
                dateto = dateto[1] + '.' + dateto[0] + '.' + dateto[2];
                $scope.toDate = dateto;
            }

            $scope.status1 = {
                opened: false
            };

            $scope.status2 = {
                opened: false
            };
            
            $scope.dayBeforeToday = new Date();
            $scope.dayBeforeToday.setDate($scope.dayBeforeToday.getDate() - 1);

            $scope.open1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.status2.opened = false;
                $scope.status1.opened = true;
                if ($scope.toDate=="") {
                    $scope.maxFromDate = new Date($scope.dayBeforeToday.getFullYear(), $scope.dayBeforeToday.getMonth(), $scope.dayBeforeToday.getDate());
                    $scope.minFromDate = new Date(2016, 1, 1);

                } else if (typeof $scope.toDate === "string") {
                    var stringDateTo = $scope.toDate.split('.');
                    $scope.maxFromDate = new Date(stringDateTo[2], stringDateTo[1], stringDateTo[0] - 1);
                    $scope.minFromDate = new Date(2016, 1, 1);
                    
                } else if (typeof $scope.toDate === "object") {
                    $scope.maxFromDate = new Date($scope.toDate.getFullYear(), $scope.toDate.getMonth(), $scope.toDate.getDate() - 1);
                    $scope.minFromDate = new Date(2016, 1, 1);

                }
            };

            $scope.open2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.status1.opened = false;
                $scope.status2.opened = true;
                $scope.maxToDate = new Date();
                if (typeof $scope.fromDate === "string") {
                    var stringDateTo = $scope.toDate.split('.');
                    var stringDateFrom = $scope.fromDate.split('.');
                    if (stringDateFrom[0] === '19' && stringDateFrom[1] === '1' && stringDateFrom[2] === '2016') {
                        $scope.minToDate = new Date(stringDateTo[2], stringDateTo[1], stringDateTo[0] + 1);
                    } else {
                        $scope.minToDate = new Date(2016, 1, 20);
                    }

                    $scope.maxToDate = new Date();

                }
                if (typeof $scope.fromDate === "object") {
                    if ($scope.fromDate !== new Date(2016, 1, 19)) {
                        $scope.minToDate = new Date($scope.fromDate.getFullYear(), $scope.fromDate.getMonth(), $scope.fromDate.getDate() + 1);
                    } else {
                        $scope.minToDate = new Date(2016, 1, 20);
                    }

                    $scope.maxToDate = new Date();
                }
            };

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

                var todate, fromdate;

                if (typeof $scope.fromDate === "string") {
                    if ($scope.fromDate !== "") {
                        var stringDateFrom = $scope.fromDate.split('.');
                        //fromdate = $scope.fromDate;
                        fromdate = stringDateFrom[1] + '.' + stringDateFrom[0] + '.' + stringDateFrom[2];
                        $scope.fromDate = new Date(stringDateFrom[2], stringDateFrom[1] - 1, stringDateFrom[0]);
                    } else {
                        fromdate = $scope.fromDate;
                    }
                } else if (typeof $scope.fromDate === "object") {
                    if ($scope.fromDate) {
                        var fromD = $scope.fromDate;
                        fromdate = (fromD.getMonth() + 1) + "." + fromD.getDate() + "." + fromD.getFullYear();
                        $scope.fromDate = new Date(fromD.getFullYear().toString(), fromD.getMonth().toString(), fromD.getDate().toString());
                    }
                } else {
                    fromdate = $scope.fromDate;
                }

                if (typeof $scope.toDate === "string") {
                    if ($scope.toDate !== "") {
                        var stringDateTo = $scope.toDate.split('.');
                        //todate = $scope.toDate;
                        todate = stringDateTo[1] + '.' + stringDateTo[0] + '.' + stringDateTo[2];
                        $scope.toDate = new Date(stringDateTo[2], stringDateTo[1] - 1, stringDateTo[0]);
                    } else {
                        todate = $scope.toDate;
                    }
                } else if (typeof $scope.toDate === "object") {
                    if ($scope.toDate) {
                        var toD = $scope.toDate;
                        todate = (toD.getMonth() + 1) + "." + toD.getDate() + "." + toD.getFullYear();
                        $scope.toDate = new Date(toD.getFullYear().toString(), toD.getMonth().toString(), toD.getDate().toString());
                    }
                } else {
                    fromdate = $scope.toDate;
                }

                $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: $scope.pageSize, filterConsumer: $scope.filterConsumer, filterProvider: $scope.filterProvider, filterDir: $scope.filterDir, filterService: $scope.filterService, filterMethod: $scope.filterMethod, fromDate: fromdate, toDate: todate, sortDir: $scope.sortDir, sortCol: $scope.sortCol });

            };

            $scope.pageChangeHandler = function (currentPage) {

                var todate, fromdate;

                if (typeof $scope.fromDate === "string") {
                    if ($scope.fromDate !== "") {
                        var stringDateFrom = $scope.fromDate.split('.');
                        //fromdate = $scope.fromDate;
                        fromdate = stringDateFrom[1] + '.' + stringDateFrom[0] + '.' + stringDateFrom[2];
                        $scope.fromDate = new Date(stringDateFrom[2], stringDateFrom[1] - 1, stringDateFrom[0]);
                    } else {
                        fromdate = $scope.fromDate;
                    }
                } else if (typeof $scope.fromDate === "object") {
                    if ($scope.fromDate) {
                        var fromD = $scope.fromDate;
                        fromdate = (fromD.getMonth() + 1) + "." + fromD.getDate() + "." + fromD.getFullYear();
                        $scope.fromDate = new Date(fromD.getFullYear().toString(), fromD.getMonth().toString(), fromD.getDate().toString());
                    }
                } else {
                    fromdate = $scope.fromDate;
                }

                if (typeof $scope.toDate === "string") {
                    if ($scope.toDate !== "") {
                        var stringDateTo = $scope.toDate.split('.');
                        //todate = $scope.toDate;
                        todate = stringDateTo[1] + '.' + stringDateTo[0] + '.' + stringDateTo[2];
                        $scope.toDate = new Date(stringDateTo[2], stringDateTo[1] - 1, stringDateTo[0]);
                    } else {
                        todate = $scope.toDate;
                    }
                } else if (typeof $scope.toDate === "object") {
                    if ($scope.toDate) {
                        var toD = $scope.toDate;
                        todate = (toD.getMonth() + 1) + "." + toD.getDate() + "." + toD.getFullYear();
                        $scope.toDate = new Date(toD.getFullYear().toString(), toD.getMonth().toString(), toD.getDate().toString());
                    }
                } else {
                    fromdate = $scope.toDate;
                }

                $state.go($state.current.name, { pageIndex: parseInt(currentPage), itemsPerPage: $scope.pageSize, filterConsumer: $scope.filterConsumer, filterProvider: $scope.filterProvider, filterDir: $scope.filterDir, filterService: $scope.filterService, filterMethod: $scope.filterMethod, fromDate: fromdate, toDate: todate, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
            };

            $scope.pageSizeChanged = function (itemsPerPage) {

                var todate, fromdate;

                if (typeof $scope.fromDate === "string") {
                    if ($scope.fromDate !== "") {
                        var stringDateFrom = $scope.fromDate.split('.');
                        //fromdate = $scope.fromDate;
                        fromdate = stringDateFrom[1] + '.' + stringDateFrom[0] + '.' + stringDateFrom[2];
                        $scope.fromDate = new Date(stringDateFrom[2], stringDateFrom[1] - 1, stringDateFrom[0]);
                    } else {
                        fromdate = $scope.fromDate;
                    }
                } else if (typeof $scope.fromDate === "object") {
                    if ($scope.fromDate) {
                        var fromD = $scope.fromDate;
                        fromdate = (fromD.getMonth() + 1) + "." + fromD.getDate() + "." + fromD.getFullYear();
                        $scope.fromDate = new Date(fromD.getFullYear().toString(), fromD.getMonth().toString(), fromD.getDate().toString());
                    }
                } else {
                    fromdate = $scope.fromDate;
                }

                if (typeof $scope.toDate === "string") {
                    if ($scope.toDate !== "") {
                        var stringDateTo = $scope.toDate.split('.');
                        //todate = $scope.toDate;
                        todate = stringDateTo[1] + '.' + stringDateTo[0] + '.' + stringDateTo[2];
                        $scope.toDate = new Date(stringDateTo[2], stringDateTo[1] - 1, stringDateTo[0]);
                    } else {
                        todate = $scope.toDate;
                    }
                } else if (typeof $scope.toDate === "object") {
                    if ($scope.toDate) {
                        var toD = $scope.toDate;
                        todate = (toD.getMonth() + 1) + "." + toD.getDate() + "." + toD.getFullYear();
                        $scope.toDate = new Date(toD.getFullYear().toString(), toD.getMonth().toString(), toD.getDate().toString());
                    }
                } else {
                    fromdate = $scope.toDate;
                }

                $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: parseInt(itemsPerPage), filterConsumer: $scope.filterConsumer, filterProvider: $scope.filterProvider, filterDir: $scope.filterDir, filterService: $scope.filterService, filterMethod: $scope.filterMethod, fromDate: fromdate, toDate: todate, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
            };

            $scope.filterHandler = function () {

                var todate, fromdate;

                if (typeof $scope.fromDate === "string") {
                    if ($scope.fromDate !== "") {
                        var stringDateFrom = $scope.fromDate.split('.');
                        //fromdate = $scope.fromDate;
                        fromdate = stringDateFrom[1] + '.' + stringDateFrom[0] + '.' + stringDateFrom[2];
                        $scope.fromDate = new Date(stringDateFrom[2], stringDateFrom[1] - 1, stringDateFrom[0]);
                    } else {
                        fromdate = $scope.fromDate;
                    }
                } else if (typeof $scope.fromDate === "object") {
                    if ($scope.fromDate) {
                        var fromD = $scope.fromDate;
                        fromdate = (fromD.getMonth() + 1) + "." + fromD.getDate() + "." + fromD.getFullYear();
                        $scope.fromDate = new Date(fromD.getFullYear().toString(), fromD.getMonth().toString(), fromD.getDate().toString());
                    }
                } else {
                    fromdate = $scope.fromDate;
                }

                if (typeof $scope.toDate === "string") {
                    if ($scope.toDate !== "") {
                        var stringDateTo = $scope.toDate.split('.');
                        //todate = $scope.toDate;
                        todate = stringDateTo[1] + '.' + stringDateTo[0] + '.' + stringDateTo[2];
                        $scope.toDate = new Date(stringDateTo[2], stringDateTo[1] - 1, stringDateTo[0]);
                    } else {
                        todate = $scope.toDate;
                    }
                } else if (typeof $scope.toDate === "object") {
                    if ($scope.toDate) {
                        var toD = $scope.toDate;
                        todate = (toD.getMonth() + 1) + "." + toD.getDate() + "." + toD.getFullYear();
                        $scope.toDate = new Date(toD.getFullYear().toString(), toD.getMonth().toString(), toD.getDate().toString());
                    }
                } else {
                    fromdate = $scope.toDate;
                }

                $state.go($state.current.name, { pageIndex: parseInt(1), itemsPerPage: $scope.pageSize, filterConsumer: $scope.filterConsumer, filterProvider: $scope.filterProvider, filterDir: $scope.filterDir, filterService: $scope.filterService, filterMethod: $scope.filterMethod, fromDate: fromdate, toDate: todate, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
            };

            $scope.filterClear = function () {
                $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: $scope.pageSize, filterConsumer: "", filterProvider: "", filterDir: "", filterService: "", filterMethod: "", fromDate: "", toDate: "" });
            };

            $scope.exportAction = function () {
                var fromd = "", tod = "";
                if (typeof $scope.fromDate === 'object') {
                    fromd = $scope.fromDate;
                }
                else if (typeof $scope.fromDate === 'string' && $scope.fromDate.length) {
                    var fdate = $scope.fromDate.split('.');
                    fromd = new Date(fdate[2], fdate[1] - 1, fdate[0]);
                }
                if (typeof $scope.toDate === 'object') {
                    tod = $scope.toDate;
                }
                else if (typeof $scope.toDate === 'string' && $scope.toDate.length) {
                    var tdate = $scope.toDate.split('.');
                    tod = new Date(tdate[2], tdate[1] - 1, tdate[0]);
                }
                messageLogService.createPdf($scope.filterConsumer, $scope.filterProvider, $scope.filterDir, $scope.filterService, $scope.filterMethod, fromd, tod, $scope.sortDir, $scope.sortCol).then(function (result) {
                    if (result) {

                        var blob = new Blob([result], { type: "application/pdf" });
                        var name = "MessageLogPDF.pdf";
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
                if (typeof $scope.fromDate === 'object') {
                    fromd = $scope.fromDate;
                }
                else if (typeof $scope.fromDate === 'string' && $scope.fromDate.length) {
                    var fdate = $scope.fromDate.split('.');
                    fromd = new Date(fdate[2], fdate[1] - 1, fdate[0]);
                }
                if (typeof $scope.toDate === 'object') {
                    tod = $scope.toDate;
                }
                else if (typeof $scope.toDate === 'string' && $scope.toDate.length) {
                    var tdate = $scope.toDate.split('.');
                    tod = new Date(tdate[2], tdate[1] - 1, tdate[0]);
                }
                messageLogService.createExcel($scope.filterConsumer, $scope.filterProvider, $scope.filterDir, $scope.filterService, $scope.filterMethod, fromd, tod, $scope.sortDir, $scope.sortCol).then(function (response) {

                    var blob = new Blob([response], { type: "application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                    var name = "MessageLogExcel.xlsx";
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

            $scope.messageLogDetails = function (transactionId, dir, pageIndex, itemsPerPage, filterConsumer, filterProvider, filterDir, filterService, filterMethod) {

                var todate, fromdate;

                if (typeof $scope.fromDate === "string") {
                    if ($scope.fromDate !== "") {
                        var stringDateFrom = $scope.fromDate.split('.');
                        //fromdate = $scope.fromDate;
                        fromdate = stringDateFrom[1] + '.' + stringDateFrom[0] + '.' + stringDateFrom[2];
                        $scope.fromDate = new Date(stringDateFrom[2], stringDateFrom[1] - 1, stringDateFrom[0]);
                    } else {
                        fromdate = $scope.fromDate;
                    }
                } else if (typeof $scope.fromDate === "object") {
                    if ($scope.fromDate) {
                        var fromD = $scope.fromDate;
                        fromdate = (fromD.getMonth() + 1) + "." + fromD.getDate() + "." + fromD.getFullYear();
                        $scope.fromDate = new Date(fromD.getFullYear().toString(), fromD.getMonth().toString(), fromD.getDate().toString());
                    }
                } else {
                    fromdate = $scope.fromDate;
                }

                if (typeof $scope.toDate === "string") {
                    if ($scope.toDate !== "") {
                        var stringDateTo = $scope.toDate.split('.');
                        //todate = $scope.toDate;
                        todate = stringDateTo[1] + '.' + stringDateTo[0] + '.' + stringDateTo[2];
                        $scope.toDate = new Date(stringDateTo[2], stringDateTo[1] - 1, stringDateTo[0]);
                    } else {
                        todate = $scope.toDate;
                    }
                } else if (typeof $scope.toDate === "object") {
                    if ($scope.toDate) {
                        var toD = $scope.toDate;
                        todate = (toD.getMonth() + 1) + "." + toD.getDate() + "." + toD.getFullYear();
                        $scope.toDate = new Date(toD.getFullYear().toString(), toD.getMonth().toString(), toD.getDate().toString());
                    }
                } else {
                    fromdate = $scope.toDate;
                }

                sharedPropertyMessageLog.setMsgProp(pageIndex, itemsPerPage, filterConsumer, filterProvider, filterDir, filterService, filterMethod, fromdate, todate, $scope.sortDir, $scope.sortCol);
                $state.go("messageLogDetails", { transactionId: transactionId, dir: dir });
            };

            $scope.selectedRow = null;
            $scope.setClickedRow = function (index) {
                $scope.selectedRow = index;
            };

        }])
    .controller('MessageLogDetailsCtrl', ['$scope', 'messageLog', 'sharedPropertyMessageLog', '$state', function ($scope, messageLog, sharedPropertyMessageLog, $state) {
        $scope.message = messageLog;

        $scope.handleBackBtnMsgLogDetails = function () {
            var obj = sharedPropertyMessageLog.getMsgProp();
            $state.go('messagelogs', { pageIndex: obj.pageIndex, itemsPerPage: obj.itemsPerPage, filterConsumer: obj.filterConsumer, filterProvider: obj.filterProvider, filterDir: obj.filterDir, filterService: obj.filterService, filterMethod: obj.filterMethod, fromDate: obj.fromDate, toDate: obj.toDate, sortDir: obj.sortDir, sortCol: obj.sortCol });
        };
    }]);



