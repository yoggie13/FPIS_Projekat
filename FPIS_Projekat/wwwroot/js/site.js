// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

var token = $("[name='__RequestVerificationToken']").val();

window.onload = function () {
    
    if (window.location.href.indexOf("Create") > -1) {
        if (localStorage.getItem("employee") !== null) {
            document.getElementById("zEmployee_Name").options.selectedIndex = localStorage.getItem("employee");
            document.getElementById("zClient_Name").options.selectedIndex = localStorage.getItem("client");
            document.getElementById("Date").value = localStorage.getItem("date");
        }
    }
};

$("#openModal").click(function (event) {
    event.preventDefault();

    //localStorage.setItem("employee", document.getElementById("zEmployee_Name").selectedIndex);
    //localStorage.setItem("client", document.getElementById("zClient_Name").selectedIndex);
    //localStorage.setItem("date", document.getElementById("Date").value);

    $("#modal").show();
});
$("#createOrder").click(function (event) {
    localStorage.removeItem("employee");
    localStorage.removeItem("client");
    localStorage.removeItem("date");
});