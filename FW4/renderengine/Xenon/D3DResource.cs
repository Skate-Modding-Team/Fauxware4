using System;

namespace FW4.RenderEngine.Xenon
{
    public class D3DResource : FW4.RW.Core.IRWObject
    {
      public uint Common;
      public uint ReferenceCount;
      public uint Fence;
      public uint ReadFence;
      public uint Identifier;
      public uint BaseFlush;
    }
}