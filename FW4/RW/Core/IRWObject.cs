namespace FW4.RW.Core
{

    public interface IRWObject
    {
        public byte[] Serialize(bool BigEndian, Pegasus.VersionData vdata);
        public void Deserialize(byte[] bytes, bool BigEndian, Pegasus.VersionData vdata);
    }
}
