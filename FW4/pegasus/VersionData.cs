using FW4.RW.Core;
using FW4.Serialization;
using static FW4.BinaryHelper;

namespace FW4.Pegasus
{
    /**
    *<summary>Gives the version information for Pegasus.</summary>
    */
    public class VersionData : IRWObject
    {  
        public uint version { get; set; }
        public uint revision { get; set; }

        /* Skate 1.19.00
         *  Version = 19
         *  Revision = 0
         * 
         * Skate it 1.24.00
         *  Version = 24
         *  Revision = 0
         * 
         * Skate 2 1.25.02
         *  Version = 25
         *  Revision = 2
         *  
         * Skate 3 1.25.13
         *  Version = 25
         *  Revision = 13
         */
        
        public byte[] Serialize(bool BigEndian, Pegasus.VersionData vdata)
        {
            var ms = new MemoryStream();
            byte[] versBytes = BitConverter.GetBytes(version);
            byte[] revBytes = BitConverter.GetBytes(revision);
            if (BigEndian)
            {
                Array.Reverse(versBytes);
                Array.Reverse(revBytes);
            }

            ms.Write(versBytes);
            ms.Write(revBytes);

            return ms.ToArray();
        }
        
        public void Deserialize(byte[] bytes, bool BigEndian, Pegasus.VersionData vdata)
        {
            var ms = new MemoryStream(bytes);
            using(var reader = new BinaryReader(ms))
            {
                version = ReadUInt32(reader.ReadBytes(4), BigEndian);
                revision = ReadUInt32(reader.ReadBytes(4), BigEndian);
            }
        }
        
    }
}
