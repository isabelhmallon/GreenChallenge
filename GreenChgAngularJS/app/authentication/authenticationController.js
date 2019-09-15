angular.module("greenAppUser").controller("authenticationController", function ($scope, $http) {

    var serviceBase = 'http://localhost:58218/';

    var _authentication = {
        isAuth: false,
        userName: ""
    };
    $scope.message = "";

    $scope.login = function () {

        var authenticationDetails = "grant_type=password&username=" + $scope.username + "&password=" + $scope.password;

        var requestConfig = {
            headers: {
                "Content-type": "application/x-www-form-urlencoded"
            }
        };

        $http.post(serviceBase + "token", authenticationDetails, requestConfig)

            .success(function (response, status) {

                sessionStorage.setItem('authorisationData', JSON.stringify({ token: response.access_token, userName: $scope.username }));
                $scope.loginStatus = status;
                _authentication.isAuth = true;
                _authentication.userName = $scope.username;
                $scope.goToPickChallenge();

            }).error(function (err, status) {
                $scope.loginStatus = status;
            });

    };
});