app.config(function ($stateProvider, appBaseUrl) {
    $stateProvider
        .state('buses', {
            title: 'Екстерни басови',
            controller: 'BusesCtrl',
            url: '/buses/{pageIndex:int}/{itemsPerPage:int}',
            templateUrl: appBaseUrl + 'Module/Buses',
            resolve: {
                busesPageList: ['busesService', '$stateParams',
                    function (busesService, $stateParams) {
                        return busesService.getBusesPaged($stateParams.pageIndex, $stateParams.itemsPerPage);
                    }]
            }
        })
        .state('createbus', {
            title: 'Креирај запис во екстерни басови',
            controller: 'CreateBusCtrl',
            url: '/buses/new',
            templateUrl: 'App/Buses/Templates/CreateBus'
        });
});

angular.module("Buses", ['ngTable', 'ui.bootstrap'])
    .service('busesService', ['$http', '$q', 'apiUri', 'handleResponseService',
        function($http, $q, apiUri, handleResponseService) {

            return ({
                getBusesPaged: getBusesPaged,
                createBus: createBus
            });

            function getBusesPaged(pageIndex, itemsPerPage) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "Buses/GetBusesPaged",
                    params: {
                        pageIndex: pageIndex,
                        itemsPerPage: itemsPerPage
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
            }

            function createBus(bus) {

                var request = $http({
                    method: "POST",
                    data: bus,
                    url: apiUri + "Buses/CreateBus"
                });
                return (request.then(handleResponseService.handleSucces//, handleResponseService.handleError
              ));
            }

        }])

    .controller("BusesCtrl", ['$scope', '$state', 'busesService', 'busesPageList', '$stateParams',
        function($scope, $state, busesService, busesPageList, $stateParams) {

            // Sort
            $scope.sortType = 'code';
            $scope.sortReverse = false;
            //Sort end

            $scope.search = [];

            var param = $stateParams;
            $scope.currentPage = parseInt(param.pageIndex);
            $scope.buses = busesPageList.items;
            $scope.itemsPerPage = busesPageList.pageSize;
            $scope.totalItems = busesPageList.totalSize;
            $scope.pageSize = busesPageList.pageSize;

            $scope.setSelectedRow = function(selectedCode) {
                $scope.selectedCode = selectedCode;
            };

            $scope.pageChangeHandler = function(currentPage) {
                $state.go($state.current.name, { pageIndex: parseInt(currentPage), itemsPerPage: $scope.pageSize });
            };

            $scope.pageSizeChanged = function(itemsPerPage) {
                $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: parseInt(itemsPerPage) });
            };
            
        }])
    
    .controller("CreateBusCtrl", ['$scope', 'busesService', '$state', 'alerts',
function ($scope, busesService, $state, alerts) {


    $scope.saveBus = function(bus) {
        var code = $scope.bus.code;
        busesService.createBus(bus)
            .then(function() {
                alerts.addSuccess("Креиран е екстерен бас со код '" + bus.code + "'");
                $state.go("buses", { pageIndex: 1, itemsPerPage: 10 });
            });
    };
}]);