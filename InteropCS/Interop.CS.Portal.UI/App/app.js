
app.config(function ($stateProvider, $urlRouterProvider, appBaseUrl) {

    //$urlRouterProvider.otherwise('/login');

    $stateProvider
        .state('process', {
            controller: 'ProcessCtrl',
            url: '/process',
            templateUrl: appBaseUrl + 'Module/Process'
        })
        .state('messageLog', {
            controller: 'MessageLogCtrl',
            url: '/messageLog',
            templateUrl: appBaseUrl + 'Module/MessageLog'
        })
       
        .state('process.firstProcess', {
            controller: 'FirstProcessCtrl',
            url: '/firstProcess',
            templateUrl: 'App/Process/FirstProcess.html'
        })
        .state('process.secondProcess', {
            controller: 'SecondProcessCtrl',
            url: '/secondProcess',
            templateUrl: 'App/Process/SecondProcess.html'
        })
        .state('user', {
            url: '/user',
            views: {
                '': {
                    templateUrl: appBaseUrl + 'Module/User',
                    controller: 'UserCtrl'
                },
                'leftContentRow1@user': {
                    templateUrl: 'App/User/UserLeft.html',
                    controller: 'UserLeftCtrl'
                },
                'rightContentRow1@user': {
                    templateUrl: 'App/User/UserRight.html',
                    controller: 'UserRightCtrl'
                },
                'leftContentRow2@user': {
                    templateUrl: 'App/User/UserNumber.html',
                    controller: 'UserCtrl'
                },
            }
        })
        
        .state('user.NoUsers', {
            url: '/NoUsers=' + ':users',
            views: {
                '': {

                },
                'nested1@user.NoUsers': {
                    templateUrl: 'App/User/Nested1.html',
                    controller: 'UserCtrl'
                },
                'nested2@user.NoUsers': {
                    templateUrl: 'App/User/Nested2.html',
                    controller: 'UserCtrl'
                }
            },
        });

});

app.config(['ngToastProvider', function (ngToast) {
    ngToast.configure({
        maxNumber: 3,
        dismissButton: true,
        dismissOnTimeout: false,
        dismissOnClick: true,
        timeout: 10000,
        horizontalPosition: 'center',
        verticalPosition: 'center'
    });
}]);

app.run(["$rootScope", "$state", "$stateParams", function ($rootScope, $state, $stateParams) {
          $rootScope.$state = $state;
          return $rootScope.$stateParams = $stateParams;
      }
]);

app.controller("MainCtrl", ['$scope', '$http', '$location', '$rootScope', '$state', 'authService', '$injector',
    function ($scope, $http, $location, $rootScope, $state, authService, $injector) {

        $scope.closeToastMsg = "(клик на пораката за да исчезне)";
        
        $scope.logOut = function () {
            authService.logOut();
            var alerts = $injector.get("alerts");
            alerts.addSuccess("Успешно се одјавивте");
            //$location.path('/login');
        };
        
        $rootScope.$on('$stateChangeError', function (event, toState, toParams, fromState, fromParams, error) {
            event.preventDefault();
            //$scope.loading = false;
            //$state.get('error').error = { code: 123, description: 'Exception stack trace' }
            return $state.go(fromState);
        });

        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
            //$scope.loading = true;
            //console.log('$stateChangeStart');
        });

        $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
            //$scope.loading = false;
            $rootScope.pageTitle = toState.title;
            //console.log('stateChangeSuccess');
        });

        //$state.includes('participant*');

        $scope.menuClass = function (page) {
            //$state.is('participants');
            var current = $location.path().substring(1);
            return page === current ? "active" : "";
        };
		
		$scope.authentication = authService.authentication;

    }]);

app.service("alerts", ['ngToast',
    function (ngToast) {

        var api = {
            addWarning: function (msg) {
                ngToast.create({
                    content: msg,
                    className: 'warning'
                });
            },
            addError: function (msg) {
                ngToast.create({
                    content: msg,
                    className: 'danger'
                });
            },
            addSuccess: function (msg) {
                ngToast.create({
                    content: msg,
                    className: 'success'
                });
            },
            addInfo: function (msg) {
                ngToast.create({
                    content: msg,
                    className: 'info'
                });
            }
        };
        return api;
    }]);

