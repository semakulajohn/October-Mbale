﻿angular
    .module('homer')
    .controller('CashEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval', 'usSpinnerService',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval, usSpinnerService) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }
        var branchId = $scope.branchId;
        var cashId = $scope.cashId;
        var action = $scope.action;

        $scope.actions = ["+", "-"];

        $http.get('/webapi/TransactionSubTypeApi/GetAllTransactionSubTypes').success(function (data, status) {
            $scope.transactionSubTypes = data;
        });

        $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
            $scope.branches = data;
        });


        $http.get('/webapi/SectorApi/GetAllSectors').success(function (data, status) {
            $scope.sectors = data;
        });

        if (action == 'create') {
            deliveryId = 0;
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
            var promise = $http.get('/webapi/CashApi/GetCash?cashId=' + cashId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.cash = {
                        CashId: b.CashId,
                        Notes: b.Notes,
                        Balance: b.Balance,
                        TransactionSubTypeId: b.TransactionSubTypeId,
                        BranchId: b.BranchId,
                        SectorId: b.SectorId,
                        Amount: b.Amount,
                        StartAmount: b.StartAmount,
                        Action: b.Action,
                        CreatedOn: b.CreatedOn,
                        TimeStamp: b.TimeStamp,
                        CreatedBy: b.CreatedBy,
                        Deleted: b.Deleted,
                        UpdatedBy: b.UpdatedBy,

                    };
                });


        }

        $scope.Save = function (cash) {
            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                usSpinnerService.spin('global-spinner');
                var promise = $http.post('/webapi/CashApi/Save', {

                    CashId: cashId,
                    Amount: cash.Amount,
                    StartAmount: cash.StartAmount,
                    Balance: cash.Balance,
                    BranchId: cash.BranchId,
                    SectorId: cash.SectorId,
                    Notes: cash.Notes,
                    Action: cash.Action,
                    TransactionSubTypeId: cash.TransactionSubTypeId,
                    CreatedOn: cash.CreatedOn,
                    TimeStamp: cash.TimeStamp,
                    CreatedBy: cash.CreatedBy,
                    Deleted: cash.Deleted,

                });

                promise.then(
                    function (payload) {

                        cashId = payload.data;
                        $scope.showMessageSave = true;
                        usSpinnerService.stop('global-spinner');
                        $timeout(function () {
                            $scope.showMessageSave = false;

                            if (action == "create") {
                                $state.go('cash.list');
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {

            $state.go('cash.list');
        };

        $scope.Delete = function (cashId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/CashApi/Delete?cashId=' + cashId, {});
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
    .module('homer').controller('CashController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/CashApi/GetAllCash');
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

                
                { name: 'Branch', field: 'BranchName' },
                { name: 'TransactionSubTypeName', field: 'TransactionSubTypeName' },
                { name: 'Notes', field: 'Notes', field: 'Notes' },
                { name: 'Start Amount', field: 'StartAmount' },
                { name: 'Action', field: 'Action' },

                { name: 'Amount', field: 'Amount' },
                 { name: 'Balance', field: 'Balance' },
                 
                 { name: 'Department', field: 'SectorName' },
                 {
                     name: 'CreatedOn', field: 'CreatedOn',
                     sort: {
                         direction: uiGridConstants.DESC,
                         priority:1
                     }
                 },

            ];




        }]);



angular
    .module('homer').controller('BranchCashController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {

            $scope.accountBalance = 0;
            var branchId = $scope.branchId;
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/CashApi/GetAllCashForAParticularBranch?branchId=' + branchId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                    $scope.Length = payload.data.length;
                    if ($scope.Length > 0) {

                        var lastIndex = $scope.Length - 1;
                        $scope.accountBalance = payload.data[lastIndex].Balance;
                    }
                    else {
                        $scope.accountBalance = 0;
                    }
                }
            );
            $scope.retrievedBranchId = $scope.branchId;
            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: false
            };

            $scope.gridData.multiSelect = false;

            $scope.gridData.columnDefs = [



                { name: 'Branch', field: 'BranchName' },
                { name: 'TransactionSubTypeName', field: 'TransactionSubTypeName' },
                { name: 'Notes', field: 'Notes' },
                { name: 'Start Amount', field: 'StartAmount' },
                { name: 'Action', field: 'Action' },

                { name: 'Amount', field: 'Amount' },
                 { name: 'Balance', field: 'Balance' },
                 {
                     name: 'CreatedOn', field: 'CreatedOn',
                     sort: {
                         direction: uiGridConstants.DESC,
                         priority: 1
                     }
                 },


            ];




        }]);

