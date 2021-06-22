using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace IVCC_Camera_CSV_Export_Utility
{
    [DelimitedRecord(","), IgnoreFirst(1)]
    public class ivCamera
    {
        [FieldQuoted('"',QuoteMode.OptionalForBoth)]
        public int Number { get; set; }
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Name { get; set; }
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Home_Site { get; set; }
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string IP_Address { get; set; }
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Service_ID { get; set; }
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Access_URL { get; set; }
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public bool Online { get; set; }
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Primary_NVR { get; set; }
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Primary_NVR_IP { get; set; }
    }
}
