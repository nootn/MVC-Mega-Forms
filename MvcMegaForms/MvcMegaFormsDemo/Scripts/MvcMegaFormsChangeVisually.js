$(document).ready(function () {
    //setup DependsOn client side
    $("input[data-val-changevisually]").each(function () {
        var to = $(this).attr('data-val-changevisually-to');
        var otherPropertyName = $(this).attr('data-val-changevisually-otherpropertyname');
        var ifOperator = $(this).attr('data-val-changevisually-ifoperator');
        var value = $(this).attr('data-val-changevisually-value');
        var conditionPassesIfNull = $(this).attr('data-val-changevisually-conditionpassesifnull');
        var jQueryParentContainerSelector = $(this).attr('data-val-changevisually-jqueryparentcontainerselector');

        var dependantProperty = $(this);
        var otherProperty = $('#' + otherPropertyName);
        ApplyChangeVisually(dependantProperty, otherProperty, to, ifOperator, value, conditionPassesIfNull, jQueryParentContainerSelector);

        otherProperty.change(function () {
            ApplyChangeVisually(dependantProperty, otherProperty, to, ifOperator, value, conditionPassesIfNull, jQueryParentContainerSelector);
        });
    });
});

function ConditionMetForChangeVisually(dependantProperty, otherProperty, to, ifOperator, value, conditionPassesIfNull) {
    var conditionMet = false;

    var val;

    if (otherProperty.is(':checkbox')) {
        val = otherProperty.is(':checked') ? 'true' : 'false';
    }
    else {
        val = otherProperty.val();
    }

    if (val == null && value != null) {
        //value is null, condition is met if we wanted it to be met when null
        conditionMet = conditionPassesIfNull;
    }
    else if (val != null && value == null) {
        //the value is not null, but we were looking for a null, determine what to do
        switch (ifOperator) {
            case "equals":
                conditionMet = false;  //we wanted a null and it was not
                break;
            case "notequals":
                conditionMet = true;  //we did not want a null and it was not
                break;
            default:
                alert('MvcMegaForms-ChangeVisually Critical Error: When checking for a null value, DisplayChangeIf must be Equals or NotEquals, supplied if operator was ' + ifOperator);
        }
    }
    else if (val == null && value == null) {
        //both are null, condition is met if we wanted it to be met when null
        conditionMet = conditionPassesIfNull;
    }
    else //both are not null
    {
        switch (ifOperator) {
            case "equals":
                conditionMet = val == value;
                break;
            case "notequals":
                conditionMet = val != value;
                break;
            case "greaterthan":
                conditionMet = val > value;
                break;
            case "greaterthanorequals":
                conditionMet = val >= value;
                break;
            case "lessthan":
                conditionMet = val < value;
                break;
            case "lessthanorequals":
                conditionMet = val <= value;
                break;
            case "contains":
                for (var currContainsItem in val) {
                    if (currContainsItem == value) {
                        conditionMet = true;
                        break;
                    }
                }
                break;
            case "notcontains":
                conditionMet = true;
                for (var currNotContainsItem in val) {
                    if (currNotContainsItem == value) {
                        conditionMet = false;
                        break;
                    }
                }
                break;
            default:
                alert('MvcMegaForms-ChangeVisually Critical Error: Unknown DisplayChangeIf supplied ' + ifOperator);
        }
    }
    return conditionMet;
}

function ApplyChangeVisually(dependantProperty, otherProperty, to, ifOperator, value, conditionPassesIfNull, jQueryParentContainerSelector) {

    var container = dependantProperty.parents(jQueryParentContainerSelector);
    if (container == null) {
        alert('MvcMegaForms-ChangeVisually Critical Error: Unable to find parent container with selector: ', +jQueryParentContainerSelector + ' for property ' + dependantProperty);
    }
    else {
        var conditionMet = ConditionMetForChangeVisually(dependantProperty, otherProperty, to, ifOperator, value, conditionPassesIfNull);
        if (to == 'hidden') {
            if (conditionMet) {
                container.hide();
            }
            else {
                container.show();
            }
        }
        else {
            if (conditionMet) {
                dependantProperty.attr('disabled', 'disabled');
                dependantProperty.addClass('ui-state-disabled');
            }
            else {
                dependantProperty.removeAttr('disabled');
                dependantProperty.removeClass('ui-state-disabled');
            }
        }
    }
}