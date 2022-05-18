angular.module('myFormApp', [])
 .controller('CategoryController', function ($scope, $http, $location, $window) {
        $scope.custModel = {};
        $scope.message = '';
        $scope.result = "color-default";
        $scope.isViewLoading = false;
        $scope.ListCategory = null;
        getallData();

        //******=========Get All Customer=========******
     function getallData() {
         
            //debugger;
         $http.get('/Category/GetAllData/')
                .success(function (data, status, headers, config) {
                    $scope.ListCategory = data;
                })
                .error(function (data, status, headers, config) {
                    $scope.message = 'Unexpected Error while loading data!!' + status;
                    $scope.result = "color-red";
                    console.log($scope.message);
                });
        };

        //******=========Get Single Customer=========******
     $scope.getCategory = function (custModel) {
                    //debugger;
         $http.get('/Category/GetbyID/' + custModel.Id)
                .success(function (data, status, headers, config) {
         
                    $scope.custModel = data;
                    getallData();
                    console.log(data);
                })
                .error(function (data, status, headers, config) {
                    $scope.message = 'Unexpected Error while loading data!!';
                    $scope.result = "color-red";
                    console.log($scope.message);
                });
        };

        //******=========Save Customer=========******
        $scope.saveCategory = function () {
            $scope.isViewLoading = true;
            
            //debugger;
            $http({
                method: 'POST',
                url: '/Category/Insert',
                data: $scope.custModel
            }).success(function (data, status, headers, config) {
                if (data.success === true) {
                    $scope.message = 'Form data Saved!';
                    $scope.result = "color-green";
                    getallData();
                    $scope.custModel = {};
                    console.log(data);
                }
                else {
                    $scope.message = 'Form data not Saved!';
                    $scope.result = "color-red";
                }
            }).error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error while saving data!!' + data.errors;
                $scope.result = "color-red";
                console.log($scope.message);
            });
            getallData();
            $scope.isViewLoading = false;
        };

        //******=========Edit Customer=========******
        $scope.updateCategory = function () {
            //debugger;
            $scope.isViewLoading = true;          
            $http({
                method: 'POST',
                url: '/Category/Update',
                data: $scope.custModel
            }).success(function (data, status, headers, config) {
                if (data.success === true) {
                    $scope.custModel = null;
                    $scope.message = 'Form data Updated!';
                    $scope.result = "color-green";
                    getallData();
                    console.log(data);
                }
                else {
                    $scope.message = 'Form data not Updated!';
                    $scope.result = "color-red";
                }
            }).error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error while updating data!!' + data.errors;
                $scope.result = "color-red";
                console.log($scope.message);
            });
            $scope.isViewLoading = false;
        };

        //******=========Delete Customer=========******
        $scope.deleteCategory = function (custModel) {
            //debugger;
            var IsConf = confirm('You are about to delete ' + custModel.DesCategory + '. Are you sure?');
            if (IsConf) {              
                $http.delete('/Category/Delete/' + custModel.Id)
                    .success(function (data, status, headers, config) {
                        if (data.success === true) {
                            $scope.message = custModel.DesCategory + ' deleted from record!!';
                            $scope.result = "color-green";
                            getallData();
                            console.log(data);
                        }
                        else {
                            $scope.message = 'Error on deleting Record!';
                            $scope.result = "color-red";
                        }
                    })
                    .error(function (data, status, headers, config) {
                        $scope.message = 'Unexpected Error while deleting data!!';
                        $scope.result = "color-red";
                        console.log($scope.message);
                    });
            }
        };
 })
.config(function ($locationProvider) {
        $locationProvider.html5Mode(true);
});