app.config(function ($stateProvider, appBaseUrl) {
    $stateProvider
        .state('participants', {
            title: 'Учесници',
            controller: 'ParticipantCtrl',
            url: '/participants/{pageIndex:int}/{itemsPerPage:int}/{sortDir:string}/{sortCol:string}/:participantCode',
            templateUrl: appBaseUrl + 'Module/Participant',
            resolve: {
                participantPageList: ['participantService', '$stateParams',
                    function (participantService, $stateParams) {
                        return participantService.getParticipantsPaged($stateParams.pageIndex, $stateParams.itemsPerPage,$stateParams.sortDir, $stateParams.sortCol, $stateParams.participantCode);
                    }]
            }
        })
        .state('createparticipant', {
            title: 'Креирај учесник',
            controller: 'CreateParticipantCtrl',
            url: '/participant/new',
            templateUrl: 'App/Participant/Templates/CreateParticipant'
        })
        .state('editparticipant', {
            title: 'Измени учесник',
            controller: 'EditParticipantCtrl',
            url: '/participant/:code',
            templateUrl: 'App/Participant/Templates/EditParticipant',
            resolve: {
                participant: ['participantService', '$stateParams',
                    function (participantService, $stateParams) {
                        return participantService.getParticipant($stateParams.code);
                    }]
            }
        })
        .state('publickey', {
            title: "Јавен Клуч",
            controller: 'PublicKeyCtrl',
            url: '/participantPublickKey/:code',
            templateUrl: 'App/Participant/Templates/ShowPublicKey',
            resolve: {
                publickey: ['participantService', '$stateParams',
                    function (participantService, $stateParams) {
                        return participantService.getPublicKey($stateParams.code);
                    }
                ]
            }
        }
        );
});

angular.module("Participant", ['ngTable', 'ui.bootstrap'])
    .directive('fileModel', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var model = $parse(attrs.fileModel);
                var modelSetter = model.assign;

                element.bind('change', function () {
                    scope.$apply(function () {
                        modelSetter(scope, element[0].files[0]);
                    });
                });
            }
        };
    }])
