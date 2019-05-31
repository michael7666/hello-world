function fileupload(filename)
{
    var inputfile = document.getElementById(filename);
    var files = inputfile.files;
    var data = new FormData();
    for (var i = 0; i !=file.length; i++)
    {
        data.append("files", files[i]);
    }
    $.ajax({

        url:"/uploadfile",
        data: fdata;
        processData: false,
        contentType: false,
        type: "post",
        success: function (data) {
            alert("file upload sucessfully");

        }


    });
}