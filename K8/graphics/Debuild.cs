using System;
using FW4;
using FW4.rw.core.arena;
using FW4.pegasus;

namespace K8.graphics
{
  /**
   * <summary></summary>
   */
  public static class TexDebuild
  {
    public static byte[] UnswizzleWiiDXT(byte[] imageData)
    {

    }

    public static byte[] Untile360DXT(byte[] imageData)
    {
      
    }
    
    /**
     * <summary>Exports all the models, textures, and other render data in an arena to the specified path in dds and obj format.</summary>
     */
    public static void ExportArenaGraphics(Arena arena, string path)
    {
      foreach(TableOfContents toc in Arena.ArenaEntries)
      {
        
      }
    }
  }
}