using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Encoder
{
    public class CryptEncoder : MessageEncoder
    {
        private static readonly RSACryptoServiceProvider RSAReceive = new RSACryptoServiceProvider();

        static CryptEncoder()
        {
            string KEY = "<RSAKeyValue><Modulus>eVefNsoBmOtpWLzrxumol1HhWsyRT0UVhBPfh+Pl4fGfhZQiZx7elOUaphTEgyB5l6gjtG3LHNgSDzBtEzErBj79JQ1eDRw4lq6Rsgu3eLgPHVG/HlQvXyB8YbXEreHekiI5lno1HekdN8RZbbYrmqLwHeGKcT+G7w1njx9UELE=</Modulus><Exponent>AAEAAQ==</Exponent><P>2kMU2Rj7+OpYKOoKP7C6+kAkKuTHgnkT5OrNQy0Rt+LYO7sTc8ovbo+kcMWRyy7Ra3GawC/zuQavtko7rHX3tQ==</P><Q>jlKWUo3Pme2LhhGx0MqTCSd3D8qilmKl0wI4Fwd0SRp9FhvsH0bYdro+72MdHtzw8/H/QY8HFo9tc+NmxVFajQ==</Q><DP>pE2xSQikzinjedFNK8rnxnE4iM22XsK0tjQHlxU7bFko/DYFG7pNYIZjfL1N1k2FOsPHgfvXFicxaSGSsG4RrQ==</DP><DQ>A0mxI1MXWqz8Liq2euZTI0EAJSM/Qk4hGpDQjuejLhUokpwuhkJyubtvvMQDZjUgc+JBTVhqh4DkvGqicyh/+Q==</DQ><InverseQ>vBZ1c2kWzhQbXc9SSRBObitsoa7Ce79L9PJuutIVzrJMLbpkMS/zKHt1QH68qHFaU+OlBu9AnrILphc3FYHgVQ==</InverseQ><D>Iyj+m3OhTtw35FypvTOLhH1XXWYVXPDZsTHI/alNvVC0NpKb/WF2gZJ5TFKMNqq8UPOJlQiTaEI7yWbw1DTVEEvIJAsBfN3/xR5GJ8aqwn0iZJGmHUVrkWxgzhRCCywgV+zSBIPpcu+c1sFJVp/zQkkgeD3qyODa2RaMfEY1mCE=</D></RSAKeyValue>";
            RSAReceive.FromXmlString(KEY);
        }

        private static readonly Random RND = new Random();
        private static readonly object RNDLock = new object();

        private MessageEncoder _inner;

        public CryptEncoder(MessageEncodingBindingElement ele)
        {
            this._inner = ele.CreateMessageEncoderFactory().Encoder;
        }

        public override string ContentType
        {
            get
            {
                return this._inner.ContentType;
            }
        }

        public override string MediaType
        {
            get
            {
                return this._inner.MediaType;
            }
        }

        public override MessageVersion MessageVersion
        {
            get
            {
                return this._inner.MessageVersion;
            }
        }

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

        public override bool IsContentTypeSupported(string contentType)
        {
            return this._inner.IsContentTypeSupported(contentType);
        }

        public override T GetProperty<T>()
        {
            return this._inner.GetProperty<T>();
        }

        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
        {
#if Encrypt
            if (buffer.Count == 0)
            {
                return this._inner.ReadMessage(buffer, bufferManager, contentType);
            }

            byte[] bytes;

            int index = buffer.Offset;
            ushort keyLen = BitConverter.ToUInt16(buffer.Array, index);
            index += 2;

            if (keyLen == 0)
            {
                bytes = AESEncrypt.Decrypt(buffer.Array, index, buffer.Count - index);
            }
            else
            {
                byte[] keyData = new byte[keyLen];
                Array.Copy(buffer.Array, index, keyData, 0, keyLen);
                index += keyLen;

                ushort ivLen = BitConverter.ToUInt16(buffer.Array, index);
                index += 2;

                byte[] ivData = new byte[ivLen];
                Array.Copy(buffer.Array, index, ivData, 0, ivLen);
                index += ivLen;

                byte[] key = RSAReceive.Decrypt(keyData, true);
                byte[] iv = RSAReceive.Decrypt(ivData, true);

                bytes = AESEncrypt.Decrypt(buffer.Array, index, buffer.Count - index, key, iv);
            }

            ArraySegment<byte> byteArray = new ArraySegment<byte>(bytes, 0, bytes.Length);
            return this._inner.ReadMessage(byteArray, bufferManager, contentType);
#else
            return this._inner.ReadMessage(buffer, bufferManager, contentType);
#endif
        }

        public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
        {
#if Encrypt
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

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

            MemoryStream ms = new MemoryStream(buffer);

            return this._inner.ReadMessage(ms, maxSizeOfHeaders, contentType);
#else
            return this._inner.ReadMessage(stream, maxSizeOfHeaders, contentType);
#endif
        }

        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
#if Encrypt
            if (RSAProvider.NoRSA())
            {
                return this._inner.WriteMessage(message, maxMessageSize, bufferManager);
            }

            ArraySegment<byte> bytes = this._inner.WriteMessage(message, maxMessageSize, bufferManager);
            if (bytes.Count == 0)
            {
                return bytes;
            }

            byte[] buffer;
            byte[] key = null;
            byte[] iv = null;

            RSACryptoServiceProvider rsa = RSAProvider.UseRSA();

            if (null == rsa)
            {
                buffer = AESEncrypt.Encrypt(bytes.Array, bytes.Offset, bytes.Count);
            }
            else
            {
                byte[] keyData;
                byte[] ivData;
                BuildAESKey(out keyData, out ivData);

                buffer = AESEncrypt.Encrypt(bytes.Array, bytes.Offset, bytes.Count, keyData, ivData);

                key = rsa.Encrypt(keyData, true);
                iv = rsa.Encrypt(ivData, true);
            }

            if (null == key)
            {
                int totalLength = 2 + buffer.Length + messageOffset;
                byte[] totalBytes = bufferManager.TakeBuffer(totalLength);

                int index = messageOffset;
                totalBytes[index] = 0;
                totalBytes[index + 1] = 0;
                index += 2;

                Array.Copy(buffer, 0, totalBytes, index, buffer.Length);

                ArraySegment<byte> byteArray = new ArraySegment<byte>(totalBytes, messageOffset, totalLength - messageOffset);
                return byteArray;
            }
            else
            {
                int totalLength = 4 + key.Length + iv.Length + buffer.Length + messageOffset;
                byte[] totalBytes = bufferManager.TakeBuffer(totalLength);

                int index = messageOffset;
                Array.Copy(BitConverter.GetBytes((ushort)key.Length), 0, totalBytes, index, 2);
                index += 2;

                Array.Copy(key, 0, totalBytes, index, key.Length);
                index += key.Length;

                Array.Copy(BitConverter.GetBytes((ushort)iv.Length), 0, totalBytes, index, 2);
                index += 2;

                Array.Copy(iv, 0, totalBytes, index, iv.Length);
                index += iv.Length;

                Array.Copy(buffer, 0, totalBytes, index, buffer.Length);

                ArraySegment<byte> byteArray = new ArraySegment<byte>(totalBytes, messageOffset, totalLength - messageOffset);
                return byteArray;
            }
#else
            return this._inner.WriteMessage(message, maxMessageSize, bufferManager, messageOffset);
#endif
        }

        public override void WriteMessage(Message message, Stream stream)
        {
#if Encrypt
            if (RSAProvider.NoRSA())
            {
                this._inner.WriteMessage(message, stream);
            }

            MemoryStream ms = new MemoryStream();
            this._inner.WriteMessage(message, ms);

            byte[] buffer = ms.ToArray();
            byte[] key = null;
            byte[] iv = null;

            RSACryptoServiceProvider rsa = RSAProvider.UseRSA();

            if (null == rsa)
            {
                buffer = AESEncrypt.Encrypt(buffer, 0, buffer.Length);
            }
            else
            {
                byte[] keyData;
                byte[] ivData;
                BuildAESKey(out keyData, out ivData);

                buffer = AESEncrypt.Encrypt(buffer, 0, buffer.Length, keyData, ivData);

                key = rsa.Encrypt(keyData, true);
                iv = rsa.Encrypt(ivData, true);
            }

            if (null == key)
            {
                stream.WriteByte(0);
                stream.WriteByte(0);
                stream.Write(buffer, 0, buffer.Length);
            }
            else
            {
                stream.Write(BitConverter.GetBytes((ushort)key.Length), 0, 2);
                stream.Write(key, 0, key.Length);
                stream.Write(BitConverter.GetBytes((ushort)iv.Length), 0, 2);
                stream.Write(iv, 0, iv.Length);
                stream.Write(buffer, 0, buffer.Length);
            }

#else
            this._inner.WriteMessage(message, stream);
#endif
        }
    }
}
