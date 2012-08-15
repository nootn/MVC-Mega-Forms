describe("GetFormValue method", function () {

    describe("Different form control types can have their values retrieved", function () {
        it("should throw an exception if not form control is supplied", function () {

            var formControl = null;

            expect(function () {
                MvcMegaForms.GetFormValue(formControl);
            }).toThrow("Undefined form control supplied");
        });

        it("should return an empty value if the text field value is not supplied", function () {

            var formControl = document.createElement("input");
            formControl.type = "text";

            expect(MvcMegaForms.GetFormValue(formControl)).toEqual("");
        });

        it("should return supplied value of a text field", function () {

            var formValue = "Supplied Value!";

            var formControl = document.createElement("input");
            formControl.type = "text";
            formControl.value = formValue;

            expect(MvcMegaForms.GetFormValue(formControl)).toEqual(formValue);
        });

        it("should return supplied value of a hidden field", function () {

            var formValue = "Supplied Value!";

            var formControl = document.createElement("input");
            formControl.type = "hidden";
            formControl.value = formValue;

            expect(MvcMegaForms.GetFormValue(formControl)).toEqual(formValue);
        });

        it("should return supplied value of a password field", function () {

            var formValue = "Supplied Value!";

            var formControl = document.createElement("input");
            formControl.type = "password";
            formControl.value = formValue;

            expect(MvcMegaForms.GetFormValue(formControl)).toEqual(formValue);
        });

        it("should return supplied value of a radio field if the radio button is checked", function () {

            var formValue = "Supplied Value!";

            var formControl = document.createElement("input");
            formControl.type = "radio";
            formControl.checked = "checked";
            formControl.name = "group";
            formControl.value = formValue;

            //radio button is a special case, needs to be in the DOM
            document.body.appendChild(formControl);

            expect(MvcMegaForms.GetFormValue(formControl)).toEqual(formValue);

            //clean up DOM
            document.body.removeChild(formControl);
        });

        it("should return undefined if the radio button is not checked", function () {

            var formValue = "Supplied Value!";

            var formControl = document.createElement("input");
            formControl.type = "radio";
            formControl.checked = null;
            formControl.value = formValue;

            //radio button is a special case, needs to be in the DOM
            document.body.appendChild(formControl);

            expect(MvcMegaForms.GetFormValue(formControl)).toEqual(undefined);

            //clean up DOM
            document.body.removeChild(formControl);
        });

        it("should return true if a checkbox field is checked", function () {

            var formControl = document.createElement("input");
            formControl.type = "checkbox";
            formControl.checked = "checked";

            expect(MvcMegaForms.GetFormValue(formControl)).toEqual(true);
        });

        it("should return false if a checkbox field is not checked", function () {

            var formControl = document.createElement("input");
            formControl.type = "checkbox";
            formControl.checked = null;

            expect(MvcMegaForms.GetFormValue(formControl)).toEqual(false);
        });

        it("should return supplied value of a submit field", function () {

            var formValue = "Supplied Value!";

            var formControl = document.createElement("input");
            formControl.type = "submit";
            formControl.value = formValue;

            expect(MvcMegaForms.GetFormValue(formControl)).toEqual(formValue);
        });

        it("should return an empty value if the select value is not selected", function () {

            var formControl = document.createElement("select");

            var option0 = document.createElement("option");
            option0.value = "";
            option0.appendChild(document.createTextNode("[Please Select]"));
            formControl.appendChild(option0);

            var option1 = document.createElement("option");
            option1.value = "1";
            option1.appendChild(document.createTextNode("One"));
            formControl.appendChild(option1);

            var option2 = document.createElement("option");
            option2.value = "2";
            option2.appendChild(document.createTextNode("Two"));
            formControl.appendChild(option2);

            expect(MvcMegaForms.GetFormValue(formControl)).toEqual("");
        });

        it("should return the selected value if a select value is selected", function () {

            var formValue = "1";

            var formControl = document.createElement("select");

            var option0 = document.createElement("option");
            option0.value = "";
            option0.appendChild(document.createTextNode("[Please Select]"));
            formControl.appendChild(option0);

            var option1 = document.createElement("option");
            option1.value = formValue;
            option1.selected = "selected";
            option1.appendChild(document.createTextNode("One"));
            formControl.appendChild(option1);

            var option2 = document.createElement("option");
            option2.value = "2";
            option2.appendChild(document.createTextNode("Two"));
            formControl.appendChild(option2);

            expect(MvcMegaForms.GetFormValue(formControl)).toEqual(formValue);
        });

        it("should return an empty value if the multiple select value is not selected", function () {

            var formControl = document.createElement("select");
            formControl.multiple = "multiple";

            var option0 = document.createElement("option");
            option0.value = "";
            option0.appendChild(document.createTextNode("[Please Select]"));
            formControl.appendChild(option0);

            var option1 = document.createElement("option");
            option1.value = "1";
            option1.appendChild(document.createTextNode("One"));
            formControl.appendChild(option1);

            var option2 = document.createElement("option");
            option2.value = "2";
            option2.appendChild(document.createTextNode("Two"));
            formControl.appendChild(option2);

            expect(MvcMegaForms.GetFormValue(formControl)).toEqual(null);
        });

        it("should return the selected values if the multipe select value has multiple items selected", function () {

            var formValue = ["1", "2"];

            var formControl = document.createElement("select");
            formControl.multiple = "multiple";

            var option0 = document.createElement("option");
            option0.value = "";
            option0.appendChild(document.createTextNode("[Please Select]"));
            formControl.appendChild(option0);

            var option1 = document.createElement("option");
            option1.value = formValue[0];
            option1.selected = "selected";
            option1.appendChild(document.createTextNode("One"));
            formControl.appendChild(option1);

            var option2 = document.createElement("option");
            option2.value = formValue[1];
            option2.selected = "selected";
            option2.appendChild(document.createTextNode("Two"));
            formControl.appendChild(option2);

            expect(MvcMegaForms.GetFormValue(formControl)).toEqual(formValue);
        });
    });
});