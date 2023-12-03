﻿using FW4.rw;
using FW4.rw.core;
using FW4.rw.core.arena;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static FW4.BinaryHelper;
using static FW4.RWObjectSerialize;

namespace FW4
{
    /**
     * <summary>A class used to Serialize and Deserialize FW4 objects</summary>
     */
    public static class ArenaSerialize
    {
        //Private Helpers

        private enum Platform
        {
            PS3 = 3371888,
            XB2 = 3302008,
            WII = 7759218,
            W32 = 3289975
        }

        /**
         * <summary>Deserializes an arena file into an Arena object.</summary>
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
                throw new FileNotFoundException("The specified arena file does not exist.", ArenaFilePath);
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
            DeserializedArena.m_resourceDescriptor = new rw.ResourceDescriptor(ReadUInt32(ArenaReader.ReadBytes(4), BE), ReadUInt32(ArenaReader.ReadBytes(4), BE));
            
            for (int i = 1; i < 5; i++)
            {
                DeserializedArena.m_resourceDescriptor.m_baseResourceDescriptors[i].m_size = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                DeserializedArena.m_resourceDescriptor.m_baseResourceDescriptors[i].m_alignment = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            }

            switch (platform)
            {
                case Platform.XB2:
                    DeserializedArena.m_resourcesUsed = new rw.ResourceDescriptor(ReadUInt32(ArenaReader.ReadBytes(4), BE), ReadUInt32(ArenaReader.ReadBytes(4), BE));
                    for (int i = 1; i < 5; i++)
                    {
                        DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors[i].m_size = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                        DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors[i].m_alignment = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    }
                    DeserializedArena.m_resource = new rw.TargetResource();
                    DeserializedArena.m_resource.m_baseResources = new uint[4];
                    for (int i = 0; i < 4; i++)
                    {
                        DeserializedArena.m_resource.m_baseResources[i] = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    }
                    DeserializedArena.m_arenaGroup = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    break;

                case Platform.PS3:
                    DeserializedArena.m_resourcesUsed = new rw.ResourceDescriptor(0,0);
                    DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors = new rw.BaseResourceDescriptor[7]
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
                    DeserializedArena.m_resourcesUsed = new rw.ResourceDescriptor(ReadUInt32(ArenaReader.ReadBytes(4), BE), ReadUInt32(ArenaReader.ReadBytes(4), BE));
                    for (int i = 1; i < 5; i++)
                    {
                        DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors[i].m_size = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                        DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors[i].m_alignment = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    }
                    DeserializedArena.m_arenaGroup = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    break;
                case Platform.W32:
                    DeserializedArena.m_resourcesUsed = new rw.ResourceDescriptor(ReadUInt32(ArenaReader.ReadBytes(4), BE), ReadUInt32(ArenaReader.ReadBytes(4), BE));
                    for (int i = 1; i < 4; i++)
                    {
                        DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors[i].m_size = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                        DeserializedArena.m_resourcesUsed.m_baseResourceDescriptors[i].m_alignment = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    }
                    DeserializedArena.m_resource = new rw.TargetResource();
                    DeserializedArena.m_resource.m_baseResources = new uint[4];
                    for (int i = 0; i < 5; i++)
                    {
                        DeserializedArena.m_resource.m_baseResources[i] = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    }
                    DeserializedArena.m_arenaGroup = ReadUInt32(ArenaReader.ReadBytes(4), BE);
                    break;
            }

            //ARENA SECTIONS
            ArenaReader.BaseStream.Seek(DeserializedArena.sections, SeekOrigin.Begin);

            ArenaSectionManifest Manifest = new ArenaSectionManifest();
            RWObjectTypes type = (RWObjectTypes)ArenaReader.ReadInt32();
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
            Types.SectionType = (RWObjectTypes)ArenaReader.ReadInt32();
            Types.NumEntries = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Types.dictOffset = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Types.dict = new RWObjectTypes[Types.NumEntries];
            ArenaReader.BaseStream.Seek(DeserializedArena.sections + Manifest.dict[0] + Types.dictOffset, SeekOrigin.Begin);
            
            for (int i = 0; i < Types.NumEntries; i++)
            {
                Types.dict[i] = (RWObjectTypes)ReadUInt32(ArenaReader.ReadBytes(4), BE);
            }

            DeserializedArena.Types = Types;

            ArenaReader.BaseStream.Seek(DeserializedArena.sections + Manifest.dict[1], SeekOrigin.Begin);

            ArenaSectionExternalArenas ExternalArenas = new ArenaSectionExternalArenas();
            ExternalArenas.SectionType = (RWObjectTypes)ArenaReader.ReadInt32();
            ExternalArenas.NumEntries = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            ExternalArenas.dictOffset = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            ExternalArenas.dict = new uint[ExternalArenas.NumEntries];
            ArenaReader.BaseStream.Seek(DeserializedArena.sections + Manifest.dict[1] + ExternalArenas.dictOffset, SeekOrigin.Begin);

            for (int i = 0; i < ExternalArenas.NumEntries; i++)
            {
                ExternalArenas.dict[i] = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            }

            DeserializedArena.ExternalArenas = ExternalArenas;

            ArenaReader.BaseStream.Seek(DeserializedArena.sections + Manifest.dict[2], SeekOrigin.Begin);

            ArenaSectionSubreferences Subreferences = new ArenaSectionSubreferences();
            Subreferences.SectionType = (RWObjectTypes)ArenaReader.ReadInt32();
            Subreferences.NumEntries = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Subreferences.m_dictAfterRefix = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Subreferences.m_recordsAfterRefix = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Subreferences.dict = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Subreferences.records = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Subreferences.numUsed = ReadUInt32(ArenaReader.ReadBytes(4), BE);
            Subreferences.RecordEntries = new ArenaSectionSubreferences.Record[Subreferences.NumEntries];
            ArenaReader.BaseStream.Seek(Subreferences.m_recordsAfterRefix, SeekOrigin.Begin);
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
            Atoms.SectionType = (RWObjectTypes)ReadUInt32(ArenaReader.ReadBytes(4), BE);
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
                Entry.type = (RWObjectTypes)ReadUInt32(ArenaReader.ReadBytes(4), BE);
                DeserializedArena.DictEntries.Add(Entry);
            }

            //ARENA ENTRIES
            foreach(Arena.ArenaDictEntry entry in DeserializedArena.DictEntries)
            {
                ArenaReader.BaseStream.Seek(entry.ptr,SeekOrigin.Begin);
                switch(entry.type) 
                {
                    case RWObjectTypes.RWOBJECTTYPE_VERSIONDATA:
                        DeserializedArena.ArenaEntries.Add(DeserializeVersionData(ArenaReader, BE));
                        break;
                    case RWObjectTypes.RWOBJECTTYPE_BASERESOURCE:
                        ArenaReader.BaseStream.Seek(DeserializedArena.m_resourceDescriptor.m_baseResourceDescriptors[0].m_size + entry.ptr, SeekOrigin.Begin);
                        DeserializedArena.ArenaEntries.Add(ArenaReader.ReadBytes((int)entry.size));
                        break;
                }
            }

            return DeserializedArena;
        }

        public static void SerializeArena(Arena arena, String FilePath)
        {
            //implement me :)
        }
    }
}