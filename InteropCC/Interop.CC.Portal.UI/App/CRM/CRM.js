app.config(function ($stateProvider, appBaseUrl) {

    $stateProvider
        .state("crmtekovnasostojbaindex", {
            title: 'Тековна состојба',
            controller: "CrmCtrl",
            url: "/crm/crmtekovnasostojbaindex",
            templateUrl: 'App/CRM/Templates/CRMTekovnaSostojba'
        })
        .state("crmtekovnasostojba", {
            title: 'Тековна состојба',
            controller: "CrmCtrl",
            url: "/crm/crmtekovnasostojba",
            templateUrl: 'App/CRM/Templates/CRMTekovnaSostojba'
        })
    .state("crmtekovnasostojbaujpindex", {
            title: 'Тековна состојба УЈП',
            controller: "CrmCtrl",
            url: "/crm/crmtekovnasostojbaujpindex",
            templateUrl: 'App/CRM/Templates/CRMTekovnaSostojbaUJP'
        })
    .state("crmtekovnasostojbaujp", {
            title: 'Тековна состојба УЈП',
            controller: "CrmCtrl",
            url: "/crm/crmtekovnasostojbaujp",
            templateUrl: 'App/CRM/Templates/CRMTekovnaSostojbaUJP'
    })
    .state("crmtekovnasostojbaaknindex", {
        title: 'Тековна состојба АКН',
        controller: "CrmCtrl",
        url: "/crm/crmtekovnasostojbaaknindex",
        templateUrl: 'App/CRM/Templates/CRMTekovnaSostojbaAKN'
    }).
    state("crmtekovnasostojbaakn", {
        title: 'Тековна состојба АКН',
        controller: "CrmCtrl",
        url: "/crm/crmtekovnasostojbaakn",
        templateUrl: 'App/CRM/Templates/CRMTekovnaSostojbaAKN'
    })
    .state("crmtekovnasostojbacurmindex", {
            title: 'Тековна состојба ЦУРМ',
            controller: "CrmCtrl",
            url: "/crm/crmtekovnasostojbacurmindex",
            templateUrl: 'App/CRM/Templates/CRMTekovnaSostojbaCURM'
        })
        .state("crmtekovnasostojbacurm", {
            title: 'Тековна состојба ЦУРМ',
            controller: "CrmCtrl",
            url: "/crm/crmtekovnasostojbacurm",
            templateUrl: 'App/CRM/Templates/CRMTekovnaSostojbaCURM'
        })
     .state("crmtekovnasostojbacurmprodindex", {
    title: 'Тековна состојба ЦУРМ',
        controller: "CrmCtrl",
                url: "/crm/crmtekovnasostojbacurmprodindex",
                templateUrl: 'App/CRM/Templates/CRMTekovnaSostojbaCURMProd'
                })
            .state("crmtekovnasostojbacurmprod", {
                    title: 'Тековна состојба ЦУРМ',
                    controller: "CrmCtrl",
                        url: "/crm/crmtekovnasostojbacurmprod",
                        templateUrl: 'App/CRM/Templates/CRMTekovnaSostojbaCURMProd'
            })
        .state("crmlistanapromeniindex", {
            title: 'Листа на промени',
            controller: "CrmCtrl",
            url: "/crm/crmlistanapromeniindex",
            templateUrl: 'App/CRM/Templates/CRMListaNaPromeni'
        })
        .state("crmlistanasubjektiindex", {
            title: 'Листа на субјекти',
            controller: "CrmCtrl",
            url: "/crm/crmlistanasubjektiindex",
            templateUrl: 'App/CRM/Templates/CRMListaNaSubjekti'
        });
        
});

