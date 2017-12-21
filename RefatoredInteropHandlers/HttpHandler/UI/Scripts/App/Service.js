app.service("institutionAService", ['$http', 'apiUrl', function ($http, apiUrl) {
    this.getPropertyList = function() {
        return $http({
            method: 'GET',
            url: apiUrl + "callinghandler/callhandler"
            //params: {
            //    username: username,
            //    password: password,
            //    opstina: opstina,
            //    katastarskaOpstina: katastarskaOpstina,
            //    brojImotenList: brojImotenList
            //}
        });
    };
}]);