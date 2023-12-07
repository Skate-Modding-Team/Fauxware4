using FW4.rw.core;

namespace FW4.pegasus
{
    public class TableOfContents : PegasusObject
    {
        RWObjectTypes type = RWObjectTypes.RWOBJECTTYPE_TABLEOFCONTENTS;

        public struct TOCEntry
        {
            public uint m_Name;
            public int unknown;
            public long m_uiGuid;
            public RWObjectTypes m_Type;
            public uint m_pObject;
        }

        public struct TypeMap
        {
            public RWObjectTypes Type;
            public uint Index;
        }

        public List<TOCEntry> TableEntries = new List<TOCEntry>();
        public List<TypeMap> TypeMapEntries = new List<TypeMap>();

        public uint m_uiItemsCount;
        public uint m_pArray;
        public uint m_pNames;
        public uint m_uiTypeCount;
        public uint m_pTypeMap;
    }
}
