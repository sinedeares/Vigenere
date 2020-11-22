using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vigenere
{
    class Program
    {
        static void Main(string[] args)
        {
            var cipher = new VigenereCipher();

            Console.WriteLine("Введите текст: ");
            var inputText = Console.ReadLine().ToUpper();

            Console.WriteLine("Введите ключ: ");
            var password = Console.ReadLine().ToUpper();

            var encryptedText = cipher.Encrypt(inputText, password);
            Console.WriteLine("Зашифрованное сообщение: {0}", encryptedText);
            Console.WriteLine("Расшифрованное сообщение: {0}", cipher.Decrypt(encryptedText, password));
            Console.ReadLine();
        }
    }

    public class VigenereCipher
    {
        
        const string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        readonly string letters;

        public VigenereCipher()
        {
            letters = alphabet;
        }

        private string GetRepeatKey(string s, int n)
        {
            var p = s;
            while (p.Length < n)
            {
                p += p;
            }

            return p.Substring(0, n);
        }

        private string Vigenere(string text, string password, bool encrypting = true)
        {
            var rk = GetRepeatKey(password, text.Length);
            var returnedValue = "";
            var q = letters.Length;

            for (int i = 0; i < text.Length; i++)
            {
                var letterIndex = letters.IndexOf(text[i]);
                var codeIndex = letters.IndexOf(rk[i]);

                if (letterIndex < 0)
                {
                    //если буква не найдена, добавляем её в исходном виде
                    returnedValue += text[i].ToString();
                }
                else
                {
                    returnedValue += letters[(q + letterIndex + ((encrypting ? 1 : -1) * codeIndex)) % q].ToString();
                }
            }

            return returnedValue;
        }

        //шифрование
        public string Encrypt(string plainMessage, string password)
            => Vigenere(plainMessage, password);

        //дешифрование 
        public string Decrypt(string encryptedMessage, string password)
            => Vigenere(encryptedMessage, password, false);
    }
}
