
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
                $window.open("/Excel/Delivery/" + $scope.reportType);
            };

        }]);


angular
    .module('homer').controller('ReportCashController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants', '$window',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants, $window) {
            $scope.loadingSpinner = true;

            $scope.reportType = 0;
            $scope.showDownloadLink = false;


            $scope.CashForThisMonth = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateCashCurrentMonthReport', {});
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

            $scope.TodaysCash = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateCashTodaysReport', {});
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

            $scope.WeeksCash = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateCashCurrentWeekReport', {});
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



            $scope.SearchCash = function (cash) {
                $scope.data = [];
                var promise = $http.post('/webapi/ReportApi/GetAllCashBetweenTheSpecifiedDates',
                        {
                            FromDate: cash.FromDate,
                            ToDate: cash.ToDate,

                            BranchId: cash.BranchId,

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
                $window.open("/Excel/Cash/" + $scope.reportType);
            };

        }]);



angular
    .module('homer').controller('ReportOrderController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants', '$window',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants, $window) {
            $scope.loadingSpinner = true;

            $scope.reportType = 0;
            $scope.showDownloadLink = false;


            $scope.OrdersForThisMonth = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateOrderCurrentMonthReport', {});
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

            $scope.TodaysOrders = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateOrderTodaysReport', {});
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

            $scope.WeeksOrders = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateOrderCurrentWeekReport', {});
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

            $scope.SearchOrders = function (order) {
                $scope.data = [];
                var promise = $http.post('/webapi/ReportApi/GetAllOrdersBetweenTheSpecifiedDates',
                        {
                            FromDate: order.FromDate,
                            ToDate: order.ToDate,
                            CustomerId: order.CustomerId,
                            BranchId: order.BranchId,

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
                $window.open("/Excel/Order/" + $scope.reportType);
            };

        }]);


angular
    .module('homer').controller('ReportFactoryExpenseController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants', '$window',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants, $window) {
            $scope.loadingSpinner = true;

            $scope.reportType = 0;
            $scope.showDownloadLink = false;


            $scope.FactoryExpenseForThisMonth = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateFactoryExpenseCurrentMonthReport', {});
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

            $scope.TodaysFactoryExpense = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateFactoryExpenseTodaysReport', {});
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

            $scope.WeeksFactoryExpense = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateFactoryExpenseCurrentWeekReport', {});
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



            $scope.SearchFactoryExpense = function (factoryExpense) {
                $scope.data = [];
                var promise = $http.post('/webapi/ReportApi/GetAllFactoryExpensesBetweenTheSpecifiedDates',
                        {
                            FromDate: factoryExpense.FromDate,
                            ToDate: factoryExpense.ToDate,

                            BranchId: factoryExpense.BranchId,

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
                $window.open("/Excel/FactoryExpense/" + $scope.reportType);
            };

        }]);


angular
    .module('homer').controller('ReportOtherExpenseController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants', '$window',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants, $window) {
            $scope.loadingSpinner = true;

            $scope.reportType = 0;
            $scope.showDownloadLink = false;


            $scope.OtherExpenseForThisMonth = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateOtherExpenseCurrentMonthReport', {});
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

            $scope.TodaysOtherExpense = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateOtherExpenseTodaysReport', {});
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

            $scope.WeeksOtherExpense = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateOtherExpenseCurrentWeekReport', {});
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



            $scope.SearchOtherExpense = function (otherExpense) {
                $scope.data = [];
                var promise = $http.post('/webapi/ReportApi/GetAllOtherExpensesBetweenTheSpecifiedDates',
                        {
                            FromDate: otherExpense.FromDate,
                            ToDate: otherExpense.ToDate,

                            BranchId: otherExpense.BranchId,

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
                $window.open("/Excel/OtherExpense/" + $scope.reportType);
            };

        }]);


angular
    .module('homer').controller('ReportFlourTransferController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants', '$window',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants, $window) {
            $scope.loadingSpinner = true;

            $scope.reportType = 0;
            $scope.showDownloadLink = false;


            $scope.FlourTransferForThisMonth = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateFlourTransferCurrentMonthReport', {});
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

            $scope.TodaysFlourTransfer = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateFlourTransferTodaysReport', {});
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

            $scope.WeeksFlourTransfer = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateFlourTransferCurrentWeekReport', {});
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



            $scope.SearchFlourTransfer = function (flourTransfer) {
                $scope.data = [];
                var promise = $http.post('/webapi/ReportApi/GetAllFlourTransfersBetweenTheSpecifiedDates',
                        {
                            FromDate: flourTransfer.FromDate,
                            ToDate: flourTransfer.ToDate,

                            BranchId: flourTransfer.BranchId,

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
                $window.open("/Excel/FlourTransfer/" + $scope.reportType);
            };

        }]);


angular
    .module('homer').controller('ReportUtilityController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants', '$window',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants, $window) {
            $scope.loadingSpinner = true;

            $scope.reportType = 0;
            $scope.showDownloadLink = false;


            $scope.UtilityForThisMonth = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateUtilityCurrentMonthReport', {});
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

            $scope.TodaysUtility = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateUtilityTodaysReport', {});
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

            $scope.WeeksUtility = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateUtilityCurrentWeekReport', {});
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



            $scope.SearchUtility = function (utility) {
                $scope.data = [];
                var promise = $http.post('/webapi/ReportApi/GetAllUtilitiesBetweenTheSpecifiedDates',
                        {
                            FromDate: utility.FromDate,
                            ToDate: utility.ToDate,

                            BranchId: utility.BranchId,

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
                $window.open("/Excel/Utility/" + $scope.reportType);
            };

        }]);


angular
    .module('homer').controller('ReportMachineRepairController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants', '$window',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants, $window) {
            $scope.loadingSpinner = true;

            $scope.reportType = 0;
            $scope.showDownloadLink = false;


            $scope.MachineRepairForThisMonth = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateMachineRepairCurrentMonthReport', {});
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

            $scope.TodaysMachineRepair = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateMachineRepairTodaysReport', {});
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

            $scope.WeeksMachineRepair = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateMachineRepairCurrentWeekReport', {});
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



            $scope.SearchMachineRepair = function (machineRepair) {
                $scope.data = [];
                var promise = $http.post('/webapi/ReportApi/GetAllMachineRepairsBetweenTheSpecifiedDates',
                        {
                            FromDate: machineRepair.FromDate,
                            ToDate: machineRepair.ToDate,

                            BranchId: machineRepair.BranchId,

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
                $window.open("/Excel/MachineRepair/" + $scope.reportType);
            };

        }]);


angular
    .module('homer').controller('ReportBatchOutPutController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants', '$window',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants, $window) {
            $scope.loadingSpinner = true;

            $scope.reportType = 0;
            $scope.showDownloadLink = false;


            $scope.BatchOutPutForThisMonth = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateBatchOutPutCurrentMonthReport', {});
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

            $scope.TodaysBatchOutPut = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateBatchOutPutTodaysReport', {});
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

            $scope.WeeksBatchOutPut = function () {
                $scope.data = [];
                var promise = $http.get('/webapi/ReportApi/GenerateBatchOutPutCurrentWeekReport', {});
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



            $scope.SearchBatchOutPut = function (batchOutPut) {
                $scope.data = [];
                var promise = $http.post('/webapi/ReportApi/GetAllBatchOutPutsBetweenTheSpecifiedDates',
                        {
                            FromDate: batchOutPut.FromDate,
                            ToDate: batchOutPut.ToDate,

                            BranchId: batchOutPut.BranchId,

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
                $window.open("/Excel/BatchOutPut/" + $scope.reportType);
            };

        }]);

