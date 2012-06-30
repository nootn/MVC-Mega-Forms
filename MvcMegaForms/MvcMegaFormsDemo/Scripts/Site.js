//Supply defaults for MegaForms
var MegaFormsChangeVisuallyJQueryParentContainerSelector = '.container'; //sets the class of a component that wraps a whole form control (I.e. label + input + validation message)
var MegaFormsChangeVisuallyJQueryHideEffect = 'fast'; //sets the jQuery effect to use when hiding a field
var MegaFormsChangeVisuallyJQueryShowEffect = 'slow'; //set the jQuery effect to use when showing a field
var MegaFormsCascadeJQueryHideEffect = 'fast'; //sets the jQuery effect to use when hiding a child cascaded list just before refreshing its items
var MegaFormsCascadeJQueryShowEffect = 'fast'; //set the jQuery effect to use when showing a child cascaded list just after refreshing its items
var MegaFormsDetectAllFormChanges = true; //set value whether to detect changes on all forms or not
var MegaFormsDetectChangesWarningMessage = '';  // "At least one unsaved value has changed, are you sure you want to leave the page?"; //The message to show if a form value has changed and page is being left