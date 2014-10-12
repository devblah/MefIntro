
namespace DevBlah.MefIntro.Contracts
{
    /// <summary>
    /// interface to provide the functionality of a encoding/decoding module
    /// </summary>
    public interface IEncoder
    {
        /// <summary>
        /// name of the encoder
        /// </summary>
        string Name { get; }

        /// <summary>
        /// encodes a given string
        /// </summary>
        /// <param name="plain"></param>
        /// <returns></returns>
        string Encode(string plain);

        /// <summary>
        /// decodes a given string to the original string
        /// </summary>
        /// <param name="encoded"></param>
        /// <returns></returns>
        string Decode(string encoded);
    }
}