.service('fileUpload', ['$http', 'apiUri', 'handleResponseService', function ($http, apiUri, handleResponseService) {
    return ({
        uploadFileToUrl: uploadFileToUrl
    });
    function uploadFileToUrl(file) {


        var fd = new FormData();
        fd.append('file', file);
        var uploadUrl = apiUri + "Participant/Upload";
        var request = $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        });
        return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
    }
}])
    .service('participantService', ['$http', '$q', 'apiUri', 'handleResponseService', 'authInterceptorService',
    function ($http, $q, apiUri, handleResponseService, authInterceptorService) {
        return ({
            getParticipants: getParticipants,
            getParticipant: getParticipant,
            createParticipant: createParticipant,
            updateParticipant: updateParticipant,
            deleteParticipant: deleteParticipant,
            getPublicKey: getPublicKey,
            getParticipantsPaged: getParticipantsPaged,
            getExternals: getExternals
        });


        function getParticipants() {
            var request = $http({
                method: "GET",
                url: apiUri + "Participant/GetParticipantList"
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
        }

        function getParticipantsPaged(pageIndex, itemsPerPage,sortDir,sortCol, participantCode) {
            var request = $http({
                method: "GET",
                url: apiUri + "Participant/GetParticipantListPaged",
                params: {
                    pageIndex: pageIndex,
                    itemsPerPage: itemsPerPage,
                    sortDir: sortDir,
                    sortCol: sortCol,
                    participantCode: participantCode
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
        }

        function createParticipant(participant) {
            var request = $http({
                method: "POST",
                data: participant,
                url: apiUri + "Participant/CreateParticipant"
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }

        function updateParticipant(participant) {
            var request = $http({
                method: "POST",
                data: participant,
                url: apiUri + "Participant/UpdateParticipant"
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }

        function getPublicKey(participantCode) {
            var request = $http({
                method: "GET",
                url: apiUri + "Participant/GetPublicKey",
                params: {
                    participantCode: participantCode
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
        }

        function getParticipant(participantCode) {
            var request = $http({
                method: "GET",
                url: apiUri + "Participant/GetParticipant",
                params:
                {
                    participantCode: participantCode
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
        }

        function deleteParticipant(participantCode) {
            var request = $http({
                method: "DELETE",
                params: { participantCode: participantCode },
                url: apiUri + "Participant/DeleteParticipant"
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
        }

        function getExternals() {
            var request = $http({
                method: "GET",
                url: apiUri + "Participant/GetExternals"
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
              ));
        }

    }])
    .service('sharedPropertiesService', function () {
        var parameters = [];

        return ({
            getParameters: getParameters,
            setParameters: setParameters,
        });

        function getParameters() {
            return parameters;
        }

        function setParameters(itemsPerPage, currentPage, sortCol, sortDir, participantCode) {
            parameters.itemsPerPage = itemsPerPage;
            parameters.currentPage = currentPage;
            parameters.sortCol = sortCol;
            parameters.sortDir = sortDir;
            parameters.participantCode = participantCode;
        }

    })

.controller("ParticipantCtrl", ['$scope', 'participantService', '$state', 'participantPageList', '$stateParams', 'sharedPropertiesService', 'alerts',
    function ($scope, participantService, $state, participantPageList, $stateParams, sharedPropertiesService, alerts) {

        // Sort
        

        $scope.sortType = 'code'; 
        $scope.sortReverse = false; 
        //Sort end

        $scope.search = [];

        var param = $stateParams;
        $scope.currentPage = parseInt(param.pageIndex);
        $scope.participants = participantPageList.items;
        $scope.itemsPerPage = participantPageList.pageSize;
        $scope.totalItems = participantPageList.totalSize;
        $scope.pageSize = participantPageList.pageSize;
        $scope.sortDir = $stateParams.sortDir;
        $scope.sortCol = $stateParams.sortCol;
        $scope.search.ParticipantCode = $stateParams.participantCode;

        $scope.selectedCode = null;

        $scope.sort = function(columnName) {
            if (columnName == $scope.sortCol) {
                if ($scope.sortDir == "asc") {
                    $scope.sortDir = "desc";
                } else {
                    $scope.sortDir = "asc";
                }
            } else {
                $scope.sortCol = columnName;
            }
            $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: $scope.itemsPerPage,sortDir: $scope.sortDir,sortCol: $scope.sortCol, participantCode: $scope.search.ParticipantCode });
        };

        $scope.setSelectedRow = function (selectedCode) {
            $scope.selectedCode = selectedCode;
        };

        $scope.pageChangeHandler = function (currentPage) {
            $state.go($state.current.name, { pageIndex: parseInt(currentPage), itemsPerPage: $scope.itemsPerPage, participantCode: $scope.search.ParticipantCode });
        };

        $scope.pageSizeChanged = function (itemsPerPage) {
            $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: parseInt(itemsPerPage), participantCode: $scope.search.ParticipantCode });
        };

        $scope.deleteParticipant = function (participantCode) {
            participantService.deleteParticipant(participantCode)
                .then(function () {
                    alerts.addSuccess("Учесникот со код '" + participantCode + "' е успешно избришан");
                    return participantService.getParticipantsPaged($scope.currentPage, $scope.itemsPerPage, $stateParams.participantCode)
                        .then(function (participants) {
                            $scope.participants = participants.items;
                            $scope.totalItems = participants.totalSize;
                        });
                });
        };

        $scope.editParticipant = function (participantCode, itemsPerPage, currentPage) {
            
            var sortCol = $stateParams.sortCol;
            var sortDir = $stateParams.sortDir;
            
            sharedPropertiesService.setParameters(itemsPerPage, currentPage, sortCol, sortDir, $scope.search.ParticipantCode);
            $state.go("editparticipant", { code: participantCode });
        };

        $scope.showPublicKey = function (participantCode, itemsPerPage, currentPage) {
            
            var sortCol = $stateParams.sortCol;
            var sortDir = $stateParams.sortDir;
            
            sharedPropertiesService.setParameters(itemsPerPage, currentPage, sortCol, sortDir, $scope.search.ParticipantCode);
            $state.go('publickey', { code: participantCode });
        };

        $scope.searchParticipants = function (searchTerm) {
            if (searchTerm === undefined) {
                $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: $scope.pageSize, participantCode: "" });
            } else {
                $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: $scope.pageSize, participantCode: searchTerm.ParticipantCode, });
            }
        };

        $scope.clearParticipants = function () {
            $scope.search = [];
            $state.go($state.current.name, { pageIndex: $scope.currentPage, itemsPerPage: $scope.pageSize, participantCode: "" });
        };

    }])
.controller("CreateParticipantCtrl", ['$scope', 'fileUpload', 'participantService', '$state', 'alerts',
function ($scope, fileUpload, participantService, $state, alerts) {
    $scope.showExternal = false;
    $scope.participant = {
        publicKey: ""
    };
    $scope.file = "";
    $scope.uploadFile = function () {
        $scope.participant.publicKey = "";

        var file = $scope.myFile;
        $scope.name = "";
        var password = $scope.password;
        fileUpload.uploadFileToUrl(file, password).then(function (publickey) {
            $scope.participant.publicKey = publickey;
            $scope.name = $scope.myFile.name;
        });


    };

    $scope.changedExternal = function () {
        var external = $scope.participant.isExternal;
        if (external == true) {
            $scope.showExternal = true;
            participantService.getExternals().then(function (response) {
                $scope.externalList = response;
            });
        } else {
            $scope.showExternal = false;
            $scope.externalList = "----";
        }
    };

    $scope.saveParticipant = function (participant) {
        $scope.participant.isActive = $scope.participant.isActive == undefined ? true : $scope.participant.isActive;
        var code = $scope.participant.code;
        var externalCode = $scope.externalCodeDropdown;
        if ($scope.participant.isExternal == true) {
            $scope.participant.code = externalCode + "$$" + code;
        }
        participantService.createParticipant(participant)
            .then(function () {
                alerts.addSuccess("Креиран е учесник со код '" + participant.code + "'");
                $state.go("participants", { pageIndex: 1, itemsPerPage: 10, sortDir:'asc', sortCol: 'code' });
            });
    };
}])

.controller("EditParticipantCtrl", ['$scope', 'participantService', 'fileUpload', '$state', 'alerts', 'participant', '$stateParams', 'sharedPropertiesService',
    function ($scope, participantService, fileUpload, $state, alerts, participant, $stateParams, sharedPropertiesService) {
        $scope.participant = participant;
        
        $scope.sortDir = sharedPropertiesService.getParameters().sortDir;
        $scope.sortCol = sharedPropertiesService.getParameters().sortCol;
        $scope.pageIndex = sharedPropertiesService.getParameters().currentPage;
        $scope.itemsPerPage = sharedPropertiesService.getParameters().itemsPerPage;
        $scope.participantCode = sharedPropertiesService.getParameters().participantCode;

        if (!angular.isObject($scope.participant)) {
            alerts.addError("Не може да се пронајде учесник со код '" + $stateParams.code + "'");
            $state.go("participants");
        }

        $scope.uploadFile = function () {
            $scope.participant.publicKey = "";

            var file = $scope.myFile;
            $scope.name = "";
            var password = $scope.password;
            fileUpload.uploadFileToUrl(file, password).then(function (publickey) {
                $scope.participant.publicKey = publickey;
                $scope.name = $scope.myFile.name;
            });


        };

        $scope.changedExternal = function () {
            var external = $scope.participant.isExternal;
            if (external == true) {
                $scope.showExternal = true;
                participantService.getExternals().then(function (response) {
                    $scope.externalList = response;
                });
            } else {
                $scope.showExternal = false;
                $scope.externalList = "----";
            }
        };

        $scope.updateParticipant = function () {
            //var code = $scope.participant.code;
            //var externalCode = $scope.externalCodeDropdown;
            //if ($scope.participant.isExternal == true) {
            //    $scope.participant.code = externalCode + "$$" + code;
            //}
            participantService.updateParticipant(participant)
                .then(function () {
                    alerts.addSuccess("Учесникот со име '" + participant.name + "' е успешно изменет");
                    $state.go("participants", { pageIndex: sharedPropertiesService.getParameters().currentPage, itemsPerPage: sharedPropertiesService.getParameters().itemsPerPage, sortDir: sharedPropertiesService.getParameters().sortDir, sortCol: sharedPropertiesService.getParameters().sortCol, participantCode: sharedPropertiesService.getParameters().participantCode });
                }, function (errMsg) {
                    //alerts.addError(errMsg.data.exceptionMessage);
                });
        };

    }])
.controller("PublicKeyCtrl", ['$scope', '$state', 'publickey', 'participantService', '$stateParams', 'sharedPropertiesService',
        function ($scope, $state, publickey, participantService, $stateParams, sharedPropertiesService) {
            $scope.publickey = publickey;
            $scope.code = $stateParams.code;
            $scope.back = function () {

                $state.go("participants", { pageIndex: sharedPropertiesService.getParameters().currentPage, itemsPerPage: sharedPropertiesService.getParameters().itemsPerPage, sortDir: sharedPropertiesService.getParameters().sortDir, sortCol: sharedPropertiesService.getParameters().sortCol, participantCode: sharedPropertiesService.getParameters().participantCode });

            };
        }
]
);