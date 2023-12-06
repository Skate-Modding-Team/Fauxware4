using FW4;
using FW4.pegasus;
using FW4.rw.core;
using FW4.rw.core.arena;
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
      arena.fileHeader.magicNumber.body = BitConverter.GetBytes(platform);
      //change the version data object
      VersionData srcversData = arena.ArenaEntries[0];
      arena.ArenaEntries[0] = versData;
      //remove non-Presentation objects from arena and convert each pres object
      ArrayList entries = new ArrayList();
      ArrayList dictEntries = new ArrayList();
      
      for(int i = 0; i < arena.ArenaDict.length(); i++)
      {
        switch()
        ConvertRWObject(arena.ArenaEntries.get(i), arena.ArenaDict.get(i).type, versData, srcversData);
      }

      arena.DictEntries = dictEntries;
      arena.ArenaEntries = entries;

      //serialize arena to file
      SerializeArena(arena, arenaPath + "\\converted\\");
    }
  }
}