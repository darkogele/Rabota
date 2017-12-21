'use strict';

angular.module("InteropTestCommunication", ['ui.bootstrap'])
    .service("interopTestCommunicationService", [
        '$http', '$q', 'handleResponseService', 'apiUri', function($http, $q, handleResponseService, apiUri) {
            return ({
                testCommunication: testCommunication
            });

            function testCommunication(providerRoutingToken) {
                var request = $http({
                    method: 'GET',
                    url: apiUri + "InteropTestCommunication/TestCommunication",
                    params: {
                        providerRoutingToken: providerRoutingToken
                    }
                });
                return (request.then(handleResponseService.handleSuccess));
            }
        }
    ]);
    
