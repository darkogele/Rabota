app.config(function ($stateProvider, appBaseUrl) {

    $stateProvider
        .state("curmdataforexecutedexportindex", {
            title: 'Податоци за извршен извоз',
            controller: "CurmCtrl",
            url: "/curm/curmdataforexecutedexportindex",
            templateUrl: 'App/CURM/Templates/CURMDataForExecutedExport'
        })
        .state("curmdataforexecutedexport", {
            title: 'Податоци за извршен извоз',
            controller: "CurmCtrl",
            url: "/curm/curmdataforexecutedexport",
            templateUrl: 'App/CURM/Templates/CURMDataForExecutedExport'
        })
        .state("curmdataforexecutedimportindex", {
            title: 'Податоци за извршен увоз',
            controller: "CurmCtrl",
            url: "/curm/curmdataforexecutedimportindex",
            templateUrl: 'App/CURM/Templates/CURMDataForExecutedImport'
        })
        .state("curmdataforexecutedimport", {
            title: 'Податоци за извршен увоз',
            controller: "CurmCtrl",
            url: "/curm/curmdataforexecutedimport",
            templateUrl: 'App/CURM/Templates/CURMDataForExecutedImport'
        })
        .state("curmsinglecustomsdocumentindex", {
            title: 'Единствен царински документ',
            controller: "CurmCtrl",
            url: "/curm/curmsinglecustomsdocumentindex",
            templateUrl: 'App/CURM/Templates/CURMSingleCustomsDocument'
        })
        .state("curmsinglecustomsdocument", {
            title: 'Единствен царински документ',
            controller: "CurmCtrl",
            url: "/curm/curmsinglecustomsdocument",
            templateUrl: 'App/CURM/Templates/CURMSingleCustomsDocument'
        });
});

