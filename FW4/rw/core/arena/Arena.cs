using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FW4.rw;

namespace FW4.rw.core.arena
{
    public class Arena
    {

        //HEADER
        public struct ArenaFileHeaderMagicNumber
        {
            public byte[] prefix { get; set; }
            public byte[] body { get; set; }
            public byte[] suffix { get; set; }
        }

        public struct ArenaFileHeader
        {
            public ArenaFileHeaderMagicNumber magicNumber { get; set; }
            public bool isBigEndian { get; set; }
            public byte pointerSizeInBits { get; set; }
            public byte pointerAlignment { get; set; }
            public byte unused { get; set; }
            public byte[] majorVersion { get; set; }
            public byte[] minorVersion { get; set; }
            public uint buildNo { get; set; }
        }

        public struct ArenaDictEntry
        {
            public uint ptr { get; set; }
            public uint reloc { get; set; }
            public uint size { get; set; }
            public uint align { get; set; }
            public uint typeIndex { get; set; }
            public RWObjectTypes type { get; set; }
        };

        public ArenaFileHeader fileHeader { get; set; }
        public uint id { get; set; }
        public uint numEntries { get; set; }
        public uint numUsed { get; set; }
        public uint alignment { get; set; }
        public uint virt { get; set; }
        public uint dictStart { get; set; }
        public uint sections { get; set; }
        public uint Base { get; set; }
        public uint m_unfixContext { get; set; }
        public uint m_fixContext { get; set; }
        public ResourceDescriptor m_resourceDescriptor { get; set; }
        public ResourceDescriptor m_resourcesUsed { get; set; }
        public TargetResource m_resource { get; set; }
        public uint m_arenaGroup { get; set; }

        //SECTIONS
        public ArenaSectionManifest Manifest { get; set; }
        public ArenaSectionTypes Types { get; set; }
        public ArenaSectionExternalArenas ExternalArenas { get; set; }
        public ArenaSectionSubreferences Subreferences { get; set; }
        public ArenaSectionAtoms Atoms { get; set; }

        //Entries
        public ArrayList DictEntries = new ArrayList();

        public ArrayList ArenaEntries = new ArrayList();

        public Arena()
        {
            m_resourceDescriptor = new ResourceDescriptor(0,4);
        }
    }
}
