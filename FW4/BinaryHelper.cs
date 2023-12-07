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
        public static Int16 ReadInt16(byte[] stream, bool BigEndian)
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

        public static UInt16 ReadUInt16(byte[] stream, bool BigEndian)
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

        public static Int32 ReadInt32(byte[] stream, bool BigEndian)
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

        public static UInt32 ReadUInt32(byte[] stream, bool BigEndian)
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

        public static byte[] UIntToBytes(uint val, bool BigEndian)
        {
          byte[] bytes = BitConverter.GetBytes(val);
          if(BigEndian)
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
    }
}
