
function createWeighttypeInput(selectedWeighttypesdiv, unitname, unitid) {
    var newDiv   = $(document.createElement('div')).attr("class", "form-group mb-3 ");
    var newLabel = $(document.createElement("label")).attr('class', 'control-label').html(unitname);
    var newTextbox = $(document.createElement("input"))
            .attr('class', 'form-control')
            .attr('type', 'text')
            .attr('name', 'FoodWeightValues[]');
    var newHidden = $(document.createElement("input"))
        .attr('class', 'foodWeightIds-'+unitid)
        .attr('type', 'hidden')
        .attr('name', 'FoodWeightIds[]')
        .attr('value', unitid );                        
    newDiv.append(newLabel);
    newDiv.append(newTextbox);
    newDiv.append(newHidden);
    
    selectedWeighttypesdiv.append(newDiv);

    
    //selectedWeighttypesdiv.append(newlabel.html());

        //newDiv.after().html(
    //    .attr("class", "form-controle").attr(';
    //type = 'text' name = 'weighttypes[]'

}

//function findAndRemoveWeighttypeInput(this, unitname) {
//    input = jQuery('<input name="myname">');
//}

$(document).ready(function () {

    var inputdiv = $('#weighttype-with-input');


    $('.weighttypescheck').on('click', function () {
        if ($(this).is(':checked')) {
            var selectedWeighttypesdiv = $('.selected-weighttypes');
            if (selectedWeighttypesdiv != null) {
                var selectedHiddenUnitname = $(this).attr('id') + '-unitname';
                var unitname = $('.' + selectedHiddenUnitname);
                createWeighttypeInput(selectedWeighttypesdiv, unitname.val(), $(this).attr('id') );
               // alert(JSON.stringify(unitname.val(), null, 4));

            }
            // create form element
            // create label
            // create inputfield
            // create hidden
           // inputdiv.append($(this).val());       
        } else {
            // check if already exist
            // remove it
        }
    });


    ///* For every keystroke change weight of fooditem */
    //$('.calendarFoodWeight').keyup(function () {
    //    var calendarFoodItemid = $(this).attr("data-calendarfooditemid");
    //    var newValue = this.value;

    //    if (newValue != content || content == "") {
    //        var myData = {
    //            "NewWeight": newValue,
    //            "CalendarFoodItemId": calendarFoodItemid,
    //        }

    //        $.ajax({
    //            type: "POST",
    //            url: '/CalendarFood/updateCalendarFoodWeight',
    //            data: JSON.stringify(myData),
    //            contentType: "application/json; charset=utf-8",
    //            success: function () {
    //                location.reload();
    //            },
    //            error: function (err) {
    //                var json = err.responseText;
    //                $('#foodsError').html(json);                  
    //            }
    //        });
    //    }
    //});

    ///* search and refine foods */
    //$('#calendarFoodSearchBox').keyup(function () {
    //    var newValue = $('#calendarFoodSearchBox').val();
    //    if ( newValue != content || content=="") {
    //        var myData = {
    //            "SearchValue": $("#calendarFoodSearchBox").val(),
    //            "MealId": $("#calendarFoodSearchBox").attr("data-mealid"),
    //            "FormattedDate": $("#calendarFoodSearchBox").attr("data-dateformatted")
    //        }
    //        $.ajax({
    //            type: "POST",
    //            url: '/CalendarFood/SearchForFoods',
    //            data: JSON.stringify(myData),
    //            contentType: "application/json; charset=utf-8",
    //            success: function (data) {
    //                $('#foods').html(data);
    //            },
    //            error: function (err) {
    //                var json = err.responseText;
    //                $('#foodsError').html(json);
    //            }
    //        });
    //    }
    //});
});