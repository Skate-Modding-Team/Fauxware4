using FW4;
using FW4.Pegasus;
using FW4.RW;
using FW4.RW.Core;
using FW4.RW.Core.Arena;
using System.Collections;
using static FW4.ArenaSerialize;

namespace K8
{
  public static class AssetConvert
  {
    /**
     * <summary>Converts a presentation stream Arena file to the version and platform specified.</summary>
     */
    public static void ConvertPresArenaFile(string ArenaPath, VersionData versData, Platform platform)
    {
      //get Arena from file
      Arena Arena = DeserializeArenaFile(ArenaPath);
      //convert resource info in header
      Platform srcplatform = (Platform)BitConverter.ToInt32(Arena.fileHeader.magicNumber.body);
      if(srcplatform != platform)
      {
        switch(platform)
        {
          case Platform.XB2:
            Arena.m_resource = new TargetResource();
            break;
        }
      }
      //change platform bytes in header
      Arena.ArenaFileHeader newHead = Arena.fileHeader;
      Arena.ArenaFileHeaderMagicNumber newMagic = Arena.fileHeader.magicNumber;
            newMagic.body = BitConverter.GetBytes((int)Platform.XB2);
            newHead.magicNumber = newMagic;
            Arena.fileHeader = newHead;
      //change the version data object
      VersionData srcversData = (VersionData)Arena.ArenaEntries[0];
      Arena.ArenaEntries[0] = versData;
      //remove non-Presentation objects from Arena and convert each pres object
      ArrayList entries = new ArrayList();
      List<Arena.ArenaDictEntry> dictEntries = new List<Arena.ArenaDictEntry>();
      
      for(int i = 0; i < Arena.DictEntries.Count; i++)
      {
        Arena.ArenaDictEntry entry = (Arena.ArenaDictEntry)Arena.DictEntries[i];
        switch (entry.type)
        {
          case ERWObjectTypes.RWGOBJECTTYPE_VERTEXDESCRIPTOR:
            if(platform == Platform.XB2)
            {
              FW4.RenderEngine.Xenon.VertexDeclaration vdecl = new FW4.RenderEngine.Xenon.VertexDeclaration();
              
            }
            break;
        }
      }

      Arena.DictEntries = dictEntries;
      Arena.ArenaEntries = entries;

      //serialize Arena to file
      SerializeArena(Arena, ArenaPath + "\\converted\\");
    }
  }
}