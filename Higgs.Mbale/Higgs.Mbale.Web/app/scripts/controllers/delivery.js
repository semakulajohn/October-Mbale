angular
    .module('homer')
    .controller('DeliveryEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval', 'usSpinnerService',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval,usSpinnerService) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }
       
        var branches = [];
        var selectedBranch;
        $scope.selectedGrades = [];
        var transactionSubTypeId = 2;
        var deliveryId = $scope.deliveryId;
        var orderId = $scope.orderId;
        var customerId = $scope.customerId;
        var action = $scope.action;
        var productId = 0;
               
        $http.get('webapi/ProductApi/GetAllproducts').success(function (data, status) {
            $scope.products = data;
        });

        $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
            $scope.xdata = {
                branches: data,
                selectedBranch: branches[0]
            }
        });

       
        $scope.OnBranchChange = function (delivery) {
            var selectedBranchId = delivery.BranchId
           
            $http.get('/webapi/StoreApi/GetAllBranchStores?branchId=' + selectedBranchId).then(function (responses) {
                $scope.stores = responses.data;

            });
            $http.get('/webapi/BatchApi/GetAllBatchesForAParticularBranchToTransfer?branchId=' + selectedBranchId + '&productId=' + $scope.order.ProductId
                     ).then(function (responses) {
                $scope.batches = responses.data;

            });
        }

        $http.get('/webapi/SectorApi/GetAllSectors').success(function (data, status) {
            $scope.sectors = data;
        });
       
        $http.get('/webapi/AccountTransactionActivityApi/GetAllPaymentModes').success(function (data, status) {
            $scope.paymentModes = data;
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
            var promise = $http.get('/webapi/DeliveryApi/GetDelivery?deliveryId=' + deliveryId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.delivery = {
                        DeliveryId: b.DeliveryId,
                        CustomerName: b.CustomerName,
                        DeliveryCost: b.DeliveryCost,
                        OrderId: b.OrderId,
                        VehicleNumber: b.VehicleNumber,
                        BranchId: b.BranchId,
                        Price: b.Price,
                       
                        Amount : b.Amount,
                        Location: b.Location,
                        StoreId : b.StoreId,
                        TransactionSubTypeId : b.TransactionSubTypeId,
                        SectorId: b.SectorId,
                        DriverNIN: b.DriverNIN,
                        PaymentModeId : b.PaymentModeId,
                        DriverName: b.DriverName,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted,
                        Batches: b.Batches,
                        DeliveryBatches: b.DeliveryBatches,
                    };
                });

            var promise = $http.get('/webapi/OrderApi/GetOrder?orderId=' + orderId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.order = {
                        OrderId: b.OrderId,
                        CustomerName: b.CustomerName,
                        Amount: b.Amount,
                        ProductId: b.ProductId,
                        BranchId: b.BranchId,
                        StatusId: b.StatusId,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted,
                        CustomerId: b.CustomerId,
                        Grades: b.Grades,
                       
                    };
                });

          // $scope.productId=  $scope.order.ProductId;

        }

        $scope.Save = function (delivery,productId) {
            $scope.showMessageSave = false;

            $scope.TotalAmount = 0;
            $scope.TotalQuantity = 0;
            $scope.DenominationQuantity = 0;
            $scope.DenominationAmount = 0;
            $scope.showMessageSave = false;
            if (delivery.Grades !=null) {
                angular.forEach(delivery.Grades, function (value, key) {
                    var denominations = value.Denominations;
                    angular.forEach(denominations, function (denominations) {
                        $scope.DenominationAmount = (denominations.Price * denominations.Quantity) + $scope.DenominationAmount;
                        $scope.DenominationQuantity = (denominations.Quantity * denominations.Value) + $scope.DenominationQuantity;
                    });
                    $scope.TotalAmount = $scope.DenominationAmount;
                    $scope.TotalQuantity = $scope.DenominationQuantity;
                    
                });
            }
            else {
                $scope.TotalAmount = delivery.Price * delivery.Quantity;
                $scope.TotalQuantity = delivery.Quantity;
            }

            usSpinnerService.spin('global-spinner');
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/DeliveryApi/Save', {
                    DeliveryId: deliveryId,
                    CustomerId :customerId,
                    DeliveryCost: delivery.DeliveryCost,
                    OrderId: orderId,
                    Amount : $scope.TotalAmount,
                    Price: delivery.Price,
                    Quantity: $scope.TotalQuantity,
                    VehicleNumber :delivery.VehicleNumber,
                    BranchId: delivery.BranchId,
                    PaymentModeId: delivery.PaymentModeId,
                    ProductId : productId,
                    Location : delivery.Location,
                    SectorId: delivery.SectorId,
                    StoreId: delivery.StoreId,
                    
                    TransactionSubTypeId : transactionSubTypeId,
                    DriverName: delivery.DriverName,
                    DriverNIN: delivery.DriverNIN,
                    CreatedBy: delivery.CreatedBy,
                    CreatedOn: delivery.CreatedOn,
                    Deleted: delivery.Deleted,
                    Grades: delivery.Grades,
                    Batches: delivery.Batches,
                    DeliveryBatches: delivery.DeliveryBatches,
                });

                promise.then(
                    function (payload) {

                        deliveryId = payload.data;
                        if (deliveryId == -1) {
                            usSpinnerService.stop('global-spinner');
                            $scope.showMessageNoBatchSelected = true;

                            $timeout(function () {
                                $scope.showMessageNoBatchSelected = false;

                            }, 3000);
                        }
                        else {
                            $scope.showMessageSave = true;
                            usSpinnerService.stop('global-spinner');
                            $timeout(function () {
                                $scope.showMessageSave = false;

                                if (action == "create") {
                                    $state.go('delivery-branch-list', { 'branchId': delivery.BranchId });

                                }

                            }, 2000);
                        }
                       


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('delivery-order-list', { 'orderId': orderId });
        };

        $scope.Delete = function (deliveryId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/DeliveryApi/Delete?deliveryId=' + deliveryId, {});
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
    .module('homer').controller('DeliveryController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/DeliveryApi/GetAllDeliveries');
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
                    name: 'Destination', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/deliveries/edit/{{row.entity.DeliveryId}}">{{row.entity.Location}}</a> </div>',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },
                {name:'OrderNumber',field:'OrderId'},
                { name: 'Customer Name', field: 'CustomerName' },
                { name: 'Driver Name', field: 'DriverName' },
                {name: 'Driver NIN',field: 'DriverNIN'},
                { name: 'Delivery Charge', field: 'DeliveryCost' },
                { name: 'Vehicle Number', field: 'VehicleNumber' },
                {name:'Quantity',field:'Quantity'},
                {name:'BatchNumber',field:'BatchNumber'},
                { name: 'Branch Name', field: 'BranchName' },
              
            ];




        }]);


