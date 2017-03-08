$(document).ready(function () {
    $('#myGrid').DataTable({
        "ajax": {
            "url": "../UserRequests/Index/",
            "dataSrc": ""
        },
        "columns": [
        { "data": "Id" },
        { "data": "Email" },
        { "data": "username" },
        { "data": "RequestedBy_Id" },
        { "data": "SaleTypeId" },
        { "data": "RequestesDate" },
        { "data": "StatusId"},
        { "data": "Name" },
        { "data": "GlobalMaster"}]
    });
});