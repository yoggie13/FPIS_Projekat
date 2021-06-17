// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

var token = $("[name='__RequestVerificationToken']").val();

$("#addItemBtn").click(function (event) {
    window.opener.$("#tableBody").empty();

    $.ajax({
        url: '../Offers/LoadItems',
        headers: { "RequestVerificationToken": token },
        dataType: 'html',
        success: function (data) {
            window.opener.$("#tableBody").append(data);
        },
        error: function (request) {
            alert(request)
        }
    });
});


document.querySelectorAll('.deleteOfferItem').forEach(item => {
    item.addEventListener('click', event => {
        event.preventDefault();

        $.ajax({
            type: "POST",
            url: `../OfferItems/Delete/${item.id}`,
            headers: { "RequestVerificationToken": token },
            success: function () {
                alert("Izbrisano");
            },
            error: function () {
                alert("Nije izbrisano");
            }
        });
    })
});