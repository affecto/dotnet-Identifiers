using System;
using Affecto.Identifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Identifiers.Tests
{
    [TestClass]
    public class IBANTests
    {
        private const string InvalidIban = "FI2112345600000784";
        private const string InvalidCountryCodeIban = "F12112345600000785";
        private const string IbanTooShort = "FI211234560000078";
        private const string IbanTooLong = "FI21123456000007855";

        private const string ValidIbanFinland = "FI2112345600000785";
        private const string ValidIbanWithSpaces = "F I 2 1 1 2 3 4 5 6 0 0 0 0 0 7 8 5";
        private const string ValidIbanNetherlands = "NL39RABO0300065264";
        private const string ValidIbanGreatBritain = "GB29NWBK60161331926819";

        #region invalid values

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNullIBAN()
        {
            IBAN.Create(null);
        }

        [TestMethod]
        public void TryNullIBAN()
        {
            AssertFailedTryCreate(null);
        }

        [TestMethod]
        public void NullIBANSpecification()
        {
            AssertDissatisfiedSpecification(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateEmptyIBAN()
        {
            IBAN.Create(string.Empty);
        }

        [TestMethod]
        public void TryEmptyIBAN()
        {
            AssertFailedTryCreate(string.Empty);
        }

        [TestMethod]
        public void EmptyIBANSpecification()
        {
            AssertDissatisfiedSpecification(string.Empty);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvalidIBAN()
        {
            IBAN.Create(InvalidIban);
        }

        [TestMethod]
        public void TryInvalidIBAN()
        {
            AssertFailedTryCreate(InvalidIban);
        }

        [TestMethod]
        public void InvalidIBANSpecification()
        {
            AssertDissatisfiedSpecification(InvalidIban);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvalidCountryCodeIBAN()
        {
            IBAN.Create(InvalidCountryCodeIban);
        }

        [TestMethod]
        public void TryInvalidCountryCodeIBAN()
        {
            AssertFailedTryCreate(InvalidCountryCodeIban);
        }

        [TestMethod]
        public void InvalidCountryCodeIBANSpecification()
        {
            AssertDissatisfiedSpecification(InvalidCountryCodeIban);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTooShortIBAN()
        {
            IBAN.Create(IbanTooShort);
        }

        [TestMethod]
        public void TryTooShortIBAN()
        {
            AssertFailedTryCreate(IbanTooShort);
        }

        [TestMethod]
        public void TooShortIBANSpecification()
        {
            AssertDissatisfiedSpecification(IbanTooShort);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTooLongIBAN()
        {
            IBAN.Create(IbanTooLong);
        }

        [TestMethod]
        public void TryTooLongIBAN()
        {
            AssertFailedTryCreate(IbanTooLong);
        }

        [TestMethod]
        public void TooLongIBANSpecification()
        {
            AssertDissatisfiedSpecification(IbanTooLong);
        }


        #endregion


        #region valid values

        [TestMethod]
        public void CreateValidIBAN()
        {
            string value = ValidIbanFinland;
            string expectedResult = ValidIbanFinland;
            IBAN result = IBAN.Create(value);
            Assert.AreEqual(expectedResult, result.ToString());
        }

        [TestMethod]
        public void TryValidIBAN()
        {
            string value = ValidIbanFinland;
            string expectedResult = ValidIbanFinland;

            AssertSuccessfulTryCreate(value, expectedResult);
        }

        [TestMethod]
        public void ValidIBANSpecification()
        {
            AssertSatisfiedSpecification(ValidIbanFinland);
        }


        [TestMethod]
        public void CreateValidIBANNetherlands()
        {
            string value = ValidIbanNetherlands;
            string expectedResult = ValidIbanNetherlands;
            IBAN result = IBAN.Create(value);
            Assert.AreEqual(expectedResult, result.ToString());
        }

        [TestMethod]
        public void TryValidIBANNetherlands()
        {
            string value = ValidIbanNetherlands;
            string expectedResult = ValidIbanNetherlands;

            AssertSuccessfulTryCreate(value, expectedResult);
        }

        [TestMethod]
        public void ValidIBANNetherlandsSpecification()
        {
            AssertSatisfiedSpecification(ValidIbanNetherlands);
        }


        [TestMethod]
        public void CreateValidIBANGreatBritain()
        {
            string value = ValidIbanGreatBritain;
            string expectedResult = ValidIbanGreatBritain;
            IBAN result = IBAN.Create(value);
            Assert.AreEqual(expectedResult, result.ToString());
        }

        [TestMethod]
        public void TryValidIBANGreatBritain()
        {
            string value = ValidIbanGreatBritain;
            string expectedResult = ValidIbanGreatBritain;

            AssertSuccessfulTryCreate(value, expectedResult);
        }

        [TestMethod]
        public void ValidIBANGreatBritainSpecification()
        {
            AssertSatisfiedSpecification(ValidIbanGreatBritain);
        }

        [TestMethod]
        public void CreateIBANWithSpaces()
        {
            string value = ValidIbanWithSpaces;
            string expectedResult = ValidIbanWithSpaces.Replace(" ", string.Empty);
            IBAN result = IBAN.Create(value);
            Assert.AreEqual(expectedResult, result.ToString());
        }

        [TestMethod]
        public void TryIBANWithSpaces()
        {
            string value = ValidIbanWithSpaces;
            string expectedResult = ValidIbanWithSpaces.Replace(" ", string.Empty);

            AssertSuccessfulTryCreate(value, expectedResult);
        }

        [TestMethod]
        public void IBANWithSpacesSpecification()
        {
            AssertSatisfiedSpecification(ValidIbanWithSpaces);
        }

        [TestMethod]
        public void CreateIBANWithLeadingSpace()
        {
            string value = " " + ValidIbanFinland;
            string expectedResult = ValidIbanFinland;
            IBAN result = IBAN.Create(value);
            Assert.AreEqual(expectedResult, result.ToString());
        }

        [TestMethod]
        public void TryIBANWithLeadingSpace()
        {
            string value = " " + ValidIbanFinland;
            string expectedResult = ValidIbanFinland;

            AssertSuccessfulTryCreate(value, expectedResult);
        }

        [TestMethod]
        public void IBANWithLeadingSpaceSpecification()
        {
            string value = " " + ValidIbanFinland;
            AssertSatisfiedSpecification(value);
        }



        [TestMethod]
        public void CreateIBANWithTrailingSpace()
        {
            string value = ValidIbanFinland + " ";
            string expectedResult = ValidIbanFinland;
            IBAN result = IBAN.Create(value);
            Assert.AreEqual(expectedResult, result.ToString());
        }

        [TestMethod]
        public void TryIBANWithTrailingSpace()
        {
            string value = ValidIbanFinland + " ";
            string expectedResult = ValidIbanFinland;

            AssertSuccessfulTryCreate(value, expectedResult);
        }

        [TestMethod]
        public void IBANWithTrailingSpaceSpecification()
        {
            string value = ValidIbanFinland + " ";
            AssertSatisfiedSpecification(value);
        }


        [TestMethod]
        public void CreateLowerCaseIBANToUpperCase()
        {
            string value = ValidIbanGreatBritain.ToLower();
            string expectedResult = ValidIbanGreatBritain;
            IBAN result = IBAN.Create(value);
            Assert.AreEqual(expectedResult, result.ToString());
        }

        [TestMethod]
        public void TryCreateLowerCaseIBANToUpperCase()
        {
            string value = ValidIbanGreatBritain.ToLower();
            string expectedResult = ValidIbanGreatBritain;

            AssertSuccessfulTryCreate(value, expectedResult);
        }

        [TestMethod]
        public void LowerCaseIBANSpecification()
        {
            string value = ValidIbanGreatBritain.ToLower();
            AssertSatisfiedSpecification(value);
        }

        #endregion


        #region private helper methods


        private static void AssertSatisfiedSpecification(string value)
        {
            var specification = new IBANSpecification();
            Assert.IsTrue(specification.IsSatisfiedBy(value));
            Assert.IsTrue(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }

        
        private static void AssertDissatisfiedSpecification(string value)
        {
            var specification = new IBANSpecification();
            Assert.IsFalse(specification.IsSatisfiedBy(value));
            Assert.IsFalse(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }

        
        private static void AssertSuccessfulTryCreate(string value, string expectedResult)
        {
            string failureReason;
            IBAN result;
            Assert.IsTrue(IBAN.TryCreate(value, out result, out failureReason));
            Assert.AreEqual(expectedResult, result.ToString());
            Assert.IsTrue(string.IsNullOrWhiteSpace(failureReason));
        }

        private static void AssertFailedTryCreate(string value)
        {
            string failureReason;
            IBAN result;
            Assert.IsFalse(IBAN.TryCreate(value, out result, out failureReason));
            Assert.IsNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(failureReason));
        }

        #endregion

    }
}
