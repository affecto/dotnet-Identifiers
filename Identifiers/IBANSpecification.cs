using Affecto.Patterns.Specification;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Affecto.Identifiers
{
    public class IBANSpecification : Specification<string>
    {
        protected override bool IsSatisfied(string entity)
        {
            if (entity == null)
            {
                AddReasonForDissatisfaction("IBAN is null.");
                return false;
            }
            if (!Regex.IsMatch(entity, @"^[\d a-zA-Z]+$"))
            {
                AddReasonForDissatisfaction(string.Format("IBAN '{0}' contains illegal characters.", entity));
                return false;
            }

            entity = (entity ?? string.Empty).Trim().Replace(" ", string.Empty).ToUpper();

            var IBANLenghtForCountry = GetIBANLengthForCountry(entity);
            if(IBANLenghtForCountry < 0)
            {
                AddReasonForDissatisfaction(string.Format("IBAN '{0}' has an invalid or not supported country code.", entity));
                return false;
            }

            if (entity.Length < IBANLenghtForCountry)
            {
                AddReasonForDissatisfaction(string.Format("IBAN '{0}' is too short.", entity));
                return false;
            }
            if (entity.Length > IBANLenghtForCountry)
            {
                AddReasonForDissatisfaction(string.Format("IBAN '{0}' is too long.", entity));
                return false;
            }

            
            if (!IsValidCheckSum(entity))
            {
                AddReasonForDissatisfaction(string.Format("IBAN '{0}' contains an invalid checksum.", entity));
                return false;
            }

            return true;

        }

        private int CalculateRemainder(string iban)
        {
            // https://www.fkl.fi/teemasivut/sepa/tekninen_dokumentaatio/Sivut/default.aspx
            // https://www.fkl.fi/teemasivut/sepa/tekninen_dokumentaatio/Dokumentit/IBAN_ja_BIC_maksuliikenteessa.pdf
            // https://www.fkl.fi/teemasivut/sepa/tekninen_dokumentaatio/Dokumentit/Suomalaiset_rahalaitostunnukset_ja_BIC-koodit.pdf
                        

            int ibanLength = iban.Length;

            int checksum = 0;

            for (int i = 0; i < ibanLength; i++)
            {
                int value = 0;
                char c = iban[(i + 4) % ibanLength];

                if (c >= '0' && c <= '9')
                {
                    value = c - '0';
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    value = c - 'A';
                    checksum = (checksum * 10 + (value / 10 + 1)) % 97;
                    value %= 10;
                }

                checksum = (checksum * 10 + value) % 97;
            }

            return checksum;
        }

        private bool IsValidCheckSum(string ibanToCheck)
        {
            // https://www.fkl.fi/teemasivut/sepa/tekninen_dokumentaatio/Sivut/default.aspx
            // https://www.fkl.fi/teemasivut/sepa/tekninen_dokumentaatio/Dokumentit/IBAN_ja_BIC_maksuliikenteessa.pdf
            // https://www.fkl.fi/teemasivut/sepa/tekninen_dokumentaatio/Dokumentit/Suomalaiset_rahalaitostunnukset_ja_BIC-koodit.pdf
            // https://www.swift.com/sites/default/files/resources/swift_standards_infopaper_ibanregistry.pdf

            //Move country code and checksum (4 chars) to the end of the string
            string iban = ibanToCheck.Substring(4) + ibanToCheck.Substring(0, 4);
            int ibanLength = iban.Length;

            int value = 0;
            BigInteger val = 0;

            //Transform chars to numbers (A = 10, B = 11, ...)
            for (int i = 0; i < ibanLength; i++)
            {                
                char c = iban[i];

                if (c >= '0' && c <= '9')
                {
                    val = val*10 + int.Parse(c.ToString());
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    value = c - 'A'; // for ex. D => 'D' - 'A' = 3
                    val = val * 100 + value + 10;
                }
            }

            BigInteger checksum = BigInteger.Remainder(val, 97);

            if(checksum == 1)
                return true;
            else
                return false;
        }


        
        private int GetIBANLengthForCountry(string iban)
        {
            string countryCode = iban.Substring(0, 2);
            int length = -1;
            if (IBANCountries.Instance.IBANLengthByCountry.Keys.Contains(countryCode))
                length = IBANCountries.Instance.IBANLengthByCountry[countryCode];
                        
            return length;

        }


    }
    

}
