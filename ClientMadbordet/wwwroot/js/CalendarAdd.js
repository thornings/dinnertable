
$(document).ready(function () {
    var content = $('#calendarFoodSearchBox').val();

    /* For every keystroke change weight of fooditem */
    $('.calendarFoodWeight').keyup(function () {
        var calendarFoodItemid = $(this).attr("data-calendarfooditemid");
        var newValue = this.value;

        if (newValue != content || content == "") {
            var myData = {
                "NewWeight": newValue,
                "CalendarFoodItemId": calendarFoodItemid,
            }

            $.ajax({
                type: "POST",
                url: '/CalendarFood/updateCalendarFoodWeight',
                data: JSON.stringify(myData),
                contentType: "application/json; charset=utf-8",
                success: function () {
                    location.reload();
                },
                error: function (err) {
                    var json = err.responseText;
                    $('#foodsError').html(json);                  
                }
            });
        }
    });

    /* search and refine foods */
    $('#calendarFoodSearchBox').keyup(function () {
        var newValue = $('#calendarFoodSearchBox').val();
        if ( newValue != content || content=="") {
            var myData = {
                "SearchValue": $("#calendarFoodSearchBox").val(),
                "MealId": $("#calendarFoodSearchBox").attr("data-mealid"),
                "FormattedDate": $("#calendarFoodSearchBox").attr("data-dateformatted")
            }
            $.ajax({
                type: "POST",
                url: '/CalendarFood/SearchForFoods',
                data: JSON.stringify(myData),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $('#foods').html(data);
                },
                error: function (err) {
                    var json = err.responseText;
                    $('#foodsError').html(json);
                }
            });
        }
    });
});