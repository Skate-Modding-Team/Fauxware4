using System;
using System.IO;
using System.Numerics;

namespace K8.serialization
{
  public class Obj
  {
    public Vector3[] vertices {get; set;}
    public Vector3[] normals {get; set;}
    public Vector2[] uvs {get; set;}
    public int[] indices {get; set;}
    
    public static void WriteOBJ(String filepath)
    {
      
    }
  }
}