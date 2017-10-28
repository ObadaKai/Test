(function () {
    var myApp = angular.module('MyApp', []);


    myApp.controller('ApStudentCtrl', ['$http', '$scope', function ($http, $scope) {
        $scope.ShowForm = true;
        var Student = {};
        $scope.Warning = false;
        $scope.Success = false;
        $scope.StudentCreate = function () {
            Student = {
                "id": $scope.id, "BDate": $scope.BDate,
                "Name": $scope.Name, "Surname": $scope.Surname,
                "Certificate": $scope.Certificate, "Mark": $scope.Mark,
                "State": $scope.State, "SDate": $scope.SDate,
                "Regimentid": $scope.Regimentid, "Stageid": $scope.Stageid, "Centerid": $scope.Centerid,
            };
            $http({ method: 'POST', url: '/Students/Create', data: Student }).then(function successCallback(response) {

                if (response.data) {
                    Student = {};
                    $scope.StCrForm.$setPristine();
                    $scope.StCrForm.$setUntouched();
                    $scope.id = null;
                    $scope.BDate = null;
                    $scope.Name = null;
                    $scope.Surname = null;
                    $scope.Certificate = null;
                    $scope.Mark = null;
                    $scope.State = null;
                    $scope.SDate = null;
                    $scope.EDate = null;
                    $scope.Regimentid = null;
                    $scope.Stageid = null;
                    $scope.Centerid = null;
                    $scope.Warning = false;
                    $scope.Success = true;
                }
                else {
                    $scope.Success = false;
                    $scope.Warning = true;
                }

            });
        };

    }]);


    myApp.controller('ApExamCtrl', ['$http', '$scope', function ($http, $scope) {
        $scope.ShowForm = true;
        $scope.Date = new Date();
        $http({ method: 'GET', url: '/Json/FetchStudentsOfDate' }).then(function successCallback(response) {
            $scope.Students = response.data;
        });
        Desc, Subjectid, Studentid, Stageid, ExamTypeid, Mark, Date

        $scope.Warning = false;
        $scope.Success = false;
        $scope.ExamCreate = function () {
            var Exam = {
                'Desc': $scope.Desc, 'Subjectid': $scope.Subjectid, 'Studentid': $scope.Studentid,
                'Stageid': $scope.Stageid, 'ExamTypeid': $scope.ExamTypeid,
                'Mark': $scope.Mark, 'Date': $scope.Date
            };
            $http({ method: 'POST', url: '/Examinations/Create', data: Exam }).then(function successCallback(response) {


                if (response.data) {
                    Exam = {};
                    $scope.ExCrFrom.$setPristine();
                    $scope.ExCrFrom.$setUntouched();
                    $scope.Desc = null;
                    $scope.Subjectid = null;
                    $scope.Stageid = null;
                    $scope.ExamTypeid = null;
                    $scope.Mark = null;
                    $scope.Date = null;
                    $scope.Studentid = null;
                    $scope.Warning = false;
                    $scope.Success = true;
                    $http({ method: 'GET', url: '/Json/FetchStudentsOfDate' }).then(function successCallback(response) {
                        $scope.Students = response.data;
                    });
                }
                else {
                    $scope.Success = false;
                    $scope.Warning = true;

                }
            });
        };
        $scope.ChangeDate = function () {
            var date = { 'Fielpate': $scope.Date, 'Subjectid': $scope.Subjectid };
            $http({ method: 'Post', url: '/Json/FetchStudentsOfDate', data: date }).then(function successCallback(response) {
                $scope.Students = response.data;
            });
        };





    }]);


    myApp.controller('SearchCtrl', ['$http', '$scope','$filter', function ($http, $scope) {
        
        $scope.ExamSearchBox = function () {
            if ($scope.ExamSearchBoxData || $scope.ExamSearchBoxDate) {
                var ToSenh2ext = { 'SearchBoxData': $scope.ExamSearchBoxData, 'SearchBoxDate': $scope.ExamSearchBoxDate };
                $http({ method: 'POST', url: '/Json/SearchExams', data: ToSenh2ext }).then(function successCallback(response) {
                    $scope.ExamRazorForm = true;
                    $scope.ExamAngularForm = true;
                    $scope.Exams = response.data;
                    angular.forEach($scope.Exams, function (value, key) {
                        value.Date = new Date(parseInt(value.Date.substr(6)));
                    });
                });
            }
            else {
                $scope.ExamRazorForm = false;
                $scope.ExamAngularForm = false;
            }
        };

        $scope.StudentSearchBox = function () {
            if ($scope.StudentSearchBoxData || $scope.StudentSearchBoxDate) {
                var ToSenh2ext = { 'SearchBoxData': $scope.StudentSearchBoxData, 'SearchBoxDate': $scope.StudentSearchBoxDate };
                $http({ method: 'POST', url: '/Json/SearchStudents', data: ToSenh2ext }).then(function successCallback(response) {
                    $scope.StudentRazorForm = true;
                    $scope.StudentAngularForm = true;
                    $scope.Exams = response.data;
                    angular.forEach($scope.Exams, function (value, key) {
                        if (value.BDate) {
                            value.BDate = new Date(parseInt(value.BDate.substr(6)));
                        }
                        if (value.EDate) {
                            value.EDate = new Date(parseInt(value.EDate.substr(6)));
                        }
                        if (value.SDate) {
                            value.SDate = new Date(parseInt(value.SDate.substr(6)));
                        }
                    });
                });
            }
            else {
                $scope.StudentRazorForm = false;
                $scope.StudentAngularForm = false;
            }

        };

        $scope.PresenceSearchBox = function () {
            if ($scope.PresenceSearchBoxData || $scope.PresenceSearchBoxDate) {
                var ToSenh2ext = { 'SearchBoxData': $scope.PresenceSearchBoxData, 'SearchBoxDate': $scope.PresenceSearchBoxDate };
                $http({ method: 'POST', url: '/Json/SearchPresence', data: ToSenh2ext }).then(function successCallback(response) {
                    $scope.PresenceRazorForm = true;
                    $scope.PresenceAngularForm = true;
                    $scope.Exams = response.data;
                    angular.forEach($scope.Exams, function (value, key) {
                        value.Date = new Date(parseInt(value.Date.substr(6)));
                    });
                });
            }
            else {
                $scope.PresenceRazorForm = false;
                $scope.PresenceAngularForm = false;
            }
        };



        $scope.EmployeeSearchBox = function () {
            if ($scope.EmployeeSearchBoxData || $scope.EmployeeSearchBoxDate) {
                var ToSenh2ext = { 'SearchBoxData': $scope.EmployeeSearchBoxData, 'SearchBoxDate': $scope.EmployeeSearchBoxDate };
                $http({ method: 'POST', url: '/Json/SearchEmployees', data: ToSenh2ext }).then(function successCallback(response) {
                    $scope.EmployeeRazorForm = true;
                    $scope.EmployeeAngularForm = true;
                    $scope.Exams = response.data;
                    angular.forEach($scope.Exams, function (value, key) {
                        if (value.EmployeeBDate) {
                            value.EmployeeBDate = new Date(parseInt(value.EmployeeBDate.substr(6)));
                        }
                        if (value.EmployeeSDate) {
                            value.EmployeeSDate = new Date(parseInt(value.EmployeeSDate.substr(6)));
                        }
                        if (value.EmployeeEDate) {
                            value.EmployeeEDate = new Date(parseInt(value.EmployeeEDate.substr(6)));
                        }

                    });
                });
            }
            else {
                $scope.EmployeeRazorForm = false;
                $scope.EmployeeAngularForm = false;
            }

        };



        



    }]);



})();
