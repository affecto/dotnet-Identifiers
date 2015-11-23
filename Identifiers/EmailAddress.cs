using System;

namespace Affecto.Identifiers
{
    public class EmailAddress
    {
        private readonly string emailAddress;

        private EmailAddress(string emailAddress)
        {
            this.emailAddress = emailAddress;
        }

        public static EmailAddress Create(string emailAddress)
        {
            if (emailAddress == null)
            {
                throw new ArgumentNullException("emailAddress");
            }

            var specification = new EmailAddressSpecification();
            if (specification.IsSatisfiedBy(emailAddress))
            {
                return new EmailAddress(emailAddress);
            }
            throw new ArgumentException(string.Format("Email address '{0}' doesn't satisfy specification.", emailAddress), "emailAddress");
        }

        public static bool TryCreate(string emailAddress, out EmailAddress result, out string failureReason)
        {
            var specification = new EmailAddressSpecification();
            if (specification.IsSatisfiedBy(emailAddress))
            {
                result = new EmailAddress(emailAddress);
                failureReason = string.Empty;
                return true;
            }

            result = null;
            failureReason = specification.GetReasonsForDissatisfactionSeparatedWithNewLine();
            return false;
        }

        public override string ToString()
        {
            return emailAddress;
        }

        public override bool Equals(object obj)
        {
            if (obj is EmailAddress)
            {
                return Equals(obj as EmailAddress);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return emailAddress.GetHashCode();
        }

        protected bool Equals(EmailAddress other)
        {
            return emailAddress.Equals(other.emailAddress);
        }
    }
}