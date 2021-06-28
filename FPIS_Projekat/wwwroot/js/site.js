// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

var token = $("[name='__RequestVerificationToken']").val();

$("#openModal").click(function (event) {
    event.preventDefault();
    $("#modal").show();
});

$("#addItemBtn").click(function (event) {

    $.ajax({
        url: '../Offers/LoadItems',
        headers: { "RequestVerificationToken": token },
        success: function (data) {
            $("#tableBody").append(data);
        }
    });
});


document.querySelectorAll('.deleteOfferItem').forEach(item => {
    item.addEventListener('click', event => {
        event.preventDefault();

        $.ajax({
            type: "POST",
            url: `../Offers/DeleteOfferItem/${item.parentNode.parentNode.id}`,
            headers: { "RequestVerificationToken": token },
            success: function () {
                alert("Izbrisano");
            },
            error: function () {
                alert("Nije izbrisano");
            }
        });

        $(`#${item.parentNode.parentNode.id}`).remove();





        //$("#tableBody").empty();

        //$.ajax({
        //    url: '../Offers/LoadItems',
        //    headers: { "RequestVerificationToken": token },
        //    dataType: 'html',
        //    success: function (data) {
        //        $("#tableBody").append(data);
        //    },
        //    error: function (request) {
        //        alert(request)
        //    }
        //});
    });
});