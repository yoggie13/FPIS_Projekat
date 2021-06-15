// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

//$("#addItemBtn").click(function (event) {
//    $.ajax({
//        url: '@Url.Action("LoadItems","Offers")',
//        dataType: 'html',
//        success: function (data) {
//            $('#partialTable').html(data);
//        },
//        error: function (request) {
//            alert(request.responseText)
//        }
//    });
//});
var token = $("[name='__RequestVerificationToken']").val();


$("#deleteOfferItem").click(function (e) {
    e.preventDefault();

    $.ajax({
        type: "POST",
        url: `../OfferItems/Delete/${parseInt(document.getElementById("itemID").innerHTML)}`,
        headers: { "RequestVerificationToken": token },
        success: function () {
            alert("Izbrisano");
        },
        error: function () {
            alert("Nije izbrisano");
        }
    });
});