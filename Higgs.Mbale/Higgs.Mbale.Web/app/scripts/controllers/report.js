
angular
    .module('homer').controller('ReportTransactionController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants', '$window',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants, $window) {
            $scope.loadingSpinner = true;

            $scope.reportType = 0;
            $scope.showDownloadLink = false;


            $scope.TransactionsForThisMonth = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateTransactionCurrentMonthReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 2;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { TransactionId: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }

            $scope.TodaysTransactions = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateTransactionTodaysReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 1;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { TransactionId: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }

            $scope.WeeksTransactions = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateTransactionCurrentWeekReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 3;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { TransactionId: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }
          


            $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
                $scope.branches = data;
            });

            $http.get('/webapi/SupplierApi/GetAllSuppliers').success(function (data, status) {
                $scope.suppliers = data;
            });

            $scope.SearchTransactions = function (accountTransaction) {
                $scope.data = [];
                var promise = $http.post('/webapi/ReportApi/GetAllTransactionsBetweenTheSpecifiedDates',
                        {
                            FromDate: accountTransaction.FromDate,
                            ToDate: accountTransaction.ToDate,
                            SupplierId: accountTransaction.Id,
                            BranchId: accountTransaction.BranchId,

                        });
                promise.then(
                 function (payload) {

                     $scope.data = payload.data;
                     $scope.reportType = 4;

                     $scope.tableParams = new ngTableParams({
                         page: 1,
                         count: 10,
                         sorting: { SupplyDate: 'desc' }
                     }, {
                         getData: function ($defer, params) {
                             var filteredData = $filter('filter')($scope.data, $scope.filter);
                             var orderedData = params.sorting() ?
                                                 $filter('orderBy')(filteredData, params.orderBy()) :
                                                 filteredData;

                             params.total(orderedData.length);
                             $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         },
                         $scope: $scope

                     });
                 });
            }


            $scope.DownloadExcelFile = function () {
                $window.open("/Excel/Index/" + $scope.reportType);
            };

            }]);
        


angular
    .module('homer').controller('ReportAccountTransactionController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants', '$window',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants, $window) {
            $scope.loadingSpinner = true;

            $scope.reportType = 0;
            $scope.showDownloadLink = false;


            $scope.AccountTransactionsForThisMonth = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateAccountTransactionCurrentMonthReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 2;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { TransactionId: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }

            $scope.AccountTodaysTransactions = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateAccountTransactionTodaysReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 1;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { TransactionId: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }

            $scope.AccountWeeksTransactions = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateAccountTransactionCurrentWeekReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 3;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { TransactionId: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }



            $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
                $scope.branches = data;
            });

            $http.get('/webapi/SupplierApi/GetAllSuppliers').success(function (data, status) {
                $scope.suppliers = data;
            });

            $scope.SearchAccountTransactions = function (accountTransaction) {
                $scope.data = [];
                var promise = $http.post('/webapi/ReportApi/GenerateAccountTransactionsBetweenTheSpecifiedDates',
                        {
                            FromDate: accountTransaction.FromDate,
                            ToDate: accountTransaction.ToDate,
                            SupplierId: accountTransaction.Id,
                            BranchId: accountTransaction.BranchId,

                        });
                promise.then(
                 function (payload) {

                     $scope.data = payload.data;
                     $scope.reportType = 4;

                     $scope.tableParams = new ngTableParams({
                         page: 1,
                         count: 10,
                         
                     }, {
                         getData: function ($defer, params) {
                             var filteredData = $filter('filter')($scope.data, $scope.filter);
                             var orderedData = params.sorting() ?
                                                 $filter('orderBy')(filteredData, params.orderBy()) :
                                                 filteredData;

                             params.total(orderedData.length);
                             $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         },
                         $scope: $scope

                     });
                 });
            }


            $scope.DownloadExcelFile = function () {
                $window.open("/Excel/Index/" + $scope.reportType);
            };

        }]);



