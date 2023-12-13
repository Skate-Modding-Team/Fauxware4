using FW4.RW.Core;
using System.Collections;
using System.IO;
using System.Xml.Linq;
using static FW4.BinaryHelper;

namespace FW4.Pegasus
{

    public class TableOfContents : IRWObject
    {
        ERWObjectTypes type = ERWObjectTypes.RWOBJECTTYPE_TABLEOFCONTENTS;

        public struct TOCEntry
        {
            public uint m_Name;
            public int unknown;
            public long m_uiGuid;
            public ERWObjectTypes m_Type;
            public uint m_pObject;
        }

        public struct TypeMap
        {
            public ERWObjectTypes Type;
            public uint Index;
        }

        public List<TOCEntry> TableEntries = new List<TOCEntry>();
        public List<TypeMap> TypeMapEntries = new List<TypeMap>();

        public uint m_uiItemsCount;
        public uint m_pArray;
        public uint m_pNames;
        public uint m_uiTypeCount;
        public uint m_pTypeMap;

        public byte[] Serialize(bool BigEndian)
        {
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter stream = new BinaryWriter(m))
                {
                    stream.Write(UIntToBytes(m_uiItemsCount, BigEndian));
                    stream.Write(UIntToBytes(m_pArray, BigEndian));
                    stream.Write(UIntToBytes(m_pNames, BigEndian));
                    stream.Write(UIntToBytes(m_uiTypeCount, BigEndian));
                    stream.Write(UIntToBytes(m_pTypeMap, BigEndian));
                    for (int i = 0; i < m_pArray - stream.BaseStream.Position; i++)
                        stream.Write((byte)0x00);
                    foreach (TOCEntry entry in TableEntries)
                    {
                        stream.Write(UIntToBytes(entry.m_Name, BigEndian));
                        stream.Write(IntToBytes(entry.unknown, BigEndian));
                        stream.Write(Int64ToBytes(entry.m_uiGuid, BigEndian));
                        stream.Write((int)entry.m_Type);
                        stream.Write(UIntToBytes(entry.m_pObject, BigEndian));
                    }
                    for (int i = 0; i < m_pTypeMap - stream.BaseStream.Position; i++)
                        stream.Write((byte)0x00);
                    foreach (TypeMap type in TypeMapEntries)
                    {
                        stream.Write((int)type.Type);
                        stream.Write(UIntToBytes(type.Index, BigEndian));
                    }
                }
                return m.ToArray();
            }
        }
    }
}