angular
    .module('homer').controller('OrderDeliveryController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var orderId = $scope.orderId;
            var promise = $http.get('/webapi/DeliveryApi/GetAllDeliveriesForAParticularOrder?orderId=' + orderId, {});
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
                    name: 'Destination', field:'Location',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },
                {name:'OrderNumber',field:'OrderId'},
                { name: 'Customer Name', field: 'CustomerName' },
                { name: 'Driver Name', field: 'DriverName' },
                {name:'Amount',field:'Amount'},
                { name: 'Quantity',field:'Quantity'},
                { name: 'Vehicle Number', field: 'VehicleNumber' },
                {name:'BatchNumber',field:'BatchNumber'},
                { name: 'Branch Name', field: 'BranchName' },
                 { name: 'Delivery Details', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/deliveries/detail/{{row.entity.DeliveryId}}"> Delivery Detail</a> </div>' },

            ];




        }]);



angular
    .module('homer').controller('BranchDeliveryController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var branchId = $scope.branchId;
            var promise = $http.get('/webapi/DeliveryApi/GetAllBranchDeliveries?branchId=' + branchId, {});
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
                    name: 'Destination', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/deliveries/edit/{{row.entity.DeliveryId}}">{{row.entity.Location}}</a> </div>',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },
                {name: 'OrderNumber',field:'OrderId'},
                { name: 'Customer Name', field: 'CustomerName' },
                { name: 'Driver Name', field: 'DriverName' },
                { name: 'Driver NIN', field: 'DriverNIN' },
                { name: 'Delivery Charge', field: 'DeliveryCost' },
                { name: 'Vehicle Number', field: 'VehicleNumber' },
                {name:'BatchNumber',field:'BatchNumber'},
                { name: 'Branch Name', field: 'BranchName' },
                { name: 'Quantity', field: 'Quantity' },

            ];




        }]);


angular
    .module('homer')
    .controller('DeliveryDetailController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$state',
    function ($scope, $http, $filter, $location, $log, $timeout, $state) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var deliveryId = $scope.deliveryId;

        var promise = $http.get('/webapi/DeliveryApi/GetDelivery?deliveryId=' + deliveryId, {});
        promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.delivery = {
                        DeliveryId: b.DeliveryId,
                        CustomerName: b.CustomerName,
                        DeliveryCost: b.DeliveryCost,
                        OrderId: b.OrderId,
                        VehicleNumber: b.VehicleNumber,
                        BranchName: b.BranchName,
                        Price: b.Price,
                        Quantity : b.Quantity,
                        Amount: b.Amount,
                        Location: b.Location,
                        StoreName: b.StoreName,
                        ProductName : b.ProductName,
                        DriverNIN: b.DriverNIN,
                        PaymentModeName: b.PaymentModeName,
                        DriverName: b.DriverName,
                        TimeStamp: b.TimeStamp,
                        Grades: b.Grades,
                        DeliveryBatches: b.DeliveryBatches,
                    };
                });
    }]);

