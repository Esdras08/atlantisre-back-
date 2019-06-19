using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Inexa.Atlantis.Re.Commons.Infras.Helpers
{
    public static class Hasher
    {
        private static readonly byte[] Bytes = Encoding.ASCII.GetBytes("CleNSIAT");



        /// <summary>
        /// Cryptage de mot de passe
        /// </summary>
        /// <param name="login">login de l'utilisateur</param>
        /// <param name="mdpHashed">mot de passe pre-crypté</param>
        /// <returns>mot de passe crypté</returns>
        public static string EncodeMdp(string login, string mdpHashed)
        {
            return string.Format("{0};{1}", login, mdpHashed).Encode();
        }

        public static string DecodeMdp(string complexeEncodedMdp)
        {
            var complexeDecodedMdp = complexeEncodedMdp.Decode();
            var result = complexeDecodedMdp;

            if (complexeDecodedMdp.Contains(";"))
            {
                var tabPassword = complexeDecodedMdp.Split(';');
                result = tabPassword[1].Decode();
            }

            return result;
        }



        public static string Encode(this string strToEncode)
        {
            var str = strToEncode.Base64Encode();
            var response = str.Base64Encode();
            return response.Base64Encode();
        }

        public static string Decode(this string strToDecode)
        {
            var str = strToDecode.Base64Decode();
            var response = str.Base64Decode();
            return response.Base64Decode();
        }



        private static string Base64Encode(this string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException
                       ("The string which needs to" + " be encrypted can not be null.");
            }
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        private static string Base64Decode(this string base64EncodedData)
        {
            if (string.IsNullOrEmpty(base64EncodedData))
            {
                throw new ArgumentNullException("The string which needs to" + " be decrypted can not be null.");
            }
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }



        public static string GetMd5Hash(this string value)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string Encrypt(this string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException
                       ("The string which needs to" + " be encrypted can not be null.");
            }
            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream,
            cryptoProvider.CreateEncryptor(Bytes, Bytes), CryptoStreamMode.Write);
            var writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        public static string Decrypt(this string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException("The string which needs to" + " be decrypted can not be null.");
            }
            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream
                (Convert.FromBase64String(cryptedString));
            var cryptoStream = new CryptoStream(memoryStream,
                                                cryptoProvider.CreateDecryptor(Bytes, Bytes), CryptoStreamMode.Read);
            var reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }
    }
}


