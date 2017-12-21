
app.config(function ($stateProvider, appBaseUrl) {

    $stateProvider
        .state("registerservice", {
            title: 'Регистрирање на сервис',
            controller: "MetaServiceCtrl",
            url: "/metaservices/registerservice",
            templateUrl: 'App/MetaService/Templates/RegisterService'
        })
        .state("providerslist", {
            title: 'Листа на провајдери',
            controller: "MetaServiceProviderCtrl",
            url: "/metaservices/providerslist/{pageIndex:int}/{itemsPerPage:int}/{sortDir:string}/{sortCol:string}/{providerCode:string}",
            templateUrl: 'App/MetaService/Templates/ProvidersList',
            resolve: {
                providers: ['metaservicesService', 'authService', 'authInterceptorService', '$stateParams', function (metaservicesService, authService, authInterceptorService, $stateParams) {
                    
                    var loggedUser = authService.userWithRoles().objUserName;
                    //var loggedUserServicesRoles = authService.userWithRoles().objUserRoles.join();
                    
                    return metaservicesService.getProvidersListPaged($stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.sortDir, $stateParams.sortCol, $stateParams.providerCode).then(function (response) {
                        var providerCodes = [];
                        var lstProviders = response.data.items;
                        for (var i = 0; i < lstProviders.length; i++) {
                            providerCodes.push(lstProviders[i].code);
                        }
                        return metaservicesService.getServicesRolesAfterGetProviders(loggedUser, providerCodes).then(function (responseServiceRoles) {
                            var serviceRolesAfterRebind = responseServiceRoles;
                            authService.rebindRolesAfterGetProviders(serviceRolesAfterRebind);
                            return response.data;
                        });
                    });
                }]
            }
        })
        .state('ProvidersListServiceList', {
            title: "MetaServiceProvListServList",
            controller: 'MetaServiceProvListServList',
            url: '/metaservices/providerslist/servicelist/:providerCode',
            templateUrl: 'App/MetaService/Templates/ProvidersListServiceList',
            resolve: {
                service: ['metaservicesService', '$stateParams',
                    function (metaservicesService, $stateParams) {
                        return metaservicesService.getServicesList($stateParams.providerCode);

                    }]
            }
        })
        .state('ProvidersListServiceListWSDLdetails', {
            title: "MetaServiceProvListServListWSDLdet",
            controller: 'MetaServiceProvListServListWSDLdet',
            url: '/metaservices/providerslist/servicelist/:providerCode/:providerService',
            templateUrl: 'App/MetaService/Templates/ProvidersListServiceListWSDLDetails',
            resolve: {
                wsdl: ['metaservicesService', '$stateParams',
                    function (metaservicesService, $stateParams) {
                        return metaservicesService.getServiceWsdl($stateParams.providerCode, $stateParams.providerService);

                    }]
            }
        })
        .state("serviceslist", {
            title: 'Листа на сервиси',
            controller: "MetaServiceCtrl",
            url: "/metaservices/cerviceslist",
            templateUrl: 'App/MetaService/Templates/ServicesList'
        })
        .state("getservice", {
            title: 'Сервис',
            controller: "MetaServiceCtrl",
            url: "/metaservices/service",
            templateUrl: 'App/MetaService/Templates/GetService'
        });
});

