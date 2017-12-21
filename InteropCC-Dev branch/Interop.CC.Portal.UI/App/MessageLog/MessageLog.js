

app.config(function ($stateProvider, appBaseUrl) {
    $stateProvider
        .state("messageLog", {
            title: 'Логови',
            controller: "MessageLogCtrl",
            url: "/messageLog/{pageIndex:int}/{itemsPerPage:int}/{filterConsumer:string}/{filterProvider:string}/{filterService:string}/{filterMethod:string}/:filterTransactionSuccess:bool/:fromDate/:toDate/{sortDir:string}/{sortCol:string}",
            templateUrl: appBaseUrl + "Module/MessageLog",
            resolve: {
                pagedMessageLogs: ['messageLogService', '$stateParams', function (messageLogService, $stateParams) {
                    return messageLogService.getPagedMessageLogs($stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.filterConsumer, $stateParams.filterProvider, $stateParams.filterService, $stateParams.filterMethod, $stateParams.filterTransactionSuccess, $stateParams.fromDate, $stateParams.toDate, $stateParams.sortDir, $stateParams.sortCol);
                }]
            }
        })
        .state("messageLogDetails", {
            title: 'Детали за избраниот лог',
            url: "/messageLogDetails/:transactionId/:dir",
            templateUrl: 'App/MessageLog/Templates/MessageLogDetails',
            controller: 'MessageLogDetailsCtrl',
            resolve: {
                messageLog: ['messageLogService', '$stateParams', function (messageLogService, $stateParams) {
                    return messageLogService.getMessageLog($stateParams.transactionId);
                }]
            }
        });

});
angular.module("MessageLog", [])

