using System;

namespace FW4.renderengine.Xenon
{
    public class D3DResource : FW4.pegasus.PegasusObject
    {
      public uint Common;
      public uint ReferenceCount;
      public uint Fence;
      public uint ReadFence;
      public uint Identifier;
      public uint BaseFlush;
    }
}