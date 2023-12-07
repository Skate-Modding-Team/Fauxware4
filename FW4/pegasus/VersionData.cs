using FW4.rw.core;

namespace FW4.pegasus
{
    /**
    *<summary>Gives the version information for pegasus.</summary>
    */
    public class VersionData : RWObject
    {
        RWObjectTypes type = rw.core.RWObjectTypes.RWOBJECTTYPE_VERSIONDATA;
        uint size = 8;
            
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

        public override byte[] Serialize(bool BigEndian)
        {
          byte[] data = new byte[8];
          byte[] versBytes = BitConverter.GetBytes(version);
          byte[] revBytes = BitConverter.GetBytes(revision);
          if (BigEndian)
          {
            versBytes = Array.Reverse(versBytes);
            revBytes = Array.Reverse(revBytes);
          }
          Array.Copy(versBytes, data, sizeof(uint));
          Array.Copy(revBytes, 0, data, sizeof(uint), sizeof(uint));
        }

        public override void Deserialize(byte[] data, bool BigEndian)
        {
          versBytes = new byte[sizeof(uint)];
          Array.Copy(data, 0, versBytes, 0, sizeof(uint));
          version = ReadUInt32(versBytes, BigEndian);
          revBytes = new byte[sizeof(uint)];
          Array.Copy(data, 4, revBytes, 0, sizeof(uint));
          revision = ReadUInt32(revBytes, BigEndian);
        }
    }
}
