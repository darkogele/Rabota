app.config(function ($stateProvider, appBaseUrl) {

    $stateProvider
        .state("mondataforregularstudentindex", {
            title: 'Податоци за редовен ученик',
            controller: "MonCtrl",
            url: "/mon/mondataforregularstudentindex",
            templateUrl: 'App/MON/Templates/MONDataForRegularStudent'
        })
        .state("mondataforregularstudent", {
            title: 'Податоци за редовен ученик',
            controller: "MonCtrl",
            url: "/mon/mondataforregularstudent",
            templateUrl: 'App/MON/Templates/MONDataForRegularStudent'
        })
        .state("monstatusforstudentindex", {
            title: 'Статус за ученик',
            controller: "MonCtrl",
            url: "/mon/monstatusforstudentindex",
            templateUrl: 'App/MON/Templates/MONStatusForStudent'
        })
        .state("monbigdataindex", {
            title: 'Голем документ',
            controller: "MonCtrl",
            url: "/mon/monbigdataindex",
            templateUrl: 'App/MON/Templates/MONBigDataService'
        })
        .state("monstatusforstudent", {
            title: 'Статус за ученик',
            controller: "MonCtrl",
            url: "/mon/monstatusforstudent",
            templateUrl: 'App/MON/Templates/MONStatusForStudent'
        });
});

angular.module("MON", ['ui.bootstrap'])
.service("monService", ['$http', '$q', 'handleResponseService', 'apiUri',
    function ($http, $q, handleResponseService, apiUri) {
        return ({
            getDataForRegularStudent: getDataForRegularStudent,
            getStatusForStudent: getStatusForStudent,
            dataForRegularStudentXml: dataForRegularStudentXml,
            dataForRegularStudentPdf: dataForRegularStudentPdf,
            bigData: bigData
        });

        function getDataForRegularStudent(embg) {
            var request = $http({
                method: 'POST',
                url: apiUri + "MON/GetDataForRegularStudent",
                params: {
                    embg: embg
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }


        function getStatusForStudent(embg) {
            var request = $http({
                method: 'POST',
                url: apiUri + "MON/GetStatusForStudent",
                params: {
                    embg: embg
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        } function dataForRegularStudentXml(namepdf) {
            var request = $http({
                method: "GET",
                url: apiUri + "MON/DataForRegularStudentPdf",
                responseType: "arraybuffer",

                params: {
                    namepdf: namepdf,

                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function dataForRegularStudentPdf(namexml) {
            var request = $http({
                method: "GET",
                url: apiUri + "MON/DataForRegularStudentXml",
                responseType: "arraybuffer",

                params: {
                    namexml: namexml,

                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }

        function bigData(typeDoc, responseSize) {
            var request = $http({
                method: "GET",
                url: apiUri + "MON/BigData",
                responseType: "arraybuffer",
                params: {
                    typeDoc: typeDoc,
                    responseSize: responseSize
                }
            });
            return (request.then(handleResponseService.handleSuccess));
        }


    }])

.controller("MonCtrl", ['$scope', 'monService', '$state', 'alerts',
function ($scope, monService, $state, alerts) {
    $scope.show = false;
    $scope.hasResult = false;

    $scope.monDataForRegularStudent = function (embg) {
        monService.getDataForRegularStudent(embg)
            .then(function (response) {
                if (response.message != null) {
                    $scope.show = false;
                    alerts.addError(response.message);
                } else {
                    $scope.municipality = response.municipality;
                    $scope.school = response.school;
                    $scope.schoolClass = response.schoolClass;
                    $scope.schoolYear = response.schoolYear;
                    $scope.grade = response.grade;
                    $scope.profession = response.profession;
                    $scope.profile = response.profile;
                    $scope.programAssociation = response.programAssociation;
                    $scope.languageEducation = response.languageEducation;
                    $scope.fullName = response.fullName;
                    $scope.fatherName = response.fatherName;
                    $scope.dateOfBirth = response.dateOfBirth;
                    $scope.placeOfBirth = response.placeOfBirth;
                    $scope.embg = response.embg;
                    $scope.ethnicity = response.ethnicity;
                    $scope.nativeLanguage = response.nativeLanguage;
                    $scope.place = response.place;
                    $scope.address = response.address;
                    $scope.phone = response.phone;
                    $scope.mobile = response.mobile;
                    $scope.schoolClassStatus = response.schoolClassStatus;
                    $scope.schoolStatus = response.schoolStatus;
                    $scope.namepdf = response.filePdfName;
                    $scope.namexml = response.fileXMLName;
                    $scope.show = true;
                }

            });
    };
    $scope.dataForRegularStudentPdf = function () {
        var savedPdFname = $scope.namepdf;
        monService.dataForRegularStudentPdf(savedPdFname).then(function (result) {
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

    $scope.bigData = function (typeDoc, responseSize) {
        $scope.hasResult = false;
        monService.bigData(typeDoc, responseSize).then(function (result) {
            if (result) {
                if (typeDoc === 'pdf') {
                    var blobPdf = new Blob([result], { type: "application/pdf" });
                    navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                    if (navigator.saveBlob) {
                        navigator.saveBlob(blobPdf, "marija.pdf");
                    } else {
                        saveBlob(blobPdf, "marija.pdf");
                    }
                }
                else if (typeDoc === 'doc') {
                    var blobDoc = new Blob([result], { type: "application/msword" });
                    navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                    if (navigator.saveBlob) {
                        navigator.saveBlob(blobDoc, "marija.doc");
                    } else {
                        saveBlob(blobDoc, "marija.doc");
                    }
                }
                $scope.hasResult = true;
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

    $scope.dataForRegularStudentXml = function () {
        var savedXmlname = $scope.namexml;
        monService.dataForRegularStudentXml(savedXmlname).then(function (result) {
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
    }
    $scope.monStatusForStudent = function (embg) {
        monService.getStatusForStudent(embg)
            .then(function (response) {
                if (response == "") {
                    $scope.show = false;
                    alerts.addError("Сервисот е моментално недостапен");
                } else {
                    $scope.resp = response;
                    $scope.show = true;
                }
            });
    };
}])

;