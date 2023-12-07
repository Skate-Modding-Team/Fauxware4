using static FW4.Serialization.BinaryHelper;

namespace FW4.rw.core
{
    public class RWObject
    {
        public RWObjectTypes type;
        public uint size;

        public byte[] Serialize(FW4.pegasus.VersionData versionData)
        {
        
        }

        public void Deserialize(FW4.pegasus.VersionData versionData, byte[] data)
        {

        }
    }
}
