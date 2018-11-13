﻿angular
    .module('homer')
    .controller('BuveraEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval', 'usSpinnerService',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval, usSpinnerService) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        $scope.selectedGrades = [];

        var buveraId = $scope.buveraId;
        var action = $scope.action;
        var storeId = $scope.storeId;
        var issuing = "NO";

        
        $http.get('webapi/GradeApi/GetAllGrades').success(function (data, status) {
            $scope.grades = data;
        });

        $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
            $scope.branches = data;
        });

        $http.get('/webapi/StoreApi/GetAllStores').success(function (data, status) {
            $scope.stores = data;
        });

        if (action == 'create') {
            orderId = 0;
            var promise = $http.get('/webapi/UserApi/GetLoggedInUser', {});
            promise.then(
                function (payload) {
                    var c = payload.data;
                    $scope.user = {
                        UserName: c.UserName,
                        Id: c.Id,
                        FirstName: c.FirstName,
                        LastName: c.LastName,
                        UserRoles: c.UserRoles,
                    };
                }

            );
        }

        if (action == 'edit') {
            var promise = $http.get('/webapi/BuveraApi/GetBuvera?buveraId=' + buveraId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.buvera = {
                        BuveraId: b.BuveraId,
                        TotalCost: b.TotalCost,
                     TotalQuantity : b.TotalQuantity,
                        BranchId: b.BranchId,
                        StoreId: b.StoreId,
                        FromSupplier: b.FromSupplier,
                        ToReceiver: b.ToReceiver,
                        Issuing : b.Issuing,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted,
                        
                        Grades: b.Grades
                    };
                });

        }

        $scope.Save = function (buvera) {
           
            $scope.TotalGradeQuantities = 0;
            $scope.denominationQuantities = 0;
            $scope.showMessageSave = false;
            usSpinnerService.spin('global-spinner');


            angular.forEach($scope.selectedGrades, function (value, key) {
                var denominations = value.Denominations;
                angular.forEach(denominations, function (denominations) {
                    $scope.denominationQuantities = parseFloat(denominations.Quantity) + parseFloat($scope.denominationQuantities);
                });
                $scope.TotalGradeQuantities = $scope.denominationQuantities;
                
            });
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/BuveraApi/Save', {
                    BuveraId: buveraId,
                    //TotalCost: buvera.TotalCost,
                    Issuing : issuing,
                    StoreId: buvera.StoreId,
                    FromSupplier : buvera.FromSupplier,
                    TotalQuantity: $scope.TotalGradeQuantities,
                    BranchId: buvera.BranchId,
                    Grades: buveraId == 0 ? $scope.selectedGrades : buvera.Grades,
                   
                });

                promise.then(
                    function (payload) {

                        buveraId = payload.data;
                        $scope.showMessageSave = true;
                        usSpinnerService.stop('global-spinner');
                        $timeout(function () {
                            $scope.showMessageSave = false;

                            if (action == "create") {
                                $state.go('buvera-edit', { 'action': 'edit', 'storeId': storeId, 'buveraId': buveraId });
                            }

                        }, 1500);

                    });
            }

        }


        $scope.Cancel = function () {
            $state.go('store-buveraStanding', { 'storeId': $scope.storeId });
        };

        $scope.Delete = function (buveraId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/BuveraApi/Delete?buveraId=' + buveraId, {});
            promise.then(
                function (payload) {
                    $scope.showMessageDeleted = true;
                    $timeout(function () {
                        $scope.showMessageDeleted = false;
                        $scope.Cancel();
                    }, 2500);
                    $scope.showMessageDeleteFailed = false;
                },
                function (errorPayload) {
                    $scope.showMessageDeleteFailed = true;
                    $timeout(function () {
                        $scope.showMessageDeleteFailed = false;
                        $scope.Cancel();
                    }, 1500);
                });
        }


    }
    ]);


angular
    .module('homer').controller('BuveraController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/BuveraApi/GetAllBuveras');
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );

            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: false
            };

            $scope.gridData.multiSelect = false;

            $scope.gridData.columnDefs = [

                {
                    name: 'BuveraId', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/buveras/edit/{{row.entity.BuveraId}}">{{row.entity.BuveraId}}</a> </div>'

                },
                {name: 'Total Quantity',field:'TotalQuantity'},
               // { name: 'TotalCost', field: 'TotalCost' },
                { name: 'StoreName', field: 'StoreName' },
                { name: 'Branch Name', field: 'BranchName' },
                { name: 'Buvera Details', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/buvera/detail/{{row.entity.BuveraId}}"> Buvera Detail</a> </div>' },
           
            ];




        }]);


angular
    .module('homer')
    .controller('BuveraDetailController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$state',
    function ($scope, $http, $filter, $location, $log, $timeout, $state) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var buveraId = $scope.buveraId;

        var promise = $http.get('/webapi/BuveraApi/GetBuvera?buveraId=' + buveraId, {});
        promise.then(
            function (payload) {
                var b = payload.data;
                $scope.buvera = {
                    BuveraId: b.BuveraId,
                    BranchId: b.BranchId,
                    StoreId: b.StoreId,
                    TimeStamp: b.TimeStamp,
                    CreatedOn: b.CreatedOn,
                    CreatedBy: b.CreatedBy,
                    UpdatedBy: b.UpdatedBy,
                    Deleted: b.Deleted,
                    Grades: b.Grades,
                    StoreName: b.StoreName,
                    BranchName: b.BranchName,
                    TotalQuantity : b.TotalQuantity,
                    TotalCost: b.TotalCost
                };
            });

    }]);


