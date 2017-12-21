'use strict';
angular.module("Security", ['ui.bootstrap'])
    .service('securityService', ['$http', '$q', 'handleResponseService', 'apiUri', 'baseApiUri', 'authInterceptorService',
    function ($http, $q, handleResponseService, apiUri, baseApiUri, authInterceptorService) {
        return ({
        getRegistered: getRegistered,
        getLoggedIn: getLoggedIn,
        getRefreshToken: getRefreshToken
        });

        function getRegistered(data) {
            var request = $http({
                method: "POST",
                url: apiUri + "/Auth/Register",
                data: data
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
        }

        function getLoggedIn(data) {
            var request = $http({
                method: "POST",
                url: baseApiUri + 'token',
                data: data,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            });
            return (request.then(handleResponseService.handleSuccess, authInterceptorService.responseError));
                }
        
        function getRefreshToken(data) {
            var request = $http({
                method: "POST",
                    url: baseApiUri + 'token',
                    data: data,
                        headers : {
                    'Content-Type': 'application/x-www-form-urlencoded'
                    }
            });
            return (request.then(handleResponseService.handleSuccess, authInterceptorService.responseError));
            }
       }]);
app.factory('authInterceptorService', ['$q', '$injector', '$location', 'localStorageService', 'alerts',
    function ($q, $injector, $location, localStorageService, alerts) {

        var authInterceptorServiceFactory = {};
        var $http;
        var _request = function (config) {

            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        };

        var _responseError = function(rejection) {
            var deferred = $q.defer();
            if (rejection.status === 401) {
                var authService = $injector.get('authService');
                authService.refreshToken().then(function (response) {
                    _retryHttpRequest(rejection.config, deferred);
                }, function() {
                    authService.logOut();
                    alerts.addError("Автентикацијата е одбиена. Обидете се повторно!");
                    deferred.reject(rejection);
                });
            } else {
                
                // start handleResponseService error
                if (!angular.isObject(rejection.data)) {
                    return ($q.reject("Настана грешка. Обидете се повторно!"));
                }

                if (rejection.data.exceptionMessage) {
                    alerts.addError(rejection.data.exceptionMessage);
                } else if (rejection.data.modelState) {
                    angular.forEach(rejection.data.modelState, function (value, key) {
                        alerts.addError(value.toString());
                    });
                }
                else if (rejection.data.message) {
                    alerts.addError(rejection.data.message);
                }

                //return ($q.reject(response.data.message));
                // end handleResponseService error

                deferred.reject(rejection);
            }
            return deferred.promise;
        };

        var _retryHttpRequest = function(config, deferred) {
            $http = $http || $injector.get('$http');
            $http(config).then(function (response) {
                deferred.resolve(response);
            }, function (response) {
                deferred.reject(response);
            });
        };
        
        //var _responseError = function (rejection) {
        //    if (rejection.status === 401) {
        //        var authService = $injector.get('authService');
        //        //var authData = localStorageService.get('authorizationData');

        //        //if (authData) {
        //        //    if (authData.useRefreshTokens) {
        //        //        $location.path('/refresh');
        //        //        return $q.reject(rejection);
        //        //    }
        //        //}
        //        authService.logOut();
        //        //$location.path('/login');
        //    }
        //    return $q.reject(rejection);
        //};

        authInterceptorServiceFactory.request = _request;
        authInterceptorServiceFactory.responseError = _responseError;

        return authInterceptorServiceFactory;
    }]);

app.factory('authService', ['$http', '$q', 'localStorageService', 'clientId', 'baseApiUri', 'securityService', 'alerts', '$location', '$injector', 'publicKey',
    function ($http, $q, localStorageService, clientId, baseApiUri, securityService, alerts, $location, $injector, publicKey) {

        var serviceBase = baseApiUri;
        var authServiceFactory = {};
        
        var _authentication = {
            isAuth: false,
            userName: "",
            permision: false,
            superPermision:false
        };

        var _externalAuthData = {
            provider: "",
            userName: "",
            externalAccessToken: ""
        };

        var _saveRegistration = function (registration) {

            _logOut();
            //return $http.post(serviceBase + 'register', registration).then(function (response) {
            //    return response;
            //});
            return securityService.getRegistered(registration).then(function (response) {
                return response;
            });

        };

        var _login = function (loginData) {
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password +"&publicKey=" +publicKey;

                //if (_authentication.useRefreshTokens) {
            data = data + "&client_id=" + clientId;
            //}

            var deferred = $q.defer();

            securityService.getLoggedIn(data).then(function(response) {
                //if (_authentication.useRefreshTokens) {
                _authentication.isAuth = true;
                _authentication.userName = loginData.userName;
                _authentication.permision = (response.permision === "true");
                _authentication.superPermision = (response.superPermision === "true");
                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: response.refresh_token, isAuth: true, permision: _authentication.permision, superPermision: _authentication.superPermision });
            //}
            //else {
            //    localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: "", useRefreshTokens: false });
                //}
                
                alerts.addSuccess("Добредојде " + response.userName);
                //_authentication.useRefreshTokens = authentication.useRefreshTokens;

                deferred.resolve(response);

            }, function (err) {
                alerts.addError("Податоците за најава не се валидни.");
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        var _logOut = function () {

            localStorageService.remove('authorizationData');

            _authentication.isAuth = false;
            _authentication.userName = "";
            _authentication.permision = false;
            _authentication.superPermision = false;
            // _authentication.useRefreshTokens = false;
            var $state = $injector.get('$state');
            $state.go("login");

        };

        var _fillAuthData = function () {
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                _authentication.isAuth = true;
                _authentication.userName = authData.userName;
                _authentication.permision = authData.permision;
                _authentication.superPermision = authData.superPermision;
                var $state2 = $injector.get('$state');
                $state2.go("home");
                // _authentication.useRefreshTokens = authData.useRefreshTokens;
            }
            else {
                var $state = $injector.get('$state');
                $state.go("login");
            }
        };

        var _refreshToken = function () {
            var deferred = $q.defer();

            var authData = localStorageService.get('authorizationData');

            if (authData) {
                //if (authData.useRefreshTokens) {

                var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + clientId;

                localStorageService.remove('authorizationData');

                securityService.getRefreshToken(data).then(function (response) {
                    _authentication.isAuth = true;
                    _authentication.userName = response.userName;
                    _authentication.permision = (response.permision === "true");
                    _authentication.superPermision = (response.superPermision === "true");
                    localStorageService.set('authorizationData', {
                        token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, permision: _authentication.permision, superPermision: _authentication.superPermision
                    });
                    
                    deferred.resolve(response);

                }, function (err) {
                    alerts.addError("Немате право на најава со внесените податоци!!");
                    _logOut();
                    deferred.reject(err);
                });
                //}
            } else {
                deferred.reject();
            }

            return deferred.promise;
        };

        var _obtainAccessToken = function (externalData) {

            var deferred = $q.defer();

            $http.get(serviceBase + 'api/auth/ObtainLocalAccessToken', { params: { provider: externalData.provider, externalAccessToken: externalData.externalAccessToken } }).success(function (response) {

                localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: response.refresh_token });

                _authentication.isAuth = true;
                _authentication.userName = response.userName;
                _authentication.permision = response.permision;
                _authentication.superPermision = response.superPermision;
                // _authentication.useRefreshTokens = false;

                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        var _registerExternal = function (registerExternalData) {

            var deferred = $q.defer();

            $http.post(serviceBase + 'api/auth/registerexternal', registerExternalData).success(function (response) {

                localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: "" });

                _authentication.isAuth = true;
                _authentication.userName = response.userName;
                _authentication.permision = (response.permision === "true");
                _authentication.superPermision = (response.superPermision === "true");
                //_authentication.useRefreshTokens = false;

                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        authServiceFactory.saveRegistration = _saveRegistration;
        authServiceFactory.login = _login;
        authServiceFactory.logOut = _logOut;
        authServiceFactory.fillAuthData = _fillAuthData;
        authServiceFactory.authentication = _authentication;
        authServiceFactory.refreshToken = _refreshToken;

        authServiceFactory.obtainAccessToken = _obtainAccessToken;
        authServiceFactory.externalAuthData = _externalAuthData;
        authServiceFactory.registerExternal = _registerExternal;

        return authServiceFactory;
    }]);


app.factory('tokensManagerService', ['$http', 'baseApiUri', function ($http, baseApiUri) {

    var serviceBase = baseApiUri;

    var tokenManagerServiceFactory = {};

    var _getRefreshTokens = function () {

        return $http.get(serviceBase + 'api/refreshtokens').then(function (results) {
            return results;
        });
    };

    var _deleteRefreshTokens = function (tokenid) {

        return $http.delete(serviceBase + 'api/refreshtokens/?tokenid=' + tokenid).then(function (results) {
            return results;
        });
    };

    tokenManagerServiceFactory.deleteRefreshTokens = _deleteRefreshTokens;
    tokenManagerServiceFactory.getRefreshTokens = _getRefreshTokens;

    return tokenManagerServiceFactory;

}]);