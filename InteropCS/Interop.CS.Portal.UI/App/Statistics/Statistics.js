
app.config(function ($stateProvider, appBaseUrl) {

    $stateProvider
        .state("statistics", {
            title: 'Статистика',
            controller: "StatisticsCtrl",
            url: "/statistics/{pageIndex:int}/{itemsPerPage:int}/:selectedConsumer/:selectedProvider/:selectedService/:fromDate/:toDate/{successfullyCheck:bool}/{unsuccessfullyCheck:bool}/{sortDir:string}/{sortCol:string}",
            templateUrl: appBaseUrl + "Module/Statistics"
        })
        .state("messageLogStatisticDetails", {
            title: 'Детали за избраната трансакција',
            url: "/messageLogStatisticDetails/:transactionId/:consumer/:routingToken",
            templateUrl: 'App/Statistics/Templates/MessageLogStatisticDetails',
            controller: 'MessageLogStatisticDetailsCtrl',
            resolve: {
                messageLogStatistic: ['statisticsService', '$stateParams', function (statisticsService, $stateParams) {
                    return statisticsService.getMessageLogStatistic($stateParams.transactionId, $stateParams.consumer, $stateParams.routingToken);
                }]
            }
        });
});

angular.module("Statistics", ['ui.bootstrap'])

    .service("statisticsService", ['$http', 'handleResponseService', 'apiUri',
        function ($http, handleResponseService, apiUri) {
            return ({
                getParticipants: getParticipants,
                getProviders: getProviders,
                getMessageLogs: getMessageLogs,
                getMessageLogStatistic: getMessageLogStatistic,
                createPdf: createPdf,
                createExcel: createExcel,
                createWord: createWord,
                getMessageLogCheckTimeStamp: getMessageLogCheckTimeStamp,
                getServices: getServices,
                getConsumersForProvider: getConsumersForProvider
            });


            function getMessageLogCheckTimeStamp(tokenTimestamp) {
                var request = $http({
                    method: 'POST',
                    url: apiUri + "Statistics/GetMessageLogCheckTimeStamp",
                    data: JSON.stringify({ TokenTimestamp: tokenTimestamp })
                    //headers: {
                    //    "Content-Type": "application/json"
                    //}
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
            }

            function getParticipants() {
                var request = $http({
                    method: 'GET',
                    url: apiUri + "Helper/GetParticipants"
                });
                return (request.then(handleResponseService.handleSuccess));
            }

            function getProviders(consumer) {
                var request = $http({
                    method: 'GET',
                    url: apiUri + "Helper/GetProviders",
                    params: {
                        consumer: consumer
                    }
                });
                return (request.then(handleResponseService.handleSuccess));
            }

            function getServices(consumer, provider) {
                var request = $http({
                    method: 'GET',
                    //url: apiUri + "Statistics/GetServices",
                    url: apiUri + "Statistics/FillServices",
                    params: {
                        consumer: consumer,
                        provider: provider
                    }
                });
                return (request.then(handleResponseService.handleSuccess));
            }

            function getConsumersForProvider(provider) {
                var request = $http({
                    method: 'GET',
                    url: apiUri + "Helper/GetConsumerByProvider",
                    params: {
                        provider: provider
                    }
                });
                return (request.then(handleResponseService.handleSuccess));
            }

            function createPdf(consumer, provider, successfully, unsuccessfully, fromDate, toDate) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "Statistics/CreatePdf",
                    responseType: "arraybuffer",

                    params: {
                        consumer: consumer,
                        provider: provider,
                        successfully: successfully,
                        unsuccessfully: unsuccessfully,
                        fromDate: fromDate,
                        toDate: toDate

                    }
                });
                return (request.then(handleResponseService.handleSuccess));
            }
            function createExcel(consumer, provider, successfully, unsuccessfully, fromDate, toDate, service) {
                var request = $http({
                    method: "POST",
                    url: apiUri + "Statistics/CreateExcel",
                    responseType: "arraybuffer",
                    params: {
                        consumer: consumer,
                        provider: provider,
                        successfully: successfully,
                        unsuccessfully: unsuccessfully,
                        fromDate: fromDate,
                        toDate: toDate,
                        service: service
                    }
                });
                return (request.then(handleResponseService.handleSuccess));
            }
            function createWord(consumer, provider, successfully, unsuccessfully, fromDate, toDate, service) {
                var request = $http({
                    method: "POST",
                    url: apiUri + "Statistics/CreateWord",
                    responseType: "arraybuffer",

                    params: {
                        consumer: consumer,
                        provider: provider,
                        successfully: successfully,
                        unsuccessfully: unsuccessfully,
                        fromDate: fromDate,
                        toDate: toDate,
                        service: service

                    }
                });
                return (request.then(handleResponseService.handleSuccess));
            }

            function getMessageLogs(consumer, provider, successfully, unsuccessfully, fromDate, toDate, service, pageIndex, itemsPerPage, sortDir, sortCol) {
                if (consumer == undefined) {
                    consumer = "";
                }
                if (provider == undefined) {
                    provider = "";
                }
                if (service == undefined) {
                    service = "";
                }
                var request = $http({
                    method: 'GET',
                    url: apiUri + "Statistics/GetMessageLogs",
                    params: {
                        consumer: consumer,
                        provider: provider,
                        successfully: successfully,
                        unsuccessfully: unsuccessfully,
                        fromDate: fromDate,
                        toDate: toDate,
                        service: service,
                        pageIndex: pageIndex,
                        itemsPerPage: itemsPerPage,
                        sortDir: sortDir,
                        sortCol: sortCol
                    }
                });
                return (request.then(handleResponseService.handleSuccess));
            }

            function getMessageLogStatistic(transactionId, consumer, routingToken) {
                var request = $http({
                    method: 'GET',
                    url: apiUri + 'Statistics/GetMessageLogStatisticsByTransactionId',
                    params: {
                        transactionId: transactionId,
                        consumer: consumer,
                        routingToken: routingToken
                    }
                });
                return (request.then(handleResponseService.handleSuccess));
            }

        }])

    .service('sharedPropertyMessageLogStatistic', ['$http', function ($http) {
        var obj = [];
        return ({
            setMsgProp: setMsgProp,
            getMsgProp: getMsgProp
        });
        function setMsgProp(pageIndex, itemsPerPage, selectedConsumer, selectedProvider, selectedService, successfullyCheck, unsuccessfullyCheck, fromDate, toDate, sortDir, sortCol) {
            obj.pageIndex = pageIndex;
            obj.itemsPerPage = itemsPerPage;
            obj.selectedConsumer = selectedConsumer;
            obj.selectedProvider = selectedProvider;
            obj.selectedService = selectedService;
            obj.successfullyCheck = successfullyCheck;
            obj.unsuccessfullyCheck = unsuccessfullyCheck;
            obj.fromDate = fromDate;
            obj.toDate = toDate;
            obj.sortDir = sortDir;
            obj.sortCol = sortCol;
        }

        function getMsgProp() {
            return obj;
        }

    }])

    .controller("StatisticsCtrl", ['$scope', 'statisticsService', '$stateParams', '$state', 'sharedPropertyMessageLogStatistic',
        function ($scope, statisticsService, $stateParams, $state, sharedPropertyMessageLogStatistic) {

            $scope.selectedRow = null;

            $scope.setSelectedRow = function (index) {
                $scope.selectedRow = index;
            };

            var param = $stateParams;


            $scope.selectedConsumer = param.selectedConsumer;
            $scope.selectedProvider = param.selectedProvider;
            $scope.selectedService = param.selectedService;
            $scope.successfullyCheck = param.successfullyCheck;
            $scope.unsuccessfullyCheck = param.unsuccessfullyCheck;
            $scope.fromDate = param.fromDate;
            $scope.toDate = param.toDate;
            $scope.sortDir = $stateParams.sortDir;
            $scope.sortCol = $stateParams.sortCol;


            //$scope.show = false;
            if ($scope.successfullyCheck == undefined) {
                $scope.successfullyCheck = false;
            }
            if ($scope.unsuccessfullyCheck == undefined) {
                $scope.unsuccessfullyCheck = false;
            }

            if ($scope.unsuccessfullyCheck == undefined) {
                $scope.visibleAfterSearch = false;
            }


            $scope.dayBeforeToday = new Date();
            $scope.dayBeforeToday.setDate($scope.dayBeforeToday.getDate());

            if ($scope.toDate === "") {
                $scope.toDate = new Date($scope.dayBeforeToday.getFullYear(), $scope.dayBeforeToday.getMonth(), $scope.dayBeforeToday.getDate());
                $scope.maxToDate = new Date($scope.dayBeforeToday.getFullYear(), $scope.dayBeforeToday.getMonth(), $scope.dayBeforeToday.getDate());
            }
            if ($scope.fromDate === "") {
                
                //Picker-ot za Od da bide setiran eden mesec porano od momentalniot datum
                var todayDate = new Date();
                if (todayDate.getMonth() == 0)/*znaci sme Januari mesec*/ {
                    $scope.fromDate = new Date(todayDate.getFullYear() - 1, todayDate.getMonth() - 1, todayDate.getDate() - 1);
                } else {
                    $scope.fromDate = new Date(todayDate.getFullYear(), todayDate.getMonth() - 1, todayDate.getDate() - 1);
                }

                //Prethodno bese vaka
                //var dateOneMonthEarlier = new Date();
                //if (dateOneMonthEarlier.getMonth() - 1 === 0) {
                //    $scope.fromDate = new Date(2016, 1, 19);
                //} else {
                //    //ako sme 2 mesec, setiraj da e 19.02.2016 default vrednost na OD pickerot
                //    if (dateOneMonthEarlier.getMonth() - 1 == 1) {
                //        $scope.fromDate = new Date(dateOneMonthEarlier.getFullYear(), dateOneMonthEarlier.getMonth() - 1, 19);
                //    } else {
                //        $scope.fromDate = new Date(dateOneMonthEarlier.getFullYear(), dateOneMonthEarlier.getMonth() - 1, dateOneMonthEarlier.getDate() - 1);
                //    }
                //}
                //$scope.minFromDate = new Date(2016, 1, 19);
            }

            $scope.statusFrom = {
                opened: false
            };
            $scope.statusTo = {
                opened: false
            };

            $scope.monthlySearch = {
                disabled: false
            };
            $scope.fromDateSearch = {
                disabled: false
            };
            $scope.toDateSearch = {
                disabled: false
            };
            $scope.yearlySearch = {
                disabled: false
            };



            $scope.openFrom = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.statusFrom.opened = true;
                if ($scope.toDate != undefined && $scope.toDate != "") {

                    $scope.maxFromDate = new Date($scope.toDate.getFullYear(), $scope.toDate.getMonth(), $scope.toDate.getDate()); // - 1 bese
                    $scope.minFromDate = new Date(2016, 1, 19);

                }
            };

            $scope.openTo = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.statusTo.opened = true;
                if ($scope.fromDate != undefined && $scope.fromDate != "") {

                    $scope.minToDate = new Date($scope.fromDate.getFullYear(), $scope.fromDate.getMonth(), $scope.fromDate.getDate());// + 1 bese
                    $scope.maxToDate = new Date($scope.dayBeforeToday.getFullYear(), $scope.dayBeforeToday.getMonth(), $scope.dayBeforeToday.getDate());

                }
            };

            $scope.checkMessageLogStatus = function (param) {
                if (param === 0) {
                    $scope.unsuccessfullyCheck = false;
                }
                if (param === 1) {
                    $scope.successfullyCheck = false;
                }
            };

            statisticsService.getParticipants()
                .then(function (response) {
                    $scope.consumerList = response;
                    //Nadminuvanje na problem, posle klik na 'Baraj' se gubi setot na podatoci po dropdown-i 
                    if ($scope.selectedConsumer != "") {
                        statisticsService.getProviders($scope.selectedConsumer).then(function (responseProviders) {
                            $scope.providerList = responseProviders;
                        });
                    } else {
                        $scope.providerList = response;
                    }
                    //$scope.providerList = response;
                });

            statisticsService.getServices().then(function (servicesResponse) {
                //Nadminuvanje na problem, posle klik na 'Baraj' se gubi setot na podatoci po dropdown-i 
                if ($scope.selectedService != "") {
                    if ($scope.selectedProvider != "" || $scope.selectedProvider != undefined) {
                        statisticsService.getServices($scope.selectedConsumer, $scope.selectedProvider).then(function (responseServices) {
                            $scope.servicesList = responseServices;
                        });
                    }
                } else {
                    //$scope.servicesList = servicesResponse;
                    statisticsService.getServices($scope.selectedConsumer, $scope.selectedProvider).then(function (responseServices) {
                        $scope.servicesList = responseServices;
                    });
                }
            });

            $scope.changedConsumer = function (consumerDropdown) {
                if (consumerDropdown != null) {

                    //isprazni provajderi
                    //rebind na provajderi
                    //isprazni services i selected service

                    if ($scope.selectedProvider == "" || $scope.selectedProvider == undefined) {
                        statisticsService.getProviders($scope.selectedConsumer).then(function (response) {
                            $scope.providerList = response;
                            $scope.selectedProvider = undefined;
                            //dodadeno
                            $scope.servicesList = null
                            $scope.selectedService = undefined;
                        });
                    } else {

                        statisticsService.getProviders($scope.selectedConsumer).then(function (response) {
                            $scope.providerList = response;
                            $scope.selectedProvider = undefined;
                            $scope.servicesList = null;
                            $scope.selectedService = undefined;
                        });
                    }
                    
                    //statisticsService.getServices($scope.selectedConsumer, $scope.selectedProvider).then(function (response) {
                    //    $scope.servicesList = response;
                    //    $scope.selectedService = undefined;
                    //});
                }
            };



            $scope.disableControls = function () {
                if ($scope.selectedYear != undefined) {
                    $scope.fromDateSearch.disabled = true;
                    $scope.toDateSearch.disabled = true;
                }
                if ($scope.selectedMonth != undefined) {
                    $scope.fromDateSearch.disabled = true;
                    $scope.toDateSearch.disabled = true;
                }
            };

            $scope.changedProvider = function (providerDropdown) {
                if (providerDropdown != null) {

                    //rebind na services ddl
                    statisticsService.getServices($scope.selectedConsumer, $scope.selectedProvider).then(function (response) {
                        $scope.servicesList = response;
                        $scope.selectedService = undefined;
                    });

                    //rebing na consumer ddl
                    statisticsService.getConsumersForProvider($scope.selectedProvider).then(function (consumersResponse) {
                        $scope.consumerList = consumersResponse;
                        $scope.successfullyCheck = false;
                        $scope.unsuccessfullyCheck = false;
                    });

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

                var tDate, fDate;

                if (typeof $scope.fromDate === "string") {
                    if ($scope.fromDate !== "") {
                        var stringDateFrom = $scope.fromDate.split('.');
                        fDate = $scope.fromDate;
                        $scope.fromDate = new Date(stringDateFrom[2], stringDateFrom[0] - 1, stringDateFrom[1]);
                    } else {
                        fDate = $scope.fromDate;
                    }
                } else if (typeof $scope.fromDate === "object") {
                    if ($scope.fromDate) {
                        var fromD = $scope.fromDate;
                        fDate = fromD.getMonth() + 1 + "." + fromD.getDate() + "." + fromD.getFullYear();
                        $scope.fromDate = new Date(fromD.getFullYear().toString(), fromD.getMonth().toString(), fromD.getDate().toString());
                    }
                } else {
                    fDate = $scope.fromDate;
                }

                if (typeof $scope.toDate === "string") {
                    if ($scope.toDate !== "") {
                        var stringDateTo = $scope.toDate.split('.');
                        tDate = $scope.toDate;
                        $scope.toDate = new Date(stringDateTo[2], stringDateTo[0] - 1, stringDateTo[1]);
                    } else {
                        tDate = $scope.toDate;
                    }
                } else if (typeof $scope.toDate === "object") {
                    if ($scope.toDate) {
                        var toD = $scope.toDate;
                        tDate = toD.getMonth() + 1 + "." + toD.getDate() + "." + toD.getFullYear();
                        $scope.toDate = new Date(toD.getFullYear().toString(), toD.getMonth().toString(), toD.getDate().toString());
                    }
                } else {
                    tDate = $scope.toDate;
                }

                $state.go($state.current.name, { pageIndex: parseInt($stateParams.pageIndex), itemsPerPage: $scope.pageSize, selectedConsumer: $scope.selectedConsumer, selectedProvider: $scope.selectedProvider, selectedService: $scope.selectedService, fromDate: fDate, toDate: tDate, successfullyCheck: $scope.successfullyCheck, unsuccessfullyCheck: $scope.unsuccessfullyCheck, sortDir: $scope.sortDir, sortCol: $scope.sortCol });

            };

            $scope.exportActionToExcel = function (consumer, provider, successfully, unsuccessfully, fromDate, toDate, service) {
                consumer = (consumer === undefined) ? "" : consumer;
                provider = (provider === undefined) ? "" : provider;
                fromDate = (fromDate === undefined) ? "" : fromDate;
                toDate = (toDate === undefined) ? "" : toDate;
                service = (service === undefined) ? "" : service;
                statisticsService.createExcel(consumer, provider, successfully, unsuccessfully, fromDate, toDate, service).then(function (response) {
                    var blob = new Blob([response], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                    var name = "Statistics.xlsx";
                    navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                    if (navigator.saveBlob) {
                        navigator.saveBlob(blob, name);
                    } else {
                        saveBlob(blob, name);
                    }
                });
            };
            $scope.exportActionToWord = function (consumer, provider, successfully, unsuccessfully, fromDate, toDate, service) {
                consumer = (consumer === undefined) ? "" : consumer;
                provider = (provider === undefined) ? "" : provider;
                service = (service === undefined) ? "" : service;
                fromDate = (fromDate === undefined) ? "" : fromDate;
                toDate = (toDate === undefined) ? "" : toDate;
                statisticsService.createWord(consumer, provider, successfully, unsuccessfully, fromDate, toDate, service).then(function (response) {
                    var blob = new Blob([response], { type: "application/msword" });
                    var name = "MessageLogWord.doc";
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

            $scope.exportAction = function (consumer, provider, successfully, unsuccessfully, fromDate, toDate) {
                consumer = (consumer === undefined) ? "" : consumer;
                provider = (provider === undefined) ? "" : provider;
                fromDate = (fromDate === undefined) ? "" : fromDate;
                toDate = (toDate === undefined) ? "" : toDate;
                statisticsService.createPdf(consumer, provider, successfully, unsuccessfully, fromDate, toDate).then(function (result) {
                    if (result) {
                        var blob = new Blob([result], { type: "application/pdf" });
                        var name = "StatisticsPdf.pdf";
                        navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                        if (navigator.saveBlob) {
                            navigator.saveBlob(blob, name);
                        } else {
                            saveBlob(blob, name);
                        }
                    }
                });
            };


            var tDate, fDate;

            if (typeof $scope.fromDate === "string") {
                if ($scope.fromDate !== "") {
                    var stringDateFrom = $scope.fromDate.split('.');
                    fDate = $scope.fromDate;
                    $scope.fromDate = new Date(stringDateFrom[2], stringDateFrom[0] - 1, stringDateFrom[1]);
                } else {
                    fDate = $scope.fromDate;
                }
            } else if (typeof $scope.fromDate === "object") {
                if ($scope.fromDate) {
                    var fromD = $scope.fromDate;
                    fDate = fromD.getMonth() + 1 + "." + fromD.getDate() + "." + fromD.getFullYear();
                    $scope.fromDate = new Date(fromD.getFullYear().toString(), fromD.getMonth().toString(), fromD.getDate().toString());
                }
            } else {
                fDate = $scope.fromDate;
            }

            if (typeof $scope.toDate === "string") {
                if ($scope.toDate !== "") {
                    var stringDateTo = $scope.toDate.split('.');
                    tDate = $scope.toDate;
                    $scope.toDate = new Date(stringDateTo[2], stringDateTo[0] - 1, stringDateTo[1]);
                } else {
                    tDate = $scope.toDate;
                }
            } else if (typeof $scope.toDate === "object") {
                if ($scope.toDate) {
                    var toD = $scope.toDate;
                    tDate = toD.getMonth() + 1 + "." + toD.getDate() + "." + toD.getFullYear();
                    $scope.toDate = new Date(toD.getFullYear().toString(), toD.getMonth().toString(), toD.getDate().toString());
                }
            } else {
                tDate = $scope.toDate;
            }

            if ($scope.selectedConsumer == undefined) {
                $scope.selectedConsumer = "";
            }
            if ($scope.selectedProvider == undefined) {
                $scope.selectedProvider = "";
            }
            if ($scope.selectedService == undefined) {
                $scope.selectedService = "";
            }
            if ($scope.fromDate === undefined) {
                $scope.fromDate = "";
            }
            if ($scope.toDate === undefined) {
                $scope.toDate = "";
            }


            statisticsService.getMessageLogs($scope.selectedConsumer, $scope.selectedProvider, $scope.successfullyCheck, $scope.unsuccessfullyCheck, fDate, tDate, $scope.selectedService, $stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.sortDir, $stateParams.sortCol).then(function (response) {
                $scope.messageList = response.items;
                $scope.totalItems = response.totalSize;
                $scope.itemsPerPage = response.pageSize;
                $scope.pageSize = response.pageSize;
                $scope.currentPage = parseInt($stateParams.pageIndex);
                $scope.pageIndex = parseInt($stateParams.pageIndex);
                $scope.visibleAfterSearch = true;
            });

            $scope.statisticsFilter = function () {

                var tDate, fDate;

                if (typeof $scope.fromDate === "string") {
                    if ($scope.fromDate !== "") {
                        var stringDateFrom = $scope.fromDate.split('.');
                        fDate = $scope.fromDate;
                        $scope.fromDate = new Date(stringDateFrom[2], stringDateFrom[0] - 1, stringDateFrom[1]);
                    } else {
                        fDate = $scope.fromDate;
                    }
                } else if (typeof $scope.fromDate === "object") {
                    if ($scope.fromDate) {
                        var fromD = $scope.fromDate;
                        fDate = fromD.getMonth() + 1 + "." + fromD.getDate() + "." + fromD.getFullYear();
                        $scope.fromDate = new Date(fromD.getFullYear().toString(), fromD.getMonth().toString(), fromD.getDate().toString());
                    }
                } else {
                    fDate = $scope.fromDate;
                }

                if (typeof $scope.toDate === "string") {
                    if ($scope.toDate !== "") {
                        var stringDateTo = $scope.toDate.split('.');
                        tDate = $scope.toDate;
                        $scope.toDate = new Date(stringDateTo[2], stringDateTo[0] - 1, stringDateTo[1]);
                    } else {
                        tDate = $scope.toDate;
                    }
                } else if (typeof $scope.toDate === "object") {
                    if ($scope.toDate) {
                        var toD = $scope.toDate;
                        tDate = toD.getMonth() + 1 + "." + toD.getDate() + "." + toD.getFullYear();
                        $scope.toDate = new Date(toD.getFullYear().toString(), toD.getMonth().toString(), toD.getDate().toString());
                    }
                } else {
                    tDate = $scope.toDate;
                }

                $state.go($state.current.name, { pageIndex: parseInt($stateParams.pageIndex), itemsPerPage: $scope.pageSize, selectedConsumer: $scope.selectedConsumer, selectedProvider: $scope.selectedProvider, selectedService: $scope.selectedService, fromDate: fDate, toDate: tDate, successfullyCheck: $scope.successfullyCheck, unsuccessfullyCheck: $scope.unsuccessfullyCheck });
            };

            $scope.pageChangeHandler = function (currentPage) {

                $state.go($state.current.name, { pageIndex: parseInt(currentPage), itemsPerPage: $scope.pageSize, selectedConsumer: $scope.selectedConsumer, selectedProvider: $scope.selectedProvider, selectedService: $scope.selectedService, successfullyCheck: $scope.successfullyCheck, unsuccessfullyCheck: $scope.unsuccessfullyCheck, fromDate: fDate, toDate: tDate });
                var a = "test";
            };

            $scope.pageSizeChanged = function (itemsPerPage) {

                $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: parseInt(itemsPerPage), selectedConsumer: $scope.selectedConsumer, selectedProvider: $scope.selectedProvider, selectedService: $scope.selectedService, successfullyCheck: $scope.successfullyCheck, unsuccessfullyCheck: $scope.unsuccessfullyCheck, fromDate: fDate, toDate: tDate });
            };

            $scope.messageLogStatisticDetails = function (transactionId, consumer, routingToken) {
                sharedPropertyMessageLogStatistic.setMsgProp($scope.pageIndex, $scope.pageSize, $scope.selectedConsumer, $scope.selectedProvider, $scope.selectedService, $scope.successfullyCheck, $scope.unsuccessfullyCheck, fDate, tDate);
                $state.go("messageLogStatisticDetails", { transactionId: transactionId, consumer: consumer, routingToken: routingToken });
            };

            $scope.clearSearchStatistics = function () {
                $scope.selectedConsumer = "";
                $scope.selectedProvider = "";
                $scope.selectedService = "";
                $scope.successfullyCheck = false;
                $scope.unsuccessfullyCheck = false;

                statisticsService.getServices().then(function (servicesResponse) {
                    $scope.servicesList = servicesResponse;
                });

                statisticsService.getParticipants().then(function (response) {
                    $scope.consumerList = response;
                    $scope.providerList = response;

                });

                //setting fromDate and toDate to default values
                $scope.toDate = new Date();
                var oneMonthEarlier = new Date();
                if ($scope.fromDate !== undefined) {
                    if (oneMonthEarlier.getMonth() - 1 === 0) {
                        $scope.fromDate = new Date(2016, 1, 19);
                    } else {
                        if (oneMonthEarlier.getMonth() - 1 == 1) {
                            $scope.fromDate = new Date(oneMonthEarlier.getFullYear(), oneMonthEarlier.getMonth() - 1, 19);
                        } else {
                            $scope.fromDate = new Date(oneMonthEarlier.getFullYear(), oneMonthEarlier.getMonth() - 1, oneMonthEarlier.getDate() - 1);
                        }
                    }
                    $scope.minFromDate = new Date(2016, 1, 19);
                }
                
                //enis => da se setiraat pikeri pri klik na kopceto iscisti
                var testToDate = new Date();
                var testFromDate = new Date();
                
                var initFDate = new Date();

                if (testFromDate.getDate() == 1) {
                    testFromDate.setDate(testFromDate.getDate() - 1);
                    initFDate = testFromDate.getMonth().toString() + "." + (testFromDate.getDate()).toString() + "." + testFromDate.getFullYear();
                } else {
                    initFDate = testFromDate.getMonth().toString() + "." + (testFromDate.getDate() - 1).toString() + "." + testFromDate.getFullYear();
                }
                
                var initTDate = (testToDate.getMonth() + 1).toString() + "." + testToDate.getDate().toString() + "." + testToDate.getFullYear();
                
                $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: $scope.pageSize, selectedConsumer: "", selectedProvider: "", selectedService: "", successfullyCheck: false, unsuccessfullyCheck: false, fromDate: initFDate, toDate: initTDate });
            };

        }])

    .controller('MessageLogStatisticDetailsCtrl', ['$scope', 'messageLogStatistic', '$state', 'statisticsService', 'sharedPropertyMessageLogStatistic',
        function ($scope, messageLogStatistic, $state, statisticsService, sharedPropertyMessageLogStatistic) {
            $scope.message = messageLogStatistic;
            $scope.noReqResponseXParticipant = messageLogStatistic.dirParticipantConsumer;
            $scope.noReqResponseYParticipant = messageLogStatistic.dirParticipantProvider;
            $scope.noReqResponseCS = messageLogStatistic.dirCS;
            $scope.showCheck = false;

            $scope.handleBackBtn = function () {
                var obj = sharedPropertyMessageLogStatistic.getMsgProp();
                $state.go('statistics', { pageIndex: obj.pageIndex, itemsPerPage: obj.itemsPerPage, selectedConsumer: obj.selectedConsumer, selectedProvider: obj.selectedProvider, selectedService: obj.selectedService, successfullyCheck: obj.successfullyCheck, unsuccessfullyCheck: obj.unsuccessfullyCheck, fromDate: obj.fromDate, toDate: obj.toDate });
            };

            $scope.modelToPreview = {
                data: {},
                title: "",
                isShown: false,
                currentId: 0,
                styleHeading: "",
                typeInfo: "",
                noReqRes: 0
            };
            $scope.messageLogStatisticDetailsPreview = function (message, type) {
                if ($scope.modelToPreview.currentId == message.id && $scope.modelToPreview.isShown) {
                    $scope.modelToPreview.isShown = false;
                } else {
                    $scope.modelToPreview.currentId = message.id;

                    switch (type) {
                        case 'consumer':
                            $scope.modelToPreview.title = message.consumerName;
                            break;
                        case 'cs':
                            $scope.modelToPreview.title = 'Централна локација';
                            break;
                        case 'routingToken':
                            $scope.modelToPreview.title = message.routingTokenName;
                            break;
                        default:
                    }
                    $scope.modelToPreview.data = message;
                    $scope.modelToPreview.isShown = true;

                    if ($scope.modelToPreview.data.faultReason != null) {

                        $scope.modelToPreview.styleHeading = "panel-danger";
                        $scope.modelToPreview.typeInfo = "Настаната е грешка на страната на";

                    } else if (type == 'consumer' && $scope.noReqResponseXParticipant != 2 && $scope.modelToPreview.data.faultReason == null) {

                        $scope.modelToPreview.styleHeading = "panel-danger";
                        $scope.modelToPreview.typeInfo = "Настаната е грешка на страната на";

                    }
                    else if (type == 'cs' && $scope.noReqResponseCS != 2 && $scope.modelToPreview.data.faultReason == null) {

                        $scope.modelToPreview.styleHeading = "panel-danger";
                        $scope.modelToPreview.typeInfo = "Настаната е грешка на страната на";

                    }
                    else if (type == 'routingToken' && $scope.noReqResponseYParticipant != 2 && $scope.modelToPreview.data.faultReason == null) {

                        $scope.modelToPreview.styleHeading = "panel-danger";
                        $scope.modelToPreview.typeInfo = "Настаната е грешка на страната на";

                    }
                    else {

                        $scope.modelToPreview.styleHeading = "panel-info";
                        $scope.modelToPreview.typeInfo = "Инфо";
                    }

                }

            };
            $scope.getMessageLogCheckTimeStamp = function (tokenTimestamp) {
                statisticsService.getMessageLogCheckTimeStamp(tokenTimestamp).then(function (response) {
                    $scope.showCheck = true;
                    $scope.checkTimestampResponse = response;
                });
            };

            $scope.selectedX = -1;
            $scope.selectedCS = -1;
            $scope.selectedY = -1;

            $scope.selectX = function (index) {
                $scope.selectedX = index;
                $scope.selectedCS = -1;
                $scope.selectedY = -1;
            };
            $scope.selectCS = function (index) {
                $scope.selectedX = -1;
                $scope.selectedCS = index;
                $scope.selectedY = -1;
            };
            $scope.selectY = function (index) {
                $scope.selectedX = -1;
                $scope.selectedCS = -1;
                $scope.selectedY = index;
            };
        }]);


