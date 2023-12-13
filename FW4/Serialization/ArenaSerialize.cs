using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FW4.RW;
using FW4.RW.Core;
using FW4.RW.Core.Arena;
using FW4.Pegasus;
using static FW4.BinaryHelper;
using static FW4.RWObjectSerialize;
using FW4.Serialization;

namespace FW4
{
    /**
     * <summary>A class used to Serialize and Deserialize FW4 objects</summary>
     */
    public static class ArenaSerialize
    {

        public enum Platform
        {
            PS3 = 3371888,
            XB2 = 3302008,
            WII = 7759218,
            W32 = 3289975
        }

        /**
         * <summary>Deserializes an Arena file into an Arena object.</summary>
         */
        public static Arena DeserializeArenaFile(string ArenaFilePath)
        {
            FileStream ArenaStream;
            try
            {
                ArenaStream = File.OpenRead(ArenaFilePath);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("The specified Arena file does not exist.", ArenaFilePath);
            }

            BinaryReader ArenaReader = new BinaryReader(ArenaStream);
            Arena DeserializedArena = new Arena();

            //ARENA HEADER

            Arena.ArenaFileHeaderMagicNumber MagicNumber = new Arena.ArenaFileHeaderMagicNumber
            {
                prefix = ArenaReader.ReadBytes(4),
                body = ArenaReader.ReadBytes(4),
                suffix = ArenaReader.ReadBytes(4),
            };

            Platform platform = (Platform)BitConverter.ToInt32 (MagicNumber.body);
            bool BE = ArenaReader.PeekChar()==1;

            Arena.ArenaFileHeader Header = new Arena.ArenaFileHeader
            {
                magicNumber = MagicNumber,
                isBigEndian = ArenaReader.ReadBoolean(),
                pointerSizeInBits = ArenaReader.ReadByte(),
                pointerAlignment = ArenaReader.ReadByte(),
                unused = ArenaReader.ReadByte(),
                majorVersion = ArenaReader.ReadBytes(4),
                minorVersion = ArenaReader.ReadBytes(4),
                buildNo = ReadUInt32(ArenaReader.ReadBytes(4), BE)
            };

            DeserializedArena.fileHeader = Header;
            DeserializedArena.id = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            DeserializedArena.numEntries = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            DeserializedArena.numUsed = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            DeserializedArena.alignment = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            DeserializedArena.virt = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            DeserializedArena.dictStart = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            DeserializedArena.sections = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            DeserializedArena.Base = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            DeserializedArena.m_unfixContext = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            DeserializedArena.m_fixContext = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            DeserializedArena.m_resourceDescriptor = new RW.ResourceDescriptor(ReadUInt32(ArenaReader.ReadBytes(4), BE), ReadUInt32(ArenaReader.ReadBytes(4), BE));
            
            for (int i = 1; i < 5; i++)
            {
                DeserializedArena.m_resourceDescriptor.m_baseResourceDescriptors[i].m_size = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                DeserializedArena.m_resourceDescriptor.m_baseResourceDescriptors[i].m_alignment = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            }

            switch (platform)
            {
                case Platform.XB2:
                    DeserializedArena.m_resourcesUsed = new RW.ResourceDescriptor(ReadUInt32(ArenaReader.ReadBytes(4), BE), ReadUInt32(ArenaReader.ReadBytes(4), BE));
                    for (int i = 1; i < 5; i++)
                    {
                        DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors[i].m_size = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                        DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors[i].m_alignment = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    }
                    DeserializedArena.m_resource = new RW.TargetResource();
                    DeserializedArena.m_resource.m_baseResources = new uint[5];
                    for (int i = 0; i < 5; i++)
                    {
                        DeserializedArena.m_resource.m_baseResources[i] = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    }
                    DeserializedArena.m_ArenaGroup = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    break;

                case Platform.PS3:
                    DeserializedArena.m_resourcesUsed = new RW.ResourceDescriptor(0,0);
                    DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors = new RW.BaseResourceDescriptor[7]
                    {
                        new BaseResourceDescriptor(0,0),
                        new BaseResourceDescriptor(0,0),
                        new BaseResourceDescriptor(0,0),
                        new BaseResourceDescriptor(0,0),
                        new BaseResourceDescriptor(0,0),
                        new BaseResourceDescriptor(0,0),
                        new BaseResourceDescriptor(0,0)
                    };
                    for (int i = 1; i < 7; i++)
                    {
                        DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors[i].m_size = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                        DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors[i].m_alignment = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    }
                    DeserializedArena.m_resource = new TargetResource();
                    DeserializedArena.m_resource.m_baseResources = new uint[7];
                    for (int i = 0; i < 7; i++)
                    {
                        DeserializedArena.m_resource.m_baseResources[i] = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    }
                    break;
                case Platform.WII:
                    DeserializedArena.m_resourcesUsed = new RW.ResourceDescriptor(ReadUInt32(ArenaReader.ReadBytes(4), BE), ReadUInt32(ArenaReader.ReadBytes(4), BE));
                    for (int i = 1; i < 5; i++)
                    {
                        DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors[i].m_size = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                        DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors[i].m_alignment = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    }
                    DeserializedArena.m_ArenaGroup = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    break;
                case Platform.W32:
                    DeserializedArena.m_resourcesUsed = new RW.ResourceDescriptor(ReadUInt32(ArenaReader.ReadBytes(4), BE), ReadUInt32(ArenaReader.ReadBytes(4), BE));
                    for (int i = 1; i < 4; i++)
                    {
                        DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors[i].m_size = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                        DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors[i].m_alignment = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    }
                    DeserializedArena.m_resource = new RW.TargetResource();
                    DeserializedArena.m_resource.m_baseResources = new uint[4];
                    for (int i = 0; i < 5; i++)
                    {
                        DeserializedArena.m_resource.m_baseResources[i] = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    }
                    DeserializedArena.m_ArenaGroup = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    break;
            }

            //ARENA SECTIONS
            ArenaReader.BaseStream.Seek(DeserializedArena.sections, SeekOrigin.Begin);

            ArenaSectionManifest Manifest = new ArenaSectionManifest();
            ERWObjectTypes type = (ERWObjectTypes)ArenaReader.ReadInt32();
            Manifest.SectionType = type;
            Manifest.NumEntries = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Manifest.dictOffset = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Manifest.dict = new uint[Manifest.NumEntries];
            ArenaReader.BaseStream.Seek(DeserializedArena.sections+Manifest.dictOffset, SeekOrigin.Begin);
            
            for (int i = 0; i < Manifest.NumEntries; i++)
            {
                Manifest.dict[i] = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            }

            DeserializedArena.Manifest = Manifest;

            ArenaReader.BaseStream.Seek(DeserializedArena.sections + Manifest.dict[0], SeekOrigin.Begin);

            ArenaSectionTypes Types = new ArenaSectionTypes();
            Types.SectionType = (ERWObjectTypes)ArenaReader.ReadInt32();
            Types.NumEntries = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Types.dictOffset = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Types.dict = new ERWObjectTypes[Types.NumEntries];
            ArenaReader.BaseStream.Seek(DeserializedArena.sections + Manifest.dict[0] + Types.dictOffset, SeekOrigin.Begin);
            
            for (int i = 0; i < Types.NumEntries; i++)
            {
                Types.dict[i] = (ERWObjectTypes)ArenaReader.ReadInt32();
            }

            DeserializedArena.Types = Types;

            ArenaReader.BaseStream.Seek(DeserializedArena.sections + Manifest.dict[1], SeekOrigin.Begin);

            ArenaSectionExternalArenas ExternalArenas = new ArenaSectionExternalArenas();
            ExternalArenas.SectionType = (ERWObjectTypes)ArenaReader.ReadInt32();
            ExternalArenas.NumEntries = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            ExternalArenas.dictOffset = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            ExternalArenas.dict = new uint[ExternalArenas.NumEntries*2];
            //ArenaReader.BaseStream.Seek(DeserializedArena.sections + Manifest.dict[1] + ExternalArenas.dictOffset-, SeekOrigin.Begin);

            for (int i = 0; i < ExternalArenas.NumEntries*2; i++)
            {
                ExternalArenas.dict[i] = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            }

            DeserializedArena.ExternalArenas = ExternalArenas;

            ArenaReader.BaseStream.Seek(DeserializedArena.sections + Manifest.dict[2], SeekOrigin.Begin);

            ArenaSectionSubreferences Subreferences = new ArenaSectionSubreferences();
            Subreferences.SectionType = (ERWObjectTypes)ArenaReader.ReadInt32();
            Subreferences.NumEntries = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Subreferences.m_dictAfterRefix = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Subreferences.m_recordsAfterRefix = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Subreferences.dict = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Subreferences.records = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Subreferences.numUsed = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Subreferences.RecordEntries = new ArenaSectionSubreferences.Record[Subreferences.NumEntries];
            ArenaReader.BaseStream.Seek(Subreferences.records, SeekOrigin.Begin);
            for(int i = 0; i < Subreferences.NumEntries; i++)
            {
                ArenaSectionSubreferences.Record record = new ArenaSectionSubreferences.Record();
                record.objectID = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                record.offset = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                Subreferences.RecordEntries[i] = record;
            }

            DeserializedArena.Subreferences = Subreferences;

            ArenaReader.BaseStream.Seek(DeserializedArena.sections + Manifest.dict[3], SeekOrigin.Begin);

            ArenaSectionAtoms Atoms = new ArenaSectionAtoms();
            Atoms.SectionType = (ERWObjectTypes)ArenaReader.ReadInt32();
            Atoms.NumEntries = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Atoms.atomTable = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            DeserializedArena.Atoms = Atoms;

            //ARENA DICTIONARY

            ArenaReader.BaseStream.Seek(DeserializedArena.dictStart, SeekOrigin.Begin);
            for(int i = 0; i < DeserializedArena.numEntries; i++)
            {
                Arena.ArenaDictEntry Entry = new Arena.ArenaDictEntry();
                Entry.ptr = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                Entry.reloc = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                Entry.size = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                Entry.align = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                Entry.typeIndex = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                Entry.type = (ERWObjectTypes)ReadUInt32(ArenaReader.ReadBytes(4), BE);
                DeserializedArena.DictEntries.Add(Entry);
            }

            //ARENA ENTRIES
            foreach(Arena.ArenaDictEntry entry in DeserializedArena.DictEntries)
            {
                ArenaReader.BaseStream.Seek(entry.ptr,SeekOrigin.Begin);
                switch(entry.type) 
                {
                    case ERWObjectTypes.RWOBJECTTYPE_VERSIONDATA:
                        VersionData vdata = new VersionData();
                        vdata.Deserialize(ArenaReader.ReadBytes(8), BE);
                        DeserializedArena.ArenaEntries.Add(vdata);
                        break;
                    case ERWObjectTypes.RWOBJECTTYPE_TABLEOFCONTENTS:
                        DeserializedArena.ArenaEntries.Add(DeserializeTOC(ArenaReader, BE));
                        break;
                    /*case RWObjectTypes.RWGOBJECTTYPE_TEXTURE:
                        if (platform == Platform.XB2)
                        {
                            RenderEngine.Xenon.D3DBaseTexture texture = new RenderEngine.Xenon.D3DBaseTexture();
                            //texture.Deserialize(ArenaReader.ReadBytes());
                            DeserializedArena.ArenaEntries.Add(texture);
                        }
                        break;*/
                    case ERWObjectTypes.RWOBJECTTYPE_BASERESOURCE:
                        ArenaReader.BaseStream.Seek(DeserializedArena.m_resourceDescriptor.m_baseResourceDescriptors[0].m_size + entry.ptr, SeekOrigin.Begin);
                        DeserializedArena.ArenaEntries.Add(ArenaReader.ReadBytes((int)entry.size));
                        break;
                }
            }

            return DeserializedArena;
        }
      