angular
    .module('homer').controller('ReportSupplyController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants', '$window',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants, $window) {
            $scope.loadingSpinner = true;

            $scope.reportType = 0;
            $scope.showDownloadLink = false;


            $scope.SuppliesForThisMonth = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateSupplyCurrentMonthReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 2;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { SupplyNumber: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }

            $scope.TodaysSupplies = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateSupplyTodaysReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 1;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { SupplyNumber: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }

            $scope.WeeksSupplies = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateSupplyCurrentWeekReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 3;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { SupplyNumber: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }


            $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
                $scope.branches = data;
            });

            $http.get('/webapi/SupplierApi/GetAllSuppliers').success(function (data, status) {
                $scope.suppliers = data;
            });

            $scope.SearchSupplies = function (supply) {
                $scope.data = [];
                var promise = $http.post('/webapi/ReportApi/GetAllSuppliesBetweenTheSpecifiedDates',
                        {
                            FromDate: supply.FromDate,
                            ToDate: supply.ToDate,
                            SupplierId: supply.Id,
                            BranchId : supply.BranchId,
                            
                        });
                promise.then(
                 function (payload) {

                     $scope.data = payload.data;
                     $scope.reportType = 4;
                     
                     $scope.tableParams = new ngTableParams({
                         page: 1,
                         count: 10,
                         sorting: { SupplyDate: 'desc' }
                     }, {
                         getData: function ($defer, params) {
                             var filteredData = $filter('filter')($scope.data, $scope.filter);
                             var orderedData = params.sorting() ?
                                                 $filter('orderBy')(filteredData, params.orderBy()) :
                                                 filteredData;

                             params.total(orderedData.length);
                             $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         },
                         $scope: $scope

                     });
                 });
            }


            $scope.DownloadExcelFile = function () {
                $window.open("/Excel/Supply/" + $scope.reportType);
            };

        }]);


angular
    .module('homer').controller('ReportDashBoardSupplyController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants', '$window',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants, $window) {
            $scope.loadingSpinner = true;

            $scope.reportType = 0;
            $scope.showDownloadLink = false;
            var supplierId = $scope.supplierId;

            $scope.SuppliesForThisMonth = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateSupplyCurrentMonthReportForAParticularSupplier?supplierId=' + supplierId, {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 2;
                     $scope.supplierReport = {
                         ReportTypeId: $scope.reportType,
                         SupplierId: supplierId,

                     };
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { SupplyNumber: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }

            $scope.TodaysSupplies = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateSupplyTodaysReportForAParticularSupplier?supplierId='+supplierId, {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 1;
                     $scope.supplierReport = {
                         ReportTypeId: $scope.reportType,
                         SupplierId: supplierId,

                     };
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { SupplyNumber: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }

            $scope.WeeksSupplies = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateSupplyCurrentWeekReportForAParticularSupplier?supplierId='+supplierId, {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 3;
                     $scope.supplierReport = {
                         ReportTypeId: $scope.reportType,
                         SupplierId: supplierId,

                     };
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { SupplyNumber: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }

            $scope.SearchSupplies = function (supply) {
                $scope.data = [];
                var promise = $http.post('/webapi/ReportApi/GetAllSuppliesBetweenTheSpecifiedDatesForAParticularSupplier?supplierId='+supplierId,
                        {
                            FromDate: supply.FromDate,
                            ToDate: supply.ToDate,

                        });
                promise.then(
                 function (payload) {

                     $scope.data = payload.data;
                     $scope.reportType = 4;
                     $scope.supplierReport = {
                         ReportTypeId: $scope.reportType,
                         SupplierId : supplierId,

                     };

                     $scope.tableParams = new ngTableParams({
                         page: 1,
                         count: 10,
                         sorting: { SupplyDate: 'desc' }
                     }, {
                         getData: function ($defer, params) {
                             var filteredData = $filter('filter')($scope.data, $scope.filter);
                             var orderedData = params.sorting() ?
                                                 $filter('orderBy')(filteredData, params.orderBy()) :
                                                 filteredData;

                             params.total(orderedData.length);
                             $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         },
                         $scope: $scope

                     });
                 });
            }


            $scope.DownloadExcelFile = function () {
                $window.open("/Excel/SupplierSupply2/" + $scope.reportType + "/"+ supplierId);
              
            };

        }]);


