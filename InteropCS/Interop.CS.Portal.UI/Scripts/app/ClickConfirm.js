angular.module('ngClickConfirmModule', ['ui.bootstrap'])
  .directive('ngClickConfirm', ['$modal',
    function ($modal) {

        var ModalInstanceClickConfirmCtrl = function ($scope, $modalInstance) {
            $scope.ok = function () {
                $modalInstance.close();
            };

            $scope.cancel = function () {
                $modalInstance.dismiss('cancel');
            };
        };

        return {
            restrict: 'A',
            scope: {
                ngClickConfirm: "&"
            },
            link: {
                pre: function (scope, element, attrs) {
                    element.bind('click', function () {
                        var message = attrs.ngClickConfirmMessage || "Are you sure ?";

                        var modalHtml =
                            '<div class="modal-body"><h4>' + message + '</h4></div>' +
                            '<div class="modal-footer">' +
                                '<button class="btn btn-success" ng-click="ok()">Да</button>' +
                                '<button class="btn btn-warning" ng-click="cancel()">Не</button>' +
                            '</div>';

                        var modalInstance = $modal.open({
                            template: modalHtml,
                            controller: ModalInstanceClickConfirmCtrl,
                            size: "sm",
                            backdrop: 'static'
                        });

                        modalInstance.result.then(function () {
                            scope.ngClickConfirm();
                        }, function () {
                            //Modal dismissed
                        });

                    });
                }
            }
        };
    }
  ]);