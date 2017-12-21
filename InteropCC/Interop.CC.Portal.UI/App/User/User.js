angular.module("User", ['ngTable', 'ui.bootstrap'])
    .service('userService', ['$http', '$q', 'apiUri', 'handleResponseService',
        function ($http, $q, apiUri, handleResponseService) {


        }])
    .controller("UserCtrl", ['$scope', '$modal', 'userService',
        function ($scope, $modal, userService) {

        }])
    .controller("UserLeftCtrl", ['$scope', '$modal', 'userService',
        function ($scope, $modal, userService) {
        }])
    .controller("UserRightCtrl", ['$scope', '$modal', 'userService',
        function ($scope, $modal, userService) {
            $scope.columns = [
                {
                    firstName: "Kreso",
                    lastName: "Jurisic"
                },
                {
                    firstName: "Cvetan",
                    lastName: "Stoimenov"
                },
                {
                    firstName: "Daniel",
                    lastName: "Jovanovski"
                },
                {
                    firstName: "Vlatko",
                    lastName: "Miloshevski"
                },
                {
                    firstName: "Simona",
                    lastName: "Stojanovska"
                },
                {
                    firstName: "Panche",
                    lastName: "Trifunov"
                }
            ];
        }]);
