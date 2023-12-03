using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW4.pegasus;
using static FW4.BinaryHelper;

namespace FW4
{
    public static class RWObjectSerialize
    {
        public static VersionData DeserializeVersionData(BinaryReader stream, bool endianess)
        {
            VersionData vdata = new VersionData();
            vdata.version = ReadUInt32(stream.ReadBytes(4), endianess);
            vdata.revision = ReadUInt32(stream.ReadBytes(4), endianess);
            return vdata;
        }
    }
}
