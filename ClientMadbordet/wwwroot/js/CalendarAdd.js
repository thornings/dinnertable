/**
 * 
 * */
function getTotalPieces() {
    var totalPieces = 0; 
    $("foodItemPieces").each(function(){
        totalPieces += $(this).val();
    });
    return totalPieces;
}

function updateFoodWeightType(foodItemId, weightTypeId, newPieces) {

    var newPiecesIsDigits = isNaN(newPieces) === false;

    if (!newPiecesIsDigits) {
        $('.viewError').html("Only digits in Pieces");
        exit;
    }

    var FoodItemWeightTypeChangeViewModel = {
        "FoodItemId": foodItemId,
        "WeightTypeId": weightTypeId,
        "Pieces": newPieces,
    }

    $.ajax({
        type: "POST",
        url: "/CalendarFood/UpdateFoodItemWeightType",
        data: JSON.stringify(FoodItemWeightTypeChangeViewModel),
        contentType: "application/json; charset=utf-8",
            success: function () {
                location.reload();
            },
        error: function (err) {
            var json = err.responseText;
            json = "Could not change Weight Type Correct";
            $('.viewError').html(json);
        }
    });
}

$(document).ready(function () {
    var content = $('#calendarFoodSearchBox').val();

    /* For every keystroke change weight of fooditem */
    $('.calendarFoodWeight').keyup(function () {
        var calendarFoodItemid = $(this).attr("data-calendarfooditemid");
        var newValue = this.value;
        var newValueIsDigits = isNaN(newValue) === false;
        var newValueIsEmpty = newValue.length == 0;

        if (!newValueIsDigits || newValueIsEmpty) {
            msg = "Should only contain digits";
            $(this).closest("td").find("p").html(msg);
            exit;
        }

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
        var newValueIsDigits = isNaN(newValue) === false;

        if (!newPieces) {
            alert("Value can only contain digits");
            exit;
        }

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

    // Food WeightType changes
    $('select').on('change', '', function (e) {
        var selectedFoodWeightUnitId = this.value;
        var selectedFoodItemId = $(this).closest(".foodItemId").attr("id");
        var selectedItemsPieces = $(this).closest("tr").find(".calendarFoodWeight").val();

        updateFoodWeightType(selectedFoodItemId, selectedFoodWeightUnitId, selectedItemsPieces);
    });
});