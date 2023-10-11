using System.Numerics;
using PxCs.Data.Texture;

namespace PxCs.Data.Mesh
{
    public class PxLightMapData
    {
        public PxTextureData? image;
        public Vector3[] normals;
        public Vector3 origin;
    }
}