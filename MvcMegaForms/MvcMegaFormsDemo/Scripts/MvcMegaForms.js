/*
Copyright (c) 2012 Andrew Newton (http://about.me/nootn)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
var MvcMegaForms = MvcMegaForms || {};

$(document).ready(function () {
    
    $("input[data-val-changevisually]").each(function () {
        var to = $(this).attr('data-val-changevisually-to');
        var otherPropertyName = $(this).attr('data-val-changevisually-otherpropertyname');
        var ifOperator = $(this).attr('data-val-changevisually-ifoperator');
        var value = $(this).attr('data-val-changevisually-value');
        var conditionPassesIfNull = $(this).attr('data-val-changevisually-conditionpassesifnull');

        var dependentProperty = $(this);

        var modelPrefix = dependentProperty.attr('name').substr(0, dependentProperty.attr("name").lastIndexOf(".") + 1);
        var otherProperty = $("[name=" + modelPrefix + otherPropertyName + "]");

        otherProperty.live("change.changevisually", function () {
            MvcMegaForms.ApplyChangeVisually(dependentProperty, otherProperty, to, ifOperator, value, conditionPassesIfNull);
        });

        otherProperty.change();
    });

    $("select[parentlistid]").each(function () {
        var parentId = $(this).attr("parentListId");

        var modelPrefix = $(this).attr("name").substr(0, $(this).attr("name").lastIndexOf(".") + 1);
        var parentList = $("[name=" + modelPrefix + parentId + "]");

        parentList.attr("childid", $(this).attr('id'));

        parentList.live("change.cascade", function () {
            MvcMegaForms.SetupCascadingDropDown($(this));
        });

        parentList.change();

    });
});

$.validator.addMethod('requiredifcontains', function (val, element, dependentproperty, dependentvalue) {
    if (val != null && $.trim(val) != '') {
        return false;
    }
    var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
    var otherProperty = $("[name=" + modelPrefix + dependentproperty + "]");
    var otherVal = MvcMegaForms.GetFormValue(otherProperty);
    for (var i = 0; i < otherVal.length; i++) {
        var currValue = otherVal[i];
        if (currValue == dependentvalue) {
            return false;
        }
    }
    return true;
});
$.validator.unobtrusive.adapters.addSingleVal('requiredifcontains', 'dependentproperty', 'dependentvalue', 'requiredifcontains');

$.validator.addMethod('requiredifnotcontains', function (val, element, dependentproperty, dependentvalue) {
    if (val != null && $.trim(val) != '') {
        return false;
    }
    var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
    var otherProperty = $("[name=" + modelPrefix + dependentproperty + "]");
    var otherVal = MvcMegaForms.GetFormValue(otherProperty);
    for (var i = 0; i < otherVal.length; i++) {
        var currValue = otherVal[i];
        if (currValue == dependentvalue) {
            return true;
        }
    }
    return false;
});
$.validator.unobtrusive.adapters.addSingleVal('requiredifnotcontains', 'dependentproperty', 'dependentvalue', 'requiredifnotcontains');

MvcMegaForms.ApplyChangeVisually = function (dependentProperty, otherProperty, to, ifOperator, value, conditionPassesIfNull) {
    var parentSelector = MegaFormsChangeVisuallyJQueryParentContainerSelector == null ? '.editor-field' : MegaFormsChangeVisuallyJQueryParentContainerSelector;
    var container = dependentProperty.parents(parentSelector);
    if (container == null) {
        alert('MvcMegaForms-ChangeVisually Critical Error: Unable to find parent container with selector: ', +parentSelector + ' for property ' + dependantProperty);
    } else {
        var conditionMet = MvcMegaForms.ConditionMetForChangeVisually(dependentProperty, otherProperty, to, ifOperator, value, conditionPassesIfNull);
        if (to == 'hidden') {
            if (conditionMet) {
                var hideEffect = MegaFormsChangeVisuallyJQueryHideEffect == null ? 'fast' : MegaFormsChangeVisuallyJQueryHideEffect;
                container.hide(hideEffect);
            } else {
                var showEffect = MegaFormsChangeVisuallyJQueryShowEffect == null ? 'fast' : MegaFormsChangeVisuallyJQueryShowEffect;
                container.show(showEffect);
            }
        } else {
            if (conditionMet) {
                dependentProperty.attr('disabled', 'disabled');
                dependentProperty.addClass('ui-state-disabled');
            } else {
                dependentProperty.removeAttr('disabled');
                dependentProperty.removeClass('ui-state-disabled');
            }
        }
    }
};

MvcMegaForms.ConditionMetForChangeVisually = function (dependantProperty, otherProperty, to, ifOperator, value, conditionPassesIfNull) {
    var conditionMet = false;
    var val = MvcMegaForms.GetFormValue(otherProperty);

    if (val == null && value != null) {
        //value is null, condition is met if we wanted it to be met when null
        conditionMet = conditionPassesIfNull;
    } else if (val != null && value == null) {
        //the value is not null, but we were looking for a null, determine what to do
        switch (ifOperator) {
            case "equals":
                conditionMet = false; //we wanted a null and it was not
                break;
            case "notequals":
                conditionMet = true; //we did not want a null and it was not
                break;
            default:
                alert('MvcMegaForms-ChangeVisually Critical Error: When checking for a null value, DisplayChangeIf must be Equals or NotEquals, supplied if operator was ' + ifOperator);
        }
    } else if (val == null && value == null) {
        //both are null, condition is met if we wanted it to be met when null
        conditionMet = conditionPassesIfNull;
    } else //both are not null
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
                for (var iMet = 0; iMet < val.length; iMet++) {
                    var currContainsItem = val[iMet];
                    if (currContainsItem == value) {
                        conditionMet = true;
                        break;
                    }
                }
                break;
            case "notcontains":
                conditionMet = true;
                for (var iNotMet = 0; iNotMet < val.length; iNotMet++) {
                    var currNotContainsItem = val[iNotMet];
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
};

MvcMegaForms.CascadeStringStatus = {
    StartParentId: 0,
    StartChildId: 1,
    EndChildId: 2,
    EndChildValueWithNext: 3,
    EndChildValue: 4
};

MvcMegaForms.SetupCascadingDropDown = function (parentList) {

    MvcMegaForms.CascadeDropDown(parentList);

    var childId = $(parentList).attr('childid');
    var childList = $('#' + childId);

    //if this child has a child one, it's selected event will not fire, so we must call it manually
    var childOfChild = childList.attr('childid');
    if (childOfChild != null && childOfChild != '') {
        MvcMegaForms.CascadeDropDown(childList);
    }
};

MvcMegaForms.CascadeDropDown = function (parentList) {
    var parentVal = MvcMegaForms.GetFormValue($(parentList));
    var childId = $(parentList).attr('childid');
    var childList = $('#' + childId);
    var combos = childList.attr('combos');

    var isChildVisible = childList.is(":visible");
    if (isChildVisible) {
        var hideEffect = MegaFormsCascadeJQueryHideEffect == null ? 'fast' : MegaFormsCascadeJQueryHideEffect;
        childList.hide(hideEffect);
    }

    childList.val(null);
    childList.empty();

    var state = MvcMegaForms.CascadeStringStatus.StartParentId;
    var currParentId = "";
    var currChildId = "";
    var currChildValue = "";
    for (var i = 0; i < combos.length; i++) {
        var val = combos[i];

        //set state
        if (val == "{") {
            state = MvcMegaForms.CascadeStringStatus.StartChildId;
        } else if (val == "~") {
            state = MvcMegaForms.CascadeStringStatus.EndChildId;
        } else if (val == ";") {
            state = MvcMegaForms.CascadeStringStatus.EndChildValueWithNext;
        } else if (val == "}") {
            state = MvcMegaForms.CascadeStringStatus.EndChildValue;
        }

        //set values
        if (state == MvcMegaForms.CascadeStringStatus.StartParentId) {
            currParentId += val;
        } else if (state == MvcMegaForms.CascadeStringStatus.StartChildId) {
            if (currParentId == parentVal) {
                if (val != "{") {
                    currChildId += val;
                }
            } else {
                currParentId = "";
            }
        } else if (state == MvcMegaForms.CascadeStringStatus.EndChildId) {
            if (currParentId != "" && currChildId != "") {
                if (val != "~") {
                    currChildValue += val;
                }
            } else {
                currParentId = "";
                currChildId = "";
            }
        } else if (state == MvcMegaForms.CascadeStringStatus.EndChildValueWithNext) {
            if (currChildId != "") {
                childList.append($('<option></option>').val(currChildId).html(currChildValue));
            }
            state = MvcMegaForms.CascadeStringStatus.StartChildId;
            currChildId = "";
            currChildValue = "";
        } else if (state == MvcMegaForms.CascadeStringStatus.EndChildValue) {
            if (currChildId != "") {
                childList.append($('<option></option>').val(currChildId).html(currChildValue));
            }
            state = MvcMegaForms.CascadeStringStatus.StartParentId;
            currParentId = "";
            currChildId = "";
            currChildValue = "";
        }
    }

    if (isChildVisible) {
        var showEffect = MegaFormsCascadeJQueryShowEffect == null ? 'fast' : MegaFormsCascadeJQueryShowEffect;
        childList.show(showEffect);
    }
};

MvcMegaForms.GetFormValue = function (formControl) {
    var val;
    if (formControl.is(':checkbox')) {
        val = formControl.is(':checked') ? 'true' : 'false';
    } else {
        val = formControl.val();
    }
    return val;
};
