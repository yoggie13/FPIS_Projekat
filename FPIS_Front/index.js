$("#searchBar").submit(function (event) {
    event.preventDefault();


    var searchTerm = $('#searchTerm').val();
    if (searchTerm === null || searchTerm === "" || searchTerm === " ") {
        alert("Niste uneli ništa za pretragu");
        return;
    }

    $.ajax({

        url: "http://localhost:11807/api/Devices/" + searchTerm,
        type: "POST",
        success: function (data) {
            console.log(data);
        }

    });
});