// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

var token = $("[name='__RequestVerificationToken']").val();

$("#addItemBtn").click(function (event) {
    event.preventDefault();

    $("#createOfferItem").submit();

    window.opener.$('#partialTable').html = "";

    $.ajax({
        url: '../Offers/loadItems',
        headers: { "RequestVerificationToken": token },
        success: function (data) {
            window.opener.$('#partialTable').append(data);
        },
        error: function (request) {
            alert(request)
        }
    });
});

//var deleteItemBtns = $(".deleteOfferItem");

//for (let i = 0; i < deleteItemBtns.length; i++) {
    
//    deleteItemBtns[i].click(function (e) {
//        e.preventDefault();

//        alert("registrovao2");

//        $.ajax({
//            type: "POST",
//            url: `../OfferItems/Delete/${i+1}`,
//            headers: { "RequestVerificationToken": token },
//            success: function () {
//                alert("Izbrisano");
//            },
//            error: function () {
//                alert("Nije izbrisano");
//            }
//        });
//    });
//}

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