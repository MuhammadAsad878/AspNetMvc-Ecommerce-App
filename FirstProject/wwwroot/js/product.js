var dataTable;
$(document).ready(() => !dataTable ? loadDataTable() : "" );
console.log("Product.js run...");

function loadDataTable() {
    dataTable = $("#productTable").DataTable(
        {
            "ajax": { url: "/Admin/Product/GetAll" },
            "columns": [
                { data: 'title', "width": "10%" },
                { data: 'author', "width": "10%" },
                { data: 'isbn', "width": "10%" },
                { data: 'price', "width": "10%" },
                { data: 'price50', "width": "10%" },
                { data: 'price100', "width": "10%" },
                { data: 'category.name', "width": "10%" },
                {
                    data: 'id',
                    width: "20%",
                    render: function (data) {
                        return `<div class="btn-group w-75 mx-auto d-flex" role="group">
                                <a href='/Admin/Product/Upsert?id=${data}' class="btn rounded rounded-2 btn-primary mx-2"> <i class="bi bi-pencil"></i> Edit <a/>
                                <a onClick=Delete('/Admin/Product/Delete?id=${data}')  class="btn rounded rounded-2 btn-danger"> <i class="bi bi-trash-fill"></i> Delete <a/>
                                </div>`;
                    }
                }

            ]
        });  
}
function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {          
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                },
            });          
        }
    });
}
