using System;
using Affecto.Identifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Identifiers.Tests
{
    [TestClass]
    public class WebAddressTests
    {
        private WebAddress sut;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullWebAddress()
        {
            WebAddress.Create(null);
        }

        [TestMethod]
        public void TryNullWebAddress()
        {
            AssertInvalidInput(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyWebAddress()
        {
            WebAddress.Create(string.Empty);
        }

        [TestMethod]
        public void TryEmptyWebAddress()
        {
            AssertInvalidInput(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OtherThanHttpProtocol()
        {
            WebAddress.Create(@"ftp://www.domainname.com");
        }

        [TestMethod]
        public void TryOtherThanHttpProtocol()
        {
            AssertInvalidInput(@"ldap://domainname.com");
        }

        [TestMethod]
        public void HttpProtocolAddress()
        {
            const string address = @"http://www.google.com";

            sut = WebAddress.Create(address);

            Assert.AreEqual(address, sut.ToString());
        }

        [TestMethod]
        public void TryHttpProtocolAddress()
        {
            AssertValidInput(@"http://www.google.fi");
        }

        [TestMethod]
        public void HttpsProtocolAddress()
        {
            const string address = @"https://www.affecto.com/";

            sut = WebAddress.Create(address);

            Assert.AreEqual(address, sut.ToString());
        }

        [TestMethod]
        public void TryHttpsProtocolAddress()
        {
            AssertValidInput(@"https://www.affecto.fi");
        }

        [TestMethod]
        public void AddressWithoutProtocol()
        {
            const string address = @"www.google.com";

            sut = WebAddress.Create(address);

            Assert.AreEqual(address, sut.ToString());
        }

        [TestMethod]
        public void TryAddressWithoutProtocol()
        {
            AssertValidInput(@"www.google.fi/");
        }

        [TestMethod]
        public void AddressWithoutWwwAndProtocol()
        {
            const string address = @"google.com";

            sut = WebAddress.Create(address);

            Assert.AreEqual(address, sut.ToString());
        }

        [TestMethod]
        public void TryAddressWithoutWwwAndProtocol()
        {
            AssertValidInput(@"google.fi/");
        }

        [TestMethod]
        public void AddressWithoutWww()
        {
            const string address = @"http://google.com";

            sut = WebAddress.Create(address);

            Assert.AreEqual(address, sut.ToString());
        }

        [TestMethod]
        public void TryAddressWithoutWww()
        {
            AssertValidInput(@"https://google.fi/");
        }

        [TestMethod]
        public void AddressWithPort()
        {
            const string address = @"http://localhost:49612/";

            sut = WebAddress.Create(address);

            Assert.AreEqual(address, sut.ToString());
        }

        [TestMethod]
        public void TryAddressWithPort()
        {
            AssertValidInput(@"localhost:49612");
        }

        [TestMethod]
        public void AddressWithFolders()
        {
            const string address = @"http://www.affecto.com/bss/recruitment";

            sut = WebAddress.Create(address);

            Assert.AreEqual(address, sut.ToString());
        }

        [TestMethod]
        public void TryAddressWithFolders()
        {
            AssertValidInput(@"localhost:49612/#/Error");
        }

        [TestMethod]
        public void AddressWithQuery()
        {
            const string address = @"http://regexlib.com/Search.aspx?k=web+address&c=-1&m=-1&ps=20";

            sut = WebAddress.Create(address);

            Assert.AreEqual(address, sut.ToString());
        }

        [TestMethod]
        public void TryAddressWithQuery()
        {
            AssertValidInput(@"https://www.google.fi/webhp?sourceid=chrome-instant&ion=1&espv=2&ie=UTF-8#q=email%20address%20regex");
        }

        [TestMethod]
        public void AddressWithFinnishAlphabetExtraCharacters()
        {
            const string address = @"http://www.häviö.fi";

            sut = WebAddress.Create(address);

            Assert.AreEqual(address, sut.ToString());
        }

        [TestMethod]
        public void TryAddressWithFinnishAlphabetExtraCharacters()
        {
            AssertValidInput(@"http://www.häviö.fi");
        }

        private static void AssertValidInput(string value)
        {
            AssertSuccessfulTryCreate(value);
            AssertSatisfiedSpecification(value);
        }

        private static void AssertSatisfiedSpecification(string value)
        {
            var specification = new WebAddressSpecification();
            Assert.IsTrue(specification.IsSatisfiedBy(value));
            Assert.IsTrue(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }

        private static void AssertSuccessfulTryCreate(string value)
        {
            string failureReason;
            WebAddress result;
            Assert.IsTrue(WebAddress.TryCreate(value, out result, out failureReason));
            Assert.AreEqual(value, result.ToString());
            Assert.IsTrue(string.IsNullOrWhiteSpace(failureReason));
        }

        private static void AssertInvalidInput(string value)
        {
            AssertFailedTryCreate(value);
            AssertDissatisfiedSpecification(value);
        }

        private static void AssertDissatisfiedSpecification(string value)
        {
            var specification = new WebAddressSpecification();
            Assert.IsFalse(specification.IsSatisfiedBy(value));
            Assert.IsFalse(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }

        private static void AssertFailedTryCreate(string value)
        {
            string failureReason;
            WebAddress result;
            Assert.IsFalse(WebAddress.TryCreate(value, out result, out failureReason));
            Assert.IsNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(failureReason));
        }
    }
}