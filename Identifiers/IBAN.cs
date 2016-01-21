using System;

namespace Affecto.Identifiers
{
    public class IBAN
    {
        private readonly string ibanNumber;

        private IBAN(string ibanNumber)
        {
            this.ibanNumber = Compress(ibanNumber);
        }

        public static IBAN Create(string ibanNumber)
        {
            if (ibanNumber == null)
            {
                throw new ArgumentNullException("ibanNumber");
            }

            var specification = new IBANSpecification();
            if (specification.IsSatisfiedBy(ibanNumber))
            {
                return new IBAN(ibanNumber);
            }
            throw new ArgumentException(string.Format("IBAN '{0}' doesn't satisfy specification.", ibanNumber), "ibanNumber");
        }

        public static bool TryCreate(string ibanNumber, out IBAN result, out string failureReason)
        {
            var specification = new IBANSpecification();
            if (specification.IsSatisfiedBy(ibanNumber))
            {
                result = new IBAN(ibanNumber);
                failureReason = string.Empty;
                return true;
            }

            result = null;
            failureReason = specification.GetReasonsForDissatisfactionSeparatedWithNewLine();
            return false;
        }

        public override string ToString()
        {
            return ibanNumber;
        }

        public override bool Equals(object obj)
        {
            if (obj is IBAN)
            {
                return Equals(obj as IBAN);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ibanNumber.GetHashCode();
        }

        protected bool Equals(IBAN other)
        {
            return ibanNumber.Equals(other.ibanNumber);
        }

        private static string Compress(string iban)
        {
            return (iban ?? string.Empty).Trim().Replace(" ", string.Empty).ToUpper();
        }
    }
}
