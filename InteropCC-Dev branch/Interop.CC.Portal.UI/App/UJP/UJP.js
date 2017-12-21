app.config(function ($stateProvider, appBaseUrl) {

    $stateProvider
    .state("ujpedbembindex", {
        title: 'Единствен Даночен Број',
        controller: "UjpCtrl",
        url: "/ujp/ujpedbembindex",
        templateUrl: 'App/UJP/Templates/UJPEdb'
    })
    .state("ujpedbemb", {
        title: 'Единствен Даночен Број',
        controller: "UjpCtrl",
        url: "/ujp/ujpedbemb",
        templateUrl: 'App/UJP/Templates/UJPEdb'
    })
    .state("ujpedbembembindex", {
        title: 'Единствен Матичен Број',
        controller: "UjpCtrl",
        url: "/ujp/ujpedbembembindex",
        templateUrl: 'App/UJP/Templates/UJPEmb'
    })
    .state("ujpedbembemb", {
        title: 'Единствен Матичен Број',
        controller: "UjpCtrl",
        url: "/ujp/ujpedbembemb",
        templateUrl: 'App/UJP/Templates/UJPEmb'
    })
    .state("ujpannualrevenuesfzoindex", {
        title: 'Годишни приходи ФЗО',
        controller: "UjpCtrl",
        url: "/ujp/ujpannualrevenuesfzoindex",
        templateUrl: 'App/UJP/Templates/UJPAnnualRevenuesFZO'
    })
    .state("ujpannualrevenuesfzo", {
        title: 'Годишни приходи ФЗО',
        controller: "UjpCtrl",
        url: "/ujp/ujpannualrevenuesfzo",
        templateUrl: 'App/UJP/Templates/UJPAnnualRevenuesFZO'
    })
    .state("ujpannualrevenueskkksindex", {
        title: 'Годишни приходи КККС',
        controller: "UjpCtrl",
        url: "/ujp/ujpannualrevenueskkksindex",
        templateUrl: 'App/UJP/Templates/UJPAnnualRevenuesKKKS'
    })
    .state("ujpannualrevenueskkks", {
        title: 'Годишни приходи КККС',
        controller: "UjpCtrl",
        url: "/ujp/ujpannualrevenueskkks",
        templateUrl: 'App/UJP/Templates/UJPAnnualRevenuesKKKS'
    })
    .state("ujpannualrevenuesmonindex", {
        title: 'Годишни приходи МОН',
        controller: "UjpCtrl",
        url: "/ujp/ujpannualrevenuesmonindex",
        templateUrl: 'App/UJP/Templates/UJPAnnualRevenuesMON'
    })
    .state("ujpannualrevenuesmon", {
        title: 'Годишни приходи МОН',
        controller: "UjpCtrl",
        url: "/ujp/ujpannualrevenuesmon",
        templateUrl: 'App/UJP/Templates/UJPAnnualRevenuesMON'
    })
    .state("ujpannualrevenuesmtspindex", {
        title: 'Годишни приходи МТСП',
        controller: "UjpCtrl",
        url: "/ujp/ujpannualrevenuesmtspindex",
        templateUrl: 'App/UJP/Templates/UJPAnnualRevenuesMTSP'
    })
    .state("ujpannualrevenuesmtsp", {
        title: 'Годишни приходи МТСП',
        controller: "UjpCtrl",
        url: "/ujp/ujpannualrevenuesmtsp",
        templateUrl: 'App/UJP/Templates/UJPAnnualRevenuesMTSP'
    })
    .state("ujpregisterofexcisebondsindex", {
        title: 'Регистар на обврзници за акциза',
        controller: "UjpCtrl",
        url: "/ujp/ujpregisterofexcisebondsindex",
        templateUrl: 'App/UJP/Templates/UJPRegisterOdExciseBonds'
    })
    .state("ujpregisterofexcisebonds", {
        title: 'Регистар на обврзници за акциза',
        controller: "UjpCtrl",
        url: "/ujp/ujpregisterofexcisebonds",
        templateUrl: 'App/UJP/Templates/UJPRegisterOdExciseBonds'
    })

    .state("ujpgetouavrm", {
        title: 'ОУ АВРМ',
        controller: "UjpCtrl",
        url: "/ujp/ujpgetouavrm",
        templateUrl: 'App/UJP/Templates/UJPGetOU_AVRM'
    })
    .state("ujpgetoufpiom", {
        title: 'ОУ ФПИОМ',
        controller: "UjpCtrl",
        url: "/ujp/ujpgetoufpiom",
        templateUrl: 'App/UJP/Templates/UJPGetOU_FPIOM'
    })
    .state("ujpgetoufzom", {
        title: 'ОУ ФЗОМ',
        controller: "UjpCtrl",
        url: "/ujp/ujpgetoufzom",
        templateUrl: 'App/UJP/Templates/UJPGetOU_FZOM'
    })

    .state("ujpgetppavrm", {
        title: 'ПП АВРМ',
        controller: "UjpCtrl",
        url: "/ujp/ujpgetppavrm",
        templateUrl: 'App/UJP/Templates/UJPGetPP_AVRM'
    })
    .state("ujpgetppfpiom", {
        title: 'ПП ФПИОМ',
        controller: "UjpCtrl",
        url: "/ujp/ujpgetppfpiom",
        templateUrl: 'App/UJP/Templates/UJPGetPP_FPIOM'
    })
    .state("ujpgetppfzom", {
        title: 'ПП ФЗОМ',
        controller: "UjpCtrl",
        url: "/ujp/ujpgetppfzom",
        templateUrl: 'App/UJP/Templates/UJPGetPP_FZOM'
    })

    .state("ujpgetnpavrm", {
        title: 'ОУ АВРМ',
        controller: "UjpCtrl",
        url: "/ujp/ujpgetnpavrm",
        templateUrl: 'App/UJP/Templates/UJPGetNP_AVRM'
    })
    .state("ujpgetnpfpiom", {
        title: 'ОУ ФПИОМ',
        controller: "UjpCtrl",
        url: "/ujp/ujpgetnpfpiom",
        templateUrl: 'App/UJP/Templates/UJPGetNP_FPIOM'
    })
    .state("ujpgetnpfzom", {
        title: 'ОУ ФЗОМ',
        controller: "UjpCtrl",
        url: "/ujp/ujpgetnpfzom",
        templateUrl: 'App/UJP/Templates/UJPGetNP_FZOM'
    });
});

