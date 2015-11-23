using System;
using Affecto.Identifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Identifiers.Tests
{
    [TestClass]
    public class EmailAddressTests
    {
        private EmailAddress sut;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullEmailAddress()
        {
            EmailAddress.Create(null);
        }

        [TestMethod]
        public void TryNullEmailAddress()
        {
            AssertInvalidValue(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyEmailAddress()
        {
            EmailAddress.Create(string.Empty);
        }

        [TestMethod]
        public void TryEmptyEmailAddress()
        {
            AssertInvalidValue(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmailAddressWithoutAtCharacter()
        {
            EmailAddress.Create("jaska.jokunen.firma.fi");
        }

        [TestMethod]
        public void TryEmailAddressWithoutAtCharacter()
        {
            AssertInvalidValue("jaska.jokunen.firma.fi");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmailAddressWithoutDomain()
        {
            EmailAddress.Create("jaskajokunen@firma");
        }

        [TestMethod]
        public void TryEmailAddressWithoutDomain()
        {
            AssertInvalidValue("jaska.jokunen@firma");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmailAddressWithoutUser()
        {
            EmailAddress.Create("@firma.fi");
        }

        [TestMethod]
        public void TryEmailAddressWithoutUser()
        {
            AssertInvalidValue("@firma.fi");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmailAddressWithoutMachine()
        {
            EmailAddress.Create("jaska.jokunen@");
        }

        [TestMethod]
        public void TryEmailAddressWithoutMachine()
        {
            AssertInvalidValue("jaska@");
        }

        [TestMethod]
        public void EmailAddressWithOneNamePart()
        {
            const string address = "jaskajokunen@firma.fi";
            sut = EmailAddress.Create(address);

            Assert.AreEqual(address, sut.ToString());
        }

        [TestMethod]
        public void TryEmailAddressWithOneNamePart()
        {
            AssertValidValue("jaska82@firma.fi");
        }

        [TestMethod]
        public void EmailAddressWithManyNameParts()
        {
            const string address = "jaska.jokunen_81@firma.fi";
            sut = EmailAddress.Create(address);

            Assert.AreEqual(address, sut.ToString());
        }

        [TestMethod]
        public void TryEmailAddressWithManyNameParts()
        {
            AssertValidValue("jaska.i.jokunen@firma.fi");
        }

        [TestMethod]
        public void EmailAddressWithThreeCharacterDomain()
        {
            const string address = "jaska.jokunen_81@firm.com";
            sut = EmailAddress.Create(address);

            Assert.AreEqual(address, sut.ToString());
        }

        [TestMethod]
        public void TryEmailAddressWithFourCharacterDomain()
        {
            AssertValidValue("jaska.i.jokunen@firma.info");
        }

        [TestMethod]
        public void EqaulsComparesEmailAddressContent()
        {
            const string address = "me@machine.com";
            sut = EmailAddress.Create(address);
            EmailAddress other = EmailAddress.Create(address);

            Assert.AreEqual(other, sut);
        }

        [TestMethod]
        public void MunicipalityIdIsNotEqualWithDifferentTypesOfObjects()
        {
            sut = EmailAddress.Create("me@computer.org");

            Assert.AreNotEqual(DateTime.Today, sut);
        }
        
        private static void AssertValidValue(string value)
        {
            AssertSuccessfulTryCreate(value);
            AssertSatisfiedSpecification(value);
        }

        private static void AssertSatisfiedSpecification(string value)
        {
            var specification = new EmailAddressSpecification();
            Assert.IsTrue(specification.IsSatisfiedBy(value));
            Assert.IsTrue(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }

        private static void AssertSuccessfulTryCreate(string value)
        {
            string failureReason;
            EmailAddress result;
            Assert.IsTrue(EmailAddress.TryCreate(value, out result, out failureReason));
            Assert.AreEqual(value, result.ToString());
            Assert.IsTrue(string.IsNullOrWhiteSpace(failureReason));
        }

        private static void AssertInvalidValue(string value)
        {
            AssertFailedTryCreate(value);
            AssertDissatisfiedSpecification(value);
        }

        private static void AssertDissatisfiedSpecification(string value)
        {
            var specification = new EmailAddressSpecification();
            Assert.IsFalse(specification.IsSatisfiedBy(value));
            Assert.IsFalse(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }

        private static void AssertFailedTryCreate(string value)
        {
            string failureReason;
            EmailAddress result;
            Assert.IsFalse(EmailAddress.TryCreate(value, out result, out failureReason));
            Assert.IsNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(failureReason));
        }
    }
}