angular.module("CURM", ['ui.bootstrap'])
    .service("curmService", ['$http', '$q', 'handleResponseService', 'apiUri',
    function ($http, $q, handleResponseService, apiUri) {
        return ({
            getDataForExecutedExport: getDataForExecutedExport,
            getDataForExecutedImport: getDataForExecutedImport,
            getSingleCustomsDocument: getSingleCustomsDocument,
            DataForExportPDF: DataForExportPDF,
            DataForExportXML: DataForExportXML,
            DataForImportPDF: DataForImportPDF,
            DataForImportXML: DataForImportXML,
            SingleDocumentXML: SingleDocumentXML,
            SingleDocumentPDF: SingleDocumentPDF
        });

        function getDataForExecutedExport(edb, amountOfExportFrom, amountOfExportTo, monthOfExportFrom, monthOfExportTo, yearOfExportFrom, yearOfExportTo) {
            var request = $http({
                method: 'POST',
                url: apiUri + "CURM/GetDataForExecutedExport",
                params: {
                    edb: edb,
                    amountOfExportFrom: amountOfExportFrom,
                    amountOfExportTo: amountOfExportTo,
                    monthOfExportFrom: monthOfExportFrom,
                    monthOfExportTo: monthOfExportTo,
                    yearOfExportFrom: yearOfExportFrom,
                    yearOfExportTo: yearOfExportTo
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function DataForImportPDF(namepdfimport) {
            var request = $http({
                method: "GET",
                url: apiUri + "CURM/DataForImportPDF",
                responseType: "arraybuffer",

                params: {
                    namepdfimport: namepdfimport

                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function DataForImportXML(namexmlimport) {
            var request = $http({
                method: "GET",
                url: apiUri + "CURM/DataForImportXML",
                responseType: "arraybuffer",

                params: {
                    namexmlimport: namexmlimport,

                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }

        function DataForExportPDF(namepdf) {
            var request = $http({
                method: "GET",
                url: apiUri + "CURM/DataForExportPDF",
                responseType: "arraybuffer",

                params: {
                    namepdf: namepdf
                       
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function DataForExportXML(namexml) {
            var request = $http({
                method: "GET",
                url: apiUri + "CURM/DataForExportXML",
                responseType: "arraybuffer",

                params: {
                    namexml: namexml

                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function SingleDocumentPDF(namepdfdocument) {
            var request = $http({
                method: "GET",
                url: apiUri + "CURM/SingleDocumentPDF",
                responseType: "arraybuffer",

                params: {
                    namepdfdocument: namepdfdocument

                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function SingleDocumentXML(namexmldocument) {
            var request = $http({
                method: "GET",
                url: apiUri + "CURM/SingleDocumentXML",
                responseType: "arraybuffer",

                params: {
                    namexmldocument: namexmldocument

                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getDataForExecutedImport(edb, amountOfImportFrom, amountOfImportTo, amountOfImportTaxFrom, amountOfImportTaxTo, monthOfImportFrom, monthOfImportTo, yearOfImportFrom, yearOfImportTo) {
            var request = $http({
                method: 'POST',
                url: apiUri + "CURM/GetDataForExecutedImport",
                params: {
                    edb: edb,
                    amountOfImportFrom: amountOfImportFrom,
                    amountOfImportTo: amountOfImportTo,
                    amountOfImportTaxFrom: amountOfImportTaxFrom,
                    amountOfImportTaxTo: amountOfImportTaxTo,
                    monthOfImportFrom: monthOfImportFrom,
                    monthOfImportTo: monthOfImportTo,
                    yearOfImportFrom: yearOfImportFrom,
                    yearOfImportTo: yearOfImportTo
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getSingleCustomsDocument(year, edbOfShippingCompany, numberOfCustomsOffice, regNumber) {
            var request = $http({
                method: 'POST',
                url: apiUri + "CURM/GetSingleCustomsDocument",
                params: {
                    year: year,
                    edbOfShippingCompany: edbOfShippingCompany,
                    numberOfCustomsOffice: numberOfCustomsOffice,
                    regNumber: regNumber
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
    }])

.controller("CurmCtrl", ['$scope', 'curmService', '$state', 'alerts',
function ($scope, curmService, $state, alerts) {

    $scope.show = false;
    
    $scope.today = function () {
        $scope.year = new Date();
    };
    $scope.today();
    $scope.clear = function () {
        $scope.year = null;
    };
    //edinstven carinski dokument, i pikeri za uvoz/izvoz - parametar 'godina od'
    $scope.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status.opened = true;
    };
    //za pikeri uvoz/izvoz - parametar 'godina do'
    $scope.open1 = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status1.opened = true;
    };
    //za pikeri uvoz/izvoz - parametar 'mesec od'
    $scope.open2 = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status2.opened = true;
    };
    //za pikeri uvoz/izvoz  - parametar 'mesec do'
    $scope.open3 = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status3.opened = true;
    };
    
    $scope.setDate = function (year, month, day) {
        $scope.year = new Date(year, month, day);
    };
    
    $scope.dateOptions = {
        formatYear: 'yyyy',
        startingDay: 1,
        minMode: 'year'
    };
    //uvoz/izvoz 'mesec od' i 'mesec do', modot da e mesec
    $scope.dateOptions1 = {
        //formatYear: 'yyyy',
        startingDay: 1,
        minMode: 'month',
        maxMode: 'month'
    };
    
    $scope.formats = ['yyyy'];
    $scope.format = $scope.formats[0];
    //uvoz/izvoz 'mesec od' i 'mesec do', format za meseci
    $scope.formats1 = ['M'];
    $scope.format1 = $scope.formats1[0];

    $scope.status = {
        opened: false
    };
    //za pikeri uvoz/izvoz - parametar 'godina do'
    $scope.status1 = {
        opened: false
    };
    //za pikeri uvoz/izvoz - parametar 'mesec od'
    $scope.status2 = {
        opened: false
    };
    //za pikeri uvoz/izvoz - parametar 'mesec do'
    $scope.status3 = {
        opened: false
    };
    
    $scope.curmDataForExecutedExport = function (edb, amountOfExportFrom, amountOfExportTo, monthOfExportFrom, monthOfExportTo, yearOfExportFrom, yearOfExportTo) {
        var monthfromtemp = null;
        var monthtotemp = null;
        var yearfromtemp = null;
        var yeartotemp = null;
        
        if (monthOfExportFrom != null && monthOfExportFrom != 'undefined' && monthOfExportFrom != "") {
                monthfromtemp = (monthOfExportFrom.getMonth() + 1);
                }
        if (monthOfExportTo != null && monthOfExportTo != 'undefined' && monthOfExportTo != "") {
                monthtotemp = (monthOfExportTo.getMonth() +1);
                }
        if(yearOfExportFrom != null && yearOfExportFrom != 'undefined' && yearOfExportFrom != "") {
                yearfromtemp = yearOfExportFrom.getFullYear();
                }
        if (yearOfExportTo != null && yearOfExportTo != 'undefined' && yearOfExportTo != "") {
                yeartotemp = yearOfExportTo.getFullYear();
                }
        $scope.show = false;
        curmService.getDataForExecutedExport(edb, amountOfExportFrom, amountOfExportTo, monthfromtemp, monthtotemp, yearfromtemp, yeartotemp)
            .then(function (response) {
                if (response.message != null) {
                    $scope.show = false;
                    alerts.addError("ГРЕШКА");
                } else {
                    $scope.executedExportList = response.executedExportList;
                    $scope.namexml = response.fileXMLName;
                    $scope.namepdf = response.filePDFName;
                    $scope.show = true;
                }
            });
    };
    $scope.DataForExportXML = function() {
        var savedXmlname = $scope.namexml;
        curmService.DataForExportXML(savedXmlname).then(function(result) {
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
    $scope.DataForExportPDF = function() {
        var savedPdFname = $scope.namepdf;
        curmService.DataForExportPDF(savedPdFname).then(function(result) {
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
    $scope.DataForImportXML = function () {
        var savedXmlname = $scope.namexmlimport;
        curmService.DataForImportXML(savedXmlname).then(function (result) {
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
    $scope.DataForImportPDF = function() {
        var savedPdFname = $scope.namepdfimport;
        curmService.DataForImportPDF(savedPdFname).then(function(result) {
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
    

    $scope.SingleDocumentXML = function () {
        var savedXmlname = $scope.namexmldocument;
        curmService.SingleDocumentXML(savedXmlname).then(function (result) {
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
    $scope.SingleDocumentPDF = function () {
        var savedPdFname = $scope.namepdfdocument;
        curmService.SingleDocumentPDF(savedPdFname).then(function (result) {
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

    $scope.curmDataForExecutedImport = function (edb, amountOfImportFrom, amountOfImportTo, amountOfImportTaxFrom, amountOfImportTaxTo, monthOfImportFrom, monthOfImportTo, yearOfImportFrom, yearOfImportTo) {
        var monthfromtemp = null;
        var monthtotemp = null;
        var yearfromtemp = null;
        var yeartotemp = null;
        if (monthOfImportFrom != null && monthOfImportFrom != 'undefined' && monthOfImportFrom != "") {
            monthfromtemp = (monthOfImportFrom.getMonth() + 1);
        }
        if (monthOfImportTo != null && monthOfImportTo != 'undefined' && monthOfImportTo != "") {
            monthtotemp = (monthOfImportTo.getMonth() + 1);
        }
        if (yearOfImportFrom != null && yearOfImportFrom != 'undefined' && yearOfImportFrom != "") {
            yearfromtemp = yearOfImportFrom.getFullYear();
        }
        if (yearOfImportTo != null && yearOfImportTo != 'undefined' && yearOfImportTo != "") {
            yeartotemp = yearOfImportTo.getFullYear();
        }
        $scope.show = false;
        curmService.getDataForExecutedImport(edb, amountOfImportFrom, amountOfImportTo, amountOfImportTaxFrom, amountOfImportTaxTo, monthfromtemp, monthtotemp, yearfromtemp, yeartotemp)
            .then(function (response) {
                if (response.message != null) {
                    $scope.show = false;
                    alerts.addError("ГРЕШКА");
                } else {
                    $scope.executedImportList = response.executedImportList;
                    $scope.namexmlimport = response.fileXMLNameImport;
                    $scope.namepdfimport = response.filePDFNameImport;
                   
                    $scope.show = true;
                }
            });
    };
    $scope.curmSingleCustomsDocument = function (year, edbOfShippingCompany, numberOfCustomsOffice, regNumber) {
        var yeartemp = null;
        if (year != null && year != 'undefined' && year != "") {
            yeartemp = year.getFullYear();
        }
        $scope.show = false;
        curmService.getSingleCustomsDocument(yeartemp, edbOfShippingCompany, numberOfCustomsOffice, regNumber)
            .then(function (response) {
                if (response.message != null) {
                    $scope.show = false;
                    alerts.addError("ГРЕШКА");
                } else {
                    $scope.generalData = response.generalData;
                    $scope.itemData = response.itemData;
                    $scope.exporterData = response.exporterData;
                    $scope.importerData = response.importerData;
                    $scope.namexmldocument = response.fileXMLNameDocument;
                    $scope.namepdfdocument = response.filePDFNameDocument;
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
