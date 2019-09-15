'use strict';
app.controller('signupController', function ($scope, $http) {

    $scope.message = "";
    var regInfo = {
        headers: {
            "Content-type": "application/json"
        },
    };

    $scope.signUp = function () {

        var signUpDetails = {
            Email: $scope.newEmail,
            Password: $scope.newPassword,
            ConfirmPassword: $scope.conPassword
        };

        $http.post("http://localhost:58218/api/Account/Register", signUpDetails, regInfo)
            .then(function (response, status) {
                sessionStorage.removeItem('authorisationData');
                $scope.successfulSignUp();
            },
            function (response) {
                var errors = [];
                    for (var key in response.data.ModelState) {
                        for (var i = 0; i < response.data.ModelState[key].length; i++) {
                            errors.push(response.data.ModelState[key][i]);
                        }
                    }
                    $scope.message = "Failed to register user as: "+ errors.join(' ');
                });
            };
});