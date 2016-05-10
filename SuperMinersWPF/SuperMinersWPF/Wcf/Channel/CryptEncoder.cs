using SuperMinersServerApplication.Encoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Channel
{
    public static class CryptEncoder
    {
        private static readonly RSACryptoServiceProvider RSASend = new RSACryptoServiceProvider();
        private static readonly RSACryptoServiceProvider RSAReceive;
        private static bool _isKeyReady = false;

        static CryptEncoder()
        {
            string KEY = "<RSAKeyValue><Modulus>eVefNsoBmOtpWLzrxumol1HhWsyRT0UVhBPfh+Pl4fGfhZQiZx7elOUaphTEgyB5l6gjtG3LHNgSDzBtEzErBj79JQ1eDRw4lq6Rsgu3eLgPHVG/HlQvXyB8YbXEreHekiI5lno1HekdN8RZbbYrmqLwHeGKcT+G7w1njx9UELE=</Modulus><Exponent>AAEAAQ==</Exponent></RSAKeyValue>";
            RSASend.FromXmlString(KEY);

            CspParameters cp = new CspParameters();
            cp.KeyContainerName = Guid.NewGuid().ToString();
            RSAReceive = new RSACryptoServiceProvider(cp);

            _isKeyReady = true;
        }

        public static bool Ready
        {
            get
            {
                return _isKeyReady;
            }
        }

        public static string Key
        {
            get
            {
                //生成长密钥（即可加，又可解）
                return RSAReceive.ToXmlString(false);
            }
        }
        
        private static readonly Random RND = new Random();
        private static readonly object RNDLock = new object();

        private static void BuildAESKey(out byte[] keyData, out byte[] ivData)
        {
            keyData = new byte[32];
            ivData = new byte[16];
            lock (RNDLock)
            {
                RND.NextBytes(keyData);
                RND.NextBytes(ivData);
            }
        }

        public static byte[] Read(Stream stream)
        {
            List<byte> dataList = new List<byte>();
            byte[] buffer = new byte[512];
            int readLen = 0;
            while ((readLen = stream.Read(buffer, 0, 512)) > 0)
            {
                for (int i = 0; i < readLen; i++)
                {
                    dataList.Add(buffer[i]);
                }
            }

            buffer = dataList.ToArray();

            if (buffer.Length == 0)
            {
                return buffer;
            }

#if Encrypt
            int index = 0;
            ushort keyLen = BitConverter.ToUInt16(buffer, index);
            index += 2;

            if (keyLen == 0)
            {
                buffer = AESEncrypt.Decrypt(buffer, index, buffer.Length - index);
            }
            else
            {
                byte[] keyData = new byte[keyLen];
                Array.Copy(buffer, index, keyData, 0, keyLen);
                index += keyLen;

                ushort ivLen = BitConverter.ToUInt16(buffer, index);
                index += 2;

                byte[] ivData = new byte[ivLen];
                Array.Copy(buffer, index, ivData, 0, ivLen);
                index += ivLen;

                byte[] key = RSAReceive.Decrypt(keyData, true);
                byte[] iv = RSAReceive.Decrypt(ivData, true);

                buffer = AESEncrypt.Decrypt(buffer, index, buffer.Length - index, key, iv);
            }
#endif
            return buffer;
        }

        public static byte[] Build(byte[] data)
        {
            byte[] buffer = data;
            byte[] key = null;
            byte[] iv = null;

            byte[] keyData;
            byte[] ivData;
            BuildAESKey(out keyData, out ivData);

            buffer = AESEncrypt.Encrypt(buffer, 0, buffer.Length, keyData, ivData);

            key = RSASend.Encrypt(keyData, true);
            iv = RSASend.Encrypt(ivData, true);

            List<byte> dataList = new List<byte>();
            if (null == key)
            {
                dataList.Add(0);
                dataList.Add(0);
                dataList.AddRange(buffer);
            }
            else
            {
                dataList.AddRange(BitConverter.GetBytes((ushort)key.Length));
                dataList.AddRange(key);
                dataList.AddRange(BitConverter.GetBytes((ushort)iv.Length));
                dataList.AddRange(iv);
                dataList.AddRange(buffer);
            }

            return dataList.ToArray();
        }

        public static int Write(byte[] data, Stream stream)
        {
#if Encrypt
            byte[] buffer = data;
            byte[] key = null;
            byte[] iv = null;

            byte[] keyData;
            byte[] ivData;
            BuildAESKey(out keyData, out ivData);

            buffer = AESEncrypt.Encrypt(buffer, 0, buffer.Length, keyData, ivData);

            key = RSASend.Encrypt(keyData, true);
            iv = RSASend.Encrypt(ivData, true);

            if (null == key)
            {
                stream.WriteByte(0);
                stream.WriteByte(0);
                stream.Write(buffer, 0, buffer.Length);
                return buffer.Length + 2;
            }
            else
            {
                stream.Write(BitConverter.GetBytes((ushort)key.Length), 0, 2);
                stream.Write(key, 0, key.Length);
                stream.Write(BitConverter.GetBytes((ushort)iv.Length), 0, 2);
                stream.Write(iv, 0, iv.Length);
                stream.Write(buffer, 0, buffer.Length);
                return buffer.Length + 4 + key.Length + iv.Length;
            }

#else
            stream.Write(data, 0, data.Length);
            return data.Length;
#endif
        }
    }
}
