
app.config(function($stateProvider, appBaseUrl) {

    $stateProvider
        .state("piomdataforretiredindex", {
            title: 'Податоци за пензионер',
            controller: "PiomCtrl",
            url: "/piom/piomdataforretiredindex",
            templateUrl: 'App/PIOM/Templates/PIOMDataForRetired'
        })
        .state("piomdataforretired", {
            title: 'Податоци за пензионер',
            controller: "PiomCtrl",
            url: "/piom/piomdataforretired",
            templateUrl: 'App/PIOM/Templates/PIOMDataForRetired'
        })
        .state("piomdataforensureeindex", {
            title: 'Податоци за осигуреник',
            controller: "PiomCtrl",
            url: "/piom/piomdataforensureeindex",
            templateUrl: 'App/PIOM/Templates/PIOMDataForEnsuree'
        })
        .state("piomdataforensuree", {
            title: 'Податоци за осигуреник',
            controller: "PiomCtrl",
            url: "/piom/piomdataforensuree",
            templateUrl: 'App/PIOM/Templates/PIOMDataForEnsuree'
        })
        .state("piomstatusforensureeindex", {
            title: 'Статус за осигуреник',
            controller: "PiomCtrl",
            url: "/piom/piomstatusforensureeindex",
            templateUrl: 'App/PIOM/Templates/PIOMStatusForEnsuree'
        })
        .state("piomstatusforensuree", {
            title: 'Статус за осигуреник',
            controller: "PiomCtrl",
            url: "/piom/piomstatusforensuree",
            templateUrl: 'App/PIOM/Templates/PIOMStatusForEnsuree'
        })
        .state("piomstatusforretiredindex", {
            title: 'Статус за пензионер',
            controller: "PiomCtrl",
            url: "/piom/piomstatusforretiredindex",
            templateUrl: 'App/PIOM/Templates/PIOMStatusForRetired'
        })
        .state("piomstatusforretired", {
            title: 'Статус за пензионер',
            controller: "PiomCtrl",
            url: "/piom/piomstatusforretired",
            templateUrl: 'App/PIOM/Templates/PIOMStatusForRetired'
        })
        .state("piomyearsofworkexperienceindex", {
            title: 'Години работен стаж',
            controller: "PiomCtrl",
            url: "/piom/piomyearsofworkexperienceindex",
            templateUrl: 'App/PIOM/Templates/PIOMYearsOfWorkExperience'
        })
        .state("piomyearsofworkexperience", {
            title: 'Години работен стаж',
            controller: "PiomCtrl",
            url: "/piom/piomyearsofworkexperience",
            templateUrl: 'App/PIOM/Templates/PIOMYearsOfWorkExperience'
        });
});

