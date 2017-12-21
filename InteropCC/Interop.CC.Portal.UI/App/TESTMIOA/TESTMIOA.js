app.config(function ($stateProvider, appBaseUrl) {

    $stateProvider
        .state("testmioagetfullnameindex", {
            title: 'Цело име',
            controller: "TestmioaCtrl",
            url: "testmioa/testmioagetfullnameindex",
            templateUrl: 'App/TESTMIOA/Templates/TestMIOAGetFullName'
        })
        .state("testmioagetfullname", {
            title: 'Цело име',
            controller: "TestmioaCtrl",
            url: "testmioa/testmioagetfullname",
            templateUrl: 'App/TESTMIOA/Templates/TestMIOAGetFullName'
        })
    .state("testmioagetsumindex", {
        title: 'Сума на броеви',
        controller: "TestmioaCtrl",
        url: "testmioa/testmioagetsumindex",
        templateUrl: 'App/TESTMIOA/Templates/TestMIOAGetSum'
    })
        .state("testmioagetsum", {
            title: 'Сума на броеви',
            controller: "TestmioaCtrl",
            url: "testmioa/testmioagetsum",
            templateUrl: 'App/TESTMIOA/Templates/TestMIOAGetSum'
        });
       
});

angular.module("TESTMIOA", ['ui.bootstrap'])
.service("testmioaService", ['$http', '$q', 'handleResponseService', 'apiUri',
    function ($http, $q, handleResponseService, apiUri) {
        return ({
            getFullName: getFullName,
            getSum: getSum
        });

        function getFullName(firstName, lastName) {
            var request = $http({
                method: 'POST',
                url: apiUri + "TESTMIOA/GetFullName",
                params: {
                    firstName: firstName,
                    lastName: lastName
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getSum(first, second) {
            var request = $http({
                method: 'POST',
                url: apiUri + "TESTMIOA/GetSumOfNums",
                params: {
                    first: first,
                    second: second
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }

    }])

.controller("TestmioaCtrl", ['$scope', 'testmioaService', '$state', 'alerts',
function ($scope, testmioaService, $state, alerts) {
    $scope.show = false;
    $scope.testmioaFullName = function (firstName, lastName) {
        testmioaService.getFullName(firstName, lastName)
            .then(function (response) {
                if (response == "") {
                    $scope.show = false;
                    alerts.addError("Сервисот е моментално недостапен");
                } else {
                    $scope.resp = response;
                    $scope.show = true;
                }

            });
    };
    $scope.testmioaSum = function (first, second) {
        testmioaService.getSum(first, second)
            .then(function (response) {
                if (response == "") {
                    $scope.show = false;
                    alerts.addError("Сервисот е моментално недостапен");
                } else {
                    $scope.resp = response;
                    $scope.show = true;
                }

            });
    };

   

   
    //$scope.monStatusForStudent = function (embg) {
    //    monService.getStatusForStudent(embg)
    //        .then(function (response) {
    //            if (response == "") {
    //                $scope.show = false;
    //                alerts.addError("Сервисот е моментално недостапен");
    //            } else {
    //                $scope.resp = response;
    //                $scope.show = true;
    //            }
    //        });
    //};
}])

;