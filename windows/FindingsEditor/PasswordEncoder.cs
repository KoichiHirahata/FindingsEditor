using System;
using System.Text;
using System.Security.Cryptography;

namespace FindingsEdior
{
    public class PasswordEncoder
    {
        private PasswordEncoder() { }

        // 128bit(16byte)のIV（初期ベクタ）とKey（暗号キー）
        private const string AesIV = @"&%jqiIurtmslLE58";
        private const string AesKey = @"3uJi<9!$kM0lkxme";

        /// 文字列をAESで暗号化
        public static string Encrypt(string text)
        {
            // AES暗号化サービスプロバイダ
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 128;
            aes.IV = Encoding.UTF8.GetBytes(AesIV);
            aes.Key = Encoding.UTF8.GetBytes(AesKey);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // 文字列をバイト型配列に変換
            byte[] src = Encoding.Unicode.GetBytes(text);

            // 暗号化する
            using (ICryptoTransform encrypt = aes.CreateEncryptor())
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

                // バイト型配列からBase64形式の文字列に変換
                return Convert.ToBase64String(dest);
            }
        }

        /// 文字列をAESで復号化
        public static string Decrypt(string text)
        {
            // AES暗号化サービスプロバイダ
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 128;
            aes.IV = Encoding.UTF8.GetBytes(AesIV);
            aes.Key = Encoding.UTF8.GetBytes(AesKey);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // Base64形式の文字列からバイト型配列に変換
            byte[] src = Convert.FromBase64String(text);

            // 複号化する
            using (ICryptoTransform decrypt = aes.CreateDecryptor())
            {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                return Encoding.Unicode.GetString(dest);
            }
        }

    }
}
