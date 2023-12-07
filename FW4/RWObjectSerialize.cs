using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW4.pegasus;
using FW4.rw.core;
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

        public static TableOfContents DeserializeTOC(BinaryReader stream, bool endianess)
        {
            long initPos = stream.BaseStream.Position;

            TableOfContents TOC = new TableOfContents();
            TOC.m_uiItemsCount = ReadUInt32(stream.ReadBytes(4), endianess);
            TOC.m_pArray = ReadUInt32(stream.ReadBytes(4), endianess);
            TOC.m_pNames = ReadUInt32(stream.ReadBytes(4), endianess);
            TOC.m_uiTypeCount = ReadUInt32(stream.ReadBytes(4), endianess);
            TOC.m_pTypeMap = ReadUInt32(stream.ReadBytes(4), endianess);

            stream.BaseStream.Seek(initPos + TOC.m_pArray, SeekOrigin.Begin);
            for (int i = 0; i < TOC.m_uiItemsCount; i++)
            {
                TableOfContents.TOCEntry entry = new TableOfContents.TOCEntry()
                {
                    m_Name = ReadUInt32(stream.ReadBytes(4), endianess),
                    unknown;
        public long m_uiGuid;
        public RWObjectTypes m_Type;
        public uint m_pObject;
    };
            }
            return TOC;
        }

        public static void SerializePegasusObject(BinaryWriter stream, bool endianess, PegasusObject POBJ)
        {
            switch(POBJ.type)
            {
                case RWObjectTypes.RWOBJECTTYPE_VERSIONDATA:
                    VersionData vdata = (VersionData)POBJ;
                    stream.Write(UIntToBytes(vdata.version, endianess));
                    stream.Write(UIntToBytes(vdata.revision, endianess));
                    break;
            }
        }
    }
}
