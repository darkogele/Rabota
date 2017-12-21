app.config(function ($stateProvider, appBaseUrl) {

    $stateProvider
        .state("messagelogerror", {
            title: 'Неправилни логови',
            controller: "MessageLogErrorCtrl",
            url: "/messagelogError",
            templateUrl: appBaseUrl + "Module/MessageLogError"
        });

});

angular.module("MessageLogError", ['ui.bootstrap'])
    .service("messageLogErrorService", ['$http', 'handleResponseService', 'apiUri',
        function ($http, handleResponseService, apiUri) {
            return ({
                createMessageLogForResponse: createMessageLogForResponse,
                createMessageLogStatistic: createMessageLogStatistic
            });
            function createMessageLogForResponse() {
                var request = $http({
                    method: "POST",
                    url: apiUri + "MessageLogError/CreateMessageLogForResponse"
                });
                return (request.then(handleResponseService.handleSuccess));
            }
            
            function createMessageLogStatistic() {
                var request = $http({
                    method: "POST",
                    url: apiUri + "MessageLogError/CreateMessageLogStatistic"
                });
                return (request.then(handleResponseService.handleSuccess));
            }
        }
    ])
    .controller("MessageLogErrorCtrl", ['$scope', 'messageLogErrorService', '$state', 'alerts',
        function ($scope, messageLogErrorService, $state, alerts) {

            $scope.createMessageLogForResponse = function () {
                messageLogErrorService.createMessageLogForResponse().then(function (responseMsg) {
                    $scope.transactions = responseMsg.split(";");
                    alerts.addInfo("Искреирани се Response-и за следниве трансакции: " + responseMsg + "<br/>");
                });
            };
            
            $scope.createMessageLogStatistic = function () {
                messageLogErrorService.createMessageLogStatistic().then(function (responseMsg) {
                    $scope.transactions = responseMsg.split(";");
                    alerts.addInfo("Искреирани се Request-и/Response-и за следниве трансакции: " + responseMsg + "<br/>");
                });
            };

        }]);

