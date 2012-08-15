describe("IsNullOrUndefined method", function () {

    describe("Null and Undefined values return true, all other values return false", function () {
        it("should return true if null", function () {

            var item = null;

            expect(MvcMegaForms.IsNullOrUndefined(item)).toEqual(true);
        });

        it("should return true if undefined", function () {

            var item = undefined;

            expect(MvcMegaForms.IsNullOrUndefined(item)).toEqual(true);
        });

        it("should return false if empty string", function () {

            var item = "";

            expect(MvcMegaForms.IsNullOrUndefined(item)).toEqual(false);
        });

        it("should return false if number", function () {

            var item = 100.0;

            expect(MvcMegaForms.IsNullOrUndefined(item)).toEqual(false);
        });

        it("should return false if boolean", function () {

            var item = false;

            expect(MvcMegaForms.IsNullOrUndefined(item)).toEqual(false);
        });
    });
});