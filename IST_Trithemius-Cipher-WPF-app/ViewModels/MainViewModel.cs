using System.Linq;
using IST_Trithemius_Cipher_WPF_app.Infrastructure.Commands;
using IST_Trithemius_Cipher_WPF_app.Services;
using System.Windows.Input;

namespace IST_Trithemius_Cipher_WPF_app.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private string _cipherKey;
        private string _plainText;
        private string _cipherText;
        private string _encodeResult;
        private string _decodeResult;
        private string _alphabet;
        private TrithemiusCipherService _cipherService;


        public string CipherKey
        {
            get => _cipherKey;
            set
            {
                _cipherKey = value;
                OnPropertyChanged(nameof(CipherKey));
            }
        }

        public string PlainText
        {
            get => _plainText;
            set
            {
                _plainText = value;
                OnPropertyChanged(nameof(PlainText));
            }
        }

        public string CipherText
        {
            get => _cipherText;
            set
            {
                _cipherText = value;
                OnPropertyChanged(nameof(CipherText));
            }
        }

        public string EncodeResult
        {
            get => _encodeResult;
            set
            {
                _encodeResult = value;
                OnPropertyChanged(nameof(EncodeResult));
            }
        }

        public string DecodeResult
        {
            get => _decodeResult;
            set
            {
                _decodeResult = value;
                OnPropertyChanged(nameof(DecodeResult));
            }
        }

        public string Alphabet
        {
            get => _alphabet;
            set
            {
                _alphabet = value;
                OnPropertyChanged(nameof(Alphabet));
            }
        }

        /*-----------------------------------------------------------------------------------------*/

        #region Commands



        #region Encode Command

        public ICommand EncodeCommand { get; set; }

        private bool CanEncodeCommandExecute(object p)
        {
            if (string.IsNullOrEmpty(CipherKey)
                || string.IsNullOrEmpty(PlainText)
                || string.IsNullOrEmpty(Alphabet)
                || !ValidateTextAlphabetCoherence(PlainText, Alphabet)
                || !ValidateTextAlphabetCoherence(CipherKey, Alphabet))
            {
                return false;
            }
            return true;
        }

        private void OnEncodeCommandExecuted(object p)
        {
            EncodeResult = _cipherService.Encode(PlainText, CipherKey, Alphabet);
        }

        #endregion


        #region Decode Command

        public ICommand DecodeCommand { get; set; }

        private bool CanDecodeCommandExecute(object p)
        {
            if (string.IsNullOrEmpty(CipherKey)
                || string.IsNullOrEmpty(CipherText)
                || string.IsNullOrEmpty(Alphabet)
                || !ValidateTextAlphabetCoherence(CipherText, Alphabet)
                || !ValidateTextAlphabetCoherence(CipherKey, Alphabet))
            {
                return false;
            }
            return true;
        }

        private void OnDecodeCommandExecuted(object p)
        {
            DecodeResult = _cipherService.Decode(CipherText, CipherKey, Alphabet);
        }

        #endregion



        #endregion

        /*-----------------------------------------------------------------------------------------*/

        #region Events



        #endregion

        /*-----------------------------------------------------------------------------------------*/

        #region Validations

        private bool ValidateTextAlphabetCoherence(string plainText, string alphabetStr)
        {
            plainText= plainText.Trim().ToUpper();
            char[] alphabet = alphabetStr.ToUpper().ToCharArray();

            for (int i = 0; i < plainText.Length; i++)
            {
                if (plainText[i] == ' ')
                {
                    continue;
                }
                if (!alphabet.Contains(plainText[i]))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        /*-----------------------------------------------------------------------------------------*/

        public MainViewModel()
        {
            _cipherService = new TrithemiusCipherService();
            Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZАБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";
            CipherKey = "миргород";
            EncodeCommand = new LambdaCommand(OnEncodeCommandExecuted, CanEncodeCommandExecute);
            DecodeCommand = new LambdaCommand(OnDecodeCommandExecuted, CanDecodeCommandExecute);
        }
    }
}
