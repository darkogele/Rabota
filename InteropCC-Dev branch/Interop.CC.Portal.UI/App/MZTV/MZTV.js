app.config(function ($stateProvider, appBaseUrl) {

    $stateProvider
        .state("mztvdataforconstructionpermitindex", {
            title: 'Податоци за градежна дозвола',
            controller: "MztvCtrl",
            url: "/mztv/mztvdataforconstructionpermitindex",
            templateUrl: 'App/MZTV/Templates/MZTVDataForConstructionPermit'
        })
        .state("mztvdataforconstructionpermit", {
            title: 'Податоци за градежна дозвола',
            controller: "MztvCtrl",
            url: "/mztv/mztvdataforconstructionpermit",
            templateUrl: 'App/MZTV/Templates/MZTVDataForConstructionPermit'
        });
});

angular.module("MZTV", ['ui.bootstrap'])
    .service("mztvService", ['$http', '$q', 'handleResponseService', 'apiUri',
    function ($http, $q, handleResponseService, apiUri) {
        return ({
            getDataForConstructionPermit: getDataForConstructionPermit,
            dataForConstructionPermitPDF: dataForConstructionPermitPDF,
            dataForConstructionPermitXML: dataForConstructionPermitXML
        });

        function getDataForConstructionPermit(archiveNumber, constructionTypeId, municipalityId, sendDate, getDocuments) {
            var request = $http({
                method: 'POST',
                url: apiUri + "MZTV/GetDataForConstructionPermit",
                params: {
                    archiveNumber: archiveNumber,
                    constructionTypeId: constructionTypeId,
                    municipalityId: municipalityId,
                    sendDate: sendDate,
                    getDocuments: getDocuments
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }

        function dataForConstructionPermitPDF(namepdf) {
            var request = $http({
                method: "GET",
                url: apiUri + "MZTV/dataForConstructionPermitPDF",
                responseType: "arraybuffer",

                params: {
                    namepdf: namepdf,

                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function dataForConstructionPermitXML(namexml) {
            var request = $http({
                method: "GET",
                url: apiUri + "MZTV/dataForConstructionPermitXML",
                responseType: "arraybuffer",

                params: {
                    namexml: namexml,

                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
    }])

.controller("MztvCtrl", ['$scope', 'mztvService', '$state', 'alerts',
function ($scope, mztvService, $state, alerts) {

    $scope.show = false;
    //$scope.getDocuments = "n";
    $scope.mztvDataForConstructionPermit = function (archiveNumber, constructionTypeId, municipalityId, sendDate, getDocuments ) {
        mztvService.getDataForConstructionPermit(archiveNumber, constructionTypeId, municipalityId, sendDate, getDocuments)
            .then(function (response) {
                if (response.archiveNumber != null) {
                    $scope.show = false;
                    alerts.addError("ГРЕШКА");
                } else {
                    $scope.archiveDate = response.archiveDate;
                    $scope.constructionAddress = response.constructionAddress;
                    $scope.constructionDescription = response.constructionDescription;
                    $scope.constructionTypeName = response.constructionTypeName;
                    $scope.documents = response.documents;
                    $scope.effectDate = response.effectDate;
                    $scope.investors = response.investors;
                    $scope.municipalities = response.municipalities;
                    $scope.sendDate = response.sendDate;
                    $scope.status = response.status;
                    $scope.show = true;
                    $scope.namepdf = response.constructionPermitPDF;
                    $scope.namexml = response.constructionPermitXML;
                }
            });
    };
    $scope.expandedOps = function () {
        if ($scope.expandOps == true) {
            $scope.expandOps = false;
        } else {
            $scope.expandOps = true;
        }
    };
    $scope.expandedLoad = function () {
        if ($scope.expandLoad == true) {
            $scope.expandLoad = false;
        } else {
            $scope.expandLoad = true;
        }
    };

    $scope.expandedObject = function () {
        if ($scope.expandObject == true) {
            $scope.expandObject = false;
        } else {
            $scope.expandObject = true;
        }
    };

    $scope.expandedOwner = function () {
        if ($scope.expandOwner == true) {
            $scope.expandOwner = false;
        } else {
            $scope.expandOwner = true;
        }
    };

    // Export to PDF
    $scope.dataForConstructionPermitPDF = function() {
        var savedPdFname = $scope.namepdf;
        mztvService.dataForConstructionPermitPDF(savedPdFname).then(function(result) {
            if (result) {

                var blob = new Blob([result], { type: "application/pdf" });
                navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                if (navigator.saveBlob) {
                    navigator.saveBlob(blob, savedPdFname);
                } else {
                    saveBlob(blob, savedPdFname);
                }
            }

        });
    };

    // Export to XML
    $scope.dataForConstructionPermitXML = function() {
        var savedXmlname = $scope.namexml;
        mztvService.dataForConstructionPermitXML(savedXmlname).then(function(result) {
            if (result) {

                var blob = new Blob([result], { type: "application/xml" });
                navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                if (navigator.saveBlob) {
                    navigator.saveBlob(blob, savedXmlname);
                } else {
                    saveBlob(blob, savedXmlname);
                }
            }
        });
    };

    // BLOB container -save function
    function saveBlob(blob, name) {
        var a = document.createElement('a');
        a.href = (window.URL || window.webkitURL).createObjectURL(blob);
        a.download = name;
        a.style.display = 'none';
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
    }

}]);
