using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW4
{
    public static class BinaryHelper
    {
        public static short ReadInt16(byte[] stream, bool BigEndian)
        {
            if (BigEndian)
            {
                return BinaryPrimitives.ReadInt16BigEndian(stream);
            }
            else
            {
                return BinaryPrimitives.ReadInt16LittleEndian(stream);
            }
        }

        public static ushort ReadUInt16(byte[] stream, bool BigEndian)
        {
            if (BigEndian)
            {
                return BinaryPrimitives.ReadUInt16BigEndian(stream);
            }
            else
            {
                return BinaryPrimitives.ReadUInt16LittleEndian(stream);
            }
        }

        public static int ReadInt32(byte[] stream, bool BigEndian)
        {
            if (BigEndian)
            {
                return BinaryPrimitives.ReadInt32BigEndian(stream);
            }
            else
            {
                return BinaryPrimitives.ReadInt32LittleEndian(stream);
            }
        }

        public static uint ReadUInt32(byte[] stream, bool BigEndian)
        {
            if (BigEndian)
            {
                return BinaryPrimitives.ReadUInt32BigEndian(stream);
            }
            else
            {
                return BinaryPrimitives.ReadUInt32LittleEndian(stream);
            }
        }

        public static float ReadFloat(byte[] stream, bool BigEndian)
        {
            if (BigEndian)
            {
                return BinaryPrimitives.ReadSingleBigEndian(stream);
            }
            else
            {
                return BinaryPrimitives.ReadSingleLittleEndian(stream);
            }
        }

        public static long ReadInt64(byte[] stream, bool BigEndian)
        {
            if (BigEndian)
            {
                return BinaryPrimitives.ReadInt64BigEndian(stream);
            }
            else
            {
                return BinaryPrimitives.ReadInt64LittleEndian(stream);
            }
        }

        public static byte[] UIntToBytes(uint val, bool BigEndian)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            if (BigEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        public static byte[] IntToBytes(int val, bool BigEndian)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            if (BigEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        public static byte[] Int64ToBytes(long val, bool BigEndian)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            if (BigEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        public static byte[] UInt64ToBytes(ulong val, bool BigEndian)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            if (BigEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        public static byte[] FloatToBytes(float val, bool BigEndian)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            if (BigEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        public static byte[] Reverse(byte[] data)
        {
            Array.Reverse(data);
            return data;
        }
    }
}
