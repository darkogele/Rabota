app.config(function ($stateProvider, appBaseUrl) {
    $stateProvider
         .state('accessmappings', {
             title: 'Пристапна листа',
             controller: 'AccessMappingCtrl',
             url: '/accessmappings/{pageIndex:int}/{itemsPerPage:int}/:consumerCode/:serviceCode/:isActive:bool',
             templateUrl: appBaseUrl + 'Module/AccessMapping',
             resolve: {
                 accessMappingList: ['accessMappingService', '$stateParams',
                     function (accessMappingService, $stateParams) {
                         return accessMappingService.getAccessMappingsPaged($stateParams.pageIndex, $stateParams.itemsPerPage);
                         //return accessMappingService.getAccMapp($stateParams.institution).then(function (response) {
                         //   return accessMappingService.getAccessMappingsPaged($stateParams.pageIndex, $stateParams.itemsPerPage, response.accessMappings);
                         //});
                     }]
             },
         })
         .state('createaccessmapping', {
             title: 'Креирај запис во пристапната листа',
             controller: 'CreateAccessMappingCtrl',
             url: '/accessmapping/new',
             templateUrl: 'App/AccessMapping/Templates/CreateAccessMapping'
         });

});
angular.module("AccessMapping", ['ngTable', 'ui.bootstrap'])
.service('accessMappingService', ['$http', '$q', 'apiUri', 'handleResponseService', 'institution',
        function ($http, $q, apiUri, handleResponseService, institution) {
            return ({
                createAccessMapping: createAccessMapping,
                getConsumers: getConsumers,
                getServices: getServices,
                getServiceMethods: getServiceMethods,
                getAccessMappingsPaged: getAccessMappingsPaged,
                getAccMapp: getAccMapp,
                activate: activate
            });

            function createAccessMapping(accessMapping) {
                var request = $http({
                    method: "POST",
                    url: apiUri + "AccessMapping/CreateAccessMapping",
                    data: accessMapping
                });
                return (request.then(handleResponseService.handleSuccess));
            }

            function getConsumers() {
                var request = $http({
                    method: "GET",
                    url: apiUri + "AccessMapping/GetOtherParticipants",
                    params: { institution: institution }
                });
                return (request.then(handleResponseService.handleSuccess));
            }

            function getServices() {
                var request = $http({
                    method: "GET",
                    url: apiUri + "AccessMapping/GetOwnServices"
                });
                return (request.then(handleResponseService.handleSuccess));
            }

            function getServiceMethods(service) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "AccessMapping/GetServiceMethods",
                    params: {
                        service: service
                    }
                });
                return (request.then(handleResponseService.handleSuccess));
            }

            function getAccessMappingsPaged(pageIndex, itemsPerPage) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "AccessMapping/GetAccessMappingsPaged",
                    //data: accessMappings,
                    params: {
                        pageIndex: pageIndex,
                        itemsPerPage: itemsPerPage,
                        providerCode: institution,
                        //accessMappings: accessMappings
                        //searchConsumerCode: consumerCode,
                        //searchServiceCode: serviceCode,
                        //isActive: isActive,
                    }

                });
                return (request.then(handleResponseService.handleSuccess));
            }

            function getAccMapp() {
                var request = $http({
                    method: "GET",
                    url: apiUri + "AccessMapping/GetAccMapp",
                    params: {
                        providerCode: institution,
                    }
                });
                return (request.then(handleResponseService.handleSuccess));
            }

            function activate(providerCode, consumerCode, serviceCode, serviceMethod, providerBusCode, consumerBusCode, activity) {
                var request = $http({
                    method: "PUT",
                    url: apiUri + "AccessMapping/ChangeAccessMappingActivity",
                    params: {
                        providerCode: providerCode,
                        consumerCode: consumerCode,
                        serviceCode: serviceCode,
                        serviceMethod: serviceMethod,
                        providerBusCode: providerBusCode,
                        consumerBusCode: consumerBusCode,
                        activity: activity
                    }
                });
                return (request.then(handleResponseService.handleSuccess));
            }

        }])
