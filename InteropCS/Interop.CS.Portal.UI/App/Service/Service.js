
app.config(function($stateProvider, appBaseUrl) {
    $stateProvider
        .state('services', {
            title: "Сервиси",
            controller: 'ServiceCtrl',
            url: '/services/{pageIndex:int}/{itemsPerPage:int}/:providerCode/:pickProvider',
            templateUrl: appBaseUrl + 'Module/Service',
            resolve: {
                servicePageList: ['serviceService', '$stateParams',
                    function(serviceService, $stateParams) {
                        return serviceService.getServicePaged($stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.providerCode,$stateParams.pickProvider);
                    }]
            }
        })
        .state('wsdl', {
            title: "WSDL",
            controller: 'WSDLCtrl',
            url: '/serviceWSDL/:providerCode/:serviceCode',
            templateUrl: 'App/Service/Templates/ShowWSDL',
            resolve: {
                wsdl: ['serviceService', '$stateParams',
                    function(serviceService, $stateParams) {
                        return serviceService.getWSDL($stateParams.providerCode, $stateParams.serviceCode);

                    }]
            }
        });

});

angular.module("Service", ['ngTable', 'ui.bootstrap'])
    .service('serviceService', ['$http', '$q', 'apiUri', 'handleResponseService',
        function($http, $q, apiUri, handleResponseService) {
            return ({
                getServiceList: getServiceList,
                getServicePaged: getServicePaged,
                getProviders: getProviders,
                getWSDL: getWSDL
            });

            function getServiceList() {
                var request = $http({
                    method: "GET",
                    url: apiUri + "Service/GetServiceList"
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
            }
            /*
            function ($http, handleResponseService, apiUri) {
                return ({
                    getParticipants: getParticipants,
                    createMessageLogStatistic: createMessageLogStatistic,
                });
            */
            function getProviders() {
                var request = $http({
                    method: 'GET',
                    url: apiUri + 'Participant/GetParticipants'
                    //url: apiUri + "UncreatedStatistics/GetParticipants"
            });
                return (request.then(handleResponseService.handleSuccess));
            }

            function getServicePaged(pageIndex, itemsPerPage,providerCode,pickProvider) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "Service/GetServiceListPaged",
                    params: {
                        pageIndex: pageIndex,
                        itemsPerPage: itemsPerPage,
                        providerCode: providerCode,
                        pickProvider: pickProvider
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
            }

            function getWSDL(providerCode, serviceCode) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "Service/GetWSDL",
                    params: {
                        
                        providerCode: providerCode,
                        serviceCode: serviceCode
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
            }


        }])
.service('sharedPropertiesServiceServices',function() {
    
    var parameters = [];

    return ({
        getParameters: getParameters,
        setParameters: setParameters
    });

    function getParameters() {
        return parameters;
    }

    function setParameters(itemsPerPage, currentPage,providerCode,pickProvider) {
        parameters.itemsPerPage = itemsPerPage;
        parameters.currentPage = currentPage;
        parameters.providerCode = providerCode;
        parameters.pickProvider = pickProvider;
  }


})
    .controller("ServiceCtrl", ['$scope', 'serviceService', 'servicePageList', '$stateParams', '$state', 'sharedPropertiesServiceServices',
        function ($scope, serviceService, servicePageList, $stateParams, $state, sharedPropertiesServiceServices) {

            // Sort
            $scope.sortType = 'name';
            $scope.sortReverse = false;
            //Sort end

            var param = $stateParams;
            $scope.currentPage = parseInt(param.pageIndex);
            $scope.services = servicePageList.items;
            $scope.itemsPerPage = servicePageList.pageSize;
            $scope.totalItems = servicePageList.totalSize;
            $scope.pageSize = servicePageList.pageSize;
            $scope.search = {
                name: '',
                provider: '',
            };
            $scope.providerName = $stateParams.providerCode;
            $scope.search.name = $stateParams.providerCode;
            $scope.search.provider = $stateParams.pickProvider;
            

            $scope.selectedCode = null;
            $scope.selectedProviderCode = null;

            $scope.setSelectedRow = function (selectedCode, selectedProviderCode) {
                $scope.selectedCode = selectedCode;
                $scope.selectedProviderCode = selectedProviderCode;
            };
            
            $scope.pageChangeHandler = function(currentPage) {
                $state.go($state.current.name, { pageIndex: parseInt(currentPage), itemsPerPage: $scope.pageSize });
            };

            $scope.pageSizeChanged = function(itemsPerPage) {
                $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: parseInt(itemsPerPage) });
            };
           
            $scope.showWSDL = function (providerCode, serviceCode, itemsPerPage, currentPage) {
                sharedPropertiesServiceServices.setParameters(itemsPerPage, currentPage, $scope.search.name, $scope.search.provider);
                $state.go('wsdl', { providerCode: providerCode, serviceCode: serviceCode, serviceCode: serviceCode,providerCode: providerCode });                
            };


            //Get all providers that are offering services
            serviceService.getProviders()
                .then(function (response) {
                    $scope.providersList = response;
                });
            //Clear the search Form
            $scope.clearServiceSearch = function () {
                $scope.search.name = '';
                $scope.search.provider = '';
                $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: $scope.pageSize, providerCode: "", pickProvider: "" });
            };
            //Search by provider
            $scope.searchProvider = function (searchTerm) {
                $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: $scope.pageSize, providerCode: searchTerm.name, pickProvider: searchTerm.provider });
                
            };
            


        }])
    .controller("WSDLCtrl", ['$scope', '$state', 'wsdl', 'sharedPropertiesServiceServices', 'serviceService', '$stateParams',
        function ($scope, $state, wsdl, sharedPropertiesServiceServices, serviceService, $stateParams) {

         
            $scope.wsdl = wsdl;
            $scope.providerName = $stateParams.providerCode;
            $scope.serviceName = $stateParams.serviceCode;
            $scope.back = function() {
                $state.go("services", { pageIndex: sharedPropertiesServiceServices.getParameters().currentPage, itemsPerPage: sharedPropertiesServiceServices.getParameters().itemsPerPage, providerCode: sharedPropertiesServiceServices.getParameters().providerCode, pickProvider: sharedPropertiesServiceServices.getParameters().pickProvider });
            };
        }
    ]);
    
