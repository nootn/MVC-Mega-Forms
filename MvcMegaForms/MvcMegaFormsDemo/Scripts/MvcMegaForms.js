/*
Copyright (c) 2012 Andrew Newton (http://about.me/nootn)
 
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
var MvcMegaForms = MvcMegaForms || {};

var MvcMegaFormsLeavingPageDueToSubmit = false;

$(document).ready(function () {
    MvcMegaForms.AttachEvents();

    if (typeof MegaFormsDetectAllFormChanges !== undefined && MegaFormsDetectAllFormChanges === true) {
        //wire up submit buttons
        $("input:submit").each(function () {
            var $me = $(this);
            $me.click(function () {
                MvcMegaFormsLeavingPageDueToSubmit = true;
            });
        });

        //ensure all selects that have options have a selected option (otherwise it will always say they changed)
        $("select").each(function () {
            var $me = $(this);
            if ($me.attr('multiple') === undefined && $me.find('option').length > 0) {
                var foundDefaultSelected = false;
                $me.find('option').each(function () {
                    if (this.defaultSelected) {
                        foundDefaultSelected = true;
                        return;
                    }
                });
                if (!foundDefaultSelected) {
                    var $firstOption = $me.find("option:first-child");
                    $firstOption.attr("selected", true);
                    $firstOption.attr("defaultSelected", true);
                }
            }
        });
    }
});

$(window).bind("beforeunload", function (event) {
    if (typeof MegaFormsDetectAllFormChanges !== undefined && MegaFormsDetectAllFormChanges === true
        && !MvcMegaFormsLeavingPageDueToSubmit) {
        var doNoLeaveMessage = '';
        $("form").each(function () {
            doNoLeaveMessage = MvcMegaForms.AlertFormChanged($(this));
            if (doNoLeaveMessage !== '') {
                return;
            }
        });
        if (doNoLeaveMessage !== '') {
            return doNoLeaveMessage;
        }
    }
});

$.validator.addMethod('requiredifcontains', function (val, element, dependentproperty, dependentvalue) {
    if (val !== null && $.trim(val) !== '') {
        return false;
    }
    var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
    var otherProperty = $("[name='" + modelPrefix + dependentproperty + "']");
    var otherVal = MvcMegaForms.GetFormValue(otherProperty).toLowerCase();
    for (var i = 0; i < otherVal.length; i++) {
        var currValue = otherVal[i].toLowerCase();
        if (currValue === dependentvalue) {
            return false;
        }
    }
    return true;
});
$.validator.unobtrusive.adapters.addSingleVal('requiredifcontains', 'dependentproperty', 'dependentvalue', 'requiredifcontains');

$.validator.addMethod('requiredifnotcontains', function (val, element, dependentproperty, dependentvalue) {
    if (val !== null && $.trim(val) !== '') {
        return false;
    }
    var modelPrefix = element.name.substr(0, element.name.lastIndexOf(".") + 1);
    var otherProperty = $("[name='" + modelPrefix + dependentproperty + "']");
    var otherVal = MvcMegaForms.GetFormValue(otherProperty).toLowerCase();
    for (var i = 0; i < otherVal.length; i++) {
        var currValue = otherVal[i].toLowerCase();
        if (currValue === dependentvalue) {
            return true;
        }
    }
    return false;
});
$.validator.unobtrusive.adapters.addSingleVal('requiredifnotcontains', 'dependentproperty', 'dependentvalue', 'requiredifnotcontains');

MvcMegaForms.AttachEvents = function () {
    $(":input").each(function () {
        var tos = $(this).attr('data-val-changevisually-to');
        if (tos !== null && tos !== '') {
            var toValues = tos.split("~");
            var otherPropertyNames = $(this).attr('data-val-changevisually-otherpropertyname').split("~");
            var ifOperators = $(this).attr('data-val-changevisually-ifoperator').split("~");
            var values = $(this).attr('data-val-changevisually-value').split("~");
            var conditionPassesIfNulls = $(this).attr('data-val-changevisually-conditionpassesifnull').split("~");

            var dependentProperty = $(this);

            //Get each unique other property name
            var uniqueOtherPropertyNames = $.unique(otherPropertyNames.slice());

            //go through each 'other' field and hook up the change event
            for (var iOuter = 0; iOuter < uniqueOtherPropertyNames.length; iOuter++) {
                var fullName = dependentProperty.attr('name').substr(0, dependentProperty.attr("name").lastIndexOf(".") + 1) + uniqueOtherPropertyNames[iOuter];
                var otherProperty = $("[name='" + fullName + "']");
                otherProperty.change({ otherPropertyOuterInitialName: uniqueOtherPropertyNames[iOuter], otherPropertyFullName: fullName }, function (event) {
                    for (var iInner = 0; iInner < otherPropertyNames.length; iInner++) {
                        if (otherPropertyNames[iInner] === event.data.otherPropertyOuterInitialName) {
                            var currentOtherProperty = $("[name='" + event.data.otherPropertyFullName + "']");
                            if (MvcMegaForms.ApplyChangeVisually(dependentProperty, currentOtherProperty, toValues[iInner], ifOperators[iInner], values[iInner], conditionPassesIfNulls[iInner])) {
                                break; //a condition has passed, don't process the rest
                            }
                        }
                    }
                });

                otherProperty.change();
            }
        }

        var parentId = $(this).attr("parentListId");
        if (parentId !== null && parentId !== '') {
            var parentList = $("[name='" + $(this).attr("name").substr(0, $(this).attr("name").lastIndexOf(".") + 1) + parentId + "']");

            parentList.attr("childid", $(this).attr('id'));

            parentList.change(function () {
                MvcMegaForms.SetupCascadingDropDown($(this));
            });

            parentList.change();
        }
    });
};

MvcMegaForms.ApplyChangeVisually = function (dependentProperty, otherProperty, to, ifOperator, value, conditionPassesIfNull) {
    var parentSelector = MegaFormsChangeVisuallyJQueryParentContainerSelector === null ? '.editor-field' : MegaFormsChangeVisuallyJQueryParentContainerSelector;
    var container = dependentProperty.parents(parentSelector);
    if (container === null) {
        alert('MvcMegaForms-ChangeVisually Critical Error: Unable to find parent container with selector: ', +parentSelector + ' for property ' + dependantProperty);
        return false;
    } else {
        var showEffect = MegaFormsChangeVisuallyJQueryShowEffect === null ? 'fast' : MegaFormsChangeVisuallyJQueryShowEffect;
        var hideEffect = MegaFormsChangeVisuallyJQueryHideEffect === null ? 'fast' : MegaFormsChangeVisuallyJQueryHideEffect;

        var conditionMet = MvcMegaForms.ConditionMetForChangeVisually(dependentProperty, otherProperty, to, ifOperator, value, conditionPassesIfNull);
        if (conditionMet) {
            if (to === 'hidden') {
                //hide
                container.hide(hideEffect);

                //enable
                dependentProperty.removeAttr('disabled');
                dependentProperty.removeClass('ui-state-disabled');
            }
            else {
                //show  
                container.show(showEffect);

                //disable
                dependentProperty.attr('disabled', 'disabled');
                dependentProperty.addClass('ui-state-disabled');
            }
        }
        else {
            //show
            container.show(showEffect);

            //enable
            dependentProperty.removeAttr('disabled');
            dependentProperty.removeClass('ui-state-disabled');
        }
        return conditionMet;
    }
};

MvcMegaForms.ConditionMetForChangeVisually = function (dependantProperty, otherProperty, to, ifOperator, value, conditionPassesIfNull) {
    var conditionMet = false;
    conditionPassesIfNull = conditionPassesIfNull.toLowerCase() === 'true'; //it was a string, make it a bool
    var val = MvcMegaForms.GetFormValue(otherProperty);

    //treat empty string as null
    if (val === '') {
        val = null;
    }
    if (value === '') {
        value = null;
    }

    if (val === null && value !== null) {
        //value is null, condition is met if we wanted it to be met when null
        conditionMet = conditionPassesIfNull;
    } else if (val !== null && value === null) {
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
    } else if (val === null && value === null) {
        //both are null, condition is met if we wanted it to be met when null
        conditionMet = conditionPassesIfNull;
    } else //both are not null
    {
        val = val.toString().toLowerCase();
        value = value.toString().toLowerCase();
        switch (ifOperator) {
            case "equals":
                conditionMet = val === value;
                break;
            case "notequals":
                conditionMet = val !== value;
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
                    var currContainsItem = val[iMet].toLowerCase();
                    if (currContainsItem === value) {
                        conditionMet = true;
                        break;
                    }
                }
                break;
            case "notcontains":
                conditionMet = true;
                for (var iNotMet = 0; iNotMet < val.length; iNotMet++) {
                    var currNotContainsItem = val[iNotMet].toLowerCase();
                    if (currNotContainsItem === value) {
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

    //if this child has a child one, it's change event will not fire, so we must call it manually
    var childOfChild = childList.attr('childid');
    if (childOfChild !== null && childOfChild !== '') {
        childList.change();
    }
    else {
        //find if there are any form elements that depend on the child one for changevisually
        var indexOfLastDot = childList.attr("name").lastIndexOf(".");
        var childNameWithoutPrefix = childList.attr('name').substr((indexOfLastDot < 0 ? -1 : indexOfLastDot) + 1);
        $(":input[data-val-changevisually-otherpropertyname]").each(function () {
            var valArray = $(this).attr("data-val-changevisually-otherpropertyname").split("~");
            if ($.inArray(childNameWithoutPrefix, valArray)) {
                childList.change();
            }
        });
    }
};

MvcMegaForms.CascadeDropDown = function (parentList) {
    var parentVal = MvcMegaForms.GetFormValue($(parentList));
    var childId = $(parentList).attr('childid');
    var childList = $('#' + childId);
    var combos = childList.attr('combos');

    var isChildVisible = childList.is(":visible");
    if (isChildVisible) {
        var hideEffect = MegaFormsCascadeJQueryHideEffect === null ? 'fast' : MegaFormsCascadeJQueryHideEffect;
        childList.hide(hideEffect);
    }

    var initialVal = MvcMegaForms.GetFormValue(childList);

    childList.val(null);
    childList.empty();

    var state = MvcMegaForms.CascadeStringStatus.StartParentId;
    var currParentId = "";
    var currChildId = "";
    var currChildValue = "";
    for (var i = 0; i < combos.length; i++) {
        var val = combos[i];

        //set state
        if (val === "{") {
            state = MvcMegaForms.CascadeStringStatus.StartChildId;
        } else if (val === "~") {
            state = MvcMegaForms.CascadeStringStatus.EndChildId;
        } else if (val === ";") {
            state = MvcMegaForms.CascadeStringStatus.EndChildValueWithNext;
        } else if (val === "}") {
            state = MvcMegaForms.CascadeStringStatus.EndChildValue;
        }

        //set values
        if (state === MvcMegaForms.CascadeStringStatus.StartParentId) {
            currParentId += val;
        } else if (state === MvcMegaForms.CascadeStringStatus.StartChildId) {
            if (currParentId === parentVal) {
                if (val !== "{") {
                    currChildId += val;
                }
            } else {
                currParentId = "";
            }
        } else if (state === MvcMegaForms.CascadeStringStatus.EndChildId) {
            if (currParentId !== "" && currChildId !== "") {
                if (val !== "~") {
                    currChildValue += val;
                }
            } else {
                currParentId = "";
                currChildId = "";
            }
        } else if (state === MvcMegaForms.CascadeStringStatus.EndChildValueWithNext) {
            if (currChildId !== "") {
                if (currChildId === initialVal) {
                    childList.append($('<option selected="selected"></option>').val(currChildId).html(currChildValue));
                }
                else {
                    childList.append($('<option></option>').val(currChildId).html(currChildValue));
                }
            }
            state = MvcMegaForms.CascadeStringStatus.StartChildId;
            currChildId = "";
            currChildValue = "";
        } else if (state === MvcMegaForms.CascadeStringStatus.EndChildValue) {
            if (currChildId !== "") {
                if (currChildId === initialVal) {
                    childList.append($('<option selected="selected"></option>').val(currChildId).html(currChildValue));
                }
                else {
                    childList.append($('<option></option>').val(currChildId).html(currChildValue));
                }
            }
            state = MvcMegaForms.CascadeStringStatus.StartParentId;
            currParentId = "";
            currChildId = "";
            currChildValue = "";
        }
    }

    if (isChildVisible) {
        var showEffect = MegaFormsCascadeJQueryShowEffect === null ? 'fast' : MegaFormsCascadeJQueryShowEffect;
        childList.show(showEffect);
    }
};

MvcMegaForms.GetFormValue = function (formControl) {
    var val;
    if (formControl.is(':checkbox')) {
        val = formControl.is(':checked') ? 'true' : 'false';
    }
    else if (formControl.is(':radio')) {
        val = $("input:radio[name='" + formControl.attr('name') + "']:checked").val();
    } else {
        val = formControl.val();
    }
    return val;
};

MvcMegaForms.FormControlValueHasChanged = function (formControl) {
    var $formControl = $(formControl);

    if ($formControl.is(':checkbox')) {
        return (formControl.checked !== formControl.defaultChecked);
    }
    else if ($formControl.is(':radio')) {
        return (formControl.checked !== formControl.defaultChecked);
    }
    else if ($formControl.is('select') && $formControl.attr('multiple') !== undefined) {
        if (formControl.options === null || formControl.options.length <= 0) {
            return false;
        }
        var allCndMet = false;
        for (var i = 0; i < formControl.options.length; i++) {
            var currOpt = formControl.options[i];
            allCndMet = allCndMet || (currOpt.selected !== currOpt.defaultSelected);
        }
        return allCndMet;
    }
    else if ($formControl.is('select')) {
        if (formControl.options === null || formControl.options.length <= 0) {
            return false;
        }
        return !(formControl.options[formControl.selectedIndex].defaultSelected);
    }
    else {
        return (formControl.value !== formControl.defaultValue);
    }
};

MvcMegaForms.FormFieldIdChanged = function ($form) {
    var changedId = null;
    $form.find('input').each(function () {
        //specifically leave 'this' as non-jquery
        if (MvcMegaForms.FormControlValueHasChanged(this)) {
            changedId = this.id === null ? this.name : this.id;
            return;
        }
    });
    if (changedId === null) {
        $form.find('select').each(function () {
            //specifically leave 'this' as non-jquery
            if (MvcMegaForms.FormControlValueHasChanged(this)) {
                changedId = this.id === null ? this.name : this.id;
                return;
            }
        });
    }
    return changedId;
};

MvcMegaForms.AlertFormChanged = function ($form) {
    var changedId = MvcMegaForms.FormFieldIdChanged($form);
    if (changedId !== null) {
        var confMsg = "At least one unsaved value has changed ('" + changedId + "'), are you sure you want to leave the page?";
        if (typeof MegaFormsDetectChangesWarningMessage !== undefined && MegaFormsDetectChangesWarningMessage !== '') {
            confMsg = MegaFormsDetectChangesWarningMessage;
        }
        return confMsg;
    }
    return '';
};