describe("ConditionMetForChangeVisually method", function () {

    describe("The ChangeVisually conditions can be evaluated when at least one of the values is not specified", function () {
        it("should throw an exception if the 'ifOperator' is not specified", function () {

            var ifOperator = null;
            var expectedValue = null;
            var actualValue = null;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(function () {
                MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat);
            }).toThrow("MvcMegaForms-ChangeVisually Critical Error in ConditionMetForChangeVisually: ifOperator was not supplied");
        });

        it("should throw an exception if the 'expectedValue' is an array - only single value checking is supported", function () {

            var ifOperator = "equals";
            var expectedValue = [1, 2];
            var actualValue = [1, 2];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(function () {
                MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat);
            }).toThrow("MvcMegaForms-ChangeVisually Critical Error in ConditionMetForChangeVisually: Array data type for expectedValue is not supported at this time.  expectedValue supplied was: " + expectedValue);
        });

        it("should return true when 'actualValue' is not specified, 'expectedValue' is specified and 'conditionPassesIfNull' is true", function () {

            var ifOperator = "equals";
            var expectedValue = "test";
            var actualValue = null;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'actualValue' is not specified, 'expectedValue' is specified and 'conditionPassesIfNull' is false", function () {

            var ifOperator = "equals";
            var expectedValue = "test";
            var actualValue = null;
            var conditionPassesIfNull = false;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return false when 'actualValue' is specified, 'expectedValue' is not specified and 'ifOperator' is equals (we wanted a null and it was not)", function () {

            var ifOperator = "equals";
            var expectedValue = null;
            var actualValue = "test";
            var conditionPassesIfNull = false;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should throw an exception when 'actualValue' is specified, 'expectedValue' is not specified and 'ifOperator' is some invalid value", function () {

            var ifOperator = "invalidValue";
            var expectedValue = null;
            var actualValue = "test";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(function () {
                MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat);
            }).toThrow("MvcMegaForms-ChangeVisually Critical Error in ConditionMetForChangeVisually: When checking for a null expectedValue, DisplayChangeIf must be Equals or NotEquals, supplied ifOperator was " + ifOperator);
        });

        it("should return true when 'actualValue' is specified, 'expectedValue' is not specified and 'ifOperator' is notequals (we did not want a null and it was not)", function () {

            var ifOperator = "notequals";
            var expectedValue = null;
            var actualValue = "test";
            var conditionPassesIfNull = false;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return true when 'actualValue' is not specified, 'expectedValue' is not specified and 'conditionPassesIfNull' is true", function () {

            var ifOperator = "equals";
            var expectedValue = null;
            var actualValue = null;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'actualValue' is not specified, 'expectedValue' is not specified and 'conditionPassesIfNull' is false", function () {

            var ifOperator = "equals";
            var expectedValue = null;
            var actualValue = null;
            var conditionPassesIfNull = false;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

    });

    describe("The ChangeVisually conditions can be evaluated when both of the values are specified and 'valueTypeToCompare' is string", function () {

        it("should throw an exception if the 'ifOperator' is anything other than contains/notcontains and 'actualValue' is an array (E.g. the value of a multi-select)", function () {

            var ifOperator = "equals";
            var expectedValue = "1";
            var actualValue = ["1", "2"];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(function () {
                MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat);
            }).toThrow("MvcMegaForms-ChangeVisually Critical Error in ConditionMetForChangeVisually: When actualValue is an array (E.g. value of a multi-select), DisplayChangeIf must be Contains or NotContains, supplied ifOperator was " + ifOperator);
        });

        it("should return true when 'ifOperator' is equals and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "equals";
            var expectedValue = "TEST";
            var actualValue = "test";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is equals and 'actualValue' is not equal to 'expectedValue'", function () {

            var ifOperator = "equals";
            var expectedValue = "TEST";
            var actualValue = "test-different";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return false when 'ifOperator' is notequals and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "notequals";
            var expectedValue = "TEST";
            var actualValue = "test";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is notequals and 'actualValue' is not equal to 'expectedValue'", function () {

            var ifOperator = "notequals";
            var expectedValue = "TEST";
            var actualValue = "test-different";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return true when 'ifOperator' is greaterthan and 'actualValue' is greater than 'expectedValue'", function () {

            var ifOperator = "greaterthan";
            var expectedValue = "abc";
            var actualValue = "xyz";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is greaterthan and 'actualValue' is less than 'expectedValue'", function () {

            var ifOperator = "greaterthan";
            var expectedValue = "xyz";
            var actualValue = "abc";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return false when 'ifOperator' is greaterthan and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "greaterthan";
            var expectedValue = "abc";
            var actualValue = "abc";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is greaterthanorequals and 'actualValue' is greater than 'expectedValue'", function () {

            var ifOperator = "greaterthanorequals";
            var expectedValue = "abc";
            var actualValue = "xyz";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is greaterthanorequals and 'actualValue' is less than 'expectedValue'", function () {

            var ifOperator = "greaterthanorequals";
            var expectedValue = "xyz";
            var actualValue = "abc";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is greaterthanorequals and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "greaterthanorequals";
            var expectedValue = "abc";
            var actualValue = "abc";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return true when 'ifOperator' is lessthan and 'actualValue' is less than 'expectedValue'", function () {

            var ifOperator = "lessthan";
            var expectedValue = "xyz";
            var actualValue = "abc";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is lessthan and 'actualValue' is greater than 'expectedValue'", function () {

            var ifOperator = "lessthan";
            var expectedValue = "abc";
            var actualValue = "xyz";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return false when 'ifOperator' is lessthan and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "lessthan";
            var expectedValue = "abc";
            var actualValue = "abc";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is lessthanorequals and 'actualValue' is less than 'expectedValue'", function () {

            var ifOperator = "lessthanorequals";
            var expectedValue = "xyz";
            var actualValue = "abc";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is lessthanorequals and 'actualValue' is greater than 'expectedValue'", function () {

            var ifOperator = "lessthanorequals";
            var expectedValue = "abc";
            var actualValue = "xyz";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is lessthanorequals and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "lessthanorequals";
            var expectedValue = "abc";
            var actualValue = "abc";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return true when 'ifOperator' is contains and 'actualValue' is an array and contains 'expectedValue'", function () {

            var ifOperator = "contains";
            var expectedValue = "1";
            var actualValue = ["0", "1", "2", "3"];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is contains and 'actualValue' is an array and does not contain 'expectedValue'", function () {

            var ifOperator = "contains";
            var expectedValue = "4";
            var actualValue = ["0", "1", "2", "3"];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is notcontains and 'actualValue' is an array and does not contain 'expectedValue'", function () {

            var ifOperator = "notcontains";
            var expectedValue = "4";
            var actualValue = ["0", "1", "2", "3"];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is notcontains and 'actualValue' is an array and contains 'expectedValue'", function () {

            var ifOperator = "notcontains";
            var expectedValue = "1";
            var actualValue = ["0", "1", "2", "3"];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is contains and 'actualValue' is not an array and contains 'expectedValue'", function () {

            var ifOperator = "contains";
            var expectedValue = "1";
            var actualValue = "1";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is contains and 'actualValue' is not an array and does not contain 'expectedValue'", function () {

            var ifOperator = "contains";
            var expectedValue = "4";
            var actualValue = "1";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is notcontains and 'actualValue' is not an array and does not contain 'expectedValue'", function () {

            var ifOperator = "notcontains";
            var expectedValue = "4";
            var actualValue = "1";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is notcontains and 'actualValue' is not an array and contains 'expectedValue'", function () {

            var ifOperator = "notcontains";
            var expectedValue = "1";
            var actualValue = "1";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "string";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });
    });

    describe("The ChangeVisually conditions can be evaluated when both of the values are specified and 'valueTypeToCompare' is number", function () {

        it("should throw an exception if the 'ifOperator' is anything other than contains/notcontains and 'actualValue' is an array (E.g. the value of a multi-select)", function () {

            var ifOperator = "equals";
            var expectedValue = 1.1;
            var actualValue = [1.1, 2.2];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(function () {
                MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat);
            }).toThrow("MvcMegaForms-ChangeVisually Critical Error in ConditionMetForChangeVisually: When actualValue is an array (E.g. value of a multi-select), DisplayChangeIf must be Contains or NotContains, supplied ifOperator was " + ifOperator);
        });

        it("should throw an exception if 'expectedValue' is not a number", function () {

            var ifOperator = "equals";
            var expectedValue = "abc";
            var actualValue = 1.1;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(function () {
                MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat);
            }).toThrow("MvcMegaForms-ChangeVisually Critical Error in ConditionMetForChangeVisually: valueTypeToCompare was 'number', but expectedValue was not a number: " + expectedValue);
        });

        it("should throw an exception if 'actualValue' is not a number", function () {

            var ifOperator = "equals";
            var expectedValue = 1.1;
            var actualValue = "abc";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(function () {
                MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat);
            }).toThrow("MvcMegaForms-ChangeVisually Critical Error in ConditionMetForChangeVisually: valueTypeToCompare was 'number', but actualValue was not a number: " + actualValue);
        });

        it("should throw an exception if 'actualValue' is an array and contains an item that is not a number", function () {

            var ifOperator = "contains";
            var expectedValue = 1.1;
            var actualValue = [1.1, 2.2, "abc"];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(function () {
                MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat);
            }).toThrow("MvcMegaForms-ChangeVisually Critical Error in ConditionMetForChangeVisually: valueTypeToCompare was 'number', but an item in the actualValue array was not a number: " + actualValue[2]);
        });

        it("should return true when 'ifOperator' is equals and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "equals";
            var expectedValue = 100.01;
            var actualValue = 100.01;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is equals and 'actualValue' is not equal to 'expectedValue'", function () {

            var ifOperator = "equals";
            var expectedValue = 100.01;
            var actualValue = 100.02;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return false when 'ifOperator' is notequals and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "notequals";
            var expectedValue = 100.01;
            var actualValue = 100.01;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is notequals and 'actualValue' is not equal to 'expectedValue'", function () {

            var ifOperator = "notequals";
            var expectedValue = 100.01;
            var actualValue = 100.02;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return true when 'ifOperator' is greaterthan and 'actualValue' is greater than 'expectedValue'", function () {

            var ifOperator = "greaterthan";
            var expectedValue = 100.01;
            var actualValue = 100.02;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is greaterthan and 'actualValue' is less than 'expectedValue'", function () {

            var ifOperator = "greaterthan";
            var expectedValue = 100.02;
            var actualValue = 100.01;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return false when 'ifOperator' is greaterthan and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "greaterthan";
            var expectedValue = 100.01;
            var actualValue = 100.01;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is greaterthanorequals and 'actualValue' is greater than 'expectedValue'", function () {

            var ifOperator = "greaterthanorequals";
            var expectedValue = 100.01;
            var actualValue = 100.02;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is greaterthanorequals and 'actualValue' is less than 'expectedValue'", function () {

            var ifOperator = "greaterthanorequals";
            var expectedValue = 100.02;
            var actualValue = 100.01;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is greaterthanorequals and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "greaterthanorequals";
            var expectedValue = 100.01;
            var actualValue = 100.01;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return true when 'ifOperator' is lessthan and 'actualValue' is less than 'expectedValue'", function () {

            var ifOperator = "lessthan";
            var expectedValue = 100.02;
            var actualValue = 100.01;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is lessthan and 'actualValue' is greater than 'expectedValue'", function () {

            var ifOperator = "lessthan";
            var expectedValue = 100.01;
            var actualValue = 100.02;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return false when 'ifOperator' is lessthan and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "lessthan";
            var expectedValue = 100.01;
            var actualValue = 100.01;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is lessthanorequals and 'actualValue' is less than 'expectedValue'", function () {

            var ifOperator = "lessthanorequals";
            var expectedValue = 100.02;
            var actualValue = 100.01;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is lessthanorequals and 'actualValue' is greater than 'expectedValue'", function () {

            var ifOperator = "lessthanorequals";
            var expectedValue = 100.01;
            var actualValue = 100.02;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is lessthanorequals and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "lessthanorequals";
            var expectedValue = 100.01;
            var actualValue = 100.01;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return true when 'ifOperator' is contains and 'actualValue' is an array and contains 'expectedValue'", function () {

            var ifOperator = "contains";
            var expectedValue = 1.1;
            var actualValue = [0, 1.1, 2.2, 3.3];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is contains and 'actualValue' is an array and does not contain 'expectedValue'", function () {

            var ifOperator = "contains";
            var expectedValue = 4.4;
            var actualValue = [0, 1.1, 2.2, 3.3];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is notcontains and 'actualValue' is an array and does not contain 'expectedValue'", function () {

            var ifOperator = "notcontains";
            var expectedValue = 4.4;
            var actualValue = [0, 1.1, 2.2, 3.3];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is notcontains and 'actualValue' is an array and contains 'expectedValue'", function () {

            var ifOperator = "notcontains";
            var expectedValue = 1.1;
            var actualValue = [0, 1.1, 2.2, 3.3];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is contains and 'actualValue' is not an array and contains 'expectedValue'", function () {

            var ifOperator = "contains";
            var expectedValue = 1.1;
            var actualValue = 1.1;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is contains and 'actualValue' is not an array and does not contain 'expectedValue'", function () {

            var ifOperator = "contains";
            var expectedValue = 4.4;
            var actualValue = 1.1;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is notcontains and 'actualValue' is not an array and does not contain 'expectedValue'", function () {

            var ifOperator = "notcontains";
            var expectedValue = 4.4;
            var actualValue = 1.1;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is notcontains and 'actualValue' is not an array and contains 'expectedValue'", function () {

            var ifOperator = "notcontains";
            var expectedValue = 1.1;
            var actualValue = 1.1;
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "number";
            var valueFormat = null;

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });
    });

    describe("The ChangeVisually conditions can be evaluated when both of the values are specified and 'valueTypeToCompare' is datetime", function () {

        it("should throw an exception if the 'ifOperator' is anything other than contains/notcontains and 'actualValue' is an array (E.g. the value of a multi-select)", function () {

            var ifOperator = "equals";
            var expectedValue = "2012-12-01";
            var actualValue = ["2012-12-01", "2012-12-02"];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(function () {
                MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat);
            }).toThrow("MvcMegaForms-ChangeVisually Critical Error in ConditionMetForChangeVisually: When actualValue is an array (E.g. value of a multi-select), DisplayChangeIf must be Contains or NotContains, supplied ifOperator was " + ifOperator);
        });

        it("should throw an exception if 'expectedValue' is not a date in the specified format", function () {

            var ifOperator = "equals";
            var expectedValue = "01-12-2012";
            var actualValue = "2012-12-01";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(function () {
                MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat);
            }).toThrow("MvcMegaForms-ChangeVisually Critical Error in ConditionMetForChangeVisually: valueTypeToCompare was 'datetime', but expectedValue does not fit the specified date format: " + expectedValue + " was not a valid DateTime in the specified format: " + valueFormat);
        });

        it("should throw an exception if 'actualValue' is not a date in the specified format", function () {

            var ifOperator = "equals";
            var expectedValue = "2012-12-01";
            var actualValue = "01-12-2012";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(function () {
                MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat);
            }).toThrow("MvcMegaForms-ChangeVisually Critical Error in ConditionMetForChangeVisually: valueTypeToCompare was 'datetime', but actualValue: " + actualValue + " was not a valid DateTime in the specified format: " + valueFormat);
        });

        it("should throw an exception if 'actualValue' is an array and contains a date in the specified format", function () {

            var ifOperator = "contains";
            var expectedValue = "2012-12-01";
            var actualValue = ["2012-12-01", "02-12-2012"];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(function () {
                MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat);
            }).toThrow("MvcMegaForms-ChangeVisually Critical Error in ConditionMetForChangeVisually: valueTypeToCompare was 'datetime', but an item in the actualValue array '" + actualValue[1] + "' was not a valid DateTime in the specified format: " + valueFormat);
        });

        it("should return true when 'ifOperator' is equals and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "equals";
            var expectedValue = "2012-12-12";
            var actualValue = "2012-12-12";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is equals and 'actualValue' is not equal to 'expectedValue'", function () {

            var ifOperator = "equals";
            var expectedValue = "2012-12-12";
            var actualValue = "2012-12-13";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return false when 'ifOperator' is notequals and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "notequals";
            var expectedValue = "2012-12-12";
            var actualValue = "2012-12-12";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is notequals and 'actualValue' is not equal to 'expectedValue'", function () {

            var ifOperator = "notequals";
            var expectedValue = "2012-12-12";
            var actualValue = "2012-12-13";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return true when 'ifOperator' is greaterthan and 'actualValue' is greater than 'expectedValue'", function () {

            var ifOperator = "greaterthan";
            var expectedValue = "2012-12-12";
            var actualValue = "2012-12-13";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is greaterthan and 'actualValue' is less than 'expectedValue'", function () {

            var ifOperator = "greaterthan";
            var expectedValue = "2012-12-13";
            var actualValue = "2012-12-12";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return false when 'ifOperator' is greaterthan and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "greaterthan";
            var expectedValue = "2012-12-12";
            var actualValue = "2012-12-12";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is greaterthanorequals and 'actualValue' is greater than 'expectedValue'", function () {

            var ifOperator = "greaterthanorequals";
            var expectedValue = "2012-12-12";
            var actualValue = "2012-12-13";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is greaterthanorequals and 'actualValue' is less than 'expectedValue'", function () {

            var ifOperator = "greaterthanorequals";
            var expectedValue = "2012-12-13";
            var actualValue = "2012-12-12";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is greaterthanorequals and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "greaterthanorequals";
            var expectedValue = "2012-12-12";
            var actualValue = "2012-12-12";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return true when 'ifOperator' is lessthan and 'actualValue' is less than 'expectedValue'", function () {

            var ifOperator = "lessthan";
            var expectedValue = "2012-12-13";
            var actualValue = "2012-12-12";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is lessthan and 'actualValue' is greater than 'expectedValue'", function () {

            var ifOperator = "lessthan";
            var expectedValue = "2012-12-12";
            var actualValue = "2012-12-13";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return false when 'ifOperator' is lessthan and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "lessthan";
            var expectedValue = "2012-12-12";
            var actualValue = "2012-12-12";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is lessthanorequals and 'actualValue' is less than 'expectedValue'", function () {

            var ifOperator = "lessthanorequals";
            var expectedValue = "2012-12-13";
            var actualValue = "2012-12-12";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is lessthanorequals and 'actualValue' is greater than 'expectedValue'", function () {

            var ifOperator = "lessthanorequals";
            var expectedValue = "2012-12-12";
            var actualValue = "2012-12-13";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is lessthanorequals and 'actualValue' is equal to 'expectedValue'", function () {

            var ifOperator = "lessthanorequals";
            var expectedValue = "2012-12-12";
            var actualValue = "2012-12-12";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return true when 'ifOperator' is contains and 'actualValue' is an array and contains 'expectedValue'", function () {

            var ifOperator = "contains";
            var expectedValue = "2012-12-01";
            var actualValue = ["2012-12-01", "2012-12-02", "2012-12-03"];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is contains and 'actualValue' is an array and does not contain 'expectedValue'", function () {

            var ifOperator = "contains";
            var expectedValue = "2012-12-04";
            var actualValue = ["2012-12-01", "2012-12-02", "2012-12-03"];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is notcontains and 'actualValue' is an array and does not contain 'expectedValue'", function () {

            var ifOperator = "notcontains";
            var expectedValue = "2012-12-04";
            var actualValue = ["2012-12-01", "2012-12-02", "2012-12-03"];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is notcontains and 'actualValue' is an array and contains 'expectedValue'", function () {

            var ifOperator = "notcontains";
            var expectedValue = "2012-12-01";
            var actualValue = ["2012-12-01", "2012-12-02", "2012-12-03"];
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is contains and 'actualValue' is not an array and contains 'expectedValue'", function () {

            var ifOperator = "contains";
            var expectedValue = "2012-12-01";
            var actualValue = "2012-12-01";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is contains and 'actualValue' is not an array and does not contain 'expectedValue'", function () {

            var ifOperator = "contains";
            var expectedValue = "2012-12-04";
            var actualValue = "2012-12-01";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });

        it("should return true when 'ifOperator' is notcontains and 'actualValue' is not an array and does not contain 'expectedValue'", function () {

            var ifOperator = "notcontains";
            var expectedValue = "2012-12-04";
            var actualValue = "2012-12-01";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(true);
        });

        it("should return false when 'ifOperator' is notcontains and 'actualValue' is not an array and contains 'expectedValue'", function () {

            var ifOperator = "notcontains";
            var expectedValue = "2012-12-01";
            var actualValue = "2012-12-01";
            var conditionPassesIfNull = true;
            var valueTypeToCompare = "datetime";
            var valueFormat = "yyyy-MM-dd";

            expect(MvcMegaForms.ConditionMetForChangeVisually(ifOperator, expectedValue, actualValue, conditionPassesIfNull, valueTypeToCompare, valueFormat)).toEqual(false);
        });
    });
});