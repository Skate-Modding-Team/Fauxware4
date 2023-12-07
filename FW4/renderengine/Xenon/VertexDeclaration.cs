using System;

namespace FW4.renderengine.Xenon
{
    public class VertexDeclaration
    {
      //D3DVertexDeclaration *m_d3dVertexDeclaration;
      uint   typesFlags;
      ushort numElements;
      ushort refCount;
      ushort instanceStreams;
      ushort pad0;
      //renderengine::VertexDescriptor::Element m_elements[1];
    }
}