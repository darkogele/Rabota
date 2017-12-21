app.config(function ($stateProvider, appBaseUrl) {
    $stateProvider
        .state('services', {
            title: 'Сервиси',
            controller: 'ServiceCtrl',
            url: '/services/{pageIndex:int}/{itemsPerPage:int}/{filterCode:string}/{filterName:string}/{sortDir:string}/{sortCol:string}',
            templateUrl: appBaseUrl + 'Module/Service',
            resolve: {
                servicePageList: ['serviceService', '$stateParams',
                    function (serviceService, $stateParams) {
                        return serviceService.getServicePaged($stateParams.pageIndex, $stateParams.itemsPerPage, $stateParams.filterCode, $stateParams.filterName, $stateParams.sortDir, $stateParams.sortCol);
                    }]
            }
        })
        .state('wsdl', {
            title: "WSDL",
            controller: 'WSDLCtrl',
            url: '/serviceWSDL/:serviceCode',
            templateUrl: 'App/Service/Templates/ShowWSDL',
            resolve: {
                wsdl: ['serviceService', '$stateParams',
                    function (serviceService, $stateParams) {
                        return serviceService.GetWSDL($stateParams.serviceCode);

                    }]
            }
        });

});

angular.module("Service", ['ngTable', 'ui.bootstrap'])
.service('serviceService', ['$http', '$q', 'apiUri', 'handleResponseService',
  function ($http, $q, apiUri, handleResponseService) {
      return ({

          getServiceList: getServiceList,
          getServicePaged: getServicePaged,
          GetWSDL: GetWSDL
      });

      function getServiceList() {
          var request = $http({
              method: "GET",
              url: apiUri + "Service/GetServiceList"
          });
          return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
      }

      function getServicePaged(pageIndex, itemsPerPage, filterCode, filterName, sortDir, sortCol) {
          var request = $http({
              method: "GET",
              url: apiUri + "Service/GetServiceListPaged",
              params: {
                  pageIndex: pageIndex,
                  itemsPerPage: itemsPerPage,
                  filterCode: filterCode,
                  filterName: filterName,
                  sortDir: sortDir,
                  sortCol: sortCol
              }
          });
          return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
      }
      function GetWSDL(serviceCode) {
          var request = $http({
              method: "GET",
              url: apiUri + "Service/GetWSDL",
              params: {
                  serviceCode: serviceCode
              }
          });
          return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
      }


  }])

    .service('sharedPropertiesServiceServices', function () {

        var parameters = [];

        return ({
            getParameters: getParameters,
            setParameters: setParameters,
        });

        function getParameters() {
            return parameters;
        }

        function setParameters(itemsPerPage, currentPage,filterName,filterCode,sortDir,sortCol) {
            parameters.itemsPerPage = itemsPerPage;
            parameters.currentPage = currentPage;
            parameters.filterName = filterName;
            parameters.filterCode = filterCode;
            parameters.sortDir = sortDir;
            parameters.sortCol = sortCol;
        }


    })

.controller("ServiceCtrl", ['$scope', 'ngTableParams', 'ngProgress', 'serviceService', 'servicePageList', '$stateParams', '$state', 'sharedPropertiesServiceServices',
function ($scope, ngTableParams, ngProgress, serviceService, servicePageList, $stateParams, $state, sharedPropertiesServiceServices) {

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
        $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: $scope.itemsPerPage, filterCode: $scope.filterCode, filterName: $scope.filterName, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
    };

    $scope.itemsPage = [
        { itemId: 1, itemName: '1' },
        { itemId: 3, itemName: '3' },
        { itemId: 5, itemName: '5' },
        { itemId: 7, itemName: '7' },
        { itemId: 10, itemName: '10' }
    ];

    $scope.filterCode = param.filterCode;
    $scope.filterName = param.filterName;


    $scope.filterHandler = function () {
        $state.go($state.current.name, { pageIndex: parseInt(1), itemsPerPage: $scope.itemsPerPage, filterCode: $scope.filterCode, filterName: $scope.filterName, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
    };
    $scope.filterClear = function () {
        $scope.filterCode = "";
        $scope.filterName = "";
        $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: $scope.itemsPerPage, filterCode: $scope.filterCode, filterName: $scope.filterName, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
    };

    $scope.pageChangeHandler = function (currentPage) {
        $state.go($state.current.name, { pageIndex: parseInt(currentPage), itemsPerPage: $scope.itemsPerPage, filterCode: $scope.filterCode, filterName: $scope.filterName, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
    };

    $scope.pageSizeChanged = function (itemsPerPage) {
        $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: parseInt(itemsPerPage), filterCode: $scope.filterCode, filterName: $scope.filterName, sortDir: $scope.sortDir, sortCol: $scope.sortCol });
    };

    $scope.showWSDL = function (serviceCode, itemsPerPage, currentPage,filterName,filterCode,sortDir,sortCol) {

        sharedPropertiesServiceServices.setParameters(itemsPerPage, currentPage, filterName, filterCode, sortDir, sortCol);
        $state.go('wsdl', { serviceCode: serviceCode });

    };

}])
.controller("WSDLCtrl", ['$scope', '$state', 'wsdl', 'sharedPropertiesServiceServices', 'serviceService', '$stateParams',
        function ($scope, $state, wsdl, sharedPropertiesServiceServices, serviceService, $stateParams) {


            $scope.wsdl = wsdl;
            $scope.serviceName = $stateParams.serviceCode;

            
            
            $scope.back = function () {
                
                $state.go("services", { pageIndex: sharedPropertiesServiceServices.getParameters().currentPage, itemsPerPage: sharedPropertiesServiceServices.getParameters().itemsPerPage, filterName: sharedPropertiesServiceServices.getParameters().filterName, filterCode: sharedPropertiesServiceServices.getParameters().filterCode, sortDir: sharedPropertiesServiceServices.getParameters().sortDir, sortCol: sharedPropertiesServiceServices.getParameters().sortCol });

            };

        }
]);