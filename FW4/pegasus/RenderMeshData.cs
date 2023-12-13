using FW4.Pegasus;

namespace FW4.Pegasus.v25
{
  public class RenderMeshData
  {
    AABB BBox;
    uint VertexDescriptorID;
    uint MeshHelperID;
    uint IndexBufferID;
    uint VertexBufferID;
    uint NumVerts;
    //Pegasus::tRMeshData::<unnamed_tag> m_DrawParams;
    uint RemapTable;
    bool IsIndexed;
    uint NumBoneMats;
    uint NumBlendShapes;
    uint BlendShapeTable;
    uint BlendShapeNames;
  }
}