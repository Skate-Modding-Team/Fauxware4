using FW4;
using FW4.pegasus;
using FW4.rw;
using FW4.rw.core;
using FW4.rw.core.arena;
using System.Collections;
using static FW4.ArenaSerialize;

namespace K8
{
  public static class AssetConvert
  {
    /**
     * <summary>Converts a presentation stream arena file to the version and platform specified.</summary>
     */
    public static void ConvertPresArenaFile(string arenaPath, VersionData versData, Platform platform)
    {
      //get arena from file
      Arena arena = DeserializeArenaFile(arenaPath);
      //convert resource info in header
      Platform srcplatform = (Platform)BitConverter.ToInt32(arena.fileHeader.magicNumber.body);
      if(srcplatform != platform)
      {
        switch(platform)
        {
          case Platform.XB2:
            arena.m_resource = new TargetResource();
            break;
        }
      }
      //change platform bytes in header
      Arena.ArenaFileHeader newHead = arena.fileHeader;
      Arena.ArenaFileHeaderMagicNumber newMagic = arena.fileHeader.magicNumber;
            newMagic.body = BitConverter.GetBytes((int)Platform.XB2);
            newHead.magicNumber = newMagic;
            arena.fileHeader = newHead;
      //change the version data object
      VersionData srcversData = (VersionData)arena.ArenaEntries[0];
      arena.ArenaEntries[0] = versData;
      //remove non-Presentation objects from arena and convert each pres object
      ArrayList entries = new ArrayList();
      ArrayList dictEntries = new ArrayList();
      
      for(int i = 0; i < arena.DictEntries.Count; i++)
      {
        Arena.ArenaDictEntry entry = (Arena.ArenaDictEntry)arena.DictEntries[i];
        switch (entry.type)
        {
          case RWObjectTypes.RWGOBJECTTYPE_VERTEXDESCRIPTOR:
            if(platform == Platform.XB2)
            {
              FW4.Xenon.VertexDeclaration vdecl = new FW4.Xenon.VertexDeclaration();
              
            }
            break;
        }
      }

      arena.DictEntries = dictEntries;
      arena.ArenaEntries = entries;

      //serialize arena to file
      SerializeArena(arena, arenaPath + "\\converted\\");
    }
  }
}