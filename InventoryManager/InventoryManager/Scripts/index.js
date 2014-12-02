$(document).ready(function () {
    var showForm = function () {
        $('#agregarProductoBtn').hide('fast', function () {
            
            $('#agregarProductoForm').slideDown("slow")
        });
    };

    $('#tablaBody').on("click", ".borrarProductoBtn", function () {
        var productID = $(this).data('pid');
        var formAction = $(this).data('faction');
        var dataString = { id: productID };
        var borrarProductoBtn = $(this);

        $.post(formAction, dataString, function (data) {
            if(data.Success == true)
            {
                // Handle success
                borrarProductoBtn.parent().parent().remove();
                bootbox.alert("El producto se elimino correctamente.");
            }
            else
            {
                // Handle error
                bootbox.alert("Hubo un error al eliminar el producto.");
            }
        });
    });
    
    $('#agregarProductoBtn').click(showForm);

    $('#ajaxform').submit(function (e) {
        e.preventDefault();
        var form = $('#agregarProductoForm');
        var values = $(this).serialize();
        var formAction = $(this).attr('action');

        $.post(formAction, values, function (data) {
            // Usa data.Success y data.Product
            if (data.Success == true) {
                $('#noData').remove();

                $('#tablaBody').append(
                    "<tr><td>" + data.Product.ProductID + "</td>"
                    + "<td>" + data.Product.Name + "</td>"
                    + "<td>" + data.Product.Type + "</td>"
                    + "<td>" + data.Product.Price + "</td>"
                    + "<td>" + data.Product.InStock + "</td>"
                    + "<td><a class='btn btn-primary btn-sm' href='/Home/UpdateProduct?productid=" + data.Product.ProductID + "'>Edit</a></tr>"
                    + "<td><button class='btn btn-danger btn-sm borrarProductoBtn' data-pid='" + data.Product.ProductID + "' data-faction='/Home/DeleteProduct'>Delete</button></td></tr>");
                              

                bootbox.alert("El producto se añadió correctamente.");
                
                form.slideUp('fast', function () {
                    form.find("input[type=text]").val("");
                    $('#agregarProductoBtn').show('fast');
                });
                
            }
            else {                
                bootbox.alert("Hubo un error al agregar el producto.");                
            }
        }, "json");

    });
});