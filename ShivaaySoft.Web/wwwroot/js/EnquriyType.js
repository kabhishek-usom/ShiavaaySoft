$(document).ready(function () {
    $(document).ready(function () {
        $("#myTable").DataTable({
            "processing": true,
            "serverSide": true, // enabling server side
            "filter": true, //set true for enabling searching
            "ajax": {
                "url": "/EnquiryType/GetList",// ajax url to load content
                "type": "POST", // type of method to call
                "datatype": "json" // return datatype
            },
            "columns": [
                { "data": "id", "name": "Id", "autoWidth": true }, // columns name and related settings
                { "data": "title", "name": "Title", "autoWidth": true },
                {
                    "render": function (data, type, row) { return "<a href='/EnquiryType/Edit/" + row.id + "' class='btn btn-success btn-sm' >Edit</a>  <button type='button' class='btn btn-danger btn-sm' data-delete-user='" + row.id + "' onclick='deleteUser(event)'>Delete</button>"; }
                },
            ]
        });
    });
    console.log(data);
});


function deleteUser(event) {
    const id = event.target.getAttribute("data-delete-user");
    Swal.fire({
        title: "Wait",
        text: "Are You sure, You want to delete this User ?",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        buttonsStyling: false,
        animation: false,
        customClass: {
            popup: 'animated tada',
            confirmButton: 'btn btn-danger',
            cancelButton: 'ml-2 btn btn-secondary'
        },
        confirmButtonColor: "#14C389",
    }).then((result) => {
        if (result.value) {
            $.ajax({
                method: 'POST',
                url: '/EnquiryType/Delete',
                data: {
                    'id': id,
                },
            }).then((response) => {
                setTimeout(function () {
                    location.reload();
                }, 1000);
            }).catch((error) => {
                console.log(error)
            })
        }
    });
}