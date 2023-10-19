$(() => {
    const modal = new bootstrap.Modal($('.modal')[0]);


    function refreshTable() {
        $("tbody").empty();
        $.get('/home/getpeople', function (people) {
            people.forEach(function (person) {
                $("tbody").append(`<tr>
                                        <td>${person.firstName}</td>
                                        <td>${person.lastName}</td>
                                        <td>${person.age}</td>
                                        <td>
                                            <button class="btn btn-warning" id="update" data-pereson-id="${person.id}">Update</button>
                                            <button class="btn btn-danger" id="delete" data-pereson-id="${person.id}">Delete</button>
                                        </td>
                                   </tr>`)
            });
        });
    }

    refreshTable();

    $("#add-person").on('click', function () {
        const titel = $("#modal-title").text("Add Person")
        $("#firstName").val('');
        $("#lastName").val('');
        $("#age").val('');
        $("#save-person").show();
        $("#update-person").hide();
        modal.show();
    });

    $("#save-person").on('click', function () {
        const firstName = $("#firstName").val();
        const lastName = $("#lastName").val();
        const age = $("#age").val();

        $.post('/home/addperson', { firstName, lastName, age }, function () {
            modal.hide();
            refreshTable();
        });


    });


    $("table").on("click", "#delete", function () {
        const button = $(this)
        const id = button.data("pereson-id")
        $.post("/home/Delete", { id }, function () {
            refreshTable()
        })

    })

    let id
    $("table").on("click", "#update", function () {
        const button = $(this)
        id = button.data("pereson-id")
        $.post("/home/getById", { id }, function (person) {
            const titel = $("#modal-title").text("Edit Person")
            $(".modal-title").val("Edit Person");
            $("#firstName").val(person.firstName);
            $("#lastName").val(person.lastName);
            $("#age").val(person.age);
            $("#save-person").hide();
            $("#update-person").show();
            modal.show();
        })

      

    })


    $("#update-person").on("click", function () {
        const updatedPerson = {
            id,
            firstName: $("#firstName").val(),
            lastName: $("#lastName").val(),
            age: $("#age").val()
        }
        $.post("/home/update", updatedPerson, function () {
            modal.hide();
            refreshTable()
        })

    })

})