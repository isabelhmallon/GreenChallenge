angular.module("greenAppUser").controller("mainController", function ($scope, $http, authInterceptorService) {
    $scope.loginPage = true;
    $scope.register = false;
    $scope.homepg = false;
    $scope.homepg2 = false;
    $scope.dayChallenge = false;
    $scope.accountMenu = false;
    $scope.changePass = false;
    $scope.pickChallenge = false;
    $scope.graph = false;
    $scope.sucessfulRegistration = false;

    $scope.switchViews = function () {
        $scope.loginPage = false;
        $scope.register = false;
        $scope.homepg = false;
        $scope.dayChallenge = false;
        $scope.accountMenu = false;
        $scope.changePass = false;
        $scope.pickChallenge = false;
        $scope.graph = false;
        $scope.sucessfulRegistration = false;
    };

    //Function start up page - changes view to allow an individual to become a user
    $scope.beginSignUp = function () {
        $scope.switchViews();
        $scope.register = true;
    };

    //Navigation function
    $scope.goToLogin = function () {
        $scope.switchViews();
        $scope.loginPage = true;
    };

    //API service
    var serviceBase = 'http://localhost:58218/';

    //Go to the home page function
    $scope.goToHome = function () {
        $scope.switchViews();
        $scope.homepg = true;
        var config = authInterceptorService.request({});
        $scope.currentUser = JSON.parse(sessionStorage.getItem('authorisationData')).userName;
        //get userchallenge class
        $http.get(serviceBase + "api/UserChallenges/" + $scope.currentChallenge.id, config)
            .success(function (response) {
                $scope.userChallengeInfo = response;
                $scope.daysOpened = []
                $scope.userChallengeInfo.days.forEach(day => {
                    $scope.daysOpened.push(day.dayNumber)
                });
            })
            .error(function (status) {
                $scope.userChallengesStatus = status;
            });
    };

    //Check if day has been opened 
    $scope.dayEnabled = function (dayNo) {
        if ($scope.daysOpened) {
            var largestDayNo = $scope.daysOpened.reduce(function (x, y) {
                return (x > y) ? x : y;
            });
            return dayNo <= (largestDayNo + 1);
        }
        return false;
    }

    //Cancels sign-up process and goes back to login page
    $scope.cancelSignUp = function () {
        $scope.switchViews();
        $scope.loginPage = true;
    };

    //Navigation function 
    $scope.successfulSignUp = function () {
        $scope.switchViews();
        $scope.sucessfulRegistration = true;
    };

    //Load day infomation
    $scope.goToThisDay = function (dayNo) {
        $scope.dayNo = dayNo;
        $scope.switchViews();
        $scope.dayChallenge = true;
        var config = authInterceptorService.request({});
        var postDay = {
            dayNumber: dayNo,
            userChallengeId: $scope.userChallengeInfo.id,
            dayCompleted: false
        }
        $scope.dayId = null;
        var day = $scope.userChallengeInfo.days.filter(function (findday) {
            return findday.dayNumber == dayNo;
        });
        //check if day exists
        if (day.length > 0) {
            $scope.dayId = day[0].id;
        }

        if ($scope.dayId != null) {
            $scope.getDayInfo();
        }
        else {
            $http.post(serviceBase + "api/Days", postDay, config)
                .success(function (response) {
                    $scope.dayId = response.id;
                    $scope.dayInfo = response;
                    $scope.getDayInfo();
                });
        }
        $http.get(serviceBase + "api/Challenges/" + $scope.userChallengeInfo.challengeId, config)
            .success(function (response) {
                $scope.dayTasks = response;
            })
            .error(function (status) {
            });
    };

    //GET info for day
    $scope.getDayInfo = function () {
        var config = authInterceptorService.request({});
        $http.get(serviceBase + "api/Days/" + $scope.dayId, config)
            .success(function (response) {
                $scope.dayInfo = response;
            })
    };

    //check if task has been completed 
    $scope.taskCompleted = function (taskId) {
        var tasksCompleted = $scope.dayInfo.tasksCompleted;
        if (tasksCompleted.filter(userTaskLog => userTaskLog.challengeTaskId == taskId).length > 0) {
            return true;
        }
        return false;
    };

    //Mark task as complete
    $scope.completeTask = function (dayTask) {
        var postCompleteTask = {
            username: $scope.currentUser,
            dateCompleted: new Date(),
            complete: true,
            dayId: $scope.dayId,
            challengeTaskId: dayTask.id
        };
        var config = authInterceptorService.request({});
        $http.post(serviceBase + "api/UserTaskLogs", postCompleteTask, config)
            .success(function (response) {
                $scope.getDayInfo();
            })
            .error(function (status) {
            })
    };

    //Load challenges
    $scope.goToPickChallenge = function () {
        $scope.currentUser = JSON.parse(sessionStorage.getItem('authorisationData')).userName;
        $scope.switchViews();
        var config = authInterceptorService.request({});
        $scope.pickChallenge = true;
        $http.get(serviceBase + "api/Challenges", config)
            .success(function (response) {
                $scope.challenges = response;
                $scope.getCurrentUserChallenges();
            })
            .error(function (status) {
                $scope.pickChallengeStatus = status;
            });
    };

    //GET challenges user is linked to 
    $scope.getCurrentUserChallenges = function () {
        var config = authInterceptorService.request({});
        $http.get(serviceBase + "api/UserChallenges?username=" + $scope.currentUser, config)
            .success(function (response) {
                $scope.currentUsersChallenges = response;
            })
            .error(function (status) {
                $scope.usersChallengesStatus = status;
            });
    };

    //Pick this challenge function
    $scope.pickThisChallenge = function (challenge) {
        var config = authInterceptorService.request({});
        var challengeCheck = $scope.currentUsersChallenges.filter(function (findChallenge) {

            return findChallenge.challengeId == challenge.id;
        });

        //check if user is linked to challenge
        if (challengeCheck.length > 0) {
            $scope.currentChallenge = challengeCheck[0];
            $scope.goToHome();
        }
        else {
            var postChallenge = {
                username: $scope.currentUser,
                challengeCompleted: false,
                challengeId: challenge.id
            }
            $http.post(serviceBase + "api/UserChallenges", postChallenge, config)
                .success(function (response) {
                    $scope.currentChallenge = response;
                    $scope.goToHome();
                })
                .error(function (status) {
                })
        };
    };

    //Navigation function
    $scope.backToHome = function () {
        $scope.switchViews();
        $scope.homepg = true;
    };

    //Navigation function
    $scope.goToAccountMenu = function () {
        $scope.switchViews();
        $scope.accountMenu = true;
    };

    //Navigation function
    $scope.goToChangePwd = function () {
        $scope.switchViews();
        $scope.changePass = true;
    };

    //Change Password function
    $scope.changePwd = function () {
        var configInfo = {
            headers: {
                "Content-type": "application/json",
                "Authorization": "Bearer " + JSON.parse(sessionStorage.getItem('authorisationData')).token
            }
        };
        var chgPwd = {
            OldPassword: $scope.oldPassword,
            NewPassword: $scope.newPassword,
            ConfirmPassword: $scope.conNewPassword
        };
        $http.post(serviceBase + "/api/Account/ChangePassword", chgPwd, configInfo)
            .then(function (response, status) {
                $scope.savedSuccessfully = true;
                $scope.message = "User has changed password.";
                $scope.chgPassStatus = status;
            },
                function (response) {
                    $scope.chgPassStatus = response.status;

                    var errors = [];
                    for (var key in response.data.ModelState) {
                        for (var i = 0; i < response.data.ModelState[key].length; i++) {
                            errors.push(response.data.ModelState[key][i]);
                        }
                    }
                    $scope.message = "Failed to change password as: " + errors.join(' ');
                });
    };

    //Create statistics function
    $scope.goToStats = function () {
        $scope.switchViews();
        $scope.graph = true;
        var daysToFilter = $scope.userChallengeInfo.days;
        var x;
        $scope.data = [];
        for (x of daysToFilter) {
            $scope.data.push({
                value: x.tasksCompleted.length,
                label: x.dayNumber
            });
        };
        //Determine max value
        $scope.max = 0;

        var arrLength = $scope.data.length;
        for (var i = 0; i < arrLength; i++) {
            // Find the max x axis value
            if ($scope.data[i].value > $scope.max)
                $scope.max = $scope.data[i].value;
        };
    };

    //Statistics graph
    $scope.width = 600;
    $scope.height = 400;
    $scope.yAxis = "Tasks Completed";
    $scope.xAxis = "Day";
});