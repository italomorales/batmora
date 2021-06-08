using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TicketBom.Infra.Crypto
{
    public static class Crypto
    {
        private static readonly byte[] Salt = {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76};
        private const string EncryptionKey = "MAKV2SPBNI99212";

        public static string ToMd5(this string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            
            foreach (var t in hashBytes) {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        public static string EncryptUrl(this string text)
        {
            var clearBytes = Encoding.Unicode.GetBytes(text);
            using var encryptor = Aes.Create();
            var pdb = new Rfc2898DeriveBytes(EncryptionKey, Salt);
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
            }
            return HttpUtility.UrlEncode(Convert.ToBase64String(ms.ToArray()));
        }

        public static string DecryptUrl(this string text)
        {
            text = HttpUtility.UrlDecode(text);
            var cipherBytes = Convert.FromBase64String(text);
            using var encryptor = Aes.Create();
            var pdb = new Rfc2898DeriveBytes(EncryptionKey, Salt);
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.Close();
            }
            return Encoding.Unicode.GetString(ms.ToArray());
        }
    }
}
