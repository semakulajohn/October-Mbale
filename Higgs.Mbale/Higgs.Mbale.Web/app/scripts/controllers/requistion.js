angular
    .module('homer')
    .controller('RequistionEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval','usSpinnerService',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval, usSpinnerService) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var requistionId = $scope.requistionId;
        var action = $scope.action;
        var statusId = "10002";
        var approvedStatusId = "2";

        $http.get('/webapi/StatusApi/GetAllStatuses').success(function (data, status) {
            $scope.statuses = data;
        });

        $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
            $scope.branches = data;
        });
        if (action == 'create') {
            transactionSubTypeId = 0;

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



            var promise = $http.get('/webapi/RequistionApi/GetRequistion?requistionId=' + requistionId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;

                    $scope.requistion = {
                        RequistionId: b.RequistionId,
                        Response: b.Response,
                        StatusId: b.StatusId,
                        Amount : b.Amount,
                        BranchId : b.BranchId,
                        Description: b.Description,
                        ApprovedById: b.ApprovedById,
                        RequistionNumber : b.RequistionNumber,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted,


                    };

                });


        }

        $scope.Save = function (requistion) {

            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                usSpinnerService.spin('global-spinner');
                var promise = $http.post('/webapi/RequistionApi/Save', {
                    RequistionId: requistionId,
                    BranchId: requistion.BranchId,
                    Description : requistion.Description,
                    Response: requistion.Response,
                    Amount : requistion.Amount,
                    StatusId : statusId,
                    ApprovedById: requistion.ApprovedById,
                    RequistionNumber : requistion.RequistionNumber,
                    CreatedBy: requistion.CreatedBy,
                    CreatedOn: requistion.CreatedOn,
                    Deleted: requistion.Deleted,

                });

                promise.then(
                    function (payload) {

                        requistionId = payload.data;

                        $scope.showMessageSave = true;
                        usSpinnerService.stop('global-spinner');
                        $timeout(function () {
                            $scope.showMessageSave = false;


                            if (action == "create") {
                                $state.go('requistion-edit', { 'action': 'edit', 'requistionId': requistionId });
                            }

                        }, 1500);


                    });
            }

        }

         $scope.Approve = function (requistion) {

            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                usSpinnerService.spin('global-spinner');
                var promise = $http.post('/webapi/RequistionApi/Save', {
                    RequistionId: requistionId,
                    BranchId: requistion.BranchId,
                    Description : requistion.Description,
                    Response: requistion.Response,
                    Amount : requistion.Amount,
                    StatusId: approvedStatusId,
                    ApprovedById: $scope.user.Id,
                    RequistionNumber : requistion.RequistionNumber,
                    CreatedBy: requistion.CreatedBy,
                    CreatedOn: requistion.CreatedOn,
                    Deleted: requistion.Deleted,

                });

                promise.then(
                    function (payload) {

                        requistionId = payload.data;

                        $scope.showMessageSave = true;
                        usSpinnerService.stop('global-spinner');
                        $timeout(function () {
                            $scope.showMessageSave = false;


                            if (action == "create") {
                                $state.go('requistion-edit', { 'action': 'edit', 'requistionId': requistionId });
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('requistions.list');

        };

        $scope.Delete = function (requistionId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/RequistionApi/Delete?requistionId=' + requistionId, {});
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
    .module('homer').controller('RequistionController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/RequistionApi/GetAllRequistions');
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
                    name: 'Requistion Number', field:'RequistionNumber',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Description', field: 'Description' },
                { name: 'Response', field: 'Response' },
                { name: 'Status', field: 'StatusName' },
                { name: 'ApprovedBy', field: 'ApprovedByName' },
                 {
                     name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/requistions/edit/{{row.entity.RequistionId}}">Edit</a> </div>',
                    
                 },

            ];




        }]);

angular
    .module('homer').controller('StatusRequistionController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var statusId = $scope.statusId;
            var promise = $http.get('/webapi/RequistionApi/GetAllRequistionsForAParticularStatus?statusId=' + statusId, {});
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
                    name: 'Requistion Number', field:'RequistionNumber',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Description', field: 'Description' },
                { name: 'Response', field: 'Response' },
                { name: 'Status', field: 'StatusName' },
                { name: 'ApprovedBy', field: 'ApprovedByName' },
                  {
                      name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/requistions/edit/{{row.entity.RequistionId}}">Edit</a> </div>',

                  },

            ];




        }]);



angular
    .module('homer').controller('BranchRequistionController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;

            var branchId = $scope.branchId;
            var promise = $http.get('/webapi/RequistionApi/GetAllRequistionsForAParticularBranch?branchId=' + branchId, {});
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
                    name: 'Requistion Number',field:'RequistionNumber',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Description', field: 'Description' },
                { name: 'Response', field: 'Response' },
                { name: 'Status', field: 'StatusName' },
                { name: 'ApprovedBy', field: 'ApprovedByName' },
                  {
                      name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/requistions/edit/{{row.entity.RequistionId}}">Edit</a> </div>',

                  },

            ];




        }]);
