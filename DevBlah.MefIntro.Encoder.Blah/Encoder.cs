using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevBlah.MefIntro.Contracts;

namespace DevBlah.MefIntro.Encoder.Blah
{
    [Export(typeof(IEncoder))]
    public class Encoder : IEncoder
    {
        public string Name { get { return "Blah"; } }
        public string Encode(string plain)
        {
            return string.Format("Blah-'{0}'", plain);
        }

        public string Decode(string encoded)
        {
            if (encoded == null || encoded.IndexOf("Blah-", StringComparison.Ordinal) == -1)
            {
                throw new Exception("The given string is not Blah encoded.");
            }

            return encoded.Substring(5);
        }
    }
}
