$(function () {

    function load(page) {
        if (!page) {
            page = 1;
        }

        $.ajax(
   {
       method: "GET",
       url: "/Todo/List?page="+page,
       success: function (data) {
           var row = "";
           data.forEach(function (item) {
               row += "<tr>";
               row += "<td>" + item.Id + "</td>";
               row += "<td>" + item.Text + "</td>";
               row += "<td>" + item.DueDate + "</td>";
               row += "<td>" + item.IsDone + "</td>";
               row += "<td> Keine... </td>";
               row += "</tr>";
           });
           $('#tbData').html(row);
       }
   });
    }

    $('.paginationLink')
        .click(function() {
            var page = $(this).data("page");
            load(page);
        });

    load();
});