$(document).ready(function () {
    $(document).ready(function () {
        $("#myEnquiryTable").DataTable({
            "processing": true,
            "serverSide": true, // enabling server side
            "filter": true, //set true for enabling searching
            "ajax": {
                "url": "/Enquiry/GetList",// ajax url to load content
                "type": "Post", // type of method to call
                "datatype": "json" // return datatype
            },
            "columns": [
                { "data": "id", "name": "Id", "autoWidth": true }, // columns name and related settings
                { "data": "firstName", "name": "FirstName", "autoWidth": true },
                { "data": "lastName", "name": "LastName", "autoWidth": true },
                { "data": "dob", "name": "Dob", "autoWidth": true },
                { "data": "gender", "name": "Gender", "autoWidth": true },
                {
                    "render": function (data, type, row) { return "<a href='/Enquiry/Edit/" + row.id + "' class='btn btn-success btn-sm'>Edit</a>  <button type='button' class='btn btn-danger btn-sm' data-delete-enquiry='" + row.id + "' onclick='deleteEnquiry(event)'>Delete</button>"; }
                },
            ]
        });
    });
});


function deleteEnquiry(event) {
    const id = event.target.getAttribute("data-delete-enquiry");
    Swal.fire({
        title: "Wait",
        text: "Are You sure, You want to delete this Enquiry ?",
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
                url: '/Enquiry/Delete',
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