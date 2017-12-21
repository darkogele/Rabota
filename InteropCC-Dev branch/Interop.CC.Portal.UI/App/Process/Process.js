angular.module("Process", ['ngTable', 'ui.bootstrap'])
    .service('processService', ['$http', '$q', 'apiUri', 'handleResponseService',
        function ($http, $q, apiUri, handleResponseService) {
 
        }])
    .controller("ProcessCtrl", ['$scope', '$modal', 'processService', 'userService', 'ngTableParams', 'ngProgress',
        function ($scope, $modal, processService, userService, ngTableParams, ngProgress) {
            $scope.openTestModal = function (user) {
                var modalInstance = $modal.open({
                    templateUrl: 'App/Process/Modals/TestModal',
                    controller: 'modalTestModal',
                    backdrop: 'static'
                    //resolve: {
                    //    user: function () {
                    //        return user;
                    //    },
                    //    userGroups: function () {
                    //        return userGroupService.list().then(function (userGroups) {
                    //            return userGroups;
                    //        });
                    //    }
                    //}
                });

            };
        }])
    .controller("FirstProcessCtrl", ['$scope', '$modal', 'processService', 'userService', 'ngTableParams', 'ngProgress',
        function ($scope, $modal, processService, userService, ngTableParams, ngProgress) {
            $scope.process1 = ['Process 1', 'Done', '23.04.2015 12:55:45'];
        }])
    .controller("SecondProcessCtrl", ['$scope', '$modal', 'processService', 'userService', 'ngTableParams', 'ngProgress',
        function ($scope, $modal, processService, userService, ngTableParams, ngProgress) {
            $scope.process2 = ['Process 2', 'State 2', '24.04.2015 13:32:52'];
        }])
    .controller('modalTestModal', ['$scope', '$modalInstance', 'processService', 'alerts',
        function ($scope, $modalInstance, processService, alerts) {
            $scope.cancel = function () {
                $modalInstance.close('cancel');
            };
        }]);
