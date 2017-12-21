app.controller("InstitutionACtrl", ["$scope", "institutionAService", function ($scope, institutionAService) {
    $scope.requestFinished = true;
    $scope.getPropertyList = function () {
        $scope.isLoading = true;
        $scope.requestFinished = false;
        institutionAService.getPropertyList()
            .then(function(response) {
                $scope.result = response;
                },
                function(error) {
                    $scope.errorMsg = error.data.ExceptionMessage;
                    $scope.errorStatus = error.status;
                }).finally(function () {
                    $scope.isLoading = false;
                    $scope.requestFinished = true;
            });
    };
}]);
