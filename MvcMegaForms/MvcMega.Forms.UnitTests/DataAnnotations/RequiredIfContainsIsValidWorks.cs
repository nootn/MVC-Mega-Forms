// /*
// Copyright (c) 2012 Andrew Newton (http://about.me/nootn)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MvcMega.Forms.DataAnnotations;
using MvcMega.Forms.UnitTests.Models;
using NUnit.Framework;
using TestStack.BDDfy;
using TestStack.BDDfy.Core;
using TestStack.BDDfy.Scanners.StepScanners.Fluent;

namespace MvcMega.Forms.UnitTests.DataAnnotations
{
    [TestFixture]
    [Story(AsA = "As a RequiredIfContains Attribute",
        IWant = "I want to ensure my IsValid method works in all scenarios",
        SoThat = "So that people can trust me to do my job")]
    public class RequiredIfContainsIsValidWorks
    {
        private RequiredIfContains _validationAttribute;
        private ValidationContext _testContext;

        private Exception _exceptionThrown;


        private void GivenAttributeExpectsNullValueAndDependentValueIsNull()
        {
            _validationAttribute = new RequiredIfContains(ModelForRequiredIfContains.DependentPropertyName, null);
            _testContext = new ValidationContext(new ModelForRequiredIfContains {DependentProperty = null}, null, null);
        }

        private void GivenAttributeExpectsNullValueAndDependentValueIsEmptyList()
        {
            _validationAttribute = new RequiredIfContains(ModelForRequiredIfContains.DependentPropertyName, null);
            _testContext = new ValidationContext(
                new ModelForRequiredIfContains {DependentProperty = new List<string>()}, null, null);
        }

        private void GivenAttributeExpectsEmptyValueAndDependentValueIsEmptyList()
        {
            _validationAttribute = new RequiredIfContains(ModelForRequiredIfContains.DependentPropertyName, string.Empty);
            _testContext = new ValidationContext(
                new ModelForRequiredIfContains {DependentProperty = new List<string>()}, null, null);
        }

        private void GivenDependentPropertyIsNotEnumerable()
        {
            _validationAttribute = new RequiredIfContains(ModelForRequiredIfContains.InvalidDependentPropertyName,
                                                          string.Empty);
            _testContext = new ValidationContext(
                new ModelForRequiredIfContains {InvalidDependentProperty = 0}, null, null);
        }

        private void GivenAttributeExpectsEmptyValueAndDependentValueIsListWithEmptyValue()
        {
            _validationAttribute = new RequiredIfContains(ModelForRequiredIfContains.DependentPropertyName, string.Empty);
            _testContext = new ValidationContext(
                new ModelForRequiredIfContains {DependentProperty = new List<string> {"one", ""}}, null, null);
        }

        private void GivenAttributeExpectsSomeValueAndDependentValueIsListWithSomeValue()
        {
            _validationAttribute = new RequiredIfContains(ModelForRequiredIfContains.DependentPropertyName, "Some Value");
            _testContext = new ValidationContext(
                new ModelForRequiredIfContains {DependentProperty = new List<string> {"one", "Some Value", "two"}}, null,
                null);
        }

        private void WhenIsValidIsCalledWithNullValueAndDependentPropertyIsNull()
        {
            try
            {
                _validationAttribute.Validate(null, _testContext);
                _exceptionThrown = null;
            }
            catch (Exception ex)
            {
                _exceptionThrown = ex;
            }
        }

        private void ThenAnExceptionIsThrownContainingText(string text)
        {
            Assert.IsTrue(_exceptionThrown != null);
            Assert.IsTrue(_exceptionThrown.Message.Contains(text));
        }

        private void ThenNullIsReturned()
        {
            Assert.IsTrue(_exceptionThrown == null);
        }

        private void ThenValidationResultIsReturned()
        {
            Assert.IsTrue(_exceptionThrown != null);
            Assert.IsTrue(_exceptionThrown is ValidationException);
            Assert.IsTrue(_exceptionThrown.Message.Contains("This field must be supplied because"));
        }


        [Test]
        public void DependantPropertyNotEnumerableThrowsException()
        {
            this.Given(_ => _.GivenDependentPropertyIsNotEnumerable())
                .When(_ => _.WhenIsValidIsCalledWithNullValueAndDependentPropertyIsNull())
                .Then(
                    _ =>
                    _.ThenAnExceptionIsThrownContainingText(
                        "The type of value being compared must be IEnumerable when using RequiredIfContains attribute"))
                .BDDfy();
        }

        [Test]
        public void EmptyValueExpectedWithListThatContainsEmptyValueSuppliedReturnsResult()
        {
            this.Given(_ => _.GivenAttributeExpectsEmptyValueAndDependentValueIsListWithEmptyValue())
                .When(_ => _.WhenIsValidIsCalledWithNullValueAndDependentPropertyIsNull())
                .Then(_ => _.ThenValidationResultIsReturned())
                .BDDfy();
        }

        [Test]
        public void EmptyValueExpectedWithListThatContainsSomeValueSuppliedReturnsResult()
        {
            this.Given(_ => _.GivenAttributeExpectsSomeValueAndDependentValueIsListWithSomeValue())
                .When(_ => _.WhenIsValidIsCalledWithNullValueAndDependentPropertyIsNull())
                .Then(_ => _.ThenValidationResultIsReturned())
                .BDDfy();
        }

        [Test]
        public void NullValueExpectedWithEmptyListSuppliedReturnsNull()
        {
            this.Given(_ => _.GivenAttributeExpectsEmptyValueAndDependentValueIsEmptyList())
                .When(_ => _.WhenIsValidIsCalledWithNullValueAndDependentPropertyIsNull())
                .Then(_ => _.ThenNullIsReturned())
                .BDDfy();
        }

        [Test]
        public void NullValueExpectedWithEmptyListSuppliedThrowsException()
        {
            this.Given(_ => _.GivenAttributeExpectsNullValueAndDependentValueIsEmptyList())
                .When(_ => _.WhenIsValidIsCalledWithNullValueAndDependentPropertyIsNull())
                .Then(_ => _.ThenAnExceptionIsThrownContainingText("DependentValue must not be null"))
                .BDDfy();
        }

        [Test]
        public void NullValueExpectedWithNullValueSuppliedThrowsException()
        {
            this.Given(_ => _.GivenAttributeExpectsNullValueAndDependentValueIsNull())
                .When(_ => _.WhenIsValidIsCalledWithNullValueAndDependentPropertyIsNull())
                .Then(_ => _.ThenAnExceptionIsThrownContainingText("DependentValue must not be null"))
                .BDDfy();
        }
    }
}