// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("[data-toggle=popover]").popover({ html: true });

    //$('#calendarMenuButton').on("click", function () {
    //    var faElement = $(this).find('i');
    //    var isOpen = faElement.hasClass("fa-rotate-45");
    //    if (isOpen)
    //    {
    //        faElement.removeClass('fa-rotate-45');
    //    } else {
    //        faElement.addClass('fa-rotate-45').addClass("fullSizeCalendar");
    //    }
    //});


    /* CHART */
    var canvasElements = $('.myChart');

    canvasElements.each(function(){
        var canvasElement = $(this);
        var ctx = canvasElement.get(0).getContext('2d');
        ctx.fillStyle = "red";
        ctx.fillRect(0, 0, canvasElement.width, canvasElement.height);

        var proteinValue = parseFloat(canvasElement.closest('#tableline').find(".proteinValue").text());
        var carbValue = parseFloat(canvasElement.closest('#tableline').find(".carbValue").text());
        var fatValue = parseFloat(canvasElement.closest('#tableline').find(".fatValue").text());

        if (proteinValue != '0' && carbValue != '0' && fatValue != '0') {
            var labels = ['protein', 'carbs', 'fat'];
            var YValues = [proteinValue, carbValue, fatValue];

            var data = {
                labels: labels,
                datasets: [{
                    label: "",
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 0, 255)'
                    ],
                    borderColor: [
                        'rgba(255,99,132,1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 0, 255)'
                    ],
                    borderWidth: 1,
                    data: YValues
                }]
            };

            var options = {
                maintainAspectRatio: true,
                responsive: false,
                legend: {
                    display: false
                }
            };

            var myChart = new Chart(ctx,
                {
                    options: options,
                    data: data,
                    type: 'pie'
                });
        }
    });

});  
