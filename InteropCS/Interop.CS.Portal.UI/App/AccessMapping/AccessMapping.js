app.config(function ($stateProvider, appBaseUrl) {
    $stateProvider
        .state('accessmappings', {
            title: 'Пристапна листа',
            controller: 'AccessMappingCtrl',
            url: '/accessmappings/{pageIndex:int}/{itemsPerPage:int}/:providerCode/:consumerCode/:serviceCode/:isActive:bool',
            templateUrl: appBaseUrl + 'Module/AccessMapping',
            resolve: {
                accessMappingPageList: ['accessMappingService', '$stateParams',
                    function (accessMappingService, $stateParams) {
                        return accessMappingService.getAccessMappingPaged($stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.providerCode, $stateParams.consumerCode, $stateParams.serviceCode, $stateParams.isActive);
                    }]
            }
        })
        .state('createaccessmapping', {
            title: 'Креирај запис во пристапната листа',
            controller: 'CreateAccessMappingCtrl',
            url: '/accessmapping/new',
            templateUrl: 'App/AccessMapping/Templates/CreateAccessMapping'
        });

});

angular.module("AccessMapping", ['ngTable', 'ui.bootstrap'])
    .service('accessMappingService', ['$http', '$q', 'apiUri', 'handleResponseService',
        function($http, $q, apiUri, handleResponseService) {
            return ({
                getAccessMappings: getAccessMappings,
                createAccessMapping: createAccessMapping,
                activate: activate,
                getAccessMappingPaged: getAccessMappingPaged,
                getServicesbyPartiCode: getServicesbyPartiCode,
                getInternalParticipants: getInternalParticipants,
                getExternalParticipants: getExternalParticipants,
                getMethodsForService: getMethodsForService,
                getBuses: getBuses,
                getParticipants: getParticipants,
                getProviders: getProviders,
                getConsumers: getConsumers,
                getAccessMappingsServices: getAccessMappingsServices
            });


            function getParticipants() {
                var request = $http({
                    method: "GET",
                    url: apiUri + "Participant/GetParticipantsWoMim"
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }
            
            function getProviders() {
                var request = $http({
                    method: "GET",
                    url: apiUri + "AccessMapping/GetProviders"
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }

            function getConsumers() {
                var request = $http({
                    method: "GET",
                    url: apiUri + "AccessMapping/GetConsumers"
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }

            function getAccessMappingsServices() {
                var request = $http({
                    method: "GET",
                    url: apiUri + "AccessMapping/GetAccessMappingServices"
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }
            /*Testing
            Testing*/
            

            function getInternalParticipants() {
                var request = $http({
                    method: "GET",
                    url: apiUri + "Participant/GetInternalParticipantsAccMap"
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }

            function getExternalParticipants(bus) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "Participant/GetExternalParticipants",
                    params: {
                        bus: bus
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }

            function getBuses() {
                var request = $http({
                    method: "GET",
                    url: apiUri + "Buses/GetBuses"
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }

            function getAccessMappings() {
                var request = $http({
                    method: "GET",
                    url: apiUri + "AccessMapping/GetAccessMappingList"
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }

            function getAccessMappingPaged(pageIndex, itemsPerPage, providerCode, consumerCode, serviceCode, isActive) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "AccessMapping/GetAccessMappingListPaged",
                    params: {
                        pageIndex: pageIndex,
                        itemsPerPage: itemsPerPage,
                        searchProviderCode: providerCode,
                        searchConsumerCode: consumerCode,
                        searchServiceCode: serviceCode,
                        isActive: isActive
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }

            function createAccessMapping(accessMapping) {

                var request = $http({
                    method: "POST",
                    data: accessMapping,
                    url: apiUri + "AccessMapping/CreateAccessMapping"
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }

            function activate(accessMappingProviderCode, accessMappingConsumerCode, accessMappingServiceCode, accessMappingMethodCode, accessMappingProviderBusCode, accessMappingConsumerBusCode, shouldActivate) {

                var request = $http({
                    method: "PUT",
                    params: {
                        accessMappingConsumerCode: accessMappingConsumerCode,
                        accessMappingProviderCode: accessMappingProviderCode,
                        accessMappingServiceCode: accessMappingServiceCode,
                        accessMappingMethodCode: accessMappingMethodCode,
                        accessMappingProviderBusCode: accessMappingProviderBusCode,
                        accessMappingConsumerBusCode: accessMappingConsumerBusCode,
                        accessMappingIsActive: shouldActivate
                    },
                    url: apiUri + "AccessMapping/ActivateAccessMapping"
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }

            function getServicesbyPartiCode(participantCode) {

                var request = $http({
                    method: "GET",
                    url: apiUri + "Service/GetListByParticipantCode",
                    params: {
                        participantCode: participantCode
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }

            function getMethodsForService(providerCode, serviceCode) {

                var request = $http({
                    method: "GET",
                    url: apiUri + "Service/GetMethodsForWsdlAccMap",
                    params: {
                        providerCode: providerCode,
                        serviceCode: serviceCode
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
                ));
            }

        }])
    .controller("AccessMappingCtrl", ['$scope', '$state', 'accessMappingService', 'accessMappingPageList', '$stateParams', 'alerts',
        function($scope, $state, accessMappingService, accessMappingPageList, $stateParams, alerts) {

            // Sort
            $scope.sortType = 'providerCode';
            $scope.sortReverse = false;
            //Sort end

            $scope.search = [];

            var param = $stateParams;
            $scope.currentPage = parseInt(param.pageIndex);
            $scope.accessMappings = accessMappingPageList.items;
            $scope.itemsPerPage = accessMappingPageList.pageSize;
            $scope.totalItems = accessMappingPageList.totalSize;
            $scope.pageSize = accessMappingPageList.pageSize;
            $scope.search.ProviderCode = $stateParams.providerCode;
            $scope.search.ConsumerCode = $stateParams.consumerCode;
            $scope.search.ServiceCode = $stateParams.serviceCode;
            $scope.participantsList = [];
            $scope.providersList = [];
            $scope.consumersList = [];
            $scope.servicesList = [];

            /*testing*/
            //Fill Provider Dropdown
            function initProviderDropdown() {
                accessMappingService.getProviders()
                    .then(function(response) {
                        $scope.providersList = response;
                    });
            }

            initProviderDropdown();

            function initConsumerDropdown() {
                accessMappingService.getConsumers()
                    .then(function (response) {
                        $scope.consumersList = response;
                    })
            }

            initConsumerDropdown();
            /*testing*/
            //Fill Service Dropdown
            function initServiceDropdown() {
                accessMappingService.getAccessMappingsServices()
                    .then(function (response) {
                        $scope.servicesList = response;
                    });
            }

            initServiceDropdown();

            function initActivityDropdown() {
                $scope.search.activityOptions = [{ optionName: 'Одбери активност ...', val: null }, { optionName: 'Активен', val: true }, { optionName: 'Неактивен', val: false }];
                if ($stateParams.isActive) {
                    if ($stateParams.isActive == 'true' || $stateParams.isActive == 'false') {
                        if ($stateParams.isActive == 'true') {
                            $scope.search.Activity = $scope.search.activityOptions[1];
                        } else {
                            $scope.search.Activity = $scope.search.activityOptions[2];
                        }
                    } else {
                        $scope.search.Activity = $scope.search.activityOptions[0];
                    }
                } else {
                    $scope.search.Activity = $scope.search.activityOptions[0];
                }
            }

            initActivityDropdown();

            $scope.selectedProvider = null;
            $scope.selectedConsumer = null;
            $scope.selectedService = null;

            $scope.setSelectedRow = function(selectedProvider, selectedConsumer, selectedService) {
                $scope.selectedProvider = selectedProvider;
                $scope.selectedConsumer = selectedConsumer;
                $scope.selectedService = selectedService;
            };

            $scope.pageChangeHandler = function(currentPage) {
                $state.go($state.current.name, { pageIndex: parseInt(currentPage), itemsPerPage: $scope.pageSize, providerCode: $scope.search.ProviderCode, consumerCode: $scope.search.ConsumerCode, serviceCode: $scope.search.ServiceCode, isActive: $scope.search.Activity.val });
            };

            $scope.pageSizeChanged = function(itemsPerPage) {
                $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: parseInt(itemsPerPage), providerCode: $scope.search.ProviderCode, consumerCode: $scope.search.ConsumerCode, serviceCode: $scope.search.ServiceCode, isActive: $scope.search.Activity.val });
            };

            $scope.activate = function(accessMappingProviderCode, accessMappingConsumerCode, accessMappingServiceCode, accessMappingMethodCode, accessMappingProviderBusCode, accessMappingConsumerBusCode, shouldActivate) {
                accessMappingService.activate(accessMappingProviderCode, accessMappingConsumerCode, accessMappingServiceCode, accessMappingMethodCode, accessMappingProviderBusCode, accessMappingConsumerBusCode, shouldActivate).then(function() {
                    if (shouldActivate === true) {
                        alerts.addSuccess("Записот во пристапната листа со код за провајдер '" + accessMappingProviderCode + "',  код за корисник '" + accessMappingConsumerCode + "' , код за сервис '" + accessMappingServiceCode + " и метод '" + accessMappingMethodCode + "' е успешно активиран.");
                    } else {
                        alerts.addSuccess("Записот во пристапната листа со код за провајдер '" + accessMappingProviderCode + "',  код за корисник '" + accessMappingConsumerCode + "' , код за сервис '" + accessMappingServiceCode + " и метод '" + accessMappingMethodCode + "' е успешно деактивиран.");
                    }

                    return accessMappingService.getAccessMappingPaged($scope.currentPage, $scope.itemsPerPage, $stateParams.providerCode, $stateParams.consumerCode, $stateParams.serviceCode, $stateParams.isActive)
                        .then(function(accessMappings) {
                            $scope.accessMappings = accessMappings.items;
                            $scope.totalItems = accessMappings.totalSize;
                        });
                });
            };

            $scope.searchAccessMappings = function(searchTerms) {
                if (searchTerms === undefined) {
                    $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: $scope.pageSize, providerCode: "", consumerCode: "", serviceCode: "", isActive: null });
                } else {
                    $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: $scope.pageSize, providerCode: searchTerms.ProviderCode, consumerCode: searchTerms.ConsumerCode, serviceCode: searchTerms.ServiceCode, isActive: searchTerms.Activity.val });
                }
            };

            $scope.clearSearchAccessMappings = function() {
                $scope.search = [];
                initActivityDropdown();
                $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: $scope.pageSize, providerCode: "", consumerCode: "", serviceCode: "", isActive: null });
            };


        }])
    .controller("CreateAccessMappingCtrl", ["$scope", 'accessMappingService', '$state', 'alerts',
        function($scope, accessMappingService, $state, alerts) {


            $scope.accessMapping = {
                form: {},
                provider: "",
                consumer: '',
                partiCode: '',
                extPartiCode: '',
                consumerParticipantddl: '',
                extConsumerParticipantddl: '',
                serviceCodetxt: '',
                //consumerCodetxt: '',
                //providerCodetxt: '',
                providerServicesddl: "",
                serviceMethodsddl: "",

                Save: function(accessMapping) {

                    if (this.form.$invalid) {
                        var s = "";
                    }

                    var objectToSave = { ProviderCode: "", ConsumerCode: "", ServiceCode: "", MethodCode: "", ProviderBusCode: "", ConsumerBusCode: "", IsActive: true };
                    if (accessMapping.consumer == "internal") {
                        objectToSave.ConsumerCode = accessMapping.consumerParticipantddl.code;
                        objectToSave.ConsumerBusCode = "MIM1";
                    } else if (accessMapping.consumer == "external") {
                        objectToSave.ConsumerBusCode = accessMapping.consumerBusddl;
                        objectToSave.ConsumerCode = accessMapping.extConsumerParticipantddl.code;

                    }
                    if (accessMapping.provider == "internal") {
                        objectToSave.ProviderCode = accessMapping.partiCode.code;
                        objectToSave.ServiceCode = accessMapping.providerServicesddl;
                        objectToSave.MethodCode = accessMapping.serviceMethodsddl;
                        objectToSave.ProviderBusCode = "MIM1";
                    } else if (accessMapping.provider == "external") {
                        objectToSave.ProviderCode = accessMapping.extPartiCode.code;
                        objectToSave.ProviderBusCode = accessMapping.providerBusddl;
                        objectToSave.ServiceCode = accessMapping.providerServicesddl;
                        objectToSave.MethodCode = accessMapping.serviceMethodsddl;
                    }

                    accessMappingService.createAccessMapping(objectToSave)
                        .then(function(result) {
                            if (result.isSaved === true && result.isActivated === false) {
                                alerts.addSuccess("Креиран е запис во пристапната листа со код за провајдер '" + objectToSave.ProviderCode + "',  код за корисник '" + objectToSave.ConsumerCode + "' , код за сервис '" + objectToSave.ServiceCode + "' и метод '" + objectToSave.MethodCode + "'");
                            }
                            if (result.isSaved === true && result.isActivated === true) {
                                alerts.addSuccess("Записот во пристапната листа со код за провајдер '" + objectToSave.ProviderCode + "',  код за корисник '" + objectToSave.ConsumerCode + "' , код за сервис '" + objectToSave.ServiceCode + "' и метод '" + objectToSave.MethodCode + "' е активиран.");
                            }

                            $state.go('accessmappings', { pageIndex: 1, itemsPerPage: 10 });
                        });
                }
            };

            $scope.showServicesforProvider = false;
            $scope.showMethodsForService = false;
            $scope.participantsConsumer = [];
            $scope.participantsProvider = [];
            $scope.extParticipantsConsumer = [];
            $scope.extParticipantsProvider = [];
            $scope.services = [];
            $scope.showConsumerParticipant = false;
            $scope.showProviderParticipant = false;
            $scope.ShowTxtForProvider = false;
            $scope.ShowTxtForConsumer = false;
            $scope.ShowTxtForService = false;
            $scope.ShowTxtForMethod = false;

            $scope.changeConsumerParticipant = function(consumer) {
                $scope.accessMapping.consumerParticipantddl = '';
                $scope.showConsumerParticipant = true;
                if (consumer == 'external') {
                    accessMappingService.getBuses().then(function(data) {

                        $scope.consumerBuses = data;

                    });
                    $scope.ShowTxtForConsumer = true;
                } else {

                    accessMappingService.getInternalParticipants().then(function(data) {

                        $scope.participantsConsumer = data;

                    });
                    // $scope.accessMapping.consumerCodetxt = "";
                    $scope.ShowTxtForConsumer = false;
                }

            };

            $scope.changeConsumerBus = function(bus) {
                $scope.accessMapping.consumerParticipantddl = '';
                $scope.showConsumerParticipant = true;

                accessMappingService.getExternalParticipants(bus).then(function(data) {

                    $scope.extParticipantsConsumer = data;
                    $scope.ShowTxtForConsumer = false;
                });              
            

            };

            $scope.changeProviderBus = function(bus) {
                $scope.participantsProvider = [];
                $scope.accessMapping.partiCode = "";
                $scope.showProviderParticipant = true;

                accessMappingService.getExternalParticipants(bus).then(function(data) {

                    $scope.extParticipantsProvider = data;

                });
                $scope.accessMapping.providerServicesddl = "";
                $scope.accessMapping.serviceMethodsddl = "";
                //$scope.accessMapping.providerCodetxt = "";
                $scope.accessMapping.serviceCodetxt = "";
                $scope.accessMapping.methodtxt = "";
                $scope.ShowTxtForProvider = false;
                $scope.showServicesforProvider = false;
                $scope.ShowTxtForService = true;
                $scope.showMethodsForService = false;
                $scope.ShowTxtForMethod = true;
            };

            $scope.changeProviderParticipant = function(provider) {
                $scope.participantsProvider = [];
                $scope.accessMapping.partiCode = "";
                $scope.showProviderParticipant = true;
                if (provider == 'external') {

                    accessMappingService.getBuses().then(function(data) {

                        $scope.providerBuses = data;

                    });
                    $scope.accessMapping.providerServicesddl = "";
                    $scope.accessMapping.serviceMethodsddl = "";
                    // $scope.accessMapping.providerCodetxt = "";
                    $scope.accessMapping.serviceCodetxt = "";
                    $scope.accessMapping.methodtxt = "";
                    $scope.ShowTxtForProvider = true;
                    $scope.showServicesforProvider = false;
                    $scope.ShowTxtForService = true;
                    $scope.showMethodsForService = false;
                    $scope.ShowTxtForMethod = true;

                } else {
                    accessMappingService.getInternalParticipants().then(function(data) {

                        $scope.participantsProvider = data;

                    });


                    // $scope.accessMapping.providerCodetxt = "";
                    $scope.accessMapping.serviceCodetxt = "";
                    $scope.accessMapping.methodtxt = "";
                    $scope.showServicesforProvider = false;
                    $scope.showMethodsForService = true;
                    $scope.ShowTxtForProvider = false;
                    $scope.ShowTxtForService = false;
                    $scope.ShowTxtForMethod = false;
                }

            };

            $scope.ServicesListforProvider = function(x) {
                if (x != undefined) {
                    // if ($scope.accessMapping.provider == 'internal') {
                    accessMappingService.getServicesbyPartiCode(x).then(function(data) {
                        $scope.services = data;
                        $scope.showServicesforProvider = true;
                    });
                    //} else {
                    //    $scope.showServicesforProvider = false;
                    //}
                    $scope.ShowTxtForMethod = false;
                    $scope.accessMapping.methodtxt = "";
                }
            };

            $scope.MethodsListForService = function(providerCode, serviceCode) {

                if (serviceCode != null) {
                    accessMappingService.getMethodsForService(providerCode, serviceCode).then(function(data) {
                        $scope.methods = data;
                        $scope.showMethodsForService = true;
                    });

                } else {
                    $scope.showMethodsForService = false;
                }
            };

        }]);