

function GetAllProducts() {
    var options = {
        url: "/Home/GetProducts",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }

    $.ajax(options).done(function (data) {
        if (data != null && data.length > 0) {
            var TabelHTML = "";

            for (var i = 0; i < data.length; i++) {
                TabelHTML += "<tr><td>" + data[i].name + "</td>" +
                    "<td>R <input style='width:120px;' value='" + data[i].price + "' type='text' class='form-control' data-product-id='" + data[i].id + "' /></td>" +
                    "<td><input style='width:50px;' value='1' type='text' class='form-control' data-pqty-id='" + data[i].id + "' /></td>" +
                    "<td><input type='checkbox' data-padd-id='" + data[i].id + "' /></td></tr>";
            }

            $('#ProductsTable').empty().append(TabelHTML);
        }
    });
}


function CreateDocument() {
    var lines = GetSelectedLines();

    var orderDocument = {
        CustomerID: $('#Customer :selected').val(),
        UserID: $('#Users :selected').val(),
        OrderLines: lines,
        TotalIncl: $('#TotalIncl').val(),
        TotalExcl: $('#TotalExcl').val(),
        Discount: $('#Discount').val(),
        DeliveryDate: $('#DeliveryDate').val(),
        InvoiceNo: $('#InvoiceNo').val(),
        ReferenceNo: $('#ReferenceNo').val()
    };

    var options = {
        url: "/Home/CreateOrderDocument",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(orderDocument)
    }

    $.ajax(options).done(function (data) {
        console.log(data);
        if (data.success) {
            window.location = "/Home/Documents";
        }
    });
}


function addSelectedLines() {
    var total = 0;

    $('[data-padd-id]:checkbox:checked').each(function () {
        var lID = $(this).attr('data-padd-id');
        var price = parseFloat($('[data-product-id="' + lID + '"]').val());
        var qty = parseInt($('[data-pqty-id="' + lID + '"]').val());

        total += (price * qty);
    });

    $('#TotalExcl').val(total);
    $('#TotalIncl').val((total * 1.14).toFixed(2));

    $('.bs-example-modal-lg').modal('hide');
}

function GetSelectedLines() {

    var lArray = new Array();

    $('[data-padd-id]:checkbox:checked').each(function () {
        var lID = $(this).attr('data-padd-id');
        var qty = parseInt($('[data-pqty-id="' + lID + '"]').val());
        var price = parseFloat($('[data-product-id="' + lID + '"]').val());

        var OrderLine = {
            lineID: lID,
            linePrice: price,
            lineQTY: qty,
            lineTotal: (price * qty)
        };

        lArray.push(OrderLine);
    });

    return lArray
}