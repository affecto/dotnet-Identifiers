using System;

namespace Affecto.Identifiers
{
    public class WebAddress
    {
        private readonly string webAddress;

        private WebAddress(string webAddress)
        {
            this.webAddress = webAddress;
        }

        public static WebAddress Create(string webAddress)
        {
            if (webAddress == null)
            {
                throw new ArgumentNullException("webAddress");
            }

            var specification = new WebAddressSpecification();
            if (specification.IsSatisfiedBy(webAddress))
            {
                return new WebAddress(webAddress);
            }
            throw new ArgumentException(string.Format("Email address '{0}' doesn't satisfy specification.", webAddress), "webAddress");
        }

        public static bool TryCreate(string webAddress, out WebAddress result, out string failureReason)
        {
            var specification = new WebAddressSpecification();
            if (specification.IsSatisfiedBy(webAddress))
            {
                result = new WebAddress(webAddress);
                failureReason = string.Empty;
                return true;
            }

            result = null;
            failureReason = specification.GetReasonsForDissatisfactionSeparatedWithNewLine();
            return false;
        }

        public override string ToString()
        {
            return webAddress;
        }

        public override bool Equals(object obj)
        {
            WebAddress address = obj as WebAddress;
            if (address != null)
            {
                return Equals(address);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return webAddress.GetHashCode();
        }

        protected bool Equals(WebAddress other)
        {
            return webAddress.Equals(other.webAddress);
        }
    }
}