        /**
        * <summary>Serializes an Arena object into an Arena file.</summary>
        */
        public static void SerializeArena(Arena Arena, String FilePath)
        {
            //---------------------//
            //  Serialize Header   //
            //---------------------//
            Platform platform = (Platform)BitConverter.ToInt32(Arena.fileHeader.magicNumber.body);
            bool Endianess = Arena.fileHeader.isBigEndian;
            FileStream ArenaStream = File.Create(FilePath);
            BinaryWriter ArenaWriter = new BinaryWriter(ArenaStream);

            ArenaWriter.Write(Arena.fileHeader.magicNumber.prefix);
            ArenaWriter.Write(Arena.fileHeader.magicNumber.body);
            ArenaWriter.Write(Arena.fileHeader.magicNumber.suffix);
         
            ArenaWriter.Write(Arena.fileHeader.isBigEndian);
            ArenaWriter.Write(Arena.fileHeader.pointerSizeInBits);
            ArenaWriter.Write(Arena.fileHeader.pointerAlignment);
            ArenaWriter.Write(Arena.fileHeader.unused);
            ArenaWriter.Write(Arena.fileHeader.majorVersion);
            ArenaWriter.Write(Arena.fileHeader.minorVersion);
            ArenaWriter.Write(UIntToBytes(Arena.fileHeader.buildNo, Endianess));

            ArenaWriter.Write(UIntToBytes(Arena.id, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.numEntries, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.numUsed, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.alignment, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.virt, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.dictStart, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.sections, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.Base, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.m_unfixContext, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.m_fixContext, Endianess));

            foreach (BaseResourceDescriptor brd in Arena.m_resourceDescriptor.m_baseResourceDescriptors)
            {
                ArenaWriter.Write(UIntToBytes(brd.m_size, Endianess));
                ArenaWriter.Write(UIntToBytes(brd.m_alignment, Endianess));
            }

            foreach (BaseResourceDescriptor brd in Arena.m_resourcesUsed.m_baseResourceDescriptors)
            {
                ArenaWriter.Write(UIntToBytes(brd.m_size, Endianess));
                ArenaWriter.Write(UIntToBytes(brd.m_alignment, Endianess));
            }

            if(platform == Platform.XB2 || platform == Platform.PS3)
            {
                foreach (uint offset in Arena.m_resource.m_baseResources)
                {
                    ArenaWriter.Write(UIntToBytes(offset, Endianess));
                }
            }

            if(platform == Platform.XB2 || platform == Platform.WII)
            {
                ArenaWriter.Write(UIntToBytes(Arena.m_ArenaGroup, Endianess));
            }

            //---------------------//
            // Serialize Sections  //
            //---------------------//

            //Manifest
            ArenaWriter.Write(IntToBytes((int)Arena.Manifest.SectionType , false));
            ArenaWriter.Write(UIntToBytes(Arena.Manifest.NumEntries, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.Manifest.dictOffset, Endianess));
            for (int i = 0; i < Arena.Manifest.dictOffset-(ArenaWriter.BaseStream.Position - Arena.sections); i++)
            {
                ArenaWriter.Write((byte)0);
            }
            foreach(uint entry in Arena.Manifest.dict)
            {
                ArenaWriter.Write(UIntToBytes(entry, Endianess));
            }

            for (int i = 0; i < Arena.Manifest.dict[0] - (ArenaWriter.BaseStream.Position - Arena.sections); i++)
            {
                ArenaWriter.Write((byte)0);
            }

            //Types
            ArenaWriter.Write(IntToBytes((int)Arena.Types.SectionType, false));
            ArenaWriter.Write(UIntToBytes(Arena.Types.NumEntries, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.Types.dictOffset, Endianess));
            for (int i = 0; i < Arena.Types.dictOffset - (ArenaWriter.BaseStream.Position - Arena.sections); i++)
            {
                ArenaWriter.Write((byte)0);
            }
            foreach (ERWObjectTypes type in Arena.Types.dict)
            {
                ArenaWriter.Write(IntToBytes((int)type, false));
            }

            for (int i = 0; i < Arena.Manifest.dict[1] - (ArenaWriter.BaseStream.Position - Arena.sections); i++)
            {
                ArenaWriter.Write((byte)0);
            }

            //External Arenas
            ArenaWriter.Write(IntToBytes((int)Arena.ExternalArenas.SectionType, false));
            ArenaWriter.Write(UIntToBytes(Arena.ExternalArenas.NumEntries, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.ExternalArenas.dictOffset, Endianess));
            foreach (uint data in Arena.ExternalArenas.dict)
            {
                ArenaWriter.Write(UIntToBytes(data, Endianess));
            }

            for (int i = 0; i < Arena.Manifest.dict[2] - (ArenaWriter.BaseStream.Position - Arena.sections); i++)
            {
                ArenaWriter.Write((byte)0);
            }

            //Subreferences
            ArenaWriter.Write(IntToBytes((int)Arena.Subreferences.SectionType, false));
            ArenaWriter.Write(UIntToBytes(Arena.Subreferences.NumEntries, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.Subreferences.m_dictAfterRefix, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.Subreferences.m_recordsAfterRefix, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.Subreferences.dict, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.Subreferences.records, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.Subreferences.numUsed, Endianess));

            for (int i = 0; i < Arena.Manifest.dict[3] - (ArenaWriter.BaseStream.Position - Arena.sections); i++)
            {
                ArenaWriter.Write((byte)0);
            }

            ArenaWriter.Write(IntToBytes((int)Arena.Atoms.SectionType, false));
            ArenaWriter.Write(UIntToBytes(Arena.Atoms.NumEntries, Endianess));
            ArenaWriter.Write(UIntToBytes(Arena.Atoms.atomTable, Endianess));

            if (ArenaWriter.BaseStream.Position % 16 != 0)
            {
                for (int i = 0; i < 16 - ArenaWriter.BaseStream.Position % 16; i++)
                {
                    ArenaWriter.Write((byte)0);
                }
            }

            //---------------------//
            //  Serialize Entries  //
            //---------------------//
            RWSerializer ser = new RWSerializer();
            for(int i = 0; i < Arena.ArenaEntries.Count; i++)
            {
                if (Arena.DictEntries[i].type != ERWObjectTypes.RWOBJECTTYPE_BASERESOURCE)
                {  
                  switch(Arena.DictEntries[i].type)
                  {
                        case ERWObjectTypes.RWOBJECTTYPE_VERSIONDATA:
                            VersionData vdata = (VersionData)Arena.ArenaEntries[i];
                            ArenaWriter.Write(ser.Serialize(vdata, Endianess));
                            break;
                        case ERWObjectTypes.RWOBJECTTYPE_TABLEOFCONTENTS:
                            TableOfContents TOC = (TableOfContents)Arena.ArenaEntries[i];
                            ArenaWriter.Write(TOC.Serialize(Endianess));
                            break;
                    }
                }
                if (i < Arena.ArenaEntries.Count - 1)
                {
                    uint nextOff = 0;
                    for (int j = i + 1; j < Arena.ArenaEntries.Count; i++)
                    {
                        if (Arena.DictEntries[i].type != ERWObjectTypes.RWOBJECTTYPE_BASERESOURCE)
                            nextOff = Arena.DictEntries[i].ptr;
                    }
                }
            }

            //---------------------//
            //  Serialize Dict.    //
            //---------------------//
            for (int i = 0; i < Arena.dictStart - ArenaWriter.BaseStream.Position; i++)
            {
                ArenaWriter.Write((byte)0);
            }

            foreach (Arena.ArenaDictEntry entry in Arena.DictEntries)
            {
                ArenaWriter.Write(UIntToBytes(entry.ptr, Endianess));
                ArenaWriter.Write(UIntToBytes(entry.reloc, Endianess));
                ArenaWriter.Write(UIntToBytes(entry.size, Endianess));
                ArenaWriter.Write(UIntToBytes(entry.align, Endianess));
                ArenaWriter.Write(UIntToBytes(entry.typeIndex, Endianess));
                ArenaWriter.Write(IntToBytes((int)entry.type, Endianess));
            }

            //---------------------//
            //  Serialize Subrefs  //
            //---------------------//
            for (int i = 0; i < Arena.Subreferences.records - ArenaWriter.BaseStream.Position; i++)
            {
                ArenaWriter.Write((byte)0);
            }

            foreach (ArenaSectionSubreferences.Record rec in Arena.Subreferences.RecordEntries)
            {
                ArenaWriter.Write(UIntToBytes(rec.objectID, Endianess));
                ArenaWriter.Write(UIntToBytes(rec.offset, Endianess));
            }

            //---------------------//
            //  Serialize Resrcs   //
            //---------------------//
            for (int i = 0; i < Arena.m_resourceDescriptor.m_baseResourceDescriptors[0].m_size - ArenaWriter.BaseStream.Position; i++)
            {
                ArenaWriter.Write((byte)0);
            }

            if (Arena.m_resourceDescriptor.m_baseResourceDescriptors[1].m_size > 0)
            {
                foreach (byte[] resource in Arena.ArenaEntries)
                {
                    ArenaWriter.Write(resource);
                }
            }
        }
    }
}
