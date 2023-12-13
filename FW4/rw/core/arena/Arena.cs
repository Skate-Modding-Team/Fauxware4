using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FW4.RW;

namespace FW4.RW.Core.Arena
{
    /*
     * <summary>The RenderWare arena file is a container for serialized objects, and is used for storing the main data for the game.</summary>
     */
    public class Arena
    {

        //HEADER
        private struct ArenaFileHeaderMagicNumber
        {
            private readonly static byte[] prefix = { 0x89, 0x52, 0x57, 0x34 }; //%RW4
            private EPlatform body { get; set; }
            private readonly static byte[] suffix = { 0x0D, 0x0A, 0x1A, 0x0A };
        }

        public struct ArenaFileHeader
        {
            private ArenaFileHeaderMagicNumber magicNumber { get; }
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
            public ERWObjectTypes type { get; set; }
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
        public uint m_ArenaGroup { get; set; }

        //SECTIONS
        public ArenaSectionManifest Manifest { get; set; }
        public ArenaSectionTypes Types { get; set; }
        public ArenaSectionExternalArenas ExternalArenas { get; set; }
        public ArenaSectionSubreferences Subreferences { get; set; }
        public ArenaSectionAtoms Atoms { get; set; }

        //Entries
        public List<ArenaDictEntry> DictEntries = new List<ArenaDictEntry>();

        public ArrayList ArenaEntries = new ArrayList();

        public Arena()
        {
            m_resourceDescriptor = new ResourceDescriptor(0,4);
        }
    }
}
