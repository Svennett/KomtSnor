using System;
using System.Security.Cryptography;

namespace KomtSnor.Domain.Encryptor
{
    public class Encryptor1
    {
        private const string EcryptedKeyFilePath = "/frREionP2vdiTGzJwuM/KEfGbEFLeBDMosgWtIvOvWd7A0J2hKJ6xrBDF/YgKNrPGeVOvU7vMUQ5fbzW4lGa0gIwm6jQ63IoWbaAMs+OJ0=";
        private const string EcryptedIVFilePath = "/frREionP2vdiTGzJwuM/KEfGbEFLeBDMosgWtIvOvWd7A0J2hKJ6xrBDF/YgKNrmwJ+VJvycXwZVAcEiuolGn9GAyheJPdW3061u0yBbVI=";


        public static string Encrypt(string text)
        {
            byte[] plainTextBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(text);

            AesCryptoServiceProvider aesCrypto = CreateCryptoServiceProvider();

            ICryptoTransform crypto = aesCrypto.CreateEncryptor(aesCrypto.Key, aesCrypto.IV);
            byte[] encryptedBytes = crypto.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
            crypto.Dispose();
            string encrypteString = Convert.ToBase64String(encryptedBytes);
            return encrypteString;
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);

            AesCryptoServiceProvider aesCrypto = CreateCryptoServiceProvider();

            ICryptoTransform crypto = aesCrypto.CreateDecryptor(aesCrypto.Key, aesCrypto.IV);
            byte[] decryptedBytes = crypto.TransformFinalBlock(encryptedTextBytes, 0, encryptedTextBytes.Length);
            crypto.Dispose();
            string decrypteString = System.Text.ASCIIEncoding.ASCII.GetString(decryptedBytes);
            return decrypteString;
        }

        private static AesCryptoServiceProvider CreateCryptoServiceProvider()
        {
            string key = GetValueFromTextFile(EcryptedKeyFilePath);
            string IV = GetValueFromTextFile(EcryptedIVFilePath);
            AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider();
            aesCrypto.BlockSize = 128;
            aesCrypto.KeySize = 256;
            aesCrypto.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
            aesCrypto.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
            aesCrypto.Padding = PaddingMode.PKCS7;
            aesCrypto.Mode = CipherMode.CBC;
            return aesCrypto;
        }

        private static string GetValueFromTextFile(string encryptedFilePath)
        {
            string decriptedFilePath = DefaultEncryptor.Decrypt(encryptedFilePath);
            string fileValue = System.IO.File.ReadAllText(decriptedFilePath);
            return fileValue ;
        }

    }
}