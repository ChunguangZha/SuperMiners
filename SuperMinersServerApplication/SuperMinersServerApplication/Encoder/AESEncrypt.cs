using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Encoder
{
    public class AESEncrypt
    {
        private static readonly byte[] KEY = new byte[32] { 90, 151, 145, 136, 200, 35, 167, 159, 237, 25, 84, 106, 2, 189, 60, 96, 173, 250, 45, 203, 46, 35, 115, 212, 206, 233, 75, 253, 73, 189, 194, 55, };
        private static readonly byte[] IV = new byte[16] { 202, 253, 32, 237, 42, 198, 96, 126, 173, 29, 254, 37, 57, 187, 67, 133, };

        public static byte[] Encrypt(byte[] src, int offset, int count)
        {
            return Encrypt(src, offset, count, KEY, IV);
        }

        public static byte[] Encrypt(byte[] src, int offset, int count, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (src == null || src.Length <= 0)
                return null;
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("iv");

            byte[] encrypted;
            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(src, offset, count);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public static byte[] Decrypt(byte[] src, int offset, int count)
        {
            return Decrypt(src, offset, count, KEY, IV);
        }

        public static byte[] Decrypt(byte[] src, int offset, int count, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (src == null || src.Length <= 0)
                return null;
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("iv");

            // Declare the string used to hold
            // the decrypted text.
            List<byte> dest = new List<byte>();

            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(src, offset, count))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] tmp = new byte[1024];
                        int readCount = 0;
                        do
                        {
                            readCount = csDecrypt.Read(tmp, 0, 1024);
                            for (int i = 0; i < readCount; i++)
                            {
                                dest.Add(tmp[i]);
                            }
                        } while (readCount > 0);
                    }
                }
            }

            return dest.ToArray();

        }
    }
}
