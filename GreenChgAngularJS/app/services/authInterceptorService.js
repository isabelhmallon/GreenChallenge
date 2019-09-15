'use strict';
app.factory('authInterceptorService', ['$q',  function ($q, ) {
 
    var authInterceptorServiceFactory = {};
 
    var _request = function (config) {
 
        config.headers = config.headers || {};
 
        var authData = JSON.parse(sessionStorage.getItem('authorisationData'));
        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        }
        return config;
    }
 
    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            $scope.goToLogin();
        }
        return $q.reject(rejection);
    }
 
    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;
    return authInterceptorServiceFactory;
}]);