angular.module("CRM", ['ui.bootstrap'])
.service("crmService", ['$http', '$q', 'handleResponseService', 'apiUri',
    function ($http, $q, handleResponseService, apiUri) {
        return ({
            getTekovnaSostojba: getTekovnaSostojba,
            TekovnaSostojbaPDF: TekovnaSostojbaPDF,
            TekovnaSostojbaXML: TekovnaSostojbaXML,
            getTekovnaSostojbaAKN: getTekovnaSostojbaAKN,
            getTekovnaSostojbaUJP: getTekovnaSostojbaUJP,
            getTekovnaSostojbaCURM: getTekovnaSostojbaCURM,
            getTekovnaSostojbaCURMProd: getTekovnaSostojbaCURMProd,
            getListaNaPromeni: getListaNaPromeni,
            getListaNaSubjekti: getListaNaSubjekti
            //uploadFileToUrl: uploadFileToUrl
        });

        function getTekovnaSostojba(edb) {
            var request = $http({
                method: 'POST',
                url: apiUri + "CRM/GetTekovnaSostojba",
                params: {
                    edb: edb
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }
        function getTekovnaSostojbaUJP(edb) {
            var request = $http({
                        method: 'POST',
                        url: apiUri + "CRM/GetTekovnaSostojbaUJP",
                    params: {
                        edb: edb
                }
                });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getTekovnaSostojbaAKN(edb) {
            var request = $http({
                method: 'POST',
                url: apiUri + "CRM/GetTekovnaSostojbaAKN",
                params: {
                    edb: edb
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getTekovnaSostojbaCURM(edb) {
            var request = $http({
                    method: 'POST',
                    url: apiUri + "CRM/GetTekovnaSostojbaCURM",
                        params: {
                            edb: edb
                    }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }
            
        function getListaNaPromeni(date) {
            var request = $http({
                method: 'POST',
                url: apiUri + "CRM/GetListaNaPromeni",
                params: {
                    date: date
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }
        function getListaNaSubjekti(date) {
            var request = $http({
                method: 'POST',
                url: apiUri + "CRM/GetListaNaSubjekti",
                params: {
                    date: date
                }
            });
            return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
        }

            function getTekovnaSostojbaCURMProd(edb) {
            var request = $http({
                    method: 'POST',
                url: apiUri + "CRM/GetTekovnaSostojbaCURMProduc",
                            params: {
                                edb: edb
                        }
            });
                        return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
            ));
            }
        function TekovnaSostojbaXML(tekovnasostojbaxml) {
            var request = $http({
                method: "GET",
                url: apiUri + "CRM/TekovnaSostojbaXML",
                responseType: "arraybuffer",

                params: {
                    tekovnasostojbaxml: tekovnasostojbaxml,

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }

        function TekovnaSostojbaPDF(tekovnasostojbapdf) {
            var request = $http({
                method: "GET",
                url: apiUri + "CRM/TekovnaSostojbaPDF",
                responseType: "arraybuffer",

                params: {
                    tekovnasostojbapdf: tekovnasostojbapdf

                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }
        //function uploadFileToUrl(file) {

        //    var fd = new FormData();
        //    fd.append('file', file);
        //    var uploadUrl = apiUri + "CRM/GetCertPath";
        //    var request = $http.post(uploadUrl, fd, {
        //        transformRequest: angular.identity,
        //        headers: { 'Content-Type': undefined }
        //    });

        //    return (request.then(handleResponseService.handleSuccess//, handleResponseService.handleError
        //    ));
        //};


    }])
    
.controller("CrmCtrl", ['$scope','$location', 'crmService', 'fileUpload', '$state', 'alerts',
function ($scope, $location, crmService, $state, fileUpload, alerts) {
    angular.element("input[type='file']").val(null);
    $scope.registration = {
        edb: "",
        certPassword: ""
    };
    //$scope.uploadFile = function () {
    //    crmService.uploadFileToUrl($scope.registration.myFile).then(function (path) {
    //        $scope.registration.path = path;
    //    });
    //};

    $scope.show = false;
    //var certPath = $scope.registration.path;
    $scope.crmTekovnaSostojba = function () {
        $scope.show = false;
        crmService.getTekovnaSostojba($scope.edb)
            .then(function (response) {
                if (response.message == null) {
                    $scope.show = false;
                    alerts.addError("Грешка при повик на сервисот");
                } else {
                    $scope.info = response.info;
                    $scope.units = response.units;
                    $scope.actors = response.actors;
                    $scope.owners = response.owners;
                    $scope.activities = response.activities;
                    $scope.membership = response.membership;
                    $scope.founding = response.founding;
                    $scope.court = response.court;
                    $scope.tekovnasostojbapdf = response.tekovnaSostojbaPDF;
                    $scope.tekovnasostojbaxml = response.tekovnaSostojbaXML;
                    $scope.show = true;
                }

            });
    };
    $scope.crmTekovnaSostojbaAKN = function () {
        $scope.show = false;
        crmService.getTekovnaSostojbaAKN($scope.edb)
            .then(function (response) {
                if (response.message == null) {
                    $scope.show = false;
                    alerts.addError("Грешка при повик на сервисот");
                } else {
                    $scope.info = response.info;
                    $scope.units = response.units;
                    $scope.actors = response.actors;
                    $scope.owners = response.owners;
                    $scope.activities = response.activities;
                    $scope.membership = response.membership;
                    $scope.founding = response.founding;
                    $scope.tekovnasostojbapdf = response.tekovnaSostojbaPDF;
                    $scope.tekovnasostojbaxml = response.tekovnaSostojbaXML;
                    $scope.show = true;
                }

            });
    };
    $scope.crmTekovnaSostojbaUJP = function () {
        $scope.show = false;
        crmService.getTekovnaSostojbaUJP($scope.edb)
            .then(function (response) {
                if (response.message == null) {
                    $scope.show = false;
                    alerts.addError("Грешка при повик на сервисот");
                } else {
                    $scope.info = response.info;
                    $scope.units = response.units;
                    $scope.actors = response.actors;
                    $scope.owners = response.owners;
                    $scope.activities = response.activities;
                    $scope.membership = response.membership;
                    $scope.founding = response.founding;
                    $scope.court = response.court;
                    $scope.tekovnasostojbapdf = response.tekovnaSostojbaPDF;
                    $scope.tekovnasostojbaxml = response.tekovnaSostojbaXML;
                    $scope.show = true;
                }

            });
    };
    $scope.crmTekovnaSostojbaCURM = function () {
        $scope.show = false;
        crmService.getTekovnaSostojbaCURM($scope.edb)
            .then(function (response) {
                if (response.message == null) {
                    $scope.show = false;
                    alerts.addError("Грешка при повик на сервисот");
                } else {
                    $scope.info = response.info;
                    $scope.actors = response.actors;
                    $scope.owners = response.owners;
                    $scope.founding = response.founding;
                    $scope.tekovnasostojbapdf = response.tekovnaSostojbaPDF;
                    $scope.tekovnasostojbaxml = response.tekovnaSostojbaXML;
                    $scope.show = true;
                }

            });
    };
    $scope.crmTekovnaSostojbaCURMProd = function () {
        $scope.show = false;
        crmService.getTekovnaSostojbaCURMProd($scope.edb)
            .then(function (response) {
                if(response.message == null) {
                    $scope.show = false;
                    alerts.addError("Грешка при повик на сервисот");
                } else {
                    $scope.info = response.info;
                    $scope.actors = response.actors;
                    $scope.owners = response.owners;
                    $scope.tekovnasostojbapdf = response.tekovnaSostojbaPDF;
                    $scope.tekovnasostojbaxml = response.tekovnaSostojbaXML;
                    $scope.show = true;
                }

            });
    };

    $scope.TekovnaSostojbaXML = function () {
        var savedXmlname = $scope.tekovnasostojbaxml;
        crmService.TekovnaSostojbaXML(savedXmlname).then(function (result) {
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
    $scope.TekovnaSostojbaPDF = function () {
        var savedPdFname = $scope.tekovnasostojbapdf;
        crmService.TekovnaSostojbaPDF(savedPdFname).then(function (result) {
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
    // CRM Lista na Promeni
    function formatFromDate(date) {
        var dd = (date.getDate() < 10 ? '0' : '') + date.getDate();
        var MM = ((date.getMonth() + 1) < 10 ? '0' : '') + (date.getMonth() + 1);
        var yyyy = date.getFullYear();
        return (dd + "-" + MM + "-" + yyyy);
    }
    function formatMaxDate(date) {
        var dd = ((date.getDate() - 1) < 10 ? '0' : '') + (date.getDate() - 1);
        var MM = ((date.getMonth() + 1) < 10 ? '0' : '') + (date.getMonth() + 1);
        var yyyy = date.getFullYear();
        return (yyyy + "-" + MM + "-" + dd);
    }
    function formatMinDate(date) {
        var dd = ((date.getDate() - 1) < 10 ? '0' : '') + (date.getDate() - 1);
        var MM = (date.getMonth() < 10 ? '0' : '') + date.getMonth();
        var yyyy = date.getFullYear();
        return (yyyy + "-" + MM + "-" + dd);
    }
    //Checkbox
    //$scope.opt = "Nula";
    //Date picker
    $scope.status1 = {
        opened: false
    };

    $scope.fromDate = new Date();   
    $scope.fromDate = $scope.fromDate.setDate($scope.fromDate.getDate() - 1);

    $scope.status1.opened = false;
    $scope.open1 = function ($event) {
        if($scope.status1.opened == false){
            $event.preventDefault();
            $event.stopPropagation();
            $scope.status1.opened = true;
            //maxDate parsing
            $scope.maxFromDate = new Date();
            $scope.maxFromDate = formatMaxDate($scope.maxFromDate);
            /*

            if ($scope.maxFromDate.getDate() < 10) {
                if (($scope.maxFromDate.getMonth() + 1) < 10) {
                    $scope.maxFromDate = $scope.maxFromDate.getFullYear() + "-" + "0" + ($scope.maxFromDate.getMonth() + 1) + "-" + "0" + ($scope.maxFromDate.getDate() - 1);
                } else {
                    $scope.maxFromDate = $scope.maxFromDate.getFullYear() + "-" + ($scope.maxFromDate.getMonth() + 1) + "-" + "0" + ($scope.maxFromDate.getDate() - 1);
                }
            } else if (($scope.maxFromDate.getMonth() + 1) < 10) {
                if ($scope.maxFromDate.getDate() == 10) {
                    $scope.maxFromDate = $scope.maxFromDate.getFullYear() + "-" + "0" + ($scope.maxFromDate.getMonth() + 1) + "-" + "0" + ($scope.maxFromDate.getDate() - 1);
                } else {
                    $scope.maxFromDate = $scope.maxFromDate.getFullYear() + "-" + "0" + ($scope.maxFromDate.getMonth() + 1) + "-" + ($scope.maxFromDate.getDate() - 1);
                }           
            } else {
                $scope.maxFromDate = $scope.maxFromDate.getFullYear() + "-" + ($scope.maxFromDate.getMonth() + 1) + "-" + ($scope.maxFromDate.getDate() - 1);
            }
            */
            /*
            $scope.minFromDate = new Date();
            if ($scope.minFromDate.getDate() < 10) {
                if ($scope.minFromDate.getMonth() < 10) {
                    $scope.minFromDate = $scope.minFromDate.getFullYear() + "-" + "0" + $scope.minFromDate.getMonth() + "-" + "0" + $scope.minFromDate.getDate();
                } else {
                    $scope.minFromDate = $scope.minFromDate.getFullYear() + "-" + $scope.minFromDate.getMonth() + "-" + "0" + $scope.minFromDate.getDate();
                }
            }else if($scope.minFromDate.getMonth() < 10){
                $scope.minFromDate = $scope.minFromDate.getFullYear() + "-" + "0" + $scope.minFromDate.getMonth() + "-" +  $scope.minFromDate.getDate();
            } else {
                $scope.minFromDate = $scope.minFromDate.getFullYear() + "-" +  $scope.minFromDate.getMonth() + "-" +  $scope.minFromDate.getDate();
            }

            $scope.minFromDate = $scope.minFromDate.getFullYear() + "-" + $scope.minFromDate.getMonth() + "-" + $scope.minFromDate.getDate();

            

            //minDate parsing
            $scope.minFromDate = new Date();
            $scope.minFromDate = $scope.maxFromDate;
            $scope.minFromDate = $scope.minFromDate.split("-");
            $scope.minFromDate[1] = (parseInt($scope.minFromDate[1]) - 1);
            if (parseInt($scope.minFromDate[1]) < 10) {
                $scope.minFromDate[1] = "0" + $scope.minFromDate[1];
            }
            */

            //minDate parsing
            $scope.minFromDate = new Date();
            $scope.minFromDate = formatMinDate($scope.minFromDate);
            
        } else {
            $scope.status1.opened == false;
        }
    };
    $scope.crmListaPromeni = function () {
        $scope.fromDate = new Date($scope.fromDate);
        $scope.fromDate = formatFromDate($scope.fromDate);


        crmService.getListaNaPromeni($scope.fromDate)
            .then(function (response) {
                $scope.test = response;
                $scope.fromDate = new Date();
                $scope.fromDate = $scope.fromDate.setDate($scope.fromDate.getDate() - 1);
                $scope.fromDate = new Date($scope.fromDate);
            });
    }
    $scope.crmListaSubjekti = function () {
        $scope.fromDate = new Date($scope.fromDate);

        crmService.getListaNaSubjekti($scope.fromDate)
            .then(function (response) {
                $scope.test2 = response;
                $scope.fromDate = new Date();
                $scope.fromDate = $scope.fromDate.setDate($scope.fromDate.getDate() - 1);
                $scope.fromDate = new Date($scope.fromDate);
            });
    }
    //crm lista na promeni finished

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
    $scope.expandedLoad1 = function () {
        if ($scope.expandLoad1 == true) {
            $scope.expandLoad1 = false;
        } else {
            $scope.expandLoad1 = true;
        }
    };
    $scope.expandedLoad2 = function () {
        if ($scope.expandLoad2 == true) {
            $scope.expandLoad2 = false;
        } else {
            $scope.expandLoad2 = true;
        }
    };
    $scope.expandedLoad3 = function () {
        if ($scope.expandLoad3 == true) {
            $scope.expandLoad3 = false;
        } else {
            $scope.expandLoad3 = true;
        }
    };
    $scope.expandedLoad4 = function () {
        if ($scope.expandLoad4 == true) {
            $scope.expandLoad4 = false;
        } else {
            $scope.expandLoad4 = true;
        }
    };
    $scope.expandedLoad5 = function () {
        if ($scope.expandLoad5 == true) {
            $scope.expandLoad5 = false;
        } else {
            $scope.expandLoad5 = true;
        }
    };
    $scope.expandedLoad6 = function () {
        if ($scope.expandLoad6 == true) {
            $scope.expandLoad6 = false;
        } else {
            $scope.expandLoad6 = true;
        }
    };
    $scope.expandedLoad7 = function () {
        if ($scope.expandLoad7 == true) {
            $scope.expandLoad7 = false;
        } else {
            $scope.expandLoad7 = true;
        }
    };
}])

;