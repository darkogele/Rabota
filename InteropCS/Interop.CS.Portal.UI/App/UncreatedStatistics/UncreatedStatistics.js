app.config(function ($stateProvider, appBaseUrl) {

    $stateProvider
        .state("uncreatedstatistics", {
            title: 'Неискреирани логови',
            controller: "UncreatedStatisticsCtrl",
            url: "/uncreatedStatistics",
            templateUrl: appBaseUrl + "Module/UncreatedStatistics"
        });

});

angular.module("UncreatedStatistics", ['ui.bootstrap'])
    .service("uncreatedStatisticsService", ['$http', 'handleResponseService', 'apiUri',
        function ($http, handleResponseService, apiUri) {
            return ({
                getParticipants: getParticipants,
                createMessageLogStatistic: createMessageLogStatistic,
            });

            function getParticipants() {
                var request = $http({
                    method: 'GET',
                    url: apiUri + "UncreatedStatistics/GetParticipants"
                });
                return (request.then(handleResponseService.handleSuccess));
            }
            
            function createMessageLogStatistic(selectedParticipant, forDate) {
                var request = $http({
                    method: "POST",
                    params: {
                        selectedParticipant: selectedParticipant,
                        forDate: forDate
                    },
                    url: apiUri + "UncreatedStatistics/CreateMessageLogStatistic"
                });
                return (request.then(handleResponseService.handleSuccess));
            }
        }
    ])
    .controller("UncreatedStatisticsCtrl", ['$scope', 'uncreatedStatisticsService', '$state', 'alerts',
        function ($scope, uncreatedStatisticsService, $state, alerts) {
            
            $scope.statusFrom = {
                opened: false
            };
            
            if ($scope.forDate === undefined) {
                var todayDate = new Date();
                $scope.forDate = new Date(todayDate.getFullYear(), todayDate.getMonth(), todayDate.getDate() - 1);
            }
            
            $scope.openFrom = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.statusFrom.opened = true;
            };

            uncreatedStatisticsService.getParticipants()
                .then(function (response) {
                    $scope.participantsList = response;
                });

            $scope.createMessageLogStatistic = function(selectedParticipant, forDate) {
                uncreatedStatisticsService.createMessageLogStatistic(selectedParticipant.toUpperCase(), forDate).then(function (responseMsg) {
                    alerts.addInfo(responseMsg);
                    //if (responseMsg.indexOf("Неуспешно") > -1) {
                    //    alerts.addError(responseMsg);
                    //} else {
                    //    alerts.addSuccess(responseMsg);
                    //}
                    $scope.clear();
                });
            };
            
            $scope.clear = function () {
                $scope.selectedParticipant = "";
                $scope.forDate = new Date(todayDate.getFullYear(), todayDate.getMonth(), todayDate.getDate() - 1);
            };


        }]);

