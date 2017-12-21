app.config(function ($stateProvider, appBaseUrl) {

    $stateProvider
        .state("namesurnameindex", {
            title: 'Име и презиме',
            controller: "InstitutionBCtrl",
            url: "/InstitutionB/namesurnameindex",
            templateUrl: 'App/InstitutionB/Templates/NameSurname'
        })
        .state("peopledataindex", {
            title: 'Податоци за пристутни',
            controller: "InstitutionBCtrl",
            url: "/institutionB/institutionBPeopleData",
            templateUrl: 'App/InstitutionB/Templates/PersonData'
        });
});

angular.module("InstitutionB", ['ui.bootstrap'] )
.service("institutionBService", ['$http', '$q', 'handleResponseService', 'apiUri',
    function ($http, $q, handleResponseService, apiUri) {
        return ({
            getNameAndSurname: getNameAndSurname,
            getPeopleData: getPeopleData
        });

        function getNameAndSurname(name, surname) {
            var request = $http({
                method: 'GET',
                url: apiUri + "InstitutionB/GetNameSurname",
                params: {
                    name: name,
                    surname: surname
                }
            });
            return (request.then(handleResponseService.handleSuccess));
        }
        
        function getPeopleData(showData) {
            var request = $http({
                method: 'GET',
                url: apiUri + "InstitutionB/GetPersonData",
                params: {
                    showData: showData
                }
            });
            return (request.then(handleResponseService.handleSuccess));
        }
}])
.controller("InstitutionBCtrl", ['$scope', 'institutionBService', '$state', 'alerts',
function ($scope, institutionBService) {
    $scope.show = false;
    $scope.hasResult = false;

    $scope.getNameAndSurname = function (name, surname) {
        $scope.show = false;
        institutionBService.getNameAndSurname(name, surname)
            .then(function (response) {
                $scope.result = response;
                $scope.show = true;
            });
    };

    $scope.getPeopleData = function (showData) {
        $scope.show = false;
        institutionBService.getPeopleData(showData)
            .then(function(response) {
                $scope.people = response;
                $scope.show = true;
            });
    };
}]);