$(function () {

    //#region Event

    // 寫入客戶
    $("#insertCustomer").on("click", function () {
        InsertCustomer();
    });

    // 更新客戶
    $("#updateCustomer").on("click", function () {
        UpdateCustomer();
    });

    // 刪除客戶
    $("#deleteCustomer").on("click", function () {
        DeleteCustomer();
    });

    // 寫入訂單
    $("#insertOrder").on("click", function () {
        InsertOrder();
    });

    // 更新訂單
    $("#updateOrder").on("click", function () {
        UpdateOrder(11082);
    });

    // 刪除訂單
    $("#deleteOrder").on("click", function () {
        DeleteOrder(11082);
    });

    // 測試Uow
    $("#testUow").on("click", function () {
        DeleteAfterInsertedCustomer("One");
    });

    //#endregion

    //#region Private Method

    function InsertCustomer() {
        
        var data = {
            "CustomerID": "8787R",
            "CompanyName": "defaultCompanyName"
        };

        $.ajax({
            type: "POST",
            crossDomain: true,
            url: "https://localhost:44339/Customers/Insert",
            headers: {
                "Content-Type": "application/json"
            },
            data: JSON.stringify(data),
            success: function (data) {
                console.log(data);
            },
            error: function (xhr, responseText) {
                console.log(xhr);
                console.log(responseText);
            }
        });
    }

    function UpdateCustomer() {

        var data = {
            "CustomerID": "8787R",
            "CompanyName": "CompanyNameUpdated"
        };

        $.ajax({
            type: "PUT",
            crossDomain: true,
            url: "https://localhost:44339/Customers/Update",
            headers: {
                "Content-Type": "application/json"
            },
            data: JSON.stringify(data),
            success: function (data) {
                console.log(data);
            },
            error: function (xhr, responseText) {
                console.log(xhr);
                console.log(responseText);
            }
        });
    }

    function DeleteCustomer() {

        $.ajax({
            type: "DELETE",
            crossDomain: true,
            url: "https://localhost:44339/Customers/Delete/".concat("8787R"),
            headers: {
                "Content-Type": "application/json"
            },
            success: function (data) {
                console.log(data);
            },
            error: function (xhr, responseText) {
                console.log(xhr);
                console.log(responseText);
            }
        });
    }

    function InsertOrder() {

        var data = {
            "CustomerID": "CHOPS",
            "EmployeeID": 9,
            "ShipVia": 1
        };

        $.ajax({
            type: "POST",
            crossDomain: true,
            url: "https://localhost:44339/Orders/Insert",
            headers: {
                "Content-Type": "application/json"
            },
            data: JSON.stringify(data),
            success: function (data) {
                console.log(data);
            },
            error: function (xhr, responseText) {
                console.log(xhr);
                console.log(responseText);
            }
        });
    }

    function UpdateOrder(orderId) {

        var data = {
            "OrderID": orderId,
            "ShipName": "ShipNameUpdated"
        };

        $.ajax({
            type: "PUT",
            crossDomain: true,
            url: "https://localhost:44339/Orders/Update",
            headers: {
                "Content-Type": "application/json"
            },
            data: JSON.stringify(data),
            success: function (data) {
                console.log(data);
            },
            error: function (xhr, responseText) {
                console.log(xhr);
                console.log(responseText);
            }
        });
    }

    function DeleteOrder(orderId) {

        $.ajax({
            type: "DELETE",
            crossDomain: true,
            url: "https://localhost:44339/Orders/Delete/".concat(orderId),
            headers: {
                "Content-Type": "application/json"
            },
            success: function (data) {
                console.log(data);
            },
            error: function (xhr, responseText) {
                console.log(xhr);
                console.log(responseText);
            }
        });
    }

    function DeleteAfterInsertedCustomer(scenarioEnglishNumber) {

        var data = {
            "CustomerID": "8787R",
            "CompanyName": "defaultCompanyName"
        };

        $.ajax({
            type: "POST",
            crossDomain: true,
            url: "https://localhost:44339/Uow/DeleteAfterInserted_Scenario" + scenarioEnglishNumber,
            headers: {
                "Content-Type": "application/json"
            },
            data: JSON.stringify(data),
            success: function (data) {
                console.log(data);
            },
            error: function (xhr, responseText) {
                console.log(xhr);
                console.log(responseText);
            }
        });
    }

    //#endregion
});