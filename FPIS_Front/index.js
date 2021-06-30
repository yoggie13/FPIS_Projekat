$("#searchBar").submit(function (event) {
    event.preventDefault();

    debugger;

    var searchTerm = $('#searchTerm').val();
    if (searchTerm === null || searchTerm === "" || searchTerm === " ") {
        alert("Niste uneli ništa za pretragu");
        return;
    }

    $.ajax({

        url: "https://localhost:5001/api/Devices/" + searchTerm,
        type: "POST",
        success: function (data) {
            console.log(data);
        }

    });
});