angular.module("PIOM", ['ui.bootstrap'])
    .service("piomService", ['$http', '$q', 'handleResponseService', 'apiUri',
        function($http, $q, handleResponseService, apiUri) {
            return ({
                getDataForRetired: getDataForRetired,
                getDataForEnsuree: getDataForEnsuree,
                getStatusForEnsuree: getStatusForEnsuree,
                getStatusForRetired: getStatusForRetired,
                getYearsOfWorkExperience: getYearsOfWorkExperience,
                DataEnsureePdf: DataEnsureePdf,
                DataEnsureeXML: DataEnsureeXML,
                DataRetiredPDF:DataRetiredPDF,
                DataRetiredXML: DataRetiredXML,
                YearsOfExperiencePDF:YearsOfExperiencePDF,
                YearsOfExperienceXML: YearsOfExperienceXML
            });

            function getDataForRetired(embg) {
                var request = $http({
                    method: 'POST',
                    url: apiUri + "FPIOM/GetDataForRetired",
                    params: {
                        embg: embg
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }

            function getDataForEnsuree(embg) {
                var request = $http({
                    method: 'POST',
                    url: apiUri + "FPIOM/GetDataForEmployee",
                    params: {
                        embg: embg
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }

            function getStatusForEnsuree(embg) {
                var request = $http({
                    method: 'POST',
                    url: apiUri + "FPIOM/GetStatusForInsured",
                    params: {
                        embg: embg
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }

            function getStatusForRetired(embg) {
                var request = $http({
                    method: 'POST',
                    url: apiUri + "FPIOM/GetStatusForRetired",
                    params: {
                        embg: embg
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }

            function getYearsOfWorkExperience(embg) {
                var request = $http({
                    method: 'POST',
                    url: apiUri + "FPIOM/GetYearsOfWorkExperience",
                    params: {
                        embg: embg
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }
            function DataEnsureePdf(namepdf) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "FPIOM/DataEnsureePdf",
                    responseType: "arraybuffer",

                    params: {
                        namepdf: namepdf,
                       
                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }
            function DataEnsureeXML(namexml) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "FPIOM/DataEnsureeXML",
                    responseType: "arraybuffer",

                    params: {
                        namexml: namexml,

                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }
            function DataRetiredPDF(namepdfretired) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "FPIOM/DataRetiredPdf",
                    responseType: "arraybuffer",

                    params: {
                        namepdfretired: namepdfretired

                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }
            function DataRetiredXML(namexmlretired) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "FPIOM/DataRetiredXML",
                    responseType: "arraybuffer",

                    params: {
                        namexmlretired: namexmlretired,

                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }

            function YearsOfExperiencePDF(yearsexperiencepdf) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "FPIOM/YearsOfExperiencePDF",
                    responseType: "arraybuffer",

                    params: {
                        yearsexperiencepdf: yearsexperiencepdf

                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }
            function YearsOfExperienceXML(yearsexperiencexml) {
                var request = $http({
                    method: "GET",
                    url: apiUri + "FPIOM/YearsOfExperienceXML",
                    responseType: "arraybuffer",

                    params: {
                        yearsexperiencexml: yearsexperiencexml

                    }
                });
                return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }
        }])
    .controller("PiomCtrl", ['$scope', 'piomService', '$state', 'alerts', '$window',
        function ($scope, piomService, $state, alerts) {
            $scope.show = false;
            
            $scope.piomDataForRetired = function (embg) {
                $scope.show = false;
                piomService.getDataForRetired(embg)
                    .then(function(response) {
                        if (response.pensionNumber == "") {
                            $scope.show = false;
                            alerts.addError("Не може да се пронајде пензионер со ЕМБГ " + response.embg);
                        } else {
                            $scope.pensionNumber = response.pensionNumber;
                            $scope.nameSurname = response.nameSurname;
                            $scope.pensionStatus = response.pensionStatus;
                            $scope.pensionStatusDesc = response.pensionStatusDesc;
                            $scope.namepdfRetired = response.filePdfNameRetired;
                            $scope.namexmlRetired = response.fileXMLNameRetired;
                            $scope.show = true;
                        }

                    });
            };
            $scope.piomDataForEnsuree = function (embg) {
                $scope.show = false;
                piomService.getDataForEnsuree(embg)
                    .then(function(response) {
                        if (response.embg == "") {
                            $scope.show = false;
                            alerts.addError("Аn error has occured!");
                        } else {
                            $scope.embg = response.embg;
                            $scope.name = response.name;
                            $scope.surname = response.surname;
                            $scope.workExperience = response.workExperience;
                            $scope.show = true;
                            $scope.namepdf = response.filePdfName;
                            $scope.namexml = response.fileXMLName;
                        }

                    });
            };
            $scope.piomStatusForEnsuree = function (embg) {
                $scope.show = false;
                piomService.getStatusForEnsuree(embg)
                    .then(function(response) {
                        if (response == "") {
                            $scope.show = false;
                            alerts.addError("Не може да се пронајде осигуреник со тој ЕМБГ ");
                        } else {
                            $scope.resp = response;
                            $scope.show = true;
                        }
                    });
            };
            $scope.piomStatusForRetired = function (embg) {
                $scope.show = false;
                piomService.getStatusForRetired(embg)
                    .then(function(response) {
                        if (response == "") {
                            $scope.show = false;
                            alerts.addError("Не може да се пронајде пензионер со тој ЕМБГ ");
                        } else {
                            $scope.resp = response;
                            $scope.show = true;
                        }
                    });
            };
            $scope.piomYearsOfWorkExperience = function (embg) {
                $scope.show = false;
                piomService.getYearsOfWorkExperience(embg)
                    .then(function(response) {
                        if (response.generalData == null) {
                            $scope.show = false;
                            alerts.addError("Не може да се пронајде осигуреник со ЕМБГ " + response.embg);
                        } else {
                            $scope.embg = response.embg;
                            $scope.generalData = response.generalData;
                            $scope.insuranceData = response.insuranceData;
                            $scope.oldAndForegnExperience = response.oldAndForegnExperience;
                            $scope.m4 = response.m4;
                            $scope.invalidM4 = response.invalidM4;
                            $scope.yearsPDF = response.yearsOfWorkPDF;
                            $scope.yearsXML = response.yearsOfWorkXML;
                            $scope.show = true;
                        }

                    });
            };
            $scope.expandedData = function() {
                if ($scope.expandData == true) {
                    $scope.expandData = false;
                } else {
                    $scope.expandData = true;
                }
            };

            $scope.expandedInsurance = function() {
                if ($scope.expandInsurance == true) {
                    $scope.expandInsurance = false;
                } else {
                    $scope.expandInsurance = true;
                }
            };

            $scope.expandedOld = function() {
                if ($scope.expandOld == true) {
                    $scope.expandOld = false;
                } else {
                    $scope.expandOld = true;
                }
            };

            $scope.expandedM4 = function() {
                if ($scope.expandM4 == true) {
                    $scope.expandM4 = false;
                } else {
                    $scope.expandM4 = true;
                }
            };

            $scope.expandedInvM4 = function() {
                if ($scope.expandInvM4 == true) {
                    $scope.expandInvM4 = false;
                } else {
                    $scope.expandInvM4 = true;
                }
            };
            $scope.EnsureePDF = function() {
                var savedPdFname = $scope.namepdf;
                piomService.DataEnsureePdf(savedPdFname).then(function(result) {
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

            function saveBlob(blob, name) {
                var a = document.createElement('a');
                a.href = (window.URL || window.webkitURL).createObjectURL(blob);
                a.download = name;
                a.style.display = 'none';
                document.body.appendChild(a);
                a.click();
                document.body.removeChild(a);
            }

            $scope.EnsureeXML = function() {
                var savedXmlname=$scope.namexml;
                piomService.DataEnsureeXML(savedXmlname).then(function(result) {
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
            $scope.RetiredPDF= function() {
                var savedPdfRetired = $scope.namepdfRetired;
                piomService.DataRetiredPDF(savedPdfRetired).then(function(result) {
                    if (result) {

                        var blob = new Blob([result], { type: "application/pdf" });
                        navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                        if (navigator.saveBlob) {
                            navigator.saveBlob(blob, savedPdfRetired);
                        } else {
                            saveBlob(blob, savedPdfRetired);
                        }
                    }
                });
            };

            $scope.RetiredXML= function() {
                var savedXmlRetired = $scope.namexmlRetired;
                piomService.DataRetiredXML(savedXmlRetired).then(function(result) {
                    if (result) {

                        var blob = new Blob([result], { type: "application/xml" });
                        navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                        if (navigator.saveBlob) {
                            navigator.saveBlob(blob, savedXmlRetired);
                        } else {
                            saveBlob(blob, savedXmlRetired);
                        }
                    }
                });
            };
            $scope.YearsExperiencePDF = function() {
                var savedPDFYearsExperience = $scope.yearsPDF;
                piomService.YearsOfExperiencePDF(savedPDFYearsExperience).then(function(result) {
                    if (result) {

                        var blob = new Blob([result], { type: "application/pdf" });
                        navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                        if (navigator.saveBlob) {
                            navigator.saveBlob(blob, savedPDFYearsExperience);
                        } else {
                            saveBlob(blob, savedPDFYearsExperience);
                        }
                    }
                });
            };
            $scope.YearsExperienceXML = function() {
                var savedXmlYearsExperience = $scope.yearsXML;
                piomService.YearsOfExperienceXML(savedXmlYearsExperience).then(function(result) {
                    if (result) {

                        var blob = new Blob([result], { type: "application/xml" });
                        navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                        if (navigator.saveBlob) {
                            navigator.saveBlob(blob, savedXmlYearsExperience);
                        } else {
                            saveBlob(blob, savedXmlYearsExperience);
                        }
                    }
                });

            };
        }]);