.service("messageLogService", ['$http', '$q', 'handleResponseService', 'apiUri', 'apiCSUri', 'institution',
    function ($http, $q, handleResponseService, apiUri, apiCsUri, institution) {
        return ({
            getPagedMessageLogs: getPagedMessageLogs,
            getMessageLog: getMessageLog,
            createPdf: createPdf,
            createExcel: createExcel,
            getParticipants: getParticipants,
            getProviders: getProviders,
            getConsumersForProvider: getConsumersForProvider,
            getServices: getServices,
            getServiceMethods: getServiceMethods,
            //NOVO
            getConsumersByAccMap: getConsumersByAccMap,
            //NOVO
            getProvidersByAccMap: getProvidersByAccMap,
            //NOVO
            getServicesByAccMap: getServicesByAccMap,
            //NOVO
            getServiceMethodsByAccMap: getServiceMethodsByAccMap,
            //NOVO
            getProvidersByConsumerAccMap: getProvidersByConsumerAccMap,
            //NOVO
            getConsumersForProviderAccMap: getConsumersForProviderAccMap
        });

        function getPagedMessageLogs(pageIndex, itemsPerPage, filterConsumer, filterProvider, filterService, filterMethod, filterTransactionSuccess, fromDate, toDate, sortDir, sortCol) {
            var request = $http({
                method: 'GET',
                url: apiUri + "MessageLog/GetMessageLogListPaged",
                params: {
                    pageIndex: pageIndex,
                    itemsPerPage: itemsPerPage,
                    filterConsumer: filterConsumer,
                    filterProvider: filterProvider,
                    filterTransactionSuccess: filterTransactionSuccess,
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

        function getMessageLog(transactionId) {
            var request = $http({
                method: 'GET',
                url: apiUri + 'MessageLog/GetMessageLogByTid',
                params: {
                    transactionId: transactionId
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function createPdf(filterConsumer, filterProvider, filterService, filterMethod, filterTransactionSuccess, fromDate, toDate, sortDir, sortCol) {
            var request = $http({
                method: "GET",
                url: apiUri + "MessageLog/CreatePdf",
                responseType: "arraybuffer",

                params: {
                    filterConsumer: filterConsumer,
                    filterProvider: filterProvider,
                    filterTransactionSuccess: filterTransactionSuccess,
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
        function createExcel(filterConsumer, filterProvider, filterService, filterMethod, filterTransactionSuccess, fromDate, toDate, sortDir, sortCol) {
            var request = $http({
                method: "POST",
                url: apiUri + "MessageLog/CreateExcel",
                responseType: "arraybuffer",

                params: {
                    filterConsumer: filterConsumer,
                    filterProvider: filterProvider,
                    filterTransactionSuccess: filterTransactionSuccess,
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

        function getParticipants() {
            var request = $http({
                method: 'GET',
                url: apiCsUri + "Helper/GetParticipants"
            });
            return (request.then(handleResponseService.handleSuccess));
        }

        //NOVO
        function getConsumersByAccMap() {
            var request = $http({
                method: 'GET',
                url: apiCsUri + "Helper/GetConsumersByAccMap",
                params: {
                    institution: institution
                }
            });
            return (request.then(handleResponseService.handleSuccess));
        }
        
        //NOVO
        function getProvidersByAccMap(consumer) {
            var request = $http({
                method: 'GET',
                url: apiCsUri + "Helper/GetProvidersByAccMap",
                params: {
                    institution: institution,
                    consumer: consumer
                }
            });
            return (request.then(handleResponseService.handleSuccess));
        }
        
        //NOVO
        function getServicesByAccMap(institutionCode, provider, consumer) {
            var request = $http({
                method: 'GET',
                url: apiCsUri + "Helper/GetServicesByAccMap",
                params: {
                    institution: institutionCode,
                    provider: provider,
                    consumer: consumer
                }
            });
            return (request.then(handleResponseService.handleSuccess));
        }
        
        //NOVO
        function getServiceMethodsByAccMap(service, provider, consumer) {
            
            var request = $http({
                method: 'GET',
                url: apiCsUri + "Helper/GetServiceMethodsByAccMap",
                params: {
                    service: service,
                    provider: provider,
                    consumer: consumer,
                    institution: institution
                }
            });
            return (request.then(handleResponseService.handleSuccess));
        }
        
        function getProviders(consumer) {
            var request = $http({
                method: "GET",
                url: apiCsUri + "Helper/GetProviders",
                params: {
                    consumer: consumer
                }
            });
            return (request.then(handleResponseService.handleSuccess));
        }
        
        //NOVO
        function getProvidersByConsumerAccMap(consumer) {
            var request = $http({
                method: "GET",
                url: apiCsUri + "Helper/GetProvidersByConsumerAccMap",
                params: {
                    consumer: consumer,
                    institution: institution
                }
            });
            return (request.then(handleResponseService.handleSuccess));
        }
        
        function getConsumersForProvider(provider) {
            var request = $http({
                method: 'GET',
                url: apiCsUri + "Helper/GetConsumerByProvider",
                params: {
                    provider: provider
                }
            });
            return (request.then(handleResponseService.handleSuccess));
        }
        
        function getConsumersForProviderAccMap(provider) {
            var request = $http({
                method: 'GET',
                url: apiCsUri + "Helper/GetConsumerForProviderAccMap",
                params: {
                    provider: provider,
                    institution: institution
                }
            });
            return (request.then(handleResponseService.handleSuccess));
        }

        function getServices(consumer, provider) {
            var request = $http({
                method: 'GET',
                url: apiCsUri + "Helper/GetServices",
                params: {
                    consumer: consumer,
                    provider: provider
                }
            });
            return (request.then(handleResponseService.handleSuccess));
        }

        function getServiceMethods(service) {
            var request = $http({
                method: 'GET',
                url: apiCsUri + "Helper/GetServiceMethods",
                params: {
                    service: service
                }
            });
            return (request.then(handleResponseService.handleSuccess));
        }

    }]).

service('sharedProperyMessageLog', ['$http', function ($http) {
    var obj = [];
    return ({
        setMsgProp: setMsgProp,
        getMsgProp: getMsgProp
    });



    function setMsgProp(pageIndex, itemsPerPage, filterConsumer, filterProvider, filterService, filterMethod, filterTransactionSuccess, fromDate, toDate, sortDir, sortCol) {
        obj.pageIndex = pageIndex;
        obj.itemsPerPage = itemsPerPage;
        obj.filterConsumer = filterConsumer;
        obj.filterProvider = filterProvider;
        obj.filterTransactionSuccess = filterTransactionSuccess;
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

}]).

controller("MessageLogCtrl", ['$scope', 'pagedMessageLogs', '$state', '$stateParams', 'sharedProperyMessageLog', 'messageLogService', 'institution',
    function ($scope, pagedMessageLogs, $state, $stateParams, sharedProperyMessageLog, messageLogService, institution) {

        var param = $stateParams;
        $scope.pageIndex = parseInt(param.pageIndex);
        $scope.totalSize = pagedMessageLogs.totalSize;
        $scope.messageLogs = pagedMessageLogs.items;
        $scope.itemsPerPage = pagedMessageLogs.pageSize;
        $scope.pageSize = pagedMessageLogs.pageSize;
        $scope.filterConsumer = param.filterConsumer;
        $scope.filterProvider = param.filterProvider;
        $scope.filterService = param.filterService;
        $scope.filterMethod = param.filterMethod;
        $scope.sortDir = $stateParams.sortDir;
        $scope.sortCol = $stateParams.sortCol;
        //init ddl
        $scope.initConsumers = [];
        $scope.initProviders = [];
        $scope.initServices = [];
        $scope.initMethods = [];
        //init dates
        $scope.initFromDate = param.fromDate;
        $scope.initToDate = param.toDate;
        

        //dodadeno
        $scope.fromDate = $stateParams.fromDate;
        $scope.toDate = $stateParams.toDate;
        
        //STARO
        //if ($scope.filterMethod != "") {
        //    messageLogService.getServiceMethods($scope.filterService).then(function (serviceMethodsResponse) {
        //        $scope.serviceMethods = serviceMethodsResponse;
        //    });
        //}
        
        //NOVO
        if ($scope.filterMethod != "") {
            messageLogService.getServiceMethodsByAccMap($scope.filterService, $scope.filterProvider, $scope.filterConsumer, institution).then(function (serviceMethodsResponse) {
                $scope.serviceMethods = serviceMethodsResponse;
            });
        }

        function initTransactionSuccessDropdown() {
            $scope.transactionSuccessOptions = [{ optionName: 'Успешност на трансакција...', val: null }, { optionName: 'Успешна', val: true }, { optionName: 'Неуспешна', val: false }];
            if ($stateParams.filterTransactionSuccess) {
                if ($stateParams.filterTransactionSuccess == 'true' || $stateParams.filterTransactionSuccess == 'false') {
                    if ($stateParams.filterTransactionSuccess == 'true') {
                        $scope.transactionSuccessSelected = $scope.transactionSuccessOptions[1];
                    } else {
                        $scope.transactionSuccessSelected = $scope.transactionSuccessOptions[2];
                    }
                } else {
                    $scope.transactionSuccessSelected = $scope.transactionSuccessOptions[0];
                }
            } else {
                $scope.transactionSuccessSelected = $scope.transactionSuccessOptions[0];
            }
        }

        initTransactionSuccessDropdown();
        
        //STARO
        //$scope.changedConsumer = function (consumerDropdown) {
        //    if (consumerDropdown == null) {

        //    } else {

        //        if ($scope.filterProvider == "" || $scope.filterProvider == undefined) {
        //            messageLogService.getProviders($scope.filterConsumer).then(function (response) {
        //                $scope.providers = response;
        //                $scope.filterProvider = undefined;
        //                $scope.filterService = undefined;
        //                $scope.serviceMethods = null;
        //                $scope.filterMethod = undefined;
        //            });
        //        } else {

        //            messageLogService.getProviders($scope.filterConsumer).then(function (response) {
        //                $scope.providers = response;
        //                $scope.filterProvider = undefined;
        //                $scope.services = null;
        //                $scope.filterService = undefined;
        //                $scope.serviceMethods = null;
        //                $scope.filterMethod = undefined;
        //            });
                    
        //            messageLogService.getServices($scope.filterConsumer, $scope.filterProvider).then(function (response) {
        //                $scope.services = response;
        //                $scope.filterService = undefined;
        //            });
        //        }
        //    }
        //};
        
        //NOVO
        $scope.changedConsumer = function (consumerDropdown) {
            if (consumerDropdown == null) {

            } else {
                
                    //$scope.providers = null;
                    //$scope.filterProvider = undefined;
                    $scope.services = null;
                    $scope.filterService = undefined;
                    $scope.serviceMethods = null;
                    $scope.filterMethod = undefined;

                    if ($scope.filterProvider == "" || $scope.filterProvider == undefined) {
                        messageLogService.getProvidersByConsumerAccMap($scope.filterConsumer).then(function (response) {
                            $scope.providers = response;
                            if ($scope.providers.length === 1) {
                                if ($scope.providers[0].key === institution) {
                                    $scope.filterProvider = $scope.providers[0].key;
                                }
                            }
                            //$scope.filterProvider = undefined;
                            //$scope.filterService = undefined;
                            
                            
                            messageLogService.getServicesByAccMap(institution, $scope.filterProvider, $scope.filterConsumer).then(function (responseServices) {
                                $scope.services = responseServices;
                                $scope.filterService = undefined;
                            });

                            $scope.serviceMethods = null;
                            $scope.filterMethod = undefined;
                        });
                    } else {
                        //ako vekje e odbrana institution kako provider nema potreba povtorno da se pravi rebind na providers
                        if ($scope.filterProvider !== institution || $scope.filterConsumer === institution) {
                            
                            messageLogService.getProvidersByConsumerAccMap($scope.filterConsumer).then(function (response) {
                                $scope.providers = response;
                                $scope.filterProvider = undefined;
                                $scope.services = null;
                                $scope.filterService = undefined;
                                $scope.serviceMethods = null;
                                $scope.filterMethod = undefined;
                            });
                            
                        }

                        messageLogService.getServicesByAccMap(institution, $scope.filterProvider, $scope.filterConsumer).then(function (response) {
                            $scope.services = response;
                            $scope.filterService = undefined;
                        });
                    }
                    
                } 
        };

        //STARO
        //$scope.changedProvider = function (providerDropdown) {
        //    if (providerDropdown != null) {
                
        //        //rebing na consumer ddl
        //        messageLogService.getConsumersForProvider($scope.filterProvider).then(function (consumersResponse) {
        //            $scope.consumers = consumersResponse;
        //            $scope.serviceMethods = null;
        //            $scope.filterMethod = undefined;
        //            //$scope.filterConsumer = undefined;
        //        });
                
        //        //rebind na services ddl
        //        messageLogService.getServices($scope.filterConsumer, $scope.filterProvider).then(function (response) {
        //            $scope.services = response;
        //            $scope.filterService = undefined;
        //        });

        //    }
        //};
        
        //NOVO
        $scope.changedProvider = function (providerDropdown) {
            if (providerDropdown != null) {

                //rebing na consumer ddl
                messageLogService.getConsumersForProviderAccMap($scope.filterProvider).then(function (consumersResponse) {
                    $scope.consumers = consumersResponse;
                    if ($scope.consumers.length === 1) {
                        if ($scope.consumers[0].key === institution) {
                            $scope.filterConsumer = $scope.consumers[0].key;
                        }
                    }
                    $scope.serviceMethods = null;
                    $scope.filterMethod = undefined;
                    
                    //rebind na services ddl
                    messageLogService.getServicesByAccMap(institution, $scope.filterProvider, $scope.filterConsumer).then(function (response) {
                        $scope.services = response;
                        $scope.filterService = undefined;
                    });
                });

                //rebind na services ddl
                //messageLogService.getServicesByAccMap(institution, $scope.filterProvider, $scope.filterConsumer).then(function (response) {
                //    $scope.services = response;
                //    $scope.filterService = undefined;
                //});
            }
        };
        
        //STARO
        //$scope.changedService = function(serviceDropdown) {
        //    if (serviceDropdown != null) {
        //        messageLogService.getServiceMethods($scope.filterService).then(function(serviceMethodsResponse) {
        //            $scope.serviceMethods = serviceMethodsResponse;
        //            $scope.filterMethod = undefined;
        //        });
        //    }
        //};
        
        //NOVO
        $scope.changedService = function (serviceDropdown, providerDropdown, consumerDropdown) {
            if (serviceDropdown != null) {
                messageLogService.getServiceMethodsByAccMap(serviceDropdown, providerDropdown, consumerDropdown, institution).then(function (serviceMethodsResponse) {
                    $scope.serviceMethods = serviceMethodsResponse;
                    $scope.filterMethod = undefined;
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

            $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: $scope.pageSize, filterConsumer: $scope.filterConsumer, filterProvider: $scope.filterProvider, filterService: $scope.filterService, filterMethod: $scope.filterMethod, filterTransactionSuccess: $scope.transactionSuccessSelected.val, fromDate: fromdate, toDate: todate, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
        };


        if ($scope.fromDate == "") {
            $scope.fromDate = param.fromDate;
        } else {
            var datefrom = param.fromDate.split(".");
            datefrom = datefrom[1] + '.' + datefrom[0] + '.' + datefrom[2];
            $scope.fromDate = datefrom;
            //$scope.fromDate = decodeURIComponent(param.fromDate);
        }
        if ($scope.toDate == "") {
            $scope.toDate = param.toDate;
        } else {
            var dateto = param.toDate.split(".");
            dateto = dateto[1] + '.' + dateto[0] + '.' + dateto[2];
            $scope.toDate = dateto;
            //$scope.toDate = decodeURIComponent(param.toDate);
        }
      

        //$scope.maxFromDateTime = $scope.toDate != null ? $scope.toDate : new Date();
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
            
            if ($scope.toDate == "") {
                $scope.maxFromDate = new Date($scope.dayBeforeToday.getFullYear(), $scope.dayBeforeToday.getMonth(), $scope.dayBeforeToday.getDate());
                $scope.minFromDate = new Date(2016, 1, 19);

            } else if (typeof $scope.toDate === "string") {
                var stringDateTo = $scope.toDate.split('.');
                $scope.maxFromDate = new Date(stringDateTo[2], stringDateTo[1] - 1, stringDateTo[0] - 1);
                $scope.minFromDate = new Date(2016, 1, 19);

            } else if (typeof $scope.toDate === "object") {
                $scope.maxFromDate = new Date($scope.toDate.getFullYear(), $scope.toDate.getMonth(), $scope.toDate.getDate() - 1);
                $scope.minFromDate = new Date(2016, 1, 19);

            }
        };
        $scope.open2 = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.status1.opened = false;
            $scope.status2.opened = true;
            
            if (typeof $scope.fromDate === "string") {
                var stringDateFrom = $scope.fromDate.split('.');
                if (stringDateFrom[0] === '19' && stringDateFrom[1] === '2' && stringDateFrom[2] === '2016') {
                    $scope.minToDate = new Date(stringDateFrom[2], (parseInt(stringDateFrom[1])-1).toString(), (parseInt(stringDateFrom[0]) + 1).toString());
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

        //STARO
        //messageLogService.getParticipants().then(function (response) {
        //    $scope.consumers = response;
        //    if ($scope.filterConsumer != "") {
        //        messageLogService.getProviders($scope.filterConsumer).then(function (responseProviders) {
        //            $scope.providers = responseProviders;
        //        });
        //    } else {
        //        $scope.providers = response;
        //        $scope.initProviders = response;
        //        $scope.initConsumers = response;
        //    }
        //    //$scope.providers = response;
        //});
        
      
            
            //NOVO
            messageLogService.getConsumersByAccMap().then(function (response) {
                $scope.consumers = response;
                $scope.initConsumers = response;
                //if ($scope.filterConsumer == "") {
                //    $scope.initConsumers = response;
                //}
            });
            
            //NOVO
            messageLogService.getProvidersByAccMap($scope.filterConsumer).then(function (response) {
                $scope.providers = response;
                $scope.initProviders = response;
            });
       
        
        //STARO
        //messageLogService.getServices($scope.filterConsumer, $scope.filterProvider).then(function (response) {
        //    if ($scope.filterService != "") {
        //        if ($scope.filterProvider != "" || $scope.filterProvider != undefined) {
        //            messageLogService.getServices($scope.filterConsumer, $scope.filterProvider).then(function (responseServices) {
        //                $scope.services = responseServices;
        //            });
        //        }
        //    } else {
        //        $scope.services = response;
        //        $scope.initServices = response;
        //    }
        //    //$scope.services = servicesResponse;
        //});
        
        //NOVO
        messageLogService.getServicesByAccMap(institution, $scope.filterProvider, $scope.filterConsumer).then(function (response) {
            if ($scope.filterService != "") {
                if ($scope.filterProvider != "" || $scope.filterProvider != undefined) {
                    messageLogService.getServicesByAccMap(institution, $scope.filterProvider, $scope.filterConsumer).then(function (responseServices) {
                        $scope.services = responseServices;
                    });
                }
            } else {
                $scope.services = response;
                $scope.initServices = response;
            }
            //$scope.services = servicesResponse;
        });
        
        //NOVO, nemase logika za ova, posle filtriranje se gubese setot na podatoci vo ddl za servis metod
        if ($scope.filterService !== "") {
            messageLogService.getServiceMethodsByAccMap($scope.filterService, $scope.filterProvider, $scope.filterConsumer, institution).then(function (serviceMethodsResponse) {
                $scope.serviceMethods = serviceMethodsResponse;
            });
        }
        
        
        $scope.itemsPage = [
           { itemId: 10, itemName: '10' },
           { itemId: 30, itemName: '30' },
           { itemId: 50, itemName: '50' },
           { itemId: 70, itemName: '70' }
        ];

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


            $state.go($state.current.name, { pageIndex: parseInt(currentPage), itemsPerPage: $scope.pageSize, filterConsumer: $scope.filterConsumer, filterProvider: $scope.filterProvider, filterService: $scope.filterService, filterMethod: $scope.filterMethod, filterTransactionSuccess: $scope.transactionSuccessSelected.val, fromDate: fromdate, toDate: todate, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
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
                if ($scope.minToDate) {
                    var fromD = $scope.minToDate;
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

            $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: parseInt(itemsPerPage), filterConsumer: $scope.filterConsumer, filterProvider: $scope.filterProvider, filterService: $scope.filterService, filterMethod: $scope.filterMethod, filterTransactionSuccess: $scope.transactionSuccessSelected.val, fromDate: fromdate, toDate: todate, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
        };

        $scope.filterHandler = function () {

            var todate, fromdate;

            if (typeof $scope.fromDate === "string") {
                if ($scope.fromDate !== "") {
                    var stringDateFrom = $scope.fromDate.split('.');
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
                todate = $scope.toDate;
            }

            $state.go($state.current.name, { pageIndex: parseInt(1), itemsPerPage: $scope.pageSize, filterConsumer: $scope.filterConsumer, filterProvider: $scope.filterProvider, filterService: $scope.filterService, filterMethod: $scope.filterMethod, filterTransactionSuccess: $scope.transactionSuccessSelected.val, fromDate: fromdate, toDate: todate, sortDir: $scope.sortDir, sortCol: $scope.sortCol });

        };

        $scope.filterClear = function () {
            initTransactionSuccessDropdown();
            //var stateFromDate = $stateParams.fromDate;
            //var stateToDate = $stateParams.toDate;
            $scope.providers = $scope.initProviders;
            $scope.consumers = $scope.initConsumers;
            $scope.services = $scope.initServices;
            $scope.serviceMethods = $scope.initMethods;

            $scope.filterProvider = "";
            $scope.filterConsumer = "";
            $scope.filterService = "";
            $scope.filterMethod = "";

            var testToDate = new Date();
            var testFromDate = new Date();
            
            var initFDate = new Date();
            
            if (testFromDate.getDate() == 1) {
                testFromDate.setDate(testFromDate.getDate() - 1);
                initFDate = testFromDate.getMonth().toString() + "." + (testFromDate.getDate()).toString() + "." + testFromDate.getFullYear();
            } else {
                initFDate = testFromDate.getMonth().toString() + "." + (testFromDate.getDate() - 1).toString() + "." + testFromDate.getFullYear();
            }

            //var initFDate = testFromDate.getMonth().toString() + "." + (testFromDate.getDate()-1).toString() + "." + testFromDate.getFullYear();
      
            var initTDate = (testToDate.getMonth() + 1).toString() + "." + testToDate.getDate().toString() + "." + testToDate.getFullYear();
            
            $state.go($state.current.name, { pageIndex: $scope.pageIndex, itemsPerPage: $scope.pageSize, filterConsumer: "", filterProvider: "", filterService: "", filterMethod: "", filterTransactionSuccess: null, fromDate: initFDate, toDate: initTDate, sortDir: 'desc', sortCol: 'RequestTimestamp' });
            

            $scope.toDate = new Date();
            var oneMonthBef = new Date();
            var valueFromDate;

            if (oneMonthBef.getMonth() - 1 === 0) {
                fromDate = new Date(2016, 1, 19);
            } else {

                //ako sme 2 mesec, setiraj da e 19.02.2016 default vrednost na OD pickerot
                if (oneMonthBef.getMonth() - 1 == 1) {
                    valueFromDate = new Date(oneMonthBef.getFullYear(), oneMonthBef.getMonth() - 1, 19);
                    $scope.fromDate = valueFromDate;
                } else {
                    valueFromDate = new Date(oneMonthBef.getFullYear(), oneMonthBef.getMonth() - 1, oneMonthBef.getDate() - 1);
                    $scope.fromDate = valueFromDate;
                }
            }
            
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

            messageLogService.createPdf($scope.filterConsumer, $scope.filterProvider, $scope.filterService, $scope.filterMethod, $scope.transactionSuccessSelected.val, fromd, tod, $scope.sortDir, $scope.sortCol).then(function (result) {
                if (result) {

                    var blob = new Blob([result], { type: "application/pdf" });
                    var name = "MessageLogs.pdf";
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


            messageLogService.createExcel($scope.filterConsumer, $scope.filterProvider, $scope.filterService, $scope.filterMethod, $scope.transactionSuccessSelected.val, fromd, tod, $scope.sortDir, $scope.sortCol).then(function (response) {

                var blob = new Blob([response], { type: "application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                var name = "MessageLogs.xlsx";
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
        //url: "/messageLog/{pageIndex:int}/{itemsPerPage:int}/{filterConsumer:string}/{filterProvider:string}/{filterDir:string}/{filterService:string}/{filterMethod:string}/:fromDate/:toDate/{sortDir:string}/{sortCol:string}",

        $scope.messageLogDetails = function (transactionId, pageIndex, itemsPerPage, filterConsumer, filterProvider, filterService, filterMethod, filterTransactionSuccess) {

            var todate, fromdate;

            if (typeof $scope.fromDate === "string") {
                if ($scope.fromDate !== "") {
                    var stringDateFrom = $scope.fromDate.split('.');
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
                todate = $scope.toDate;
            }

            sharedProperyMessageLog.setMsgProp(pageIndex, itemsPerPage, filterConsumer, filterProvider, filterService, filterMethod, filterTransactionSuccess, fromdate, todate, $scope.sortDir, $scope.sortCol);
            $state.go("messageLogDetails", { transactionId: transactionId });
        };

        $scope.selectedRow = null;
        $scope.setClickedRow = function (index) {
            $scope.selectedRow = index;
        };
        
        //saved vars after loading
        
      
    }]).

controller('MessageLogDetailsCtrl', ['$scope', 'messageLog', 'sharedProperyMessageLog', '$state', function ($scope, messageLog, sharedProperyMessageLog, $state) {
    $scope.message = messageLog;

    $scope.handleBackBtnMsgLogDetails = function () {
        var obj = sharedProperyMessageLog.getMsgProp();
        if (obj.pageIndex) {
            $state.go('messageLog', { pageIndex: obj.pageIndex, itemsPerPage: obj.itemsPerPage, filterConsumer: obj.filterConsumer, filterProvider: obj.filterProvider, filterService: obj.filterService, filterMethod: obj.filterMethod, filterTransactionSuccess: obj.filterTransactionSuccess, fromDate: obj.fromDate, toDate: obj.toDate, sortDir: obj.sortDir, sortCol: obj.sortCol });
        } else {
            $state.go('messageLog', { pageIndex: 1, itemsPerPage: 10 });
        }

    };
}]);
