using System;

namespace FW4.Revolution
{
  public class VertexDeclaration
  {
    public enum Attribute
    {
      GX_VA_PTNMTXIDX = 0,
      GX_VA_TEX0MTXIDX = 1,
      GX_VA_TEX1MTXIDX = 2,
      GX_VA_TEX2MTXIDX = 3,
      GX_VA_TEX3MTXIDX = 4,
      GX_VA_TEX4MTXIDX = 5,
      GX_VA_TEX5MTXIDX = 6,
      GX_VA_TEX6MTXIDX = 7,
      GX_VA_TEX7MTXIDX = 8,
      GX_VA_POS = 9,
      GX_VA_NRM = 10,
      GX_VA_CLR0 = 11,
      GX_VA_CLR1 = 12,
      GX_VA_TEX0 = 13,
      GX_VA_TEX1 = 14,
      GX_VA_TEX2 = 15,
      GX_VA_TEX3 = 16,
      GX_VA_TEX4 = 17,
      GX_VA_TEX5 = 18,
      GX_VA_TEX6 = 19,
      GX_VA_TEX7 = 20,
      GX_POSMTXARRAY = 21,
      GX_NRMMTXARRAY = 22,
      GX_TEXMTXARRAY = 23,
      GX_LIGHTARRAY = 24,
      GX_VA_NBT = 25,
      GX_VA_MAXATTR = 26,
      GX_VA_NULL = 0xff
    }

    public enum componentType
    {
      GX_CLR_RGB = 0,
      GX_CLR_RGBA = 1,
      GX_NRM_NBT = 1,
      GX_NRM_NBT3 = 2,
      GX_NRM_XYZ = 0,
      GX_POS_XY = 0,
      GX_POS_XYZ = 1,
      GX_TEX_S = 0,
      GX_TEX_ST = 1
    }

    public enum componentSize
    {
      GX_F32 = 4,
      GX_RGB565 = 0,
      GX_RGB8 = 1,
      GX_RGBA4 = 3,
      GX_RGBA6 = 4,
      GX_RGBA8 = 5,
      GX_RGBX8 = 2,
      GX_S16 = 3,
      GX_S8 = 1,
      GX_U16 = 2,
      GX_U8 = 0
    }
    
    public struct Declaration
    {
        public Attribute att;
        public componentType compType;
        public componentSize compSize;
        public byte fraction;
        public byte unknown;
        public byte unknown2;
        public uint offset;
    }
    
    public byte numVBuffs;
    public byte numDecls;
    public Declaration[] decls;
  }
}