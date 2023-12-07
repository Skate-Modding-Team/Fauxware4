using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW4.pegasus;
using FW4.rw.core;
using FW4.Xenon;
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
                    unknown = ReadUInt32(stream.ReadBytes(4), endianess),
                    m_uiGuid = ReadInt64(stream.ReadBytes(8), endianess),
                    m_Type = (RWObjectType)ReadInt32(stream.ReadBytes(4), false),
                    m_pObject = ReadUInt32(stream.ReadBytes(4), endianess)
                };
                TOC.TableEntries.Add(entry);
            }

            stream.BaseStream.Seek(initPos + TOC.m_pTypeMap, SeekOrigin.Begin);
            for (int i = 0; i < TOC.m_uiTypeCount; i++)
            {
                TableOfContents.TypeMap type = new TableOfContents.TypeMap()
                {
                    Type = (RWObjectType)ReadInt32(stream.ReadBytes(4), false),
                    Index = ReadUInt32(stream.ReadBytes(4), endianess)
                };
                TOC.TypeMapEntries.Add(type);
            }
            return TOC;
        }

        public static D3DBaseTexture DeserializeD3DBaseTexture(BinaryReader stream, bool endianess)
        {
          
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
              
                case RWObjectTypes.RWOBJECTTYPE_TABLEOFCONTENTS:
                    TableOfContents TOC = (TableOfContents)POBJ;
                    long initPos = stream.BaseStream.Position;
                    stream.Write(UIntToBytes(TOC.m_uiItemsCount, endianess));
                    stream.Write(UIntToBytes(TOC.m_pArray, endianess));
                    stream.Write(UIntToBytes(TOC.m_pNames, endianess));
                    stream.Write(UIntToBytes(TOC.m_uiTypeCount, endianess));
                    stream.Write(UIntToBytes(TOC.m_pTypeMap, endianess));
                    for(int i = 0; i < (TOC.m_pArray+initPos)-stream.BaseStream.Position; i++)
                      stream.Write((byte)0x00);
                    foreach(TableOfContents.TOCEntry entry in TOC.TableEntries)
                    {
                        stream.Write(UIntToBytes(entry.m_Name, endianess));
                        stream.Write(UIntToBytes(entry.unknown, endianess));
                        stream.Write(Int64ToBytes(entry.m_uiGuid, endianess));
                        stream.Write(Int32ToBytes((int)entry.m_Type);
                        stream.Write(UIntToBytes(entry.m_pObject, endianess));
                    }
                    for(int i = 0; i < (TOC.m_pTypeMap+initPos)-stream.BaseStream.Position; i++)
                      stream.Write((byte)0x00);
                    foreach(TableOfContents.TypeMap type in TOC.TypeMapEntries)
                    {
                        stream.Write(Int32ToBytes((int)type.Type, endianess));
                        stream.Write(UIntToBytes(type.Index, endianess));
                    }
                    break;

              
            }
        }
    }
}
