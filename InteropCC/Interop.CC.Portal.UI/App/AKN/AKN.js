;

app.config(function ($stateProvider, appBaseUrl) {

    $stateProvider
        .state("akncadastralparcelindex", {
            title: 'Катастарска парцела',
            controller: "AknCtrl",
            url: "/akn/akncadastralparcelindex",
            templateUrl: 'App/AKN/Templates/AKNCadastralParcel'
        })
        .state("aknpropertylistindex", {
            title: 'Имотен лист',
            controller: "AknCtrl",
            url: "/akn/aknpropertylistindex",
            templateUrl: 'App/AKN/Templates/AKNPropertyList'
        })
        .state("aknpropertylistdocindex", {
            title: 'Имотен лист документ',
            controller: "AknCtrl",
            url: "/akn/aknpropertylistdocindex/{service:string}",
            templateUrl: 'App/AKN/Templates/AKNPropertyListDoc'
        })
        .state("aknpcadastralplandocindex", {
            title: 'Копија од Катастарски План Документ',
            controller: "AknCtrl",
            url: "/akn/aknpcadastralplandocindex/{service:string}",
            templateUrl: 'App/AKN/Templates/AKNCadastralPlanDoc'
        })
        .state("akndataforifacilitiesdocindex", {
            title: 'Податоци за Инфраструктурни Објекти Документ',
            controller: "AknCtrl",
            url: "/akn/akndataforifacilitiesdocindex/{service:string}",
            templateUrl: 'App/AKN/Templates/AKNDataForIFacilitiesDoc'
        });
});

