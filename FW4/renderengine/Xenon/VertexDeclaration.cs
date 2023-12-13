using System;

namespace FW4.RenderEngine.Xenon
{
    public class VertexDeclaration
    {
      //D3DVertexDeclaration *m_d3dVertexDeclaration;
      uint   typesFlags;
      ushort numElements;
      ushort refCount;
      ushort instanceStreams;
      ushort pad0;
      //RenderEngine::VertexDescriptor::Element m_elements[1];
    }
}