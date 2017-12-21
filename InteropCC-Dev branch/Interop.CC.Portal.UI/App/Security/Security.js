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
                var temp = String.fromCharCode.apply(null, new Uint8Array(rejection.data));
                if (temp !== "") {
                    var jsonStr = JSON.parse(temp);
                    var temp1 = decodeURIComponent(escape(jsonStr.exceptionMessage));
                    alerts.addError(temp1);
                    //return ($q.reject(response.data.message));
                    // end handleResponseService error
                }
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
            availableServices: []
        };
        
        var listMenuItems = [];
        
       var _externalAuthData = {
            provider: "",
            userName: "",
            externalAccessToken: ""
        };
        

        function rebindMenu(items) {
            for (var i = 0; i < items.length; i++) {
                if (items[i] === "SuperAdmin" || items[i] === "Admin" || items[i] === "User") {
                    items.splice(i, 1);
                }
            }
            return items;
        };

        function getMenuItemLink(menuItemText) {
            var urlMenuItemLink = "";
            //АКН
            if (menuItemText === "АКН - Имотен лист")
                urlMenuItemLink = "aknpropertylistindex";
            if (menuItemText === "АКН - Катастарска парцела")
                urlMenuItemLink = "akncadastralparcelindex";
            if (menuItemText === "АКН - Документ - Лист за предбележување на градба")
                urlMenuItemLink = "akndataforifacilitiesdocindex({service:'pio'})";
            if (menuItemText === "АКН - Документ - Копија од катастарски план")
                urlMenuItemLink = "aknpcadastralplandocindex({service:'kkp'})";
            if (menuItemText === "АКН - Документ - Имотен лист")
                urlMenuItemLink = "aknpropertylistdocindex({service:'ild'})";

            //ФПИОМ
            if (menuItemText === "ФПИОМ - Податоци за осигуреник")
                urlMenuItemLink = "piomdataforensureeindex";
            if (menuItemText === "ФПИОМ - Податоци за пензионер")
                urlMenuItemLink = "piomdataforretiredindex";
            if (menuItemText === "ФПИОМ - Статус на осигуреник")
                urlMenuItemLink = "piomstatusforensureeindex";
            if (menuItemText === "ФПИОМ - Статус на пензионер")
                urlMenuItemLink = "piomstatusforretiredindex";
            if (menuItemText === "ФПИОМ - Години работен стаж")
                urlMenuItemLink = "piomyearsofworkexperienceindex";

            //Институција Б
            if (menuItemText === "Институција Б - Име и презиме")
                urlMenuItemLink = "namesurnameindex";
            if (menuItemText === "Институција Б - Податоци за присутни")
                urlMenuItemLink = "peopledataindex";

            //МОН
            if (menuItemText === "МОН - Потврда за редовен ученик")
                urlMenuItemLink = "mondataforregularstudentindex";
            if (menuItemText === "МОН - Статус на ученик")
                urlMenuItemLink = "monstatusforstudentindex";

            //МЗТВ
            if (menuItemText === "МзТВ - Одобрение за градба")
                urlMenuItemLink = "mztvdataforconstructionpermitindex";

            //ЦРМ
            if (menuItemText === "ЦРМ - Тековна состојба (сите податоци)")
                urlMenuItemLink = "crmtekovnasostojbaindex";
            if (menuItemText === "ЦРМ - Тековна состојба (УЈП)")
                urlMenuItemLink = "crmtekovnasostojbaujpindex";
            if (menuItemText === "ЦРМ - Тековна состојба (АКН)")
                urlMenuItemLink = "crmtekovnasostojbaaknindex";
            if (menuItemText === "ЦРМ - Тековна состојба (ЦУРМ)")
                urlMenuItemLink = "crmtekovnasostojbacurmprodindex";

            //ЦУРМ
            if (menuItemText === "ЦУРМ - Податоци за извршен извоз")
                urlMenuItemLink = "curmdataforexecutedexportindex";
            if (menuItemText === "ЦУРМ - Податоци за извршен увоз")
                urlMenuItemLink = "curmdataforexecutedimportindex";
            if (menuItemText === "ЦУРМ - Единствен царински документ и список на ЕЦД")
                urlMenuItemLink = "curmsinglecustomsdocumentindex";
            if (menuItemText === "ЦУРМ - Регистар на обврзници за акциза")
                urlMenuItemLink = "ujpregisterofexcisebondsindex";

            //УЈП
            if (menuItemText === "УЈП - Единствен матичен број")
                urlMenuItemLink = "ujpedbembembindex";
            if (menuItemText === "УЈП - Единствен даночен број")
                urlMenuItemLink = "ujpedbembindex";
            return urlMenuItemLink;
            
        };

        var _saveRegistration = function (registration) {
            _logOut();
            return securityService.getRegistered(registration).then(function (response) {
                return response;
            });
        };
        
        var _getPermission = function (roles) {
            var rolesNames = roles.split(',');
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                for (var i = 0; i < rolesNames.length; i++) {
                    var temp = authData.userRoles.indexOf(rolesNames[i]);
                    if (temp == -1)
                        return false;
                }
            }
            else
            {
                return false;
            }
            return true;

        };
       

        var _login = function (loginData) {
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password +"&publicKey=" +publicKey;
            data = data + "&client_id=" + clientId;
            var deferred = $q.defer();

            securityService.getLoggedIn(data).then(function(response) {
                _authentication.isAuth = true;
                _authentication.userName = loginData.userName;
                var userRoless = JSON.parse(response.roles);
                
                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: response.refresh_token, isAuth: true, userRoles: userRoless });

                if (listMenuItems.length === 0) {
                    getServiceRolesForMenu();
                }

                alerts.addSuccess("Добредојде " + response.userName);
                
                deferred.resolve(response);

            }, function (err) {
                alerts.addError("Податоците за најава не се валидни.");
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        var _changePassword = function () {
            
        };

        var _userWithRoles = function () {
            var authData = localStorageService.get('authorizationData');
            var objUserWithRoles = {
                objUserName: "",
                objUserRoles: []
            };
            if (authData) {
                objUserWithRoles.objUserName = authData.userName;
                objUserWithRoles.objUserRoles = rebindMenu(authData.userRoles);
            }
            return objUserWithRoles;
        };

        var _logOut = function () {

            localStorageService.remove('authorizationData');

            _authentication.isAuth = false;
            _authentication.userName = "";
            listMenuItems = [];
            _authentication.availableServices = [];
            var $state = $injector.get('$state');
            $state.go("login");

        };

        var _fillAuthData = function () {
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                _authentication.isAuth = true;
                _authentication.userName = authData.userName;
                
                if (listMenuItems.length === 0) {
                    getServiceRolesForMenu();
                }
                
                
                var $state2 = $injector.get('$state');
                $state2.go("home");
            } else {
                var $state = $injector.get('$state');
                $state.go("login");
            }
        };

        var _rebindRolesAfterGetProviders = function (rolesAfterRebind) {
            var userRolesFromStorage = localStorageService.get('authorizationData').userRoles;
          
            var removedDefaultRole = "";
            //Default user roles
            var defaultUserRoles = ['SuperAdmin', 'Admin', 'User'];
            

            for (var j = 0; j < userRolesFromStorage.length; j++) {
                //remove default user role from list 
                for (var k = 0; k < defaultUserRoles.length; k++) {
                    if (userRolesFromStorage[j] === defaultUserRoles[k]) {
                        //store removed default user role in variable
                        removedDefaultRole = defaultUserRoles[k];
                        //remove from list roles that are default
                        userRolesFromStorage.splice(j, 1);
                    }
                }
                //check if every element from one list is in another list
                //if not remove it
                if (rolesAfterRebind.data.indexOf(userRolesFromStorage[j]) === -1) {
                    userRolesFromStorage.splice(j, 1);
                }
            }
            
            if (removedDefaultRole !== "") {
                //add to list default user role that was previously deleted
                userRolesFromStorage.push(removedDefaultRole);
            }
            
            //set user roles in local storage one again
            var localStorage = localStorageService.get('authorizationData');
            localStorageService.set('authorizationData', { token: localStorage.token, userName: localStorage.userName, refreshToken: localStorage.refreshToken, isAuth: localStorage.isAuth, userRoles: userRolesFromStorage });
            
            //create menu
            getServiceRolesForMenu();

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
                    var userRoless = JSON.parse(response.roles);
                    
                    localStorageService.set('authorizationData', {
                        token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, userRoles: userRoless,
                    });
                    
                    if (listMenuItems.length === 0) {
                        getServiceRolesForMenu();
                    }
                    
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

                localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, userRoles: userRoless });

                _authentication.isAuth = true;
                _authentication.userName = response.userName;
                
                if (listMenuItems.length === 0) {
                    getServiceRolesForMenu();
                }

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
                
                if (listMenuItems.length === 0) {
                    getServiceRolesForMenu();
                }
                //_authentication.useRefreshTokens = false;

                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        function getServiceRolesForMenu() {
           
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                if (authData.userRoles !== undefined) {
                    if (authData.userRoles.length > 0) {
                        _authentication.availableServices = [];
                        listMenuItems = [];
                        listMenuItems = rebindMenu(localStorageService.get('authorizationData').userRoles);
                        for (var i = 0; i < listMenuItems.length; i++) {
                            var menuAvailableService = {};
                            //So cel da ne se dodavaat dvata servisi na CRM, lista subjekti vo menito, bidejki nemame UI za niv
                            //se povikuvaat samo system-to-system
                            if (!listMenuItems[i].includes("субјекти")) {
                                menuAvailableService.text = listMenuItems[i];
                                menuAvailableService.link = getMenuItemLink(listMenuItems[i]);
                                _authentication.availableServices.push(menuAvailableService);
                            }
                        }
                    }
                }
            }
            
        };

        authServiceFactory.saveRegistration = _saveRegistration;
        authServiceFactory.login = _login;
        authServiceFactory.logOut = _logOut;
        authServiceFactory.fillAuthData = _fillAuthData;
        authServiceFactory.getPermission = _getPermission;
        authServiceFactory.authentication = _authentication;
        authServiceFactory.refreshToken = _refreshToken;
        authServiceFactory.obtainAccessToken = _obtainAccessToken;
        authServiceFactory.externalAuthData = _externalAuthData;
        authServiceFactory.registerExternal = _registerExternal;
        authServiceFactory.userWithRoles = _userWithRoles;
        authServiceFactory.rebindRolesAfterGetProviders = _rebindRolesAfterGetProviders;
        authServiceFactory.changePassword = _changePassword;

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