.controller("CreateAccessMappingCtrl", ["$scope", 'accessMappingService', '$state', 'alerts', 'institution',
        function ($scope, accessMappingService, $state, alerts, institution) {

            accessMappingService.getConsumers(institution).then(function (consumersResponse) {
                $scope.consumers = consumersResponse;
            });

            accessMappingService.getServices().then(function (servicesResponse) {
                $scope.services = servicesResponse;
            });

            $scope.getServiceMethods = function (service) {
                accessMappingService.getServiceMethods(service).then(function (serviceMethodsReponse) {
                    $scope.methods = serviceMethodsReponse;
                });
            };

            $scope.createAccessMapping = function (accessMapping) {
                var accessMappingToSave = { ProviderCode: "", ConsumerCode: "", ServiceCode: "", MethodCode: "", ProviderBusCode: "", ConsumerBusCode: "", IsActive: true };
                accessMappingToSave.ProviderCode = institution;
                accessMappingToSave.ConsumerCode = accessMapping.consumerCode.value;
                accessMappingToSave.ServiceCode = accessMapping.serviceCode.value;
                accessMappingToSave.MethodCode = accessMapping.methodCode;
                accessMappingService.createAccessMapping(accessMappingToSave).then(function (result) {
                    if (result.isSaved === true && result.isActivated === false) {
                        alerts.addSuccess("Креиран е запис во пристапната листа за корисник '" + accessMapping.consumerCode.text + "', код за сервис '" + accessMapping.serviceCode.text + "' и метод '" + accessMappingToSave.MethodCode + "'");
                    }
                    if (result.isSaved === true && result.isActivated === true) {
                        alerts.addSuccess("Записот во пристапната листа за корисник '" + accessMapping.consumerCode.text + "', код за сервис '" + accessMapping.serviceCode.text + "' и метод '" + accessMappingToSave.MethodCode + "' е активиран.");
                    }

                    $state.go('accessmappings', { pageIndex: 1, itemsPerPage: 10 });
                });
            };

        }])
.controller("AccessMappingCtrl", ["$scope", 'accessMappingService', '$state', 'alerts', '$stateParams', 'institution', 'accessMappingList', 
    function ($scope, accessMappingService, $state, alerts, $stateParams, institution, accessMappingList) {

        var param = $stateParams;
        $scope.accessMappings = accessMappingList.items;
        $scope.currentPage = parseInt(param.pageIndex);
        $scope.totalItems = accessMappingList.totalSize;
        $scope.itemsPerPage = accessMappingList.pageSize;
        $scope.pageSize = accessMappingList.pageSize;
        $scope.consumersList = [];
        $scope.servicesList = [];
        

        //function fillSearchParams() {
        //    for (var i = 0; i < accessMappingList.items.length; i++) {
        //        if ($scope.consumersList.indexOf(accessMappingList.items[i].consumerCode) == -1) {
        //            $scope.consumersList.push(accessMappingList.items[i].consumerCode);
        //        }
        //        if ($scope.servicesList.indexOf(accessMappingList.items[i].serviceCode) == -1) {
        //            $scope.servicesList.push(accessMappingList.items[i].serviceCode);
        //        }
        //    }
        //}

        //fillSearchParams();
       
        $scope.pageSizeChanged = function (itemsPerPage) {
            $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: parseInt(itemsPerPage) });
        };

        $scope.pageChangeHandler = function (currentPage) {
            $state.go($state.current.name, { pageIndex: parseInt(currentPage), itemsPerPage: $scope.pageSize });
        };

        $scope.activate = function (providerCode, providerName, consumerCode, consumerName, serviceCode, serviceName, serviceMethod, providerBusCode, consumerBusCode, activity) {
            accessMappingService.activate(providerCode, consumerCode, serviceCode, serviceMethod, providerBusCode, consumerBusCode, activity).then(function () {
                if (activity === true) {
                    alerts.addSuccess("Записот во пристапната листа за провајдер '" + providerName + "',  корисник '" + consumerName + "', сервис '" + serviceName + " и метод '" + serviceMethod + "' е успешно активиран.");
                } else {
                    alerts.addSuccess("Записот во пристапната листа за провајдер '" + providerName + "',  корисник '" + consumerName + "', сервис '" + serviceName + " и метод '" + serviceMethod + "' е успешно деактивиран.");
                }
                return accessMappingService.getAccessMappingsPaged($scope.currentPage, $scope.itemsPerPage, institution)
                    .then(function (accessMappings) {
                        $scope.accessMappings = accessMappings.items;
                        $scope.totalItems = accessMappings.totalSize;
                    });
            });
        };
    }
]);