angular.module("MetaService", ['ui.bootstrap'])
.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}])
    .service("metaservicesService", ['$http', '$q', 'handleResponseService', 'apiUri', 'authInterceptorService',
    function ($http, $q, handleResponseService, apiUri, authInterceptorService) {
        return ({
            registerService: registerService,
            getprovidersList: getprovidersList,
            getProvidersListPaged: getProvidersListPaged,
            getServicesForRoles: getServicesForRoles,
            getServiceWsdl: getServiceWsdl,
            getServicesList: getServicesList,
            uploadWSDLfile: uploadWSDLfile,
            downloadWsdlfile: downloadWsdlfile,
            getServicesRolesAfterGetProviders: getServicesRolesAfterGetProviders
        });

        function getServicesRolesAfterGetProviders(username, providerCodes) {
            var request = $http({
                method: 'GET',
                url: apiUri + "MetaServices/GetServiceRoles",
                params: {
                    username: username,
                    providerCodes: providerCodes
                }
                
            });
            //return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
            return request;
        }

       function downloadWsdlfile(providerCode, serviceCode) {
            var request = $http({
                method: "GET",
                url: apiUri + "MetaServices/DownloadWsdl",
                responseType: "arraybuffer",
                params: {
                    providerCode: providerCode,
                    serviceCode: serviceCode,
                    callType: "test"

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }

        function uploadWSDLfile(file) {
            var fd = new FormData();
            fd.append('file', file);
            var uploadUrl = apiUri + "MetaServices/Upload";
            var request = $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function registerService(registerervice) {
            var request = $http({
                method: 'POST',
                data: registerervice,
                url: apiUri + "MetaServices/ApiRegisterService",
                params: {
                    Name: registerervice.Name,
                    Wsdl: registerervice.Wsdl
                }
           
            });
            return (request.then(handleResponseService.handleSuccess, authInterceptorService._responseError));
        }
      
        function getprovidersList() {
            var request = $http({
                method: 'GET',
                url: apiUri + "MetaServices/ApiGetProviders"
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }

        function getServicesForRoles() {
            var request = $http({
                method: 'GET',
                url: apiUri + "MetaServices/GetServicesForRoles",
            });
            return (request.then(handleResponseService.handleSuccess));
        }
        
        function getProvidersListPaged(pageIndex, itemsPerPage, sortDir, sortCol, providerCode) {
            var request = $http({
                method: 'GET',
                url: apiUri + "MetaServices/GetProvidersPaged",
                params: {
                    pageIndex: pageIndex,
                    itemsPerPage: itemsPerPage,
                    sortDir: sortDir,
                    sortCol: sortCol,
                    providerCode: providerCode
                }
            });
            return request;
            //(request.then(handleResponseService.handleSuccess));
            //, handleResponseService.handleError
        }
        function getServiceWsdl(provider, serviceId) {
            var request = $http({
                method: 'GET',
                url: apiUri + "MetaServices/ApiGetService",
                params: {
                    ProviderCode: provider,
                    ServiceCode: serviceId,
                    calltype: "test"
                }

            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }

        function getServicesList(provider) {
            var request = $http({
                method: 'GET',
                url: apiUri + "MetaServices/ApiGetServices",
                params: {
                    ProviderCode: provider
                }

            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
    }])
    .service('sharedPropertiesService', function () {

        var parameters = [];

        return ({
            getParameters: getParameters,
            setParameters: setParameters,
        });

        function getParameters() {
            return parameters;
        }

        function setParameters(currentPage, itemsPerPage, sortDir, sortCol,providerCode) {
            parameters.itemsPerPage = itemsPerPage;
            parameters.currentPage = currentPage;
            parameters.sortDir = sortDir;
            parameters.sortCol = sortCol;
            parameters.providerCode = providerCode;
        }


    })

.controller("MetaServiceCtrl", ['$scope', 'metaservicesService', '$state', 'alerts',
function ($scope, metaservicesService, $state, alerts) {

    $scope.wsdlservice = '';
    

    $scope.registerservice = {
        form: {},
        newservice: '',
        wsdl: '',      

        SaveService: function (registerervice) {
            var objectToSave = { Name: registerervice.newservice, Wsdl: registerervice.wsdl };
           
            metaservicesService.registerService(objectToSave).then(function (response) {
                alerts.addSuccess("Успешно е регистриран нов сервис");
                $state.go("services", { pageIndex: 1, itemsPerPage: 5, filterCode: "", filterName: "" });
                
            }, function (error) {
                alerts.addError(error.message);
            });
        }
    };

    $(document).ready(
    function () {
        $('input:file').change(
            function () {
                if ($(this).val()) {
                    $('a').attr('disabled', false);
                   
                }
            }
            );
    });
    $scope.uploadWSDLfile = function () {
        var file = $scope.myFile;
        $scope.registerservice.wsdl = "";
        var substring = ".wsdl";
        metaservicesService.uploadWSDLfile(file).then(function (result) {
            //if (file.name.contains(substring)) {
            //    if (result) {
                    alerts.addSuccess("Датотеката е успешно прикачена");
            $scope.registerservice.wsdl = result;
            //    } else {
            //        alerts.addError("Датотеката не може да биде успешно прикачена");
            //    }
            //} else {
            //    alerts.addError("Датотеката мора да биде од тип .wsdl");
            //}
            
           
           
        });
    };


    $scope.showWsdl = function (provider, service) {
        metaservicesService.getServiceWsdl(provider, service).then(function (response) {
            $scope.wsdlservice = response;
        });
    };

    $scope.getServicesList = function (provider) {

        metaservicesService.getServicesList(provider).then(function (response) {
            $scope.services = response;
        });
    };
}])

.controller("MetaServiceProviderCtrl", ['$scope', 'metaservicesService', 'interopTestCommunicationService', '$state', 'alerts', '$stateParams', 'providers', 'sharedPropertiesService',
function ($scope, metaservicesService, interopTestCommunicationService, $state, alerts, $stateParams, providers, sharedPropertiesService) {

    $scope.showServicesList = false;
    $scope.showWsdlText = false;
    $scope.sortDir = $stateParams.sortDir;
    $scope.sortCol = $stateParams.sortCol;
    $scope.providers = providers.items;
    $scope.itemsPerPage = providers.pageSize;
    $scope.totalItems = providers.totalSize;
    $scope.pageSize = providers.pageSize;
    $scope.currentPage = parseInt($stateParams.pageIndex);
    $scope.filterProviderName = $stateParams.providerCode;

    $scope.servicesRoles = [];

    //metaservicesService.getprovidersList().then(function (response) {

    //    $scope.participants = response;
    //    $scope.totalItems = response;
    //});
    


    $scope.refreshprovidersList = function () {
        metaservicesService.getprovidersList().then(function (response) {
            $scope.providers = response;
        });
        $scope.filterProviderName = "";
        $state.go('providerslist', { pageIndex: $scope.currentPage, itemsPerPage: $scope.itemsPerPage, sortDir: $scope.sortDir, sortCol: $scope.sortCol, providerCode: $scope.filterProviderName });
        //$state.transitionTo($state.current, $stateParams, { reload: true, inherit: false, notify: true });
    };

    $scope.itemsPage = [
        { itemId: 1, itemName: '1' },
        { itemId: 3, itemName: '3' },
        { itemId: 5, itemName: '5' },
        { itemId: 7, itemName: '7' },
        { itemId: 10, itemName: '10' }
    ];

    $scope.interopTestCommunication = function(providerRoutingToken) {
        interopTestCommunicationService.testCommunication(providerRoutingToken)
            .then(function(response) {
                if (JSON.parse(response) == true) {
                    alerts.addSuccess('Успешна комуникација со провајдерот со код: ' + providerRoutingToken);
                } else {
                    alerts.addError('Неуспешна комуникација со провајдерот со код:' + providerRoutingToken);
                }
            });
    };
    $scope.filterHandler = function(searchTerm) {
        $state.go('providerslist', { pageIndex: $scope.currentPage, itemsPerPage: $scope.itemsPerPage, sortDir: $scope.sortDir, sortCol: $scope.sortCol, providerCode: searchTerm });
    };
    $scope.filterClear = function() {
        $scope.filterProviderName = "";
        $state.go('providerslist', { pageIndex: $scope.currentPage, itemsPerPage: $scope.itemsPerPage, sortDir: $scope.sortDir, sortCol: $scope.sortCol, providerCode: $scope.filterProviderName });
    };
    $scope.pageChangeHandler = function(currentPage) {
        $state.go("providerslist", { pageIndex: parseInt(currentPage), itemsPerPage: $scope.itemsPerPage, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
    };

    $scope.pageSizeChanged = function(itemsPerPage) {
        $state.go("providerslist", { pageIndex: $scope.currentPage, itemsPerPage: parseInt(itemsPerPage), sortDir: $scope.sortDir, sortCol: $scope.sortCol });
    };
    
    $scope.getServicesList = function (providerCode) {
        //metaservicesService.getServicesList(provider).then(function (response) {
        //    $scope.showServicesList = true;
        //    $scope.providerCode = provider;
        //    $scope.servicesList = response;
        //});
        sharedPropertiesService.setParameters($scope.currentPage, $scope.itemsPerPage, $scope.sortDir, $scope.sortCol, $scope.filterProviderName);
        $state.go('ProvidersListServiceList', { providerCode: providerCode });
    };
    $scope.sort = function(columnName) {
        if (columnName == $scope.sortCol) {
            if ($scope.sortDir == "asc") {
                $scope.sortDir = "desc";
            } else {
                $scope.sortDir = "asc";
            }
        } else {
            $scope.sortCol = columnName;
        }
        $state.go('providerslist', { pageIndex: $scope.currentPage, itemsPerPage: $scope.itemsPerPage, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
    };
}])
.controller("MetaServiceProvListServList", ['$scope', '$state', 'service', 'sharedPropertiesService',
        function ($scope, $state, service, sharedPropertiesService) {

            $scope.service = service;
            $scope.providerCode = $state.params.providerCode;
            $scope.showWsdl = function (providerService, providerCode) {
                //sharedPropertiesService.setParameters(parseInt($stateParams.pageIndex), $scope.itemsPerPage, $scope.sortDir, $scope.sortCol);

                //metaservicesService.getServiceWsdl($scope.providerCode, service).then(function (response) {
                //    $scope.showWsdlText = true;
                //    $scope.wsdlservice = response;
                //});
                $state.go('ProvidersListServiceListWSDLdetails', {
                    providerCode: $scope.providerCode, providerService: providerService
                    });
            };
            $scope.showProviders = function() {
                $state.go('providerslist', { pageIndex: sharedPropertiesService.getParameters().currentPage, itemsPerPage: sharedPropertiesService.getParameters().itemsPerPage, sortDir: sharedPropertiesService.getParameters().sortDir, sortCol: sharedPropertiesService.getParameters().sortCol, providerCode: sharedPropertiesService.getParameters().providerCode });
            };
        }
])
.controller("MetaServiceProvListServListWSDLdet", ['$scope', '$state', 'wsdl', 'sharedPropertiesService', 'metaservicesService',
        function ($scope, $state, wsdl, sharedPropertiesService, metaservicesService) {

            $scope.wsdlservice = wsdl;
            $scope.providerService = $state.params.providerService;
            $scope.providerCode = $state.params.providerCode;
            
            $scope.showWsdl = function (providerService) {
                //metaservicesService.getServiceWsdl($scope.providerCode, service).then(function (response) {
                //    $scope.showWsdlText = true;
                //    $scope.wsdlservice = response;
                //});
                var providerCode = $state.params.providerCode;
                $state.go('ProvidersListServiceList', { providerCode: providerCode });
            };

            $scope.showProviders = function() {
                $state.go('providerslist', { pageIndex: sharedPropertiesService.getParameters().currentPage, itemsPerPage: sharedPropertiesService.getParameters().itemsPerPage, sortDir: sharedPropertiesService.getParameters().sortDir, sortCol: sharedPropertiesService.getParameters().sortCol, providerCode: sharedPropertiesService.getParameters().providerCode });
            };

            $scope.downloadWsdl = function (providerCode, providerService) {
                var savedXmlname = providerCode + "_" + providerService + ".wsdl";
                metaservicesService.downloadWsdlfile(providerCode, providerService).then(function (result) {
                    if (result) {
                        var blob = new Blob([result], { type: "application/xml" });
                        navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                        if (navigator.saveBlob) {
                            navigator.saveBlob(blob, savedXmlname);
                        } else {
                            saveBlob(blob, savedXmlname);
                        }
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

        }
]);