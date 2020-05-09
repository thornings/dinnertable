// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("[data-toggle=popover]").popover({ html: true });
    var totalsmenuitemdiv = $(this).find(".totalsmenuitem");

    /* CHART */
    var canvasElements = $('.myChart');

    canvasElements.each(function () {        
        var canvasElement = $(this);
        var ctx = canvasElement.get(0).getContext('2d');
        ctx.fillStyle = "#FED";
        ctx.fillRect(0, 0, canvasElement.width, canvasElement.height);

        var proteinValue = parseFloat(canvasElement.data("proteins"));
        var carbValue = parseFloat(canvasElement.data("carbs"));
        var fatValue = parseFloat(canvasElement.data("fats"));
        var carbsText = canvasElement.data("titlecarbs");
        var proteinText = canvasElement.data("titleproteins");
        var fatText = canvasElement.data("titlefats");

        if (carbValue == '0') {
            carbValue = '0.001';
        }

        if (proteinValue == '0' ) {
            proteinValue = '0.0001';
        }

        if (fatValue == '0') {
             fatValue = '0.0001';
        }
        
        var labels = [proteinText, carbsText, fatText];
        var YValues = [proteinValue, carbValue, fatValue];

        var data = {
            labels: labels,
            datasets: [{
                label: "",
                backgroundColor: [
                    'rgba(115, 115, 115, 1)',
                    'rgba(153, 153, 255, 1)',
                    'rgba(107, 0, 179,1)'
                ],
                borderColor: [
                    'rgba(255,255,255,1)',
                    'rgba(255,255,255, 1)',
                    'rgba(255, 255, 255)'
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
        }

        var myChart = new Chart(ctx,
            {
                options: options,
                data: data,
                type: 'pie'
            }
        )
        
    });

});  