app.service("handleResponseService", ['$q', 'alerts',
    function ($q, alerts) {
        var api = {
            handleError: function handleError(response) {
                //if (!angular.isObject(response.data)) {
                //    return ($q.reject("An unknown error occurred."));
                //}

                //if (response.data.еxceptionMessage) {
                //    alerts.addError(response.data.еxceptionMessage);
                //} else if (response.data.modelState) {
                //    angular.forEach(response.data.modelState, function(value, key) {
                //        alerts.addError(value.toString());
                //    });
                //}
                //else if (response.data.message) {
                //    alerts.addError(response.data.message);
                //}

                //if (!response.data.modelState) {
                //    alerts.addError(msg);
                //}
                //return ($q.reject(response.data.message));
            },

            handleSuccess: function handleSuccess(response) {
                return (response.data);
            }
        };

        return api;
    }]);

app.controller("HomeCtrl", ['$scope', 'ngProgress', '$rootScope', '$location', function ($scope, ngProgress, $rootScope, $location) {
    $scope.text = 'Hello, this is homepage';
    //$scope.title = $rootScope.title;
}]);

app.directive('onFinishRender', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last) {
                return $timeout(function () {
                    scope.$emit('ngRepeatFinished');
                });
            }
        }
    };
});

app.directive('fixedHeader', ['$timeout', function ($timeout) {
    return {
        restrict: 'A',
        scope: {
            tableHeight: '@'
        },
        link: function ($scope, $elem, $attrs, $ctrl) {
            // wait for content to load into table and the tbody to be visible
            $scope.$watch(function () { return $elem.find("tbody").is(':visible'); },
                function (newValue, oldValue) {
                    if (newValue === true) {
                        // reset display styles so column widths are correct when measured below
                        $elem.find('thead, tbody').css('display', '');

                        // wrap in $timeout to give table a chance to finish rendering
                        $timeout(function () {
                            // set widths of columns
                            $elem.find('th').each(function (i, thElem) {
                                thElem = $(thElem);
                                var tdElems = $elem.find('tbody tr:first td:nth-child(' + (i + 1) + ')');
                                //var tfElems = $elem.find('tfoot tr:first td:nth-child(' + (i + 1) + ')');

                                var columnWidth = tdElems.width();
                                thElem.width(columnWidth);
                                tdElems.width(columnWidth);
                                //tfElems.width(columnWidth);
                            });

                            // set css styles on thead and tbody
                            $elem.find('thead').css({
                                'display': 'block',
                            });

                            $elem.find('tbody').css({
                                'display': 'block',
                                'height': $scope.tableHeight || '300px',
                                'overflow': 'auto'
                            });

                            // reduce width of last column by width of scrollbar
                            var scrollBarWidth = $elem.find('thead').width() - $elem.find('tbody')[0].clientWidth;
                            if (scrollBarWidth > 0) {
                                // for some reason trimming the width by 2px lines everything up better
                                scrollBarWidth -= 2;
                                $elem.find('tbody tr:first td:last-child').each(function (i, elem) {
                                    $(elem).width($(elem).width() - scrollBarWidth);
                                });


                            }
                        });
                    }
                });
        }
    };
}]);

// Authentication Start

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

// Authentication End

app.directive('loader', ['$http', function ($http) {
    return {
        restrict: 'A',
        link: function (scope, elm, attrs) {
            scope.isLoading = function () {
                return $http.pendingRequests.length > 0;
            };

            scope.$watch(scope.isLoading, function (v) {
                if (v) {
                    elm.show();
                    $("#MainContent").css('opacity', 0.5);
                } else {
                    elm.hide();
                    $("#MainContent").css('opacity', 1);
                }
            });
        }
    };
}]);
app.directive('bsPopover', function () {
    return function (scope, element, attrs) {
        element.find("a[rel=popover]").popover({ placement: 'bottom', html: 'true' });
    };
});