angular
    .module('homer').controller('ReportBatchController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants', '$window',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants, $window) {
            $scope.loadingSpinner = true;

            $scope.reportType = 0;
            $scope.showDownloadLink = false;


            $scope.BatchesForThisMonth = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateBatchCurrentMonthReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 2;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { CreatedOn: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }

            $scope.TodaysBatches = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateBatchTodaysReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 1;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { CreatedOn: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }

            $scope.WeeksBatches = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateBatchCurrentWeekReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 3;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { CreatedOn: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }


            $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
                $scope.branches = data;
            });

          

            $scope.SearchBatches = function (batch) {
                $scope.data = [];
                var promise = $http.post('/webapi/ReportApi/GetAllBatchesBetweenTheSpecifiedDates',
                        {
                            FromDate: batch.FromDate,
                            ToDate: batch.ToDate,
                          
                            BranchId: batch.BranchId,

                        });
                promise.then(
                 function (payload) {

                     $scope.data = payload.data;
                     $scope.reportType = 4;

                     $scope.tableParams = new ngTableParams({
                         page: 1,
                         count: 10,
                         sorting: { CreatedOn: 'desc' }
                     }, {
                         getData: function ($defer, params) {
                             var filteredData = $filter('filter')($scope.data, $scope.filter);
                             var orderedData = params.sorting() ?
                                                 $filter('orderBy')(filteredData, params.orderBy()) :
                                                 filteredData;

                             params.total(orderedData.length);
                             $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         },
                         $scope: $scope

                     });
                 });
            }


            $scope.DownloadExcelFile = function () {
                $window.open("/Excel/Batch/" + $scope.reportType);
            };

        }]);



angular
    .module('homer').controller('ReportDeliveryController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants', '$window',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants, $window) {
            $scope.loadingSpinner = true;

            $scope.reportType = 0;
            $scope.showDownloadLink = false;


            $scope.DeliveriesForThisMonth = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateDeliveryCurrentMonthReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 2;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { CreatedOn: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }

            $scope.TodaysDeliveries = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateDeliveryTodaysReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 1;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { CreatedOn: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }

            $scope.WeeksDeliveries = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateDeliveryCurrentWeekReport', {});
                $scope.showDownloadLink = false;
                promise.then(
                 function (payload) {
                     $scope.data = payload.data;
                     $scope.reportType = 3;
                     if ($scope.data.length > 0) {
                         $scope.showDownloadLink = true;
                     }
                     $scope.tableParams = new ngTableParams({ page: 1, count: 20, sorting: { CreatedOn: 'desc' } }, {
                         total: $scope.data.length, getData: function ($defer, params) {
                             var orderData = params.sorting() ?
                                                 $filter('orderBy')($scope.data, params.orderBy()) :
                                                 $scope.data;
                             $defer.resolve(orderData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         }
                     });
                 });
            }


            $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
                $scope.branches = data;
            });

            $http.get('/webapi/CustomerApi/GetAllCustomers').success(function (data, status) {
                $scope.customers = data;
            });

            $scope.SearchDeliveries = function (delivery) {
                $scope.data = [];
                var promise = $http.post('/webapi/ReportApi/GetAllDeliveriesBetweenTheSpecifiedDates',
                        {
                            FromDate: delivery.FromDate,
                            ToDate: delivery.ToDate,
                            CustomerId: delivery.CustomerId,
                            BranchId: delivery.BranchId,

                        });
                promise.then(
                 function (payload) {

                     $scope.data = payload.data;
                     $scope.reportType = 4;

                     $scope.tableParams = new ngTableParams({
                         page: 1,
                         count: 10,
                         sorting: { CreatedOn: 'desc' }
                     }, {
                         getData: function ($defer, params) {
                             var filteredData = $filter('filter')($scope.data, $scope.filter);
                             var orderedData = params.sorting() ?
                                                 $filter('orderBy')(filteredData, params.orderBy()) :
                                                 filteredData;

                             params.total(orderedData.length);
                             $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                         },
                         $scope: $scope

                     });
                 });
            }


            $scope.DownloadExcelFile = function () {
                $window.open("/Excel/Batch/" + $scope.reportType);
            };

        }]);

