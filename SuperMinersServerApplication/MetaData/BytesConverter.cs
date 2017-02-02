using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    public static class BytesConverter
    {
        public const int HEADOFNULL = -999;
        public const int HEADOFENTITY = 999;

        #region Byte Array

        /// <summary>
        /// 前4位为数组长度
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static int ComputeBytesLengthOfBytesArray(byte[] byteArray)
        {
            int length = 4;
            if (byteArray != null)
            {
                length += byteArray.Length;
            }

            return length;
        }

        /// <summary>
        /// 前4位为数组长度
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static byte[] ConvertByteArrayToBytes(byte[] byteArray)
        {
            if (byteArray == null)
            {
                return BitConverter.GetBytes(-1);
            }

            List<byte> buffer = new List<byte>();
            buffer.AddRange(BitConverter.GetBytes(byteArray.Length));
            buffer.AddRange(byteArray);

            return buffer.ToArray();
        }

        public static byte[] GetByteArrayFromBytes(byte[] source, int inIndex, out int outIndex)
        {
            outIndex = inIndex;

            int arrayLength = BitConverter.ToInt32(source, outIndex);
            outIndex += 4;
            if (arrayLength == -1)
            {
                return null;
            }

            byte[] value = new byte[arrayLength];
            Array.Copy(source, outIndex, value, 0, arrayLength);

            outIndex += arrayLength;

            return value;
        }

        #endregion

    }
}
