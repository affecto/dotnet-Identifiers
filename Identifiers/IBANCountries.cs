using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Affecto.Identifiers
{
    public sealed class IBANCountries
    {
        private static volatile IBANCountries instance;
        private static object syncRoot = new Object();
        public Dictionary<string, int> IBANLengthByCountry;

        private IBANCountries()
        {
            //Load country data from XML file
            //<Document>
            //  <Country>
            //    <ISOCode>FI</ISOCode>
            //    <IBANLength>18</IBANLength>
            //  </Country>
            //  .
            //  .
            //</Document>

            IBANLengthByCountry = new Dictionary<string, int>();

            using (var strm = File.OpenRead("IBANCountryData.xml"))
            {
                XDocument xml = XDocument.Load(strm);
                var countries = xml.Descendants("Country");
                foreach (var country in countries)
                {
                    string ISOCode = country.Element("ISOCode").Value;
                    if (String.IsNullOrWhiteSpace(ISOCode))
                        throw new ApplicationException("Invalid ISOCode in country element in IBANCountryData xml.");

                    int length;
                    var isValidLength = int.TryParse(country.Element("IBANLength").Value, out length);
                    if (isValidLength && length > 0)
                        IBANLengthByCountry.Add(ISOCode, length);
                    else
                        throw new ApplicationException(string.Format("Invalid IBANLength for country '{0}' in IBANCountryData xml.", ISOCode));
                }

                strm.Close();
            }

        }

        public static IBANCountries Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new IBANCountries();
                    }
                }

                return instance;
            }
        }
    }

}
