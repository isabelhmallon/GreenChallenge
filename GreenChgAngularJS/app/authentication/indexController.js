'use strict';
app.controller('indexController', ['$scope', function ($scope) {

    $scope.logOut = function () {
        $scope.username = null;
        $scope.password = null;
        $scope.goToLogin();
        sessionStorage.removeItem('authorisationData');
    };

}]);