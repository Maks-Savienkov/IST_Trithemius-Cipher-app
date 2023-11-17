using System.Linq;

namespace IST_Trithemius_Cipher_WPF_app.Services
{
    internal class TrithemiusCipherService
    {
        private string _keywordAlphabet;
        private string _alphabet;

        private void UpdateInfo(string cipherKey, string alphabet)
        {
            cipherKey = cipherKey.Trim().ToUpper();
            _keywordAlphabet = new string(
                    cipherKey.Distinct()
                             .Concat(alphabet.Where(c => char.IsLetter(c) && !cipherKey.Contains(c)))
                             .ToArray()
                );
            _alphabet = alphabet;
        }

        public string Encode(string plainText, string cipherKey, string alphabet)
        {
            UpdateInfo(cipherKey, alphabet);

            string ciphertext = "";
            foreach (char c in plainText)
            {
                ciphertext += char.IsLetter(c) ? GetEncodeSymbol(c) : c;
            }

            return ciphertext;
        }

        public string Decode(string cipherText, string cipherKey, string alphabet)
        {
            UpdateInfo(cipherKey, alphabet);

            string decryptedText = "";
            foreach (char c in cipherText)
            {
                decryptedText += char.IsLetter(c) ? GetDecodeSymbol(c) : c;
            }

            return decryptedText;
        }

        private char GetEncodeSymbol(char c) 
        {
            bool isUpper = char.IsUpper(c);
            int index = isUpper ? _alphabet.IndexOf(c) : _alphabet.IndexOf(char.ToUpper(c));
            char encryptedChar = _keywordAlphabet[index];

            return isUpper ? encryptedChar : char.ToLower(encryptedChar);
        }

        private char GetDecodeSymbol(char c)
        {
            bool isUpper = char.IsUpper(c);
            int index = _keywordAlphabet.IndexOf(isUpper ? c : char.ToUpper(c));
            char decryptedChar = _alphabet[index];
            return isUpper ? decryptedChar : char.ToLower(decryptedChar);
        }
    }
}