angular.module("AKN", ['ui.bootstrap'])
    .service("aknService", ['$http', '$q', 'handleResponseService', 'apiUri',
    function ($http, $q, handleResponseService, apiUri) {
        return ({
            getPiomPropertyList: getPiomPropertyList,
            getPiomCadastralParcel: getPiomCadastralParcel,
            getPropertyListDoc: getPropertyListDoc,
            getCadastralPlanDoc: getCadastralPlanDoc,
            getDataForIFacilities: getDataForIFacilities,
            PropertyListXML: PropertyListXML,
            PropertyListPDF: PropertyListPDF,
            KatastarPDF: KatastarPDF,
            KatastarXML: KatastarXML,
            getMunicipalities: getMunicipalities,
            getCadastralMunicipalities: getCadastralMunicipalities
        });

        function getPiomPropertyList(municipality, cadastralMunicipality, noPropertyList) {
            var request = $http({
                method: 'POST',
                url: apiUri + "AKN/GetPropertyList",
                params: {
                    municipality: municipality,
                    cadastralMunicipality: cadastralMunicipality,
                    noPropertyList: noPropertyList
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }

        function getPiomCadastralParcel(municipality, cadastralMunicipality, noCadastralParcel) {
            var request = $http({
                method: 'POST',
                url: apiUri + "AKN/GetCadastralParcel",
                params: {
                    municipality: municipality,
                    cadastralMunicipality: cadastralMunicipality,
                    noCadastralParcel: noCadastralParcel
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getPropertyListDoc(opstina, katastarskaOpstina, showEMB, brImotenList, brParcela) {
            var request = $http({
                method: "GET",
                url: apiUri + "AKN/CreatePropertyListPDF",
                responseType: "arraybuffer",

                params: {
                    opstina: opstina,
                    katastarskaOpstina: katastarskaOpstina,
                    showEMB: showEMB,
                    brImotenList: brImotenList,
                    brParcela: brParcela
                    
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getCadastralPlanDoc(opstina, katastarskaOpstina, showEMB, brImotenList, brParcela) {
            var request = $http({
                method: "GET",
                url: apiUri + "AKN/CreateCadastralPlanPDF",
                responseType: "arraybuffer",

                params: {
                    opstina: opstina,
                    katastarskaOpstina: katastarskaOpstina,
                    showEMB: showEMB,
                    brImotenList: brImotenList,
                    brParcela: brParcela

                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getDataForIFacilities(opstina, katastarskaOpstina, showEMB, brImotenList, brParcela) {
            var request = $http({
                method: "GET",
                url: apiUri + "AKN/CreateDataForIFacilitiesPDF",
                responseType: "arraybuffer",

                params: {
                    opstina: opstina,
                    katastarskaOpstina: katastarskaOpstina,
                    showEMB: showEMB,
                    brImotenList: brImotenList,
                    brParcela: brParcela

                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function PropertyListXML(propertylistxml) {
            var request = $http({
                method: "GET",
                url: apiUri + "AKN/PropertyListXML",
                responseType: "arraybuffer",

                params: {
                    propertylistxml: propertylistxml,

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }

        function PropertyListPDF(propertylistpdf) {
            var request = $http({
                method: "GET",
                url: apiUri + "AKN/PropertyListPDF",
                responseType: "arraybuffer",

                params: {
                    propertylistpdf: propertylistpdf

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }


        function KatastarXML(katastarxml) {
            var request = $http({
                method: "GET",
                url: apiUri + "AKN/KatastarXML",
                responseType: "arraybuffer",

                params: {
                    katastarxml: katastarxml,

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }

        function KatastarPDF(katastarpdf, cadMunVal, munVal) {
            var request = $http({
                method: "GET",
                url: apiUri + "AKN/KatastarPDF",
                responseType: "arraybuffer",

                params: {
                    katastarpdf: katastarpdf
                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }

        function getMunicipalities() {
            var request = $http({
                method: "GET",
                url: apiUri + "AKN/GetMunicipalities",
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }

        function getCadastralMunicipalities(municipalityValue) {
            var request = $http({
                method: "GET",
                url: apiUri + "AKN/GetCadastralMunicipalities",
                params: {
                    municipalityValue: municipalityValue
                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }

    }])

.controller("AknCtrl", ['$scope', 'aknService', '$state', 'alerts','$stateParams',
function ($scope, aknService, $state, alerts, $stateParams) {
    
    var service = $stateParams.service;

    $scope.myErrorClass = "my-has-error";

    $scope.validateFields = function () {

        if (service == "ild" || service == "pio") {
            if ($scope.opstina && $scope.katastarskaOpstina) {
                if ($scope.showEMB == true || $scope.showEMB == false) {
                    if ($scope.brImotenList) {
                        return true;
                    }
                }
            } else {
                false;
            }
        }
        if (service == "kkp") {
            if ($scope.opstina && $scope.katastarskaOpstina) {
                if ($scope.showEMB == true || $scope.showEMB == false) {
                    if ($scope.brParcela) {
                        return true;
                    }
                }
            } else {
                false;
            }
        }
    }

    
    $scope.show = false;
    $scope.expand = false;

    aknService.getMunicipalities().then(function (municipalities) {
        $scope.municipalities = municipalities;
    });

    $scope.changeMunicipality = function (municipalityValue) {
        aknService.getCadastralMunicipalities(municipalityValue).then(function (cadastralMunicipalities) {
            $scope.cadastralMunicipalities = cadastralMunicipalities;
        });
    };

    $scope.aknPropertyList = function (municipalityValue, cadastralMunicipalityValue, noPropertyList) {
        $scope.show = false;
        aknService.getPiomPropertyList(municipalityValue, cadastralMunicipalityValue, noPropertyList)
            .then(function (response) {
                $scope.loadsList = response.loadsList;
                $scope.objectsList = response.objectsList;
                $scope.ownersList = response.ownersList;
                $scope.parcelsList = response.parcelsList;
                $scope.dateItem = response.date;
                $scope.messageItem = response.message;
                $scope.properylistxml = response.propertylistXML;
                $scope.properylistpdf = response.propertylistPDF;
                $scope.show = true;
            });
    };

    $scope.aknCadastralParcel = function (municipality, cadastralMunicipalityValue, noCadastralParcel) {
        $scope.show = false;
        aknService.getPiomCadastralParcel(municipality, cadastralMunicipalityValue, noCadastralParcel)
            .then(function (response) {
                $scope.attributesList = response.attributesList;
                $scope.messageItem = response.message;
                $scope.katastarxml = response.cadastralParcelXML;
                $scope.katastarpdf = response.cadastralParcelPDF;
                $scope.show = true;
            });
    };

    $scope.aknPropertyListDoc = function (opstina, katastarskaOpstina, showEMB, brImotenList, brParcela) {
        aknService.getPropertyListDoc(opstina, katastarskaOpstina, showEMB, brImotenList, brParcela).
        then(function (response) {
            if (response.message != null) {
                alerts.addError(response.message);
            }
            if (response) {

                var blob = new Blob([response], { type: "text/plain" });
                var name = "ImotenListDokument.pdf";
                navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                if (navigator.saveBlob) {
                    navigator.saveBlob(blob, name);
                } else {
                    saveBlob(blob, name);
                }
            }
        });
    };
    $scope.aknCadastralPlanDoc = function (opstina, katastarskaOpstina, showEMB, brImotenList, brParcela) {
        aknService.getCadastralPlanDoc(opstina, katastarskaOpstina, showEMB, brImotenList, brParcela).
        then(function (response) {
            if (response.message != null) {
                alerts.addError(response.message);
            }
            if (response) {

                var blob = new Blob([response], { type: "text/plain" });
                var name = "KopijaOdKatastarskiPlanDokument.pdf";
                navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                if (navigator.saveBlob) {
                    navigator.saveBlob(blob, name);
                } else {
                    saveBlob(blob, name);
                }
            }
        });
    };
    $scope.aknDataForIFacilities = function (opstina, katastarskaOpstina, showEMB, brImotenList, brParcela) {
        aknService.getDataForIFacilities(opstina, katastarskaOpstina, showEMB, brImotenList, brParcela).
        then(function (response) {
            if (response.message != null) {
                alerts.addError(response.message);
            }
            if (response) {

                var blob = new Blob([response], { type: "text/plain" });
                var name = "PodatociZaInfrastrukturniObjekti.pdf";
                navigator.saveBlob = navigator.msSaveOrOpenBlob || navigator.msSaveBlob || navigator.mozSaveBlob || navigator.webkitSaveBlob;
                if (navigator.saveBlob) {
                    navigator.saveBlob(blob, name);
                } else {
                    saveBlob(blob, name);
                }
            }
        });
    };

    $scope.PropertyListXML = function () {
        var savedXmlname = $scope.properylistxml;
        aknService.PropertyListXML(savedXmlname).then(function (result) {
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
    $scope.PropertyListPDF = function () {
        var savedPdFname = $scope.properylistpdf;
        aknService.PropertyListPDF(savedPdFname).then(function (result) {
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



    $scope.KatastarXML = function () {
        var savedXmlname = $scope.katastarxml;
        aknService.KatastarXML(savedXmlname).then(function (result) {
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
    $scope.KatastarPDF = function () {
        var savedPdFname = $scope.katastarpdf;
        aknService.KatastarPDF(savedPdFname).then(function (result) {
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

    $scope.expandedParcel = function () {
        if ($scope.expandParcel == true) {
            $scope.expandParcel = false;
        } else {
            $scope.expandParcel = true;
        }
    };

    $scope.expandedAttribute = function () {
        if ($scope.expandAttribute == true) {
            $scope.expandAttribute = false;
        } else {
            $scope.expandAttribute = true;
        }
    };

}]);
