//Supply defaults for MegaFormsChangeVisually
var MegaFormsChangeVisuallyJQueryParentContainerSelector = '.container';
var MegaFormsChangeVisuallyJQueryHideEffect = 'fast';
var MegaFormsChangeVisuallyJQueryShowEffect = 'slow';
var MegaFormsCascadeJQueryHideEffect = 'fast';
var MegaFormsCascadeJQueryShowEffect = 'fast';
var MegaFormsDetectAllFormChanges = true; //set value whether to detect changes on all forms or not
var MegaFormsDetectChangesWarningMessage = '';  //The message to show if a form value has changed and page is being left.  If blank uses default which includes ID of first element found
var MegaFormsDetectChangesFormClass = 'detect-changes'; //The class to give forms that you want changes detected on if 'MegaFormsDetectAllFormChanges' is false
var MegaFormsIgnoreDetectChangesClass = 'ignore-detect-changes'; //Add this class to any elements you want to allow to be clicked that leave the page but don't show the message (E.g. clear buttons that reset the form)
var MegaFormsDisabledOrReadonlyCssClass = 'ui-state-disabled';  //The class to give controls that are disabled or readonly