angular.module("UJP", ['ui.bootstrap'])
    .service("ujpService", ['$http', '$q', 'handleResponseService', 'apiUri',
    function ($http, $q, handleResponseService, apiUri) {
        return ({
            getEdbEmb: getEdbEmb,
            getEdbEmbEmb: getEdbEmbEmb,
            getAnnualRevenuesFZO: getAnnualRevenuesFZO,
            getAnnualRevenuesKKKS: getAnnualRevenuesKKKS,
            getAnnualRevenuesMON: getAnnualRevenuesMON,
            getAnnualRevenuesMTSP: getAnnualRevenuesMTSP,
            getRegisterOfExciseBonds: getRegisterOfExciseBonds,
            EDBXML: EDBXML,
            EDBPDF: EDBPDF,
            EMBPDF: EMBPDF,
            EMBXML: EMBXML,
            FZOXML: FZOXML,
            FZOPDF: FZOPDF,
            MONXML: MONXML,
            MONPDF: MONPDF,
            MTSPXML: MTSPXML,
            MTSPPDF: MTSPPDF,
            ujpgetoufpiom: ujpgetoufpiom,
            ujpgetouavrm: ujpgetouavrm,
            ujpgetoufzom: ujpgetoufzom,

            ujpgetppfpiom: ujpgetppfpiom,
            ujpgetppavrm: ujpgetppavrm,
            ujpgetppfzom: ujpgetppfzom,

            ujpgetnpfpiom: ujpgetnpfpiom,
            ujpgetnpavrm: ujpgetnpavrm,
            ujpgetnpfzom: ujpgetnpfzom,

            RegistarXML: RegistarXML,
            RegistarPDF: RegistarPDF
        });

        function RegistarPDF(registarpdf) {
            var request = $http({
                method: "GET",
                url: apiUri + "UJP/RegistarPDF",
                responseType: "arraybuffer",

                params: {
                    registarpdf: registarpdf

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }
        function RegistarXML(registarxml) {
            var request = $http({
                method: "GET",
                url: apiUri + "UJP/RegistarXML",
                responseType: "arraybuffer",

                params: {
                    registarxml: registarxml

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }


        function MTSPPDF(namepdfmtsp) {
            var request = $http({
                method: "GET",
                url: apiUri + "UJP/MTSPPDF",
                responseType: "arraybuffer",

                params: {
                    namepdfmtsp: namepdfmtsp

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }
        function MTSPXML(namexmlmtsp) {
            var request = $http({
                method: "GET",
                url: apiUri + "UJP/MTSPXML",
                responseType: "arraybuffer",

                params: {
                    namexmlmtsp: namexmlmtsp

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }


        function MONPDF(namepdfmon) {
            var request = $http({
                method: "GET",
                url: apiUri + "UJP/MONPDF",
                responseType: "arraybuffer",

                params: {
                    namepdfmon: namepdfmon

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }
        function MONXML(namexmlmon) {
            var request = $http({
                method: "GET",
                url: apiUri + "UJP/MONXML",
                responseType: "arraybuffer",

                params: {
                    namexmlmon: namexmlmon

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }


        function FZOPDF(namepdffzo) {
            var request = $http({
                method: "GET",
                url: apiUri + "UJP/FZOPDF",
                responseType: "arraybuffer",

                params: {
                    namepdffzo: namepdffzo

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }
        function FZOXML(namexmlfzo) {
            var request = $http({
                method: "GET",
                url: apiUri + "UJP/FZOXML",
                responseType: "arraybuffer",

                params: {
                    namexmlfzo: namexmlfzo

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }




        function EDBPDF(namepdfedb) {
            var request = $http({
                method: "GET",
                url: apiUri + "UJP/EDBPDF",
                responseType: "arraybuffer",

                params: {
                    namepdfedb: namepdfedb

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }
        function EDBXML(namexmledb) {
            var request = $http({
                method: "GET",
                url: apiUri + "UJP/EDBXML",
                responseType: "arraybuffer",

                params: {
                    namexmledb: namexmledb

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }


        function EMBPDF(namepdfemb) {
            var request = $http({
                method: "GET",
                url: apiUri + "UJP/EMBPDF",
                responseType: "arraybuffer",

                params: {
                    namepdfemb: namepdfemb

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }
        function EMBXML(namexmlemb) {
            var request = $http({
                method: "GET",
                url: apiUri + "UJP/EMBXML",
                responseType: "arraybuffer",

                params: {
                    namexmlemb: namexmlemb

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }

        function getEdbEmb(edb_emb) {
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetEdb",
                params: {
                    edb_emb: edb_emb
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getEdbEmbEmb(edb_emb) {
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetEmb",
                params: {
                    edb_emb: edb_emb
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getAnnualRevenuesFZO(edb, year) {
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetAnnualRevenuesFZO",
                params: {
                    edb: edb,
                    year: year
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getAnnualRevenuesKKKS(edb, year) {
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetAnnualRevenuesKKKS",
                params: {
                    edb: edb,
                    year: year
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getAnnualRevenuesMON(edb, year) {
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetAnnualRevenuesMON",
                params: {
                    edb: edb,
                    year: year
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getAnnualRevenuesMTSP(edb, year) {
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetAnnualRevenuesMTSP",
                params: {
                    edb: edb,
                    year: year
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getRegisterOfExciseBonds(edb, number) {
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetRegisterOfExciseBonds",
                params: {
                    edb: edb,
                    number: number
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }

        function ujpgetouavrm(dateTime, service, institution) {
            var test = "test";
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetOU_AVRM",
                responseType: "arraybuffer",
                params: {
                    dateTime: dateTime,
                    service: service,
                    institution: institution
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function ujpgetoufpiom(dateTime, service, institution) {
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetOU_FPIOM",
                responseType: "arraybuffer",
                params: {
                    dateTime: dateTime,
                    service: service,
                    institution: institution
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function ujpgetoufzom(dateTime, service, institution) {
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetOU_FZOM",
                responseType: "arraybuffer",
                params: {
                    dateTime: dateTime,
                    service: service,
                    institution: institution
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }

        function ujpgetppavrm(dateTime, number, service, institution, additionalInfo) {
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetPP_AVRM",
                responseType: "arraybuffer",
                params: {
                    dateTime: dateTime,
                    number: number,
                    service: service,
                    institution: institution,
                    additionalInfo: additionalInfo
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function ujpgetppfpiom(dateTime, number, service, institution, additionalInfo) {
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetPP_FPIOM",
                responseType: "arraybuffer",
                params: {
                    dateTime: dateTime,
                    number: number,
                    service: service,
                    institution: institution,
                    additionalInfo: additionalInfo
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function ujpgetppfzom(dateTime, number, service, institution, additionalInfo) {
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetPP_FZOM",
                responseType: "arraybuffer",
                params: {
                    dateTime: dateTime,
                    number: number,
                    service: service,
                    institution: institution,
                    additionalInfo: additionalInfo
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }

        function ujpgetnpavrm(dateTime, service, institution) {
            var test = "test";
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetNP_AVRM",
                responseType: "arraybuffer",
                params: {
                    dateTime: dateTime,
                    service: service,
                    institution: institution
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function ujpgetnpfpiom(dateTime, service, institution) {
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetNP_FPIOM",
                responseType: "arraybuffer",
                params: {
                    dateTime: dateTime,
                    service: service,
                    institution: institution
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function ujpgetnpfzom(dateTime, service, institution) {
            var request = $http({
                method: 'POST',
                url: apiUri + "UJP/GetNP_FZOM",
                responseType: "arraybuffer",
                params: {
                    dateTime: dateTime,
                    service: service,
                    institution: institution
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
    }])

.controller("UjpCtrl", ['$scope', 'ujpService', '$state', 'alerts',
function ($scope, ujpService, $state, alerts) {

    $scope.show = false;

    $scope.status1 = {
        opened: false
    };

    $scope.open1 = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status1.opened = true;
    };
    
    $scope.checkboxAdditional = {
        value: false
    };

    $scope.ujpEdbEmb = function (edb_emb) {
        ujpService.getEdbEmb(edb_emb)
            .then(function (response) {
                if (response.message != null) {
                    $scope.show = false;
                    alerts.addError(response.message);
                } else {
                    $scope.edb_EmbList = response.edb_EmbList;
                    $scope.namexmledb = response.edbXml;
                    $scope.namepdfedb = response.edbPdf;
                    $scope.show = true;
                }
            });
    };


    $scope.EDBXML = function () {
        var savedXmlname = $scope.namexmledb;
        ujpService.EDBXML(savedXmlname).then(function (result) {
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
    $scope.EDBPDF = function () {
        var savedPdFname = $scope.namepdfedb;
        ujpService.EDBPDF(savedPdFname).then(function (result) {
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



    $scope.EMBXML = function () {
        var savedXmlname = $scope.namexmlemb;
        ujpService.EMBXML(savedXmlname).then(function (result) {
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
    $scope.EMBPDF = function () {
        var savedPdFname = $scope.namepdfemb;
        ujpService.EMBPDF(savedPdFname).then(function (result) {
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



    $scope.FZOXML = function () {
        var savedXmlname = $scope.namexmlfzo;
        ujpService.FZOXML(savedXmlname).then(function (result) {
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
    $scope.FZOPDF = function () {
        var savedPdFname = $scope.namepdffzo;
        ujpService.FZOPDF(savedPdFname).then(function (result) {
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



    $scope.MONXML = function () {
        var savedXmlname = $scope.namexmlmon;
        ujpService.MONXML(savedXmlname).then(function (result) {
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
    $scope.MONPDF = function () {
        var savedPdFname = $scope.namepdfmon;
        ujpService.MONPDF(savedPdFname).then(function (result) {
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




    $scope.MTSPXML = function () {
        var savedXmlname = $scope.namexmlmtsp;
        ujpService.MTSPXML(savedXmlname).then(function (result) {
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
    $scope.MTSPPDF = function () {
        var savedPdFname = $scope.namepdfmtsp;
        ujpService.MONPDF(savedPdFname).then(function (result) {
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



    $scope.ujpEdbEmbEmb = function (edb_emb) {
        ujpService.getEdbEmbEmb(edb_emb)
            .then(function (response) {
                if (response.message != null) {
                    $scope.show = false;
                    alerts.addError(response.message);
                } else {
                    $scope.edb_EmbList = response.edb_EmbList;
                    $scope.namexmlemb = response.edbXml;
                    $scope.namepdfemb = response.edbPdf;
                    $scope.show = true;
                }
            });
    };
    $scope.ujpAnnualRevenuesFZO = function (edb, year) {
        ujpService.getAnnualRevenuesFZO(edb, year)
            .then(function (response) {
                if (response.message != null) {
                    $scope.show = false;
                    alerts.addError(response.message);
                } else {
                    $scope.annualRevenuesFZO = response.annualRevenuesFZO;
                    $scope.namexmlfzo = response.xmlfzo;
                    $scope.namepdffzo = response.pdffzo;
                    $scope.show = true;
                }
            });
    };
    $scope.ujpAnnualRevenuesKKKS = function (edb, year) {
        ujpService.getAnnualRevenuesKKKS(edb, year)
            .then(function (response) {
                if (response.message != null) {
                    $scope.show = false;
                    alerts.addError(response.message);
                } else {
                    $scope.annualRevenueKKKS = response.annualRevenueKKKS;
                    $scope.show = true;
                }
            });
    };
    $scope.ujpAnnualRevenuesMON = function (edb, year) {
        ujpService.getAnnualRevenuesMON(edb, year)
            .then(function (response) {
                if (response.message != null) {
                    $scope.show = false;
                    alerts.addError(response.message);
                } else {
                    $scope.annualRevenuesMON = response.annualRevenuesMON;
                    $scope.namexmlmon = response.monXml;
                    $scope.namepdfmon = response.monPdf;
                    $scope.show = true;
                }
            });
    };
    $scope.ujpAnnualRevenuesMTSP = function (edb, year) {
        ujpService.getAnnualRevenuesMTSP(edb, year)
            .then(function (response) {
                if (response.message != null) {
                    $scope.show = false;
                    alerts.addError(response.message);
                } else {
                    $scope.annualRevenuesMTSP = response.annualRevenuesMTSP;
                    $scope.namexmlmtsp = response.mtspXml;
                    $scope.namepdfmtsp = response.mtspPdf;
                    $scope.show = true;
                }
            });
    };

    $scope.ujpgetouavrm = function (date, service, institution) {
        ujpService.ujpgetouavrm(date, service, institution)
            .then(function (response) {
                if (response.message != null) {
                    alerts.addError(response.message);
                }
                if (response) {
                    
                    var blob = new Blob([response], { type: "application/octet-stream" });
                    navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                    if (navigator.saveBlob) {
                        navigator.saveBlob(blob, "AVRM_" + (date.getDate() < 10 ? "0" +
                            date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                            "_" + service.toUpperCase() + ".pdf");//.txt.gpg
                    } else {
                        saveBlob(blob, "AVRM_" + (date.getDate() < 10 ? "0" +
                            date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                            "_" + service.toUpperCase() + ".pdf");//.txt.gpg
                    }
                }
            });
    };

    $scope.ujpgetoufpiom = function (date, service, institution) {
        ujpService.ujpgetoufpiom(date, service, institution).
        then(function (response) {
            if (response.message != null) {
                alerts.addError(response.message);
            }
            if (response) {

                var blob = new Blob([response], { type: "application/octet-stream" });
                navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                if (navigator.saveBlob) {
                    navigator.saveBlob(blob, "FPIOM_" + (date.getDate() < 10 ? "0" +
                        date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                        "_" + service.toUpperCase() + ".pdf");//.txt.gpg
                } else {
                    saveBlob(blob, "FPIOM_" + (date.getDate() < 10 ? "0" +
                        date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                        "_" + service.toUpperCase() + ".pdf");//.txt.gpg
                }
            }
        });
    };

    $scope.ujpgetoufzom = function (date, service, institution) {
        ujpService.ujpgetoufzom(date, service, institution)
            .then(function (response) {
                if (response.message != null) {
                    alerts.addError(response.message);
                }
                if (response) {

                    var blob = new Blob([response], { type: "application/octet-stream" });
                    navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                    if (navigator.saveBlob) {
                        navigator.saveBlob(blob, "FZOM_" + (date.getDate() < 10 ? "0" +
                            date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                            "_" + service.toUpperCase() + ".pdf");//.txt.gpg
                    } else {
                        saveBlob(blob, "FZOM_" + (date.getDate() < 10 ? "0" +
                            date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                            "_" + service.toUpperCase() + ".pdf");//.txt.gpg
                    }
                }
            });
    };


    $scope.ujpgetppavrm = function (date, number, service, institution, additionalInfo) {
        ujpService.ujpgetppavrm(date, number, service, institution, additionalInfo)
            .then(function (response) {
                if (response.message != null) {
                    alerts.addError(response.message);
                }
                if (response) {

                    var blob = new Blob([response], { type: "application/octet-stream" });
                    navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                    if (navigator.saveBlob) {
                        navigator.saveBlob(blob, "AVRM_" + (date.getDate() < 10 ? "0" +
                            date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                            "_" + service.toUpperCase() + (additionalInfo.toString().toLowerCase() == "true" ? "_D" : "") + ".pdf");//.zip.gpg
                    } else {
                        saveBlob(blob, "AVRM_" + (date.getDate() < 10 ? "0" +
                            date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                            "_" + service.toUpperCase() + (additionalInfo.toString().toLowerCase() == "true" ? "_D" : "") + ".pdf");//.zip.gpg
                    }
                }
            });
    };

    $scope.ujpgetppfpiom = function (date, number, service, institution, additionalInfo) {
        ujpService.ujpgetppfpiom(date, number, service, institution, additionalInfo).
        then(function (response) {
            if (response.message != null) {
                alerts.addError(response.message);
            }
            if (response) {

                var blob = new Blob([response], { type: "application/octet-stream" });
                navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                if (navigator.saveBlob) {
                    navigator.saveBlob(blob, "FPIOM_" + (date.getDate() < 10 ? "0" +
                        date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                        "_" + service.toUpperCase() + (additionalInfo.toString().toLowerCase() == "true" ? "_D" : "") + ".pdf");//.zip.gpg
                } else {
                    saveBlob(blob, "FPIOM_" + (date.getDate() < 10 ? "0" +
                        date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                        "_" + service.toUpperCase() + (additionalInfo.toString().toLowerCase() == "true" ? "_D" : "") + ".pdf");//.zip.gpg
                }
            }
        });
    };

    $scope.ujpgetppfzom = function (date, number, service, institution, additionalInfo) {
        ujpService.ujpgetppfzom(date, number, service, institution, additionalInfo)
            .then(function (response) {
                if (response.message != null) {
                    alerts.addError(response.message);
                }
                if (response) {

                    var blob = new Blob([response], {
                        type: "application/octet-stream"
                    });
                    navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                    if (navigator.saveBlob) {
                        navigator.saveBlob(blob, "FZOM_" + (date.getDate() < 10 ? "0" +
                            date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                            "_" + service.toUpperCase() + (additionalInfo.toString().toLowerCase() == "true" ? "_D" : "") + ".pdf");//.zip.gpg
                    } else {
                        saveBlob(blob, "FZOM_" + (date.getDate() < 10 ? "0" +
                            date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                            "_" + service.toUpperCase() + (additionalInfo.toString().toLowerCase() == "true" ? "_D" : "") + ".pdf");//.zip.gpg
                    }
                }
            });
    };


    $scope.ujpgetnpavrm = function (date, service, institution) {
        ujpService.ujpgetnpavrm(date, service, institution)
            .then(function (response) {
                if (response.message != null) {
                    alerts.addError(response.message);
                }
                if (response) {

                    var blob = new Blob([response], {
                        type: "application/octet-stream"
                    });
                    navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                    if (navigator.saveBlob) {
                        navigator.saveBlob(blob, "AVRM_" + (date.getDate() < 10 ? "0" +
                            date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                            "_" + service.toUpperCase() + ".pdf");//.doc.gpg
                    } else {
                        saveBlob(blob, "AVRM_" + (date.getDate() < 10 ? "0" +
                            date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                            "_" + service.toUpperCase() + ".pdf");//.doc.gpg
                    }
                }
            });
    };

    $scope.ujpgetnpfpiom = function (date, service, institution) {
        ujpService.ujpgetnpfpiom(date, service, institution).
        then(function (response) {
            if (response.message != null) {
                alerts.addError(response.message);
            }
            if (response) {

                var blob = new Blob([response], {
                    type: "application/octet-stream"
                });
                navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                if (navigator.saveBlob) {
                    navigator.saveBlob(blob, "FPIOM_" + (date.getDate() < 10 ? "0" +
                        date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                        "_" + service.toUpperCase() + ".pdf");//.doc.gpg
                } else {
                    saveBlob(blob, "FPIOM_" + (date.getDate() < 10 ? "0" +
                        date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                        "_" + service.toUpperCase() + ".pdf");//.doc.gpg
                }
            }
        });
    };

    $scope.ujpgetnpfzom = function (date, service, institution) {
        ujpService.ujpgetnpfzom(date, service, institution)
            .then(function (response) {
                if (response.message != null) {
                    alerts.addError(response.message);
                }
                if (response) {

                    var blob = new Blob([response], {
                        type: "application/octet-stream"
                    });
                    navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                    if (navigator.saveBlob) {
                        navigator.saveBlob(blob, "FZOM_" + (date.getDate() < 10 ? "0" +
                            date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                            "_" + service.toUpperCase() + ".pdf");//.doc.gpg
                    } else {
                        saveBlob(blob, "FZOM_" + (date.getDate() < 10 ? "0" +
                            date.getDate() : date.getDate()) + ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) + date.getFullYear() +
                            "_" + service.toUpperCase() + ".pdf");//.doc.gpg
                    }
                }
            });
    };


    $scope.RegistarXML = function () {
        var savedXmlname = $scope.registarxml;
        ujpService.RegistarXML(savedXmlname).then(function (result) {
            if (result) {

                var blob = new Blob([result], {
                    type: "application/xml"
                });
                navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                if (navigator.saveBlob) {
                    navigator.saveBlob(blob, savedXmlname);
                } else {
                    saveBlob(blob, savedXmlname);
                }
            }
        });
    };
    $scope.RegistarPDF = function () {
        var savedPdFname = $scope.registarpdf;
        ujpService.RegistarPDF(savedPdFname).then(function (result) {
            if (result) {

                var blob = new Blob([result], {
                    type: "application/pdf"
                });
                navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                if (navigator.saveBlob) {
                    navigator.saveBlob(blob, savedPdFname);
                } else {
                    saveBlob(blob, savedPdFname);
                }
            }

        });
    };





    $scope.ujpRegisterOfExciseBonds = function (edb, number) {
        ujpService.getRegisterOfExciseBonds(edb, number)
            .then(function (response) {
                if (response.message != null) {
                    $scope.show = false;
                    alerts.addError(response.message);
                } else {
                    $scope.exciseBonds = response.exciseBonds;
                    $scope.exciseGoods = response.exciseGoods;
                    $scope.exciseSpaces = response.exciseSpaces;
                    $scope.registarpdf = response.registarPDF;
                    $scope.registarxml = response.registarXML;
                    $scope.show = true;
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



}]);