angular
    .module('homer').controller('StoreBuveraController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var storeId = $scope.storeId;


            var promise = $http.get('/webapi/BuveraApi/GetAllBuverasForAparticularStore?storeId=' + storeId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );

            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: true
            };

            $scope.gridData.multiSelect = true;

            $scope.gridData.columnDefs = [

                {
                    name: 'BuveraId', field: 'BuveraId'

                },
                { name: 'Total Quantity', field: 'TotalQuantity' },
                { name: 'Total Cost', field: 'TotalCost' },
                { name: 'Store', field: 'StoreName' },
                { name: 'Branch Name', field: 'BranchName' },
                { name: 'Date', field: 'TimeStamp' },
               
            
            ];




        }]);


angular
    .module('homer')
    .controller('StoreBuveraStandingController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var storeId = $scope.storeId;

        var promisestore = $http.get('/webapi/StoreApi/GetStore?storeId=' + storeId, {});
        promisestore.then(
            function (payload) {
                var b = payload.data;

                $scope.store = {
                    StoreId: b.StoreId,
                    Name: b.Name,

                };

            });


        var promise = $http.get('/webapi/BuveraApi/GetStoreBuveraStock?storeId=' + storeId, {});
        promise.then(
            function (payload) {
                var b = payload.data;

                $scope.retrievedStoreId = $scope.storeId;

                $scope.storeBuveraGradeSize = {

                    StoreBuveraSizeGrades: b.StoreBuveraSizeGrades,

                };


            });

    }
    ]);

angular
    .module('homer')
    .controller('BuveraIssueController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval', 'usSpinnerService',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval, usSpinnerService) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        $scope.selectedGrades = [];
        var branches = [];
        var selectedBranch;
        var buveraId = $scope.buveraId;
        var action = $scope.action;
        var storeId = $scope.storeId;
        var issuing = "YES"


        $http.get('webapi/GradeApi/GetAllGrades').success(function (data, status) {
            $scope.grades = data;
        });

        $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
            $scope.bdata = {
                branches: data,
                selectedBranch: branches[0]
            }
        });

        $scope.OnBranchChange = function (buvera) {
            var selectedBranchId = buvera.BranchId
            $http.get('/webapi/StoreApi/GetAllBranchStores?branchId=' + selectedBranchId).then(function (responses) {
                $scope.stores = responses.data;

            });
        }
        

        if (action == 'create') {
            buveraId = 0;
            var promise = $http.get('/webapi/UserApi/GetLoggedInUser', {});
            promise.then(
                function (payload) {
                    var c = payload.data;
                    $scope.user = {
                        UserName: c.UserName,
                        Id: c.Id,
                        FirstName: c.FirstName,
                        LastName: c.LastName,
                        UserRoles: c.UserRoles,
                    };
                }

            );
        }

        if (action == 'edit') {
            var promise = $http.get('/webapi/BuveraApi/GetBuvera?buveraId=' + buveraId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.buvera = {
                        BuveraId: b.BuveraId,
                        TotalCost: b.TotalCost,
                        TotalQuantity: b.TotalQuantity,
                        BranchId: b.BranchId,
                        StoreId: b.StoreId,
                        FromSupplier: b.FromSupplier,
                        ToReceiver: b.ToReceiver,
                        Issuing: b.Issuing,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted,

                        Grades: b.Grades
                    };
                });

        }

        $scope.Save = function (buvera) {

            $scope.TotalGradeQuantities = 0;
            $scope.denominationQuantities = 0;
            $scope.showMessageSave = false;
            usSpinnerService.spin('global-spinner');


            angular.forEach($scope.selectedGrades, function (value, key) {
                var denominations = value.Denominations;
                angular.forEach(denominations, function (denominations) {
                    $scope.denominationQuantities = parseFloat(denominations.Quantity) + parseFloat($scope.denominationQuantities);
                });
                $scope.TotalGradeQuantities = $scope.denominationQuantities;

            });
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/BuveraApi/Save', {
                    BuveraId: buveraId,
                    //TotalCost: buvera.TotalCost,
                    Issuing: issuing,
                    StoreId: storeId,
                    ToReceiver : buvera.StoreId,
                    //FromSupplier: storeId,
                    TotalQuantity: $scope.TotalGradeQuantities,
                    BranchId: buvera.BranchId,
                    Grades: buveraId == 0 ? $scope.selectedGrades : buvera.Grades,

                });

                promise.then(
                    function (payload) {

                        buveraId = payload.data;
                        $scope.showMessageSave = true;
                        usSpinnerService.stop('global-spinner');
                        $timeout(function () {
                            $scope.showMessageSave = false;

                            if (action == "create") {
                                $state.go('buvera-edit', { 'action': 'edit', 'storeId': storeId, 'buveraId': buveraId });
                            }

                        }, 1500);

                    });
            }

        }


        $scope.Cancel = function () {
            $state.go('store-buveraStanding', { 'storeId': $scope.storeId });
        };

        $scope.Delete = function (buveraId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/BuveraApi/Delete?buveraId=' + buveraId, {});
            promise.then(
                function (payload) {
                    $scope.showMessageDeleted = true;
                    $timeout(function () {
                        $scope.showMessageDeleted = false;
                        $scope.Cancel();
                    }, 2500);
                    $scope.showMessageDeleteFailed = false;
                },
                function (errorPayload) {
                    $scope.showMessageDeleteFailed = true;
                    $timeout(function () {
                        $scope.showMessageDeleteFailed = false;
                        $scope.Cancel();
                    }, 1500);
                });
        }


    }
    ]);
