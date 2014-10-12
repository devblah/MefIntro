using System;
using System.ComponentModel.Composition;
using DevBlah.MefIntro.Contracts;

namespace DevBlah.MefIntro.Encoder.Base64
{
    [Export(typeof(IEncoder))]
    public class Encoder : IEncoder
    {
        /// <summary>
        /// Name of our encoder
        /// </summary>
        public string Name { get { return "Base 64"; } }

        /// <summary>
        /// encodes a given string into a base64 string
        /// </summary>
        /// <param name="plain"></param>
        /// <returns></returns>
        public string Encode(string plain)
        {
            return Convert.ToBase64String(GetBytes(plain));
        }

        /// <summary>
        /// decodes a given base64 string to the original string
        /// </summary>
        /// <param name="encoded"></param>
        /// <returns></returns>
        public string Decode(string encoded)
        {
            return GetString(Convert.FromBase64String(encoded));
        }

        /// <summary>
        /// convert a byte array into a string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static string GetString(byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        /// <summary>
        /// convert a string into a byte array
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
