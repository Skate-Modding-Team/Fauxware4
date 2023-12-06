using FW4.pegasus;

namespace FW4.pegasus.v25
{
  public class RenderMeshData
  {
    AABB BBox;
    uint VertexDescriptorID;
    uint MeshHelperID;
    uint IndexBufferID;
    uint VertexBufferID;
    uint NumVerts;
    pegasus::tRMeshData::<unnamed_tag> m_DrawParams;
    uint RemapTable;
    bool IsIndexed;
    uint NumBoneMats;
    uint NumBlendShapes;
    uint BlendShapeTable;
    uint BlendShapeNames;
  }
}