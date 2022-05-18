angular.module('myFormApp', [])
    .controller('ProductController', function ($scope, $http, $location, $window) {
        $scope.custModel = {};
        $scope.message = '';
        $scope.result = "color-default";
        $scope.isViewLoading = false;
        $scope.ListCategory = null;
        $scope.ListProductCategory = null;

        getallData();

        //******=========Get All Customer=========******
        function getallData() {
          
            //debugger;
            $http.get('/Category/GetAllData/')
                .success(function (data, status, headers, config) {
                    $scope.ListCategory = data;
                    $scope.sCategory = $scope.ListCategory[0].Id;
                })
                .error(function (data, status, headers, config) {
                    $scope.message = 'Unexpected Error while loading data!!';
                    $scope.result = "color-red";
                    console.log($scope.message);
                });
            $http.get('/Product/GetAllData')
                .success(function (data, status, headers, config) {
                    $scope.ListProductCategory = data;
                })
                .error(function (data, status, headers, config) {
                    $scope.message = 'Unexpected Error while loading data!!';
                    $scope.result = "color-red";
                    console.log($scope.message);
                });
        };

        //******=========Get Single Customer=========******
        $scope.getProduct = function (custModel) {
            $http.get('/Product/GetbyID/' + custModel.product.Id)
                .success(function (data, status, headers, config) {
                    //debugger;
                    $scope.custModel = data;
                    //getallData();
                    //console.log("Id Category:" + custModel.product.IdCategory);
                    $('#sCategory').val(custModel.product.IdCategory);
                    //$('#sCategory').removeAttr('required');
                    //$("#sCategory").prop('required', false);
                  /*  $('#sCategory').click();*/
                    console.log(data);
                })
                .error(function (data, status, headers, config) {
                    $scope.message = 'Unexpected Error while loading data!!';
                    $scope.result = "color-red";
                    console.log($scope.message);
                });
        };

        //******=========Save Customer=========******
        $scope.saveProduct = function () {
            $scope.isViewLoading = true;
            $scope.custModel.product.IdCategory = $('#sCategory').val();
            $http({
                method: 'POST',
                url: '/Product/Insert',
                data: $scope.custModel.product
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
        $scope.updateProduct = function () {
            //debugger;
            $scope.isViewLoading = true;
            $scope.custModel.product.IdCategory = $('#sCategory').val();
            $http({
                method: 'POST',
                url: '/Product/Update',
                data: $scope.custModel.product
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
        $scope.deleteProduct = function (custModel) {
            //debugger;
            var IsConf = confirm('You are about to delete ' + custModel.product.DesProduct + '. Are you sure?');
            if (IsConf) {
                $http.delete('/Product/Delete/' + custModel.product.Id)
                    .success(function (data, status, headers, config) {
                        if (data.success === true) {
                            $scope.message = custModel.DesProduct  + ' deleted from record!!';
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
        $scope.GetSelectedValue = function () {
            console.log('Id = ' + $('#sCategory').val());
        };

    })
    .config(function ($locationProvider) {
        $locationProvider.html5Mode(true);
    });